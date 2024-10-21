using Petsi.Managers;
using Petsi.Services;
using Petsi.Utils;
using System.Collections.Specialized;

namespace Petsi.Units
{
    public class CatalogItemPetsi : ModelUnitBase, IEquatable<CatalogItemPetsi>
    {
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
        public List<(string variationId, string variationName)> DisabledVariationList { get; set; }
        public CatalogItemPetsi VeganPieAssociation { get; set; }
        public CatalogItemPetsi TakeNBakePieAssociation { get; set; }
        public CatalogItemPetsi VeganTakeNBakePieAssociation { get; set; }
        public string StandardLabelFilePath { get; set; }
        public string CutieLabelFilePath { get; set; }
        public bool IsPOTM {  get; set; }
        public bool IsParbake {  get; set; }

        public CatalogItemPetsi(CatalogItemPetsi? copyItem)
        {
            if(copyItem != null)
            {
                CategoryId = copyItem.CategoryId;
                CatalogObjectId = copyItem.CatalogObjectId;
                ItemName = copyItem.ItemName;
                NaturalNames = new List<string>(copyItem.NaturalNames);
                Alt_CatalogObjId = new List<string>(copyItem.Alt_CatalogObjId);
                Variations = copyItem.Variations;
                VariationList = new List<(string variationId, string variationName)>(copyItem.VariationList);
                DisabledVariationList = new List<(string variationId, string variationName)>(copyItem.DisabledVariationList);

                if (copyItem.VeganPieAssociation != null)
                {
                    VeganPieAssociation = new CatalogItemPetsi(copyItem.VeganPieAssociation);
                }

                if (copyItem.TakeNBakePieAssociation != null)
                {
                    TakeNBakePieAssociation = new CatalogItemPetsi(copyItem.TakeNBakePieAssociation);
                }

                if (copyItem.VeganTakeNBakePieAssociation != null)
                {
                    VeganTakeNBakePieAssociation = new CatalogItemPetsi(copyItem.VeganTakeNBakePieAssociation);
                }

                StandardLabelFilePath = copyItem.StandardLabelFilePath;
                CutieLabelFilePath = copyItem.CutieLabelFilePath;
                IsPOTM = copyItem.IsPOTM;
                IsParbake = copyItem.IsParbake;
            }
            else
            {
                Variations = new ListDictionary();
                NaturalNames = new List<string>();
                VariationList = new List<(string variationName, string variationId)>();
                DisabledVariationList = new List<(string variationId, string variationName)>();
                Alt_CatalogObjId = new List<string>();
            }
        }

        public CatalogItemPetsi(string categoryId, string catalogObjectId, string itemName)
        {
            CategoryId = categoryId;
            CatalogObjectId = catalogObjectId;
            ItemName = itemName;
            Variations = new ListDictionary();
            NaturalNames = new List<string>();
            VariationList = new List<(string variationName, string variationId)>();
            DisabledVariationList = new List<(string variationId, string variationName)>();
            Alt_CatalogObjId = new List<string>();
        }
        public CatalogItemPetsi(string categoryId, string catalogObjectId, string itemName, ListDictionary variations, List<string> naturalNames)
        {
            CategoryId = categoryId;
            CatalogObjectId = catalogObjectId;
            ItemName = itemName;
            Variations = variations;
            NaturalNames = naturalNames;
            VariationList = new List<(string variationName, string variationId)>();
            DisabledVariationList = new List<(string variationId, string variationName)>();
            Alt_CatalogObjId = new List<string>();
        }
        public CatalogItemPetsi()
        {
            Variations = new ListDictionary();
            NaturalNames = new List<string>();
            VariationList = new List<(string variationName, string variationId)>();
            DisabledVariationList = new List<(string variationId, string variationName)>();
            Alt_CatalogObjId = new List<string>();
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

            if (NaturalNames.Count > 0)
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

        /// <summary>
        /// Enables or disables a legal size for an item in the catalog. 
        /// Change is made in the CatalogItemView by clicking a check box.
        /// Square has variation id's per size, if a new item or new size is enabled, a new variation id is made.
        /// If a size variation is disabled, the variation id is placed in a list of unused sizes, 
        /// if the size is enabled/renabled, the list of unused sizes is checked first for reinsatement
        /// 
        /// There can be duplicate size/variations due to duplicate square category items,
        /// so the forloops must iterate through entire list and not return on first match.
        /// </summary>
        /// <param name="sizeVariation"></param>
        /// <param name="isChecked"></param>
        public void UpdateSizeVariation(string sizeVariation, bool isChecked)
        {
            if (isChecked)
            {
                //if sizeVariation is found in VariationList, do nothing
                List<(string variationId, string variationName)> copy = new List<(string variationId, string variationName)>(VariationList);
                foreach(var item in copy)
                {
                    if(item.variationName.Contains(sizeVariation))
                    {
                        return;
                    }
                }

                //if not found
                //if variation found in disabledVariationList, move variation to Variation list
                List<(string variationId, string variationName)> disCopy = new List<(string variationId, string variationName)>(DisabledVariationList);
                foreach (var item in disCopy)
                {
                    if (item.variationName.Contains(sizeVariation))
                    {
                        VariationList.Add(item);
                        DisabledVariationList.Remove(item);
                        return;
                    }
                }

                //if not in disabledList
                //Create new variation
                CatalogService cs = GetCatalogService();
                VariationList.Add((cs.GenerateCatalogId(), sizeVariation));
            }
            else
            {
                //if not checked
                //if variation is found in variation list, move to disabledList
                List<(string variationId, string variationName)> copy = new List<(string variationId, string variationName)>(VariationList);
                foreach (var item in copy)
                {
                    if (item.variationName.Contains(sizeVariation))
                    {
                        DisabledVariationList.Add(item);
                        VariationList.Remove(item);
                        return;
                    }
                }
            }
        }
        private CatalogService GetCatalogService() { return (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG); }

        public bool VariationExists(string variationName) 
        {
            foreach((string variationId, string variationName) entry in VariationList)
            {
                if(entry.variationName.ToLower().Contains(variationName.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        public static string GenerateCatalogId()
        {
            return Identifiers.USER_BASED_ID_TAG + Guid.NewGuid().ToString();
        }

        public void CleanAltCatalogId()
        {
            if (Alt_CatalogObjId == null || Alt_CatalogObjId.Count == 0)
            {
                return;
            }
            List<string> result = new List<string>();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (string altId in Alt_CatalogObjId)
            {
                if (!dict.ContainsKey(altId)) 
                {
                    dict.Add(altId, altId);
                }
            }
            Alt_CatalogObjId = dict.Values.ToList();
        }
    }
}
