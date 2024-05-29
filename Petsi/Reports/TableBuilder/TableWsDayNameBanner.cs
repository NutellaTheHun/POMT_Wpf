using ClosedXML.Excel;

namespace Petsi.Reports.TableBuilder
{
    public class TableWsDayNameBanner : TableBase
    {
        public TableWsDayNameBanner((int row, int col) rootPosition, int maxColumns, int maxRows) : base(rootPosition, maxColumns, maxRows)
        {

        }
        public override void BuildTable<T>(IXLWorksheet page, List<T> tableOrders, DateTime reportDate, string? recipient)
        {
            page.Cell(_rootPosition.row,_rootPosition.col).Value = "For " + reportDate.DayOfWeek.ToString();
            FormatTable(page);
            _rowIndex = _rootPosition.row;
        }
        protected override void FormatTable(IXLWorksheet page)
        {
            
        }
    }
}
