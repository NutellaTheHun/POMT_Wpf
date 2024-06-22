using Petsi.Events;
using Petsi.Services;
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
        public ReportWindow()
        {
            InitializeComponent();
            rwvm = new ReportWindowViewModel();
            ErrorService.Instance().TBOverflow += NotifyOverFlowEvent;
        }

        public void NotifyOverFlowEvent(object sender, EventArgs e)//Error service is sender?
        {
            TBOverflowEventArgs args = (TBOverflowEventArgs)e;
            NotifyTableBuilderOverFlow view = NotifyTableBuilderOverFlow.Instance();
            NotifyTableBuilderOverFlow.UpdateListNames(args.OverflowList);
            view.Show();
        }

        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Print_ButtonClick(Object sender, RoutedEventArgs e)
        {
            DateTime? selectedStartDate = datePickerStart.SelectedDate;
            DateTime? selectedEndDate = datePickerEnd.SelectedDate;

            if (selectedStartDate.HasValue)
            {
                rwvm.ProduceReport((DateTime)selectedStartDate, (DateTime?)selectedEndDate);
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
    }
}
