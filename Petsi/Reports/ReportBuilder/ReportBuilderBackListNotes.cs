using Petsi.Reports.PageBuilder;

namespace Petsi.Reports.ReportBuilder
{
    public class ReportBuilderBackListNotes : ReportBuilderBase
    {
        public ReportBuilderBackListNotes(Report report): base(report) {
            ConfigureBuilders();
        }
        protected override void ConfigureBuilders()
        {
            pageBuilders.Add(new PageBuilderBackListPieNotes(_report));
        }
    }
}
