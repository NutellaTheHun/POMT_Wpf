using Petsi.Reports.TableBuilder;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Reports.PageBuilder
{
    public class PageBuilderFrontListDelivery : PageBuilderBase
    {
        public PageBuilderFrontListDelivery(Report report) : base(report)
        {
            SetMaxOrders(1);
            ConfigureTables();
        }
        public override int GetItemLineCount<T>(T item)
        {
            return 1;
        }

        public override bool IsRelevantItemToList<T>(T item, int lineItemCount)
        {
            PetsiOrder order = item as PetsiOrder;
            return order.FulfillmentType == Identifiers.FULFILLMENT_DELIVERY
                && order.OrderType != Identifiers.ORDER_TYPE_WHOLESALE;
        }

        protected override void ConfigureMaxRows()
        {
            maxPageRowCount = 44; //arbitrary setting, same as FrontListOrder pages, just cant be 0 for edgecase in ReportBuilder.BuildReport()
        }

        protected override void ConfigureTables()
        {
            //_tables.Add(new TableBuilderFrontListDelivery((1, 1), 11, 18));
        }
    }
}
