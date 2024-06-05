using Petsi.Input;
using Petsi.Models;
using Petsi.Reports;
using Petsi.Services;
using Square.Service;
using System.Windows;

namespace POMT_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            SquareClientFactory scf = new SquareClientFactory();

            OrderModelPetsi omp = new OrderModelPetsi();
            CatalogModelPetsi cmp = new CatalogModelPetsi();

            CategoryService categoryService = new CategoryService();
            CatalogService catalogIdService = new CatalogService();
            LabelService labelService = new LabelService();

            //ReportDirector director = new ReportDirector();

            SquareCatalogInput sci = new SquareCatalogInput(scf);
            SquareOrderInput soi = new SquareOrderInput(scf);
            //WholesaleInput wsi = new WholesaleInput();
            
            sci.Execute().Wait();
            soi.Execute().Wait();
            //wsi.Execute().Wait();
        }
    }

}
