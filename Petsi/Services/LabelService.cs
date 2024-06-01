using Petsi.CommandLine;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;
using System.Diagnostics;

namespace Petsi.Services
{
    public class LabelService : ServiceBase
    {
        string cutieDirectoryPath;
        string pieDirectoryPath;
        /// <summary>
        /// Key: CatalogObjectId, Value:pdfFilepath
        /// </summary>
        Dictionary<string, string> _standardLabelMap;
        /// <summary>
        /// Key: CatalogObjectId, Value:pdfFilepath
        /// </summary>
        Dictionary<string, string> _cutieLabelMap;

        //FileBehavior _fileBehavior;
        LabelServiceFrameBehavior frameBehavior;
        public LabelService()
        {
            frameBehavior = new LabelServiceFrameBehavior(this);
            _standardLabelMap = new Dictionary<string, string>();
            _cutieLabelMap = new Dictionary<string, string>();
            cutieDirectoryPath = PetsiConfig.GetInstance().GetFilepath("cutieDirectory");
            pieDirectoryPath = PetsiConfig.GetInstance().GetFilepath("standardDirectory");
            SetServiceName(Identifiers.SERVICE_LABEL);
            ServiceManagerSingleton.GetInstance().Register(this);
            CommandFrame.GetInstance().RegisterFrame("lbl", frameBehavior);
            CatalogModelPetsi cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            cmp.AddModelService(this);
        }
        public FrameBehaviorBase GetFrameBehavior() { return frameBehavior; }
        public void LoadLabelMap(List<CatalogItemPetsi> inputList)
        {
            //CLEAR OR TRYADD
            foreach(CatalogItemPetsi item in inputList)
            {
                if(item.StandardLabelFilePath != null)
                {
                    _standardLabelMap[item.CatalogObjectId] = item.StandardLabelFilePath;
                }
                if(item.CutieLabelFilePath != null)
                {
                    _cutieLabelMap[item.CatalogObjectId] = item.CutieLabelFilePath;
                }
            }
        }
        public void Print_4x2(DateTime targetDate)
        {
            OrderModelPetsi omp = GetOrderModel();

            List<LabelPrintData> printList = LoadPrintList(omp.GetWsDayData(targetDate));
            PrintStandard(printList);
        }
        public void Print_2x1(DateTime targetDate)
        {
            OrderModelPetsi omp = GetOrderModel();

            List<LabelPrintData> printList = LoadPrintList(omp.GetWsDayData(targetDate));
            PrintCare(printList);
            PrintCutie(printList);
        }
        public void Print_Round(DateTime targetDate)
        {
            OrderModelPetsi omp = GetOrderModel();

            List<LabelPrintData> printList = LoadPrintList(omp.GetWsDayData(targetDate));
            PrintRound(printList);
        }
        private void PrintStandard(List<LabelPrintData> inputList)
        {
            /*
            foreach(LabelPrintData printItem in inputList)
            {
                ExecuteRolloPrint(pieDirectoryPath + _standardLabelMap[printItem.Id], printItem.GetStandardAmount() );
            }*/
            ExecuteRolloPrint("D:/Git-Repos/Petsi/Petsi/Labels/Files/Pie/Apple-Crumb_pie_ingredient_labels.jpg", 1);
        }
        private void PrintCutie(List<LabelPrintData> inputList)
        {
            foreach (LabelPrintData printItem in inputList)
            {
                ExecuteRolloPrint(cutieDirectoryPath + _cutieLabelMap[printItem.Id], printItem.GetCutieAmount() );
            }
        }
        private void PrintCare(List<LabelPrintData> inputList)
        {
            int count = 0;
            foreach (LabelPrintData printItem in inputList)
            {
                count += printItem.GetStandardAmount();
               
            }
            ExecuteRolloPrint(pieDirectoryPath + _standardLabelMap["care"] , count );
        }
        private void PrintRound(List<LabelPrintData> inputList)
        {
            int count = 0;
            foreach (LabelPrintData printItem in inputList)
            {
                count += printItem.GetCutieAmount();

            }
            ExecuteRolloPrint(pieDirectoryPath + _standardLabelMap["round"] , count );
        }
        private List<LabelPrintData> LoadPrintList(List<PetsiOrderLineItem> inputList)
        {
            List<LabelPrintData> printList = new List<LabelPrintData>();

            foreach (PetsiOrderLineItem item in inputList)
            {
                printList.Add(new LabelPrintData(item.CatalogObjectId, item.Amount3, item.Amount5, item.Amount8));
            }
            return printList;
        }
        private void ExecuteRolloPrint(string PdfFp, int copies)
        {
            string nodePath = @"C:\Program Files\nodejs\node.exe";

            string scriptPath = @"D:\Git-Repos\Petsi\Petsi\Services\RolloPrinter.js";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = nodePath,
                Arguments = $"{scriptPath} \"{PdfFp}\" {copies}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                Console.WriteLine("Output: " + output);
                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("Error: " + error);
                }
            }
        }   
        public override void Update(ModelBase model)
        {
            var catalog = (CatalogModelPetsi)model;
            LoadLabelMap(catalog.GetItems());
        }

        private OrderModelPetsi GetOrderModel()
        {
            return (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
        }
    }
    public class LabelPrintData
    {
        public string Id { get; }
        public int Amount3 { get; }
        public int Amount5 { get; }
        public int Amount8 { get; }

        public LabelPrintData(string id, int amount3, int amount5, int amount8)
        {
            Id = id;
            Amount3 = amount3;
            Amount5 = amount5;
            Amount8 = amount8;
        }
        public int GetStandardAmount() { return Amount5 + Amount8; }
        public int GetCutieAmount() {  return Amount3; }
    }
}
