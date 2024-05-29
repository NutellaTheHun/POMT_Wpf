using Petsi.Input;
using Petsi.Interfaces;
using Petsi.Units;
using Square.Models;

namespace Petsi.CommandLine
{
    public class CatalogInputFrameBehavior : FrameBehaviorBase
    {
        SquareCatalogInput comp;

        public CatalogInputFrameBehavior(SquareCatalogInput comp)
        {
            this.comp = comp;
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
                    await comp.Execute();
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
                        comp.SetSquareResponse(comp.GetFileBehavior().BuildDataListFile<ListCatalogResponse>(args[2]));
                        comp.SetCatalogItems(comp.CatalogResponseToCatalogPetsiItems(comp.GetSquareResponses()));
                        comp.SetIsFileExecute(true);
                        await comp.Execute();
                    }
                    else if (args[1] == "o")
                    {
                        comp.SetCatalogItems(comp.GetFileBehavior().BuildDataListFile<CatalogItemPetsi>(args[2]));
                        comp.SetIsFileExecute(true);
                        await comp.Execute();
                    }
                    break;
                case "iserialize":
                    if (!comp.GetHasExecuted()) { Console.WriteLine("Nothing to Serialize, needs to execute first."); break; }
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Invalid iserialize command. \"iserialize <fileName>\"");
                        break;
                    }
                    comp.GetFileBehavior().DataListToFile(args[1], comp.GetSquareResponses());
                    break;
                case "oserialize":
                    if (!comp.GetHasExecuted()) { Console.WriteLine("Nothing to Serialize, needs to execute first."); break; }
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Invalid oserialize command. \"oserialize <fileName>");
                        break;
                    }
                    comp.GetFileBehavior().DataListToFile(args[1], comp.GetCatalogItems());
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
        private void PrintHelp()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("     exectute: pulls input data and loads model");
            Console.WriteLine("     fexectute <fileName>: pulls input data from serialized file and loads model");
            Console.WriteLine("     iserialize <fileName>: saves input object to file");
            Console.WriteLine("     oserialize <fileName>: saves created object from input to file (CatalogItems)");
            Console.WriteLine("     listfp: list saved files in filepath");
            Console.WriteLine("     back: returns to Command Frame");
            Console.WriteLine("     help: lists valid commands");
        }
        public override string GetComponentName()
        {
            return "Square Catalog Input Component";
        }
    }
}
