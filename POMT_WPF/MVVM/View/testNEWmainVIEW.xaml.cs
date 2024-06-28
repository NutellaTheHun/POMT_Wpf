using System.Windows;
using POMT_WPF.MVVM.ViewModel;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for testNEWmainVIEW.xaml
    /// </summary>
    public partial class testNEWmainVIEW : Window
    {
        MainViewModel vm; 
        public testNEWmainVIEW()
        {
            vm = new MainViewModel();
            DataContext = vm;
            InitializeComponent();
        }

        private void CatalogButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LabelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
