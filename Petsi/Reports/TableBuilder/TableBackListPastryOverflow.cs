using ClosedXML.Excel;
using Petsi.Units;

namespace Petsi.Reports.TableBuilder
{
    public class TableBackListPastryOverflow : TableBase
    {
        public TableBackListPastryOverflow((int row, int col) rootPosition, int maxColumns, int maxRows) : base(rootPosition, maxColumns, maxRows)
        {
        }

        public override void BuildTable<T>(IXLWorksheet page, List<T> tableOrders, DateTime reportDate, string? recipient)
        {
            List<PetsiOrderLineItem> items = tableOrders as List<PetsiOrderLineItem>;
            string amountReg;
            foreach (PetsiOrderLineItem lineItem in items)
            {
                AddLine(page, ref _rowIndex, _rootPosition.col, lineItem.ItemName, lineItem.AmountRegular.ToString());
            }
            FormatTable(page);
            _rowIndex = _rootPosition.row;
        }

        protected override void FormatTable(IXLWorksheet page)
        {
            string tableRange = TableFormat.BuildRange(_rootPosition.row, _rowIndex - 1, "B", "C");

            TableFormat.RangeAllBorders(page, tableRange);
            TableFormat.RangeAlignment(page, "center", tableRange);
            TableFormat.RangeFontSize(page, 13, tableRange);
            TableFormat.ColWidthFitSizeOfText(page, "B");
            TableFormat.ColumnSetPixelLength(page, 8.57, "C");
        }
    }
}
