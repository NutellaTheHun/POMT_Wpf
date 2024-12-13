
using ClosedXML.Excel;
using Newtonsoft.Json;
using Petsi.Input;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Reports;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using Square.Models;
using Square.Service;
using Xunit.Abstractions;

namespace Petsi.Tests.ReportTests.BackListPastry
{
    [Collection("Sequential")]
    public class BoxOf6BaconBiscSerialized : IDisposable
    {
        private readonly ITestOutputHelper helper;
        TestEnvHelper teh;
        CatalogModelPetsi cmp;
        OrderModelPetsi omp;
        CategoryService categoryService;
        CatalogService catalogIdService;
        PetsiConfig config;
        ReportTemplateService rts;
        SquareClientFactory scf;
        LabelService labelService;
        ReportDirector director;
        SquareCatalogInput sci;
        SquareOrderInput soi;
        string dateContext = "11/22/2024";

        public BoxOf6BaconBiscSerialized(ITestOutputHelper helper)
        {
            this.helper = helper;
            teh = new TestEnvHelper();

            List<PetsiOrder> testOneShotOrders = teh.fb.BuildDataListFile<PetsiOrder>(Identifiers.TEST_ONESHOT_ORDERS);
            List<PetsiOrder> testPeriodicOrders = teh.fb.BuildDataListFile<PetsiOrder>(Identifiers.TEST_PERIODIC_ORDERS);

            List<CatalogItemPetsi> catalogItems = teh.fb.BuildDataListFile<CatalogItemPetsi>(Identifiers.MAIN_MODEL_CATALOG_FILE);
            List<(string name, string id)> categories = teh.fb.BuildDataListFile<(string name, string id)>(Identifiers.MAIN_MODEL_CATALOG_CATEGORIES_FILE);

            config = PetsiConfig.GetInstance();
            PetsiConfig.TESTINGChangeCurrentDate(dateContext);

            omp = new OrderModelPetsi(testOneShotOrders, testPeriodicOrders);
            cmp = new CatalogModelPetsi(catalogItems, categories);
            rts = ReportTemplateService.Instance();

            scf = new SquareClientFactory();

            categoryService = new CategoryService();
            categoryService.Update(cmp);
            catalogIdService = new CatalogService();
            catalogIdService.Update(cmp);
            labelService = new LabelService();

            director = new ReportDirector();

            sci = new SquareCatalogInput(scf);
            soi = new SquareOrderInput(scf);
            BatchRetrieveOrdersResponse response = JsonConvert.DeserializeObject<BatchRetrieveOrdersResponse>(File.ReadAllText("D:\\Git-Repos\\POMT_WPF\\Petsi.Tests\\Input files\\BatchOrderResponseBoxOf6BiscTest.txt"));
            soi.TestExecute(response);
        }

        public void Dispose()
        {
            teh = null;
            cmp = null;
            omp = null;
            categoryService = null;
            catalogIdService = null;
            config = null;
            rts = null;
            scf = null;
            labelService = null;
            director = null;
            sci = null;
            soi = null;
            ServiceManagerSingleton.Reset();
            ModelManagerSingleton.Reset();
        }

        //Ensuring a Box of 6 Bacon Bisc is properlly translated to 6 Bacon Cheddar Biscuits
        [Fact]
        public void BackListPastryBoxOf6BaconBiscAggregationTest()
        {
            DateTime start = DateTime.Parse(dateContext);

            IXLWorkbook result = director.CreatePastryBackList(start, null,
                false, true, true, true, true, true, true, true, "BackListPastryBoxOf6BiscAggTest", BacklistTemplateFormatSelector.GetTestPastryTemplate()).Result;

            XLWorkbook expected = new XLWorkbook("D:\\Git-Repos\\POMT_WPF\\Petsi.Tests\\ExpectedCases\\BackListPastryBoxOf6BiscAggResult.xlsx");

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
