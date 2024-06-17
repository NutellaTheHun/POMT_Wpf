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
        public CatalogListViewWindow()
        {
            InitializeComponent();
            CatalogListViewWindowModel model = new CatalogListViewWindowModel();
            catalogListDataGrid.ItemsSource = model.Items;
            catalogListDataGrid.SelectionChanged += CatalogListDataGrid_SelectionChanged;
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

        private void AddLineItem_BtnClk(object sender, RoutedEventArgs e)
        {

        }

        private void CatalogListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var catalogListDataGrid = sender as DataGrid;
            if (catalogListDataGrid != null)
            {
                var selectedItem = catalogListDataGrid.SelectedItem;
                if (selectedItem != null)
                {
                    CatalogItemViewWindow civw = new CatalogItemViewWindow(selectedItem as CatalogItemPetsi);
                    civw.Show();
                }
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
