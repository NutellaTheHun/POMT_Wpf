using Petsi.Units;
using POMT_WPF.Interfaces;
using POMT_WPF.MVVM.ObsModels;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase, IObsOrderModelSubscriber
    {
        private MainWindow view;
        private ObservableCollection<PetsiOrder> _orders;
        public ObservableCollection<PetsiOrder> Orders 
        { 
            get { return _orders; } 
            set
            {
                if(_orders != value)
                {
                    _orders = value;
                    OnPropertyChanged(nameof(Orders));
                }
            }
        }

        private ObservableCollection<PetsiOrder> _frozenOrders;
        public ObservableCollection<PetsiOrder> FrozenOrders
        {
            get { return _frozenOrders; }
            set
            {
                if (_frozenOrders != value)
                {
                    _frozenOrders = value;
                    OnPropertyChanged(nameof(FrozenOrders));
                }
            }
        }

        private int _totalOrderCount;
        public int TotalOrderCount
        {
            get { return _totalOrderCount; }
            set
            {
                _totalOrderCount = value;
                OnPropertyChanged(nameof(TotalOrderCount));
            }
        }

        private string activeFilter;

        public MainWindowViewModel(MainWindow mainWindowView)
        {
            view = mainWindowView;
            ObsOrderModelSingleton.Instance.Subscribe(this);
            Orders = ObsOrderModelSingleton.Instance.Orders;
            //FrozenOrders = ObsOrderModelSingleton.Instance.FrozenOrders;
            TotalOrderCount = Orders.Count();
            this.view = view;
        }

        public void AddOrder(PetsiOrder order)
        {
            Orders.Add(order);
            TotalOrderCount = Orders.Count;
        }

        public void RemoveOrder(PetsiOrder order)
        {
            Orders.Remove(order);
            TotalOrderCount = Orders.Count;
        }

        /// <summary>
        /// filters:
        /// Square -> InputOriginType: SQUARE_ORDER_INPUT
        ///  Wholesale -> orderType: wholesale, IsPeriodic? (isUserEntered)
        /// SpecialOrders(Other) -> IsUserEntered, IsOneShot?      (isUserEntered)
        /// OrderTypes:
        ///     Square
        ///     Wholesale
        ///     Ez-Cater
        ///     SpecialOrder
        /// InputOriginType:
        ///     Square
        ///     UserEntered
        ///     Ez-Cater
        /// </summary>
        /// <param name="filter"></param>
        public void FilterOrderType(string? orderTypefilter)
        {
            activeFilter = orderTypefilter;
            
            if(orderTypefilter == null)
            {
                Orders = ObsOrderModelSingleton.Instance.Orders;
                TotalOrderCount = Orders.Count;
            }
            else
            {
                Orders = new ObservableCollection<PetsiOrder>(ObsOrderModelSingleton.Instance.Orders.Where(x => x.OrderType == orderTypefilter));
                TotalOrderCount = Orders.Count;
            }
        }

        public void FilterSearchBar(string text)
        {
            ObservableCollection<PetsiOrder> modelOrders = ObsOrderModelSingleton.Instance.Orders;
            ObservableCollection<PetsiOrder> results = new ObservableCollection<PetsiOrder>();
            foreach (PetsiOrder order in modelOrders)
            {
                if (order.Recipient.ToLower().Contains(text.ToLower()))
                {
                    results.Add(order);
                    continue;
                }
                foreach (PetsiOrderLineItem lineItem in order.LineItems)
                {
                    if (lineItem.ItemName.ToLower().Contains(text.ToLower()))
                    {
                        results.Add(order);
                        continue;
                    }
                }
            }
            Orders = results;
            TotalOrderCount = Orders.Count;
        }

        public void UpdateOrderList()
        {
            Orders = ObsOrderModelSingleton.Instance.Orders;
            FilterOrderType(activeFilter);
        }

        public void UpdateFrozenOrderList()
        {
            //FrozenOrders = ObsOrderModelSingleton.Instance.FrozenOrders;
        }
        public void Update()
        {
            UpdateOrderList();
            UpdateFrozenOrderList();
            //view.UpdateDataGrid();
        }

        public void SetActiveFilter(string v)
        {
            //activeFilter = v;
        }
    }
}
