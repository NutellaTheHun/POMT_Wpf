﻿using Petsi.Interfaces;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;
using SystemLogging.Service;

namespace Petsi.Services
{
    public class CatalogService : ServiceBase, ICatalogService, IModelSubsciber
    {
        /// <summary>
        /// Used by ICatalogService Method for OrderModel to retrieve catalogObjectId. Key: variationId or item name, value: categoryObjectId
        /// </summary>
        //Dictionary<string, string> catalogIdDict;
        Dictionary<string, CatalogItemPetsi> catalogIdDict;

        List<CatalogItemPetsi> catalog;

        public CatalogService()
        {
            catalogIdDict = new Dictionary<string, CatalogItemPetsi>();
            catalog = new List<CatalogItemPetsi>();
            SetServiceName(Identifiers.SERVICE_CATALOG);
            ServiceManagerSingleton.GetInstance().Register(this);
            CatalogModelPetsi cmp = ModelManagerSingleton.GetInstance().GetCatalogModel();
            cmp.AddModelService(this);
        }

        /// <summary>
        /// Returns the catalog object ID from square's catalog API, returns "" if fails
        /// </summary>
        /// <param name="input">Can be item name or variation Id</param>
        /// <returns></returns>
        public string GetCatalogObjectId(string input)
        {
            CatalogItemPetsi source = null;
            string result = "";
            try { catalogIdDict.TryGetValue(input, out source); }
            catch (ArgumentNullException e)
            {
                Logger.LogError("GetCatalogObjectID TryGetValue input is null", "CatalogService GetCatalogObjId()");
            }

            if (source != null)
            {
                result = source.CatalogObjectId;
            }
            return result;
        }
        public bool IsModifyItem(string catalogObjectId)
        {
            if (catalogObjectId == Identifiers.MODIFY_BOX_OF_6_COOKIES
                ||
                catalogObjectId == Identifiers.MODIFY_BOX_OF_6_MUFFINS
                ||
                catalogObjectId == Identifiers.MODIFY_BOX_OF_6_SCONES
                ||
                catalogObjectId == Identifiers.MODIFY_SCONE)
            {
                return true;
            }
            return false;
        }

        //For ItemName form validating
        public bool TryValidateItemName(string name, out string catalogId)
        {
            List<CatalogItemPetsi> results = new List<CatalogItemPetsi>();

            foreach (CatalogItemPetsi item in catalog)
            {
                if (item.ItemName.ToLower().Equals(name.ToLower()) || item.NaturalNameEquals(name.ToLower()))
                {
                    results.Add(item);
                }
            }
            if (results.Count == 1)
            {
                catalogId = results[0].CatalogObjectId;
                return true;
            }
            catalogId = "";
            return false;
        }
        //For ItemName form validating
        public bool ValidateItemName(string name)
        {
            CatalogItemPetsi item = GetCatalogItem(name);

            if (item == null) { return false; }

            return true;
        }

        /// <summary>
        /// Will match the given modifer name to item name in catalog, 
        /// returns catalog item's name if 1 match is found
        /// creates a new item in catalog if 0 matches are found
        /// returns "" if multiple matches are found
        /// </summary>
        /// <param name="name">item name from modifiers section of squareOrderLineItem</param>
        /// <returns></returns>
        public string ValidateModifyItemName(string name)
        {
            List<CatalogItemPetsi> results = new List<CatalogItemPetsi>();
            results = GetItemNameValidationResults(name);

            //HANDLE ADDING NEW ITEM TO CATALOG -> NOTIFY USER
            if (results.Count == 0)
            {
                //Handle an unknown modifer name, either create a new catalog item, or add to natural name
                //CommandFrame.GetInstance().InjectErrorHandlingFrame(new CatalogServiceErrorFrameBehavior(name));
                //HandleNewModifier(name);

                CatalogModelPetsi cmp = ModelManagerSingleton.GetInstance().GetCatalogModel();
                CatalogItemPetsi newItem = new CatalogItemPetsi();
                newItem.ItemName = name;
                newItem.CatalogObjectId = GenerateCatalogId();
                cmp.AddNewItem(newItem);

                ErrorService.Instance().RaiseSoiNewItemEvent(newItem);

                return ValidateModifyItemName(name);
            }
            else if (results.Count > 1)
            {
                ErrorService.Instance().RaiseSoiMultiItemEvent(name, results);

                return name;
            }
            return results[0].ItemName;
        }

        public override void Update(ModelBase model) //Is this truly updating/neccessay?
        {
            catalogIdDict.Clear();

            CatalogModelPetsi cmp = model as CatalogModelPetsi;

            catalog = cmp.GetItems();
            foreach (CatalogItemPetsi item in catalog)
            {
                catalogIdDict.TryAdd(item.ItemName, item);

                foreach ((string variationId, string variationName) in item.VariationList)
                {
                    catalogIdDict.TryAdd(variationId, item);
                }
                foreach (string nName in item.NaturalNames)
                {
                    catalogIdDict.TryAdd(nName, item);
                }
            }
        }

        public bool NameExists(string input)
        {
            if (input == null || input == "") { return false; }
            return catalog.Any(item => item.ItemName.ToLower().Equals(input.ToLower()));
        }

        public string GenerateCatalogId()
        {
            return Identifiers.USER_BASED_ID_TAG + Guid.NewGuid().ToString();
        }

