using Petsi.Events;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {
        OrderViewModel ViewModel { get; set; }
        public OrderView()
        {
            ViewModel = new OrderViewModel();
            InitializeComponent();
            ErrorService.Instance().SoiNewItem += NotifyUserNewItem;
            ErrorService.Instance().SoiMultiItem += NotifyUserMultiItemMatch;

            dashboardDataGrid.ItemsSource = ViewModel.Orders;
            //dashboardDataGrid.MouseDoubleClick += DashboardDataGrid_MouseDoubleClick;

            DataContext = ViewModel;
            ErrorService.RaiseMainWindowEvents();
        }

        public void NotifyUserNewItem(object sender, EventArgs e)
        {
            NotifyNewCatalogItemView view = new NotifyNewCatalogItemView((SoiNewItemEventArgs)e);
            view.Show();
        }

        public void NotifyUserMultiItemMatch(object sender, EventArgs e)
        {
            SoiMultiItemEventArgs args = (SoiMultiItemEventArgs)e;
            NotifyCatalogValidateMultiItemView view = new NotifyCatalogValidateMultiItemView();
            view.UpdateListNames(args.MultItemList);
            view.SetItemContext(args.ItemContext);
            view.Show();
        }

        private void DashboardDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dashboardDataGrid = sender as DataGrid;
            if (dashboardDataGrid != null)
            {
                var selectedItem = dashboardDataGrid.SelectedItem;
                if (selectedItem != null)
                {
                    PetsiOrderWindow petsiOrderWin = new PetsiOrderWindow(selectedItem as PetsiOrder, true);
                    petsiOrderWin.ShowDialog();
                }
            }
        }

        private void DashboardDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dashboardDataGrid = sender as DataGrid;
            if (dashboardDataGrid != null)
            {
                var selectedItem = dashboardDataGrid.SelectedItem;
                if (selectedItem != null)
                {
                    PetsiOrderWindow petsiOrderWin = new PetsiOrderWindow(selectedItem as PetsiOrder, true);
                    petsiOrderWin.ShowDialog();
                    ViewModel.UpdateOrderList();
                }
            }
        }

        private void AddOrder_ButtonClick(object sender, RoutedEventArgs e)
        {
            PetsiOrderWindow petsiOrderWin = new PetsiOrderWindow(null, false);
            petsiOrderWin.ShowDialog();
            ViewModel.UpdateOrderList();

        }
        public bool FrozenOrdersSelected { get; private set; }
        public void UpdateDataGrid()
        {
            if (FrozenOrdersSelected) { dashboardDataGrid.ItemsSource = ViewModel.FrozenOrders; }
            else
            {
                dashboardDataGrid.ItemsSource = ViewModel.Orders;
            }
        }
        private void FilterAll_Button_Click(object sender, RoutedEventArgs e)
        {
            FrozenOrdersSelected = false;
            ViewModel.FilterOrderType(null);
            dashboardDataGrid.ItemsSource = ViewModel.Orders;
        }
        private void FilterWholesale_Button_Click(object sender, RoutedEventArgs e)
        {
            FrozenOrdersSelected = false;
            ViewModel.FilterOrderType(Identifiers.ORDER_TYPE_WHOLESALE);
            dashboardDataGrid.ItemsSource = ViewModel.Orders;
        }
        private void FilterSquare_Button_Click(object sender, RoutedEventArgs e)
        {
            FrozenOrdersSelected = false;
            ViewModel.FilterOrderType(Identifiers.ORDER_TYPE_SQUARE);
            dashboardDataGrid.ItemsSource = ViewModel.Orders;
        }
        private void FilterSpecial_Button_Click(object sender, RoutedEventArgs e)
        {
            FrozenOrdersSelected = false;
            ViewModel.FilterOrderType(Identifiers.ORDER_TYPE_SPECIAL);
            dashboardDataGrid.ItemsSource = ViewModel.Orders;
        }

        private void FilterRetail_Button_Click(object sender, RoutedEventArgs e)
        {
            FrozenOrdersSelected = false;
            ViewModel.FilterOrderType(Identifiers.ORDER_TYPE_RETAIL);
            dashboardDataGrid.ItemsSource = ViewModel.Orders;
        }

        private void FilterFrozen_Button_Click(object sender, RoutedEventArgs e)
        {
            FrozenOrdersSelected = true;
            dashboardDataGrid.ItemsSource = ViewModel.FrozenOrders;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.FilterSearchBar(txtFilter.Text);
            dashboardDataGrid.ItemsSource = ViewModel.Orders;
        }
    }
}

