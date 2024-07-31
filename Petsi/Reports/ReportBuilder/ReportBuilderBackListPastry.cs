
using Petsi.Reports.PageBuilder;

namespace Petsi.Reports.ReportBuilder
{
    public class ReportBuilderBackListPastry : ReportBuilderBase
    {
        public ReportBuilderBackListPastry(Report report) : base(report) { }

        protected override void ConfigureBuilders()
        {
            pageBuilders.Add(new PageBuilderBackListPastry(_report));
        }
    }
}
