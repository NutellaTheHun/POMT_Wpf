using ClosedXML.Excel;
using Newtonsoft.Json;
using Petsi.Input;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Reports;
using Petsi.Services;
using Petsi.Tests.ReportTests;
using Petsi.Units;
using Petsi.Utils;
using Square.Models;
using Square.Service;
using Xunit.Abstractions;

namespace Petsi.Tests.InputTests
{
    [Collection("Sequential")]
    public class SquareInputValentinesCuties : IDisposable
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

        string dateContext = "02/13/2025";

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

        public SquareInputValentinesCuties(ITestOutputHelper helper)
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
            PetsiConfig.TESTINGChangeCurrentDate(dateContext);

            omp = new OrderModelPetsi(null, null);

            rts = ReportTemplateService.Instance();

            scf = new SquareClientFactory();

            labelService = new LabelService();

            director = new ReportDirector();

            sci = new SquareCatalogInput(scf);
            soi = new SquareOrderInput(scf);
            BatchRetrieveOrdersResponse response = JsonConvert.DeserializeObject<BatchRetrieveOrdersResponse>(File.ReadAllText("D:\\Git-Repos\\POMT_WPF\\Petsi.Tests\\Input files\\valentinesCuties.txt"));
            soi.TestExecute(response);
        }

        [Fact]
        public void SquareValentinesCutiesItemParse()
        {
            DateTime start = DateTime.Parse(dateContext);
            IXLWorkbook result = director.CreatePieBackList(start, null,
                false, true, true, true, true, true, true, true,
                "ValentineCuties",
                BacklistTemplateFormatSelector.GetTestFallPieTemplate()
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
