using POMT_WPF.MVVM.ViewModel;
using System.Windows;

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
    }
}
