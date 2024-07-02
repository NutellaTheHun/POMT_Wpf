using Petsi.Events;
using Petsi.Units;
using POMT_WPF.Core;
using POMT_WPF.MVVM.View;

namespace POMT_WPF.MVVM.ViewModel
{
    class NotifyNewCatalogItemViewModel : ViewModelBase
    {
        private NotifyNewCatalogItemWindow _view;
        private CatalogItemPetsi _newItem;
        public string NewItemName
        {
            get { return _newItem.ItemName; }
            /*set
            {
                if (_newItem != value) 
                {
                    _newItem = value;
                    OnPropertyChanged(nameof(NewItem));
                }
            }*/
        }

        public RelayCommand ViewItem { get; set; }
        public RelayCommand Close { get; set; }


        public NotifyNewCatalogItemViewModel(SoiNewItemEventArgs args, NotifyNewCatalogItemWindow view)
        {
            _view = view;
            _newItem = args.NewItem;

            ViewItem = new RelayCommand(o  => { _view.Close(); MainViewModel.Instance().OpenCatalogItemView(_newItem); });
            Close = new RelayCommand(o  => { _view.Close(); });
        }
    }
}
