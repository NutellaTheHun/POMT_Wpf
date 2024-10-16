
using Petsi.Reports.TableBuilder;

namespace Petsi.Reports.PageBuilder
{
    public class PageBuilderPastryOverflow : PageBuilderBase
    {
        public PageBuilderPastryOverflow(Report report) : base(report)
        {
            ConfigureTables();
        }

        public override int GetItemLineCount<T>(T item)
        {
            return 1;
        }

        public override bool IsRelevantItemToList<T>(T item, int lineItemCount)
        {
            return lineItemCount != 0;
        }

        protected override void ConfigureMaxRows()
        {
            maxPageRowCount = 40;
        }

        protected override void ConfigureTables()
        {
            _tables.Add(new TableBackListPastry((7, 2), 5, 32, null));
        }
    }
}
