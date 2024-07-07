using Petsi.Events;
using Petsi.Services;
using System.Windows.Controls;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : UserControl
    {
        NotifyTableBuilderOverFlowWindow _overflowErrorWin;
        public ReportView()
        {
            InitializeComponent();
            ErrorService.Instance().TBOverflow += NotifyOverFlowEvent;
            ErrorService.Instance().ReportPrintEmptyInput += NotifyReportPrintNoData;
        }
        public void NotifyOverFlowEvent(object sender, EventArgs e)
        {   
            if (_overflowErrorWin == null)
            {
                _overflowErrorWin = new NotifyTableBuilderOverFlowWindow((TBOverflowEventArgs)e);
                _overflowErrorWin.Owner = System.Windows.Application.Current.MainWindow;
                _overflowErrorWin.Show();
            }
            else
            {
                _overflowErrorWin.AddItems((TBOverflowEventArgs)e);
            }
            
        }
        public void NotifyReportPrintNoData(object sender, EventArgs e)
        {
            GeneralErrorWindow window = new GeneralErrorWindow("No orders found for report.");
            window.Owner = System.Windows.Application.Current.MainWindow;
            window.Show();
        }
    }
}
