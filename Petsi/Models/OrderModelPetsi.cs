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
        HashSet<string> OrderTypesSet;
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
            OrderTypesSet = InitOrderTypes();
        }

        private HashSet<string>? InitOrderTypes()
        {
            List<string> filedList = fileBehavior.BuildDataListFile<string>("OrderTypeSet");
            if(filedList == null)
            {
                filedList = new List<string> { Identifiers.ORDER_TYPE_DAILY_BAKE, Identifiers.ORDER_TYPE_EZ_CATER,
                    Identifiers.ORDER_TYPE_SPECIAL, Identifiers.ORDER_TYPE_SQUARE, Identifiers.ORDER_TYPE_WHOLESALE };
            }
            HashSet<string> result = new HashSet<string>(filedList);
            foreach (PetsiOrder o in Orders)
            {
                result.Add(o.OrderType);
            }
            return result;
        }

        public List<string> GetOrderTypes() { return OrderTypesSet.ToList(); }

        public void AddOrderType(string orderType)
        {
            if (OrderTypesSet.Add(orderType))
            {
                SerializeOrderTypeSet();
            }
        }

        private void SerializeOrderTypeSet()
        {
            fileBehavior.DataListToFile("OrderTypeSet", OrderTypesSet.ToList());
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
            OrderTypesSet.Add(order.OrderType);
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
            //List<PetsiOrder> orderCpy = new List<PetsiOrder>(orders);
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
                        aggregate[lineItem.CatalogObjectId] = new PetsiOrderLineItem(lineItem);
                    }
                }

            }
            return aggregate.Values.ToList();
        }
        public List<PetsiOrder> GetFrontListData(DateTime? targetDate)
        {
            IEnumerable<PetsiOrder> query;
            if (targetDate != null)
            {//doesn't filter out wholesale because they're used for Coverpage and notes page, order data is filtered out in report builders
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
            List<PetsiOrder> periodicOrders = new List<PetsiOrder>();
            if (targetDate == null) //all data, WARNING WHOLESALE ONLY ONCE CURRENTLY, add ws orders up to max day, or end of week of max day?
            {
              query =
              from order in Orders
              select order;
            }
            else if (endDate == null) //target date
            {
               query =
               from order in Orders
               where (order.IsPeriodic == true && DateTime.Parse(order.OrderDueDate).DayOfWeek == targetDate.Value.DayOfWeek)  //wholesale/periodic is weekly, so by day of week
                      ||
                     (order.IsPeriodic == false && DateTime.Parse(order.OrderDueDate).Date == targetDate.Value.Date)
               select order;
            }
            else //range
            {
                //Gather Non-periodic Orders
                
                 query =
                 from order in Orders
                 where 
                     order.IsOneShot == true
                     && DateTime.Parse(order.OrderDueDate) >= targetDate.Value
                     && DateTime.Parse(order.OrderDueDate) <= endDate.Value
                 select order;
                
                //Gather periodic orders, fulfilment date of periodic(weekly) orders is only used to get the corresponding day of the week.
                //To get periodic orders, for each day of the date range, get the orders of that day and add to list
                for (DateTime date = targetDate.Value; date <= endDate; date = date.AddDays(1))
                {
                    AccumulatePeriodicOrders(periodicOrders, date);
                }

                //combine periodic orders with oneshot orders and return
                periodicOrders.AddRange(query.ToList());
                return AggregatePetsiOrders(periodicOrders);
            }
            return AggregatePetsiOrders(query.ToList()); 
        }

        private void AccumulatePeriodicOrders(List<PetsiOrder> periodicOrders, DateTime targetDate)
        {
            IEnumerable<PetsiOrder> query;
            query =
              from order in Orders
              where (order.IsPeriodic == true && DateTime.Parse(order.OrderDueDate).DayOfWeek == targetDate.DayOfWeek)  //wholesale/periodic is weekly, so by day of week
              select order;
            periodicOrders.AddRange(query.ToList());
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
                        where DateTime.Parse(order.OrderDueDate).DayOfWeek == targetDate.Value.DayOfWeek
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
                        where DateTime.Parse(order.OrderDueDate).DayOfWeek == targetDate.Value.DayOfWeek
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

        public void ModifyOrder(PetsiOrder modOrder)
        {
            int index = 0;
            foreach (PetsiOrder order in Orders)
            {
                if (order.OrderId == modOrder.OrderId)
                {
                    index = Orders.IndexOf(order);
                    break;
                }
            }
            Orders[index] = modOrder;
            if (modOrder.IsPeriodic)
            {
                UpdatePeriodicOrders(modOrder);
                if(HandleOneShotOrdersContains(modOrder))
                {
                    UpdateOneShotOrders(null);
                }
            }
            else if (modOrder.IsOneShot)
            {
                UpdateOneShotOrders(modOrder);
                if(HandlePeriodicOrdersContains(modOrder))
                {
                    UpdatePeriodicOrders(null);
                }
            }
        }
        /// <summary>
        /// If modOrder is found in the OneShotOrders list, it is removed.
        /// Neccessary for the case that a order is changed from periodic to oneshot.
        /// </summary>
        /// <param name="modOrder"></param>
        /// <returns></returns>
        private bool HandleOneShotOrdersContains(PetsiOrder modOrder)
        {
            foreach (PetsiOrder order in OneShotOrders)
            {
                if (order.OrderId == modOrder.OrderId)
                {
                    OneShotOrders.Remove(order);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// If modOrder is found in the PeriodicOrders list, it is removed.
        /// Neccessary for the case that a order is changed from oneshot to periodic.
        /// </summary>
        /// <param name="modOrder"></param>
        /// <returns></returns>
        private bool HandlePeriodicOrdersContains(PetsiOrder modOrder)
        {
            foreach (PetsiOrder order in PeriodicOrders)
            {
                if (order.OrderId == modOrder.OrderId)
                {
                    PeriodicOrders.Remove(order);
                    return true;
                }
            }
            return false;
        }

        private void UpdateOneShotOrders(PetsiOrder? modOrder)
        {
            if(modOrder != null)
            {
                int index = 0;
                foreach (PetsiOrder order in OneShotOrders)
                {
                    if (order.OrderId == modOrder.OrderId)
                    {
                        index = OneShotOrders.IndexOf(order);
                        break;
                    }
                }
                OneShotOrders[index] = modOrder;
            }

            fileBehavior.DataListToFile(Identifiers.ONE_SHOT_ORDERS, OneShotOrders);   
        }

        private void UpdatePeriodicOrders(PetsiOrder? modOrder)
        {
            if (modOrder != null)
            {
                int index = 0;
                foreach (PetsiOrder order in PeriodicOrders)
                {
                    if (order.OrderId == modOrder.OrderId)
                    {
                        index = PeriodicOrders.IndexOf(order);
                        break;
                    }
                }
                PeriodicOrders[index] = modOrder;
            }

            fileBehavior.DataListToFile(Identifiers.PERIODIC_ORDERS, PeriodicOrders);
        }
    }   
}

