using Backup.Service;
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
        public OrderModelPetsi(List<PetsiOrder>? testOneshotOrders, List<PetsiOrder>? testPeriodicOrders)
        {
            subscribers = new List<IOrderModelSubscriber>();
            fileBehavior = new FileBehavior("TEST_OrderModel");
            SetModelName(Identifiers.TEST_MODEL_ORDERS);
            ModelManagerSingleton.GetInstance().Register(this);
            EnvironCaptureRegistrySingleton.GetInstance().Register(this);

            if (testOneshotOrders != null && testPeriodicOrders != null)
            {
                Orders = new List<PetsiOrder>(testOneshotOrders);
                Orders.AddRange(testPeriodicOrders);
            }
            else
            {
                Orders = new List<PetsiOrder>();
            }

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

        public async Task<List<PetsiOrderLineItem>> GetBackListDataAsync(DateTime? targetDate, DateTime? endDate, bool isRetail, bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater, bool isFarmer)
        {
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
                select order;
                //Gather periodic orders, fulfilment date of periodic(weekly) orders is only used to get the corresponding day of the week.
                //To get periodic orders, for each day of the date range, get the orders of that day and add to list

                for (DateTime date = targetDate.Value; date <= endDate; date = date.AddDays(1))
                {
                    AccumulatePeriodicOrders(periodicOrders, filteredOrders, date);
                }

                //combine periodic orders with oneshot orders and return
                periodicOrders.AddRange(query.ToList());
                return AggregatePetsiOrders(periodicOrders);
            }
            return AggregatePetsiOrders(query.ToList());
        }

        /// <summary>
        /// Transforms Breakfast Pastry Platter, Coffee Break, mini scone sampler, and muffin sampler to its inner items
        /// </summary>
        /// <param name="targetDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isRetail"></param>
        /// <param name="isSquare"></param>
        /// <param name="isWholesale"></param>
        /// <param name="isSpecial"></param>
        /// <param name="isEzCater"></param>
        /// <param name="isFarmer"></param>
        /// <returns></returns>
        public async Task<List<PetsiOrderLineItem>> GetBackListPastryDataAsync(DateTime? targetDate, DateTime? endDate, bool isRetail, bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater, bool isFarmer)
        {
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
                select order;
                //Gather periodic orders, fulfilment date of periodic(weekly) orders is only used to get the corresponding day of the week.
                //To get periodic orders, for each day of the date range, get the orders of that day and add to list

                for (DateTime date = targetDate.Value; date <= endDate; date = date.AddDays(1))
                {
                    AccumulatePeriodicOrders(periodicOrders, filteredOrders, date);
                }

                //combine periodic orders with oneshot orders and return
                periodicOrders.AddRange(query.ToList());
                return TransformBoxedItemsAndAggregatePetsiOrders(periodicOrders);
            }
            return TransformBoxedItemsAndAggregatePetsiOrders(query.ToList());
        }

        public async Task<List<PetsiOrderLineItem>> GetBackListPieDataAsync(DateTime? targetDate, DateTime? endDate, bool isRetail, bool isSquare, bool isWholesale, bool isSpecial, bool isEzCater, bool isFarmer)
        {
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
                select order;
                //Gather periodic orders, fulfilment date of periodic(weekly) orders is only used to get the corresponding day of the week.
                //To get periodic orders, for each day of the date range, get the orders of that day and add to list

                for (DateTime date = targetDate.Value; date <= endDate; date = date.AddDays(1))
                {
                    AccumulatePeriodicOrders(periodicOrders, filteredOrders, date);
                }

                //combine periodic orders with oneshot orders and return
                periodicOrders.AddRange(query.ToList());
                return AggregateAndFilterBacklistPiePetsiOrders(periodicOrders);
            }
            return AggregateAndFilterBacklistPiePetsiOrders(query.ToList());
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
                        where (order.IsPeriodic == true && DateTime.Parse(order.OrderDueDate).DayOfWeek == targetDate.Value.DayOfWeek)  //wholesale/periodic is weekly, so by day of week
                               ||
                              (order.IsOneShot == true && DateTime.Parse(order.OrderDueDate).Date == targetDate.Value.Date)
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
                        where (order.IsPeriodic == true && DateTime.Parse(order.OrderDueDate).DayOfWeek == targetDate.Value.DayOfWeek)  //wholesale/periodic is weekly, so by day of week
                              ||
                              (order.IsOneShot == true && DateTime.Parse(order.OrderDueDate).Date == targetDate.Value.Date)
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

        /// <summary>
        /// <br>
        ///  - Goes through entire input list and aggregates item quantities into a single item,
        /// </br>
        /// 
        /// <br>
        ///  - Then checks the aggregate dictionary for specific items that rae container items and translates them to their inner items.
        /// </br>
        /// 
        /// <br>
        /// - Then re-aggregates the list of inner items back into the dictionary, (to properly merge orders that share the same items as the container items)
        /// </br>
        ///
        /// <br>
        /// - returns the dictionary values as a list
        /// </br>
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        private List<PetsiOrderLineItem> TransformBoxedItemsAndAggregatePetsiOrders(List<PetsiOrder> orders)
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
                        aggregate[lineItem.CatalogObjectId] = new PetsiOrderLineItem(lineItem);
                    }
                }
            }

            List<PetsiOrderLineItem> list = new List<PetsiOrderLineItem>();
            foreach (PetsiOrderLineItem lineItem in aggregate.Values.ToList())
            {
                // Catering Category
                if(lineItem.CatalogObjectId == Identifiers.BREAKFAST_PASTRY_PLATTER)
                {
                    PetsiOrderLineItem currant = new PetsiOrderLineItem("Buttermilk Currant", Identifiers.CURRANT_SCONE, 0, 0, 0, 0, 0);
                    PetsiOrderLineItem lemon = new PetsiOrderLineItem("Lemon Glaze", Identifiers.LEMON_SCONE, 0, 0, 0, 0, 0);
                    PetsiOrderLineItem triple = new PetsiOrderLineItem("Triple Berry", Identifiers.BERRY_SCONE, 0, 0, 0, 0, 0);
                    PetsiOrderLineItem blueMuffin = new PetsiOrderLineItem("Blueberry Muffin", Identifiers.BLUE_MUFFIN, 0, 0, 0, 0, 0);
                    PetsiOrderLineItem bananaCCMuffin = new PetsiOrderLineItem("Banana Chocolate Chip Muffin", Identifiers.BANANA_MUFFIN, 0, 0, 0, 0, 0);
                    PetsiOrderLineItem donut = new PetsiOrderLineItem("Donut", Identifiers.DONUT, 0, 0, 0, 0, 0);
                    PetsiOrderLineItem coffeeCake = new PetsiOrderLineItem("Coffee Cake", Identifiers.COFFEE_CAKE, 0, 0, 0, 0, 0);
                    if (lineItem.Amount5 > 0)
                    {
                        currant.Amount3 += 2 * lineItem.Amount5;
                        lemon.Amount3 += 2 * lineItem.Amount5;
                        triple.Amount3 += 2 * lineItem.Amount5;
                        blueMuffin.Amount3 += 3 * lineItem.Amount5;
                        bananaCCMuffin.Amount3 += 3 * lineItem.Amount5;
                        donut.AmountRegular += 6 * lineItem.Amount5;
                        coffeeCake.AmountRegular += lineItem.Amount5;
                    }
                    if(lineItem.Amount8 > 0)
                    {
                        currant.Amount3 += 3 * lineItem.Amount8;
                        lemon.Amount3 += 3 * lineItem.Amount8;
                        triple.Amount3 += 3 * lineItem.Amount8;
                        blueMuffin.Amount3 += 4 * lineItem.Amount8;
                        bananaCCMuffin.Amount3 += 4 * lineItem.Amount8;
                        donut.AmountRegular += 8 * lineItem.Amount8;
                        coffeeCake.AmountRegular += lineItem.Amount8;
                    }
                    if(lineItem.Amount10 > 0)
                    {
                        currant.Amount3 += 4 * lineItem.Amount10;
                        lemon.Amount3 += 4 * lineItem.Amount10;
                        triple.Amount3 += 4 * lineItem.Amount10;
                        blueMuffin.Amount3 += 5 * lineItem.Amount10;
                        bananaCCMuffin.Amount3 += 5 * lineItem.Amount10;
                        donut.AmountRegular += 10 * lineItem.Amount10;
                        coffeeCake.AmountRegular += lineItem.Amount10;
                    }
                    list.Add(currant);
                    list.Add(lemon);
                    list.Add(triple);
                    list.Add(blueMuffin);
                    list.Add(bananaCCMuffin);
                    list.Add(donut);
                    list.Add(coffeeCake);
                }
                // Catering Category
                else if (lineItem.CatalogObjectId == Identifiers.COFFEE_BREAK_PLATTER)
                {
                    PetsiOrderLineItem chocolateChip = new PetsiOrderLineItem("Chocolate Chip Cookie", Identifiers.CC_COOKIE, 0, 0, 0, 0, 0);
                    PetsiOrderLineItem snickerdoodle = new PetsiOrderLineItem("Snickerdoodle Cookie", Identifiers.SNICK_COOKIE, 0, 0, 0, 0, 0);
                    PetsiOrderLineItem ginger = new PetsiOrderLineItem("Ginger Molasses Cookie", Identifiers.GING_COOKIE, 0, 0, 0, 0, 0);
                    //PetsiOrderLineItem walnutBrownie = new PetsiOrderLineItem("Walnut Brownie", Identifiers.WALNUT_BROWNIE, 0, 0, 0, 0, 0);
                    //PetsiOrderLineItem raspSwirlBar = new PetsiOrderLineItem("Raspberry Swirl Bar", Identifiers.RASP_SWIRL_BAR, 0, 0, 0, 0, 0);
                    //PetsiOrderLineItem miniTeaCake = new PetsiOrderLineItem("Mini Tea Cake", Identifiers.MINI_TEACAKE, 0, 0, 0, 0, 0);

                    if (lineItem.Amount5 > 0)
                    {
                        chocolateChip.Amount3 += 4 * lineItem.Amount5;
                        snickerdoodle.Amount3 += 4 * lineItem.Amount5;
                        ginger.Amount3 += 4 * lineItem.Amount5;
                        //walnutBrownie.AmountRegular = 3 * lineItem.Amount5;
                        //raspSwirlBar.AmountRegular = 6 * lineItem.Amount5;
                        //miniTeaCake.Amount3 = 6 * lineItem.Amount5;
                    }
                    if (lineItem.Amount8 > 0)
                    {
                        chocolateChip.Amount3 += 6 * lineItem.Amount8;
                        snickerdoodle.Amount3 += 6 * lineItem.Amount8;
                        ginger.Amount3 += 6 * lineItem.Amount8;
                        //walnutBrownie.AmountRegular = 4 * lineItem.Amount8;
                        //raspSwirlBar.AmountRegular = 8 * lineItem.Amount8;
                        //miniTeaCake.Amount3 = 8 * lineItem.Amount8;
                    }
                    if (lineItem.Amount10 > 0)
                    {
                        chocolateChip.Amount3 += 8 * lineItem.Amount10;
                        snickerdoodle.Amount3 += 8 * lineItem.Amount10;
                        ginger.Amount3 += 8 * lineItem.Amount10;
                        //walnutBrownie.AmountRegular = 5 * lineItem.Amount10;
                        //raspSwirlBar.AmountRegular = 10 * lineItem.Amount10;
                        //miniTeaCake.Amount3 = 12 * lineItem.Amount10;
                    }
                    list.Add(chocolateChip);
                    list.Add(snickerdoodle);
                    list.Add(ginger);
                    //list.Add(walnutBrownie);
                    //list.Add(raspSwirlBar);
                    //list.Add(miniTeaCake);
                }
                else if (lineItem.CatalogObjectId == Identifiers.MINI_SCONE_SAMPLER)
                {
                    PetsiOrderLineItem currant = new PetsiOrderLineItem("Buttermilk Currant", Identifiers.CURRANT_SCONE, 0, 0, 0, 0, 0);
                    PetsiOrderLineItem lemon = new PetsiOrderLineItem("Lemon Glaze", Identifiers.LEMON_SCONE, 0, 0, 0, 0, 0);
                    PetsiOrderLineItem triple = new PetsiOrderLineItem("Triple Berry", Identifiers.BERRY_SCONE, 0, 0, 0, 0, 0);
                    // 15 scones size
                    if (lineItem.Amount5 > 0)
                    {
                        currant.Amount3 += 5 * lineItem.Amount5;
                        lemon.Amount3 += 5 * lineItem.Amount5;
                        triple.Amount3 += 5 * lineItem.Amount5;
                    }
                    // 30 scones size
                    if (lineItem.Amount10 > 0)
                    {
                        currant.Amount3 += 10 * lineItem.Amount10;
                        lemon.Amount3 += 10 * lineItem.Amount10;
                        triple.Amount3 += 10 * lineItem.Amount10;
                    }
                    list.Add(currant);
                    list.Add(lemon);
                    list.Add(triple);
                }
                else if (lineItem.CatalogObjectId == Identifiers.MUFFIN_SAMPLER)
                {
                    PetsiOrderLineItem bananaCC = new PetsiOrderLineItem("Banana Chocolate Chip Muffin", Identifiers.BANANA_MUFFIN, 0, 0, 0, 0, 0);
                    PetsiOrderLineItem cornRasp = new PetsiOrderLineItem("Corn Muffin w Raspberry Jam", Identifiers.CORN_RASP_MUFFIN, 0, 0, 0, 0, 0);
                    PetsiOrderLineItem blue = new PetsiOrderLineItem("Blueberry Muffin", Identifiers.BLUE_MUFFIN, 0, 0, 0, 0, 0);

                    bananaCC.AmountRegular += lineItem.AmountRegular * 4;
                    cornRasp.AmountRegular += lineItem.AmountRegular * 4;
                    blue.AmountRegular += lineItem.AmountRegular * 4;

                    list.Add(bananaCC);
                    list.Add(cornRasp);
                    list.Add(blue);
                }
                else
                {
                    list.Add(lineItem);
                }
            }
            Dictionary<string, PetsiOrderLineItem> secondAggregate = new Dictionary<string, PetsiOrderLineItem>();
            foreach (PetsiOrderLineItem lineItem in list)
            {
                if (secondAggregate.ContainsKey(lineItem.CatalogObjectId))
                {
                    secondAggregate[lineItem.CatalogObjectId].Merge(lineItem);
                }
                else
                {
                    secondAggregate[lineItem.CatalogObjectId] = new PetsiOrderLineItem(lineItem);
                }
            }

            return secondAggregate.Values.ToList();
        }

        /// <summary>
        /// Aggregates duplicate line items, and also filters out Take n Bake line items from square orders,
        /// as per buisness logic are to be ignored on the baking list.
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        private List<PetsiOrderLineItem> AggregateAndFilterBacklistPiePetsiOrders(List<PetsiOrder> orders)
        {
            Dictionary<string, PetsiOrderLineItem> aggregate = new Dictionary<string, PetsiOrderLineItem>();

            foreach (PetsiOrder order in orders)
            {
                foreach (PetsiOrderLineItem lineItem in order.LineItems)
                {
                    if(lineItem.ItemName.Contains("Take 'n Bake") || lineItem.ItemName.Contains("Take 'n Thaw"))
                    {
                        if(order.OrderType == Identifiers.ORDER_TYPE_SQUARE)
                        {
                            continue;
                        }
                    }
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

        private void AccumulatePeriodicOrders(List<PetsiOrder> periodicOrders, List<PetsiOrder> FilteredList, DateTime targetDate)
        {
            IEnumerable<PetsiOrder> query;
            query =
              from order in FilteredList
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

