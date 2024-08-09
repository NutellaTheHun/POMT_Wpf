using Petsi.Reports.TableBuilder;

namespace Petsi.Reports.PageBuilder
{
    public class PageBuilderWsDay : PageBuilderBase
    {
        private string _day;
        public PageBuilderWsDay(Report report) : base(report)
        {
        }

        public override int GetItemLineCount<T>(T item)
        {
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
            _tables.Add(new TableWsDay((7,2), 5, 32));
        }
        public void SetDay(string day)
        {
            _day = day;
        }
        public string GetDay() { return _day; }
    }
}
