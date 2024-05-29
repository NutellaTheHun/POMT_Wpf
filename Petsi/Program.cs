using Petsi.CommandLine;
using Petsi.Input;
using Petsi.Models;
using Petsi.Reports;
using Petsi.Services;
using Square.Service;

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

            SquareClientFactory scf = new SquareClientFactory();

            OrderModelPetsi omp = new OrderModelPetsi();
            CatalogModelPetsi cmp = new CatalogModelPetsi();

            CategoryService categoryService = new CategoryService();
            CatalogService catalogIdService = new CatalogService();
            LabelService labelService = new LabelService();

            ReportDirector director = new ReportDirector();

            SquareCatalogInput sci = new SquareCatalogInput(scf);
            SquareOrderInput soi = new SquareOrderInput(scf);
            WholesaleInput wsi = new WholesaleInput();

            CommandFrame cli = CommandFrame.GetInstance();
            string cmdLine = null;

            while (cmdLine != "exit")
            {
                Console.WriteLine("\nContext: " + cli.GetCurrentContextName());
                Console.Write("\n      > ");
                cmdLine = Console.ReadLine();
                Console.Clear();
                cli.RunCommand(cmdLine); 
            }

        }
    }
}