
using Petsi.Interfaces;
using Petsi.Services;
using Petsi.Utils;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class SetPastryTemplateViewModel : ViewModelBase, ITemplateService
    {
        public ObservableCollection<string> templateNames = new ObservableCollection<string>();
        ReportTemplateService rts;

        public SetPastryTemplateViewModel()
        {
            rts = ReportTemplateService.Instance();
            rts.Subscribe(this);
            templateNames = new ObservableCollection<string>(rts.GetTemplateNames());
        }

        public void Update()
        {
            var newTemplateNames = rts.GetTemplateNames();
            templateNames.Clear();
            foreach (var name in newTemplateNames)
            {
                templateNames.Add(name);
            }
        }
    }
}
