using ClosedXML.Excel;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Reports.TableBuilder
{
    /// <inheritdoc/>
    public class TableBackListPie : TableBase
    {
        /// <inheritdoc/>
        public TableBackListPie((int row, int col) rootPosition, int maxColumns, int maxRows) : base(rootPosition, maxColumns, maxRows)
        {
        }

        public override void BuildTable<T>(IXLWorksheet page, List<T> tableOrders, DateTime reportDate, string? recipient)
        {
            List<PetsiOrderLineItem> items = tableOrders as List<PetsiOrderLineItem>;
            List<PetsiOrderLineItem> itemTracker = new List<PetsiOrderLineItem>(items);
            List<BackListItem> listFormat = BacklistTemplateFormatSelector.GetInstance().GetPieFormat();
            
            //Header
            AddLine(page, ref _rowIndex, _rootPosition.col, "", "3\"", "5\"", "8\"", "10\"");

            foreach (BackListItem item in listFormat)
            {
                string amount3 = "", amount5 = "", amount8 = "", amount10 = "";
                foreach (PetsiOrderLineItem lineItem in items)
                {
                    if (item.CatalogObjId == lineItem.CatalogObjectId)
                    {
                        if(lineItem.Amount3 != 0) { amount3 = lineItem.Amount3.ToString(); }
                        if (lineItem.Amount5 != 0) { amount5 = lineItem.Amount5.ToString(); }
                        if (lineItem.Amount8 != 0) { amount8 = lineItem.Amount8.ToString(); }
                        if (lineItem.Amount10 != 0) { amount10 = lineItem.Amount10.ToString(); }
                        itemTracker.Remove(lineItem);
                        break;
                    }
                    
                }
                AddLine(page, ref _rowIndex, _rootPosition.col,
                        item.PageDisplayName, amount3, amount5, amount8, amount10);
            }
            if(itemTracker.Count > 0)
            {
                SystemLogger.Log("Items for backlist pie not added to BackListPage: ");
                foreach(PetsiOrderLineItem item in itemTracker)
                {
                    SystemLogger.Log("   " + item.ItemName);
                }
            }
            FormatTable(page);
            _rowIndex = _rootPosition.row;
        }

        protected override void FormatTable(IXLWorksheet page)//format line?
        {
            string tableRange = TableFormat.BuildRange(_rootPosition.row, _rowIndex - 1, "B", "F");
            string headerRange = TableFormat.BuildRange(_rootPosition.row, _rootPosition.row, "B", "F");

            TableFormat.RangeAllBorders(page, tableRange);
            TableFormat.RangeAlignment(page, "center", tableRange);
            TableFormat.RangeFontSize(page, 16, tableRange);
            TableFormat.ColWidthFitSizeOfText(page, "B");
            TableFormat.ColumnSetPixelLength(page, 8.57, "C:F");
            TableFormat.RangeBold(page, headerRange);
        }
    }
}
