using Petsi.Reports.PageBuilder;
using Petsi.Units;

namespace Petsi.Reports.ReportBuilder
{
    public class ReportBuilderBackListPie : ReportBuilderBase
    {
        List<BackListItem> Template;
        public ReportBuilderBackListPie(Report report, List<BackListItem>? template) : base(report) 
        {   
            Template = template;
            ConfigureBuilders();
        }
  
        protected override void ConfigureBuilders()
        {
            pageBuilders.Add(new PageBuilderBackListPie(_report, Template));
        }
    }
}
