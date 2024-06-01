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
    }
}
