using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;
using Square.Models;

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

        public string PickupDate
        {
            get { return DateTime.Parse(Order.OrderDueDate).ToShortDateString(); }
            set
            {
                DateTime test;
                if (DateTime.TryParse(value, out test))
                {
                    Order.OrderDueDate = value;
                    OnPropertyChanged(nameof(Order.OrderDueDate));
                }
            }
        }
        public string PickupTime
        {
            get { return DateTime.Parse(Order.OrderDueDate).ToLocalTime().ToString(); }
            set
            {
                DateTime test;
                if (DateTime.TryParse(value, out test))
                {
                    if(test.ToLocalTime() != default)
                    Order.OrderDueDate = value;
                    OnPropertyChanged(nameof(Order.OrderDueDate));
                }
            }
        }

        public PetsiOrderWindowViewModel(PetsiOrder? petsiOrder)
        {
            if (petsiOrder != null)
            {
                Order = petsiOrder;
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
        public void AddOrder(bool fulfillPickup, bool oneTime, string pickupDay, string pickupTime)
        {
            Order.FulfillmentType = fulfillPickup ? "Pickup" : "Delivery";
            Order.OrderDueDate = pickupDay + " " + pickupTime;
            OrderModelPetsi omp = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            Order.OrderId = Order.InputOriginType+"-"+omp.GenerateOrderId();
            omp.AddItem(Order);
        }
    }
}
