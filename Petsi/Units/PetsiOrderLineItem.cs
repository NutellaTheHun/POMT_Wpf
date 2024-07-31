using Petsi.Events.ItemEvents;
using Petsi.Managers;
using Petsi.Services;
using Petsi.Utils;
using System.ComponentModel;

namespace Petsi.Units
{
    public class PetsiOrderLineItem : IEquatable<PetsiOrderLineItem>, INotifyPropertyChanged
    {
        public string ItemName { get; set; }
        public string CatalogObjectId { get; set; }

        private int _amount3;
        public int Amount3 
        { 
            get { return _amount3; }
            set { _amount3 = value; OrderLineItemEvents.RaiseOnQuantityChange(); }
        }
        private int _amount5;
        public int Amount5
        {
            get { return _amount5; }
            set { _amount5 = value; OrderLineItemEvents.RaiseOnQuantityChange(); }
        }
        private int _amount8;
        public int Amount8
        {
            get { return _amount8; }
            set { _amount8 = value; OrderLineItemEvents.RaiseOnQuantityChange(); }
        }

        private int _amount10;
        public int Amount10
        {
            get { return _amount10; }
            set { _amount10 = value; OrderLineItemEvents.RaiseOnQuantityChange(); }
        }

        private int _amountRegular;
        public int AmountRegular
        {
            get { return _amountRegular; }
            set { _amountRegular = value; OrderLineItemEvents.RaiseOnQuantityChange(); }
        }
        private bool _isValid;
        public bool IsValid 
        {
            get { return _isValid; }
            set 
            { 
                if (_isValid == value) 
                { 
                    _isValid = value;
                    OnPropertyChanged(nameof(IsValid));
                } 
            }
        }

        //The only way I could get the datagrid for the order form to propertly be set to read only.
        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }
            set
            {
                if (_isReadOnly != value)
                {
                    _isReadOnly = value;
                    OnPropertyChanged(nameof(IsReadOnly));
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //---------------------------------
        public bool NotReadOnly { get; set; }

        public PetsiOrderLineItem() 
        {
            ItemName = "";
            CatalogObjectId = "";
            Amount10 = 0;
            Amount3 = 0;
            Amount5 = 0;
            Amount8 = 0;
            AmountRegular = 0;
        }
        public PetsiOrderLineItem(PetsiOrderLineItem itemSource)
        {
            ItemName = itemSource.ItemName;
            CatalogObjectId = itemSource.CatalogObjectId;
            Amount10 = itemSource.Amount10;
            Amount3 = itemSource.Amount3;
            Amount5 = itemSource.Amount5;
            Amount8 = itemSource.Amount8;
            AmountRegular = itemSource.AmountRegular;
        }
        public PetsiOrderLineItem(string itemName, string catalogObjectId, int amount3, int amount5, int amount8, int amount10, int regular)
        {
            ItemName = itemName;
            CatalogObjectId = catalogObjectId;
            Amount3 = amount3;
            Amount5 = amount5;
            Amount8 = amount8;
            Amount10 = amount10;
            AmountRegular = regular;
        }
        public void TestPrintOnOrderLineItem()
        {
            Console.WriteLine("     Item: " + ItemName +
                "     Id: " + CatalogObjectId +
                "     3\": " + Amount3 +
                "     5\": " + Amount5 +
                "     8\": " + Amount8 +
                "     10\": " + Amount10+
                "     Regular: " + AmountRegular);
        }
        public override string ToString()
        {
            return "     Item: " + ItemName +
                "     Id: " + CatalogObjectId +
                "     3\": " + Amount3 +
                "     5\": " + Amount5 +
                "     8\": " + Amount8 +
                "     10\": " + Amount10 +
                "     Regular: " + AmountRegular;
        }
        public void Merge(PetsiOrderLineItem sourceItem)
        {
            if(sourceItem.ItemName != this.ItemName)
            {
                Console.WriteLine("Item name mismatch:");
                Console.WriteLine("    source: " + sourceItem.ItemName);
                Console.WriteLine("    target: " + ItemName);
            }
            Amount3 += sourceItem.Amount3;
            Amount5 += sourceItem.Amount5;
            Amount8 += sourceItem.Amount8;
            Amount10 += sourceItem.Amount10;
            AmountRegular += sourceItem.AmountRegular;
        }

        
        public bool Equals(PetsiOrderLineItem? other)
        {
            if(other.ItemName != null && ItemName.ToLower() != other.ItemName.ToLower()) {  return false; }
            if (CatalogObjectId != other.CatalogObjectId) { return false; }
            if (Amount3 != other.Amount3) {  return false; }
            if(Amount5 != other.Amount5) {  return false; }
            if(Amount8 != other.Amount8) { return false; }
            if(Amount10 != other.Amount10) {  return false; }
            if(AmountRegular != other.AmountRegular) { return false; }
            
            return true;
        }
        
        public bool IsPOTM()
        {
           CatalogService cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
           return cs.IsPOTM(CatalogObjectId);
        }

        public bool IsParbake()
        {
            CatalogService cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            return cs.IsParbake(CatalogObjectId);
        }

        /// <summary>
        /// returns true if the lineItem is a vegan pie assoicated with the backListItemId.
        /// If lineItem is a Vegan apple and the backlistItemId is for classic Apple, returns true
        /// Templates aren't intended to have rows for specific vegan categories, vegan quantities are expressed in its
        /// assoicated non-vegan counterpart.
        /// </summary>
        /// <param name="backListItemId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsVeganTo(string backListItemId)
        {
            CatalogService cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            return cs.IsVeganAssociate(backListItemId, CatalogObjectId);
        }

        public bool IsCategory(string categoryId)
        {
            CatalogService cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            CatalogItemPetsi item = cs.GetCatalogItemById(CatalogObjectId);
            return item.CategoryId == categoryId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetSize">A Size identifier string</param>
        /// <returns></returns>
        public bool IsValidSize(string targetSize)
        {
            CatalogService cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            CatalogItemPetsi item = cs.GetCatalogItemById(CatalogObjectId);

            if (!item.VariationExists(targetSize)) { return false; }

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetSize">A Size identifier string</param>
        /// <returns></returns>
        public bool IsValidName(string targetSize)
        {
            CatalogService cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            CatalogItemPetsi item = cs.GetCatalogItem(ItemName);

            if(item == null) { return false; }

            return true;
        }
    }
}
