using ClosedXML.Excel;

namespace Petsi.Interfaces
{
    public interface IPageBuilder
    {
        /// <summary>
        /// IPageBuilder Method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Wb"></param>
        /// <param name="pageContextOrders"></param>
        /// <param name="pageName"></param>
        public void BuildPage<T>(XLWorkbook Wb, List<T> pageContextOrders, string pageName);
        /// <summary>
        /// IPageBuilder Method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public int GetItemLineCount<T>(T item);
    }
}
