using System.Windows.Controls;
using Petsi.Services;
using Petsi.Events;
using Petsi.Units;
using POMT_WPF.MVVM.ViewModel;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {
        public OrderView()
        {
            InitializeComponent();
            ErrorService.Instance().SoiNewItem += NotifyUserNewItem;
            ErrorService.Instance().SoiMultiItem += NotifyUserMultiItemMatch;
            ErrorService.Instance().NewStartupEvent += NotifyUserSquareKeyMissing;
            ErrorService.RaiseOrderViewEvents();
            PetsiOrder viewedOrder = MainViewModel.Instance().viewedOrderItem;
            if (viewedOrder != null)
            {
                dashboardDataGrid.ScrollIntoView(viewedOrder);
                dashboardDataGrid.SelectedItem = viewedOrder;
                SetFilterRadioButton();
            }
        }

        private void SetFilterRadioButton()
        {
            string activeFilter = MainViewModel.Instance().orderViewFilter;
            switch (activeFilter) 
            {
                case "All_rb":
                    All_rb.IsChecked = true;
                    break;
                case "Wholesale_rb":
                    Wholesale_rb.IsChecked = true;
                    break;
                case "Square_rb":
                    Square_rb.IsChecked = true;
                    break;
                case "Retail_rb":
                    Retail_rb.IsChecked = true;
                    break;
                case "Special_rb":
                    Special_rb.IsChecked = true;
                    break;
                case "Frozen_rb":
                    Frozen_rb.IsChecked = true;
                    break;
                case "Farmer_rb":
                    Farmer_rb.IsChecked = true;
                    break;
                case "History_rb":
                    History_rb.IsChecked = true;
                    break;
            }
        }

        public void NotifyUserNewItem(object sender, EventArgs e)
        {
            NewItemEventWindow window = new NewItemEventWindow((SoiNewItemEventArgs)e);
            window.Owner = System.Windows.Application.Current.MainWindow;
            window.Show();
        }

        public void NotifyUserMultiItemMatch(object sender, EventArgs e)
        {
            NotifyMultiItemMatchWindow view = new NotifyMultiItemMatchWindow((SoiMultiItemEventArgs)e);
            view.Owner = System.Windows.Application.Current.MainWindow;
            view.Show();
        }

        public void NotifyUserSquareKeyMissing(object sender, EventArgs e)
        {
            SquareKeyMissingWindow view = new SquareKeyMissingWindow();
            view.Owner = System.Windows.Application.Current.MainWindow;
            view.Show();
        }

        private void All_rb_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainViewModel.Instance().orderViewFilter = "All_rb";
        }

        private void Wholesale_rb_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainViewModel.Instance().orderViewFilter = "Wholesale_rb";
        }

        private void Square_rb_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainViewModel.Instance().orderViewFilter = "Square_rb";
        }

        private void Retail_rb_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainViewModel.Instance().orderViewFilter = "Retail_rb";
        }

        private void Special_rb_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainViewModel.Instance().orderViewFilter = "Special_rb";
        }

        private void Farmers_rb_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainViewModel.Instance().orderViewFilter = "Farmer_rb";
        }

        private void Frozen_rb_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainViewModel.Instance().orderViewFilter = "Frozen_rb";
        }

        private void History_rb_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainViewModel.Instance().orderViewFilter = "History_rb";
        }
    }
}

