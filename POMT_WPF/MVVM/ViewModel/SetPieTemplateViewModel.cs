
using Petsi.Interfaces;
using Petsi.Services;
using Petsi.Utils;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class SetPieTemplateViewModel : ViewModelBase, ITemplateService
    {
        public ObservableCollection<string> templateNames = new ObservableCollection<string>();
        ReportTemplateService rts;
       

        public SetPieTemplateViewModel()
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

        public void SetPieTemplate(string? v)
        {
            if(v != null)
            {
                PetsiConfig.GetInstance().SetValue(Identifiers.SETTING_PIE_TEMPLATE, v);
            }
        }
    }
}
