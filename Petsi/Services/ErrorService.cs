using Petsi.Events;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Services
{
    public class ErrorService
    {

        #region singleton/Observer

        public static ErrorService instance;
        private ErrorService()
        {
           
        }

        public static ErrorService Instance()
        {
            if (instance == null) { instance = new ErrorService(); }
            return instance;
        }

        #endregion

        #region Report Events

        //Table Builder Overflow Event
        public delegate void TableBuilderOverflowEvent(object sender, EventArgs e);

        public event TableBuilderOverflowEvent TBOverflow;
        public void RaiseTBOverflowEvent(List<PetsiOrderLineItem> overflowList)
        {
            SystemLogger.LogWarning($"TableBuilder overflow event");
            TBOverflowEventArgs args = new TBOverflowEventArgs(overflowList);
            TBOverflow?.Invoke(this, args);
        }

        public event EventHandler ReportPrintEmptyInput;
        public static void RaiseReportEmptyInput()
        {
            SystemLogger.LogStatus($"Given list to report is empty");
            Instance().ReportPrintEmptyInput?.Invoke(Instance(), EventArgs.Empty);
        }

        #endregion

        #region SquareOrderInput Events

        List<EventArgs> mainWindowEvents = new List<EventArgs>()
            ;
        //Square Order Input New Item Event
        public delegate void SquareOrderInputNewItemEvent(object sender, EventArgs e);

        public event SquareOrderInputNewItemEvent SoiNewItem;

        public void RaiseSoiNewItemEvent(CatalogItemPetsi newItem)
        {
            SystemLogger.LogStatus($"(from errorService) New catalog item event {newItem.ItemName}");
            SoiNewItemEventArgs args = new SoiNewItemEventArgs(newItem);
            mainWindowEvents.Add(args);
        }

        //Square Order Input Multi Item Event
        public delegate void SquareOrderInputMultiItemEvent(object sender, EventArgs e);

        public event SquareOrderInputMultiItemEvent SoiMultiItem;

        //To prevent duplicate windows being created for each time the same item returns multiple matches
        List<string> multiItemNameEventCalls = new List<string>();
        public void RaiseSoiMultiItemEvent(string itemContext, List<CatalogItemPetsi> multiItemList)
        {
            //If event hasn't been raised for the given item name
            if(!Instance().multiItemNameEventCalls.Contains(itemContext))
            {
                SystemLogger.LogStatus($"New multimatch item event {itemContext}");
                multiItemNameEventCalls.Add(itemContext);
                SoiMultiItemEventArgs args = new SoiMultiItemEventArgs(itemContext, multiItemList);
                mainWindowEvents.Add(args);
            }          
        }

        #endregion

        #region Label Events

        List<EventArgs> labelViewEvents = new List<EventArgs>();

        public delegate void LabelServiceValidateFilePathEvent(object sender, EventArgs e);
        public event LabelServiceValidateFilePathEvent LabelServiceValidateFilePath;
        public void RaiseLabelServiceValidateFilePathEvent(string catalogId, string fileName, string pieType)
        {
            SystemLogger.LogWarning($"LabelSerivce filepath failed to be validated");
            LabelServiceValidateFpEventArgs args = new LabelServiceValidateFpEventArgs(catalogId, fileName, pieType);
            labelViewEvents.Add(args);
        }

        public event EventHandler LabelPrinterNotFoundEvent;
        public static void RaisePrinterNotFoundEvent()
        {
            SystemLogger.LogWarning($"Labelprinter not found.");
            Instance().LabelPrinterNotFoundEvent?.Invoke(Instance(), EventArgs.Empty);
        }

        public event EventHandler InputLabelNotFoundEvent;
        public static void RaiseInputLabelNotFound(LabelServiceInputLabelNotFoundArgs args)
        {
            SystemLogger.LogWarning($"label not found {args.ItemId}");
            Instance().InputLabelNotFoundEvent?.Invoke(Instance(), args);
        }

        public event EventHandler LabelFilePathNotSetEvent;
        public static void RaiseLabelFilePathNotSet()
        {
            SystemLogger.LogWarning($"LabelSerivce filepath not set");
            Instance().LabelFilePathNotSetEvent?.Invoke(Instance(), EventArgs.Empty);
        }

        #endregion

        #region Startup Events

        public delegate void SquareMissingKeyEvent(object sender, EventArgs e);
        public event SquareMissingKeyEvent NewStartupEvent;
        public void RaiseNewStartupEvent()
        {
            SquareMissingKeyEventArgs args = new SquareMissingKeyEventArgs();
            mainWindowEvents.Add(args);
        }

        #endregion

        public delegate void ExceptionHandlerEvent(object sender, string errorMessage);
        public ExceptionHandlerEvent ExceptionHandlerErrorEvent;

        public static void RaiseExceptionHandlerError(string errorMessage, string sender)
        {
            Instance().ExceptionHandlerErrorEvent?.Invoke(Instance(), errorMessage);
            SystemLogger.LogError(errorMessage, sender);
        }

        public static void RaiseSoftExceptionHandlerError(string errorMessage)
        {
            Instance().ExceptionHandlerErrorEvent?.Invoke(Instance(), errorMessage);
 
        }

        /// <summary>
        /// Events that occur before the LabelView is initialized are added to the mainWindowEvents list
        /// and is called in the mainWindow view constructor.
        /// </summary>
        public static void RaiseLabelEvents()
        {
            List<EventArgs> argsList = new List<EventArgs>(Instance().labelViewEvents);
            foreach(var arg in argsList)
            {
                if (arg.GetType() == typeof(LabelServiceValidateFpEventArgs))
                {
                    Instance().LabelServiceValidateFilePath?.Invoke(Instance(), arg);
                    Instance().labelViewEvents.Remove(arg);
                }
            }
        }

        public static void RaiseOrderViewEvents()
        {
            //notifiy new item test
            /*
            CatalogItemPetsi test = new CatalogItemPetsi();
            test.ItemName = "test";
            Instance().RaiseSoiNewItemEvent(test);
            */
            List<EventArgs> argsList = new List<EventArgs>(Instance().mainWindowEvents);
            foreach (var arg in argsList)
            {
                if (arg.GetType() == typeof(SoiMultiItemEventArgs))
                {
                    Instance().SoiMultiItem?.Invoke(Instance(), arg);
                    Instance().mainWindowEvents.Remove(arg);
                }
                if (arg.GetType() == typeof(SoiNewItemEventArgs))
                {
                    Instance().SoiNewItem?.Invoke(Instance(), arg);
                    Instance().mainWindowEvents.Remove(arg);
                }
                if (arg.GetType() == typeof(SquareMissingKeyEventArgs))
                {
                    Instance().NewStartupEvent?.Invoke(Instance(), arg);
                    Instance().mainWindowEvents.Remove(arg);
                }
            }
        }
    }

    public class SquareMissingKeyEventArgs : EventArgs
    {
        public SquareMissingKeyEventArgs()
        {
            
        }
    }

}
