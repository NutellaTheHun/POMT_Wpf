﻿using ClosedXML.Excel;
using Petsi.Filing;
using Petsi.Managers;
using Petsi.Reports.TableBuilder;
using Petsi.Utils;

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
            _filePath = PetsiConfig.GetInstance().GetFilepath("createdReportPath");
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
                CaptureEnvironment();
            }
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
