using Petsi.Filing;
using Petsi.Reports;
using Petsi.Units;
using Petsi.Utils;
using System.ComponentModel;

namespace Petsi.Services
{
    public class ReportTemplateService
    {
        List<(string templateName, List<BackListItem> templateItems)> items;
        FileBehavior filebehavior;
        string filename = "templateItems";

        private static ReportTemplateService instance;

        private ReportTemplateService() 
        {
            filebehavior = new FileBehavior(Identifiers.SERVICE_TEMPLATE);
            items = filebehavior.BuildDataListFile<(string, List<BackListItem>)>(filename);
            if(items == null) 
            { 
                items = new List<(string name, List<BackListItem> templateItems)> ();
                BacklistTemplateFormatSelector btfs = BacklistTemplateFormatSelector.GetInstance();
                items.Add(btfs.BootPastryFormat());
                items.Add(btfs.BootPieFormat());
                items.Add(btfs.BootSummerFormat());
                Save();
            }
        }

        public static ReportTemplateService Instance()
        {
            if(instance == null) { instance  = new ReportTemplateService(); }
            return instance;
        }

        public List<string> GetTemplateNames()
        {
            return items.Select(x => x.templateName).ToList();
        }

        public (string templateName, List<BackListItem> templateItems) GetTemplate(string templateName)
        {
            return items.First(x => x.templateName == templateName);
        }

        /// <summary>
        /// If template name exists, will overwrite, saves to file
        /// </summary>
        /// <param name="newTemplate"></param>
        public void AddTemplate((string templateName, List<BackListItem> templateItems) newTemplate)
        {
            var existingTemplate = items.FirstOrDefault(x => x.templateName == newTemplate.templateName);
            if (existingTemplate != default) { items.Remove(existingTemplate); } //Needs testing, default in this case?
            items.Add(newTemplate);
            Save(); }

        /// <summary>
        /// removes template and saves new list to file
        /// </summary>
        /// <param name="templateName"></param>
        public void RemoveTemplate(string templateName)
        {
            var template = items.First(x => x.templateName.Equals(templateName)); //test if not found?
            items.Remove(template);
            Save();
        }
        private void Save()
        {
            filebehavior.DataListToFile(filename, items);
        }

    }
}
