
namespace Petsi.Events.ItemEvents
{
    public class CatalogItemViewEvents
    {
        public static CatalogItemViewEvents instance;
        private CatalogItemViewEvents()
        {

        }

        public static CatalogItemViewEvents Instance
        {
            get
            {
                if (instance == null) { instance = new CatalogItemViewEvents(); }
                return instance;
            }
        }

        
        public event EventHandler ItemNameInvalid;
        public static void OnItemNameInvalid()
        {
            Instance.ItemNameInvalid?.Invoke(Instance, EventArgs.Empty);
        }
    }
}
