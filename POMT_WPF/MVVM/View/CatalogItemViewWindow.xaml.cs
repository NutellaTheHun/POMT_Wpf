using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for CatalogItemViewWindow.xaml
    /// </summary>
    public partial class CatalogItemViewWindow : Window
    {
        public CatalogItemViewWindow()
        {
            InitializeComponent();
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
    }
}
