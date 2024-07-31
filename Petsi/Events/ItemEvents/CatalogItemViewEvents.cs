
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

        public event EventHandler CategoryNameInvalid;
        public static void OnCategoryNameInvalid()
        {
            Instance.CategoryNameInvalid?.Invoke(Instance, EventArgs.Empty);
        }
        public event EventHandler CategorySizesInvalid;
        public static void OnCategorySizesInvalid()
        {
            Instance.CategorySizesInvalid?.Invoke(Instance, EventArgs.Empty);
        }

        public event EventHandler SaveSuccessful;
        public static void OnSaveSuccessful()
        {
            Instance.SaveSuccessful?.Invoke(Instance, EventArgs.Empty);
        }
    }
}
