using Newtonsoft.Json;
using Petsi.Filing;
using Petsi.Input;
using Petsi.Interfaces;
using Petsi.Managers;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using System.Collections.ObjectModel;

namespace Petsi.Models
{
    public class OrderModelPetsi : ModelBase, IModelInput, IOrderModelPublisher, IStartupSubscriber
    {
        List<PetsiOrder> Orders;

        List<IOrderModelSubscriber> subscribers;
        HashSet<string> OrderTypesSet;
        FileBehavior fileBehavior;

        private bool oneShotStartupRecieved;
        private bool periodicStartupRecieved;

        public OrderModelPetsi()
        {
            subscribers = new List<IOrderModelSubscriber>();
            fileBehavior = new FileBehavior("OrderModel");
            SetModelName(Identifiers.MODEL_ORDERS);
            ModelManagerSingleton.GetInstance().Register(this);
            EnvironCaptureRegistrySingleton.GetInstance().Register(this);
            Orders = new List<PetsiOrder>();
            InitSerializedOrders();
            OrderTypesSet = InitOrderTypes();

            StartupService.Instance.Register(this);
        }

        /// <summary>
        /// For Testing environments only
        /// </summary>
        /// <param name="serializedSquareOrders"></param>
        public OrderModelPetsi(List<PetsiOrder> testOneshotOrders, List<PetsiOrder> testPeriodicOrders)
        {
            subscribers = new List<IOrderModelSubscriber>();
            fileBehavior = new FileBehavior("TEST_OrderModel");
            SetModelName(Identifiers.TEST_MODEL_ORDERS);
            ModelManagerSingleton.GetInstance().Register(this);
            EnvironCaptureRegistrySingleton.GetInstance().Register(this);

            Orders = new List<PetsiOrder>(testOneshotOrders);
            Orders.AddRange(testPeriodicOrders);

            OrderTypesSet = InitOrderTypes();
            StartupService.Instance.Register(this);
        }

        /// <summary>
        /// For Testing environments only, for use with InputGenerator objects
        /// </summary>
        /// <param name="serializedSquareOrders"></param>
        public OrderModelPetsi(List<PetsiOrder> generatedOrders)
        {
            subscribers = new List<IOrderModelSubscriber>();
            fileBehavior = new FileBehavior("TEST_OrderModel");
            SetModelName(Identifiers.TEST_MODEL_ORDERS);
            ModelManagerSingleton.GetInstance().Register(this);
            EnvironCaptureRegistrySingleton.GetInstance().Register(this);

            Orders = new List<PetsiOrder>(generatedOrders);

            OrderTypesSet = InitOrderTypes();
            StartupService.Instance.Register(this);
        }

        public void Notify()
        {
            foreach (IOrderModelSubscriber subscriber in subscribers)
            {
                subscriber.UpdateSubscriber();
            }
        }
        public async Task RefreshOrderModelAsync()
        {   
            Orders.Clear();
            InitSerializedOrders();
            SquareOrderInput soi = (SquareOrderInput)InputManagerSingleton.GetInstance().GetInputComponent(Identifiers.SQUARE_ORDER_INPUT);
            await soi.Execute();
            Notify();
        }
        public void Subscribe(IOrderModelSubscriber subscription)
        {
            subscribers.Add(subscription);
        }
        public void UpdateModel(ObservableCollection<PetsiOrder> orders)
        {
            Orders = orders.ToList();
            SaveAll();
            Notify();
        }

        private void SaveAll()
        {
            List<PetsiOrder> PeriodicOrders = new List<PetsiOrder>();
            List<PetsiOrder> OneShotOrders = new List<PetsiOrder>();
            foreach (var order in Orders)
            {
                if (order.IsUserEntered)
                {
                    if (order.IsPeriodic) PeriodicOrders.Add(order);
                    else if (order.IsOneShot) OneShotOrders.Add(order);
                }
            }
            fileBehavior.DataListToFile(Identifiers.PERIODIC_ORDERS, PeriodicOrders);
            fileBehavior.DataListToFile(Identifiers.ONE_SHOT_ORDERS, OneShotOrders);

            SaveBackup(PeriodicOrders, OneShotOrders);
            SaveTestFileAllOrders();
        }

