using ClosedXML.Excel;
using Petsi.Units;

namespace Petsi.Reports.TableBuilder
{
    public class TableBackListPieOverflow : TableBase
    {
        public TableBackListPieOverflow((int row, int col) rootPosition, int maxColumns, int maxRows) : base(rootPosition, maxColumns, maxRows)
        {

        }

        public override void BuildTable<T>(IXLWorksheet page, List<T> tableOrders, DateTime reportDate, string? recipient)
        {
            List<PetsiOrderLineItem> items = tableOrders as List<PetsiOrderLineItem>;

            //Header
            AddLine(page, ref _rowIndex, _rootPosition.col, "", "3\"", "5\"", "8\"", "10\"");
              
                foreach (PetsiOrderLineItem lineItem in items)
                {
                    string amount3 = "", amount5 = "", amount8 = "", amount10 = "";
                    bool isVegan = false;

                    if (lineItem.ItemName.ToLower().Contains("vegan"))
                    {
                        if (lineItem.Amount3 != 0) { amount3 = HandleVeganLineAmount(lineItem.Amount3.ToString(), amount3); }
                        if (lineItem.Amount5 != 0) { amount5 = HandleVeganLineAmount(lineItem.Amount5.ToString(), amount5); }
                        if (lineItem.Amount8 != 0) { amount8 = HandleVeganLineAmount(lineItem.Amount8.ToString(), amount8); }
                        if (lineItem.Amount10 != 0) { amount10 = HandleVeganLineAmount(lineItem.Amount10.ToString(), amount10); }
                    }
                    else
                    {
                        if (lineItem.Amount3 != 0) { amount3 = HandleLineAmount(lineItem.Amount3.ToString(), amount3); }
                        if (lineItem.Amount5 != 0) { amount5 = HandleLineAmount(lineItem.Amount5.ToString(), amount5); }
                        if (lineItem.Amount8 != 0) { amount8 = HandleLineAmount(lineItem.Amount8.ToString(), amount8); }
                        if (lineItem.Amount10 != 0) { amount10 = HandleLineAmount(lineItem.Amount10.ToString(), amount10); }
                    }
                    AddLine(page, ref _rowIndex, _rootPosition.col,
                    lineItem.ItemName, amount3, amount5, amount8, amount10);
                }   

            FormatTable(page);
            _rowIndex = _rootPosition.row;
        }

        /// <summary>
        /// The input is a quanitity of normal (non-vegan) items, the source can either be empty, or contain a 
        /// vegan quantity, denoted with "V", ex: 4V.
        /// example: inputAmount = 5, source = "", output -> "5"
        /// example: inputAmount = 3, source = "2V", output -> "3, 2V"
        /// </summary>
        /// <param name="inputAmount"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private string HandleLineAmount(string inputAmount, string source)
        {
            if (source == "") { return inputAmount; }
            if (source.ToLower().Contains("v"))
            {
                return inputAmount + ", " + source;
            }
            return inputAmount;
        }

        /// <summary>
        /// The incoming input is a quantity of vegan type pies, the source can either be empty("") or
        /// already contain a quantity of normal type pies, and must be modified.
        /// example: inputAmount = 4, source = "", output -> "4V"
        /// example: inputAmount = 1, source = "3", output -> "1,3V"
        /// </summary>
        /// <param name="inputAmount"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private string HandleVeganLineAmount(string inputAmount, string source)
        {
            if (source == "") { return inputAmount + "V"; }
            else
            {
                return source + "," + inputAmount + "V";
            }
        }

        protected override void FormatTable(IXLWorksheet page)//format line?
        {
            string tableRange = TableFormat.BuildRange(_rootPosition.row, _rowIndex - 1, "B", "F");
            string headerRange = TableFormat.BuildRange(_rootPosition.row, _rootPosition.row, "B", "F");

            TableFormat.RangeAllBorders(page, tableRange);
            TableFormat.RangeAlignment(page, "center", tableRange);
            TableFormat.RangeFontSize(page, 13, tableRange);
            TableFormat.ColWidthFitSizeOfText(page, "B");
            TableFormat.ColumnSetPixelLength(page, 8.57, "C:F");
            TableFormat.RangeBold(page, headerRange);
        }
    }
}
