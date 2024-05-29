using ClosedXML.Excel;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Reports.TableBuilder
{
    /// <inheritdoc/>
    public class TableFrontListCover : TableBase
    {
        /// <inheritdoc/>
        public TableFrontListCover((int row, int col) rootPosition, int maxColumns, int maxRows) : base(rootPosition, maxColumns, maxRows)
        {
        }
        public override void BuildTable<T>(IXLWorksheet page, List<T> orderList, DateTime reportDate, string? recipient)
        {
            List<PetsiOrder> inputList = orderList as List<PetsiOrder>;
            //Header
            AddLine(page, ref _rowIndex, _rootPosition.col, "Deliveries", "Total Bags", "Comments");

            foreach (PetsiOrder item in inputList)
            {
                AddLine(page, ref _rowIndex, _rootPosition.col, item.Recipient);
            }
            FormatTable(page);
            _rowIndex = _rootPosition.row;
        }
        protected override void FormatTable(IXLWorksheet page)
        {
            string tableRange = TableFormat.BuildRange(_rootPosition.row, _rowIndex-1, "B", "F");
            string headerRange = TableFormat.BuildRange(_rootPosition.row, _rootPosition.row, "B", "F");

            TableFormat.RangeAllBorders(page, tableRange);
            TableFormat.RangeAlignment(page, "center", tableRange);
            TableFormat.RangeFontSize(page, 14, tableRange);
            TableFormat.ColWidthFitSizeOfText(page, "B:F");
            for(int i = _rootPosition.row; i < _rowIndex; i++)
            {
                TableFormat.RangeMerge(page, "D" + i + ":" + "F" + i);
            }

            TableFormat.RangeBold(page, headerRange);
        }
    }
}
