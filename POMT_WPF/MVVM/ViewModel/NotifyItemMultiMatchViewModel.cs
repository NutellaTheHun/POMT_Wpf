using Petsi.Events;
using Petsi.Units;
using POMT_WPF.Core;
using POMT_WPF.MVVM.ObsModels;
using POMT_WPF.MVVM.View;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    /// <summary>
    /// On application startup, when pulling square orders in the SquareOrderInput object, 
    /// some items in the square catalog contain the relevant item information in the modifiers section, such as Box of 6 scones/cookies/muffins,
    /// These item representations in the modifiers section have no connection to the items they represent to the actual objects in the catalog,
    /// these items are delibrately matched using the catalog service.
    /// The NotifyItemMultiMatch event occurs when an item name returns multiple items as a potential match, 
    /// for example: item name "Lemon Glaze" scones used to just be called "Lemon", 
    /// when attempting to match would return multiple items such as "Lemon Chess", "Lemon Chess with Lavender", and "Lemon Chess with Blueberry"
    /// To continue parsing orders when this event occurs, the problem is marked with a unique ID in the catalogObjId field for correcting once the application is finished.
    /// This event must be handled by the user, by either associating the new item with a pre-existing one, or creating a new item.
    /// after a user makes a choice, the update to the ObsCatalogModel calls the ObsOrderModel to check for any standing orders containing an item with the uniqueId for the multimatch event
    /// If the Unique id is found, the catalog service is called to update the ID to either the associated item, or the new item.

    /// </summary>
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
            _view.Close();
            MainViewModel.Instance().OpenCatalogItemView(newItem);
        }

        private void SelectItemCommand(object o)
        {
            if(o is CatalogItemPetsi)
            {
                ((CatalogItemPetsi)o).NaturalNames.Add(ItemContext);
                ObsCatalogModelSingleton.Instance.AddItem((CatalogItemPetsi)o);
                _view.Close();
            }
        }
    }
}
