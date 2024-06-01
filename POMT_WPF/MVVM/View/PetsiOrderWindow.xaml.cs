using Petsi.Units;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;


namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for PetsiOrderWindow.xaml
    /// </summary>
    public partial class PetsiOrderWindow : Window
    {
        PetsiOrderWindowViewModel vm;

        public PetsiOrderWindow(PetsiOrder? existingOrder)
        {
            InitializeComponent();
            vm = new PetsiOrderWindowViewModel(existingOrder);
            DataContext = vm;
            orderFormDataGrid.ItemsSource = vm.Order.LineItems;
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
            /*
            if(orderFormDataGrid.Items.Count == 0 || !AllLineItemsComplete())
            {
                PetsiOrderFormErrorWindow errorWindow =
                    new PetsiOrderFormErrorWindow("Order must have at least one item, and all items filled in.");
                errorWindow.Show();
                return;
            }
            */
            
            //ADD ORDER IF NEW!! CAN DELETE IF EXISTS
            vm.AddOrder(orderTimeTextBox.Text + orderTimeComboBox.Text);
            Close();
        }

        private bool AllLineItemsComplete()
        {
            throw new NotImplementedException();
        }

        private void Delete_BtnClk(object sender, RoutedEventArgs e)
        {
            //Are you sure window?
            //delete
            Close();
        }
        private void AddLineItem_BtnClk(object sender, RoutedEventArgs e)
        {
            vm.AddLineItem(new PetsiOrderLineItem());
        }

        private void orderDateTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
