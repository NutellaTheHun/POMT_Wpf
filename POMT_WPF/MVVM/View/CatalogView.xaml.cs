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

    }
}
