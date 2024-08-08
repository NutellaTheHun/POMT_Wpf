using Petsi.Interfaces;
using Petsi.Units;
using System.Collections;

namespace Petsi.CommandLine
{
    public class CatalogItemFrameBehavior : FrameBehaviorBase, IFrameUnitBuild
    {
        CatalogItemPetsi item;

        public CatalogItemFrameBehavior(CatalogItemPetsi cip)
        {
            item = cip;
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
                "Item Name: " + item.ItemName +
                "\nCatalog Object Id: " + item.CatalogObjectId +
                "\ncategory Id: " + item.CategoryId +
                "\nnatural names: ");
            PrintNaturalNames();
            Console.WriteLine("variations: ");
            PrintVariations();
            Console.WriteLine("\ntype \"help\" for commands.");
           if(item.StandardLabelFilePath != null) { Console.WriteLine("standard label: " + item.StandardLabelFilePath); }
           if (item.CutieLabelFilePath != null) { Console.WriteLine("cutie label: " + item.CutieLabelFilePath); }
        }
        public override string GetComponentName()
        {
            return "Catalog Item";
        }
        public void PrintNaturalNames()
        {
            foreach(string name in item.NaturalNames)
            {
                Console.WriteLine("   " + name);
            }
        }
        public void PrintVariations()
        {
            foreach (DictionaryEntry de in item.Variations)
            {
                Console.WriteLine("    key,value: " + de.Key + ", " + de.Value);
            }
        }
    }
}
