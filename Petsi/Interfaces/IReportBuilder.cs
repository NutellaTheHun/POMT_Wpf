using ClosedXML.Excel;

namespace Petsi.Interfaces
{
    public interface IReportBuilder
    {
        /// <summary>
        /// IReportBuilder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList"></param>
        /// <returns></returns>
        public XLWorkbook BuildReport<T>(List<T> inputList, DateTime? targetDate, DateTime? endDate);
    }
}
