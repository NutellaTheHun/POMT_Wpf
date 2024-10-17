using ClosedXML.Excel;
using Petsi.Units;

namespace Petsi.Reports.TableBuilder
{
    /// <inheritdoc/>
    public class TableWsDayName : TableBase
    {
        string _recipient;
        /// <inheritdoc/>
        public TableWsDayName((int row, int col) rootPosition, int maxColumns, int maxRows) : base(rootPosition, maxColumns, maxRows)
        {
            SetMaxOrderLimit(1);
        }
        public override void BuildTable<T>(IXLWorksheet page, List<T> tableOrders, DateTime reportDate, string? recipient)
        {
            SetRecipient(recipient);
            List<PetsiOrder> items = tableOrders as List<PetsiOrder>;
            int total3 = 0;
            int total5 = 0;
            int total8 = 0;
            int total10 = 0;
            //Header
            AddLine(page, ref _rowIndex, _rootPosition.col, recipient);
            AddLine(page, ref _rowIndex, _rootPosition.col, " ", "3\"", "5\"", "8\"", "10\"");

            //Body
            string amount3 = "", amount5 = "", amount8 = "", amount10 = "";
            foreach (PetsiOrder order in items)
            {
                foreach (PetsiOrderLineItem lineItem in order.LineItems)
                {
                    //Remove text from cell if 0 to reduce clutter on report.
                    amount3 = ""; amount5 = ""; amount8 = ""; amount10 = "";
                    if (lineItem.Amount3 != 0) { amount3 = lineItem.Amount3.ToString(); total3 += lineItem.Amount3; }
                    if (lineItem.Amount5 != 0) { amount5 = lineItem.Amount5.ToString(); total5 += lineItem.Amount5; }
                    if (lineItem.Amount8 != 0) { amount8 = lineItem.Amount8.ToString(); total8 += lineItem.Amount8; }
                    if (lineItem.Amount10 != 0) { amount10 = lineItem.Amount10.ToString(); total10 += lineItem.Amount10; }

                    AddLine(page, ref _rowIndex, _rootPosition.col, 
                        lineItem.ItemName, amount3, amount5, amount8, amount10);
                }
            }

            //Totals column
            AddLine(page, ref _rowIndex, _rootPosition.col,
                        "", total3.ToString(), total5.ToString(), total8.ToString(), total10.ToString());

            FormatTable(page);
            _rowIndex = _rootPosition.row;
        }
        protected override void FormatTable(IXLWorksheet page)
        {
            string tableRange = TableFormat.BuildRange(
                _rootPosition.row, _rowIndex - 2, // - 1 is default, is - 2 to not include last row which is footer (column totals)
                ToColumnLetter(_rootPosition.col), ToColumnLetter(_rootPosition.col+_maxColumns-1));

            string headerRange = TableFormat.BuildRange(
                _rootPosition.row, _rootPosition.row,
                ToColumnLetter(_rootPosition.col), ToColumnLetter(_rootPosition.col + _maxColumns - 1));

            string footerRange = TableFormat.BuildRange(
                _rowIndex - 1, _rowIndex - 1,
                ToColumnLetter(_rootPosition.col), ToColumnLetter(_rootPosition.col + _maxColumns - 1));

            string sizingHeaderRange = TableFormat.BuildRange(
                _rootPosition.row+1, _rootPosition.row+1,
                ToColumnLetter(_rootPosition.col), ToColumnLetter(_rootPosition.col + _maxColumns - 1));


            //List
            TableFormat.RangeAllBorders(page, tableRange);
            TableFormat.RangeAlignment(page, "center", tableRange);
            TableFormat.RangeFontSize(page, 16, tableRange);
            
            //Header
            ////Border, align center, font 14, BOLD, merge
            TableFormat.RangeMerge(page, headerRange);
            TableFormat.RangeBold(page, headerRange);
            TableFormat.RangeBold(page, sizingHeaderRange);
            TableFormat.RangeFontSize(page, 16, headerRange);

            //Footer (column totals)
            TableFormat.RangeBold(page, footerRange);
            TableFormat.RangeAlignment(page, "center", footerRange);
            TableFormat.RangeFontSize(page, 16, footerRange);

            TableFormat.ColWidthFitSizeOfText(page, "A:L");
        }
        /// <summary>
        /// Turns an integer to its corresponding column in an excel file, doesnt handle past z column(26 as input).
        /// </summary>
        /// <param name="col">cannot be greater than 26, cannot be 0</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private string ToColumnLetter(int col)
        {
            if(col > 26) { throw new Exception("ToColumnLetter(int) doesn't handle past the 26th column (Z column)."); }
            if (col == 0) { throw new Exception("ToColumnLetter(int) cant take 0 as input."); }

            return ((char)('A' + col - 1)).ToString();
        }

        public string GetRecipient() { return _recipient;}
        public void SetRecipient(string recipient) { _recipient = recipient; }
    }
}
