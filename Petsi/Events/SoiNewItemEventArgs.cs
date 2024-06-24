
using Petsi.Units;

namespace Petsi.Events
{
    public class SoiNewItemEventArgs : EventArgs
    {
        CatalogItemPetsi NewItem { get; set; }
        public SoiNewItemEventArgs(CatalogItemPetsi item) { NewItem = item; }
    }
}
