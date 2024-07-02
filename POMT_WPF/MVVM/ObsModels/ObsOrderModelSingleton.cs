using Petsi.Interfaces;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.Interfaces;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ObsModels
{
    public class ObsOrderModelSingleton : IOrderModelSubscriber
    {
        private OrderModelPetsi _omp;
        private static ObsOrderModelSingleton _instance;
        public static ObsOrderModelSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ObsOrderModelSingleton();
                }
                return _instance;
            }
        }

        //private ObservableCollection<PetsiOrder> _orders;
        public ObservableCollection<PetsiOrder> Orders { get; set; }
        /*{
            get
            {
                if (_orders == null)
                {
                    _orders = new ObservableCollection<PetsiOrder>();
                }
                return _orders;
            }
            set
            {
                if (_orders != value)
                {
                    _orders = value;
                }
            }
        }*/

        //private ObservableCollection<PetsiOrder> _frozenOrders;
        //public ObservableCollection<PetsiOrder> FrozenOrders;
        /*{
            get
            {
                if (_frozenOrders == null)
                {
                    _frozenOrders = new ObservableCollection<PetsiOrder>();
                }
                return _frozenOrders;
            }
            set
            {
                if (_frozenOrders != value)
                {
                    _frozenOrders = value;
                }
            }
        }*/

        private List<IObsOrderModelSubscriber> _subscriptions;

        private ObsOrderModelSingleton()
        {
            _subscriptions = new List<IObsOrderModelSubscriber>();
            //Orders = new ObservableCollection<PetsiOrder>();
            //FrozenOrders = new ObservableCollection<PetsiOrder>();
            _omp = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            Orders = new ObservableCollection<PetsiOrder>(_omp.GetOrders());
            Orders.CollectionChanged += (s, e) => { UpdateOrderModel(); };

            //_omp.Subscribe(this);
            //UpdateSubscriber();
        }

        private void UpdateOrderModel()
        {
            OrderModelPetsi model = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            model.UpdateModel(Orders);
        }

        private void Notify()
        {
            foreach(var subscriptions in _subscriptions)
            {
                subscriptions.Update();
            }
        }

        public void Subscribe(IObsOrderModelSubscriber subscriber) { _subscriptions.Add(subscriber); }

        public void AddOrder(PetsiOrder orderItem)
        {
            bool isFound = false;

            //Try to modify
            foreach (var order in Orders)
            {
                if (order.OrderId == orderItem.OrderId)
                {
                    isFound = true;
                    int index = Orders.IndexOf(order);
                    Orders[index] = orderItem;
                    break;
                }
            }

            //HANDLE FROZEN, FIX FROZEN ENTIRELY

            //If not modify, add new item
            if (!isFound) { Orders.Add(orderItem); }

            //Orders.Add(orderItem);
            //AddOrderMainModel(orderItem);
            //Notify();
        }
        /*
        public static void ModifyOrder(PetsiOrder modOrder)
        {
            int index = 0;
            bool isfound = false;
            foreach(PetsiOrder order  in Instance.Orders)
            {
                if(order.OrderId == modOrder.OrderId)
                {
                    index = Instance.Orders.IndexOf(order);
                    isfound = true;
                    break;
                }
            }
            if(isfound)
            {
                if(modOrder.IsFrozen)
                {
                    //FreezeOrder(modOrder);
                }
                else
                {
                    Instance.Orders[index] = modOrder;
                }
                
                Instance.ModifyOrderMainModel(modOrder);
                Instance.Notify();
            }
            else
            {
                SystemLogger.Log("ObsOrderModel modifyorder failed: " + modOrder.Recipient + modOrder.OrderId);
            }

        }
        */
        public void RemoveOrder(PetsiOrder orderItem)
        {
            int count = Orders.Count;
            foreach (var item in Orders)
            {
                if (item.OrderId == orderItem.OrderId)
                {
                    Orders.Remove(item);
                    break;
                }
            }
            if (count - 1 != Orders.Count)
            {
                SystemLogger.Log("ObsOrders RemoveItem failure: " + orderItem.Recipient);
            }
            /*
            var orderToRemove = Orders.FirstOrDefault(order => order.OrderId == orderId);
            if (orderToRemove != null)
            {
                int count = Orders.Count;
                Orders.Remove(orderToRemove);
                if(count-1 != Orders.Count)
                {
                    SystemLogger.Log("ObsOrderModel RemoveOrder failed with order: " + orderToRemove.Recipient + " : " + orderToRemove.OrderId);
                }
                
            }
            else
            {
                SystemLogger.Log("ObsOrderModel RemoveOrder could not locat order with id: " + orderId);
            }
            */
            //RemoveOrderMainModel(orderId);
            //Notify();
        }
        public void AddOrderMainModel(PetsiOrder order)
        {
            _omp.AddOrder(order);
        }
        /*
        public void ModifyOrderMainModel(PetsiOrder order)
        {
            _omp.ModifyOrder(order);
        }
        */
        public void RemoveOrderMainModel(string orderId)
        {
            //_omp.RemoveItem(orderId);
            throw new NotImplementedException();
        }
        /*
        public static void UpdateMultiLineMatchEvent()
        {
            CatalogService cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            List<PetsiOrder> copy = new List<PetsiOrder>(Instance.Orders);
            foreach(PetsiOrder order in copy)
            {
                foreach(PetsiOrderLineItem line in order.LineItems)
                {
                    if(line.CatalogObjectId == Identifiers.SOI_MULTI_LINE_EVENT_ID_SIG)
                    {
                        line.CatalogObjectId = cs.GetCatalogObjectId(line.ItemName);
                        if(line.CatalogObjectId == "")
                        {
                            SystemLogger.Log("Update MultiLineMatch Event FAILED: recipient " + order.Recipient + " item: " + line.ItemName);
                        }
                        else
                        {
                            //ModifyOrder(order);
                        }
                    }
                }
            }
        }
        */
        public void UpdateSubscriber()
        {
            Orders.Clear();
            List<PetsiOrder> newOrders = _omp.GetOrders();
            foreach (PetsiOrder order in newOrders)
            {
                Orders.Add(order);
            }
            /*
            FrozenOrders.Clear();
            List<PetsiOrder> frozenOrders = _omp.GetFrozenOrders();
            foreach(PetsiOrder order in frozenOrders)
            {
                FrozenOrders.Add(order);
            }
            */
            Notify();
        }
    }
}
