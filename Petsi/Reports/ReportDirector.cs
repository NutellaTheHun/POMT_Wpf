using ClosedXML.Excel;
using Petsi.CommandLine;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Reports.ReportBuilder;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Reports
{
    public class ReportDirector
    {
        OrderModelPetsi orderModel;
        ReportDirectorFrameBehavior frameBehavior;
        public ReportDirector() 
        {
            frameBehavior = new ReportDirectorFrameBehavior(this);
            CommandFrame.GetInstance().RegisterFrame("dir", frameBehavior);
        }

        public FrameBehaviorBase GetFrameBehavior() { return frameBehavior; }

        public IXLWorkbook CreateFrontList(DateTime? targetDate, bool isRetail, bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater)
        {
            Report report = new Report("FrontList");
            ReportBuilderFrontList builder = new ReportBuilderFrontList(report);

            orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            List<PetsiOrder> orders = FilterOrders(orderModel.GetOrders(), isRetail, isSquare, isWholesale, isSpecial, isEzCater);

            IEnumerable<PetsiOrder> query;
            if (targetDate != null)
            {//doesn't filter out wholesale because they're used for Coverpage and notes page, order data is filtered out in report builders
                query =
                from order in orders
                where (order.IsPeriodic == true && DateTime.Parse(order.OrderDueDate).DayOfWeek == targetDate.Value.DayOfWeek)  //wholesale/periodic is weekly, so by day of week
                      ||
                     (order.IsPeriodic == false && DateTime.Parse(order.OrderDueDate).ToShortDateString() == targetDate.Value.ToShortDateString())
                orderby order.FulfillmentType, DateTime.Parse(order.OrderDueDate).TimeOfDay
                select order;
            }
            else
            {
                query =
                from order in orders
                orderby order.FulfillmentType, DateTime.Parse(order.OrderDueDate).TimeOfDay
                select order;
            }

            //builder.BuildReport(orderModel.GetFrontListData(targetDate), targetDate, null);
            builder.BuildReport(query.ToList(), targetDate, null);

            report.FinalizeReport();

            return report.Wb;
        }
        public IXLWorkbook CreateBackList(DateTime? targetDate, DateTime? endDate, bool isRetail, bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater)
        {
            Report report = new Report("BackList");
            ReportBuilderBackList builder = new ReportBuilderBackList(report);

            orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            List<PetsiOrder> orders = FilterOrders(orderModel.GetOrders(), isRetail, isSquare, isWholesale, isSpecial, isEzCater);

            if (endDate == null)//if endDate is null, report is for single day, targetDate is used in report header as targetDate
            {
                builder.BuildReport(orderModel.GetBackListData(targetDate, endDate), targetDate, endDate);
            }
            else //otherwise printing all orders (for testing purposes) or is printing a range, displaying targetDate as a range not implemented yet, make arg param[] dateTime?
            {
                if(targetDate < endDate)
                {
                    builder.BuildReport(orderModel.GetBackListData(targetDate, endDate), null, null);
                }
                
            }

            report.FinalizeReport();

            return report.Wb;
        }
        public IXLWorkbook CreateWsDay(DateTime? targetDate, bool isRetail, bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater)
        {
            Report report = new Report("WholesaleByDay");
            ReportBuilderWsDay builder = new ReportBuilderWsDay(report);

            orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            List<PetsiOrder> orders = FilterOrders(orderModel.GetOrders(), isRetail, isSquare, isWholesale, isSpecial, isEzCater);

            builder.BuildReport(orderModel.GetWsDayData(targetDate), targetDate, null);

            report.FinalizeReport();

            return report.Wb;
        }

        public IXLWorkbook CreateWsDayName(DateTime? targetDate, bool isRetail, bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater)
        {
            Report report = new Report("WholesaleByDaybyName");
            ReportBuilderWsDayName builder = new ReportBuilderWsDayName(report);

            orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            List<PetsiOrder> orders = FilterOrders(orderModel.GetOrders(), isRetail, isSquare, isWholesale, isSpecial, isEzCater);

            builder.BuildReport(orderModel.GetWsDayNameData(targetDate), targetDate, null);
            report.isLandscape = true;
            report.FinalizeReport();

            return report.Wb;
        }

       //
        private List<PetsiOrder> FilterOrders(List<PetsiOrder> petsiOrders, bool isRetail, bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater)
        {
            List<PetsiOrder> result = new List<PetsiOrder>();
            foreach (PetsiOrder order in petsiOrders)
            {
                if(order.IsFrozen) { continue; }

                if(isRetail) { if (order.OrderType == Identifiers.ORDER_TYPE_RETAIL) result.Add(order); continue; }
                if(isSquare) { if (order.OrderType == Identifiers.ORDER_TYPE_SQUARE) result.Add(order); continue; }
                if(isWholesale) { if (order.OrderType == Identifiers.ORDER_TYPE_WHOLESALE) result.Add(order); continue; } 
                if(isSpecial) { if (order.OrderType == Identifiers.ORDER_TYPE_SPECIAL) result.Add(order); continue; }
                if(isEzCater) { if (order.OrderType == Identifiers.ORDER_TYPE_EZ_CATER) result.Add(order); continue; }
            }

            return result;
        }
    }
}
