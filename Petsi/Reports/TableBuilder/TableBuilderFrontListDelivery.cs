using ClosedXML.Excel;

namespace Petsi.Reports.TableBuilder
{
    public class TableBuilderFrontListDelivery : TableBase
    {
        public TableBuilderFrontListDelivery((int row, int col) rootPosition, int maxColumns, int maxRows) : base(rootPosition, maxColumns, maxRows)
        {
        
        }
        public override void BuildTable<T>(IXLWorksheet page, List<T> tableOrders, DateTime reportDate, string? recipient)
        {
            throw new NotImplementedException();
        }

        protected override void FormatTable(IXLWorksheet page)
        {
            throw new NotImplementedException();
        }
    }
}
