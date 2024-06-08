using Petsi.Units;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for CatalogItemViewWindow.xaml
    /// </summary>
    public partial class CatalogItemViewWindow : Window
    {
        CatalogItemViewModel civm;
        public CatalogItemViewWindow(CatalogItemPetsi? item)
        {
            InitializeComponent();
            civm = new CatalogItemViewModel(item);
            DataContext = civm;
        }
        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmCloseWin_BtnClk(object sender, RoutedEventArgs e)
        {
            //Error Handle form
            Close();
        }

        private void Delete_BtnClk(object sender, RoutedEventArgs e)
        {
            //Are you sure window?
            //delete
            Close();
        }
        
    }
}
