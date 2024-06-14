using ClosedXML.Excel;
using Petsi.Filing;
using Petsi.Managers;
using Petsi.Reports.TableBuilder;
using Petsi.Utils;
using System.Diagnostics;
using System.Drawing.Printing;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;



namespace Petsi.Reports
{
    public class Report
    {
        private DateTime _targetDate;
        private string _filePath;
        public string ReportName { get; private set; }
        public string DatePrinted { get; private set; } //change to date time
        public int ReportId { get; private set; }
        public XLWorkbook Wb { get; private set; }
        FileBehavior fileBehavior;
        public Report(string name)
        {
            _filePath = PetsiConfig.GetInstance().GetVariable("createdReportPath");
            ReportName = name;//might need to expand, include date/time, or id/count?
            DatePrinted = DateTime.Now.ToShortDateString();
            ReportId = ReportUtil.CreateReportId();
            Wb = new XLWorkbook();
            fileBehavior = new FileBehavior("environs/"+ReportId.ToString());
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
        public void FinalizeReport()
        {
            FinalizeTotalPageCount();
            FormatReportHeader();
            if(Wb.Worksheets.Count > 0)
            {
                ReportUtil.Save(Wb, _filePath + ReportName);
                PrintReport(_filePath, ReportName);
                CaptureEnvironment();
            }
        }

        private void PrintReport(string filepath, string fileName)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = app.Workbooks.Open(filepath + fileName + ".xlsx",
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            wb.PrintOut(
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            // Cleanup:
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Marshal.FinalReleaseComObject(wb);

            wb.Close(false, Type.Missing, Type.Missing);
            Marshal.FinalReleaseComObject(wb);

            app.Quit();
            Marshal.FinalReleaseComObject(app);

            /*
            XLWorkbook temp;
            int index = 1;
            foreach (IXLWorksheet sheet in wb.Worksheets)
            {
                temp = new XLWorkbook();
                temp.AddWorksheet(sheet);
                ReportUtil.Save(temp, filepath + fileName + index);
                var pi = new ProcessStartInfo(filepath + fileName + index + ".xlsx");
                pi.UseShellExecute = true;
                pi.Verb = "PrintTo";
                pi.Arguments = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_STD_PRINTER);
                var process = System.Diagnostics.Process.Start(pi);
                index++;
                //ReportUtil.RemoveFile(filepath  + fileName + index);
            }
            */
            /*
            //var pi = new ProcessStartInfo(filepath + fileName + ".xlsx");
            //pi.UseShellExecute = true;
            //pi.Verb = "PrintTo";
            //pi.Arguments = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_STD_PRINTER);
            //var process = System.Diagnostics.Process.Start(pi);
            */
        }

        private void FormatReportHeader()
        {
            foreach (var item in Wb.Worksheets)
            {
                TableFormat.RangeAlignment(item, "left", "B1:B5");
            }
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
            EnvironCaptureRegistrySingleton.GetInstance().CaptureEnvironment(fileBehavior);
        }


    }
}
