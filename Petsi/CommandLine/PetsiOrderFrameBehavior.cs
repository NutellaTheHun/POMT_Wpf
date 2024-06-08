using Petsi.Interfaces;
using Petsi.Units;

namespace Petsi.CommandLine
{
    public class PetsiOrderFrameBehavior : FrameBehaviorBase, IFrameUnitBuild
    {
        PetsiOrder item;

        public PetsiOrderFrameBehavior(PetsiOrder po)
        {
            item = po;
        }
        public override Task Actions(Stack<ICommandable> contextChain, string actionIdentifier)
        {
            if (actionIdentifier == null) { return Task.CompletedTask; }

            string[] args = actionIdentifier.ToLower().Split(' ');
            switch (args[0])
            {
                case "add":
                    break;
                case "del":
                    break;
                case "repl":
                    break;
                case "help":
                    Console.WriteLine("Commands:");
                    break;
                default:
                    break;
            }
            return Task.CompletedTask;
        }

        public void Build()
        {
            throw new NotImplementedException();
        }

        public override void CommandFrameView()
        {
            Console.WriteLine(
                "          Recipient: " + item.Recipient +
                "\n         Order Id: " + item.OrderId +
                "\n   Order Due Date: " + DateTime.Parse(item.OrderDueDate).ToShortDateString() +
                "\nOrder Pickup Time: " + DateTime.Parse(item.OrderDueDate).ToShortTimeString() +
                "\nInput Origin Type: " + item.InputOriginType +
                "\n      Order  Type: " + item.OrderType +
                "\n Fulfillment Type: " + item.FulfillmentType +
                "\n             Note: " + item.Note +
                "\n       Line Items: "
                );
            PrintLineItems();
            Console.WriteLine("\ntype \"help\" for commands.");
        }

        public override string GetComponentName()
        {
            return "Petsi Order";
        }

        private void PrintLineItems() 
        {
            foreach(PetsiOrderLineItem item in item.GetLineItems())
            {
                Console.WriteLine( "   " +
                    item.ItemName + "- 3\":" + 
                    item.Amount3 + " 5\":" + 
                    item.Amount5 + " 8\":" +
                    item.Amount8 + " 10\":" +
                    item.Amount10);
            }
        }
    }
}
