using ClosedXML.Excel;
using Petsi.Utils;

namespace Petsi.Reports
{
    public static class ReportUtil
    {
        /// <summary>
        /// might not be the accurate depending on font size
        /// </summary>
        public static readonly int MAX_LIST_CONTENT_LN_COUNT = 41;
        /// <summary>
        /// might not be the accurate depending on font size
        /// </summary>
        private static readonly int MAX_LIST_LN_COUNT = 37;
        private static readonly int ROW_INDEX_START = 1;
        private static readonly double DEFAULT_CELL_LENGTH = 18.75f;
        /// <summary>
        /// returns the size and corresponding amount from a lineItem, can be either 3", 5", 8", 10". For orders from SquareOrderInput,
        /// only 1 size should be populated.
        /// </summary>
        /// <param name="lineItem"></param>
        /// <param name="size"></param>
        /// <param name="amount"></param>
        /// <exception cref="Exception"></exception>
        public static void Save(XLWorkbook workbook, string filepathAndFileName){ workbook.SaveAs(filepathAndFileName + ".xlsx");}
        public static int CreateReportId()
        {
            int number = 0;
            try
            {
                if (File.Exists(PetsiConfig.GetInstance().GetFilepath(Identifiers.SETTING_REPORT_CNT_PATH)))
                {
                    string content = File.ReadAllText(PetsiConfig.GetInstance().GetFilepath(Identifiers.SETTING_REPORT_CNT_PATH));
                    int.TryParse(content, out number);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }

            IncrementReportId(number);
            return number;
        }
        private static void IncrementReportId(int number)
        {
            int num = number + 1;
            try
            {
                File.WriteAllText(PetsiConfig.GetInstance().GetFilepath(Identifiers.SETTING_REPORT_CNT_PATH), num.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing file: " + ex.Message);
            }
        }
        public static void InitPageReportHeader(IXLWorksheet page, Report report)
        {
            page.Cell("A1").Value = "Report #";
            page.Cell("B1").Value = report.ReportId;

            page.Cell("A2").Value = "Date Printed:";
            page.Cell("B2").Value = report.DatePrinted;

            page.Cell("A3").Value = "Target Date";
            page.Cell("B3").Value = HandleTargetDate(report);

            page.Cell("A4").Value = "Page #";
            page.Cell("B4").Value = report.GetPageCount();

            page.Cell("A5").Value = "Total Pages";
            page.Cell("B5").Value = "not calculated yet"; //once report is finished, director passes through all pages with final report size
        }
        private static string HandleTargetDate(Report report)
        {
            if(report.GetReportTargetDate().ToShortDateString() == "1/1/0001")
            {
                return "No set date";
            }
            return report.GetReportTargetDate().ToShortDateString();
        }

        public static void RemoveFile(string filePath)
        {
            File.Delete(filePath);
        }
    }
}
