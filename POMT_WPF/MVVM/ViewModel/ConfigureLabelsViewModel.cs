using Petsi.Managers;
using Petsi.Models;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.Core;
using POMT_WPF.Interfaces;
using POMT_WPF.MVVM.ObsModels;
using POMT_WPF.MVVM.View;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class ConfigureLabelsViewModel : ViewModelBase, IObsCatalogModelSubscriber
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
        private ObservableCollection<CatalogItemPetsi> _items;
        public ObservableCollection<CatalogItemPetsi> Items 
        {
            get { return _items; }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged(nameof(Items));
                }
            }
        }

        public RelayCommand GoBack { get; set; }
        public RelayCommand ViewLabelMapping { get; set; }
        public RelayCommand CreateLabelMapping { get; set; }
        public RelayCommand RemoveLabelMapping { get; set; }

        public ConfigureLabelsViewModel()
        {
            cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            ObsCatalogModelSingleton.Instance.Subscribe(this);
            Items = new ObservableCollection<CatalogItemPetsi>( 
                SelectLabeledItems(
                    ObsCatalogModelSingleton.Instance.CatalogItems.ToList()));

            SelectedItem = null;

            GoBack = new RelayCommand(o => { MainViewModel.Instance().BackLabelView(); });
            ViewLabelMapping = new RelayCommand(o => { OpenLabelMapCommand(o); } );
            RemoveLabelMapping = new RelayCommand(o => { RemoveLabelMapCommand(o); } );
            CreateLabelMapping = new RelayCommand(o => { CreateLabelMapCommand(); } );
        }

        private void RemoveLabelMapCommand(object o)
        {
            if (o is CatalogItemPetsi)
            {
                ConfirmationWindow window = new ConfirmationWindow(null);
                window.ShowDialog();
                if(window.ControlBool)
                {
                    /*
                    CatalogItemPetsi item = (CatalogItemPetsi)o;
                    item.StandardLabelFilePath = null;
                    item.CutieLabelFilePath = null;
                    //save?
                    */
                    CatalogService cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
                    CatalogItemPetsi itemToUpdate = cs.GetCatalogItem(((CatalogItemPetsi)o).ItemName);
                    if (itemToUpdate != null)
                    {
                        itemToUpdate.StandardLabelFilePath = null;
                        itemToUpdate.CutieLabelFilePath = null;

                        //Bad naming in this instance, AddItem function modifies before adding, 
                        //removing an item in this context is clearing the filepaths from the catalog item
                        //not a true delete from the model.
                        ObsCatalogModelSingleton.Instance.AddItem(itemToUpdate);
                    }
                    else
                    {
                        SystemLogger.Log("Label Configuration cannot find catalog item in model: " + ((CatalogItemPetsi)o).ItemName);
                    }
                }
            }
        }

        private void OpenLabelMapCommand(object o) 
        {
            if(o is CatalogItemPetsi)
            {
                LabelItemWindow view = new LabelItemWindow((CatalogItemPetsi)o, Items.ToList());
                view.Show();
            }
        }

        private void CreateLabelMapCommand()
        {
            LabelItemWindow view = new LabelItemWindow(null, Items.ToList());
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

                //Bad naming in this instance, AddItem function modifies before adding, 
                //removing an item in this context is clearing the filepaths from the catalog item
                //not a true delete from the model.
                ObsCatalogModelSingleton.Instance.AddItem(item); //this is updating the wrong item lol
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

        public void Update()
        {
            Items = new ObservableCollection<CatalogItemPetsi>(
               SelectLabeledItems(
                   ObsCatalogModelSingleton.Instance.CatalogItems.ToList()));
        }
    }
}
