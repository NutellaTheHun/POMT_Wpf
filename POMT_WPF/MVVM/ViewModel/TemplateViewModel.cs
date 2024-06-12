using DocumentFormat.OpenXml.Office2010.Excel;
using Petsi.Managers;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class TemplateViewModel
    {
        ReportTemplateService rts;
        CatalogService cs;
        string TemplateName {  get; set; }
        public ObservableCollection<BackListItem> TemplateItems { get; set; } = new ObservableCollection<BackListItem>();

        public TemplateViewModel(string? templateName)
        {
            rts = ReportTemplateService.Instance();
            cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATEGORY);
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

        public bool IsValidItemName(string text)
        {
            string result = cs.GetCatalogObjectId(text);
            if(result == "") { return false; }
            return true;
        }

        public bool ValidateItemName(string? text)
        {
            BackListItem templateitem = TemplateItems.First(x => x.ItemName == text);
            if (templateitem == null) { return false; }
            string id = "";
            if (cs.TryValidateItemName(text, out id))
            {
                templateitem.CatalogObjId = id;
                return true;
            }
            templateitem.CatalogObjId = "";
            return false;
        }

        public List<CatalogItemPetsi> GetItemMatchResults(string itemName)
        {
            return cs.GetItemNameValidationResults(itemName);
        }

        public bool IsValidTemplate()
        {
            if(TemplateName == "" || TemplateName == null) { return false; }
            if(TemplateItems.Count == 0) { return false; };
            foreach (BackListItem item in TemplateItems)
            {
                if(item.ItemName == "" || item.ItemName == null) { return false; }
                if(item.CatalogObjId == "" || item.CatalogObjId==null) { return false; }
                if(item.PageDisplayName == "" || item.PageDisplayName == null) { return false; }
            }
            return true;
        }

        public void SaveTemplate()
        {
            List<BackListItem> backListItems = TemplateItems.ToList();
            rts.AddTemplate((TemplateName, backListItems));
        }
    }
}
