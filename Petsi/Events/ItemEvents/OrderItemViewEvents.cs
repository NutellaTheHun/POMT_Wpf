
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
        public static void RaiseRecipientInvalidEvent()
        {
            Instance.RecipientInvalid?.Invoke(Instance, EventArgs.Empty);
        }

        public event EventHandler FulfillmentInvalid;
        public static void RaiseFulfillmentInvalidEvent()
        {
            Instance.FulfillmentInvalid?.Invoke(Instance, EventArgs.Empty);
        }


        public event EventHandler OrderTypeInvalid;
        public static void RaiseOrderTypeInvalidEvent()
        {
            Instance.OrderTypeInvalid?.Invoke(Instance, EventArgs.Empty);
        }


        public event EventHandler FrequencyInvalid;
        public static void RaiseFrequencyInvalidEvent()
        {
            Instance.FrequencyInvalid?.Invoke(Instance, EventArgs.Empty);
        }

        public event EventHandler LineItemsInvalid;
        public static void RaiseLineItemsInvalidEvent()
        {
            Instance.LineItemsInvalid?.Invoke(Instance, EventArgs.Empty);
        }

        public event EventHandler DelAddressInvalid;
        public static void RaiseDelAddressEvent()
        {
            Instance.DelAddressInvalid?.Invoke(Instance, EventArgs.Empty);
        }

        public event EventHandler PhoneInvalid;
        public static void RaisePhoneInvalidEvent()
        {
            Instance.PhoneInvalid?.Invoke(Instance, EventArgs.Empty);
        }

        public event EventHandler DatePickerInvalid;
        public static void RaiseDatePickerInvalidEvent()
        {
            Instance.DatePickerInvalid?.Invoke(Instance, EventArgs.Empty);
        }

        public event EventHandler DatePickerLessThanInvalid;
        public static void RaiseDatePickerLessThanEvent()
        {
            Instance.DatePickerLessThanInvalid?.Invoke(Instance, EventArgs.Empty);
        }

        public event EventHandler TimeInvalid;
        public static void RaiseTimeInvalidEvent()
        {
            Instance.TimeInvalid?.Invoke(Instance, EventArgs.Empty);
        }

        public event EventHandler SaveSuccess;
        public static void RaiseSaveSuccessEvent()
        {
            Instance.SaveSuccess?.Invoke(Instance, EventArgs.Empty);
        }
    }
}
