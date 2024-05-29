using ClosedXML.Excel;

namespace Petsi.Interfaces
{
    public interface ITableBuilder
    {
        /// <summary>
        /// ITableBuilder Method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="tableOrders"></param>
        public void BuildTable<T>(IXLWorksheet page, List<T> tableOrders, DateTime reportDate, string? recipient);
    }
}
