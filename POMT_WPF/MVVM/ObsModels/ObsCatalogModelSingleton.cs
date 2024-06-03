
using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ObsModels
{
    public class ObsCatalogModelSingleton
    {
        private static ObsCatalogModelSingleton _instance;
        private CatalogModelPetsi _cmp;

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

        public static void AddItem(CatalogItemPetsi catalogItem)
        {
            Instance.CatalogItems.Add(catalogItem);
            Instance.AddOrderMainModel(catalogItem);
        }
        public static void RemoveItem(CatalogItemPetsi catalogItem)
        {
            Instance.CatalogItems.Remove(catalogItem);
            Instance.RemoveOrderMainModel(catalogItem);
        }
        public void AddOrderMainModel(CatalogItemPetsi catalogItem)
        {
            _cmp.AddOrder(catalogItem);
        }
        public void RemoveOrderMainModel(CatalogItemPetsi catalogItem)
        {
            _cmp.RemoveItem(catalogItem);
        }
    }
}
