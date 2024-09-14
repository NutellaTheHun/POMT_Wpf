using Petsi.Events;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;
using System.Drawing.Printing;

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

        public LabelService()
        {
            _standardLabelMap = new Dictionary<string, string>();
            _cutieLabelMap = new Dictionary<string, string>();
            cutieDirectoryPath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_CUTIE_LBL_PATH);
            pieDirectoryPath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_PIE_LBL_PATH);
            SetServiceName(Identifiers.SERVICE_LABEL);
            ServiceManagerSingleton.GetInstance().Register(this);
            //CatalogModelPetsi cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            CatalogModelPetsi cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetCatalogModel();
            cmp.AddModelService(this);
            LoadLabelMap(cmp.GetItems());
        }
        
        /// <summary>
        /// Recieves the Catalaog and maps any item that contains a standard or cutie label file
        /// </summary>
        /// <param name="inputList"></param>
        public void LoadLabelMap(List<CatalogItemPetsi> inputList)
        {
            //CLEAR OR TRYADD
            _standardLabelMap["round"] = "Round-Allergen-Label-01.png";
            _standardLabelMap["care"] = "pie-care-directory-label-v2-03.jpg";
            foreach (CatalogItemPetsi item in inputList)
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
            SystemLogger.LogStatus("Label Service Print4x2 start");
            OrderModelPetsi omp = GetOrderModel();

            List<LabelPrintData> printList = LoadPrintList(omp.GetWsDayData(targetDate));
            PrintStandard(printList);
            SystemLogger.LogStatus("Label Service Print4x2 complete");
        }
        public void Print_2x1(DateTime targetDate)
        {
            SystemLogger.LogStatus("Label Service Print2x1 start");
            OrderModelPetsi omp = GetOrderModel();

            List<LabelPrintData> printList = LoadPrintList(omp.GetWsDayData(targetDate));
            PrintCare(printList);
            PrintCutie(printList);
            SystemLogger.LogStatus("Label Service Print2x1 complete");
        }
        
        public void Print_Round(DateTime targetDate)
        {
            SystemLogger.LogStatus("Label Service PrintRound start");
            OrderModelPetsi omp = GetOrderModel();

            List<LabelPrintData> printList = LoadPrintList(omp.GetWsDayData(targetDate));
            PrintRound(printList);
            SystemLogger.LogStatus("Label Service PrintRound complete");
        }

        private void PrintStandard(List<LabelPrintData> inputList)
        {
            if(!ValidateStandardLabelMap(inputList)){ return; }

            PrintDocument pd;
            foreach (LabelPrintData printItem in inputList)
            {
                pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_LABEL_PRINTER);
                pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 400, 200); //hundreths of an inch
                pd.PrinterSettings.Copies = (short)printItem.GetStandardAmount();
                if (pd.PrinterSettings.Copies == 0) { continue; }
                pd.PrintPage += (sender, args) =>
                {
                    Image img = Image.FromFile(pieDirectoryPath + "\\" + _standardLabelMap[printItem.Id]);
                    Point loc = new Point(0, 0);
                    args.Graphics.DrawImage(img, loc);
                };
                int a = 1;
                try { pd.Print(); }
                catch (InvalidPrinterException e)
                {
                    ErrorService.RaisePrinterNotFoundEvent();
                    return;
                }
            }
        }

        /// <summary>
        /// Before print cutie labels, verify every item in inputlist has a label mapping, and that all filepaths are valid.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="labelMap"></param>
        /// <returns></returns>
        private bool ValidateCutieLabelMap(List<LabelPrintData> inputList)
        { 
            string labelsFilepath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_CUTIE_LBL_PATH);
            if (labelsFilepath == null || labelsFilepath == "") { return false; }


            string test;
            foreach (LabelPrintData printItem in inputList)
            {

                if(printItem.GetCutieAmount() == 0) { continue;}

                try { test = _cutieLabelMap[printItem.Id]; }
                catch (KeyNotFoundException e)
                {
                    LabelServiceInputLabelNotFoundArgs args = new LabelServiceInputLabelNotFoundArgs(printItem.Id);
                    ErrorService.RaiseInputLabelNotFound(args);
                    return false;
                }
                if(!File.Exists(cutieDirectoryPath + "\\" + _cutieLabelMap[printItem.Id]))
                {

                    ErrorService.RaiseExceptionHandlerError("Label file path invalid: " + cutieDirectoryPath + "\\" + _cutieLabelMap[printItem.Id], "LabelService, ValidateCutieMap");
                    LabelServiceInputLabelNotFoundArgs args = new LabelServiceInputLabelNotFoundArgs(printItem.Id);
                    ErrorService.RaiseInputLabelNotFound(args);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Before print standard labels, verify every item in inputlist has a label mapping, and that all filepaths are valid.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="labelMap"></param>
        /// <returns></returns>
        private bool ValidateStandardLabelMap(List<LabelPrintData> inputList)
        {
            string labelsFilepath = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_PIE_LBL_PATH);
            if (labelsFilepath == null || labelsFilepath == "") { return false; }

            string test;
            foreach (LabelPrintData printItem in inputList)
            {

                if(printItem.GetStandardAmount() == 0) { continue; }

                try { test = _standardLabelMap[printItem.Id]; }
                catch (KeyNotFoundException e)
                {
                    LabelServiceInputLabelNotFoundArgs args = new LabelServiceInputLabelNotFoundArgs(printItem.Id);
                    ErrorService.RaiseInputLabelNotFound(args);
                    return false;
                }
                if (!File.Exists(pieDirectoryPath + "\\" + _standardLabelMap[printItem.Id]))
                {

                    ErrorService.RaiseExceptionHandlerError("Label file path invalid: " + pieDirectoryPath + "\\" + _standardLabelMap[printItem.Id], "LabelService, ValidateStandardLabelMap");
                    LabelServiceInputLabelNotFoundArgs args = new LabelServiceInputLabelNotFoundArgs(printItem.Id);
                    ErrorService.RaiseInputLabelNotFound(args);
                    return false;
                }
            }
            return true;
        }

        private void PrintCutie(List<LabelPrintData> inputList)
        {
            //Validate label filepaths
            if (!ValidateCutieLabelMap(inputList)) { return; }

            PrintDocument pd;
            foreach (LabelPrintData printItem in inputList)
            {
                pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_LABEL_PRINTER);
                pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 200, 100); //hundreths of an inch
                pd.PrinterSettings.Copies = (short)printItem.GetCutieAmount();
                if (pd.PrinterSettings.Copies == 0) { continue; }
                pd.PrintPage += (sender, args) =>
                {
                    Image img = Image.FromFile(cutieDirectoryPath + "\\" + _cutieLabelMap[printItem.Id]);
                    Point loc = new Point(0, 0);
                    args.Graphics.DrawImage(img, loc);
                };

                try { pd.Print(); }
                catch (InvalidPrinterException e) 
                {
                    ErrorService.RaisePrinterNotFoundEvent();
                    return;
                }
            }
        }
        private void PrintCare(List<LabelPrintData> inputList)
        {
            //Validate Filepath
            if (!File.Exists(pieDirectoryPath + "\\" + _standardLabelMap["care"]))
            {
                ErrorService.RaiseExceptionHandlerError("Label file path invalid: " + pieDirectoryPath + "\\" + _standardLabelMap["care"], "LabelService, PrintCare");
                return;
            }

            int count = 0;
            foreach (LabelPrintData printItem in inputList)
            {
                count += printItem.GetStandardAmount();
            }

            if(count == 0) { return; }

            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_LABEL_PRINTER);
            pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 200, 100); //hundreths of an inch
            pd.PrinterSettings.Copies = (short)count;
            //pd.PrinterSettings.Copies = 1;
            pd.PrintPage += (sender, args) =>
            {
                Image img = Image.FromFile(pieDirectoryPath + "\\" + _standardLabelMap["care"]);
                Point loc = new Point(0, 0);
                args.Graphics.DrawImage(img, loc);
            };
            try { pd.Print(); }
            catch (InvalidPrinterException e)
            {
                ErrorService.RaisePrinterNotFoundEvent();
                return;
            }
        }
        private void PrintRound(List<LabelPrintData> inputList)
        {
            //Validate Filepath
            if (!File.Exists(pieDirectoryPath + "\\" + _standardLabelMap["round"]))
            {
                ErrorService.RaiseExceptionHandlerError("Label file path invalid: " + pieDirectoryPath + "\\" + _standardLabelMap["round"], "LabelService, PrintRound");
                return;
            }

            int count = 0;
            foreach (LabelPrintData printItem in inputList)
            {
                count += printItem.GetCutieAmount();
            }
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_LABEL_PRINTER);
            pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 200, 200); //hundreths of an inch
            pd.PrinterSettings.Copies = (short)count;
            //pd.PrinterSettings.Copies = 1;
            pd.PrintPage += (sender, args) =>
            {
                Image img = Image.FromFile(pieDirectoryPath + "\\" + _standardLabelMap["round"]);
                Point loc = new Point(0, 0);
                args.Graphics.DrawImage(img, loc);
            };

            try { pd.Print(); }
            catch (InvalidPrinterException e)
            {
                ErrorService.RaisePrinterNotFoundEvent();
                return;
            }
        }

        //--------------
        public void ValidateFilePaths()
        {
            if (cutieDirectoryPath == null || cutieDirectoryPath == "") { ErrorService.RaiseLabelFilePathNotSet(); return; }
            if (pieDirectoryPath == null || pieDirectoryPath == "") { ErrorService.RaiseLabelFilePathNotSet(); return; }

            CatalogService cmp = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);

            if (!File.Exists(pieDirectoryPath + "\\" + "Round-Allergen-Label-01.png"))
            {
                ErrorService.Instance().RaiseLabelServiceValidateFilePathEvent("round label", "Round-Allergen-Label-01.png", "Pie");
            }
            if(!File.Exists(pieDirectoryPath + "\\" + "pie-care-directory-label-v2-03.jpg"))
            {
                ErrorService.Instance().RaiseLabelServiceValidateFilePathEvent("care sticker", "pie-care-directory-label-v2-03.jpg", "Pie");
            }

            //key: catalog id, val: fileName
            foreach (KeyValuePair<string, string> entry in _standardLabelMap)
            {
                if(entry.Key == "round") { continue; }
                if(entry.Key == "care") { continue; }
                if (!File.Exists(pieDirectoryPath + "\\" + entry.Value)) 
                {
                    CatalogItemPetsi item = cmp.GetCatalogItemById(entry.Key);
                    ErrorService.Instance().RaiseLabelServiceValidateFilePathEvent(item.ItemName, entry.Value, "Pie");
                }
            }
            foreach (KeyValuePair<string, string> entry in _cutieLabelMap)
            {
                if (!File.Exists(cutieDirectoryPath + "\\" + entry.Value))
                {
                    CatalogItemPetsi item = cmp.GetCatalogItemById(entry.Key);
                    ErrorService.Instance().RaiseLabelServiceValidateFilePathEvent(item.ItemName, entry.Value, "Cutie");
                }
            }
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

        public override void Update(ModelBase model)
        {
            var catalog = (CatalogModelPetsi)model;
            LoadLabelMap(catalog.GetItems());
        }

        private OrderModelPetsi GetOrderModel()
        {
            //return (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            return (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetOrderModel();
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
