
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

namespace Petsi.Tests.ReportTests.BackListPie
{
    [Collection("Sequential")]
    public class BacklistPieMultiDayGenerated : IDisposable
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

        public BacklistPieMultiDayGenerated(ITestOutputHelper helper)
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

            List<PetsiOrder> generatedOrders = InputGenerator.GetTestOrdersMultiDay(InputGenerator.GetSummerPieIds(), InputGenerator.GetStandardOrderTypes(), 1,
                                                                                                                    DateTime.Parse("10/1/2024"), DateTime.Parse("10/8/2024"));
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
            ServiceManagerSingleton.Reset();
            ModelManagerSingleton.Reset();
        }

        [Fact]
        public void BacklistPieMultiDay_Generated()
        {
            //For Start 10/1/2024 - 10/8/2024, Sunday (1st and 8th overlap, so SUNDAY will occur twice, so a total of 36 items
            DateTime start = DateTime.Parse("10/1/2024");
            DateTime end = DateTime.Parse("10/8/2024");
            IXLWorkbook result = director.CreatePieBackList(start, end,
                false, true, true, true, true, true, true, true,
                "BlPieMultidayGenerated",
                BacklistTemplateFormatSelector.GetTestSummerPieTemplate()
                ).Result;

            XLWorkbook expected = new XLWorkbook("D:\\Git-Repos\\POMT_WPF\\Petsi.Tests\\ExpectedCases\\BackListPieMultiDayGeneratedResult.xlsx");
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