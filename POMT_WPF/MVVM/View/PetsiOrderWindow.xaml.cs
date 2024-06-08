using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.MVVM.ObsModels;
using POMT_WPF.MVVM.View.Controls;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for PetsiOrderWindow.xaml
    /// </summary>
    public partial class PetsiOrderWindow : Window
    {
        PetsiOrderWindowViewModel _vm;
        public PetsiOrderWindowViewModel ViewModel 
        { 
            get { return _vm; }
            set
            {
                if (_vm != value)
                {
                    _vm = value;
                }
            }
        }
        bool _isExistingOrder;
        public bool IsExistingOrder 
        {
            get { return _isExistingOrder; }
            set
            {
                if (_isExistingOrder != value)
                {
                    _isExistingOrder = value;
                    CanDelete = (ViewModel.InputOriginType == Identifiers.USER_ENTERED_INPUT);
                    //if(ViewModel.InputOriginType == Identifiers.USER_ENTERED_INPUT) { CanDelete = true; }
                    //else { CanDelete = false; }
                }
            }
        }
        bool _canDelete;
        public bool CanDelete
        {
            get { return _canDelete; }
            set
            {
                if (_canDelete != value)
                {
                    _canDelete = value;
                }
            }
        }
        public PetsiOrderWindow(PetsiOrder? existingOrder, bool isExistingOrder)
        {
            InitializeComponent();
            ViewModel = new PetsiOrderWindowViewModel(existingOrder);
            IsExistingOrder = isExistingOrder;
            DataContext = this;
            orderFormDataGrid.ItemsSource = ViewModel.LineItems;
            OrderTypeComboBox.ItemsSource = ViewModel.OrderTypes;
            if(IsExistingOrder)
            {
                ViewModel.IsReadOnly = true;
                ViewModel.ItemsIsReadOnly();
                ViewModel.NotReadOnly = false;
                if (ViewModel.InputOriginType != Identifiers.USER_ENTERED_INPUT)
                {
                    editToggleButton.IsEnabled = false;
                    editToggleButton.Visibility = Visibility.Hidden;
                    AddLineButton.Visibility = Visibility.Hidden;
                    DeleteLineButton.Visibility = Visibility.Hidden;
                }                
            }
            else
            {
                ViewModel.IsReadOnly = false;
                ViewModel.ItemsNotReadOnly();
                ViewModel.NotReadOnly = true;
                editToggleButton.IsEnabled = false;
                editToggleButton.Visibility = Visibility.Hidden;
            }
        }

        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmCloseWin_BtnClk(object sender, RoutedEventArgs e)
        {
            //Validate
            if(recipientTextBox.Text == "")
            {
                PetsiOrderFormErrorWindow errorWindow = 
                    new PetsiOrderFormErrorWindow("Recipient is required.");
                errorWindow.Show();
                return;
            }
            if(PickupRadioButton.IsChecked == false && DeliveryRadioButton.IsChecked == false)
            {
                PetsiOrderFormErrorWindow errorWindow =
                    new PetsiOrderFormErrorWindow("Please select a pickup or delivery option.");
                errorWindow.Show();
                return;
            }
            if(DeliveryRadioButton.IsChecked == true 
                && DeliveryAddressTextBox.Text == ""
                && phoneTextBox.Text == "")
            {
                PetsiOrderFormErrorWindow errorWindow =
                    new PetsiOrderFormErrorWindow("Deliveries require a delivery address and phone number.");
                errorWindow.Show();
                return;
            }
            if (OrderTypeComboBox.SelectedItem == null)
            {
                PetsiOrderFormErrorWindow errorWindow =
                    new PetsiOrderFormErrorWindow("Order type is required.");
                errorWindow.Show();
                return;
            }

            if (WeeklyRadioButton.IsChecked == false && OneTimeRadioButton.IsChecked == false)
            {
                PetsiOrderFormErrorWindow errorWindow =
                    new PetsiOrderFormErrorWindow("Please select a weekly or one-time order.");
                errorWindow.Show();
                return;
            }

            if (orderDatePicker.Text == "")
            {
                PetsiOrderFormErrorWindow errorWindow =
                    new PetsiOrderFormErrorWindow("Order date is required.");
                errorWindow.Show();
                return;
            }

            DateTime testDate;
            if (OneTimeRadioButton.IsChecked == true
                && !DateTime.TryParse(orderTimeTextBox.Text, out testDate))
            {
                PetsiOrderFormErrorWindow errorWindow =
                    new PetsiOrderFormErrorWindow("fulfillment time was not valid. make sure to use AM/PM");
                errorWindow.Show();
                return;
            }
            
            if(!ViewModel.IsValidLineItems())
            {
                PetsiOrderFormErrorWindow errorWindow =
                    new PetsiOrderFormErrorWindow("Order must have at least one item, and all items filled in. (Needs a name and a quantity)");
                errorWindow.Show();
                return;
            }

            ViewModel.AddOrder(orderTimeTextBox.Text);
            Close();
        }

        private void DeleteOrder_BtnClk(object sender, RoutedEventArgs e)
        {
            ConfirmationWindow confirmationWindow = new ConfirmationWindow();
            confirmationWindow.ShowDialog();
            if (confirmationWindow.ControlBool)
            {
                ObsOrderModelSingleton.RemoveOrder(ViewModel.OrderId);
            }
            Close();
        }
        private void AddLineItem_BtnClk(object sender, RoutedEventArgs e)
        {
            ViewModel.AddLineItem(new PetsiOrderLineItem());
        }

        private void orderTimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.VMPickupTime = orderTimeTextBox.Text;
        }

        private void ItemNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isValidItem = false;
            bool hasListofResults = false;


            TextFillTextBox itemNameTextBox = (TextFillTextBox)sender;
            if (ViewModel.ValidateItemName(itemNameTextBox.Text))
            {
                isValidItem = true;
            }
            else
            {
                Grid grid = itemNameTextBox.Parent as Grid;
                ComboBox itemNameCb = grid.FindName("itemNameComboBox") as ComboBox;
                string itemName = itemNameTextBox.Text;
                List<CatalogItemPetsi> results = ViewModel.GetItemMatchResults(itemName);

                itemNameCb.ItemsSource = results.Select(x => x.ItemName);
                
                if (results.Count != 0)
                {
                    itemNameCb.IsDropDownOpen = true;
                }
                if (results.Count == 0)
                {
                    
                }
                else
                {
                    hasListofResults = true;
                }
            }
        }

        private void ItemNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //# D64933 chili red
            ComboBox comboBox = (ComboBox)sender;
            Grid grid = comboBox.Parent as Grid;
            TextFillTextBox itemNameTextBox = grid.FindName("ItemNameTextBox") as TextFillTextBox;
            if (!ViewModel.IsValidItem(itemNameTextBox.Text))
            {
                BrushConverter brushConverter = new BrushConverter();
                Brush brush = (Brush)brushConverter.ConvertFromString("#D64933");
                itemNameTextBox.Background = brush;
            }
            else
            {
                BrushConverter brushConverter = new BrushConverter();
                Brush brush = (Brush)brushConverter.ConvertFromString("#CCD7E1");
                itemNameTextBox.Background = brush;
            }
        }
        private void itemNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //#D64933 chili red
            ComboBox comboBox = (ComboBox)sender;
            Grid grid = comboBox.Parent as Grid;
            TextFillTextBox itemNameTextBox = grid.FindName("ItemNameTextBox") as TextFillTextBox;
            if (comboBox.SelectedItem != null)
            {
                itemNameTextBox.Text = comboBox.SelectedItem.ToString();
                TextFillTextBox idTextBox = grid.FindName("testcatalogObjId") as TextFillTextBox;
                if (ViewModel.ValidateItemName(itemNameTextBox.Text))
                {
                    
                }
                else
                {
                    BrushConverter brushConverter = new BrushConverter();
                    Brush brush = (Brush)brushConverter.ConvertFromString("#D64933");
                    itemNameTextBox.Background = brush;
                }
            }
        }

        private void itemNameComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
             //#D64933 chili red
            ComboBox comboBox = (ComboBox)sender;
            Grid grid = comboBox.Parent as Grid;
            TextFillTextBox itemNameTextBox = grid.FindName("ItemNameTextBox") as TextFillTextBox;

            if(!ViewModel.IsValidItem((string)comboBox.SelectedItem))
            {
                BrushConverter brushConverter = new BrushConverter();
                Brush brush = (Brush)brushConverter.ConvertFromString("#D64933");
                itemNameTextBox.Background = brush;
            }
            else
            {
                BrushConverter brushConverter = new BrushConverter();
                Brush brush = (Brush)brushConverter.ConvertFromString("#CCD7E1");
                itemNameTextBox.Background = brush;
            }
        }

        private void editToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.IsReadOnly) { ViewModel.IsReadOnly = false; ViewModel.NotReadOnly = true; ViewModel.ItemsNotReadOnly(); }
            else { ViewModel.IsReadOnly = true; ViewModel.ItemsIsReadOnly(); ViewModel.NotReadOnly = false; }
        }

        private void DeleteLineButton_Click(object sender, RoutedEventArgs e)
        {
            if(orderFormDataGrid.SelectedItem != null)
            {
                bool deleteConfirmation = false;
                ConfirmationWindow confirmationWindow = new ConfirmationWindow();
                confirmationWindow.ShowDialog();
                if (confirmationWindow.ControlBool)
                {
                    ViewModel.LineItems.Remove((PetsiOrderLineItem)orderFormDataGrid.SelectedItem);
                }      
            }
        }
    }
}
