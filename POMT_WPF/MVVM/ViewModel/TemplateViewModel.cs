using Petsi.Managers;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class TemplateViewModel : ViewModelBase
    {
        ReportTemplateService rts;
        CatalogService cs;

        private string _templateName;
        public string TemplateName
        {
            get { return _templateName; }
            set
            {
                if (_templateName != value)
                {
                    _templateName = value;
                    OnPropertyChanged(nameof(TemplateName));
                }
            }
        }
        public ObservableCollection<BackListItem> TemplateItems { get; set; } = new ObservableCollection<BackListItem>();

        public TemplateViewModel(string? templateName)
        {
            rts = ReportTemplateService.Instance();
            cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            if (templateName != null)
            {
                List<BackListItem> templateItems = rts.GetTemplate(templateName);
                if (templateItems != null)
                {
                    TemplateName = templateName;
                    TemplateItems = new ObservableCollection<BackListItem>(templateItems);
                } 
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
            BackListItem templateItem = TemplateItems.First(x => x.ItemName == text);
            if (templateItem == null) { return false; }
            string id = "";
            if (cs.TryValidateItemName(text, out id))
            {
                templateItem.CatalogObjId = id;
                return true;
            }
            templateItem.CatalogObjId = "";
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

        public void SetPageDisplayName(BackListItem selectedItem, string displayName)
        {
            BackListItem templateItem = TemplateItems.First(x => x.ItemName == selectedItem.ItemName);
            templateItem.PageDisplayName = displayName;
        }

        public void SwapItemUp(BackListItem selectedItem)
        {
            BackListItem templateItem = TemplateItems.First(x => x == selectedItem);
            foreach (BackListItem item in TemplateItems)
            {
                if (item == selectedItem)
                {
                    int index = TemplateItems.IndexOf(item);
                    if (index != 0)
                    {
                        List<BackListItem> tempList = TemplateItems.ToList();
                        BackListItem temp = tempList[index - 1];
                        tempList[index - 1] = tempList[index];
                        tempList[index] = temp;

                        TemplateItems.Clear();
                        foreach (BackListItem name in tempList)
                        {
                            TemplateItems.Add(name);
                        }
                        return;
                    }
                }
            }
        }

        public void SwapItemDown(BackListItem selectedItem)
        {
            BackListItem templateItem = TemplateItems.First(x => x == selectedItem);
            foreach (BackListItem item in TemplateItems)
            {
                if (item == selectedItem)
                {
                    int index = TemplateItems.IndexOf(item);
                    if(index != TemplateItems.Count - 1)
                    {
                        List<BackListItem> tempList = TemplateItems.ToList();
                        BackListItem temp = tempList[index + 1];
                        tempList[index + 1] = TemplateItems[index];
                        tempList[index] = temp;

                        TemplateItems.Clear();
                        foreach (BackListItem name in tempList)
                        {
                            TemplateItems.Add(name);
                        }
                        return;
                    }
                }
            }
        }
    }
}