        private void SaveTestFileAllOrders()
        {
            List<PetsiOrder> PeriodicOrders = new List<PetsiOrder>();
            List<PetsiOrder> OneShotOrders = new List<PetsiOrder>();
            foreach (var order in Orders)
            {
                if (order.IsPeriodic) 
                {
                    PeriodicOrders.Add(order);
                } 
                else 
                { 
                    OneShotOrders.Add(order);
                }
            }
            fileBehavior.DataListToFile(Identifiers.TEST_ONESHOT_ORDERS, PeriodicOrders);
            fileBehavior.DataListToFile(Identifiers.TEST_PERIODIC_ORDERS, OneShotOrders);
        }

        private void SaveBackup(List<PetsiOrder> PeriodicOrders, List<PetsiOrder> OneShotOrders)
        {
            string backupFp = null;
            backupFp = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_BACKUP_PATH);
            if (backupFp != null && backupFp != "")
            {
                File.WriteAllText(backupFp + "\\" + Identifiers.PERIODIC_ORDERS, JsonConvert.SerializeObject(PeriodicOrders));
                File.WriteAllText(backupFp + "\\" + Identifiers.ONE_SHOT_ORDERS, JsonConvert.SerializeObject(OneShotOrders));
            }
        }

        private HashSet<string>? InitOrderTypes()
        {
            List<string> filedList = fileBehavior.BuildDataListFile<string>("OrderTypeSet");
            if (filedList == null)
            {
                filedList = new List<string> { Identifiers.ORDER_TYPE_RETAIL, Identifiers.ORDER_TYPE_EZ_CATER,
                    Identifiers.ORDER_TYPE_SPECIAL, Identifiers.ORDER_TYPE_SQUARE, Identifiers.ORDER_TYPE_WHOLESALE };
                fileBehavior.DataListToFile("OrderTypeSet", filedList);
            }
            HashSet<string> result = new HashSet<string>(filedList);
            
            foreach (PetsiOrder o in Orders)
            {
                result.Add(o.OrderType);
            }
            result.Add(Identifiers.ORDER_TYPE_FARMERS);
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
            List<PetsiOrder> OneShotOrders = fileBehavior.BuildDataListFile<PetsiOrder>(Identifiers.ONE_SHOT_ORDERS);
            List<PetsiOrder> PeriodicOrders = fileBehavior.BuildDataListFile<PetsiOrder>(Identifiers.PERIODIC_ORDERS);
            if (OneShotOrders != null)
            {
                Orders.AddRange(OneShotOrders);
            }
            if (PeriodicOrders != null)
            {
                Orders.AddRange(PeriodicOrders);
            }
        }

        public override void AddData(ModelUnitBase unit)
        {
            Orders.Add((PetsiOrder)unit);
            OrderTypesSet.Add(((PetsiOrder)unit).OrderType);
        }
        public List<PetsiOrder> GetOrders() { return Orders; }
        public void SetOrders(List<PetsiOrder> newOrders) { Orders = newOrders; }
        public FileBehavior GetFileBehavior() { return fileBehavior; }
        public override void ClearModel() { Orders.Clear(); }

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

        #region Report Pulls

        public async Task<List<PetsiOrder>> GetFrontListDataAsync(DateTime? targetDate, bool isRetail, bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater, bool isFarmer)
        {
            await RefreshOrderModelAsync();
            List<PetsiOrder> filteredOrders = FilterOrders(Orders, isRetail, isSquare, isWholesale, isSpecial, isEzCater, isFarmer);

            IEnumerable<PetsiOrder> query;
            if (targetDate != null)
            {//doesn't filter out wholesale because they're used for Coverpage and notes page, order data is filtered out in report builders
                query =
                from order in filteredOrders
                where (order.IsPeriodic == true && DateTime.Parse(order.OrderDueDate).DayOfWeek == targetDate.Value.DayOfWeek)  //wholesale/periodic is weekly, so by day of week
                      ||
                     (order.IsPeriodic == false && DateTime.Parse(order.OrderDueDate).ToShortDateString() == targetDate.Value.ToShortDateString())
                orderby order.FulfillmentType, DateTime.Parse(order.OrderDueDate).TimeOfDay
                select order;
            }
            else
            {
                query =
                from order in filteredOrders
                orderby order.FulfillmentType, DateTime.Parse(order.OrderDueDate).TimeOfDay
                select order;
            }
            return query.ToList();
        }

