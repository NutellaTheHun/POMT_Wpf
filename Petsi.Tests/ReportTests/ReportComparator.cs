using ClosedXML.Excel;

namespace Petsi.Tests.ReportTests
{
    public class ReportComparator
    {
        public ReportComparator() { }

        public static bool Compare(IXLWorkbook expected, IXLWorkbook result, List<string> mismatches)
        {
            bool isEqual = true;
            int expectedRowRange, expectedColRange, resultRowRange, resultColRange;
            int expectedSheetNum, resultSheetNum;
            expectedSheetNum = expected.Worksheets.Count;
            resultSheetNum = result.Worksheets.Count;
            if (expectedSheetNum != resultSheetNum) { mismatches.Add($"Num of sheets not equal expected:{expectedSheetNum}, actual: {resultSheetNum}"); return false; }
            for(int sheet = 0; sheet < expectedSheetNum; sheet++)
            {
                IXLWorksheet expectedSheet = expected.Worksheets.ToList()[sheet];
                IXLWorksheet resultSheet = result.Worksheets.ToList()[sheet];
                expectedRowRange = expectedSheet.LastRowUsed().RowNumber();
                expectedColRange = expectedSheet.LastColumnUsed().ColumnNumber();

                resultRowRange = resultSheet.LastRowUsed().RowNumber();
                resultColRange = resultSheet.LastColumnUsed().ColumnNumber();
                for (int row = 1; row <= Math.Max(expectedRowRange, resultRowRange); row++)
                {
                    for(int col = 1; col <= Math.Max(expectedColRange,resultColRange); col++)
                    {
                        if(expectedSheet.Cell(row,col).Value.ToString() != resultSheet.Cell(row,col).Value.ToString())
                        {
                            //ignore the report id(1,2) and the date printed (2,2) positions
                            if ( (row != 1 && col != 2) || (row != 2 && col != 2) )
                            {
                                isEqual = false;
                                mismatches.Add($"{expectedSheet.Cell(row, col)}");
                            }
                        }
                    }
                }
            }
            return isEqual;
        }
    }
}
