using ClosedXML.Excel;
using Petsi.Reports.PageBuilder;
using Petsi.Units;

namespace Petsi.Reports.ReportBuilder
{
    /// <inheritdoc/>
    public class ReportBuilderWsDayName : ReportBuilderBase
    {
        /// <inheritdoc/>
        public ReportBuilderWsDayName(Report report) : base(report)
        {

        }

        protected override void ConfigureBuilders()
        {
            pageBuilders.Add(new PageBuilderWsDayName(_report));
        }
        public override XLWorkbook BuildReport<T>(List<T> orderData, DateTime? targetDate)
        {
            int pageCount = 1;
            _report.SetReportTargetDate(targetDate);
            List<PetsiOrder> notSorted = orderData as List<PetsiOrder>;
            List<List<PetsiOrder>> orders = SortByDayOfWeek(notSorted);
            foreach(List<PetsiOrder> inputList in orders)
            {
                foreach (PageBuilderBase pageBuilder in pageBuilders)//will only build once, for orders, need to iterate all list for each builder, like table
                {
                    List<PetsiOrder> pageSizeOrders = new List<PetsiOrder>();
                    int builderMaxLineLimit = pageBuilder.GetPageContentMaxLineCount();
                    int currentLineCount = 0;

                    int builderMaxOrderLimit = pageBuilder.GetMaxOrders();
                    int currentOrderCount = 0;
                    int itemLineValue;
                    foreach (PetsiOrder item in inputList)
                    {
                        //orderLineValue must be checked on each item, value depends on state of item which varies
                        itemLineValue = pageBuilder.GetItemLineCount(item);

                        //Check PageBuilderFrontListCover GetItemLineCount() for understanding
                        if (itemLineValue == 0)
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
            }
            
            //---
            return _report.Wb;
        }

        private List<List<PetsiOrder>> SortByDayOfWeek(List<PetsiOrder>? notSorted)
        {
            Dictionary<string, List<PetsiOrder>> byDayDict = new Dictionary<string, List<PetsiOrder>>();
            string key;
            foreach (PetsiOrder order in notSorted)
            {
                key = DateTime.Parse(order.OrderDueDate).ToShortDateString();
                if (!byDayDict.ContainsKey(key))
                {
                    byDayDict[key] = new List<PetsiOrder>();
                }
                byDayDict[key].Add(order);
            }
            return byDayDict.Values.ToList();
        }
    }
}
