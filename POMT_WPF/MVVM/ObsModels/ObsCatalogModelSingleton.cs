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
    

        private ObsCatalogModelSingleton()
        {
            _cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            
            CatalogItems = new ObservableCollection<CatalogItemPetsi>(_cmp.GetItems());
            _subscriptions = new List<IObsCatalogModelSubscriber>();
            CatalogItems.CollectionChanged += (s, e) => { UpdateCatalogModel(); };
        }

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

        private void UpdateCatalogModel()
        {
            CatalogModelPetsi model = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            model.UpdateModel(CatalogItems);
        }

        public void Subscribe(IObsCatalogModelSubscriber subscriber) { _subscriptions.Add(subscriber); }

        public void AddItem(CatalogItemPetsi catalogItem)
        {
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
            
            /*
            AddItemMainModel(catalogItem);
            Notify();
            */
        }
        private void AddItemMainModel(CatalogItemPetsi catalogItem)
        {
            _cmp.AddOrder(catalogItem);
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
            /*
            else
            {
                RemoveItemMainModel(catalogItem);
                Notify();
            }*/
        }
        private void RemoveItemMainModel(CatalogItemPetsi catalogItem)
        {
            _cmp.RemoveItem(catalogItem);
        }

        public static void ModifyItem(CatalogItemPetsi modCatalogItem)
        {
            int index = 0;
            bool isfound = false;
            foreach (CatalogItemPetsi item in Instance.CatalogItems)
            {
                if (item.CatalogObjectId == modCatalogItem.CatalogObjectId)
                {
                    index = Instance.CatalogItems.IndexOf(item);
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
