using ClosedXML.Excel;
using Petsi.Units;

namespace Petsi.Reports.TableBuilder
{
    /// <inheritdoc/>
    public class TableFrontListNote : TableBase
    {
        /// <inheritdoc/>
        public TableFrontListNote((int row, int col) rootPosition, int maxColumns, int maxRows) : base(rootPosition, maxColumns, maxRows)
        {
        }
        public override void BuildTable<T>(IXLWorksheet page, List<T> tableOrders, DateTime reportDate, string? recipient)
        {
            List<PetsiOrder> inputList = tableOrders as List<PetsiOrder>;
            List<(string name, string note)> orderNotes = new List<(string name, string note)>();
            foreach(PetsiOrder item in inputList)
            {
                if(item.Note != "")
                {
                    orderNotes.Add((item.Recipient, item.Note));
                }
            }
            //Header
            AddLine(page, ref _rowIndex, _rootPosition.col, "Name", "Note");

            foreach (var order in orderNotes) 
            {
                AddLine(page, ref _rowIndex, _rootPosition.col, order.name, order.note);
            }
            FormatTable(page);
            _rowIndex = _rootPosition.row;
        }
        protected override void FormatTable(IXLWorksheet page)
        {
            string tableRange = TableFormat.BuildRange(_rootPosition.row,_rowIndex-1, "A", "B");
            string headerRange = TableFormat.BuildRange(_rootPosition.row, _rootPosition.row, "A", "B");
            string noteRange = TableFormat.BuildRange(_rootPosition.row, _rowIndex - 1, "B", "B");

            /*
            The act of fixing the note column length to be long,
            yet properly allow multi lines per order isn't straight forward
            with closedXML and includes a bug in the API, so the formatting is weird here.
            */
            //header
            TableFormat.ColWidthFitSizeOfText(page, "A:B");
            TableFormat.ColumnIncreaseLength(page, 4, "B");

            TableFormat.RangeBold(page, headerRange);
            TableFormat.RangeAllBorders(page, headerRange);
            TableFormat.RangeAlignment(page, "center", headerRange);
            TableFormat.RangeFontSize(page, 14, headerRange);
            
            //table
            TableFormat.RangeAllBorders(page, tableRange);
            TableFormat.RangeAlignment(page, "center", tableRange);
            TableFormat.RangeFontSize(page, 14, tableRange);
            TableFormat.WrapText(page, noteRange);
            TableFormat.RowWidthFitSizeOfText(page, _rootPosition.row, _rowIndex - 1);

            TableFormat.ColWidthFitSizeOfText(page, "A");

        }
    }
}
