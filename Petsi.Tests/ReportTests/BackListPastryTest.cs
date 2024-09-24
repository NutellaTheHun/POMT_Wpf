using ClosedXML.Excel;
using Petsi.Input;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Reports;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using Square.Service;
using Xunit.Abstractions;

namespace Petsi.Tests.ReportTests
{
    [Collection("Sequential")]
    public class BackListPastryTest : IDisposable
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

        public BackListPastryTest(ITestOutputHelper helper)
        {
            this.helper = helper;
            teh = new TestEnvHelper();

            List<PetsiOrder> testOneShotOrders = teh.fb.BuildDataListFile<PetsiOrder>(Identifiers.TEST_ONESHOT_ORDERS);
            List<PetsiOrder> testPeriodicOrders = teh.fb.BuildDataListFile<PetsiOrder>(Identifiers.TEST_PERIODIC_ORDERS);

            List<CatalogItemPetsi> catalogItems = teh.fb.BuildDataListFile<CatalogItemPetsi>(Identifiers.MAIN_MODEL_CATALOG_FILE);
            List<(string name, string id)> categories = teh.fb.BuildDataListFile<(string name, string id)>(Identifiers.MAIN_MODEL_CATALOG_CATEGORIES_FILE);

            config = PetsiConfig.GetInstance();

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

        [Fact]
        public void BackListPastryTest_SingleDay()
        {
            TestEnvHelper teh = new TestEnvHelper();

            List<PetsiOrder> testOneShotOrders = teh.fb.BuildDataListFile<PetsiOrder>(Identifiers.TEST_ONESHOT_ORDERS);
            List<PetsiOrder> testPeriodicOrders = teh.fb.BuildDataListFile<PetsiOrder>(Identifiers.TEST_PERIODIC_ORDERS);

            List<CatalogItemPetsi> catalogItems = teh.fb.BuildDataListFile<CatalogItemPetsi>(Identifiers.MAIN_MODEL_CATALOG_FILE);
            List<(string name, string id)> categories = teh.fb.BuildDataListFile<(string name, string id)> (Identifiers.MAIN_MODEL_CATALOG_CATEGORIES_FILE);

            PetsiConfig config = PetsiConfig.GetInstance();

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

            IXLWorkbook result = director.CreatePastryBackList(start, null,
                false, true, true, true, true, true, true, true).Result;
            //omp.ClearModel();
            //cmp.ClearModel();
            Assert.NotNull(result);
        }
    }
}
