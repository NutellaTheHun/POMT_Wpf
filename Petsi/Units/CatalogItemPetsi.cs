using Petsi.CommandLine;
using System.Collections.Specialized;

namespace Petsi.Units
{
    public class CatalogItemPetsi : ModelUnitBase, IEquatable<CatalogItemPetsi>
    {
        CatalogItemFrameBehavior frameBehavior;
        public string categoryId { get; set; }
        public string catalogObjectId { get; set; }
        public string itemName { get; set; }
        /// <summary>
        /// A list of associated names or terms with the item, such as abbreviations, accroynms or alternative naming.
        /// Such as Corn w Rasp Jam in modifiers representing a Corn Muffin with Raspberry Jam,
        /// or CBP for Chocolate Bourbon Pecan
        /// These natural terms are used when validating names from modifiers section of a square order line item, 
        /// and when validiating user entered terms when adding to a whole sale account.
        /// </summary>
        public List<string> naturalNames { get; set; }

        /// <summary>
        /// key is variation id, value is variation name,
        /// catalogObjectId in Orders api equates to variation id of catalog object id in catalog api
        /// </summary>
        public ListDictionary variations { get; set; }

        public string StandardLabelFilePath { get; set; }
        public string CutieLabelFilePath { get; set; }
        public CatalogItemPetsi(string categoryId, string catalogObjectId, string itemName)
        {
            this.categoryId = categoryId;
            this.catalogObjectId = catalogObjectId;
            this.itemName = itemName;
            variations = new ListDictionary();
            frameBehavior = new CatalogItemFrameBehavior(this);
            naturalNames = new List<string>();
        }
        public CatalogItemPetsi(string categoryId, string catalogObjectId, string itemName, ListDictionary variations, List<string> naturalNames)
        {
            this.categoryId = categoryId;
            this.catalogObjectId = catalogObjectId;
            this.itemName = itemName;
            this.variations = variations;
            frameBehavior = new CatalogItemFrameBehavior(this);
            this.naturalNames = naturalNames;
        }
        public CatalogItemPetsi()
        {
            variations = new ListDictionary();
            frameBehavior = new CatalogItemFrameBehavior(this);
            naturalNames = new List<string>();
        }
        public override FrameBehaviorBase GetFrameBehavior()
        {
            return frameBehavior;
        }

        public List<string> GetNaturalNames()
        {
            return naturalNames;
        }

        public ListDictionary GetVariations()
        {
            return variations;
        }

        public bool Equals(CatalogItemPetsi? other)
        {
            if (categoryId != other.categoryId)//soft equality
            {
                return false;
            }
            if (itemName.ToLower() != other.itemName.ToLower())//soft equality
            {
                return false;
            }

            if (catalogObjectId != other.catalogObjectId)//hard equality
            {
                return false;
            }
            return true;
        }
        public bool NaturalNameContains(string searchTerm)
        {
            
            if(naturalNames.Count > 0) 
            {
                naturalNames.Any(name => name.ToLower().Contains(searchTerm.ToLower()));
                /*
                foreach (string naturalName in naturalNames)
                {
                    if (naturalName.ToLower().Contains(searchTerm.ToLower()))
                    {
                        return true;
                    }
                }
                */
            } 
            return false;
            
            //return naturalNames.Any(name => name.ToLower().Contains(searchTerm.ToLower()));
        }

        public void AddNaturalName(string errorName)
        {
            naturalNames.Add(errorName);
        }
    }
}
