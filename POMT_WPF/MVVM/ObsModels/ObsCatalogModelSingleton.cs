using DocumentFormat.OpenXml.Wordprocessing;
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
        private static ObsCatalogModelSingleton _instance;
        private CatalogModelPetsi _cmp;

        private List<IObsCatalogModelSubscriber> _subscriptions;

        private ObservableCollection<CatalogItemPetsi> _catalogItems;
        public ObservableCollection<CatalogItemPetsi> CatalogItems
        {
            get
            {
                if (_catalogItems == null)
                {
                    _catalogItems = new ObservableCollection<CatalogItemPetsi>();
                }
                return _catalogItems;
            }
            set
            {
                if (_catalogItems != value)
                {
                    _catalogItems = value;
                }
            }
        }

        private ObsCatalogModelSingleton()
        {
            _cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            CatalogItems = new ObservableCollection<CatalogItemPetsi>(_cmp.GetItems());
            _subscriptions = new List<IObsCatalogModelSubscriber>();
        }
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

        public void Subscribe(IObsCatalogModelSubscriber subscriber) { _subscriptions.Add(subscriber); }

        public static void AddItem(CatalogItemPetsi catalogItem)
        {
            int count = Instance.CatalogItems.Count;
            Instance.CatalogItems.Add(catalogItem);
            if(count+1 != Instance.CatalogItems.Count)
            {
                SystemLogger.Log("ObsCatalog AddItem failure: " + catalogItem.ItemName);
            }
            else
            {
                Instance.AddItemMainModel(catalogItem);
                Instance.Notify();
            }
        }
        private void AddItemMainModel(CatalogItemPetsi catalogItem)
        {
            _cmp.AddOrder(catalogItem);
        }
        public static void RemoveItem(CatalogItemPetsi catalogItem)
        {
            int count = Instance.CatalogItems.Count;
            Instance.CatalogItems.Remove(catalogItem);
            if (count - 1 != Instance.CatalogItems.Count)
            {
                SystemLogger.Log("ObsCatalog RemoveItem failure: " + catalogItem.ItemName);
            }
            else
            {
                Instance.RemoveItemMainModel(catalogItem);
                Instance.Notify();
            }
        }
        private void RemoveItemMainModel(CatalogItemPetsi catalogItem)
        {
            _cmp.RemoveItem(catalogItem);
        }

        public static void ModifyItem(CatalogItemPetsi modCatalogItem)
        {
            int index = 0;
            bool isfound = false;
            foreach (CatalogItemPetsi item in _instance.CatalogItems)
            {
                if (item.CatalogObjectId == modCatalogItem.CatalogObjectId)
                {
                    index = _instance.CatalogItems.IndexOf(item);
                    isfound = true;
                    break;
                }
            }
            if (isfound)
            {
                Instance.CatalogItems[index] = modCatalogItem;
                Instance.ModifyItemMainModel(modCatalogItem);
                Instance.Notify();
            }
            else
            {
                SystemLogger.Log("ObsCatalogModel item not found: " + modCatalogItem.ItemName + " : " +  modCatalogItem.CatalogObjectId);
            }
        }
        private void ModifyItemMainModel(CatalogItemPetsi catalogItem)
        {
            _cmp.ModifyItem(catalogItem);
        }
    }
}
