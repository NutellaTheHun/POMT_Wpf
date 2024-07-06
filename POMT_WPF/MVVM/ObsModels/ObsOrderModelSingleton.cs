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

        public ObservableCollection<PetsiOrder> Orders { get; set; }

        private List<IObsOrderModelSubscriber> _subscriptions;

        private ObsOrderModelSingleton()
        {
            _subscriptions = new List<IObsOrderModelSubscriber>();
            _omp = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            Orders = new ObservableCollection<PetsiOrder>(_omp.GetOrders());
            Orders.CollectionChanged += (s, e) => { UpdateBackEndOrderModel(); };
        }

        private void UpdateBackEndOrderModel()
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

        /// <summary>
        /// If the order is matched by OrderId, the order is modified, if the id is not matched,
        /// the order is added as a new item.
        /// </summary>
        /// <param name="orderItem"></param>
        public void UpdateOrder(PetsiOrder orderItem)
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

            //If not modify, add new item
            if (!isFound) { Orders.Add(orderItem); }
        }
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
        }

        public void UpdateSubscriber()
        {
            Orders.Clear();
            List<PetsiOrder> newOrders = _omp.GetOrders();
            foreach (PetsiOrder order in newOrders)
            {
                Orders.Add(order);
            }
            Notify();
        }

        public bool ContainsItem(CatalogItemPetsi cItem)
        {
            foreach (var item in Orders)
            {
                foreach(var lineItem in item.LineItems)
                {
                    if(lineItem.CatalogObjectId == cItem.CatalogObjectId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void CheckMultiMatchEvent()
        {
            bool matchFound = false;
            CatalogService cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            List<PetsiOrder> copy = new List<PetsiOrder>(Orders);
            foreach (PetsiOrder order in copy)
            {
                foreach (PetsiOrderLineItem line in order.LineItems)
                {
                    if (line.CatalogObjectId == Identifiers.SOI_MULTI_ITEM_MATCH_EVENT_ID_SIG)
                    {
                        matchFound = true;
                        line.CatalogObjectId = cs.GetCatalogObjectId(line.ItemName);
                        if (line.CatalogObjectId == "")
                        {
                            SystemLogger.Log("Update MultiLineMatch Event FAILED: recipient " + order.Recipient + " item: " + line.ItemName);
                        }
                        else
                        {
                            UpdateOrder(order);
                        }
                    }
                }
            }
            if (matchFound) { UpdateBackEndOrderModel(); }
        }
    }
}
