using Petsi.Events;
using Petsi.Units;
using POMT_WPF.Core;
using POMT_WPF.MVVM.ObsModels;
using POMT_WPF.MVVM.View;

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

        public string ItemName { get { return _newItem.ItemName; } }
        private CatalogItemPetsi _newItem;
        private NewItemEventWindow _view;

        public NewItemEventWindowViewModel(SoiNewItemEventArgs args, NewItemEventWindow view)
        {
            CatalogVM = new CatalogViewModel();
            _newItem = args.NewItem;
            _view = view;
            CatalogItemPetsi newItem = new CatalogItemPetsi();

            CurrentView = CatalogVM;

            SelectCatalogItem = new RelayCommand(o => { SelectItemCommand(o); });
            CreateCatalogItem = new RelayCommand(o => { CreateItemCommand(); });
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
    }
}
