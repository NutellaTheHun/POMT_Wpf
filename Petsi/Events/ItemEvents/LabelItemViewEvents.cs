
namespace Petsi.Events.ItemEvents
{
    public class LabelItemViewEvents
    {
        public static LabelItemViewEvents instance;
        private LabelItemViewEvents()
        {

        }

        public static LabelItemViewEvents Instance
        {
            get
            {
                if (instance == null) { instance = new LabelItemViewEvents(); }
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
