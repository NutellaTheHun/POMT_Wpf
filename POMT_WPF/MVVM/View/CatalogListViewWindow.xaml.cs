using Petsi.Units;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for CatalogListViewWindow.xaml
    /// </summary>
    public partial class CatalogListViewWindow : Window
    {
        CatalogListViewWindowModel viewModel;
        public CatalogListViewWindow()
        {
            InitializeComponent();
            viewModel = new CatalogListViewWindowModel();
            catalogListDataGrid.ItemsSource = viewModel.Items;
            catalogListDataGrid.MouseDoubleClick += catalogListDataGrid_MouseDoubleClick;
        }

        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmCloseWin_BtnClk(object sender, RoutedEventArgs e)
        {
            //Do something
            Close();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.FilterSearchBar(txtFilter.Text);
            catalogListDataGrid.ItemsSource = viewModel.Items;
        }

        private void AddCatalogItemBtn_Click(object sender, RoutedEventArgs e)
        {
            CatalogItemViewWindow view = new CatalogItemViewWindow(null);
            view.Show();
        }

        private void catalogListDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var catalogListDataGrid = sender as DataGrid;
            if (catalogListDataGrid != null)
            {
                var selectedItem = catalogListDataGrid.SelectedItem;
                if (selectedItem != null)
                {
                    CatalogItemViewWindow view = new CatalogItemViewWindow(selectedItem as CatalogItemPetsi);
                    view.Show();
                }
            }
        }
    }
}
