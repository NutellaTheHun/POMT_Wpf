
using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class ConfigureLabelsViewModel : ViewModelBase
    {
        CatalogModelPetsi cmp;
        CatalogItemPetsi? _selectedItem;
        public CatalogItemPetsi? SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if( _selectedItem != value )
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof( SelectedItem));
                }
            }
        }

        ObservableCollection<CatalogItemPetsi> _items;
        public ObservableCollection<CatalogItemPetsi> Items
        {
            get { return _items; }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged(nameof(_items));
                }
            }
        }

        public ConfigureLabelsViewModel()
        {
            cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            Items = new ObservableCollection<CatalogItemPetsi>( SelectLabeledItems(cmp.GetItems()));
            SelectedItem = null;
        }

        private List<CatalogItemPetsi> SelectLabeledItems(List<CatalogItemPetsi> inputList)
        {
            return inputList.Where(x => x.StandardLabelFilePath != null || x.CutieLabelFilePath != null).ToList();
        }
    }
}
