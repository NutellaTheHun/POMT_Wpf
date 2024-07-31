
namespace Petsi.Events.ItemEvents
{
    public class TemplateItemViewEvents
    {
        public static TemplateItemViewEvents instance;
        private TemplateItemViewEvents()
        {

        }

        public static TemplateItemViewEvents Instance
        {
            get
            {
                if (instance == null) { instance = new TemplateItemViewEvents(); }
                return instance;
            }
        }

        public event EventHandler SaveSuccessful;
        public static void OnSaveSuccessful()
        {
            Instance.SaveSuccessful?.Invoke(Instance, EventArgs.Empty);
        }
    }
}
