using Petsi.Reports.TableBuilder;
using Petsi.Units;

namespace Petsi.Reports.PageBuilder
{
    public class PageBuilderBackListPie : PageBuilderBase
    {
        public PageBuilderBackListPie(Report report) : base(report)
        {
        }

        public override int GetItemLineCount<T>(T item)
        {
            /*
            PetsiOrderLineItem lineItem = item as PetsiOrderLineItem;
            if (lineItem.ItemName.ToLower().Contains("vegan"))
            {
                return 0;
            }*/
            return 1;
        }

        protected override void ConfigureMaxRows()
        {
            maxPageRowCount = 40;
        }

        protected override void ConfigureTables()
        {
            _tables.Add(new TableBackListPie((7,2), 5, 32));
        }
    }
}
