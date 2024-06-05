using DocumentFormat.OpenXml.Packaging;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;


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
                    if(ViewModel.Order.InputOriginType == Identifiers.WHOLESALE_INPUT 
                        || ViewModel.Order.InputOriginType == Identifiers.ONE_SHOT_INPUT) 
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

        private void textChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            
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
                && !DateTime.TryParse(orderTimeTextBox.Text + orderTimeComboBox.Text, out testDate))
            {
                PetsiOrderFormErrorWindow errorWindow =
                    new PetsiOrderFormErrorWindow("Order time input was not accepted.");
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

            //ADD ORDER IF NEW!! CAN DELETE IF EXISTS
            ViewModel.AddOrder(orderTimeTextBox.Text + orderTimeComboBox.Text);
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

        private void orderDateTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
