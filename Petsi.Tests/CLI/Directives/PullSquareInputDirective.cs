using Petsi.Units;
using Petsi.Input;
using Square.Service;
using Petsi.Models;
using Petsi.Services;

namespace Petsi.Tests.CLI.Directives
{
    public class PullSquareInputDirective : Directive
    {
        public PullSquareInputDirective()
        {
            name = "psi";
            argSize = 3;
        }

        public override string Description()
        {
            return "Creates a serialized file of order data from square order API\n" +
                "\t\t Given a date range (mm/dd/yyyy) and file name.\n" +
                "\t\t pso <startDate> <endDate> <fileName>";
        }
        public override void Execute(string[] args, Executor executor)
        {
            
            CatalogModelPetsi cmp = new CatalogModelPetsi();

            CategoryService categoryService = new CategoryService();
            CatalogService catalogIdService = new CatalogService();

            SquareClientFactory scf = new SquareClientFactory();

            SquareCatalogInput sci = new SquareCatalogInput(scf);
            sci.Execute().Wait();

            SquareOrderInput sor = new SquareOrderInput(scf);

            List<string> locIds = new List<string>() { SquareVars.Chill, SquareVars.Vill };
            List<string> states = new List<string>() { SquareVars.OPEN, SquareVars.COMPL };
            List<PetsiOrder> orders = sor.AsyncGetSquareOrders(scf, locIds, states, "2024-08-20", null, args[1], args[2]).Result;

            /*//test 
            foreach (PetsiOrder order in orders)
            {
                Console.WriteLine($"{order.OrderId} : {order.Recipient} : {order.LineItems.Count} unique items");
            }*/

            //Send to file now
            string fileName = args[3];
            executor.fb.DataListToFile("i%"+fileName, orders);
        }
    }
}
