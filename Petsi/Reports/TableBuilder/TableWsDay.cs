﻿using ClosedXML.Excel;
using Petsi.Units;

namespace Petsi.Reports.TableBuilder
{
    /// <inheritdoc/>
    public class TableWsDay : TableBase
    {
        /// <inheritdoc/>
        public TableWsDay((int row, int col) rootPosition, int maxColumns, int maxRows) : base(rootPosition, maxColumns, maxRows)
        {

        }
        public override void BuildTable<T>(IXLWorksheet page, List<T> tableOrders, DateTime reportDate, string? recipient)
        {
            List<PetsiOrderLineItem> items = tableOrders as List<PetsiOrderLineItem>;

            //Header
            AddLine(page, ref _rowIndex, _rootPosition.col, "For " + reportDate.DayOfWeek.ToString());
            AddLine(page, ref _rowIndex, _rootPosition.col, " " , "3\"" , "5\"", "8\"", "10\"");
            int total3 = 0;
            int total5 = 0;
            int total8 = 0;
            int total10 = 0;
            //Body
            string amount3 = "", amount5 = "", amount8 = "", amount10 = "";
            foreach (PetsiOrderLineItem item in items)
            {
                amount3 = ""; amount5 = ""; amount8 = ""; amount10 = "";
                if (item.Amount3 != 0) { amount3 = item.Amount3.ToString(); total3 += item.Amount3; }
                if (item.Amount5 != 0) { amount5 = item.Amount5.ToString(); total5 += item.Amount5; }
                if (item.Amount8 != 0) { amount8 = item.Amount8.ToString(); total8 += item.Amount8; }
                if (item.Amount10 != 0) { amount10 = item.Amount10.ToString(); total10 += item.Amount10; }

                AddLine(page, ref _rowIndex, _rootPosition.col, 
                    item.ItemName, amount3, amount5, amount8, amount10);
            }

            //Totals column
            AddLine(page, ref _rowIndex, _rootPosition.col,
                        "", total3.ToString(), total5.ToString(), total8.ToString(), total10.ToString());

            FormatTable(page);
            _rowIndex = _rootPosition.row;
        }
        protected override void FormatTable(IXLWorksheet page)
        {
            string tableRange = TableFormat.BuildRange(_rootPosition.row, _rowIndex - 2, "B", "F");
            string headerDayRange = TableFormat.BuildRange(_rootPosition.row, _rootPosition.row, "B", "F");
            string headerRange = TableFormat.BuildRange(_rootPosition.row+1, _rootPosition.row+1, "B", "F");

            string footerRange = TableFormat.BuildRange(_rowIndex - 1, _rowIndex - 1, "B", "F");

            TableFormat.RangeAllBorders(page, tableRange);
            TableFormat.RangeAlignment(page, "center", tableRange);
            TableFormat.RangeFontSize(page, 14, tableRange);
            

            TableFormat.RangeMerge(page, headerDayRange);
            TableFormat.RangeBold(page, headerDayRange);
            TableFormat.RangeFontSize(page, 16, headerDayRange);

            TableFormat.RangeBold(page, headerRange);
            TableFormat.RangeFontSize(page, 16, headerRange);

            TableFormat.RangeAlignment(page, "center", footerRange);
            TableFormat.RangeFontSize(page, 16, footerRange);
            TableFormat.RangeBold(page, footerRange);

            TableFormat.ColWidthFitSizeOfText(page, "A:F");
        }
    }
}
