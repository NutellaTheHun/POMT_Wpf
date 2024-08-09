using ClosedXML.Excel;
using Petsi.Interfaces;
using Petsi.Reports.TableBuilder;
using Petsi.Units;

namespace Petsi.Reports.PageBuilder
{
    public abstract class PageBuilderBase : IPageBuilder
    {
        protected int maxContentLineCount;
        protected int maxPageRowCount;
        protected int _maxOrders;
        protected List<TableBase> _tables;
        protected Report _report;
        
        public PageBuilderBase(Report report)
        {
            _tables = new List<TableBase>();
            _report = report;
            ConfigureTables();
            ConfigureMaxRows();
            maxContentLineCount = GetPageContentMaxLineCount();
        }
        protected abstract void ConfigureMaxRows();
        protected abstract void ConfigureTables();
        public abstract int GetItemLineCount<T>(T item);
        public abstract bool IsRelevantItemToList<T>(T item, int lineItemCount);
        public virtual int GetPageContentMaxLineCount()
        {
            int result = 0;
            foreach (TableBase table in _tables)
            {
                result += table.GetTableMaxLineLimit();
            }
            return result;
        }
        public virtual void BuildPage<T>(XLWorkbook Wb, List<T> pageSizeOrders, string pageName)
        {
            IXLWorksheet page = Wb.Worksheets.Add(pageName);
            BuildReportHeader(page, pageSizeOrders);
            
            if (_tables.Count == 0)
            {
                throw new Exception("Table input empty");
            }
            /*
            //if a mono-table page
            else if (_tables.Count == 1)
            {
                _tables[0].BuildTable(page, pageSizeOrders, _report.GetReportTargetDate(), HandleTableTitle(pageSizeOrders[0]));
            }*/
            else //a multi table page
            {
                List<T> tableSizeOrders = new List<T>();
                int tableIndex = 0;

                int tableMaxOrderLimit = _tables[tableIndex].GetMaxOrderLimit();//set to 0 wsdayname
                int currentOrderCount = 0;

                int itemLineValue;
                int tableMaxLineLimit = _tables[tableIndex].GetTableMaxLineLimit();//set to 1 wsdayname
                int currentTableLineCount = 0;

                //***
                foreach (T item in pageSizeOrders)
                {
                    //itemLineValue must be checked on each item, value depends on state of item which varies
                    itemLineValue = GetItemLineCount(item);

                    if (!IsTableFull(tableMaxOrderLimit, currentOrderCount, itemLineValue, currentTableLineCount, tableMaxLineLimit))
                    {
                        if(item is PetsiOrderLineItem petsiOrderLineItem)
                        {
                            if (!petsiOrderLineItem.ItemName.ToLower().Contains("vegan"))
                            {
                                currentTableLineCount += itemLineValue;
                            }
                        }
                        else
                        {
                            currentTableLineCount += itemLineValue;
                        }
                        
                        tableSizeOrders.Add(item);
                        currentOrderCount++;
                    }
                    //Edgecase, if an order is larger than an entire page, it must be added,
                    //otherwise the builder will build an empty page and the order will be skipped.
                    else if (currentTableLineCount == 0)
                    {
                        tableSizeOrders.Add(item);
                        currentTableLineCount += itemLineValue;
                    }
                    else
                    {
                        _tables[tableIndex].BuildTable(page, tableSizeOrders, _report.GetReportTargetDate(), HandleTableTitle(tableSizeOrders[0]));

                        //reset
                        tableIndex++;

                        tableMaxOrderLimit = _tables[tableIndex].GetMaxOrderLimit();
                        tableMaxLineLimit = _tables[tableIndex].GetTableMaxLineLimit();

                        tableSizeOrders.Clear();
                        tableSizeOrders.Add(item);
                        currentOrderCount = 1;
                        currentTableLineCount = itemLineValue;
                    }

                }
                //handle remaining orders in ContextOrders once foreach is done.
                if (tableSizeOrders.Count > 0)
                {
                    _tables[tableIndex].BuildTable(page, tableSizeOrders, _report.GetReportTargetDate(), HandleTableTitle(tableSizeOrders[0]));
                }
            }
        }

        private bool IsTableFull(int tableMaxOrderLimit, int currentOrderCount, int itemLineValue, int currentTableLineCount, int tableMaxLineLimit)
        {
            if(tableMaxOrderLimit != 0)
            {
                if(currentOrderCount >= tableMaxOrderLimit)
                {
                    return true;
                }
            }
            if (currentTableLineCount + itemLineValue >= tableMaxLineLimit)
            {
                return true;
            }
            return false;
        }
        private string? HandleTableTitle<T>(T? t)
        {
            string result = null;
            if(typeof(T) == typeof(PetsiOrder))
            {
                var order = t as PetsiOrder;
                result = order.Recipient;
            }
            return result;
        }
        protected virtual void BuildReportHeader<T>(IXLWorksheet page, List<T>? pageSizeOrders)
        {
            ReportUtil.InitPageReportHeader(page, _report);
            //FormatReportHeader(page, 0, "");//0 and "" not used in this builder
        }
        protected void SetMaxOrders(int maxOrders) { _maxOrders = maxOrders; }
        public int GetMaxOrders() { return _maxOrders; }

    }
}
