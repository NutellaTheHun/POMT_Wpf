using Petsi.Managers;
using Petsi.Services;
using Petsi.Utils;

namespace Petsi.Units
{
    public class SquareOrderItem
    {
        public string Id { get; set; }
        public string FulfillmentType { get; set; }
        public string LocationId { get; set; }
        public string Uid { get; set; }
        public string Pickup_time { get; set; }
        public string Note { get; set; }
        public string RecipientName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<LineItem> LineItems { get; set; }
        public SquareOrderItem() { }
        public SquareOrderItem(
            string id, string fulfillmentType,
            string locationId,
            string uid, string pickup_time,
            string note, string recipientName,
            string address, List<LineItem> lineItems, string phoneNumber)
        {
            Id = id;
            FulfillmentType = fulfillmentType;
            LocationId = locationId;
            Uid = uid;
            Pickup_time = pickup_time;
            Note = note;
            RecipientName = recipientName;
            Address = address;
            LineItems = lineItems;
            PhoneNumber = phoneNumber;
        }
        public PetsiOrder ToPetsiOrder()
        {
            PetsiOrder o = new PetsiOrder();

            //if a chill order
            if(LocationId == "LMM3H2WYN5K4W")
            {
                o.FulfillmentType = "DELIVERY";
                o.Note = "Chill Order " + Note;
            } 
            else 
            {             
                o.FulfillmentType = FulfillmentType;
                o.Note = Note;
            }
            o.InputOriginType = Identifiers.ORDER_INPUT_ORIGIN_SQUARE;
            o.Recipient = RecipientName;
            o.OrderId = Id;
            o.OrderDueDate = Pickup_time;         
            o.LineItems = ToPetsiOrderLineItemList();
            o.IsPeriodic = false;
            o.IsOneShot = true;
            o.OrderType = Identifiers.ORDER_TYPE_SQUARE;
            o.DeliveryAddress = Address;
            o.PhoneNumber = PhoneNumber;
            return o;
        }
        private List<PetsiOrderLineItem> ToPetsiOrderLineItemList()
        {
            CategoryService categories = (CategoryService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATEGORY);
            CatalogService catalog = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            //Key: CatalogObjId
            Dictionary<string, PetsiOrderLineItem> variationSizeDict = new Dictionary<string, PetsiOrderLineItem>();
            PetsiOrderLineItem PetsiLineItem;

            foreach (LineItem ChannelLineItem in LineItems)
            {
                //If the item is already in the dictionary, (a previous size has already been parsed)
                if (variationSizeDict.ContainsKey(ChannelLineItem.CatalogObjectId))
                {
                    //A order from square shouldnt have duplicate lines of item and size, so an assignment should suffice, (just "=" rather than "+=")
                    if (ChannelLineItem.VariationName.Contains(Identifiers.SIZE_SMALL))
                    {
                        variationSizeDict[ChannelLineItem.CatalogObjectId].Amount5 += int.Parse(ChannelLineItem.Quantity);
                    }
                    else if (ChannelLineItem.VariationName.Contains(Identifiers.SIZE_MEDIUM))
                    {
                        variationSizeDict[ChannelLineItem.CatalogObjectId].Amount8 += int.Parse(ChannelLineItem.Quantity);
                    }
                    else if (ChannelLineItem.VariationName.Contains(Identifiers.SIZE_LARGE))
                    {
                        variationSizeDict[ChannelLineItem.CatalogObjectId].Amount10 += int.Parse(ChannelLineItem.Quantity);
                    }
                    else if (ChannelLineItem.VariationName.Contains(Identifiers.SIZE_REGULAR)) //Regular is used for items that aren't Pies, such as pastries and merch.
                    {
                        if (ChannelLineItem.IsTakeNBake(categories, catalog))
                        {
                            variationSizeDict[ChannelLineItem.CatalogObjectId].Amount10 += int.Parse(ChannelLineItem.Quantity);
                        }
                        else
                        {
                            variationSizeDict[ChannelLineItem.CatalogObjectId].AmountRegular += int.Parse(ChannelLineItem.Quantity);
                        }
                        
                    }
                }
                else //create a new item, parse the size and quantity, add to dictionary
                {
                    PetsiLineItem = new PetsiOrderLineItem();
                    PetsiLineItem.CatalogObjectId = ChannelLineItem.CatalogObjectId;
                    PetsiLineItem.ItemName = ChannelLineItem.ItemName;
                    PetsiLineItem.Amount3 = 0;
                    PetsiLineItem.IsValid = true;

                    if (ChannelLineItem.VariationName.Contains("Small"))
                    {
                        PetsiLineItem.Amount5 = int.Parse(ChannelLineItem.Quantity);
                    }
                    else if (ChannelLineItem.VariationName.Contains("Medium"))
                    {
                        PetsiLineItem.Amount8 = int.Parse(ChannelLineItem.Quantity);
                    }
                    else if (ChannelLineItem.VariationName.Contains("Large"))
                    {
                        PetsiLineItem.Amount10 = int.Parse(ChannelLineItem.Quantity);
                    }
                    else if (ChannelLineItem.VariationName.Contains("Regular"))
                    {
                        /*
                        if (ChannelLineItem.IsTakeNBake(categories, catalog))
                        {
                            PetsiLineItem.Amount10 = int.Parse(ChannelLineItem.Quantity);
                        }
                        else
                        {
                            PetsiLineItem.AmountRegular = int.Parse(ChannelLineItem.Quantity);
                        }*/
                        PetsiLineItem.AmountRegular = int.Parse(ChannelLineItem.Quantity);

                    }
                    variationSizeDict.Add(ChannelLineItem.CatalogObjectId, PetsiLineItem);
                }
            }
            return variationSizeDict.Values.ToList();
        }
        public void TestPrintOrder()
        {
            Console.WriteLine("NAME: " + RecipientName + ", ID: " + Id + ", LOC ID: " + LocationId + ", UID: " + Uid);
            Console.WriteLine("TYPE: " + FulfillmentType + ", PICKUP: " + Pickup_time + ", NOTE: " + Note);
            foreach (LineItem lineItem in LineItems)
            {
                Console.WriteLine(lineItem.ToString());
            }
        }

        private string TestLineItems()
        {
            string result = "";
            foreach (LineItem lineItem in LineItems)
            {
                result += lineItem.ToString();
            }
            return result;
        }
        public override string ToString()
        {
            string result = "NAME: " + RecipientName + ", ID: " + Id + ", LOC ID: " + LocationId + ", UID: " + Uid + "\n"
                + "TYPE: " + FulfillmentType + ", PICKUP: " + Pickup_time + ", NOTE: " + Note;
            result += TestLineItems();
            return result;
        }
    }
}