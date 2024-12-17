using ClosedXML.Excel;
using Petsi.Filing;
using Petsi.Managers;
using Petsi.Reports.TableBuilder;
using Petsi.Utils;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Petsi.Services;
using System.Drawing.Printing;

namespace Petsi.Reports
{
    public class Report
    {
        private DateTime _targetDate;

        private string _filePath;
        public string ReportName { get; private set; }
        public string DatePrinted { get; private set; } //change to date time
        public int ReportId { get; private set; }

        public bool isLandscape;

        private bool isPrint;
        private bool isExport;
        public XLWorkbook Wb { get; private set; }
        FileBehavior fileBehavior;
        public Report(string name, bool isPrint, bool isExport)
        {
            _filePath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_REPORT_EXPORT_PATH);
            ReportName = name;//might need to expand, include date/time, or id/count?
            this.isPrint = isPrint;
            this.isExport = isExport;
            DatePrinted = DateTime.Now.ToShortDateString();
            ReportId = ReportUtil.CreateReportId();
            Wb = new XLWorkbook();
            string fp = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_ENVIRON_PATH);
            fileBehavior = new FileBehavior(fp + "\\" + ReportId.ToString());
        }
        public int GetPageCount()
        {
            return Wb.Worksheets.Count;
        }
        private void FinalizeTotalPageCount()
        {
            int totalPages = Wb.Worksheets.Count;
            foreach (var item in Wb.Worksheets)
            {
                item.Cell("B5").Value = totalPages;
            }
        }
        private void FormatReportHeader()
        {
            foreach (var item in Wb.Worksheets)
            {
                TableFormat.RangeAlignment(item, "left", "B1:B5");
            }
        }
        public void FinalizeReport(string? reportName)
        {
            if (Wb.Worksheets.Count > 0)
            {
                FinalizeTotalPageCount();
                FormatReportHeader();
                ReportUtil.IncrementReportId(ReportId);
                CaptureEnvironment();

                if (reportName == null)
                {
                    ReportUtil.Save(Wb, PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_REPORT_EXPORT_PATH) + "\\" + ReportName + ReportId);
                }
                else
                {
                    ReportUtil.Save(Wb, PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_REPORT_EXPORT_PATH) + "\\" + reportName);
                }
                
               
                if(isPrint)
                {
                    if (!PrinterReady()) { ErrorService.RaiseSoftExceptionHandlerError("Report Printer is not available."); }
                    //PrintReport(_filePath + "\\" + ReportName + ReportId);
                    PrintReport(PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_REPORT_EXPORT_PATH) + "\\" + ReportName + ReportId);
                }
                if (!isExport)
                {
                    //ReportUtil.Save(Wb, PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_REPORT_EXPORT_PATH) + "\\" + ReportName + ReportId);
                    File.Delete(PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_REPORT_EXPORT_PATH) + "\\" + ReportName + ReportId + ".xlsx");
                }
                else
                {
                    SystemLogger.LogStatus($"FinalizeReport {ReportId} Export Success");
                }
                SystemLogger.LogStatus($"FinalizeReport {ReportId} Success");
            }
            else
            {
                SystemLogger.LogWarning($"FinalizeReport {ReportId} Failed, report was 0 worksheets");
            }
        }

        public void HandlePrintAndExport()
        {
            if (Wb.Worksheets.Count > 0)
            {
                ReportUtil.Save(Wb, PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_REPORT_EXPORT_PATH) + "\\" + ReportName + ReportId);
                if (isPrint)
                {
                    if (!PrinterReady()) { ErrorService.RaiseSoftExceptionHandlerError("Report Printer is not available."); }
                    //PrintReport(_filePath + "\\" + ReportName + ReportId);
                    PrintReport(PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_REPORT_EXPORT_PATH) + "\\" + ReportName + ReportId);
                }
                if (!isExport)
                {
                    //ReportUtil.Save(Wb, PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_REPORT_EXPORT_PATH) + "\\" + ReportName + ReportId);
                    File.Delete(PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_REPORT_EXPORT_PATH) + "\\" + ReportName + ReportId + ".xlsx");
                }
                else
                {
                    SystemLogger.LogStatus($"FinalizeReport {ReportId} Export Success");
                }
            }
            else
            {
                SystemLogger.LogWarning($"FinalizeReport {ReportId} Failed, report was 0 worksheets");
            }
        }

        private bool PrinterReady()
        {
            PrinterSettings settings = new PrinterSettings();
            settings.PrinterName = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_STD_PRINTER);

            if (settings.IsValid) { return true; }
            return false;
        }

        private void PrintReport(string filepathFileName)
        {
            SystemLogger.LogStatus($"Printing report start");
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = app.Workbooks.Open(filepathFileName+".xlsx",
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            app.PrintCommunication = false;
            foreach (Worksheet ws in wb.Worksheets)
            {
                if(isLandscape)
                {
                    ws.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                }
                ws.PageSetup.FitToPagesWide = true;
            }
            app.PrintCommunication = true;

            wb.PrintOut(
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_STD_PRINTER), Type.Missing, Type.Missing, Type.Missing);

            // Cleanup:
            GC.Collect();
            GC.WaitForPendingFinalizers();


            wb.Close(false, Type.Missing, Type.Missing);
            Marshal.FinalReleaseComObject(wb);

            app.Quit();
            Marshal.FinalReleaseComObject(app);
            SystemLogger.LogStatus($"Printing report success");
        }


        public DateTime GetReportTargetDate() { return  _targetDate; }
        public void SetReportTargetDate(DateTime? date)
        {
            _targetDate = (DateTime)default;
            if(date != null)
            {
                _targetDate = (DateTime)date;
            }
        }
        public int GetReportId() { return ReportId; }
        /// <summary>
        /// Corresponding Load Environment, separate top level object?
        /// </summary>
        private void CaptureEnvironment()
        {
            Directory.CreateDirectory(fileBehavior.GetDirectoryName());
            EnvironCaptureRegistrySingleton.GetInstance().CaptureEnvironment(fileBehavior);
        }
    }
}
