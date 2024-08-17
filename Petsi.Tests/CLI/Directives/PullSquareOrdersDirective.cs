using Petsi.Units;
using Petsi.Input;
using Square.Service;
using Petsi.Models;
using Petsi.Services;

namespace Petsi.Tests.CLI.Directives
{
    public class PullSquareOrdersDirective : Directive
    {
        public PullSquareOrdersDirective()
        {
            name = "pso";
            argSize = 4;
            description = "Creates a serialized file of order data from square order API\n" +
                "\t Given a date range and file name.\n" +
                "\t <pso> <startDate> <endDate> <fileName>";
        }
        public override void Execute(string[] args, Executor executor)
        {
            //Task<List<PetsiOrder>> AsyncGetSquareOrders(SquareClientFactory squareClient, List<string> locationIds, List<string> states,
            //string? createStartAt, string? createEndAt)
            //List<PetsiOrder> result = new List<PetsiOrder>();
            CatalogModelPetsi cmp = new CatalogModelPetsi();

            CategoryService categoryService = new CategoryService();
            CatalogService catalogIdService = new CatalogService();

            
            SquareClientFactory scf = new SquareClientFactory();

            SquareCatalogInput sci = new SquareCatalogInput(scf);

            SquareOrderInput sor = new SquareOrderInput(scf);

            List<string> locIds = new List<string>() { SquareVars.Chill, SquareVars.Vill };
            List<string> states = new List<string>() { SquareVars.OPEN, SquareVars.COMPL };
            List<PetsiOrder> orders = sor.AsyncGetSquareOrders(scf, locIds, states, "2024-01-01", null, args[1], args[2]).Result;
            foreach (PetsiOrder order in orders)
            {
                Console.WriteLine($"{order.OrderId} : {order.Recipient} : {order.LineItems.Count} unique items");
            }

        }
    }
}
