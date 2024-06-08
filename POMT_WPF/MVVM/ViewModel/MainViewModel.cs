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
        ///     SpecialOrder
        /// InputOriginType:
        ///     Square
        ///     UserEntered
        ///     Ez-Cater
        /// </summary>
        /// <param name="filter"></param>
        public void GetOrders(string? filter)
        {
            if(filter == null)
            {
                Orders = ObsOrderModelSingleton.Instance.Orders;
            }
            else
            {
                Orders = new ObservableCollection<PetsiOrder>(ObsOrderModelSingleton.Instance.Orders.Where(x => x.InputOriginType == filter));
            }
        }
    }
}
