using Petsi.Reports.PageBuilder;

namespace Petsi.Reports.ReportBuilder
{
    /// <inheritdoc/>
    public class ReportBuilderBackList : ReportBuilderBase
    {
        /// <inheritdoc/>
        public ReportBuilderBackList(Report report) : base(report)
        {
        }

        protected override void ConfigureBuilders()
        {
            pageBuilders.Add(new PageBuilderBackListPie(_report));
            pageBuilders.Add(new PageBuilderBackListPastry(_report));
        }
    }
}
