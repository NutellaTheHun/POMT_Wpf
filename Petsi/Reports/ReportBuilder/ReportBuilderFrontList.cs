using Petsi.Reports.PageBuilder;

namespace Petsi.Reports.ReportBuilder
{
    /// <inheritdoc/>
    public class ReportBuilderFrontList : ReportBuilderBase
    {
        /// <inheritdoc/>
        public ReportBuilderFrontList(Report report) : base(report)
        {
            ConfigureBuilders();
        }

        protected override void ConfigureBuilders()
        {
            pageBuilders.Add(new PageBuilderFrontListCover(_report));
            pageBuilders.Add(new PageBuilderFrontListOrders(_report));
            pageBuilders.Add(new PageBuilderFrontListNotes(_report));
            pageBuilders.Add(new PageBuilderFrontListDelivery(_report));
        }
    }
}
