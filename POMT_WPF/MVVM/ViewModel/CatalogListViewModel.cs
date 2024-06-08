using Petsi.Models;
using Petsi.Managers;
using Petsi.Units;
using Petsi.Utils;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class CatalogListViewWindowModel : ViewModelBase
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
                    OnPropertyChanged(nameof(_items));
                }     
            }
        }

        public CatalogListViewWindowModel()
        {
            cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            Items = new ObservableCollection<CatalogItemPetsi>(cmp.GetItems());
        }

    }
}
