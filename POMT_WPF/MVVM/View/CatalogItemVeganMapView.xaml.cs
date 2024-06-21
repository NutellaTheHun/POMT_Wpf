using Petsi.Units;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for CatalogItemVeganMapView.xaml
    /// </summary>
    public partial class CatalogItemVeganMapView : Window
    {
        CatalogListViewWindowModel viewModel;
        public CatalogItemPetsi selection;
        public CatalogItemVeganMapView()
        {
            InitializeComponent();
            viewModel = new CatalogListViewWindowModel();
            catalogVeganMapperListDataGrid.ItemsSource = viewModel.Items;
            catalogVeganMapperListDataGrid.SelectionChanged += CatalogListDataGrid_SelectionChanged;
            //catalogVeganMapperListDataGrid.LostFocus += CatalogListDataGrid_LostFocus;
            //selection = null;
        }

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            selection = null;
            Close();
        }

        private void CatalogListDataGrid_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var catalogListDataGrid = sender as DataGrid;
            if (catalogListDataGrid.SelectedItem != null && catalogListDataGrid.SelectedItem.GetType() == typeof(CatalogItemPetsi))
            {
                selection = (CatalogItemPetsi)catalogListDataGrid.SelectedItem;
            }
        }

        private void CatalogListDataGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            var catalogListDataGrid = sender as DataGrid;
            if (catalogListDataGrid.SelectedItem != null)
            {
                selection = (CatalogItemPetsi)catalogListDataGrid.SelectedItem;
            }
        }

        private void doneBttn_Click(object sender, RoutedEventArgs e)
        {
            if (selection != null)
            {
                Close();
            }
            else
            {
                PetsiOrderFormErrorWindow errorWin = new PetsiOrderFormErrorWindow("Please select an item");
                errorWin.ShowDialog();
            }
        }

        private void cancelBttn_Click(object sender, RoutedEventArgs e)
        {
            selection = null;
            Close();
        }
    }
}
