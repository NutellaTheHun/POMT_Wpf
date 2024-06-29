using POMT_WPF.MVVM.ViewModel;
using System.Windows.Controls;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        SettingsViewModel ViewModel;
        public SettingsView()
        {
            InitializeComponent();
            ViewModel = new SettingsViewModel();
            DataContext = ViewModel;
        }

        private void SetLabelPrinterButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void SetStandardPrinterButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void SetLabelFilePathButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void SetPieTemplateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void SetPastryTemplateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ConfigureLabelsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ConfigureTemplatesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ManageCatalogButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