        public async Task<List<PetsiOrderLineItem>> GetBackListData(DateTime? targetDate, DateTime? endDate, bool isRetail, bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater, bool isFarmer)
        {
            if(ModelName == Identifiers.MODEL_ORDERS)
            {
                await RefreshOrderModelAsync();
            }
            
            List<PetsiOrder> filteredOrders = FilterOrders(Orders, isRetail, isSquare, isWholesale, isSpecial, isEzCater, isFarmer);

            IEnumerable<PetsiOrder> query;
            List<PetsiOrder> periodicOrders = new List<PetsiOrder>();
            if (targetDate == null) //all data, WARNING WHOLESALE ONLY ONCE CURRENTLY, add ws orders up to max day, or end of week of max day?
            {
                query =
                from order in filteredOrders
                select order;
            }
            else if (endDate == null) //target date
            {
                query =
                from order in filteredOrders
                where (order.IsPeriodic == true && DateTime.Parse(order.OrderDueDate).DayOfWeek == targetDate.Value.DayOfWeek)  //wholesale/periodic is weekly, so by day of week
                       ||
                      (order.IsPeriodic == false && DateTime.Parse(order.OrderDueDate).Date == targetDate.Value.Date)
                select order;
            }
            else //range
            {
                //Gather Non-periodic Orders

                query =
                from order in filteredOrders
                where
                    (order.IsOneShot == true
                        && DateTime.Parse(order.OrderDueDate).Date >= targetDate.Value.Date
                        && DateTime.Parse(order.OrderDueDate).Date <= endDate.Value.Date)
                    ||
                    (order.IsPeriodic 
                        && 
                        (DateTime.Parse(order.OrderDueDate).DayOfWeek >= targetDate.Value.DayOfWeek
                        &&
                        DateTime.Parse(order.OrderDueDate).DayOfWeek <= endDate.Value.DayOfWeek
                        )
                    )
                select order;

                //Gather periodic orders, fulfilment date of periodic(weekly) orders is only used to get the corresponding day of the week.
                //To get periodic orders, for each day of the date range, get the orders of that day and add to list
                /*
                for (DateTime date = targetDate.Value; date <= endDate; date = date.AddDays(1))
                {
                    AccumulatePeriodicOrders(periodicOrders, date);
                }*/

                //combine periodic orders with oneshot orders and return
                periodicOrders.AddRange(query.ToList());
                return AggregatePetsiOrders(periodicOrders);
            }
            return AggregatePetsiOrders(query.ToList());
        }

        //Label Service uses it, WS_Day_report still needs day separation tho, and day info
        public List<PetsiOrderLineItem> GetWsDayData(DateTime? targetDate)
        {
            List<PetsiOrder> filteredOrders = FilterOrders(Orders, false, false, true, false, false, false);
            IEnumerable<PetsiOrder> query;
            if (targetDate == null)
            {
                query = from order in filteredOrders
                        where order.OrderType == Identifiers.ORDER_TYPE_WHOLESALE
                        select order;
            }
            else
            {
                query = from order in filteredOrders
                        where order.OrderType == Identifiers.ORDER_TYPE_WHOLESALE
                        where DateTime.Parse(order.OrderDueDate).DayOfWeek == targetDate.Value.DayOfWeek
                        select order;
            }
            return AggregatePetsiOrders(query.ToList()).OrderBy(item => item.ItemName).ToList();
        }

        public List<PetsiOrder> GetWsDayNameData(DateTime? targetDate)
        {
            List<PetsiOrder> filteredOrders = FilterOrders(Orders, false, false, true, false, false, false);
            IEnumerable<PetsiOrder> query;
            if (targetDate == null)
            {
                query = from order in filteredOrders
                        where order.OrderType == Identifiers.ORDER_TYPE_WHOLESALE
                        select order;
            }
            else
            {
                query = from order in filteredOrders
                        where order.OrderType == Identifiers.ORDER_TYPE_WHOLESALE
                        where DateTime.Parse(order.OrderDueDate).DayOfWeek == targetDate.Value.DayOfWeek
                        select order;
            }

            List<PetsiOrder> result = query.ToList();

            foreach (PetsiOrder order in result)
            {
                order.LineItems = order.LineItems.OrderBy(line => line.ItemName).ToList();
            }
            return result;
        }
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

        private void AccumulatePeriodicOrders(List<PetsiOrder> periodicOrders, DateTime targetDate)
        {
            IEnumerable<PetsiOrder> query;
            query =
              from order in Orders
              where (order.IsPeriodic == true && DateTime.Parse(order.OrderDueDate).DayOfWeek == targetDate.DayOfWeek)
              select order;
            periodicOrders.AddRange(query.ToList());
        }

