using Petsi.Managers;
using Petsi.Models;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using POMT_WPF.MVVM.ObsModels;
using System.Collections.ObjectModel;

namespace POMT_WPF.MVVM.ViewModel
{
    public class PetsiOrderWindowViewModel : ViewModelBase
    {

        #region props

        private PetsiOrder _order;
        CatalogService cs;
        public string Recipient
        {
            get => _order.Recipient;
            set
            {
                if (_order.Recipient != value)
                {
                    _order.Recipient = value;
                    OnPropertyChanged(Recipient);
                }
            }
        }
        public string FulfillmentType
        {
            get => _order.FulfillmentType;
            set
            {
                if (_order.FulfillmentType != value)
                {
                    _order.FulfillmentType = value;
                    OnPropertyChanged(FulfillmentType);
                }
            }
        }
        public string DeliveryAddress
        {
            get => _order.DeliveryAddress;
            set
            {
                if (_order.DeliveryAddress != value)
                {
                    _order.DeliveryAddress = value;
                    OnPropertyChanged(DeliveryAddress);
                }
            }
        }
        public string OrderType
        {
            get => _order.OrderType;
            set
            {
                if (_order.OrderType != value)
                {
                    _order.OrderType = value;
                    OnPropertyChanged(OrderType);
                }
            }
        }
        public string InputOriginType
        {
            get => _order.InputOriginType;
            set
            {
                if (_order.InputOriginType != value)
                {
                    _order.InputOriginType = value;
                    OnPropertyChanged(InputOriginType);
                }
            }
        }
        public bool IsPeriodic
        {
            get => _order.IsPeriodic;
            set
            {
                if (_order.IsPeriodic != value)
                {
                    _order.IsPeriodic = value;
                    OnPropertyChanged(nameof(IsPeriodic));
                }
            }
        }
        public bool IsOneShot
        {
            get => _order.IsOneShot;
            set
            {
                if (_order.IsOneShot != value)
                {
                    _order.IsOneShot = value;
                    OnPropertyChanged(nameof(IsOneShot));
                }
            }
        }
        public string OrderDueDate
        {
            get => _order.OrderDueDate;
            set
            {
                if (_order.OrderDueDate != value)
                {
                    _order.OrderDueDate = value;
                    OnPropertyChanged(nameof(OrderDueDate));
                }
            }
        }
        public string OrderId
        {
            get => _order.OrderId;
            set
            {
                if (_order.OrderId != value)
                {
                    _order.OrderId = value;
                    OnPropertyChanged(nameof(OrderId));
                }
            }
        }
        public string PhoneNumber
        {
            get => _order.PhoneNumber;
            set
            {
                if (_order.PhoneNumber != value)
                {
                    _order.PhoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }
        public string Email
        {
            get => _order.Email;
            set
            {
                if (_order.Email != value)
                {
                    _order.Email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        public string Note
        {
            get => _order.Note;
            set
            {
                if (_order.Note != value)
                {
                    _order.Note = value;
                    OnPropertyChanged(nameof(Note));
                }
            }
        }

        public ObservableCollection<PetsiOrderLineItem> LineItems { get; set; }

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
                    OnPropertyChanged(nameof(VMPickupDate));
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
                    OnPropertyChanged(nameof(VMPickupTime));
                }
            }
        }

        private string _VMOrderType;
        public string VMOrderType
        {
            get
            {
                if (_VMOrderType != null)
                {
                    return _VMOrderType;
                }
                return "";
            }
            set
            {
                if (_VMOrderType != value)
                {
                    _VMOrderType = value;
                    OnPropertyChanged(nameof(VMOrderType));
                }
            }
        }

        private bool _isReadOnly;

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                if (_isReadOnly != value)
                {
                    _isReadOnly = value;
                    OnPropertyChanged(nameof(IsReadOnly));
                }
            }
        }

        private bool _notReadOnly;
        public bool NotReadOnly
        {
            get { return _notReadOnly; }
            set
            {
                if (_notReadOnly != value)
                {
                    _notReadOnly = value;
                    OnPropertyChanged(nameof(NotReadOnly));
                }
            }
        }

        public List<string> OrderTypes;

        #endregion

