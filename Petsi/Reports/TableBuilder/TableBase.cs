using ClosedXML.Excel;
using Petsi.Interfaces;

namespace Petsi.Reports.TableBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class TableBase : ITableBuilder
    {
        protected (int row,int col) _rootPosition;
        protected int _maxColumns;
        protected int _maxRows;
        protected int _maxOrdersLimit;
        protected int _rowIndex;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootPosition">Start position of table</param>
        /// <param name="maxColumns">max amount of columns</param>
        /// <param name="maxRows">max amount of rows</param>
        /// <param name="_maxOrdersLimit">Maximum amount of orders allowed per table</param>
        public TableBase((int row, int col) rootPosition, int maxColumns, int maxRows)
        {
            _rootPosition = rootPosition;
            _maxColumns = maxColumns;
            _maxRows = maxRows;
            _rowIndex = rootPosition.row;
        }
        public abstract void BuildTable<T>(IXLWorksheet page, List<T> tableOrders, DateTime reportDate, string? recipient);
        protected abstract void FormatTable(IXLWorksheet page);
        public virtual int GetTableMaxLineLimit()
        {
            return _maxRows;
        }
        protected virtual void AddLine(IXLWorksheet page, ref int rowIndex, int startCol, params string[] inputValues)
        {
            int nextCol = 0;
            foreach(string value in inputValues)
            {
                page.Cell(rowIndex, startCol + nextCol).Value = value;
                nextCol++;
            }
            rowIndex++;
        }
        protected void SetMaxOrderLimit(int maxOrders) { _maxOrdersLimit = maxOrders; }
        public int GetMaxOrderLimit() { return _maxOrdersLimit; }
    }
}
