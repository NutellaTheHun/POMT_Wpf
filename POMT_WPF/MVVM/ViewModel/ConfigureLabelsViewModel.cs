
using Petsi.Managers;
using Petsi.Models;
using Petsi.Services;
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

        public void RemoveItem(object selectedItem)
        {
            CatalogItemPetsi item = (CatalogItemPetsi)selectedItem;

            CatalogService cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            CatalogItemPetsi itemToUpdate = cs.GetCatalogItem(item.ItemName);
            if (itemToUpdate != null)
            {
                itemToUpdate.StandardLabelFilePath = null;
                itemToUpdate.CutieLabelFilePath = null;
                cmp.UpdateModel();
            }
            else
            {
                SystemLogger.Log("Label Configuration cannot find catalog item in model: " + item.ItemName);
            }

        }
        public void UpdateLabelList()
        {
            Items.Clear();
            List<CatalogItemPetsi> catalogList = SelectLabeledItems(cmp.GetItems());
            foreach (CatalogItemPetsi item in catalogList)
            {
                Items.Add(item);
            }
        }
    }
}
