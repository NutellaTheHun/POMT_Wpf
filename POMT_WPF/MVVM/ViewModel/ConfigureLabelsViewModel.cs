using Petsi.Managers;
using Petsi.Models;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.Core;
using POMT_WPF.MVVM.View;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class ConfigureLabelsViewModel : ViewModelBase
    {
        CatalogModelPetsi cmp;

        private CatalogItemPetsi _selectedItem;
        public CatalogItemPetsi? SelectedItem { 
            get { return _selectedItem; } 
            set 
            { 
                if (_selectedItem != value) 
                { _selectedItem = value; 
                    OnPropertyChanged(nameof(SelectedItem));
                } 
            } 
        }
        public ObservableCollection<CatalogItemPetsi> Items { get; set; }

        public RelayCommand GoBack { get; set; }
        public RelayCommand ViewLabelMapping { get; set; }
        public RelayCommand RemoveLabelMapping { get; set; }

        public ConfigureLabelsViewModel()
        {
            cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            Items = new ObservableCollection<CatalogItemPetsi>( SelectLabeledItems(cmp.GetItems()));

            SelectedItem = null;

            GoBack = new RelayCommand(o => { MainViewModel.Instance().BackLabelView(); });
            ViewLabelMapping = new RelayCommand(o => { OpenLabelMapCommand(o); } );
            RemoveLabelMapping = new RelayCommand(o => { } );
        }

        private void RemoveLabelMapCommand(object o)
        {
            if (o is CatalogItemPetsi)
            {
                CatalogItemPetsi item = (CatalogItemPetsi)o;
                item.StandardLabelFilePath = null;
                item.CutieLabelFilePath = null;
                //save?
            }
        }

        private void OpenLabelMapCommand(object o) 
        {
            LabelItemWindow view = new LabelItemWindow((CatalogItemPetsi)o);
            view.Show();
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
