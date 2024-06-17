using Petsi.CommandLine;
using System.Collections.Specialized;

namespace Petsi.Units
{
    public class CatalogItemPetsi : ModelUnitBase, IEquatable<CatalogItemPetsi>
    {
        CatalogItemFrameBehavior frameBehavior;
        public string CategoryId { get; set; }
        public string CatalogObjectId { get; set; }
        public string ItemName { get; set; }
        /// <summary>
        /// A list of associated names or terms with the item, such as abbreviations, accroynms or alternative naming.
        /// Such as Corn w Rasp Jam in modifiers representing a Corn Muffin with Raspberry Jam,
        /// or CBP for Chocolate Bourbon Pecan
        /// These natural terms are used when validating names from modifiers section of a square order line item, 
        /// and when validiating user entered terms when adding to a whole sale account.
        /// </summary>
        public List<string> NaturalNames { get; set; }

        public List<string> Alt_CatalogObjId { get; set; }

        /// <summary>
        /// key is variation id, value is variation name,
        /// catalogObjectId in Orders api equates to variation id of catalog object id in catalog api
        /// </summary>
        public ListDictionary Variations { get; set; }
        public List<(string variationId, string variationName)> VariationList { get; set; }

        public string StandardLabelFilePath { get; set; }
        public string CutieLabelFilePath { get; set; }
        public CatalogItemPetsi(string categoryId, string catalogObjectId, string itemName)
        {
            this.CategoryId = categoryId;
            this.CatalogObjectId = catalogObjectId;
            this.ItemName = itemName;
            Variations = new ListDictionary();
            frameBehavior = new CatalogItemFrameBehavior(this);
            NaturalNames = new List<string>();
            VariationList = new List<(string variationName, string variationId)>();
            Alt_CatalogObjId = new List<string>();
        }
        public CatalogItemPetsi(string categoryId, string catalogObjectId, string itemName, ListDictionary variations, List<string> naturalNames)
        {
            this.CategoryId = categoryId;
            this.CatalogObjectId = catalogObjectId;
            this.ItemName = itemName;
            this.Variations = variations;
            frameBehavior = new CatalogItemFrameBehavior(this);
            this.NaturalNames = naturalNames;
            VariationList = new List<(string variationName, string variationId)>();
            Alt_CatalogObjId = new List<string>();
        }
        public CatalogItemPetsi()
        {
            Variations = new ListDictionary();
            frameBehavior = new CatalogItemFrameBehavior(this);
            NaturalNames = new List<string>();
            VariationList = new List<(string variationName, string variationId)>();
            Alt_CatalogObjId = new List<string>();
        }
        public override FrameBehaviorBase GetFrameBehavior()
        {
            return frameBehavior;
        }

        public List<string> GetNaturalNames()
        {
            return NaturalNames;
        }

        public ListDictionary GetVariations()
        {
            return Variations;
        }

        public bool Equals(CatalogItemPetsi? other)
        {
            if (CategoryId != other.CategoryId)//soft equality
            {
                return false;
            }
            if (ItemName.ToLower() != other.ItemName.ToLower())//soft equality
            {
                return false;
            }

            if (CatalogObjectId != other.CatalogObjectId)//hard equality
            {
                return false;
            }
            return true;
        }
        public bool NaturalNameContains(string searchTerm)
        {
            
            if(NaturalNames.Count > 0) 
            {
                return NaturalNames.Any(name => name.ToLower().Contains(searchTerm.ToLower()));
            } 
            return false; 
        }
        public bool NaturalNameEquals(string searchTerm)
        {

            if (NaturalNames.Count > 0)
            {
                return NaturalNames.Any(name => name.ToLower().Equals(searchTerm.ToLower()));
            }
            return false;
        }

        public void AddNaturalName(string errorName)
        {
            NaturalNames.Add(errorName);
        }

        public void RemoveNaturalName(string selectedItem)
        {
            NaturalNames.Remove(selectedItem);
        }
    }
}
