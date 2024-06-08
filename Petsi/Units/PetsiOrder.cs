using Petsi.CommandLine;
using System.ComponentModel;

namespace Petsi.Units
{
    public class PetsiOrder : ModelUnitBase/*, IEquatable<PetsiOrder>*/
    {
        PetsiOrderFrameBehavior frameBehavior;
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
        public string OrderType { get; set; }
        public List<PetsiOrderLineItem> LineItems{ get; set; }
        
        public PetsiOrder()
        {
            LineItems = new List<PetsiOrderLineItem>();
            frameBehavior = new PetsiOrderFrameBehavior(this);
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
            frameBehavior = new PetsiOrderFrameBehavior(this);
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
            frameBehavior = new PetsiOrderFrameBehavior(this);
        }
        public override FrameBehaviorBase GetFrameBehavior()
        {
            return frameBehavior;
        }

        public List<PetsiOrderLineItem> GetLineItems()
        {
            return LineItems;
        }

        //public bool Equals(PetsiOrder? other)
        //{
        //    if (InputOriginType != other.InputOriginType)
        //    {
        //        return false;
        //    }
        //    if (Recipient.ToLower() != other.Recipient.ToLower())
        //    {
        //        return false;
        //    }
        //    if(OrderId != other.OrderId)
        //    {
        //        return false;
        //    }
        //    if(OrderDueDate != other.OrderDueDate)
        //    {
        //        return false;
        //    }
        //    if(FulfillmentType != other.FulfillmentType)
        //    {
        //        return false;
        //    }
        //    if (Note != other.Note)
        //    {
        //        return false;
        //    }
        //    foreach(PetsiOrderLineItem lineItem in other.LineItems)
        //    {
        //        if (!LineItems.Contains(lineItem)){ return false; }
        //    }
        //    return true;
        //}
    }
}
