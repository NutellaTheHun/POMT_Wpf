using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Petsi.Input;
using Petsi.Models;
using Petsi.Reports;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using Square.Service;
using Xunit.Abstractions;

namespace Petsi.Tests.ReportTests
{
    public class BackListPieTest
    {
        private readonly ITestOutputHelper helper;
        public BackListPieTest(ITestOutputHelper helper)
        {
            this.helper = helper;
        }

        [Fact]
        public void BackListPieTest_SingleDay()
        {
            TestEnvHelper teh = new TestEnvHelper();

            List<PetsiOrder> testOneShotOrders = teh.fb.BuildDataListFile<PetsiOrder>(Identifiers.TEST_ONESHOT_ORDERS);
            List<PetsiOrder> testPeriodicOrders = teh.fb.BuildDataListFile<PetsiOrder>(Identifiers.TEST_PERIODIC_ORDERS);

            List<CatalogItemPetsi> catalogItems = teh.fb.BuildDataListFile<CatalogItemPetsi>(Identifiers.MAIN_MODEL_CATALOG_FILE);
            List<(string name, string id)> categories = teh.fb.BuildDataListFile<(string name, string id)>(Identifiers.MAIN_MODEL_CATALOG_CATEGORIES_FILE);

            PetsiConfig config = PetsiConfig.GetInstance();

            //These three items utilize StartupService, they're initialized before services to ensure the registration is smooth
            OrderModelPetsi omp = new OrderModelPetsi(testOneShotOrders, testPeriodicOrders);
            CatalogModelPetsi cmp = new CatalogModelPetsi(catalogItems, categories);
            ReportTemplateService rts = ReportTemplateService.Instance();

            SquareClientFactory scf = new SquareClientFactory();

            CategoryService categoryService = new CategoryService();
            categoryService.Update(cmp);
            CatalogService catalogIdService = new CatalogService();
            catalogIdService.Update(cmp);
            LabelService labelService = new LabelService();

            ReportDirector director = new ReportDirector();

            SquareCatalogInput sci = new SquareCatalogInput(scf);
            SquareOrderInput soi = new SquareOrderInput(scf);

            DateTime start = DateTime.Parse("9/14/2024");

            IXLWorkbook result = director.CreatePieBackList(start, null,
                false, true, true, true, true, true, true, true).Result;

            XLWorkbook expected = new XLWorkbook("D:\\Git-Repos\\POMT_WPF\\Petsi.Tests\\ExpectedCases\\BackListPieSingleDayResult.xlsx");
            List<string> mismatches = new List<string>();
            bool eval = ReportComparator.Compare(expected, result, mismatches);
            if(!eval)
            {
                foreach (string ln in mismatches) {
                    helper.WriteLine(ln);
                }
                
            }
            Assert.True(eval);
        }
    }
}
