
namespace Petsi.Units
{
    public class PetsiOrder : ModelUnitBase/*, IEquatable<PetsiOrder>*/
    {
        public string InputOriginType { get; set; }
        public string Recipient { get; set; }
        public string OrderId { get; set; }
        public string OrderDueDate { get; set; }
        public string FulfillmentType { get; set; }
        public string Note { get; set; }
        public string DeliveryAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsPeriodic { get; set; }
        public bool IsOneShot { get; set; }
        public bool IsUserEntered { get; set; }
        public bool IsFrozen { get; set; }
        public string OrderType { get; set; }
        public string OrderFrequency { get; set; }

        public string DisplayOrderDueDate
        {
            get 
            {
                if(IsPeriodic)
                {
                    return DateTime.Parse(OrderDueDate).DayOfWeek.ToString();
                }
                return DateTime.Parse(OrderDueDate).ToString("d"); 
            }
            set {  }
        }
        public List<PetsiOrderLineItem> LineItems{ get; set; }

        public PetsiOrder(PetsiOrder? source)
        { 
            if(source != null)
            {
                InputOriginType = source.InputOriginType;
                Recipient = source.Recipient;
                OrderId = source.OrderId;
                OrderDueDate = source.OrderDueDate;
                FulfillmentType = source.FulfillmentType;
                Note = source.Note;
                DeliveryAddress = source.DeliveryAddress;
                PhoneNumber = source.PhoneNumber;
                Email = source.Email;
                IsPeriodic = source.IsPeriodic;
                IsOneShot = source.IsOneShot;
                IsUserEntered = source.IsUserEntered;
                IsFrozen = source.IsFrozen;
                OrderType = source.OrderType;
                LineItems = source.LineItems;
            }
        }

        public PetsiOrder()
        {
            LineItems = new List<PetsiOrderLineItem>();
        }
        /// <summary>
        /// For initializing a new OnOrderItem, LineItems list is initialized and empty.
        /// </summary>
        /// <param name="inputOrigin"></param>
        /// <param name="recipient"></param>
        /// <param name="orderId"></param>
        /// <param name="orderDueDate"></param>
        /// <param name="fulfillmentType"></param>
        /// <param name="note"></param>
        public PetsiOrder(
           string inputOrigin, string recipient, string orderId,
           string orderDueDate, string fulfillmentType, string note
           )
        {
            InputOriginType = inputOrigin;
            Recipient = recipient;
            OrderId = orderId;
            OrderDueDate = orderDueDate;
            FulfillmentType = fulfillmentType;
            Note = note;
            LineItems = new List<PetsiOrderLineItem>();
        }
        public PetsiOrder(
          string inputOrigin, string recipient, string orderId,
          string orderDueDate, string fulfillmentType, string note,
          List<PetsiOrderLineItem> lineItems
          )
        {
            InputOriginType = inputOrigin;
            Recipient = recipient;
            OrderId = orderId;
            OrderDueDate = orderDueDate;
            FulfillmentType = fulfillmentType;
            Note = note;
            LineItems = lineItems;
        }

        public List<PetsiOrderLineItem> GetLineItems(){ return LineItems;}

        public static string GenerateOrderId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
