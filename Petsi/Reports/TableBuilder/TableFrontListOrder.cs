﻿using ClosedXML.Excel;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Reports.TableBuilder
{
    /// <inheritdoc/>
    public class TableFrontListOrder : TableBase
    {
        /// <inheritdoc/>
        public TableFrontListOrder((int row, int col) rootPosition, int maxColumns, int maxRows) : base(rootPosition, maxColumns, maxRows)
        {
        }
        public override void BuildTable<T>(IXLWorksheet page, List<T> tableOrders, DateTime reportDate, string? recipient)
        {
            List<PetsiOrder> orderList = tableOrders as List<PetsiOrder>;
            string lineItemAmount = "";
            string size = "";

            //Header
            AddLine(page, ref _rowIndex, _rootPosition.col,
                "Name", "Time", "Type", "Size", "Item", "Quantity");
            foreach(PetsiOrder order in orderList)
            {
                //name, time, type, size, item, quantity
                AddLine(page, ref _rowIndex, _rootPosition.col, order.Recipient, DateTime.Parse(order.OrderDueDate).ToShortTimeString(), order.FulfillmentType, CHECKNOTES(order));
                foreach(PetsiOrderLineItem lineItem in order.LineItems)
                {
                    if (lineItem.Amount5 != 0)
                    { 
                        lineItemAmount = lineItem.Amount5.ToString();
                        size = "5\"";
                    }
                    else if (lineItem.Amount8 != 0)
                    { 
                        lineItemAmount = lineItem.Amount8.ToString();
                        size = "8\"";
                    }
                    else if (lineItem.Amount10 != 0)
                    { 
                        lineItemAmount = lineItem.Amount10.ToString();
                        size = "10\"";
                    }
                    else if(lineItem.AmountRegular != 0)
                    {
                        lineItemAmount = lineItem.AmountRegular.ToString();
                         size = "";
                    }
                    else
                    {
                        Console.WriteLine("TABLE ADDLINE AMOUNT ERROR");
                        Console.WriteLine("Order: " + order.Recipient + " : " + order.OrderId);
                        Console.WriteLine("Item: " + lineItem.ItemName);
                    }
                    //name, time, type, size, item, quantity
                    AddLine(page, ref _rowIndex, _rootPosition.col, "", "", "", size, TableFormat.MaxLineLength(lineItem.ItemName, 25), lineItemAmount);//shorten 25
                }
            }
            FormatTable(page);
            _rowIndex = _rootPosition.row;
        }
        protected override void FormatTable(IXLWorksheet page)
        {
            string tableRange = TableFormat.BuildRange(_rootPosition.row, _rowIndex-1, "A", "F");
            string headerRange = TableFormat.BuildRange(_rootPosition.row, _rootPosition.row, "A", "F");

            TableFormat.RangeAllBorders(page, tableRange);
            TableFormat.RangeAlignment(page, "center", tableRange);
            TableFormat.RangeFontSize(page,14,tableRange);
            TableFormat.ColWidthFitSizeOfText(page, "A:F");

            TableFormat.RangeBold(page, headerRange);

        }
        private string CHECKNOTES(PetsiOrder order)
        {
            if (order.Note != "")
            {
                return "CHECK NOTES";
            }
            return "";
        }
    }
}
