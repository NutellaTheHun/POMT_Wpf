using Petsi.Events;
using Petsi.Services;
using POMT_WPF.MVVM.ViewModel;
using System.Windows.Controls;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : UserControl
    {
        ReportViewModel ViewModel;
        public ReportView()
        {
            InitializeComponent();
            new ReportViewModel();
            ErrorService.Instance().TBOverflow += NotifyOverFlowEvent;
        }
        public void NotifyOverFlowEvent(object sender, EventArgs e)//Error service is sender?
        {
            TBOverflowEventArgs args = (TBOverflowEventArgs)e;
            NotifyTableBuilderOverFlow view = NotifyTableBuilderOverFlow.Instance();
            NotifyTableBuilderOverFlow.UpdateListNames(args.OverflowList);
            view.Show();
        }
        private void SetPieTemplateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void SetPastryTemplateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void PrintWholesaleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void PrintWholesaleAggButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void PrintFrontListButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void PrintBackListButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ManageCatalogButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
