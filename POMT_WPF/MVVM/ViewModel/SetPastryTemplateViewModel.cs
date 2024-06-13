
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
            //templateNames = new ObservableCollection<string>(rts.GetTemplateNames());
            var newTemplateNames = rts.GetTemplateNames();
            templateNames.Clear();
            foreach (var name in newTemplateNames)
            {
                templateNames.Add(name);
            }
        }

        public void SetPastryTemplate(string? v)
        {
            if (v != null)
            {
                PetsiConfig.GetInstance().SetValue(Identifiers.SETTING_PASTRY_TEMPLATE, v);
            }
        }
    }
}
