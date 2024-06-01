using Petsi.CommandLine;
using Petsi.Filing;
using Petsi.Interfaces;
using Petsi.Managers;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Models
{
    public class OrderModelPetsi : ModelBase, IModelInput
    {
        List<PetsiOrder> Orders;

        OrderModelFrameBehavior frameBehavior;
        FileBehavior fileBehavior;
        public OrderModelPetsi()
        {
            Orders = new List<PetsiOrder>();
            frameBehavior = new OrderModelFrameBehavior(this);
            fileBehavior = new FileBehavior("OrderModel");
            SetModelName(Identifiers.MODEL_ORDERS);
            ModelManagerSingleton.GetInstance().Register(this);
            CommandFrame.GetInstance().RegisterFrame("omp", frameBehavior);
            EnvironCaptureRegistrySingleton.GetInstance().Register(this);
        }
        public override void AddData(ModelUnitBase unit){ Orders.Add((PetsiOrder)unit);}
        public override FrameBehaviorBase GetFrameBehavior() { return frameBehavior; }
        public List<PetsiOrder> GetOrders() { return Orders; }
        public void SetOrders(List<PetsiOrder> newOrders) { Orders = newOrders; }
        public FileBehavior GetFileBehavior() { return fileBehavior; }
        public override void ClearModel() { Orders.Clear(); }
        public override void AddOrder(ModelUnitBase order) { Orders.Add((PetsiOrder)order); SortOrders(); }
        public void RemoveOrder(ModelUnitBase item) { Orders.Remove((PetsiOrder)item);}
        public override void Complete() 
        {
           SortOrders();
        }
        private void SortOrders()
        {
            Orders = Orders.OrderBy(order => DateTime.Parse(order.OrderDueDate).Date)
                .ThenBy(order => order.Recipient)
                .ToList();
        }
        public override string GetModelName() { return ModelName; }
        public override void SetModelName(string modelName) { ModelName = modelName; }

        /// <summary>
        /// Generates a new order ID, use inputOrigin as prefix
        /// </summary>
        /// <returns></returns>
        public string GenerateOrderId()
        {
            return Guid.NewGuid().ToString();
        }
        //----------
        private List<PetsiOrderLineItem> AggregatePetsiOrders(List<PetsiOrder> orders)
        {
            Dictionary<string, PetsiOrderLineItem> aggregate = new Dictionary<string, PetsiOrderLineItem>();

            foreach (PetsiOrder order in orders)
            {
                foreach (PetsiOrderLineItem lineItem in order.LineItems)
                {
                    if (aggregate.ContainsKey(lineItem.CatalogObjectId))
                    {
                        aggregate[lineItem.CatalogObjectId].Merge(lineItem);
                    }
                    else
                    {
                        aggregate[lineItem.CatalogObjectId] = lineItem;
                    }
                }

            }
            return aggregate.Values.ToList();
        }
        public List<PetsiOrder> GetFrontListData(DateTime? targetDate)
        {
            IEnumerable<PetsiOrder> query;
            if (targetDate != null)
            {
                query =
                from order in Orders
                where DateTime.Parse(order.OrderDueDate).Date == targetDate.Value.Date
                orderby order.FulfillmentType, DateTime.Parse(order.OrderDueDate).TimeOfDay
                select order;
            }
            else
            {   
                query =
                from order in Orders
                orderby order.FulfillmentType, DateTime.Parse(order.OrderDueDate).TimeOfDay
                select order;
            }
            return query.ToList();
        }

        public List<PetsiOrderLineItem> GetBackListData(DateTime? targetDate, DateTime? endDate)
        {
            IEnumerable<PetsiOrder> query;
            if (targetDate == null) //all data
            {
              query =
              from order in Orders
              select order;
            }
            else if (endDate == null) //target date
            {
               query =
               from order in Orders
               where DateTime.Parse(order.OrderDueDate).Date == targetDate.Value.Date
               select order;
            }
            else //range
            {
                query =
                 from order in Orders
                 where DateTime.Parse(order.OrderDueDate).Date >= targetDate.Value.Date
                 where DateTime.Parse(order.OrderDueDate).Date <= endDate.Value.Date
                 select order;
            }
            return AggregatePetsiOrders(query.ToList()); 
        }

        //Label Service uses it, WS_Day_report still needs day separation tho, and day info
        public List<PetsiOrderLineItem> GetWsDayData(DateTime? targetDate)
        {
            IEnumerable<PetsiOrder> query;
            if (targetDate == null)
            {
                query = from order in Orders
                        where order.InputOriginType == Identifiers.WHOLESALE_INPUT
                        select order;
            }
            else
            {
                query = from order in Orders
                        where order.InputOriginType == Identifiers.WHOLESALE_INPUT
                        where DateTime.Parse(order.OrderDueDate).Date == targetDate.Value.Date
                        select order;
            }
            return AggregatePetsiOrders(query.ToList()).OrderBy(item => item.ItemName).ToList();
        }

        public List<PetsiOrder> GetWsDayNameData(DateTime? targetDate)
        {
            IEnumerable<PetsiOrder> query;
            if (targetDate == null)
            {
                query = from order in Orders
                        where order.InputOriginType == Identifiers.WHOLESALE_INPUT
                        select order;
            }
            else
            {
                query = from order in Orders
                        where order.InputOriginType == Identifiers.WHOLESALE_INPUT
                        where DateTime.Parse(order.OrderDueDate).Date == targetDate.Value.Date
                        select order;
            }

            List <PetsiOrder> result = query.ToList();

            foreach (PetsiOrder order in result)
            {
                order.LineItems = order.LineItems.OrderBy(line => line.ItemName).ToList();
            }
            return result;
        }
        //-----
        public static List<PetsiOrder> MergeOrders(List<PetsiOrder> mainOrders,  List<PetsiOrder> otherOrders)
        {
            List<PetsiOrder> result = new List<PetsiOrder>(mainOrders);
            result.AddRange(otherOrders.Where(order => !result.Contains(order)));
            return result;
        }
        /// <summary>
        /// Returns all orders where input is contained within the recipient variable
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public List<PetsiOrder> SearchByRecipient(string searchTerm)
        {
            List<PetsiOrder> result = new List<PetsiOrder>();

            result.AddRange(Orders.Where(order => order.Recipient.ToLower().Contains(searchTerm)));
            return result;
        }
        public override void CaptureEnvironment(FileBehavior reportFb)
        {
            reportFb.DataListToFile(Identifiers.ENV_OMP, Orders);
        }

        public void RemoveItem(PetsiOrder order)
        {
            Orders.Remove(order);
        }
    }   
}

