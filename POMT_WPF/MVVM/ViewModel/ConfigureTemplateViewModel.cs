using Petsi.Interfaces;
using Petsi.Services;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class ConfigureTemplateViewModel : ViewModelBase, ITemplateService
    {
        public ObservableCollection<string> templateNames = new ObservableCollection<string>();
        ReportTemplateService rts;
        public ConfigureTemplateViewModel()
        {
            rts = ReportTemplateService.Instance();
            rts.Subscribe(this);
            templateNames = new ObservableCollection<string>(rts.GetTemplateNames());
        }

        public void Update()
        {
            //templateNames = new ObservableCollection<string>(rts.GetTemplateNames());
            var newTemplateNames = rts.GetTemplateNames();
            templateNames.Clear();
            foreach (var name in newTemplateNames)
            {
                templateNames.Add(name);
            }
        }

        public void RemoveTemplate(string name)
        {
            rts.RemoveTemplate(name);
        }
    }
}
