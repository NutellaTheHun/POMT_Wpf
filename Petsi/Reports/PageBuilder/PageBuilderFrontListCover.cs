using Petsi.Reports.TableBuilder;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Reports.PageBuilder
{
    public class PageBuilderFrontListCover : PageBuilderBase
    {
        public PageBuilderFrontListCover(Report report) : base(report)
        {
            ConfigureTables();
        }

        public override int GetItemLineCount<T>(T item)
        {
            PetsiOrder order = item as PetsiOrder;
            if (order.OrderType == Identifiers.ORDER_TYPE_FARMERS)        { return 1; }
            if (order.FulfillmentType == Identifiers.FULFILLMENT_DELIVERY){ return 1; }
            if (order.OrderType == Identifiers.ORDER_TYPE_WHOLESALE)      { return 1; }
            return 0;
        }

        public override bool IsRelevantItemToList<T>(T item, int lineItemCount)
        {
            return lineItemCount != 0;
        }

        protected override void ConfigureMaxRows()
        {
            maxPageRowCount = 40;//??
        }

        protected override void ConfigureTables()
        {
            _tables.Add(new TableFrontListCover((7,2), 4, 32));
        }
    }
}
