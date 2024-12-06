using Petsi.Interfaces;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Services
{
    public class CategoryService : ServiceBase, ICategoryService, IModelSubsciber
    {
        /// <summary>
        /// given an item name, variation id, or catalog object id, returns square's category ID. See Identifier class for types of categories.
        /// </summary>
        Dictionary<string, string> categoryMap;


        List<(string categoryName, string id)> categoryList;

        public CategoryService()
        {
            categoryMap = new Dictionary<string, string>();
            categoryList = new List<(string categoryName, string id)>();
            SetServiceName(Identifiers.SERVICE_CATEGORY);
            ServiceManagerSingleton.GetInstance().Register(this);
            CatalogModelPetsi cmp = ModelManagerSingleton.GetInstance().GetCatalogModel();
            cmp.AddModelService(this);
        }

        public List<string> GetCategoryNames()
        {
            List<string> result = new List<string>();
            result.AddRange(categoryList.Select(item => item.categoryName));
            return result;
        }

        public string GetCategoryIdByIndex(int categoryIndex)
        {
            return categoryList[categoryIndex].id;
        }

        public string GetCategoryIdByCategoryName(string categoryName)
        {
            string result = "";
            foreach ((string categoryName, string id) item in categoryList)
            {
                if(item.categoryName == categoryName)
                {
                    result = item.id;
                }
            }
            return result;
        }

        /// <summary>
        /// Returns the category ID given an item name, catalog id, or variation id.
        /// See Identifier class for categories.
        /// </summary>
        /// <param name="input">can be an item name, catalog obj id, or variation id</param>
        /// <returns></returns>
        public string GetItemCategoryId(string input)
        {
            string result = "";
            if (categoryMap.TryGetValue(input, out result))
            {
                return result;
            }
            else
            {
                throw new Exception("GetCategoryId key/value doesn't exist, key used: " + input);
            }
        }

        public string GetCategoryName(string categoryId)
        {
            string result = "";
            foreach ((string categoryName, string id) item in categoryList)
            {
                if (item.id == categoryId)
                {
                    result = item.categoryName;
                }
            }
            return result;
        }

        public override void Update(ModelBase model)
        {
            categoryMap.Clear();

            CatalogModelPetsi cmp = model as CatalogModelPetsi;

            categoryList = new List<(string name, string id)>(cmp.GetCategories());

            foreach (CatalogItemPetsi item in cmp.GetItems())
            {
                if(item.CategoryId != null)
                {
                    categoryMap.TryAdd(item.ItemName, item.CategoryId);
                    categoryMap.TryAdd(item.CatalogObjectId, item.CategoryId);
                    if(item.VariationList != null)
                    {
                        foreach(var entry in item.VariationList)
                        {
                            categoryMap.TryAdd(entry.variationId, item.CategoryId);
                        }
                    }
                    if(item.Alt_CatalogObjId != null)
                    {
                        foreach (var entry in item.Alt_CatalogObjId)
                        {
                            categoryMap.TryAdd(entry, item.CategoryId);
                        }
                    }
                }
            }
        }

        public bool ValidateCategory(string categoryIdentifier, string categoryId)
        {
            foreach(var item in categoryList)
            {
                if (item.categoryName == categoryIdentifier)
                {
                    return item.id == categoryId;
                }
            }
            return false;
        }

        /// <summary>
        /// Given an item name, variation id, or catalog id, returns the category id
        /// </summary>
        /// <param name="itemIdentifier">an item name, variation id, or catalog id</param>
        /// <returns> returns the catalog id </returns>
        public string GetCategoryId(string itemIdentifier)
        {
            if (categoryMap.TryGetValue(itemIdentifier, out string result))
            {
                return result;
            }

            SystemLogger.LogError($"GetCategoryID could not get CATEGORY from given id: {itemIdentifier}", "CategoryService.GetCategoryID()");
            return string.Empty;
        }
    }
}
