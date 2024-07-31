﻿using ClosedXML.Excel;
using Petsi.CommandLine;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Reports.ReportBuilder;
using Petsi.Utils;

namespace Petsi.Reports
{
    public class ReportDirector
    {
        ReportDirectorFrameBehavior frameBehavior;
        public ReportDirector() 
        {
            frameBehavior = new ReportDirectorFrameBehavior(this);
            CommandFrame.GetInstance().RegisterFrame("dir", frameBehavior);
        }

        public FrameBehaviorBase GetFrameBehavior() { return frameBehavior; }

        public IXLWorkbook CreateFrontList(DateTime? targetDate, bool isPrint, bool isExport, bool isRetail, bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater, bool isFarmer)
        {
            Report report = new Report("FrontList", isPrint, isExport);
            ReportBuilderFrontList builder = new ReportBuilderFrontList(report);

            OrderModelPetsi orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);

            builder.BuildReport(orderModel.GetFrontListData(targetDate, isRetail, isSquare, isWholesale, isSpecial, isEzCater, isFarmer), targetDate, null);

            report.FinalizeReport();

            return report.Wb;
        }
        public IXLWorkbook CreateBackList(DateTime? targetDate, DateTime? endDate, bool isPrint, bool isExport, bool isRetail, bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater, bool isFarmer)
        {
            Report report = new Report("BackList", isPrint, isExport);
            ReportBuilderBackList builder = new ReportBuilderBackList(report);

            OrderModelPetsi orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);

            if (endDate == null)//if endDate is null, report is for single day, targetDate is used in report header as targetDate
            {
                builder.BuildReport(orderModel.GetBackListData(targetDate, endDate, isRetail, isSquare, isWholesale, isSpecial, isEzCater, isFarmer), targetDate, endDate);
            }
            else //otherwise printing all orders (for testing purposes) or is printing a range, displaying targetDate as a range not implemented yet, make arg param[] dateTime?
            {
                if(targetDate < endDate)
                {
                    builder.BuildReport(orderModel.GetBackListData(targetDate, endDate, isRetail, isSquare, isWholesale, isSpecial, isEzCater, isFarmer), null, null);
                }
            }

            report.FinalizeReport();

            return report.Wb;
        }

        public IXLWorkbook CreatePieBackList(DateTime? targetDate, DateTime? endDate, bool isPrint, bool isExport, bool isRetail, bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater, bool isFarmer)
        {
            Report report = new Report("BackListPie", isPrint, isExport);
            ReportBuilderBackListPie builder = new ReportBuilderBackListPie(report);

            OrderModelPetsi orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);

            if (endDate == null)//if endDate is null, report is for single day, targetDate is used in report header as targetDate
            {
                builder.BuildReport(orderModel.GetBackListData(targetDate, endDate, isRetail, isSquare, isWholesale, isSpecial, isEzCater, isFarmer), targetDate, endDate);
            }
            else //otherwise printing all orders (for testing purposes) or is printing a range, displaying targetDate as a range not implemented yet, make arg param[] dateTime?
            {
                if (targetDate < endDate)
                {
                    builder.BuildReport(orderModel.GetBackListData(targetDate, endDate, isRetail, isSquare, isWholesale, isSpecial, isEzCater, isFarmer), null, null);
                }
            }

            report.FinalizeReport();

            return report.Wb;
        }

        public IXLWorkbook CreatePastryBackList(DateTime? targetDate, DateTime? endDate, bool isPrint, bool isExport, bool isRetail, bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater, bool isFarmer)
        {
            Report report = new Report("BackListPie", isPrint, isExport);
            ReportBuilderBackListPastry builder = new ReportBuilderBackListPastry(report);

            OrderModelPetsi orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);

            if (endDate == null)//if endDate is null, report is for single day, targetDate is used in report header as targetDate
            {
                builder.BuildReport(orderModel.GetBackListData(targetDate, endDate, isRetail, isSquare, isWholesale, isSpecial, isEzCater, isFarmer), targetDate, endDate);
            }
            else //otherwise printing all orders (for testing purposes) or is printing a range, displaying targetDate as a range not implemented yet, make arg param[] dateTime?
            {
                if (targetDate < endDate)
                {
                    builder.BuildReport(orderModel.GetBackListData(targetDate, endDate, isRetail, isSquare, isWholesale, isSpecial, isEzCater, isFarmer), null, null);
                }
            }

            report.FinalizeReport();

            return report.Wb;
        }
        public IXLWorkbook CreateWsDay(DateTime? targetDate, bool isPrint, bool isExport)
        {
            Report report = new Report("WholesaleByDay", isPrint, isExport);
            ReportBuilderWsDay builder = new ReportBuilderWsDay(report);

            OrderModelPetsi orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);

            builder.BuildReport(orderModel.GetWsDayData(targetDate), targetDate, null);

            report.FinalizeReport();

            return report.Wb;
        }

        public IXLWorkbook CreateWsDayName(DateTime? targetDate, bool isPrint, bool isExport)
        {
            Report report = new Report("WholesaleByDaybyName", isPrint, isExport);
            ReportBuilderWsDayName builder = new ReportBuilderWsDayName(report);

            OrderModelPetsi orderModel = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);

            builder.BuildReport(orderModel.GetWsDayNameData(targetDate), targetDate, null);
            report.isLandscape = true;
            report.FinalizeReport();

            return report.Wb;
        }
    }
}
