using Petsi.Input;
using Petsi.Interfaces;
using Petsi.Units;

namespace Petsi.CommandLine
{
    public class WholesaleInputFrameBehavior : FrameBehaviorBase
    {
        WholesaleInput comp;

        public WholesaleInputFrameBehavior(WholesaleInput wsi)
        {
            comp = wsi;
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
                    comp.Execute();
                    Console.WriteLine("Done.");
                    break;
                case "fexecute":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Invalid fexectute command. \"fexecute <fileName>");
                        break;
                    }
                    comp.SetItems(comp.GetFileBehavior().BuildDataListFile<WholesaleItem>(args[1]));
                    comp.SetIsFileExecute(true);
                    await comp.Execute();
                    break;
                case "iserialize":
                    Console.WriteLine("iserialize not implemented for this component.\n Output of component is only saved data.");
                    break;
                case "oserialize":
                    if (!comp.GetHasExecuted()) { Console.WriteLine("Nothing to Serialize, needs to execute first."); break; }
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Invalid iserialize command. \"iserialize <fileName>");
                        break;
                    }
                    comp.GetFileBehavior().DataListToFile(args[1], comp.GetItems());
                    break;
                case "listf":
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
            return "Wholesale Input";
        }

        private void PrintHelp()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("     exectute: pulls input data and loads model");
            Console.WriteLine("     fexectute: pulls input data from serialized file and loads model");
            Console.WriteLine("     oserialize: saves created object from input to file (CatalogItems)");
            Console.WriteLine("     listfp: list saved files in filepath");
            Console.WriteLine("     back: returns to Command Frame");
        }
    }
}
