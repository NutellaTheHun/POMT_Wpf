using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;
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

        private ObsOrderModelSingleton()
        {
            _omp = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            Orders = new ObservableCollection<PetsiOrder>(_omp.GetOrders());
        }

        public static void AddOrder(PetsiOrder order)
        {
            Instance.Orders.Add(order);
            Instance.AddOrderMainModel(order);
        }
        public static void RemoveOrder(string orderId)
        {
            var orderToRemove = Instance.Orders.FirstOrDefault(order => order.OrderId == orderId);
            if (orderToRemove != null)
            {
                Instance.Orders.Remove(orderToRemove);
                Instance.RemoveOrderMainModel(orderId);
            }
        }
        public void AddOrderMainModel(PetsiOrder order)
        {
            _omp.AddOrder(order);
        }
        public void RemoveOrderMainModel(string orderId)
        {
            _omp.RemoveItem(orderId);
        } 
    }
}
