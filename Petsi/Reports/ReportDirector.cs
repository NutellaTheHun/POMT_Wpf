using ClosedXML.Excel;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Reports.ReportBuilder;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Reports
{
    public class ReportDirector
    {
        public ReportDirector() 
        {

        }

        public async Task<IXLWorkbook> CreateFrontList(DateTime? targetDate, bool isPrint, bool isExport, bool isRetail, 
                                                                            bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater, bool isFarmer, string? reportName)
        {
            SystemLogger.LogStatus($"CreateFrontlist start targetDate: {targetDate}" +
                $"-isPrint {isPrint}, -isExport {isExport}, -isRetail {isRetail}, -isSquare {isSquare}, -isWholesale {isWholesale}, -isSpecial {isSpecial}, -isEzCater {isEzCater}, -isFarmer {isFarmer}");

            Report report = new Report("FrontList", isPrint, isExport);
            ReportBuilderFrontList builder = new ReportBuilderFrontList(report);

            //OrderModelPetsi orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            OrderModelPetsi orderModel = ModelManagerSingleton.GetInstance().GetOrderModel();

            builder.BuildReport(await orderModel.GetFrontListDataAsync(targetDate, isRetail, isSquare, isWholesale, isSpecial, isEzCater, isFarmer), targetDate, null);

            report.FinalizeReport(reportName);

            return report.Wb;
        }
        public async Task<IXLWorkbook> CreateBackList(DateTime? targetDate, DateTime? endDate, bool isPrint, bool isExport, bool isRetail, 
                                                                            bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater, bool isFarmer, string? reportName)
        {
            SystemLogger.LogStatus($"CreateBackList start startDate: {targetDate}, end date: {endDate}" +
                $"-isPrint {isPrint}, -isExport {isExport}, -isRetail {isRetail}, -isSquare {isSquare}, -isWholesale {isWholesale}, -isSpecial {isSpecial}, -isEzCater {isEzCater}, -isFarmer {isFarmer}");

            Report report = new Report("BackList", isPrint, isExport);
            ReportBuilderBackList builder = new ReportBuilderBackList(report);

            //OrderModelPetsi orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            OrderModelPetsi orderModel = ModelManagerSingleton.GetInstance().GetOrderModel();

            if (endDate == null)//if endDate is null, report is for single day, targetDate is used in report header as targetDate
            {
                builder.BuildReport(
                    await orderModel.GetBackListDataAsync(targetDate, endDate, 
                    isRetail, isSquare, isWholesale, isSpecial, isEzCater, isFarmer),
                                                                         targetDate, endDate);
            }
            else //otherwise printing all orders (for testing purposes) or is printing a range, displaying targetDate as a range not implemented yet, make arg param[] dateTime?
            {
                if(targetDate < endDate)
                {
                    builder.BuildReport(await orderModel.GetBackListDataAsync(targetDate, endDate, isRetail, isSquare, isWholesale, isSpecial, isEzCater, isFarmer), null, null);
                }
            }

            report.FinalizeReport(reportName);

            return report.Wb;
        }

        public async Task<IXLWorkbook> CreatePieBackList(DateTime? targetDate, DateTime? endDate, bool isPrint, bool isExport, bool isRetail, 
                                                                               bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater, bool isFarmer, string? reportName, List<BackListItem>? template)
        {
            SystemLogger.LogStatus($"CreatePieBackList start startDate: {targetDate}, end date: {endDate}" +
                $"-isPrint {isPrint}, -isExport {isExport}, -isRetail {isRetail}, -isSquare {isSquare}, -isWholesale {isWholesale}, -isSpecial {isSpecial}, -isEzCater {isEzCater}, -isFarmer {isFarmer}" );

            Report report = new Report("BackListPie", isPrint, isExport);
            ReportBuilderBackListPie builder = new ReportBuilderBackListPie(report, template);

            //OrderModelPetsi orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            OrderModelPetsi orderModel = ModelManagerSingleton.GetInstance().GetOrderModel();

            if (endDate == null)//if endDate is null, report is for single day, targetDate is used in report header as targetDate
            {
                builder.BuildReport(await orderModel.GetBackListDataAsync(targetDate, endDate, isRetail, isSquare, isWholesale, isSpecial, isEzCater, isFarmer), targetDate, endDate);
            }
            else //otherwise printing all orders (for testing purposes) or is printing a range, displaying targetDate as a range not implemented yet, make arg param[] dateTime?
            {
                if (targetDate < endDate)
                {
                    builder.BuildReport(await orderModel.GetBackListDataAsync(targetDate, endDate, isRetail, isSquare, isWholesale, isSpecial, isEzCater, isFarmer), null, null);
                }
            }

            report.FinalizeReport(reportName);

            return report.Wb;
        }

        public async Task<IXLWorkbook> CreatePastryBackList(DateTime? targetDate, DateTime? endDate, bool isPrint, bool isExport, bool isRetail, 
                                                                                  bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater, bool isFarmer, string? reportName, List<BackListItem>? template)
        {
            SystemLogger.LogStatus($"CreatePastryBackList start startDate: {targetDate}, end date: {endDate}");

            Report report = new Report("BackListPastry", isPrint, isExport);
            ReportBuilderBackListPastry builder = new ReportBuilderBackListPastry(report, template);

            //OrderModelPetsi orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            OrderModelPetsi orderModel = ModelManagerSingleton.GetInstance().GetOrderModel();

            if (endDate == null)//if endDate is null, report is for single day, targetDate is used in report header as targetDate
            {
                builder.BuildReport(await orderModel.GetBackListDataAsync(targetDate, endDate, isRetail, isSquare, isWholesale, isSpecial, isEzCater, isFarmer), targetDate, endDate);
            }
            else //otherwise printing all orders (for testing purposes) or is printing a range, displaying targetDate as a range not implemented yet, make arg param[] dateTime?
            {
                if (targetDate < endDate)
                {
                    builder.BuildReport(await orderModel.GetBackListDataAsync(targetDate, endDate, isRetail, isSquare, isWholesale, isSpecial, isEzCater, isFarmer), null, null);
                }
            }

            report.FinalizeReport(reportName);

            return report.Wb;
        }
        public IXLWorkbook CreateWsDay(DateTime? targetDate, bool isPrint, bool isExport, string? reportName)
        {

            SystemLogger.LogStatus($"CreateWsDay start startDate: {targetDate}, isPrint-{isPrint} isExport-{isExport}");

            Report report = new Report("WholesaleByDay", isPrint, isExport);
            ReportBuilderWsDay builder = new ReportBuilderWsDay(report);

            //OrderModelPetsi orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            OrderModelPetsi orderModel = ModelManagerSingleton.GetInstance().GetOrderModel();

            builder.BuildReport(orderModel.GetWsDayData(targetDate), targetDate, null);

            report.FinalizeReport(reportName);

            return report.Wb;
        }

        public IXLWorkbook CreateWsDayName(DateTime? targetDate, bool isPrint, bool isExport, string? reportName)
        {
            SystemLogger.LogStatus($"CreateWsDayName start startDate: {targetDate}, isPrint-{isPrint} isExport-{isExport}");

            Report report = new Report("WholesaleByDaybyName", isPrint, isExport);
            ReportBuilderWsDayName builder = new ReportBuilderWsDayName(report);

            //OrderModelPetsi orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            OrderModelPetsi orderModel = ModelManagerSingleton.GetInstance().GetOrderModel();

            builder.BuildReport(orderModel.GetWsDayNameData(targetDate), targetDate, null);
            report.isLandscape = true;
            report.FinalizeReport(reportName);

            return report.Wb;
        }
    }
}