        public PetsiOrderWindowViewModel(PetsiOrder? petsiOrder)
        {
            cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            if (petsiOrder != null)
            {
                _order = petsiOrder;
                VMPickupDate = DateTime.Parse(_order.OrderDueDate).ToShortDateString();
                VMPickupTime = DateTime.Parse(_order.OrderDueDate).ToShortTimeString();
                LineItems = new ObservableCollection<PetsiOrderLineItem>(_order.LineItems);
                LineItems.CollectionChanged += (s, e) => _order.LineItems = LineItems.ToList();
            }
            else
            {
                _order = new PetsiOrder();
                LineItems = new ObservableCollection<PetsiOrderLineItem>(_order.LineItems);
                LineItems.CollectionChanged += (s, e) => _order.LineItems = LineItems.ToList();
                LineItems.Add(new PetsiOrderLineItem());
            }
            OrderModelPetsi omp = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            OrderTypes = omp.GetOrderTypes();
        }

        public void AddLineItem(PetsiOrderLineItem newLine)
        {
            LineItems.Add(newLine);
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
            _order.OrderDueDate = DateTime.Parse(Date + " " + pickupTime).ToString();
            _order.InputOriginType = Identifiers.USER_ENTERED_INPUT;
            _order.IsUserEntered = true;
            OrderModelPetsi omp = (OrderModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS);
            _order.OrderId = omp.GenerateOrderId();
            ObsOrderModelSingleton.AddOrder(_order);
        }

        public void ModifyOrder(string pickupTime)
        {
            string Date = DateTime.Parse(VMPickupDate).ToShortDateString();
            _order.OrderDueDate = DateTime.Parse(Date + " " + pickupTime).ToString();
            ObsOrderModelSingleton.ModifyOrder(_order);
        }

        public PetsiOrder GetOrder() { return _order; }

        public bool IsValidLineItems()
        {
            
            foreach (PetsiOrderLineItem lineItem in LineItems)
            {
                string id = cs.GetCatalogObjectId(lineItem.ItemName);
                if (id == "" || id == null) { return false; }
                if (lineItem.CatalogObjectId != id) { return false; }
                if (lineItem.ItemName == "" || lineItem.ItemName == null) { return false; }
                if (lineItem.AmountRegular == 0
                       && lineItem.Amount3 == 0
                       && lineItem.Amount5 == 0
                       && lineItem.Amount8 == 0
                      && lineItem.Amount10 == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// If item name returns one catalog item, the lineitem's catalog id will be completed and function returns true.
        /// If catalog service returns 0 items or more than 1 item, function returns false
        /// </summary>
        /// <param name="text">item name</param>
        /// <returns></returns>
        public bool ValidateItemName(string text)
        {
            if (text != "")
            {
                PetsiOrderLineItem lineItem = LineItems.First(x => x.ItemName == text);
                if (lineItem == null) { return false; }
                string id = "";
                if (cs.TryValidateItemName(text, out id))
                {
                    lineItem.CatalogObjectId = id;
                    lineItem.IsValid = true;
                    return true;
                }
                lineItem.CatalogObjectId = "";
                lineItem.IsValid = false;
            }
            return false;
        }

        public bool IsValidItem(string itemName)
        {
            PetsiOrderLineItem lineItem = LineItems.First(x => x.ItemName == itemName);
            if (lineItem == null) { return false; }
            if(lineItem.IsValid)
            {
                return true;
            }
            return false;
        }

        public List<CatalogItemPetsi> GetItemMatchResults(string itemName)
        {
            return cs.GetItemNameValidationResults(itemName);
        }

        public List<string> GetOrderTypes()
        {
            return Identifiers.GetOrderTypes();
        }

        public void ItemsIsReadOnly()
        {
            foreach (PetsiOrderLineItem lineItem in LineItems)
            {
                lineItem.IsReadOnly = true;
                lineItem.NotReadOnly = false;
            }
        }
        public void ItemsNotReadOnly()
        {
            foreach (PetsiOrderLineItem lineItem in LineItems)
            {
                lineItem.IsReadOnly = false;
                lineItem.NotReadOnly = true;
            }
        }
        public void HandleOrderFreezeToggle(bool? isChecked)
        {
            if (isChecked.Value)
            {
                _order.IsFrozen = true;
                //ObsOrderModelSingleton.FreezeOrder(_order);
            }
            else
            {
                _order.IsFrozen = false;
                //ObsOrderModelSingleton.ThawOrder(_order);
            }
        }
    }
}
