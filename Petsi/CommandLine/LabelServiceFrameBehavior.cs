using Petsi.Interfaces;
using Petsi.Services;
using Petsi.Utils;

namespace Petsi.CommandLine
{
    public class LabelServiceFrameBehavior : FrameBehaviorBase
    {
        LabelService _labelService;
        DateTime date;
        public LabelServiceFrameBehavior(LabelService labelService)
        {
            _labelService = labelService;
        }
        public override Task Actions(Stack<ICommandable> contextChain, string actionIdentifier)
        {
            string[] args = actionIdentifier.Split(' ');
            switch(args[0])
            {
                case "4x2":
                    if(args.Length < 2) { Console.WriteLine("Invalid input length, 4x2 [date]"); }
                    if (DateTime.TryParse(args[1], out date))
                    {
                        _labelService.Print_4x2(date);
                    }
                    else
                    {
                        Console.WriteLine("Invalid DateParse: " + args[1]);
                    }

                    break;
                case "2x1":
                    if (args.Length < 2) { Console.WriteLine("Invalid input length, 4x2 [date]"); }
                    if (DateTime.TryParse(args[1], out date))
                    {
                        _labelService.Print_2x1(date);
                    }
                    else
                    {
                        Console.WriteLine("Invalid DateParse: " + args[1]);
                    }
                    break;
                case "round":
                    if (args.Length < 2) { Console.WriteLine("Invalid input length, 4x2 [date]"); }
                    if (DateTime.TryParse(args[1], out date))
                    {
                        _labelService.Print_Round(date);
                    }
                    else
                    {
                        Console.WriteLine("Invalid DateParse: " + args[1]);
                    }
                    break;
                case "init":
                    contextChain.Push(new LabelServiceCatalogMapFrameBehavior(_labelService));
                    break;
                case "listfp":
                    PrintPieLabels();
                    PrintCutieLabels();
                    break;
                case "default":
                    Console.WriteLine("Invalid Input");
                    break;
            }
            return Task.CompletedTask;
        }

        private void PrintCutieLabels()
        {
            foreach(string name in GetFileDirectoryList("Cuties"))
            {
                Console.WriteLine(name);
            }
        }

        private void PrintPieLabels()
        {
            foreach (string name in GetFileDirectoryList("Pie"))
            {
                Console.WriteLine(name);
            }
        }

        private List<string> GetFileDirectoryList(string directoryName)
        {
            string path = PetsiConfig.GetInstance().GetVariable("labelDirectory");
            if (!Directory.Exists(path + directoryName))
            {
                Directory.CreateDirectory(path + directoryName);
            }
            return Directory.GetFiles(path + directoryName).ToList();
        }

        public override void CommandFrameView()
        {
            Console.WriteLine("4x2: Print 4x2 Labels");
            Console.WriteLine("2x1: Print 2x1 Labels");
            Console.WriteLine("round: Print Round Labels");
            Console.WriteLine("init: Map Catalog Items to Labels");
        }

        public override string GetComponentName()
        {
            return "Label Service";
        }
    }
}
