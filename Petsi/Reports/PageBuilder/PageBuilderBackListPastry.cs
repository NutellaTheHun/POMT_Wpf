using Petsi.Reports.TableBuilder;
using Petsi.Units;

namespace Petsi.Reports.PageBuilder
{
    public class PageBuilderBackListPastry : PageBuilderBase
    {
        List<BackListItem>? Template;
        public PageBuilderBackListPastry(Report report, List<BackListItem>? template) : base(report)
        {
            Template = template;
            ConfigureTables();
        }

        public override int GetItemLineCount<T>(T item)
        {
            PetsiOrderLineItem lineItem = item as PetsiOrderLineItem;
            if (lineItem.IsCategory(Utils.Identifiers.CATEGORY_PASTRY))
            {
                return 1;
                
            }
            return 0;
        }

        public override bool IsRelevantItemToList<T>(T item, int lineItemCount)
        {
            PetsiOrderLineItem lineItem = item as PetsiOrderLineItem;
            if (lineItem.IsCategory(Utils.Identifiers.CATEGORY_PASTRY))
            {
                return true;
            }
            return false;
        }

        protected override void ConfigureMaxRows()
        {
            maxPageRowCount = 40;
        }

        protected override void ConfigureTables()
        {
            _tables.Add(new TableBackListPastry((7,2), 2, 32, Template));
        }
    }
}
