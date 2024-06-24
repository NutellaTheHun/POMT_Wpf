using DocumentFormat.OpenXml.Wordprocessing;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.Interfaces;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ObsModels
{
    public class ObsOrderModelSingleton
    {
        private static ObsOrderModelSingleton _instance;
        private OrderModelPetsi _omp;
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

        private ObservableCollection<PetsiOrder> _orders;
        public ObservableCollection<PetsiOrder> Orders
        {
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
        }

        private List<IObsOrderModelSubscriber> _subscriptions;

        private ObsOrderModelSingleton()
        {
            _omp = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            Orders = new ObservableCollection<PetsiOrder>(_omp.GetOrders());
            _subscriptions = new List<IObsOrderModelSubscriber>();
        }

        private void Notify()
        {
            foreach(var subscriptions in _subscriptions)
            {
                subscriptions.Update();
            }
        }

        public void Subscribe(IObsOrderModelSubscriber subscriber) { _subscriptions.Add(subscriber); }

        public static void AddOrder(PetsiOrder order)
        {
            Instance.Orders.Add(order);
            Instance.AddOrderMainModel(order);
            Instance.Notify();
        }

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
                Instance.Orders[index] = modOrder;
                Instance.ModifyOrderMainModel(modOrder);
                Instance.Notify();
            }
            else
            {
                SystemLogger.Log("ObsOrderModel modifyorder failed: " + modOrder.Recipient + modOrder.OrderId);
            }

        }
        public static void RemoveOrder(string orderId)
        {
            var orderToRemove = Instance.Orders.FirstOrDefault(order => order.OrderId == orderId);
            if (orderToRemove != null)
            {
                int count = Instance.Orders.Count;
                Instance.Orders.Remove(orderToRemove);
                if(count-1 != Instance.Orders.Count)
                {
                    SystemLogger.Log("ObsOrderModel RemoveOrder failed with order: " + orderToRemove.Recipient + " : " + orderToRemove.OrderId);
                }
                Instance.RemoveOrderMainModel(orderId);
                Instance.Notify();
            }
            else
            {
                SystemLogger.Log("ObsOrderModel RemoveOrder could not locat order with id: " + orderId);
            }
        }
        public void AddOrderMainModel(PetsiOrder order)
        {
            _omp.AddOrder(order);
        }
        public void ModifyOrderMainModel(PetsiOrder order)
        {
            _omp.ModifyOrder(order);
        }
        public void RemoveOrderMainModel(string orderId)
        {
            _omp.RemoveItem(orderId);
        }

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
                            ModifyOrder(order);
                        }
                    }
                }
            }
        }
    }
}
