using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        OrderModelPetsi omp;

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
            OrderModelPetsi omp = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            Orders = new ObservableCollection<PetsiOrder>(omp.GetOrders());
        }

        public void AddOrder(PetsiOrder order)
        {
            Orders.Add(order);
            omp.AddItem(order);
        }

        public void RemoveOrder(PetsiOrder order)
        {
            Orders.Remove(order);
            omp.RemoveItem(order);
        }
    }
}