        /// <summary>
        /// Filters orders based on given booleans representing order types, and discounting frozen orders
        /// </summary>
        /// <param name="petsiOrders"></param>
        /// <param name="isRetail"></param>
        /// <param name="isSquare"></param>
        /// <param name="isWholesale"></param>
        /// <param name="isSpecial"></param>
        /// <param name="isEzCater"></param>
        /// <returns></returns>
        private List<PetsiOrder> FilterOrders(List<PetsiOrder> petsiOrders, bool isRetail, bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater, bool isFarmer)
        {
            List<PetsiOrder> result = new List<PetsiOrder>();
            foreach (PetsiOrder order in petsiOrders)
            {
                if (order.IsFrozen) { continue; }

                if (isRetail) {
                    if (order.OrderType == Identifiers.ORDER_TYPE_RETAIL)
                    { 
                        result.Add(order); 
                        continue; 
                    }
                }
                if (isSquare)
                {
                    if (order.OrderType == Identifiers.ORDER_TYPE_SQUARE)
                    { 
                        result.Add(order); 
                        continue;
                    } 
                }
                if (isWholesale)
                {
                    if (order.OrderType == Identifiers.ORDER_TYPE_WHOLESALE)
                    {   result.Add(order);
                        continue;
                    } 
                }
                if (isSpecial) 
                { if (order.OrderType == Identifiers.ORDER_TYPE_SPECIAL)
                    {
                        result.Add(order);
                        continue;
                    }
                }
                if (isEzCater) 
                { if (order.OrderType == Identifiers.ORDER_TYPE_EZ_CATER)
                    {
                        result.Add(order);
                        continue;
                    }
                }
                if (isFarmer)
                {
                    if (order.OrderType == Identifiers.ORDER_TYPE_FARMERS)
                    {
                        result.Add(order);
                        continue;
                    }
                }
            }
            return result;
        }
        #endregion

        public override void CaptureEnvironment(FileBehavior reportFb)
        {
            //reportFb.DataListToFile(Identifiers.ENV_OMP, Orders);
            reportFb.DataListToPureFilePath(Identifiers.ENV_OMP, Orders);
        }

        private void SaveDeletedOrder(PetsiOrder order)
        {
            List<PetsiOrder> deletedOrders = fileBehavior.BuildDataListFile<PetsiOrder>(Identifiers.DELETED_ORDERS);
            if (deletedOrders == null) { deletedOrders = new List<PetsiOrder>(); }
            deletedOrders.Add(order);
            fileBehavior.DataListToFile(Identifiers.DELETED_ORDERS, deletedOrders);
        }

        public override void AddItem(ModelUnitBase order)
        {
            Orders.Add((PetsiOrder)order); 
            SortOrders();
            Notify();
        }

        public void RemoveItem(PetsiOrder targetOrder)
        {
            foreach (PetsiOrder order in Orders)
            {
                if (order.OrderId == targetOrder.OrderId)
                {
                    Orders.Remove(order);
                    Notify();
                    return;
                }
            }
        }

        public void LoadStartupFiles(List<(string fileName, string filePath)> FileList)
        {
            if (FileList == null || FileList.Count == 0) { return; }
            foreach (var fileListing in FileList)
            {
                if (fileListing.fileName == Identifiers.PERIODIC_ORDERS)
                {
                    StartupLoadPeriodicOrders(fileListing.filePath);
                    fileBehavior.DataListToFile(Identifiers.PERIODIC_ORDERS, Orders);
                    periodicStartupRecieved = true;
                }
                if (fileListing.fileName == Identifiers.ONE_SHOT_ORDERS)
                {
                    StartupLoadOneShotOrders(fileListing.filePath);
                    fileBehavior.DataListToFile(Identifiers.ONE_SHOT_ORDERS, Orders);
                    oneShotStartupRecieved = true;
                }
            }
            if (oneShotStartupRecieved && periodicStartupRecieved) { StartupService.Instance.Deregister(this); }
        }
        private void StartupLoadPeriodicOrders(string filePath)
        {
            string input;
            if (File.Exists(filePath))
            {
                input = File.ReadAllText(filePath);
                List<PetsiOrder> items = JsonConvert.DeserializeObject<List<PetsiOrder>>(input);
                Orders.AddRange(items);
            }
        }
        private void StartupLoadOneShotOrders(string filePath)
        {
            string input;
            if (File.Exists(filePath))
            {
                input = File.ReadAllText(filePath);
                List<PetsiOrder> items = JsonConvert.DeserializeObject<List<PetsiOrder>>(input);
                Orders.AddRange(items);
            }
        }
    }
}

