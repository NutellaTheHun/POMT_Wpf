using Petsi.Input;
using Petsi.Models;
using Petsi.Reports;
using Petsi.Services;
using Square.Service;
using ClosedXML.Excel;
using System.Xml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Petsi
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());      
            /*
            SquareClientFactory scf = new SquareClientFactory();

            OrderModelPetsi omp = new OrderModelPetsi();
            CatalogModelPetsi cmp = new CatalogModelPetsi();

            CategoryService categoryService = new CategoryService();
            CatalogService catalogIdService = new CatalogService();
            LabelService labelService = new LabelService();

            ReportDirector director = new ReportDirector();

            SquareCatalogInput sci = new SquareCatalogInput(scf);
            SquareOrderInput soi = new SquareOrderInput(scf);
            */
            XLWorkbook a = new XLWorkbook();
            IXLWorksheet b = a.Worksheets.Add();

            b.Cell("a1").Value = 1;
            b.Cell("c1").Value = 2;

            XLWorkbook c = new XLWorkbook();
            IXLWorksheet d = a.Worksheets.Add();
            b.Cell("a1").Value = 1;

            //int rowRange = b.LastRowUsed().RowNumber();
            //int colRange = b.LastColumnUsed().ColumnNumber();
            //XLWorkbook expected = new XLWorkbook("D:\\Git-Repos\\POMT_WPF\\Petsi.Tests\\ExpectedCases\\BackListPieSingleDayResult.xlsx");
            //int rowRange = b.LastRowUsed().RowNumber();
            //int colRange = b.LastColumnUsed().ColumnNumber();
            /*
            foreach(IXLWorksheet ws in expected.Worksheets)
            {
                int rowRange = ws.LastRowUsed().RowNumber();
                int colRange = ws.LastColumnUsed().ColumnNumber();
                Console.WriteLine($"SHEET: {ws.Name}, ROW RANGE: {rowRange}, COL RANGE: {colRange}");
                string v;
                for (int i = 1; i <= rowRange; i++)
                {
                    for(int j = 1; j <= colRange; j++)
                    {
                        v = ws.Cell(i, j).Value.ToString();
                        if(v == "")
                        {
                            Console.Write("NA, ");
                        }
                        else
                        {
                            Console.Write($"{ws.Cell(i, j).Value.ToString()} ");
                        }
                        
                    }
                    Console.WriteLine();
                }
            }*/
            /*
            Console.WriteLine($"ROW RANGE: {rowRange} COL RANGE: {colRange}");
            for (int i = 1; i <= rowRange; i++) 
            {
                for (int j = 1; j <= colRange; j++)
                {
                    if(b.Cell(i,j).Value.ToString() != d.Cell(i,j).Value.ToString())
                    {
                        Console.WriteLine("FALSE");
                    }
                }
                if (b.Cell(1,i).Value.ToString() != ""){
                    Console.WriteLine(b.Cell(1, i).Value);
                }
                else
                {
                    Console.WriteLine($"{b.Cell(1, i)} is empty.");
                }
            }*/
            /*
            XLWorkbook c = new XLWorkbook();
            IXLWorksheet d = a.Worksheets.Add();
            b.Cell("a1").Value = 1;

            if (a == c)
            {
                Console.WriteLine("EQUAL");
            }
            else
            {
                Console.WriteLine("NOT EQUAL");
            }
            */

        }
    }
}