
using Petsi.Services;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class ConfigureTemplateViewModel
    {
        public ObservableCollection<string> templateNames = new ObservableCollection<string>();
        ReportTemplateService rts;
        public ConfigureTemplateViewModel()
        {
            rts = ReportTemplateService.Instance();
            templateNames = new ObservableCollection<string>(rts.GetTemplateNames());
        }
    }
}
