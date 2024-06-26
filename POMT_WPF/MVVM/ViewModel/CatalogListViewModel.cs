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
            ObsCatalogModelSingleton.Instance.Subscribe(this);
            Items = ObsCatalogModelSingleton.Instance.CatalogItems;
        }

        public void Update()
        {
           Items = ObsCatalogModelSingleton.Instance.CatalogItems;
        }

        public void FilterSearchBar(string text)
        {
            ObservableCollection<CatalogItemPetsi> catalogItems = ObsCatalogModelSingleton.Instance.CatalogItems;
            ObservableCollection<CatalogItemPetsi> results = new ObservableCollection<CatalogItemPetsi>();
            foreach (CatalogItemPetsi item in catalogItems)
            {
                if (item.ItemName.ToLower().Contains(text.ToLower()))
                {
                    results.Add(item);
                    continue;
                }
            }
            Items = results;
        }
    }
}
