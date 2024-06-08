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
        List<PetsiOrder> OneShotOrders;
        List<PetsiOrder> PeriodicOrders;

        OrderModelFrameBehavior frameBehavior;
        FileBehavior fileBehavior;
        public OrderModelPetsi()
        {

            frameBehavior = new OrderModelFrameBehavior(this);
            fileBehavior = new FileBehavior("OrderModel");
            SetModelName(Identifiers.MODEL_ORDERS);
            ModelManagerSingleton.GetInstance().Register(this);
            CommandFrame.GetInstance().RegisterFrame("omp", frameBehavior);
            EnvironCaptureRegistrySingleton.GetInstance().Register(this);
            Orders = new List<PetsiOrder>();
            OneShotOrders = new List<PetsiOrder>();
            PeriodicOrders = new List<PetsiOrder>();
            InitSerializedOrders();
        }

        private void InitSerializedOrders()
        {
            OneShotOrders = fileBehavior.BuildDataListFile<PetsiOrder>(Identifiers.ONE_SHOT_ORDERS);
            PeriodicOrders = fileBehavior.BuildDataListFile<PetsiOrder>(Identifiers.PERIODIC_ORDERS);
            Orders.AddRange(OneShotOrders);
            Orders.AddRange(PeriodicOrders);
        }

        public override void AddData(ModelUnitBase unit)
        {
            PetsiOrder order = (PetsiOrder)unit;
            Orders.Add(order);
            if (order.IsUserEntered)
            {
                if (order.IsPeriodic)
                {
                    PeriodicOrders.Add(order);
                }
                else if (order.IsOneShot)
                {
                    OneShotOrders.Add(order);
                }
            }
        }
        public override FrameBehaviorBase GetFrameBehavior() { return frameBehavior; }
        public List<PetsiOrder> GetOrders() { return Orders; }
        public void SetOrders(List<PetsiOrder> newOrders) { Orders = newOrders; }
        public FileBehavior GetFileBehavior() { return fileBehavior; }
        public override void ClearModel() { Orders.Clear(); }
        public override void AddOrder(ModelUnitBase order) 
        { 
            PetsiOrder o = (PetsiOrder)order;
            Orders.Add(o); SortOrders(); 
            if(o.IsPeriodic)
            {
                PeriodicOrders.Add(o);
                fileBehavior.DataListToFile(Identifiers.PERIODIC_ORDERS, PeriodicOrders);
            }
            else if(o.IsOneShot)
            {
                OneShotOrders.Add(o);
                fileBehavior.DataListToFile(Identifiers.ONE_SHOT_ORDERS, OneShotOrders);
            }
        }
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
                        where order.OrderType == Identifiers.ORDER_TYPE_WHOLESALE
                        select order;
            }
            else
            {
                query = from order in Orders
                        where order.OrderType == Identifiers.ORDER_TYPE_WHOLESALE
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
                        where order.OrderType == Identifiers.ORDER_TYPE_WHOLESALE
                        select order;
            }
            else
            {
                query = from order in Orders
                        where order.OrderType == Identifiers.ORDER_TYPE_WHOLESALE
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

        private void SaveAll()
        {
            SavePeriodicModel();
            SaveOneShotModel();
        }
        private void SavePeriodicModel(){fileBehavior.DataListToFile(Identifiers.PERIODIC_ORDERS, PeriodicOrders); }
        private void SaveOneShotModel() { fileBehavior.DataListToFile(Identifiers.ONE_SHOT_ORDERS, OneShotOrders); }

        private void SaveDeletedOrder(PetsiOrder order) 
        {
            List<PetsiOrder> deletedOrders = fileBehavior.BuildDataListFile<PetsiOrder>(Identifiers.DELETED_ORDERS);
            if(deletedOrders == null) { deletedOrders = new List<PetsiOrder>(); }
            deletedOrders.Add(order);
            fileBehavior.DataListToFile(Identifiers.DELETED_ORDERS, deletedOrders);
        }
        public void RemoveItem(string orderId)
        {
            foreach (PetsiOrder order in Orders)
            {
                if (order.OrderId == orderId)
                {
                    Orders.Remove(order);

                    SaveDeletedOrder(order);

                    if (order.IsOneShot)
                    {
                        OneShotOrders.Remove(order);
                        SaveOneShotModel();
                    }
                    else 
                    { 
                        PeriodicOrders.Remove(order);  
                        SavePeriodicModel();
                    }
                    break;
                }
            }
        }
    }   
}

