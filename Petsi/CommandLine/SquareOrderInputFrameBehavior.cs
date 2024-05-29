using Petsi.Input;
using Petsi.Interfaces;
using Petsi.Units;
using Square.Models;

namespace Petsi.CommandLine
{
    public class SquareOrderInputFrameBehavior : FrameBehaviorBase
    {
        SquareOrderInput comp;

        public SquareOrderInputFrameBehavior(SquareOrderInput soi)
        {
            comp = soi;
        }
        public override async Task Actions(Stack<ICommandable> contextChain, string actionIdentifier)
        {
            if (actionIdentifier == null) { return; }

            string[] args = actionIdentifier.ToLower().Split(' ');

            switch (args[0])
            {
                case "execute":
                    Console.WriteLine("Executing...");
                    comp.SetIsFileExecute(false);
                    comp.Execute().Wait();
                    Console.WriteLine("Done.");
                    break;
                case "fexecute":
                    if (args.Length < 3)
                    {
                        Console.WriteLine("Invalid fexectute command. \"fexecute < o|i > <fileName>");
                        break;
                    }
                    if (args[1] == "i")
                    {
                        comp.SetSquareResponses(comp.GetFileBehavior().BuildDataListFile<BatchRetrieveOrdersResponse>(args[2]));
                        comp.SetOrders(comp.BatchOrdersToOrderItems());
                        comp.SetIsFileExecute(true);
                        comp.Execute().Wait();
                    }
                    else if (args[1] == "o")
                    {
                        comp.SetOrders(comp.GetFileBehavior().BuildDataListFile<SquareOrderItem>(args[2]));
                        comp.SetIsFileExecute(true);
                        comp.Execute().Wait();
                    }
                    break;
                case "iserialize":
                    if (!comp.GetHasExecuted()) { Console.WriteLine("Nothing to Serialize, needs to execute first."); break; }
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Invalid iserialize command. \"iserialize <fileName>");
                        break;
                    }
                    comp.GetFileBehavior().DataListToFile("i_"+args[1], comp.GetSquareResponses());
                    break;
                case "oserialize":
                    if (!comp.GetHasExecuted()) { Console.WriteLine("Nothing to Serialize, needs to execute first."); break; }
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Invalid oserialize command. \"oserialize <fileName>");
                        break;
                    }
                    comp.GetFileBehavior().DataListToFile("o_"+args[1], comp.GetOrders());
                    break;
                case "listfp":
                    comp.GetFileBehavior().ListFileDirectory();
                    break;
                case "help":
                    PrintHelp();
                    break;
                default:
                    Console.WriteLine("Invalid SquareCatalogInput Command");
                    break;
            }
        }
        public override void CommandFrameView()
        {
            PrintHelp();
        }
        public override string GetComponentName()
        {
            return "Square Order Input";
        }
        private void PrintHelp()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("     exectute: pulls input data and loads model");
            Console.WriteLine("     fexectute <i|o> <fileName>: pulls input data from serialized file and loads model");
            Console.WriteLine("     iserialize <fileName>: saves input object to file");
            Console.WriteLine("     oserialize <fileName>: saves created object from input to file (CatalogItems)");
            Console.WriteLine("     listfp: list saved files in filepath");
            Console.WriteLine("     back: returns to Command Frame");
            Console.WriteLine("     help: lists valid commands");
        }
    }
}
