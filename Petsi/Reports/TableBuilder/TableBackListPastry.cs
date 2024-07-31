using ClosedXML.Excel;
using Petsi.Events.ReportEvents;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Reports.TableBuilder
{
    /// <inheritdoc/>
    public class TableBackListPastry : TableBase
    {
        /// <inheritdoc/>
        public TableBackListPastry((int row, int col) rootPosition, int maxColumns, int maxRows) : base(rootPosition, maxColumns, maxRows)
        {
        }
        public override void BuildTable<T>(IXLWorksheet page, List<T> tableOrders, DateTime reportDate, string? recipient)
        {
            List<PetsiOrderLineItem> items = tableOrders as List<PetsiOrderLineItem>;
            List<PetsiOrderLineItem> itemTracker = new List<PetsiOrderLineItem>(items);
            //List <BackListItem> listFormat = BacklistTemplateFormatSelector.GetInstance().GetPastryFormat();
            List<BackListItem> listFormat = ReportTemplateService.Instance().GetActiveBacklistPastryTemplate();
            if(listFormat == null)
            {
                SystemLogger.Log("TableBackListPastry GetActiveBackListPastryTemplate returned an empty list");
                return;
            }
            string amountReg;

            foreach (BackListItem item in listFormat)
            {
                amountReg = "";
                foreach (PetsiOrderLineItem lineItem in items)
                {
                    if(item.CatalogObjId == lineItem.CatalogObjectId)
                    {
                        amountReg = lineItem.AmountRegular.ToString();
                        itemTracker.Remove(lineItem);
                        break;
                    }
                }
                AddLine(page, ref _rowIndex, _rootPosition.col, item.PageDisplayName, amountReg);
            }
            if (itemTracker.Count > 0)
            {
                List<PetsiOrderLineItem> remainders = new List<PetsiOrderLineItem>();
                
                foreach (PetsiOrderLineItem item in itemTracker)
                {
                    if (item.IsCategory(Identifiers.CATEGORY_PASTRY))
                    {
                        remainders.Add(item);
                    }
                }
                if (remainders.Count > 0)
                {
                    ErrorService.Instance().RaiseTBOverflowEvent(remainders);
                    BackListOverflowEvent.OnPastryOverflow(remainders);
                }
            }
            FormatTable(page);
            _rowIndex = _rootPosition.row;
        }
        protected override void FormatTable(IXLWorksheet page)
        {
            string tableRange = TableFormat.BuildRange(_rootPosition.row, _rowIndex - 1, "B", "C");

            TableFormat.RangeAllBorders(page, tableRange);
            TableFormat.RangeAlignment(page, "center", tableRange);
            TableFormat.RangeFontSize(page, 18, tableRange);
            TableFormat.ColWidthFitSizeOfText(page, "B");
            TableFormat.ColumnSetPixelLength(page, 8.57, "C");
        }
    }
}
