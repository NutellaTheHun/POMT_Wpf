using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


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
