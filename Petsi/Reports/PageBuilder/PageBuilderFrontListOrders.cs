using Petsi.Reports.TableBuilder;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Reports.PageBuilder
{
    public class PageBuilderFrontListOrders : PageBuilderBase
    {
        public PageBuilderFrontListOrders(Report report) : base(report)
        {
        }

        public override int GetItemLineCount<T>(T item)
        {
           var order = item as PetsiOrder;

           if(order.InputOriginType == Identifiers.WHOLESALE_INPUT) { return 0; }

           return order.LineItems.Count + 1;
        }

        protected override void ConfigureMaxRows()
        {
            maxPageRowCount = 40;
        }

        protected override void ConfigureTables()
        {
            _tables.Add(new TableFrontListOrder((7,1), 6, 32));
        }
    }
}
