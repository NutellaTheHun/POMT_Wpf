
using Petsi.Reports.PageBuilder;
using Petsi.Units;

namespace Petsi.Reports.ReportBuilder
{
    public class ReportBuilderBackListPastry : ReportBuilderBase
    {
        List<BackListItem> Template;
        public ReportBuilderBackListPastry(Report report, List<BackListItem>? template) : base(report)
        {
            Template = template;
            ConfigureBuilders();
        }

        protected override void ConfigureBuilders()
        {
            pageBuilders.Add(new PageBuilderBackListPastry(_report, Template));
        }
    }
}
