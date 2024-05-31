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
            PetsiOrderWindowViewModel vm = new PetsiOrderWindowViewModel(existingOrder);
            DataContext = vm.Order;
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
            if(WeeklyRadioButton.IsChecked == false && OneTimeRadioButton.IsChecked == false)
            {
                //error message
                return;
            }
            if (OneTimeRadioButton.IsChecked == true && orderTimeTextBox.Text == null)
            {
                //error message
                return;
            }
            //test day will be set to Sunday if tryparse fails
            DayOfWeek testDay;
            if (WeeklyRadioButton.IsChecked == true && !Enum.TryParse(orderDateTextBox.Text, true, out testDay)) 
            {
                //error message
                return;
            }
            if (OrderTypeTextBox.Text == null)
            {
                //error message
                return;
            }

            vm.AddOrder((bool)PickupRadioButton.IsChecked, (bool)OneTimeRadioButton.IsChecked, orderDateTextBox.Text, orderTimeTextBox.Text);
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
    }
}
