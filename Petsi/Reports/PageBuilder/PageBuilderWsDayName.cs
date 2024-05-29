using ClosedXML.Excel;
using Petsi.Reports.TableBuilder;
using Petsi.Units;

namespace Petsi.Reports.PageBuilder
{
    public class PageBuilderWsDayName : PageBuilderBase
    {
        public PageBuilderWsDayName(Report report) : base(report)
        {
            SetMaxOrders(2);
        }
        public override int GetItemLineCount<T>(T item)
        {
            var order = item as PetsiOrder;
            return order.LineItems.Count;
        }
        protected override void ConfigureMaxRows()
        {
            maxPageRowCount = ReportUtil.MAX_LIST_CONTENT_LN_COUNT;
        }
        protected override void ConfigureTables()
        {
            //_tables.Add(new TableWsDayNameBanner((6,2), 6, 1));
            _tables.Add(new TableWsDayName((8,2), 5, 33));
            _tables.Add(new TableWsDayName((8,8), 5, 33));
        }
        protected override void BuildReportHeader<T>(IXLWorksheet page, List<T>? pageSizeOrders)
        {
            ReportUtil.InitPageReportHeader(page, _report);
            List<PetsiOrder> pageOrders = pageSizeOrders as List<PetsiOrder>;
            BuildPageBanner(page, _report, DateTime.Parse(pageOrders[0].OrderDueDate).DayOfWeek.ToString());
            //FormatReportHeader(page, 0, "");//0 and "" not used in this builder
        }

        private void BuildPageBanner(IXLWorksheet page, Report report, string dayOfWeek)
        {
            string range = "B7:L7";
            page.Cell(7, 2).Value = "For " + dayOfWeek;
            TableFormat.RangeMerge(page,range);
            TableFormat.RangeFontSize(page, 16, range);
            TableFormat.RangeAllBorders(page, range);
            TableFormat.RangeAlignment(page, "center", range);
            TableFormat.RangeBold(page, range);
        }
    }
}
