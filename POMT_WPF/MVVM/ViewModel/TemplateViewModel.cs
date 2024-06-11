using Petsi.Services;
using Petsi.Units;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class TemplateViewModel
    {
        ReportTemplateService rts;
        string TemplateName {  get; set; }
        public ObservableCollection<BackListItem> TemplateItems { get; set; } = new ObservableCollection<BackListItem>();

        public TemplateViewModel(string? templateName)
        {
            rts = ReportTemplateService.Instance();
            if (templateName != null)
            {
                (string name, List<BackListItem> items) template = rts.GetTemplate(templateName);
                TemplateName = template.name;
                TemplateItems = new ObservableCollection<BackListItem>(template.items);
            }
        }

        public void Add(BackListItem backListItem)
        {
            TemplateItems.Add(backListItem);
        }
    }
}
