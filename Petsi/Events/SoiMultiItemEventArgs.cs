using Petsi.Units;

namespace Petsi.Events
{
    public class SoiMultiItemEventArgs : EventArgs
    {
        public List<CatalogItemPetsi> MultItemList { get; set; }
        public string ItemContext;
        public SoiMultiItemEventArgs( string itemContext, List<CatalogItemPetsi> input) { ItemContext = itemContext; MultItemList = new List<CatalogItemPetsi>(input); }
    }
}
