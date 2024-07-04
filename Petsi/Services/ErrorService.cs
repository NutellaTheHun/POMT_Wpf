﻿using Petsi.Events;
using Petsi.Events.ItemEvents;
using Petsi.Models;
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
            //SetServiceName(Identifiers.SERVICE_ERROR);
        }

        public static ErrorService Instance()
        {
            if (instance == null) { instance = new ErrorService(); }
            return instance;
        }
        /*
        public override void Update(ModelBase model)
        {
            throw new NotImplementedException();
        }
        */
        #endregion

        #region events
        List<EventArgs> mainWindowEvents = new List<EventArgs>();

        List<EventArgs> labelViewEvents = new List<EventArgs>();

        //Table Builder Overflow Event
        public delegate void TableBuilderOverflowEvent(object sender, EventArgs e);

        public event TableBuilderOverflowEvent TBOverflow;
        public void RaiseTBOverflowEvent(List<PetsiOrderLineItem> overflowList)
        {
            TBOverflowEventArgs args = new TBOverflowEventArgs(overflowList);
            TBOverflow?.Invoke(this, args);
        }

        //Square Order Input New Item Event
        public delegate void SquareOrderInputNewItemEvent(object sender, EventArgs e);

        public event SquareOrderInputNewItemEvent SoiNewItem;

        public void RaiseSoiNewItemEvent(CatalogItemPetsi newItem)
        {
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
                multiItemNameEventCalls.Add(itemContext);
                SoiMultiItemEventArgs args = new SoiMultiItemEventArgs(itemContext, multiItemList);
                mainWindowEvents.Add(args);
            }          
        }

        public delegate void LabelServiceValidateFilePathEvent(object sender, EventArgs e);
        public event LabelServiceValidateFilePathEvent LabelServiceValidateFilePath;
        public void RaiseLabelServiceValidateFilePathEvent(string catalogId, string fileName, string pieType)
        {
            LabelServiceValidateFpEventArgs args = new LabelServiceValidateFpEventArgs(catalogId, fileName, pieType);
            //LabelServiceValidateFilePath?.Invoke(this, args);
            labelViewEvents.Add(args);
        }

        #endregion

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

        public static void RaiseWindowEvents()
        {
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
            }
        }
    }
}
