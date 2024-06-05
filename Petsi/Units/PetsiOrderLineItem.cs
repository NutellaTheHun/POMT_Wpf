
using System.ComponentModel;

namespace Petsi.Units
{
    public class PetsiOrderLineItem : IEquatable<PetsiOrderLineItem>
    {
        public string ItemName { get; set; }
        public string CatalogObjectId { get; set; }
        public int Amount3 { get; set; }
        public int Amount5 { get; set; }
        public int Amount8 { get; set; }
        public int Amount10 { get; set; }
        public int AmountRegular { get; set; }
        public bool IsInvalid { get; set; }
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
            if(AmountRegular != other.Amount10) { return false; }
            
            return true;
        }
    }
}
