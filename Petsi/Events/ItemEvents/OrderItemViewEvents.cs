
namespace Petsi.Events.ItemEvents
{
    public class OrderItemViewEvents
    {
        public static OrderItemViewEvents instance;
        private OrderItemViewEvents()
        {
            
        }

        public static OrderItemViewEvents Instance
        {
            get
            {
                if (instance == null) { instance = new OrderItemViewEvents(); }
                return instance;
            }  
        }

        public event EventHandler RecipientInvalid;
        public static void OnRecipientInvalid()
        {
            Instance.RecipientInvalid?.Invoke(Instance, EventArgs.Empty);
        }

        public event EventHandler FulfillmentInvalid;
        public static void OnFulfillmentInvalid()
        {
            Instance.FulfillmentInvalid?.Invoke(Instance, EventArgs.Empty);
        }


        public event EventHandler OrderTypeInvalid;
        public static void OnOrderTypeInvalid()
        {
            Instance.OrderTypeInvalid?.Invoke(Instance, EventArgs.Empty);
        }


        public event EventHandler FrequencyInvalid;
        public static void OnFrequencyInvalid()
        {
            Instance.FrequencyInvalid?.Invoke(Instance, EventArgs.Empty);
        }

        public event EventHandler LineItemsInvalid;
        public static void OnLineItemsInvalid()
        {
            Instance.LineItemsInvalid?.Invoke(Instance, EventArgs.Empty);
        }

        public event EventHandler DelAddressInvalid;
        public static void OnDelAddressInvalid()
        {
            Instance.DelAddressInvalid?.Invoke(Instance, EventArgs.Empty);
        }

        public event EventHandler PhoneInvalid;
        public static void OnPhoneInvalid()
        {
            Instance.PhoneInvalid?.Invoke(Instance, EventArgs.Empty);
        }

        public event EventHandler DatePickerInvalid;
        public static void OnDatePickerInvalid()
        {
            Instance.DatePickerInvalid?.Invoke(Instance, EventArgs.Empty);
        }

        public event EventHandler DatePickerLessThanInvalid;
        public static void OnDatePickerLessThanInvalid()
        {
            Instance.DatePickerLessThanInvalid?.Invoke(Instance, EventArgs.Empty);
        }

        public event EventHandler DOTWInvalid;
        public static void OnDOTWInvalid()
        {
            Instance.DOTWInvalid?.Invoke(Instance, EventArgs.Empty);
        }
    }
}
