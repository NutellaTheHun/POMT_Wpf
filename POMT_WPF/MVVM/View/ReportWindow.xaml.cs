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
        bool reportSelected;
        public ReportWindow()
        {
            InitializeComponent();
            rwvm = new ReportWindowViewModel();
            reportSelected = false;
            dt1 = default;
            dt2 = default;
        }

        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Print_ButtonClick(Object sender, RoutedEventArgs e)
        {
            if(dt1 != default && reportSelected) //boolean checks?
            {
                rwvm.ProduceReport(dt1);
            }
        }

        private void SelectFrontList_ButtonClick(Object sender, RoutedEventArgs e)
        {
            rwvm.SetFrontList();
            reportSelected = true;
        }
        private void SelectBackList_ButtonClick(Object sender, RoutedEventArgs e)
        {
            rwvm.SetBackList();
            reportSelected = true;
        }
        private void SelectWsAggList_ButtonClick(Object sender, RoutedEventArgs e)
        {
            rwvm.SetWsAggList();
            reportSelected = true;
        }
        private void SelectWsList_ButtonClick(Object sender, RoutedEventArgs e)
        {
            rwvm.SetWsList();
            reportSelected = true;
        }
        private void SetDateTime_DatePicker(Object sender, RoutedEventArgs e)
        {

        }
    }
}
