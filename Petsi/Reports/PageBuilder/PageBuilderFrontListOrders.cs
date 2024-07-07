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

           if(order.OrderType == Identifiers.ORDER_TYPE_WHOLESALE) { return 0; }
           int lineCount = 0;
           foreach (var lineItem in order.LineItems) 
           {
                if (lineItem.AmountRegular > 0) { lineCount++; }
                if (lineItem.Amount3 > 0) { lineCount++; }
                if (lineItem.Amount5 > 0) { lineCount++; }
                if (lineItem.Amount8 > 0) { lineCount++; }
                if (lineItem.Amount10 > 0) { lineCount++; }
           }
           return lineCount + 1;
        }

        protected override void ConfigureMaxRows()
        {
            maxPageRowCount = 44;
        }

        protected override void ConfigureTables()
        {
            _tables.Add(new TableFrontListOrder((7,1), 6, 32));
        }
    }
}
