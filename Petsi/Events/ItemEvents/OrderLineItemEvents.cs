
namespace Petsi.Events.ItemEvents
{
    public class OrderLineItemEvents
    {
        public static OrderLineItemEvents instance;
        private OrderLineItemEvents()
        {

        }

        public static OrderLineItemEvents Instance
        {
            get
            {
                if (instance == null) { instance = new OrderLineItemEvents(); }
                return instance;
            }
        }

        public event EventHandler OnQuantityChange;
        public static void RaiseOnQuantityChange()
        {
            Instance.OnQuantityChange?.Invoke(Instance, EventArgs.Empty);
        }
    }
}
