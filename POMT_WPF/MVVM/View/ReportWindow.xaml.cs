using POMT_WPF.MVVM.ViewModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        ReportWindowViewModel rwvm;
        DateTime dt1;
        DateTime dt2;
        //booleans?
        public ReportWindow()
        {
            InitializeComponent();
            rwvm = new ReportWindowViewModel();
        }

        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Print_ButtonClick(Object sender, RoutedEventArgs e)
        {
            if(dt1 != null) //boolean checks?
            {
                rwvm.ProduceReport(dt1);
            }
        }

        private void SelectFrontList_ButtonClick(Object sender, RoutedEventArgs e)
        {
            rwvm.SetFrontList();
        }
        private void SelectBackList_ButtonClick(Object sender, RoutedEventArgs e)
        {
            rwvm.SetBackList();
        }
        private void SelectWsAggList_ButtonClick(Object sender, RoutedEventArgs e)
        {
            rwvm.SetWsAggList();
        }
        private void SelectWsList_ButtonClick(Object sender, RoutedEventArgs e)
        {
            rwvm.SetWsList();
        }
        private void SetDateTime_DatePicker(Object sender, RoutedEventArgs e)
        {

        }
    }
}
