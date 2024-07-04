using DocumentFormat.OpenXml.Spreadsheet;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.Interfaces;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ObsModels
{
    public class ObsCatalogModelSingleton
    {
        
        private CatalogModelPetsi _cmp;

        private List<IObsCatalogModelSubscriber> _subscriptions;

        public ObservableCollection<CatalogItemPetsi> CatalogItems;

        private static ObsCatalogModelSingleton _instance;
        public static ObsCatalogModelSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ObsCatalogModelSingleton();
                }
                return _instance;
            }
        }
        private void Notify()
        {
            foreach (var subscriptions in _subscriptions)
            {
                subscriptions.Update();
            }
        }

        private ObsCatalogModelSingleton()
        {
            _cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            
            CatalogItems = new ObservableCollection<CatalogItemPetsi>(_cmp.GetItems());
            _subscriptions = new List<IObsCatalogModelSubscriber>();
            CatalogItems.CollectionChanged += (s, e) => { UpdateCatalogModel(); Notify(); };
        }

        private void UpdateCatalogModel()
        {
            CatalogModelPetsi model = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            model.UpdateModel(CatalogItems);
        }

        public void Subscribe(IObsCatalogModelSubscriber subscriber) { _subscriptions.Add(subscriber); }

        /// <summary>
        /// Will check for a catalogObjectId match and replace if found, otherwise will add a new item.
        /// </summary>
        /// <param name="catalogItem"></param>
        public void AddItem(CatalogItemPetsi catalogItem)
        {
            if(catalogItem == null)
            {
                SystemLogger.Log("CATALOG ITEM ADD IS NULL");
                return;
            }
            bool isFound = false;

            //Try to modify
            foreach (CatalogItemPetsi item in CatalogItems)
            {
                if (item.CatalogObjectId == catalogItem.CatalogObjectId)
                {
                    int index = CatalogItems.IndexOf(item);
                    CatalogItems[index] = catalogItem;
                    isFound = true;
                    break;
                }
            }

            //If not modify, add new item
            if(!isFound) CatalogItems.Add(catalogItem);
        }

        public void RemoveItem(CatalogItemPetsi catalogItem)
        {
            int count = CatalogItems.Count;
            foreach(var item in CatalogItems)
            {
                if(item.CatalogObjectId == catalogItem.CatalogObjectId)
                {
                    CatalogItems.Remove(item);
                    break;
                }
            }
            if (count - 1 != CatalogItems.Count)
            {
                SystemLogger.Log("ObsCatalog RemoveItem failure: " + catalogItem.ItemName);
            }
        }
    }
}
