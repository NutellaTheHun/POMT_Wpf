using Petsi.Events;
using Petsi.Models;
using Petsi.Units;
using POMT_WPF.Core;
using POMT_WPF.MVVM.ObsModels;
using POMT_WPF.MVVM.View;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    class NewItemEventWindowViewModel : ObservableObject
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public CatalogViewModel CatalogVM { get; set; }
        public CatalogItemViewModel CatalogItemVM { get; set; }

        public RelayCommand SelectCatalogItem {  get; set; }
        public RelayCommand CreateCatalogItem {  get; set; }

        public string ItemName { get { return _newItem.ItemName; } set { } }
        private CatalogItemPetsi _newItem;
        private NewItemEventWindow _view;

        CatalogModelPetsi cmp;
        public ObservableCollection<CatalogItemPetsi> Items { get; set; }

        public NewItemEventWindowViewModel(SoiNewItemEventArgs args, NewItemEventWindow view)
        {
            CatalogVM = new CatalogViewModel();
            _newItem = args.NewItem;
            _view = view;
            CatalogItemPetsi newItem = new CatalogItemPetsi();

            CurrentView = CatalogVM;

            SelectCatalogItem = new RelayCommand(o => { SelectItemCommand(o); });
            CreateCatalogItem = new RelayCommand(o => { CreateItemCommand(); });
            Items = ObsCatalogModelSingleton.Instance.CatalogItems;
        }

        private void CreateItemCommand()
        {
            _newItem.CatalogObjectId = CatalogItemPetsi.GenerateCatalogId();
            _view.Close();
            MainViewModel.Instance().OpenCatalogItemView(_newItem);
        }

        private void SelectItemCommand(object o)
        {
            if (o is CatalogItemPetsi)
            {
                ((CatalogItemPetsi)o).NaturalNames.Add(_newItem.ItemName);
                ObsCatalogModelSingleton.Instance.AddItem((CatalogItemPetsi)o);
                _view.Close();
            }
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
