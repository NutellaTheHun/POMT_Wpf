
using Petsi.Models;
using Petsi.Units;

namespace Petsi.Tests.CLI.Directives
{
    public class PullUserInputDirective : Directive
    {
        public PullUserInputDirective() 
        {
            name = "pui";
            argSize = 1;
        }
        public override string Description()
        {
            return "Gathers all currently existing serialized orders from the order model." +
                "\t\t pui <filename>";
        }

        public override void Execute(string[] args, Executor executor)
        {
            OrderModelPetsi model = new OrderModelPetsi();
            List<PetsiOrder> list = model.GetOrders();

            /* //test
            foreach (PetsiOrder order in list)
            {
                Console.WriteLine($"{order.OrderId} : {order.Recipient} : unique items {order.LineItems.Count}");
            }*/

            executor.fb.DataListToFile("i%" + args[1], list);
        }
    }
}
