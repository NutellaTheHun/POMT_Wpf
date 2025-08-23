using Petsi.Reports.TableBuilder;
using Petsi.Units;

namespace Petsi.Reports.PageBuilder
{
    public class PageBuilderBackListPieNotes : PageBuilderBase
    {
        public PageBuilderBackListPieNotes(Report report) : base(report) {
            ConfigureTables();
        }
        public override int GetItemLineCount<T>(T item)
        {
            PetsiOrder order = item as PetsiOrder;
            if (isRelevantNote(order))
            {
                return 1;
            }
            return 0;
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
            _tables.Add(new TableFrontListNote((7, 1), 2, 32));
        }

        private bool isRelevantNote(PetsiOrder order)
        {

            if (string.IsNullOrWhiteSpace(order.Note))
            {
                return false;
            }

            string[] keywords = { "crumb", "allergy", "lattice", "no", "top", "topping", "pie", "flavor" };
            string noteLower = order.Note.ToLower();

            return keywords.Any(k => noteLower.Contains(k));
        }
    }
}
