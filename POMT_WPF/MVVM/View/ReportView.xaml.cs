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
        }
        public void NotifyOverFlowEvent(object sender, EventArgs e)
        {   
            if (_overflowErrorWin == null)
            {
                _overflowErrorWin = new NotifyTableBuilderOverFlowWindow((TBOverflowEventArgs)e);
                _overflowErrorWin.Show();
            }
            else
            {
                _overflowErrorWin.AddItems((TBOverflowEventArgs)e);
            }
            
        }
    }
}
