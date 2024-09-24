using ClosedXML.Excel;
using Petsi.Input;
using Petsi.Models;
using Petsi.Reports;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using Square.Service;
using Xunit.Abstractions;

namespace Petsi.Tests.ReportTests.BackListPie
{
    [Collection("Sequential")]
    public class BackListPieSingleDayGenerated
    {
        private readonly ITestOutputHelper helper;
        public BackListPieSingleDayGenerated(ITestOutputHelper helper)
        {
            this.helper = helper;
        }

        [Fact]
        public void BackListPieTest_GeneratedOrder()
        {
            TestEnvHelper teh = new TestEnvHelper();

            List<CatalogItemPetsi> catalogItems = teh.fb.BuildDataListFile<CatalogItemPetsi>(Identifiers.MAIN_MODEL_CATALOG_FILE);
            List<(string name, string id)> categories = teh.fb.BuildDataListFile<(string name, string id)>(Identifiers.MAIN_MODEL_CATALOG_CATEGORIES_FILE);

            CatalogModelPetsi cmp = new CatalogModelPetsi(catalogItems, categories);

            CategoryService categoryService = new CategoryService();
            categoryService.Update(cmp);
            CatalogService catalogIdService = new CatalogService();
            catalogIdService.Update(cmp);

            PetsiConfig config = PetsiConfig.GetInstance();

            List<PetsiOrder> generatedOrders = InputGenerator.GetTestOrders(InputGenerator.GetSummerPieIds(), InputGenerator.GetStandardOrderTypes(), 1, DateTime.Today.Date);
            OrderModelPetsi omp = new OrderModelPetsi(generatedOrders);

            ReportTemplateService rts = ReportTemplateService.Instance();

            SquareClientFactory scf = new SquareClientFactory();

            LabelService labelService = new LabelService();

            ReportDirector director = new ReportDirector();

            SquareCatalogInput sci = new SquareCatalogInput(scf);
            SquareOrderInput soi = new SquareOrderInput(scf);

            //  - - - - - 

            DateTime start = DateTime.Today.Date;
            IXLWorkbook result = director.CreatePieBackList(start, null,
                false, true, true, true, true, true, true, true).Result;

            omp.ClearModel();
            cmp.ClearModel();
            XLWorkbook expected = new XLWorkbook("D:\\Git-Repos\\POMT_WPF\\Petsi.Tests\\ExpectedCases\\BackListPieTest_GeneratedOrder_Expected.xlsx");
            List<string> mismatches = new List<string>();
            bool eval = ReportComparator.Compare(expected, result, mismatches);
            if (!eval)
            {
                foreach (string ln in mismatches)
                {
                    helper.WriteLine(ln);
                }

            }
            Assert.True(eval);
        }
    }
}
