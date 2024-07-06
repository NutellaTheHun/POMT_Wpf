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
        public RelayCommand Select { get; set; }


        public NotifyNewCatalogItemViewModel(SoiNewItemEventArgs args, NotifyNewCatalogItemWindow view)
        {
            _view = view;
            _newItem = args.NewItem;
            ViewItem = new RelayCommand(o  => { _view.Close(); MainViewModel.Instance().OpenCatalogItemView(_newItem); });
            Select = new RelayCommand(o  => {  });
        }

        private void OpenSelectItemViewWindow()
        {
            NewItemEventWindow window = new NewItemEventWindow(true, NewItemName);
            window.Show();
        }
        private void OpenNewItemViewWindow()
        {
            NewItemEventWindow window = new NewItemEventWindow(false);
            window.Show();
        }
    }
}
