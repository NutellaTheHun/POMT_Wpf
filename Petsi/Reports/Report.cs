using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Petsi.Filing;
using Petsi.Managers;
using Petsi.Reports.TableBuilder;
using Petsi.Utils;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


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
        public void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            string labelPath = ((PrintDocument)sender).DocumentName;
            e.Graphics.DrawImage(new Bitmap(labelPath), 0, 0);
        }
        private void PrintReport(string filepath, string fileName)
        {
            PrintDialog dialog = new PrintDialog();
            using (PrintDocument pd = new PrintDocument())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = filepath+fileName+".xlsx";
                    pd.OriginAtMargins = true;
                    pd.PrintPage += pd_PrintPage;
                    pd.DocumentName = filePath;
                    pd.Print();
                    pd.PrintPage -= pd_PrintPage;
                }
            }
            //
            //Entire Workbook
            //Scaling : All Columns to page
            //PrintDialog dialog = new PrintDialog();
            //PrintDocument pDocument = new PrintDocument();

            //pDocument.DocumentName = filepathAndFileName;
            //dialog.Document = pDocument;
            //dialog.AllowSomePages = true;
            //pDocument.PrinterSettings.PrinterName = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_STD_PRINTER);


            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    //pDocument.Print();
            //    Process p = new Process();
            //    p.StartInfo = new ProcessStartInfo()
            //    {
            //        CreateNoWindow = true,
            //        Verb = "print",
            //        FileName = filepathAndFileName //put the correct path here
            //    };
            //    p.Start();
            //}
            //using (PrintDialog printDialog = new PrintDialog())
            //{
            //    printDialog.AllowSomePages = true;
            //    printDialog.AllowSelection = true;
            //    printDialog.PrinterSettings.PrinterName = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_STD_PRINTER);
            //    if (printDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        var StartInfo = new ProcessStartInfo
            //        {
            //            CreateNoWindow = true,
            //            UseShellExecute = true,
            //            Verb = "printTo",
            //            Arguments = "\"" + printDialog.PrinterSettings.PrinterName + "\"",
            //            WindowStyle = ProcessWindowStyle.Hidden,
            //            WorkingDirectory = Path.GetDirectoryName(filepath),
            //            FileName = fileName
            //        };

            //        Process.Start(StartInfo);
            //    }
            //}
            //    Process p = new Process();
            //p.StartInfo = new ProcessStartInfo()
            //{
            //    CreateNoWindow = true,
            //    Verb = "print",
            //    WorkingDirectory = Path.GetDirectoryName(filepath),
            //    FileName = fileName + ".xlsx" //put the correct path here
            //};
            //p.Start();

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
