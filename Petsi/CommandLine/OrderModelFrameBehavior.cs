using Petsi.CommandLine.UnitBuilders;
using Petsi.Interfaces;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.CommandLine
{
    public class OrderModelFrameBehavior : FrameBehaviorBase
    {
        OrderModelPetsi _omp;
        List<PetsiOrder> _searchList;
        string _searchTerm;
        public OrderModelFrameBehavior(OrderModelPetsi omp)
        {
            _omp = omp;
            _searchList = new List<PetsiOrder>();
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
                    contextChain.Push(new PetsiOrderUnitBuilder(_omp));
                    break;
                case "select":
                    if (args.Length > 1)
                    {
                        contextChain.Push(_omp.GetOrders()[Int32.Parse(args[1])].GetFrameBehavior());
                        contextChain.Peek().CommandFrameView();
                    }
                    else
                    {
                        Console.WriteLine("invalid select command, length less than 2");
                    }
                    break;
                case "serialize":
                    if (_omp.GetOrders().Count == 0) { Console.WriteLine("Nothing to Serialize, needs to execute first."); break; }
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Invalid serialize command. \"serialize <fileName>\"");
                        break;
                    }
                    _omp.GetFileBehavior().DataListToFile(args[1], _omp.GetOrders());
                    break;
                case "listfp":
                    _omp.GetFileBehavior().ListFileDirectory();
                    break;
                case "clear":
                    _omp.ClearModel();
                    break;
                case "search":
                    if(args.Length < 2)
                    {
                        Console.WriteLine("Invalid search command. \"search <recipient name>\"");
                        break;
                    }
                    else
                    {
                        _searchTerm = BuildSearchTerm(args);
                        _searchList = _omp.SearchByRecipient(_searchTerm);
                    }
                    if(_searchList.Count == 0)
                    {
                        SystemLogger.Log("Catalog model found no matching result for: " + _searchTerm);
                    }
                    if(_searchList.Count == 1)
                    {
                        contextChain.Push(_searchList[0].GetFrameBehavior());
                        contextChain.Peek().CommandFrameView();
                    }
                    else
                    {
                        SystemLogger.Log("Results:");
                        {
                            foreach(PetsiOrder searchResult in _searchList)
                            {
                                PrintModel(searchResult.Recipient);
                            }
                        }
                    }
                    break;
                case "build":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Invalid build command. \"build <fileName>");     
                    }
                    else
                    {
                        _omp.SetOrders(_omp.GetFileBehavior().BuildDataListFile<PetsiOrder>(args[1]));
                    }
                    break;
                case "merge":
                    if(args.Length < 4)
                    {
                        Console.WriteLine("Invalid merge command. \" merge <file_1> <file_2> <newFile>");
                    }
                    else
                    {
                        _omp.GetFileBehavior().DataListToFile(args[3],
                                               OrderModelPetsi.MergeOrders(
                                                    _omp.GetFileBehavior().BuildDataListFile<PetsiOrder>(args[1]),
                                                    _omp.GetFileBehavior().BuildDataListFile<PetsiOrder>(args[2])));
                    }
                    break;
                case "help":
                    PrintHelp();
                    break;
                default:
                    Console.WriteLine("Invalid Order Model Command");
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
                if (i != args.Length - 1) { result += " "; }
            }
            return result;
        }
        public override void CommandFrameView()
        {
            PrintHelp();
        }
        private void PrintModel(string? searchRecipient)
        {
            int i = 0;
            foreach (PetsiOrder order in _omp.GetOrders())
            {
                if(searchRecipient == null || order.Recipient.ToLower() == searchRecipient.ToLower())
                {
                    Console.WriteLine("[" + i + "]: " + order.Recipient + " " +
                    DateTime.Parse(order.OrderDueDate).ToShortDateString() + " " +
                    order.FulfillmentType
                    );
                }
                i++;
            }
        }
        private void PrintHelp()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("     view: lists contents of model");
            Console.WriteLine("     add: Add a PetsiOrder to the model");
            Console.WriteLine("     select <item>: changes context to selected item");
            Console.WriteLine("     clear: clears the model's data");
            Console.WriteLine("     build <fileName>: builds model's data from given file name");
            Console.WriteLine("     serialize <fileName>: saves model's data to file");
            Console.WriteLine("     listfp: list saved files in filepath");
            Console.WriteLine("     merge <file_1> <file_2> <newFile>: merges two model's data into new file");
            Console.WriteLine("     search <search term>: searches orders with given term, if 1 result, shows item, otherwise provides list");
            Console.WriteLine("     back: returns to Command Frame");
        }
        public override string GetComponentName()
        {
            return "Order Model";
        }
    }
}
