using DocumentFormat.OpenXml.Office2010.Excel;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.MVVM.View.Controls;
using POMT_WPF.MVVM.ViewModel;
using System.Diagnostics;
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
                    if(ViewModel.InputOriginType == Identifiers.WHOLESALE_INPUT 
                        || ViewModel.InputOriginType == Identifiers.ONE_SHOT_INPUT) 
                    { CanDelete = true; }
                    else { CanDelete = false; }
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
            if (OrderTypeTextBox.Text == "")
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

        private void Delete_BtnClk(object sender, RoutedEventArgs e)
        {
            //Are you sure window?
            //delete
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
            //if text == catalogItemName
            //  set catalog Obj Id
            //else
            //  get list of matches
            //  click match => textChangedEvent?

            bool isValidItem = false;
            bool hasListofResults = false;


            TextFillTextBox itemNameTextBox = (TextFillTextBox)sender;
            Trace.WriteLine(itemNameTextBox.Text);
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

            //if(itemNameTextBox.IsFocused == false && !isValidItem)
            //{
            //    BrushConverter brushConverter = new BrushConverter();
            //    Brush brush = (Brush)brushConverter.ConvertFromString("#D64933");
            //    itemNameTextBox.Background = brush;
            //}
            //else
            //{
            //    BrushConverter brushConverter = new BrushConverter();
            //    Brush brush = (Brush)brushConverter.ConvertFromString("#F7FFF7");
            //    itemNameTextBox.Background = brush;
            //}
        }

        private void ItemNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //# D64933 chili red
            TextFillTextBox itemNameTextBox = (TextFillTextBox)sender;
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

        private void itemNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //#D64933 chili red
            //ComboBox comboBox = (ComboBox)sender;
            //Grid grid = comboBox.Parent as Grid;
            //TextFillTextBox itemNameTextBox = grid.FindName("ItemNameTextBox") as TextFillTextBox;
            //if (!ViewModel.IsValidItem((string)comboBox.SelectedItem))
            //{
            //    BrushConverter brushConverter = new BrushConverter();
            //    Brush brush = (Brush)brushConverter.ConvertFromString("#D64933");
            //    itemNameTextBox.Background = brush;
            //}
            //else
            //{
            //    BrushConverter brushConverter = new BrushConverter();
            //    Brush brush = (Brush)brushConverter.ConvertFromString("#CCD7E1");
            //    itemNameTextBox.Background = brush;
            //}
        }
    }
}
