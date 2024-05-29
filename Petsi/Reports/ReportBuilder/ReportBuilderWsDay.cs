using Petsi.Reports.PageBuilder;

namespace Petsi.Reports.ReportBuilder
{
    /// <inheritdoc/>
    public class ReportBuilderWsDay : ReportBuilderBase
    {
        /// <inheritdoc/>
        public ReportBuilderWsDay(Report report) : base(report)
        {

        }

        protected override void ConfigureBuilders()
        {
            pageBuilders.Add(new PageBuilderWsDay(_report));
        }
    }
}
