using Petsi.CommandLine;
using Petsi.Filing;
using Petsi.Managers;
using Petsi.Units;
using Petsi.Util;
using Petsi.Utils;

namespace Petsi.Input
{
    public class WholesaleInput : ModelInputBase
    {
        List<WholesaleItem> items;
        CSVHandler csvh;
        public static int LoggerWholesaleInputCount;
        public static int LoggerWholesaleCSVLinesProcessedCount;
        WholesaleInputFrameBehavior frameBehavior;
        FileBehavior fileBehavior;
        bool isFileExecute;
        private bool hasExecuted;

        public WholesaleInput()
        {
            items = new List<WholesaleItem>();
            csvh = new CSVHandler(PetsiConfig.GetInstance().GetFilepath("onOrderPath"));
            LoggerWholesaleInputCount = 0;
            LoggerWholesaleCSVLinesProcessedCount = 0;
            frameBehavior = new WholesaleInputFrameBehavior(this);
            fileBehavior = new FileBehavior("WholesaleInput");
            isFileExecute = false;
            hasExecuted = false;

            SetModel(ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS));
            SetInputName(Identifiers.WHOLESALE_INPUT);
            InputManagerSingleton.GetInstance().Register(this);
            EnvironCaptureRegistrySingleton.GetInstance().Register(this);
            CommandFrame.GetInstance().RegisterFrame("wsi", frameBehavior);
        }
        public override async Task Execute()
        {
            if(!isFileExecute)
            {
                items = csvh.LoadWholesaleData();
                LoggerWholesaleCSVLinesProcessedCount = CSVHandler.wholesaleLinesProcessed;
            }
            
            foreach (PetsiOrder item in WholesaleItemsToPetsiOrders())
            {
                Model.AddData(item);
            }
            hasExecuted = true;
        }
        public List<PetsiOrder> WholesaleItemsToPetsiOrders()
        {
            Dictionary<string, PetsiOrder> dict = new Dictionary<string, PetsiOrder>();
            string onOrderId;
            foreach (WholesaleItem item in items)
            {
                onOrderId = item.WholesaleName + item.Day;
                if (dict.ContainsKey(onOrderId))
                {
                    dict[onOrderId].LineItems.Add(item.ToOnOrderLineItem());
                }
                else
                {
                    PetsiOrder ooi = item.ToOnOrderItemHeader();
                    ooi.LineItems.Add(item.ToOnOrderLineItem());
                    dict.Add(onOrderId, ooi);
                }
            }
            return dict.Values.ToList();
        }
        public override FrameBehaviorBase GetFrameBehavior(){ return frameBehavior;}
        public FileBehavior GetFileBehavior(){ return fileBehavior;}
        public List<WholesaleItem> GetItems(){ return items;}
        public void SetItems(List<WholesaleItem> list){ items = list;}
        public void SetIsFileExecute(bool v){ isFileExecute = v;}
        public bool GetHasExecuted() { return hasExecuted; }
        public void SetHasExecuted(bool v) { hasExecuted = v; }
        public override void CaptureEnvironment(FileBehavior reportFb){reportFb.DataListToFile(Identifiers.ENV_WSI, items); }
    }
}
