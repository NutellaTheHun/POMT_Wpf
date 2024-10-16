using Petsi.Reports.TableBuilder;
using Petsi.Units;

namespace Petsi.Reports.PageBuilder
{
    public class PageBuilderBackListPie : PageBuilderBase
    {
        List<BackListItem>? Template;
        public PageBuilderBackListPie(Report report, List<BackListItem>? template) : base(report)
        {
            Template = template;
            ConfigureTables();
        }

        /// <summary>
        /// Determines if the item will require N number of rows on the report. Items with a line count of 0 can still neccesary to be present on the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int GetItemLineCount<T>(T item)
        {
            
            PetsiOrderLineItem lineItem = item as PetsiOrderLineItem;
            if (lineItem.ItemName.ToLower().Contains("vegan"))
            {
                return 0;
            }
            if (lineItem.IsCategory(Utils.Identifiers.CATEGORY_PASTRY))
            {
                return 0;
            }
            if (lineItem.IsCategory(Utils.Identifiers.CATEGORY_TAKENBAKE))
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// Some items for a list may equat to 0 lines on the list but are still required for the report. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool IsRelevantItemToList<T>(T item, int lineItemCount)
        {
            PetsiOrderLineItem lineItem = item as PetsiOrderLineItem;
            if (lineItem.IsCategory(Utils.Identifiers.CATEGORY_PASTRY))
            {
                return false;
            }
            return true;
        }

        protected override void ConfigureMaxRows()
        {
            maxPageRowCount = 40;
        }

        protected override void ConfigureTables()
        {
            _tables.Add(new TableBackListPie((7,2), 5, 32, Template));
        }
    }
}
