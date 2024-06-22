using Petsi.Models;
using Petsi.Units;
using System.Collections.ObjectModel;
using POMT_WPF.MVVM.ObsModels;
using POMT_WPF.Interfaces;

namespace POMT_WPF.MVVM.ViewModel
{
    public class CatalogListViewWindowModel : ViewModelBase, IObsCatalogModelSubscriber
    {
        CatalogModelPetsi cmp;

        ObservableCollection<CatalogItemPetsi> _items;
        public ObservableCollection<CatalogItemPetsi> Items
        {
            get { return _items; }
            set {
                if(_items != value)
                {
                    _items = value;
                    OnPropertyChanged(nameof(Items));
                }     
            }
        }

        ObservableCollection<CatalogItemPetsi> _filterItems;
        public ObservableCollection<CatalogItemPetsi> FilterItems
        {
            get { return _filterItems; }
            set
            {
                if (_filterItems != value)
                {
                    _filterItems = value;
                    OnPropertyChanged(nameof(FilterItems));
                }
            }
        }

        public CatalogListViewWindowModel()
        {
            //cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            //Items = new ObservableCollection<CatalogItemPetsi>(cmp.GetItems());
            ObsCatalogModelSingleton.Instance.Subscribe(this);
            Items = ObsCatalogModelSingleton.Instance.CatalogItems;
        }

        public void Update()
        {
           Items = ObsCatalogModelSingleton.Instance.CatalogItems;
        }
    }
}
