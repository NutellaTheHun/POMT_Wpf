using ClosedXML.Excel;
using Petsi.CommandLine;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Reports.ReportBuilder;
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

        public IXLWorkbook CreateFrontList(DateTime? targetDate)
        {
            Report report = new Report("FrontList");
            ReportBuilderFrontList builder = new ReportBuilderFrontList(report);

            orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);

            builder.BuildReport(orderModel.GetFrontListData(targetDate), targetDate, null);

            report.FinalizeReport();

            return report.Wb;
        }
        public IXLWorkbook CreateBackList(DateTime? targetDate, DateTime? endDate)
        {
            Report report = new Report("BackList");
            ReportBuilderBackList builder = new ReportBuilderBackList(report);

            orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);

            if(endDate == null)//if endDate is null, report is for single day, targetDate is used in report header as targetDate
            {
                builder.BuildReport(orderModel.GetBackListData(targetDate, endDate), targetDate, endDate);
            }
            else //otherwise printing all orders (for testing purposes) or is printing a range, displaying targetDate as a range not implemented yet, make arg param[] dateTime?
            {
                builder.BuildReport(orderModel.GetBackListData(targetDate, endDate), null, null);
            }

            report.FinalizeReport();

            return report.Wb;
        }
        public IXLWorkbook CreateWsDay(DateTime? targetDate)
        {
            Report report = new Report("WholesaleByDay");
            ReportBuilderWsDay builder = new ReportBuilderWsDay(report);

            orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);

            builder.BuildReport(orderModel.GetWsDayData(targetDate), targetDate, null);

            report.FinalizeReport();

            return report.Wb;
        }
        public IXLWorkbook CreateWsDayName(DateTime? targetDate)
        {
            Report report = new Report("WholesaleByDaybyName");
            ReportBuilderWsDayName builder = new ReportBuilderWsDayName(report);

            orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);

            builder.BuildReport(orderModel.GetWsDayNameData(targetDate), targetDate, null);
            report.isLandscape = true;
            report.FinalizeReport();

            return report.Wb;
        }
    }
}
