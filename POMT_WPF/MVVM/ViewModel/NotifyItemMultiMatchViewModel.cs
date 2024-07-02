using Petsi.Events;
using Petsi.Units;
using POMT_WPF.Core;
using POMT_WPF.MVVM.View;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class NotifyItemMultiMatchViewModel : ViewModelBase
    {
        private NotifyMultiItemMatchWindow _view;

        public ObservableCollection<CatalogItemPetsi> MultiItemList { get; set; }

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
        
        public RelayCommand CreateItem {  get; set; }
        public RelayCommand SelectItem {  get; set; }

        public NotifyItemMultiMatchViewModel(SoiMultiItemEventArgs args, NotifyMultiItemMatchWindow view)
        {
            _view = view;
            ItemContext = args.ItemContext;
            MultiItemList = new ObservableCollection<CatalogItemPetsi>(args.MultItemList);

            CreateItem = new RelayCommand(o => { CreateItemCommand(); });
            SelectItem = new RelayCommand(o => { SelectItemCommand(o); });
        }

        private void CreateItemCommand()
        {
            CatalogItemPetsi newItem = new CatalogItemPetsi();
            newItem.ItemName = ItemContext;
            _view.Close(); //????????????????????
            MainViewModel.Instance().OpenCatalogItemView(newItem);
        }

        private void SelectItemCommand(object o)
        {
            if(o is CatalogItemPetsi)
            {
                ((CatalogItemPetsi)o).NaturalNames.Add(ItemContext);

                //Update

                //ObsCatalogModelSingleton.ModifyItem(matchItem);

                //OrderModelPetsi omp = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
                //omp.UpdateMultiLineMatchEvent();

                _view.Close();
            }
        }
    }
}
