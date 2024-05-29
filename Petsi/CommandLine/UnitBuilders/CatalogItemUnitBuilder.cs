using Petsi.Interfaces;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;
using System.Collections;
using System.Collections.Specialized;
using Petsi.Managers;

namespace Petsi.CommandLine.UnitBuilders
{
    public class CatalogItemUnitBuilder : UnitBuilderBase
    {
        CatalogModelPetsi model;
        ICategoryService categoryService;
        ICatalogService catalogService;

        string categoryId;
        string catalogObjectId;
        string itemName;
        List<string> naturalNames;
        ListDictionary variations;

        int step;

        /// <summary>
        /// Given by CatalogServiceErrorFrame when validateModifyName fails and user is adding new item from failure point.
        /// </summary>
        string errorName;
        public CatalogItemUnitBuilder(CatalogModelPetsi inputModel) //ref newItem
        {
            model = inputModel;

            ServiceManagerSingleton sm = ServiceManagerSingleton.GetInstance();
            catalogService = (ICatalogService)sm.GetService(Identifiers.SERVICE_CATALOG);
            categoryService = (ICategoryService)sm.GetService(Identifiers.SERVICE_CATEGORY);
           
            naturalNames = new List<string>();
            variations = new ListDictionary();

            step = 0;   
        }
        public CatalogItemUnitBuilder(CatalogModelPetsi inputModel, string errorName)
        {
            itemName = errorName;

            model = inputModel;

            ServiceManagerSingleton sm = ServiceManagerSingleton.GetInstance();
            catalogService = (ICatalogService)sm.GetService(Identifiers.SERVICE_CATALOG);
            categoryService = (ICategoryService)sm.GetService(Identifiers.SERVICE_CATEGORY);

            naturalNames = new List<string>();
            variations = new ListDictionary();

            step = 1;
        }
        public override Task Actions(Stack<ICommandable> contextChain, string actionIdentifier)
        {
            string input;
            while(step != 5)
            {
                Console.Clear();
                CommandFrameView();
                switch (step)
                {
                    case 0:
                        if(itemName == null)
                        {
                            Console.Write("Item Name: ");
                            itemName = Console.ReadLine();
                        }
                        step++;
                        break;
                    case 1:
                        Console.WriteLine("Choose Category: ");
                        PrintCategories();
                        Console.Write("Enter Index: ");
                        input = Console.ReadLine();
                        int inputNum;
                        if(int.TryParse(input, out inputNum))
                        {
                            categoryId = categoryService.GetCategoryId(Int32.Parse(input));
                            step++;
                        }
                        else
                        {
                            if (input == "back")
                            {
                                step--;
                                break;
                            }
                            Console.WriteLine("Incorrect entry for category selection.");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Select available sizes: reg, 3, 5, 8, 10");
                        Console.WriteLine("Type \"done\" when finished");
                        Console.Write("Choose Sizes: ");
                        input = Console.ReadLine();
                        if (input == "done")
                        {
                            step++;
                            break;
                        }
                        else if (input == "back")
                        {
                            step--;
                            break;
                        }
                        ParseSizes(input, variations);
                        break;
                    case 3:
                        Console.WriteLine("Type \"done\" when finished");
                        Console.Write("Add a natural name: ");
                        input = Console.ReadLine();
                        if (input == "done")
                        {
                            step++;
                            break;
                        }
                        else if (input == "back")
                        {
                            step--;
                            break;
                        }
                        if (!catalogService.NameExists(input))
                        {
                            naturalNames.Add(input);
                        }
                        else
                        {
                            Console.WriteLine("Name is already in use.");
                        }
                        break;
                    case 4:
                        catalogObjectId = catalogService.GenerateCatalogId();
                        model.AddItem(new CatalogItemPetsi(categoryId, catalogObjectId, itemName, variations, naturalNames));
                        contextChain.Pop();
                        step++;
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Opps something wrong in CatalogItem Unit builder switch");
                        break;
                }
            }
            return Task.CompletedTask;
        }

        private void ParseSizes(string input, ListDictionary variations)
        {
            string[] args = input.ToLower().Split(' ');
            for(int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "reg":
                        variations.Add("reg_variation_" + itemName, Identifiers.SIZE_REGULAR);
                        break;
                    case "3":
                        variations.Add("cutie_variation_" + itemName, Identifiers.SIZE_CUTIE);
                        break;
                    case "5":
                        variations.Add("small_variation_" + itemName, Identifiers.SIZE_SMALL);
                        break;
                    case "8":
                        variations.Add("med_Variation_" + itemName, Identifiers.SIZE_MEDIUM);
                        break;
                    case "10":
                        variations.Add("lrg_variation_" + itemName, Identifiers.SIZE_LARGE);
                        break;
                    default:
                        Console.WriteLine("invalid SIZE given");
                        break;
                }
            }
        }

        public override void CommandFrameView()
        {
            Console.WriteLine("Item Name: " + itemName);
            Console.WriteLine("category Id: " + categoryId);
            Console.WriteLine("variations: ");
            PrintVariations();
            Console.WriteLine("natural names: ");
            PrintNaturalNames();
            Console.WriteLine("");
        }

        private void PrintVariations()
        {
            foreach(DictionaryEntry entry in variations)
            {
                Console.WriteLine("   " + entry.Key + " " + entry.Value);
            }
        }

        private void PrintNaturalNames()
        {
            foreach(string name in naturalNames) { Console.WriteLine("   " + name); }
        }

        public override string GetComponentName()
        {
            return "Catalog Item Unit Builder";
        }

        public void PrintCategories()
        {
           List<string> categoryNames = categoryService.GetCategoryNames();
           int i  = 0;
           foreach(string categoryName in categoryNames)
           {
                Console.WriteLine("["+i+"]: " + categoryName);
                i++;
           }
        }
    }
}
