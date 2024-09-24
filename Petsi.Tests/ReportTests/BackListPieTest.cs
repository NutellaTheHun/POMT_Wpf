using ClosedXML.Excel;
using Petsi.Input;
using Petsi.Models;
using Petsi.Reports;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using Square.Service;
using Xunit.Abstractions;

namespace Petsi.Tests.ReportTests
{/*
    public class BackListPieTest : IDisposable
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
        LabelService labelService ;
        ReportDirector director;
        SquareCatalogInput sci;
        SquareOrderInput soi;

        public BackListPieTest(ITestOutputHelper helper)
        {
            this.helper = helper;
            teh = new TestEnvHelper();

            List<CatalogItemPetsi> catalogItems = teh.fb.BuildDataListFile<CatalogItemPetsi>(Identifiers.MAIN_MODEL_CATALOG_FILE);
            List<(string name, string id)> categories = teh.fb.BuildDataListFile<(string name, string id)>(Identifiers.MAIN_MODEL_CATALOG_CATEGORIES_FILE);

            cmp = new CatalogModelPetsi(catalogItems, categories);

            categoryService = new CategoryService();
            categoryService.Update(cmp);
            catalogIdService = new CatalogService();
            catalogIdService.Update(cmp);

            config = PetsiConfig.GetInstance();

            List<PetsiOrder> generatedOrders = InputGenerator.GetTestOrders(InputGenerator.GetSummerPieIds(), InputGenerator.GetStandardOrderTypes(), 1, DateTime.Today.Date);
            omp = new OrderModelPetsi(generatedOrders);

            rts = ReportTemplateService.Instance();

            scf = new SquareClientFactory();

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
        }

        [Fact]
        public void BackListPieTest_GeneratedOrder()
        {
           
            //  - - - - - 

            DateTime start = DateTime.Today.Date;
            IXLWorkbook result = director.CreatePieBackList(start, null,
                false, true, true, true, true, true, true, true).Result;

            //omp.ClearModel();
            //cmp.ClearModel();
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

        [Fact]
        public void BackListPieTest_SingleDay()
        {
            
            TestEnvHelper teh = new TestEnvHelper();

            List<PetsiOrder> testOneShotOrders = teh.fb.BuildDataListFile<PetsiOrder>(Identifiers.TEST_ONESHOT_ORDERS);
            List<PetsiOrder> testPeriodicOrders = teh.fb.BuildDataListFile<PetsiOrder>(Identifiers.TEST_PERIODIC_ORDERS);

            List<CatalogItemPetsi> catalogItems = teh.fb.BuildDataListFile<CatalogItemPetsi>(Identifiers.MAIN_MODEL_CATALOG_FILE);
            List<(string name, string id)> categories = teh.fb.BuildDataListFile<(string name, string id)>(Identifiers.MAIN_MODEL_CATALOG_CATEGORIES_FILE);

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
            
            //  - - - - - 

            DateTime start = DateTime.Parse("9/14/2024");
            IXLWorkbook result = director.CreatePieBackList(start, null,
                false, true, true, true, true, true, true, true).Result;

            //omp.ClearModel();
            //cmp.ClearModel();

            XLWorkbook expected = new XLWorkbook("D:\\Git-Repos\\POMT_WPF\\Petsi.Tests\\ExpectedCases\\BackListPieSingleDayResult.xlsx");
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

    }*/
}
