using Petsi.Reports.TableBuilder;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Reports.PageBuilder
{
    public class PageBuilderFrontListCover : PageBuilderBase
    {
        public PageBuilderFrontListCover(Report report) : base(report)
        {

        }

        public override int GetItemLineCount<T>(T item)
        {
            PetsiOrder order = item as PetsiOrder;
            if(order.FulfillmentType == Identifiers.FULFILLMENT_DELIVERY
                ||
               order.OrderType == Identifiers.ORDER_TYPE_WHOLESALE)
            {
                return 1;
            }
            return 0;
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
