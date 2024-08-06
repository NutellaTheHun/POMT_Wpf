using ClosedXML.Excel;
namespace Petsi.Reports.TableBuilder
{
    public static class TableFormat
    {
        private static readonly double DEFAULT_CELL_LENGTH = 18.75f;
        private static readonly int MAX_LIST_LN_COUNT = 40;
        private static readonly int ROW_INDEX_START = 1;
        public static string BuildRange(int startRow, int endRow, string startColumn, string endColumn)
        {
            return(startColumn+startRow.ToString()+":"+endColumn+endRow.ToString());
        }
        public static void RangeBold(IXLWorksheet ws, string range)
        {
            ws.Range(range).Cells().Style.Font.SetBold(true);
        }
        public static void RangeFontSize(IXLWorksheet ws, int fontSize, string range)
        {
            ws.Range(range).Cells().Style.Font.SetFontSize(fontSize);
        }
        public static void RangeMerge(IXLWorksheet ws, string range)
        {
            ws.Range(range).Merge();
        }
        public static void RangeAllBorders(IXLWorksheet ws, string range)
        {
            ws.Range(range).Cells().Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(range).Cells().Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);
            ws.Range(range).Cells().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(range).Cells().Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        }
        public static void RangeAlignment(IXLWorksheet ws, string alignment, string range)
        {
            if (alignment == "center")
            {
                ws.Range(range).Cells().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Range(range).Cells().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            }
            else if (alignment == "left")
            {
                ws.Range(range).Cells().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                ws.Range(range).Cells().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            }
            else if (alignment == "right")
            {
                ws.Range(range).Cells().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                ws.Range(range).Cells().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            }
        }
        public static void RowWidthFitSizeOfText(IXLWorksheet ws, int startRow, int endRow)
        {
            for(int i = startRow; i < endRow; i++)
            {
                ws.Row(i).AdjustToContents();
                ws.Row(i).ClearHeight(); //Required due to bug with Row().AdjustToContents(). Currently doesn't factor in text wrapping.
                //solution found here: https://github.com/ClosedXML/ClosedXML/issues/934
            }
        }
        public static void ColWidthFitSizeOfText(IXLWorksheet ws, string colRange)
        {
             ws.Columns(colRange).AdjustToContents();
        }

        public static void ColWidthFitSizeOfText_MinWidth(IXLWorksheet ws, string colRange, double minWidth)
        {
            ws.Columns(colRange).AdjustToContents();
            foreach (var col in ws.Columns())
            {
                if (col.Width < minWidth)
                {
                    col.Width = minWidth;
                }
            }
        }
        public static void WrapText(IXLWorksheet ws, /*int targetRow, params string[] cols*/string range)
        {
            ws.Range(range).Cells().Style.Alignment.WrapText = true;
        }
        public static void ColumnIncreaseLength(IXLWorksheet ws, int sizeMultiplier, string colRange)
        {
            double cellSize = sizeMultiplier * DEFAULT_CELL_LENGTH;
            ws.Columns(colRange).Width = cellSize;
        }
        public static void ColumnSetPixelLength(IXLWorksheet ws, double width, string colRange)
        {
            ws.Columns(colRange).Width = width;
        }
        public static string MaxLineLength(string input, int maxLength)
        {
            if(input.Length <= maxLength) { return input; }
            return input.Substring(0, maxLength);
        }
    }
}
