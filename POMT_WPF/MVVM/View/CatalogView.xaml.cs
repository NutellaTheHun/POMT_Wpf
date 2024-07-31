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
        ScrollViewer scrollViewer;
        
        public CatalogView()
        {
            InitializeComponent();
            ViewModel = new CatalogViewModel();
            catalogListDataGrid.Focus();
            CatalogItemPetsi viewedItem = MainViewModel.Instance().viewedCatalogItem;
            if (viewedItem != null)
            {
                catalogListDataGrid.ScrollIntoView(viewedItem);
                catalogListDataGrid.SelectedItem = viewedItem;
            }        
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.FilterSearchBar(SearchTextBox.Text);
            catalogListDataGrid.ItemsSource = ViewModel.Items;
        }
    }
}
