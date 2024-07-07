using Petsi.Models;
using Petsi.Units;
using System.Collections.ObjectModel;
using POMT_WPF.MVVM.ObsModels;
using POMT_WPF.Core;

namespace POMT_WPF.MVVM.ViewModel
{
    public class CatalogViewModel : ViewModelBase
    {
        CatalogModelPetsi cmp;
        public ObservableCollection<CatalogItemPetsi> Items { get; set; }
        //public ObservableCollection<CatalogItemPetsi> FilterItems { get; set; }

        public RelayCommand OpenCatalogItemView {  get; set; }

        public CatalogViewModel()
        {
            Items = ObsCatalogModelSingleton.Instance.CatalogItems;
            OpenCatalogItemView = new RelayCommand(o => { MainViewModel.Instance().OpenCatalogItemView(o); });
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
