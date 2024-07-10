using ClosedXML.Excel;
using Petsi.Interfaces;
using Petsi.Reports.PageBuilder;
using Petsi.Services;
using Petsi.Utils;


namespace Petsi.Reports.ReportBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ReportBuilderBase : IReportBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        protected Report _report;
        /// <summary>
        /// 
        /// </summary>
        protected List<PageBuilderBase> pageBuilders;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        public ReportBuilderBase(Report report)//constructor for abstract?
        {
            _report = report;
            pageBuilders = new List<PageBuilderBase>();
            ConfigureBuilders();
        }
        
        public virtual XLWorkbook BuildReport<T>(List<T> inputList, DateTime? targetDate, DateTime? targetRangeEndDate)
        {
            if(inputList.Count == 0) 
            { 
                SystemLogger.Log("Given list to report is empty"); 
                ErrorService.RaiseReportEmptyInput(); 
                return _report.Wb; 
            }
            int pageCount = 1;
            _report.SetReportTargetDate(targetDate);
            foreach (PageBuilderBase pageBuilder in pageBuilders)//will only build once, for orders, need to iterate all list for each builder, like table
            {
                List<T> pageSizeOrders = new List<T>();
                int builderMaxLineLimit = pageBuilder.GetPageContentMaxLineCount();
                int currentLineCount = 0;
                
                int builderMaxOrderLimit = pageBuilder.GetMaxOrders();
                int currentOrderCount = 0;
                int itemLineValue;
                foreach (T item in inputList)
                {
                    //orderLineValue must be checked on each item, value depends on state of item which varies
                    itemLineValue = pageBuilder.GetItemLineCount(item);
                    
                    //Check PageBuilderFrontListCover GetItemLineCount() for understanding
                    if( itemLineValue == 0 )
                    {
                        continue;
                    }
                    if (!IsPageFull(builderMaxOrderLimit, currentOrderCount, currentLineCount, itemLineValue, builderMaxLineLimit))
                    {
                        currentLineCount += itemLineValue;
                        currentOrderCount++;
                        pageSizeOrders.Add(item);
                    }

                    //Edgecase, if an order is larger than an entire page, it must be added,
                    //otherwise the builder will build an empty page and the order will be skipped.
                    else if (currentLineCount == 0)
                    {
                        pageSizeOrders.Add(item);
                        currentLineCount += itemLineValue;
                    }
                    else
                    {
                        pageBuilder.BuildPage(_report.Wb, pageSizeOrders, pageCount.ToString());
                        pageCount++;
                        //reset
                        pageSizeOrders.Clear();
                        pageSizeOrders.Add(item);
                        currentOrderCount = 1;
                        currentLineCount = itemLineValue;
                    }
                }
                //handle remaining orders in ContextOrders once foreach is done.
                if (pageSizeOrders.Count > 0)
                {
                    pageBuilder.BuildPage(_report.Wb, pageSizeOrders, pageCount.ToString());
                    pageCount++;
                }
            }
            //---
            return _report.Wb;
        }
        /// <summary>
        /// Checks whether the page builder has either reached max orders or maxed lines when parsing the list of items to be written to the page.
        /// </summary>
        /// <param name="pageMaxOrder">The pagebuilder's declared max amount of orders allowed on a page</param>
        /// <param name="currentOrderCount">the current amount of parsed orders</param>
        /// <param name="lineCount">the current amount of parsed lines</param>
        /// <param name="orderLineCount">the amount of lines one item/order is required to print</param>
        /// <param name="builderMaxLineCount">the pagebuilder's declared max amount of lines allowed on a page.</param>
        /// <returns></returns>
        protected bool IsPageFull(int? pageMaxOrder, int currentOrderCount, int lineCount, int orderLineCount, int builderMaxLineCount)
        {
            if (pageMaxOrder != 0)//0 == null
            {
                if (currentOrderCount >= pageMaxOrder)
                {
                    return true;
                }
            }
            if (lineCount + orderLineCount >= builderMaxLineCount)
            {
                return true;
            }
            return false;
        }
        protected abstract void ConfigureBuilders();
        protected virtual void SetReport(Report report) { _report = report; }
    }
}
