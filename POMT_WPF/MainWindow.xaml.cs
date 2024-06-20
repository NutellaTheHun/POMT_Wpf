using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.MVVM.View;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace POMT_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainWindowViewModel();
            dashboardDataGrid.ItemsSource = viewModel.Orders;
            dashboardDataGrid.MouseDoubleClick += DashboardDataGrid_MouseDoubleClick;
            DataContext = viewModel;
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
                    petsiOrderWin.Show();
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) { this.DragMove(); }
        }


        private bool isMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (isMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    isMaximized = false;
                }
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                isMaximized = true;
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
                    petsiOrderWin.Show();
                    viewModel.UpdateOrderList();
                }
            }
        }

        private void AddOrder_ButtonClick(object sender, RoutedEventArgs e)
        {
            PetsiOrderWindow petsiOrderWin = new PetsiOrderWindow(null, false);
            petsiOrderWin.ShowDialog();
            viewModel.UpdateOrderList();

        }
        private void ReportWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            ReportWindow reportWin = new ReportWindow();
            reportWin.Show();
        }
        private void LabelWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            LabelWindow LabelWin = new LabelWindow();
            LabelWin.Show();
        }

        private void SettingsWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            SettingsWindow SettingsWin = new SettingsWindow();
            SettingsWin.Show();
        }

        private void CloseMainWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void FilterAll_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FilterOrderType(null);
            dashboardDataGrid.ItemsSource = viewModel.Orders;
        }
        private void FilterWholesale_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FilterOrderType(Identifiers.ORDER_TYPE_WHOLESALE);
            dashboardDataGrid.ItemsSource = viewModel.Orders;
        }
        private void FilterSquare_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FilterOrderType(Identifiers.ORDER_TYPE_SQUARE);
            dashboardDataGrid.ItemsSource = viewModel.Orders;
        }
        private void FilterOther_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FilterOrderType(Identifiers.ORDER_TYPE_SPECIAL);
            dashboardDataGrid.ItemsSource = viewModel.Orders;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.FilterSearchBar(txtFilter.Text);
            dashboardDataGrid.ItemsSource = viewModel.Orders;
        }
    }
}