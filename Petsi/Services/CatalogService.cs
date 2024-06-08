using DocumentFormat.OpenXml.Bibliography;
using Petsi.CommandLine;
using Petsi.CommandLine.ErrorHandlers;
using Petsi.Interfaces;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;

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
            CatalogModelPetsi cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            cmp.AddModelService(this);
        }

        /// <summary>
        /// Returns the catalog object ID from square's catalog API, returns "" if fails
        /// </summary>
        /// <param name="input">Can be item name or variation Id</param>
        /// <returns></returns>
        public string GetCatalogObjectId(string input)
        {
            CatalogItemPetsi source;
            string result = "";
            catalogIdDict.TryGetValue(input, out source);
            if (source != null)
            {
                result = source.CatalogObjectId;
            }
            return result;
            //if (catalogIdDict.TryGetValue(input, out source))
            //{
            //    if(source == null)
            //    {
            //        return "";
            //    }
            //    else
            //    {
            //        return source.CatalogObjectId;
            //    }
            //    //return result;
            //}
            //else
            //{
            //    throw new Exception("GetCatalogObjId key/value doesn't exist, key used: " + input);
            //}
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
            if(results.Count == 1)
            {
                catalogId = results[0].CatalogObjectId;
                return true;
            }
            catalogId = "";
            return false;
        }
        public string ValidateModifyItemName(string name)
        {
            List<string> results = new List<string>();

            foreach (CatalogItemPetsi item in catalog)
            {
                if (item.ItemName.ToLower().Contains(name.ToLower()) || item.NaturalNameContains(name.ToLower()))
                {
                    results.Add(item.ItemName);
                }
            }
            if (results.Count == 0)
            {
                //throw new Exception("No matching catalog name found from given modified name: " + name);
                //string correction = await HandleNewModifier(name);
                //results.Add(correction);

                //Handle an unknown modifer name, either create a new catalog item, or add to natural name
                //CommandFrame.GetInstance().InjectErrorHandlingFrame(new CatalogServiceErrorFrameBehavior(name));
                HandleNewModifier(name);
                return ValidateModifyItemName(name);
            }
            else if (results.Count > 1)
            {
                if (name == "Lemon")//temporary until square updates "Lemon" to "Lemon Glaze"
                {
                    return ValidateModifyItemName("Lemon Glaze")/*.Result*/;
                }
                else
                {
                    Console.WriteLine("multiple matching catalog names found from given modified name: " + name);
                    for (int i = 0; i < results.Count; i++)
                    {
                        Console.WriteLine("   " + results[i]);
                    }
                }
                return "";
            }
            return results[0];
        }

        private void HandleNewModifier(string name)
        {
            CommandFrame.GetInstance().InjectErrorHandlingFrame(new CatalogServiceErrorFrameBehavior(name));
        }

        public override void Update(ModelBase model) //Is this truly updating/neccessay?
        {
            catalogIdDict.Clear();

            CatalogModelPetsi cmp = model as CatalogModelPetsi;

            catalog = cmp.GetItems();
            foreach (CatalogItemPetsi item in catalog)
            {
                catalogIdDict.TryAdd(item.ItemName, item);
                //foreach (DictionaryEntry entry in item.Variations)
                //{
                //    catalogIdDict.TryAdd((string)entry.Key, item.CatalogObjectId);
                //}
                foreach ((string variationId, string variationName) in item.VariationList)
                {
                    catalogIdDict.TryAdd(variationId, item);
                }
            }
        }

        public bool NameExists(string input)
        {
            return catalog.Any(item => item.ItemName.ToLower().Contains(input.ToLower()));  
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

            if(searchItem == null) { SystemLogger.Log("CatalogSerivce GetItem() did not find item: " + searchItemName); }

            result.ItemName = searchItem.ItemName;
            result.CatalogObjectId = searchItem.CatalogObjectId;
            result.VariationName = Identifiers.SIZE_REGULAR;
            result.Quantity = quantity.ToString();
            result.VariationId = searchItem.CatalogObjectId;

            return result;
        }

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
    }
}
