using Petsi.CommandLine.UnitBuilders;
using Petsi.Interfaces;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.CommandLine
{
    public class CatalogModelFrameBehavior : FrameBehaviorBase
    {
        protected CatalogModelPetsi _cmp;
        List<CatalogItemPetsi> _searchList;
        string searchTerm;
        public CatalogModelFrameBehavior(CatalogModelPetsi cmp)
        {
            _cmp = cmp;
            _searchList = new List<CatalogItemPetsi>();
        }
        public override Task Actions(Stack<ICommandable> contextChain, string actionIdentifier)
        {
            if (actionIdentifier == null) { return Task.CompletedTask; }

            string[] args = actionIdentifier.ToLower().Split(' ');

            switch (args[0])
            {
                case "view":
                    PrintModel(null);
                    break;
                case "add":
                    contextChain.Push(new CatalogItemUnitBuilder(_cmp));
                    break;
                case "select":
                    if (args.Length > 1)
                    {
                        contextChain.Push(_cmp.GetItems()[Int32.Parse(args[1])].GetFrameBehavior());
                        contextChain.Peek().CommandFrameView();
                    }
                    else
                    {
                        Console.WriteLine("invalid select command, length less than 2");
                    }
                    break;
                case "serialize":
                    if (_cmp.GetItems().Count == 0) { Console.WriteLine("Nothing to Serialize, needs to execute first."); break; }
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Invalid serialize command. \"serialize <fileName>\"");
                        break;
                    }
                    _cmp.GetFileBehavior().DataListToFile(args[1], _cmp.GetItems());
                    break;
                case "listfp":
                    _cmp.GetFileBehavior().ListFileDirectory();
                    break;
                case "clear":
                    _cmp.ClearModel();
                    break;
                case "build":
                    if (args.Length <= 2)
                    {
                       _cmp.SetItemList(_cmp.GetFileBehavior().BuildDataListFile<CatalogItemPetsi>(args[1]));
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid build command. \"build <fileName>\"");
                    }
                    break;
                case "search":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Invalid search command. \"search <itemName>\"");
                        break;
                    }
                    else
                    {
                        searchTerm = BuildSearchTerm(args);
                        _searchList = _cmp.SearchByItemName(searchTerm);
                    }
                    if (_searchList.Count == 0)
                    {
                        SystemLogger.Log("Catalog model found no matching result for: " + searchTerm);
                    }
                    if (_searchList.Count == 1)
                    {
                        contextChain.Push(_searchList[0].GetFrameBehavior());
                        contextChain.Peek().CommandFrameView();
                    }
                    else
                    {
                        SystemLogger.Log("Results:");
                        {
                            foreach (CatalogItemPetsi searchResult in _searchList)
                            {
                                PrintModel(searchResult.itemName);
                            }
                        }
                    }
                    break;
                case "lblboot":
                    _cmp.InitialLabelMapBoot();
                    break;
                case "help":
                    PrintHelp();
                    break;
                default:
                    Console.WriteLine("Invalid Catalog Model Command");
                    break;
            }
            return Task.CompletedTask;
        }
        
        private string BuildSearchTerm(string[] args)
        {
            string result = "";
            for (int i = 1; i < args.Length; i++)
            {
                result += args[i];
                if(i != args.Length - 1) { result += " "; }
            }
            return result;
        }
        
        public override void CommandFrameView()
        {
            PrintHelp();
        }
        protected void PrintModel(string? searchItemName)
        {
            int i = 0;
            foreach (CatalogItemPetsi item in _cmp.GetItems())
            {
                if(searchItemName == null || searchItemName.ToLower() == item.itemName.ToLower())
                {
                    Console.WriteLine("[" + i + "]: " + item.itemName + " " + item.catalogObjectId);
                }
                i++;
            }
        }
        private void PrintHelp()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("     view: prints contents of the model");
            Console.WriteLine("     add: Add a Catalog item to the model");
            Console.WriteLine("     select <itemName>: changes context to selected item");
            Console.WriteLine("     clear: clears the model's data");
            Console.WriteLine("     build <fileName>: builds model's data from file");
            Console.WriteLine("     serialize <fileName>: saves model's data to file");
            Console.WriteLine("     listfp: list saved files in filepath");
            Console.WriteLine("     back: returns to Command Frame");
            Console.WriteLine("     search <search term>: searches catalog with given term, if 1 result, shows item, otherwise provides list");
            Console.WriteLine("     lblboot: initialize label file associations and saves to main model");
            Console.WriteLine("     help: lists valid commands");
        }
        public override string GetComponentName()
        {
            return "Square Catalog Model";
        }
    }
}
