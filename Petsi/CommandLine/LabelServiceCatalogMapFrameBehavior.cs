using Petsi.Interfaces;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.CommandLine
{
    public class LabelServiceCatalogMapFrameBehavior : FrameBehaviorBase
    {
        LabelService _labelService;
        List<CatalogItemPetsi> itemList;
        List<string> cutieFileNames;
        List<string> standardFileNames;
        public LabelServiceCatalogMapFrameBehavior(LabelService ls)
        {
            _labelService = ls;
            var catalog = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            itemList = catalog.GetItems();
            //cutieFileNames = GetFileDirectoryList("Cuties");
            //standardFileNames = GetFileDirectoryList("Pie");
        }
        //List of cutie directory
        //List of pie directory

        //foreach Pie in catalog
        //   --Select Pie, or skip
        //   --Select cutie, or skip

        //map care
        //map round
        public override Task Actions(Stack<ICommandable> contextChain, string actionIdentifier)
        {
            string input;
            string indexInput;
            int index;
            string[] args;
            foreach(var item in itemList)
            {
                if(item.CategoryId == Identifiers.CATEGORY_PIE)
                {
                    Console.WriteLine(item.ItemName + ": ");
                    Console.WriteLine("n: next, p: pie, c: cutie");
                    input = Console.ReadLine();
                    args = input.ToLower().Split(' ');
                    while (args[0] != "n")
                    {
                        switch (args[0])
                        {
                            case "p":
                                PrintLabelList(standardFileNames);
                                indexInput = Console.ReadLine();
                                if (int.TryParse(indexInput, out index))
                                {
                                    if (index < standardFileNames.Count)
                                    {
                                        item.StandardLabelFilePath = standardFileNames[index];
                                    }
                                }
                                break;
                            case "c":
                                PrintLabelList(cutieFileNames);
                                indexInput = Console.ReadLine();
                                if (int.TryParse(indexInput, out index))
                                {
                                    if (index < standardFileNames.Count)
                                    {
                                        item.CutieLabelFilePath = standardFileNames[index];
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
               
            }
            return Task.CompletedTask;
        }

        public override void CommandFrameView()
        {
            throw new NotImplementedException();
        }

        public override string GetComponentName()
        {
            return "Label Service Catalog Mapping";
        }
        /*
        private List<string> GetFileDirectoryList(string directoryName)
        {
            
            string path = PetsiConfig.GetInstance().GetFilepath("labelDirectory");
            if (!Directory.Exists(path + directoryName))
            {
                Directory.CreateDirectory(path + directoryName);
            }
            return Directory.GetFiles(path + directoryName).ToList();
        }
        */
        private void PrintLabelList(List<string> inputList)
        {
            int i = 0;
            foreach(var item in inputList)
            {
                Console.WriteLine("["+i+"] " + item);
                i++;
            }
        }
    }
}
