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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
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
        private void ConfigureLabelsWin_BtnClk(object sender, RoutedEventArgs e)
        {
            ConfigureLabels ConfigureLabelsWin = new ConfigureLabels();
            ConfigureLabelsWin.Show();
            //Do something

        }
        private void ConfigureTemplatesWin_BtnClk(object sender, RoutedEventArgs e)
        {
            ConfigureTemplates ConfigureTemplatesWin = new ConfigureTemplates();
            ConfigureTemplatesWin.Show();
        }
        private void ManageCatalogWin_BtnClk(object sender, RoutedEventArgs e)
        {
            CatalogListViewWindow clvw = new CatalogListViewWindow();
            clvw.Show();
        }
        private void SetRolloPrinter_BtnClk(object sender, RoutedEventArgs e)
        {

        }
        private void SetStandardPrinter_BtnClk(object sender, RoutedEventArgs e)
        {

        }
        private void SetLabelsFilePath_BtnClk(object sender, RoutedEventArgs e)
        {

        }
        private void SetPieTemplate_BtnClk(object sender, RoutedEventArgs e)
        {

        }
        private void SetpastryTemplate_BtnClk(object sender, RoutedEventArgs e)
        {

        }
    }
}
