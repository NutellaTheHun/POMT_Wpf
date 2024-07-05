using Petsi.Units;
using POMT_WPF.MVVM.ViewModel;
using System.Windows.Controls;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for CatalogView.xaml
    /// </summary>
    public partial class CatalogView : UserControl
    {
        CatalogViewModel ViewModel;
        public CatalogView()
        {
            InitializeComponent();
            ViewModel = new CatalogViewModel();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.FilterSearchBar(SearchTextBox.Text);
            catalogListDataGrid.ItemsSource = ViewModel.Items;
        }

        private void AddCatalogItemButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        private void catalogListDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var catalogListDataGrid = sender as DataGrid;
            if (catalogListDataGrid != null)
            {
                var selectedItem = catalogListDataGrid.SelectedItem;
                if (selectedItem != null)
                {
                    //CatalogItemViewWindow view = new CatalogItemViewWindow(selectedItem as CatalogItemPetsi);
                    //view.Show();
                }
            }
        }
    }
}
