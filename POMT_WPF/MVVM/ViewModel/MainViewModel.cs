using Petsi.Units;
using POMT_WPF.MVVM.ObsModels;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {

        private ObservableCollection<PetsiOrder> _orders;
        public ObservableCollection<PetsiOrder> Orders 
        { 
            get { return _orders; } 
            set
            {
                if(_orders != value)
                {
                    _orders = value;
                    OnPropertyChanged(nameof(_orders));
                }
            }
        }
        public MainWindowViewModel()
        {
            Orders = ObsOrderModelSingleton.Instance.Orders;
        }

        public void AddOrder(PetsiOrder order)
        {
            Orders.Add(order);
        }

        public void RemoveOrder(PetsiOrder order)
        {
            Orders.Remove(order);
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
            if(orderTypefilter == null)
            {
                Orders = ObsOrderModelSingleton.Instance.Orders;
            }
            else
            {
                Orders = new ObservableCollection<PetsiOrder>(ObsOrderModelSingleton.Instance.Orders.Where(x => x.OrderType == orderTypefilter));
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
        }
    }
}
