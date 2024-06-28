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
    }
}
