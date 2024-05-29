using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
