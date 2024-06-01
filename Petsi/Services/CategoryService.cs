using Petsi.Interfaces;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;
using System.Collections;

namespace Petsi.Services
{
    public class CategoryService : ServiceBase, ICategoryService, IModelSubsciber
    {
        /// <summary>
        /// given an item name, variation id, or catalog object id, returns square's category ID. See Identifier class for types of categories.
        /// </summary>
        Dictionary<string, string> categoryMap;

        List<(string name, string id)> categoryList;

        public CategoryService()
        {
            categoryMap = new Dictionary<string, string>();
            categoryList = new List<(string name, string id)>();
            SetServiceName(Identifiers.SERVICE_CATEGORY);
            ServiceManagerSingleton.GetInstance().Register(this);
            CatalogModelPetsi cmp = (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG);
            cmp.AddModelService(this);
        }

        public List<string> GetCategoryNames()
        {
            List<string> result = new List<string>();
            result.AddRange(categoryList.Select(item => item.name));
            /*
            foreach(var item in categoryList)
            {
                result.Add(item.name);
            }
            */
            return result;
        }

        public string GetCategoryId(int categoryIndex)
        {
            return categoryList[categoryIndex].id;
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

        public override void Update(ModelBase model)
        {
            categoryMap.Clear();

            CatalogModelPetsi cmp = model as CatalogModelPetsi;

            categoryList = new List<(string name, string id)>(cmp.GetCategories());

            foreach (CatalogItemPetsi item in cmp.GetItems())
            {
                categoryMap.TryAdd(item.ItemName, item.CategoryId);
                categoryMap.TryAdd(item.CatalogObjectId, item.CategoryId);
                foreach (DictionaryEntry entry in item.Variations)
                {
                    categoryMap.TryAdd((string)entry.Key, item.CategoryId);
                }
            }
        }
    }
}
