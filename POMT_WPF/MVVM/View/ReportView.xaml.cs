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
        public ReportView()
        {
            InitializeComponent();
            ErrorService.Instance().TBOverflow += NotifyOverFlowEvent;
        }
        public void NotifyOverFlowEvent(object sender, EventArgs e)
        {   /*
            TBOverflowEventArgs args = (TBOverflowEventArgs)e;
            NotifyTableBuilderOverFlow view = NotifyTableBuilderOverFlow.Instance();
            NotifyTableBuilderOverFlow.UpdateListNames(args.OverflowList);
            view.Show();
            */
            NotifyTableBuilderOverFlowWindow view = new NotifyTableBuilderOverFlowWindow((TBOverflowEventArgs)e);
            view.Show();
        }
    }
}
