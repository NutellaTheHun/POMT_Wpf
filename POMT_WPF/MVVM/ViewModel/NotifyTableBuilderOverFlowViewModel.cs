using Petsi.Events;
using POMT_WPF.Core;
using POMT_WPF.MVVM.View;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    class NotifyTableBuilderOverFlowViewModel
    {
        private NotifyTableBuilderOverFlowWindow _view;
        public ObservableCollection<string> OverflowListNames { get; set; }

        public RelayCommand Close {  get; set; }
        public NotifyTableBuilderOverFlowViewModel(TBOverflowEventArgs args, NotifyTableBuilderOverFlowWindow view)
        {
            _view = view;
            OverflowListNames = new ObservableCollection<string>(args.OverflowList.Select(x => x.ItemName));
            Close = new RelayCommand(o => { _view.Close(); });
        }
    }
}
