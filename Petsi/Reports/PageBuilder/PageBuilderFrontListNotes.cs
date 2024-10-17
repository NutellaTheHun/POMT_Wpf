using Petsi.Reports.TableBuilder;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Reports.PageBuilder
{
    public class PageBuilderFrontListNotes : PageBuilderBase
    {
        public PageBuilderFrontListNotes(Report report) : base(report)
        {
            ConfigureTables();
        }

        public override int GetItemLineCount<T>(T item)
        {
            PetsiOrder order = item as PetsiOrder;
            if (order.OrderType == Identifiers.ORDER_TYPE_FARMERS) { return 0; }
            if (order.Note == "" || order.Note == null)
            {
                return 0;
            }
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
            _tables.Add(new TableFrontListNote((7,1), 2, 32));
        }
    }
}
