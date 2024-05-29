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
    /// Interaction logic for CatalogValidateErrorHandleWindow.xaml
    /// </summary>
    public partial class CatalogValidateErrorHandleWindow : Window
    {
        public CatalogValidateErrorHandleWindow()
        {
            InitializeComponent();
        }
        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void NewCatalogItem_ButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void ModifyCatalogItem_ButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
