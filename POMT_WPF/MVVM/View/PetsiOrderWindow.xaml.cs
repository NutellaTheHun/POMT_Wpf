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
            if(recipientTextBox.Text == null)
            {
                //error message
                return;
            }
            if(PickupRadioButton.IsChecked == false && DeliveryRadioButton.IsChecked == false)
            {
                //error message
                return;
            }
            if(DeliveryRadioButton.IsChecked == true 
                && DeliveryAddressTextBox.Text == null 
                && phoneTextBox.Text == null)
            {
                //Error message
                return;
            }
            if (OrderTypeTextBox.Text == null)
            {
                //error message
                return;
            }

            if (WeeklyRadioButton.IsChecked == false && OneTimeRadioButton.IsChecked == false)
            {
                //error message
                return;
            }

            if (orderDatePicker.Text == null)
            {
                //error message
                return;
            }

            DateTime testDate;
            if (OneTimeRadioButton.IsChecked == true
                && !DateTime.TryParse(orderTimeTextBox.Text + orderTimeComboBox.Text, out testDate))
            {
                //error message
                return;
            }

            
            
            //ADD ORDER IF NEW!! CAN DELETE IF EXISTS
            vm.AddOrder(orderTimeTextBox.Text + orderTimeComboBox.Text);
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
            vm.AddLineItem(new PetsiOrderLineItem());
        }

        private void orderDateTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
