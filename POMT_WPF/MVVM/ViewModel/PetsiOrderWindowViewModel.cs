using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;

namespace POMT_WPF.MVVM.ViewModel
{
    public class PetsiOrderWindowViewModel : ViewModelBase
    {
        private PetsiOrder _order;
        public PetsiOrder Order 
        {
            get { return _order;}
            set
            {
                if (_order != value)
                {
                    _order = value;
                    OnPropertyChanged(nameof(_order));
                }
            }
        }

        private string _VMPickupDate;
        public string VMPickupDate
        {
            get 
            {
                if(_VMPickupDate != null)
                {
                    return _VMPickupDate;
                }
                return ""; 
            }
            set
            {
                if (_VMPickupDate != value)
                {
                    _VMPickupDate = value;
                    OnPropertyChanged(nameof(_VMPickupDate));
                }
            }
        }

        private string _VMPickupTime;
        public string VMPickupTime
        {
            get 
            {
                if (_VMPickupTime != null)
                {
                    return _VMPickupTime;
                }
                return "";
            }
            set
            {
                if (_VMPickupTime != value)
                {
                    _VMPickupTime = value;
                    OnPropertyChanged(nameof(_VMPickupTime));
                }
            }
        }

        private bool _isPeriodic;
        public bool IsPeriodic
        {
            get { return _isPeriodic; }
            set 
            {
                if(_isPeriodic != value)
                {
                    _isPeriodic = value;
                    OnPropertyChanged(nameof(_isPeriodic));
                }
            }
        }

        private bool _isOneTime;
        public bool IsOneTime
        {
            get { return _isOneTime; }
            set 
            { 
                if(_isOneTime != value)
                {
                    _isOneTime = value;
                    OnPropertyChanged(nameof(_isOneTime));
                }
            }
        }


        public PetsiOrderWindowViewModel(PetsiOrder? petsiOrder)
        {
            if (petsiOrder != null)
            {
                Order = petsiOrder;
                VMPickupDate = DateTime.Parse(Order.OrderDueDate).ToShortDateString();
                VMPickupTime = DateTime.Parse(Order.OrderDueDate).ToLocalTime().ToString();
            }
            else
            {
                Order = new PetsiOrder();
                Order.LineItems.Add(new PetsiOrderLineItem());
            }
        }

        public void AddLineItem(PetsiOrderLineItem newLine)
        {
            Order.LineItems.Add(newLine);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fulfillPickup"></param>
        /// <param name="oneTime"></param>
        /// <param name="pickupDay"></param>
        /// <param name="pickupTime"></param>
        public void AddOrder(string pickupTime)
        {
            string Date = DateTime.Parse(VMPickupDate).ToShortDateString();
            Order.OrderDueDate = DateTime.Parse(Date + " " + pickupTime).ToString();
            Order.IsPeriodic = IsPeriodic;
            OrderModelPetsi omp = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            Order.OrderId = Order.InputOriginType+"-"+omp.GenerateOrderId();
            //omp.AddItem(Order);
        }
    }
}
