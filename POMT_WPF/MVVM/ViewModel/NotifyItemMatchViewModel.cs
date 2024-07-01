
using Petsi.Events;
using Petsi.Units;
using POMT_WPF.Core;
using POMT_WPF.MVVM.View;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class NotifyItemMatchViewModel : ViewModelBase
    {
        private NotifyMultiItemMatchWindow _view;

        private string _itemContext;
        public string ItemContext
        {
            get { return _itemContext; }
            set
            {
                if (_itemContext != value)
                {
                    _itemContext = value;
                    OnPropertyChanged(nameof(ItemContext));
                }
            }
        }
        ObservableCollection<CatalogItemPetsi> MultiItemList;
        public RelayCommand CreateItem {  get; set; }
        public RelayCommand SelectItem {  get; set; }

        public NotifyItemMatchViewModel(SoiMultiItemEventArgs args, NotifyMultiItemMatchWindow view)
        {
            _view = view;
            ItemContext = args.ItemContext;
            MultiItemList = new ObservableCollection<CatalogItemPetsi>(args.MultItemList);

            CreateItem = new RelayCommand(o => { CreateItemCommand(); });
            SelectItem = new RelayCommand(o => { SelectItemCommand(o); });
        }

        private void CreateItemCommand()
        {

        }

        private void SelectItemCommand(object o)
        {
            if(o is CatalogItemPetsi)
            {

            }
        }
    }
}
