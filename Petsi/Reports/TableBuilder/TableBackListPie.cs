using ClosedXML.Excel;
using Petsi.Events.ReportEvents;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Reports.TableBuilder
{
    /// <inheritdoc/>
    public class TableBackListPie : TableBase
    {
        List<BackListItem> listFormat;
        /// <inheritdoc/>
        public TableBackListPie((int row, int col) rootPosition, int maxColumns, int maxRows, List<BackListItem>? template) : base(rootPosition, maxColumns, maxRows)
        {
            listFormat = template;
        }

        public override void BuildTable<T>(IXLWorksheet page, List<T> tableOrders, DateTime reportDate, string? recipient)
        {
            List<PetsiOrderLineItem> items = tableOrders as List<PetsiOrderLineItem>;
            List<PetsiOrderLineItem> itemTracker = new List<PetsiOrderLineItem>(items);
            if (listFormat == null){
                listFormat = ReportTemplateService.Instance().GetActiveBacklistPieTemplate();
            }
            

            bool veganPresent5 = false; //bool unbakedPresent5 = false; bool veganUnbaked5 = false;
            bool veganPresent8 = false; //bool unbakedPresent8 = false; bool veganUnbaked8 = false;
            bool veganPresent10 = false; //bool unbakedPresent10 = false; bool veganUnbaked10 = false;

            int total3 = 0;
            int total5 = 0;
            int total8 = 0;
            int total10 = 0;

            if (listFormat == null)
            {
                SystemLogger.LogStatus("TableBackListPie GetActiveBacklistPieTemplate returned an empty list");
                return;
            }

            //Header
            AddLine(page, ref _rowIndex, _rootPosition.col, "", "3\"", "5\"", "8\"", "10\"");

            foreach (BackListItem item in listFormat)
            {
                string amount3 = "", amount5 = "", amount8 = "", amount10 = "";
                if(item.CatalogObjId == Identifiers.CATEGORY_PARBAKE)
                {
                    HandleParbake(items, out amount5, out amount8, out amount10);
                }
                else
                {
                    foreach (PetsiOrderLineItem lineItem in items)
                    {
                        bool isVegan = false;
                        bool isTakeNBake = false;
                        bool isVeganTakeNBake = false;
                        if (MatchCatalogId(lineItem, item.CatalogObjId, out isVegan, out isTakeNBake, out isVeganTakeNBake))
                        {
                            if (isVegan)
                            {
                                if (lineItem.Amount3 != 0)  
                                { 
                                    amount3 = HandleVeganLineAmount(lineItem.Amount3.ToString(), amount3);
                                    total3 += lineItem.Amount3;
                                }
                                if (lineItem.Amount5 != 0)  
                                { 
                                    amount5 = HandleVeganLineAmount(lineItem.Amount5.ToString(), amount5); veganPresent5 = true;
                                    total5 += lineItem.Amount5;
                                }
                                if (lineItem.Amount8 != 0)  
                                { 
                                    amount8 = HandleVeganLineAmount(lineItem.Amount8.ToString(), amount8); veganPresent8 = true; 
                                    total8 += lineItem.Amount8;
                                }
                                if (lineItem.Amount10 != 0) 
                                { 
                                    amount10 = HandleVeganLineAmount(lineItem.Amount10.ToString(), amount10); veganPresent10 = true;
                                    total10 += lineItem.Amount10;
                                }

                                itemTracker.Remove(lineItem);
                                continue;
                            }/*
                            if(isTakeNBake)
                            {
                                if (lineItem.Amount3 != 0)  { amount3 = HandleTakeNBakeLineAmount(lineItem.Amount3.ToString(), amount3); }
                                if (lineItem.Amount5 != 0)  { amount5 = HandleTakeNBakeLineAmount(lineItem.Amount5.ToString(), amount5); unbakedPresent5 = true; }
                                if (lineItem.Amount8 != 0)  { amount8 = HandleTakeNBakeLineAmount(lineItem.Amount8.ToString(), amount8); unbakedPresent8 = true; }
                                if (lineItem.Amount10 != 0) { amount10 = HandleTakeNBakeLineAmount(lineItem.Amount10.ToString(), amount10); unbakedPresent10 = true; }

                                itemTracker.Remove(lineItem);
                                continue;
                            }
                            if (isVeganTakeNBake)
                            {
                                if (lineItem.Amount3 != 0)  { amount3 = HandleVeganTakeNBakeLineAmount(lineItem.Amount3.ToString(), amount3); }
                                if (lineItem.Amount5 != 0)  { amount5 = HandleVeganTakeNBakeLineAmount(lineItem.Amount5.ToString(), amount5); veganUnbaked5 = true; }
                                if (lineItem.Amount8 != 0)  { amount8 = HandleVeganTakeNBakeLineAmount(lineItem.Amount8.ToString(), amount8); veganUnbaked8 = true; }
                                if (lineItem.Amount10 != 0) { amount10 = HandleVeganTakeNBakeLineAmount(lineItem.Amount10.ToString(), amount10); veganUnbaked10 = true; }

                                itemTracker.Remove(lineItem);
                                continue;
                            }*/
                            else
                            {
                                if (lineItem.Amount3 != 0)  
                                { 
                                    amount3 = HandleLineAmount(lineItem.Amount3.ToString(), amount3);
                                    total3 += lineItem.Amount3;
                                }
                                if (lineItem.Amount5 != 0)  
                                { 
                                    amount5 = HandleLineAmount(lineItem.Amount5.ToString(), amount5);
                                    total5 += lineItem.Amount5;
                                }
                                if (lineItem.Amount8 != 0)  
                                { 
                                    amount8 = HandleLineAmount(lineItem.Amount8.ToString(), amount8);
                                    total8 += lineItem.Amount8;
                                }
                                if (lineItem.Amount10 != 0) 
                                { 
                                    amount10 = HandleLineAmount(lineItem.Amount10.ToString(), amount10);
                                    total10 += lineItem.Amount10;
                                }

                                itemTracker.Remove(lineItem);
                                continue;
                            }
                        }
                    }
                }              
                AddLine(page, ref _rowIndex, _rootPosition.col,
                        item.PageDisplayName, amount3, amount5, amount8, amount10);
            }
            if(itemTracker.Count > 0)
            {
                List<PetsiOrderLineItem> remainders = new List<PetsiOrderLineItem>();
                //SystemLogger.Log("Items for backlist pie not added to BackListPage: ");
                foreach(PetsiOrderLineItem item in itemTracker)
                {
                    if(item.IsCategory(Identifiers.CATEGORY_PIE))
                    {
                        remainders.Add(item);
                    }
                }
                if (remainders.Count > 0)
                {
                    ErrorService.Instance().RaiseTBOverflowEvent(remainders);
                    BackListOverflowEvent.OnPieOverflow(remainders);
                }
            }

            //Totals column
            AddLine(page, ref _rowIndex, _rootPosition.col,
                        "", total3.ToString(), total5.ToString(), total8.ToString(), total10.ToString());

            FormatTable(page);
            _rowIndex = _rootPosition.row;
        }

        private void HandleParbake(List<PetsiOrderLineItem> items, out string amount5, out string amount8, out string amount10)
        {
             int a5 = 0, a8 = 0, a10 = 0;
             amount5 = ""; amount8 = ""; amount10 = "";

            foreach (PetsiOrderLineItem item in items)
            {
                if (!item.IsParbake()){ continue; }

                if (item.Amount5 != 0) { a5 += item.Amount5; }
                if (item.Amount8 != 0) { a8 += item.Amount8; }
                if (item.Amount10 != 0) { a10 += item.Amount10; }
            }

            if(a5 != 0) { amount5 = a5.ToString(); }
            if(a8 != 0) { amount8 = a8.ToString(); }
            if(a10 != 0){ amount10 = a10.ToString(); }
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
            if(source == "") { return inputAmount; }
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
            if (source == "") { return inputAmount+"V"; }
            else
            {
                return source + ", " + inputAmount + "V";
            }
        }

        private string HandleTakeNBakeLineAmount(string inputAmount, string source)
        {
            if (source == "") { return inputAmount + "U"; }
            else
            {
                return source + ", " + inputAmount + "U";
            }
        }

        private string HandleVeganTakeNBakeLineAmount(string inputAmount, string source)
        {
            if (source == "") { return inputAmount + "UV"; }
            else
            {
                return source + ", " + inputAmount + "UV";
            }
        }

        /// <summary>
        /// Conditions to check for each row of the backlist/bakers list.
        /// Condition 1: If backlistitem represents the POTM, the list is checked for any items where bool IsPOTM is true.
        /// Condition 2: If the backlist item has an associated vegan pie within the list, its quantity is represented in the row denoted with a "V"
        /// Condition 3: If the backlist item and lineitem share the same catalog id
        /// </summary>
        /// <param name="lineItem"></param>
        /// <param name="BackListItemId"></param>
        /// <param name="isVegan"></param>
        /// <returns></returns>
        private bool MatchCatalogId(PetsiOrderLineItem lineItem, string BackListItemId, out bool isVegan, out bool isTakeNBake, out bool isVeganTakeNBake)
        {
            isVegan = false;
            isTakeNBake = false;
            isVeganTakeNBake = false;

            if (BackListItemId == Identifiers.CATEGORY_POTM && lineItem.IsPOTM())
            {
                return true;
            }
            else if(lineItem.IsVeganTo(BackListItemId))
            {
                isVegan = true;
                return true;
            }/*
            else if(lineItem.IsTakeNBakeTo(BackListItemId))
            {
                isTakeNBake = true;
                return true;
            }
            else if (lineItem.IsVeganTakeNBakeTo(BackListItemId))
            {
                isVeganTakeNBake = true;
                return true;
            }*/
            else if(lineItem.CatalogObjectId == BackListItemId)
            {
                return true;
            }
            return false;
        }

        protected override void FormatTable(IXLWorksheet page)//format line?
        {
            string tableRange = TableFormat.BuildRange(_rootPosition.row, _rowIndex - 2, "B", "F");
            string headerRange = TableFormat.BuildRange(_rootPosition.row, _rootPosition.row, "B", "F");
            string footerRange = TableFormat.BuildRange(_rowIndex - 1, _rowIndex - 1, "B", "F");

            TableFormat.RangeAllBorders(page, tableRange);
            TableFormat.RangeAlignment(page, "center", tableRange);
            TableFormat.RangeFontSize(page, 18, tableRange);
            TableFormat.ColWidthFitSizeOfText_MinWidth(page, "B:F", 8.57);
            //TableFormat.ColWidthFitSizeOfText(page, "B:F");
            //TableFormat.ColumnSetPixelLength(page, 20, "C:F");
            TableFormat.RangeBold(page, headerRange);

            TableFormat.RangeAlignment(page, "center", footerRange);
            TableFormat.RangeFontSize(page, 16, footerRange);
            TableFormat.RangeBold(page, footerRange);
        }
    }
}
