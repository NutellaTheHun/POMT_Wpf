using ClosedXML.Excel;
using Petsi.Input;
using Petsi.Models;
using Petsi.Reports;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using Square.Service;

namespace Petsi.Tests.ReportTests
{
    [Collection("Sequential")]
    public class BackListPastryTest
    {
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
            omp.ClearModel();
            cmp.ClearModel();
            Assert.NotNull(result);
        }
    }
}