        public List<string> NameContains(string inputName)
        {
            List<string> results = new List<string>();

            results.AddRange(catalog
                .Where(item => item.ItemName.ToLower().Contains(inputName.ToLower()))
                .Select(item => item.ItemName));
            return results;
        }

        /// <summary>
        /// Given the name of an item with the size of regular ONLY, returns its lineItem form
        /// Sets variation ID to the catalogObjId
        /// </summary>
        /// <param name="searchItemName"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public LineItem GetRegularItem(string searchItemName, int quantity)
        {
            CatalogItemPetsi searchItem = null;
            LineItem result = new LineItem();

            searchItem = catalog.FirstOrDefault(item => item.ItemName.ToLower() == searchItemName.ToLower());

            if (searchItem == null) { Logger.LogWarning("CatalogSerivce GetItem() did not find item: " + searchItemName); }

            result.ItemName = searchItem.ItemName;
            result.CatalogObjectId = searchItem.CatalogObjectId;
            result.VariationName = Identifiers.SIZE_REGULAR;
            result.Quantity = quantity.ToString();
            result.VariationId = searchItem.CatalogObjectId;

            return result;
        }

        /// <summary>
        /// matches the given name for an item name in the catalog. Searches in a priority of itemName to natural name,
        /// and if name equals the source to the name contains the source. used in fuzzy search and validating modifier names.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<CatalogItemPetsi> GetItemNameValidationResults(string name)
        {
            List<CatalogItemPetsi> results = new List<CatalogItemPetsi>();

            //hard equals itemName
            foreach (CatalogItemPetsi item in catalog)
            {
                if (item.ItemName.ToLower().Equals(name.ToLower()))
                {
                    results.Add(item);
                }
            }
            //hard equals natural name
            if (results.Count == 0)
            {
                foreach (CatalogItemPetsi item in catalog)
                {
                    if (item.NaturalNameEquals(name.ToLower()))
                    {
                        results.Add(item);
                    }
                }
            }
            //contains item name
            if (results.Count == 0)
            {
                foreach (CatalogItemPetsi item in catalog)
                {
                    if (item.ItemName.ToLower().Contains(name.ToLower()))
                    {
                        results.Add(item);
                    }
                }
            }
            //contains natural name
            if (results.Count == 0)
            {
                foreach (CatalogItemPetsi item in catalog)
                {
                    if (item.NaturalNameContains(name.ToLower()))
                    {
                        results.Add(item);
                    }
                }
            }
            return results;
        }

        /// <summary>
        /// Given a item name, returns the corresponding CatalogItemPetsi object, returns null if fails.
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public CatalogItemPetsi GetCatalogItem(string itemName)
        {
            CatalogItemPetsi result = null;
            catalogIdDict.TryGetValue(itemName, out result);
            return result;
        }

        public CatalogItemPetsi GetCatalogItemById(string targetid)
        {
            CatalogItemPetsi result = null;
            foreach (CatalogItemPetsi item in catalog)
            {
                if (item.CatalogObjectId == targetid)
                {
                    return item;
                }
            }
            return result;
        }

        public bool IsPOTM(string catalogObjectId)
        {
            foreach (CatalogItemPetsi item in catalog)
            {
                if (item.CatalogObjectId == catalogObjectId)
                {
                    return item.IsPOTM;
                }
            }
            return false;
        }
        public bool IsParbake(string catalogObjectId)
        {
            foreach (CatalogItemPetsi item in catalog)
            {
                if (item.CatalogObjectId == catalogObjectId)
                {
                    return item.IsParbake;
                }
            }
            return false;
        }

        /// <summary>
        /// If catalog item returned from backListItemId's VeganPieAssoication's catalogObjectId matches the target Id, returns true
        /// </summary>
        /// <param name="backListItemId">catalog item that could have a vegan counterpart</param>
        /// <param name="targetId">the line item that is checked to be associated as vegan version of backlistItem</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsVeganAssociate(string backListItemId, string targetId)
        {
            foreach (CatalogItemPetsi item in catalog)
            {
                if (item.CatalogObjectId == backListItemId)
                {
                    if (item.VeganPieAssociation != null)
                    {
                        return (item.VeganPieAssociation.CatalogObjectId == targetId);
                    }
                    break;
                }
            }
            return false;
        }

        public bool IsTakeNBakeAssociate(string backListItemId, string targetId)
        {
            foreach (CatalogItemPetsi item in catalog)
            {
                if (item.CatalogObjectId == backListItemId)
                {
                    if (item.TakeNBakePieAssociation != null)
                    {
                        return (item.TakeNBakePieAssociation.CatalogObjectId == targetId);
                    }
                    break;
                }
            }
            return false;
        }

        public bool IsVeganTakeNBakeAssociate(string backListItemId, string targetId)
        {
            foreach (CatalogItemPetsi item in catalog)
            {
                if (item.CatalogObjectId == backListItemId)
                {
                    if (item.VeganTakeNBakePieAssociation != null)
                    {
                        return (item.VeganTakeNBakePieAssociation.CatalogObjectId == targetId);
                    }
                    break;
                }
            }
            return false;
        }

        public List<string> GetItemIdsByCategory(string targetCategoryId)
        {
            List<string> result = new List<string>();
            foreach (CatalogItemPetsi item in catalog)
            {
                if (item.CategoryId == targetCategoryId)
                {
                    result.Add(item.CategoryId);
                }
            }
            return result;
        }
    }
}
