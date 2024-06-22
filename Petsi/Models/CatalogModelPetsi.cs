using Petsi.CommandLine;
using Petsi.Filing;
using Petsi.Interfaces;
using Petsi.Labels;
using Petsi.Managers;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Models
{
    /// <summary
    ///  <para>Data model of products from squares catalog API. products are represented as CatalogItemPetsi data types. </para> 
    ///  <para>INTERFACES: ICatalogService</para> 
    /// </summary>
    public class CatalogModelPetsi : ModelBase, IModelInput, IModelPublishable
    {
        List<ServiceBase> ServiceListeners;
        /// <summary>
        /// container for catalog items pulled from Square Catalog api. Holds information regarding item names, id, and variation names and ids.
        /// </summary>
        List<CatalogItemPetsi> items;

        /// <summary>
        /// Recieved from square catalog input component, passed to Catalog services
        /// </summary>
        List<(string name, string id)> Categories;
       
        CatalogModelFrameBehavior frameBehavior;
        FileBehavior fileBehavior;

        public CatalogModelPetsi()
        {
            items = new List<CatalogItemPetsi>();
            Categories = new List<(string name, string id)>();
            frameBehavior = new CatalogModelFrameBehavior(this);
            fileBehavior = new FileBehavior("CatalogModel");
            ServiceListeners = new List<ServiceBase>();

            SetModelName(Identifiers.MODEL_CATALOG);
            ModelManagerSingleton.GetInstance().Register(this);
            CommandFrame.GetInstance().RegisterFrame("cmp", frameBehavior);
            EnvironCaptureRegistrySingleton.GetInstance().Register(this);
        }

        public override void AddData(ModelUnitBase item)
        { 
            CatalogItemPetsi cip = (CatalogItemPetsi)item;
            if(!items.Contains(cip)) //Sqaure ListCatalog API call contains duplicates.
            {
                items.Add(cip);
            }
        }

        public void AddNewItem(CatalogItemPetsi newItem)
        {
            if (!items.Contains(newItem)) //Sqaure ListCatalog API call contains duplicates.
            {
                items.Add(newItem);
                NotifyModelServices();
            }
            else
            {
                SystemLogger.Log("ERROR Duplicate newItem entered to catalog while handling new item from soi");
            }
        }
        public override FrameBehaviorBase GetFrameBehavior(){ return frameBehavior; }
        public List<CatalogItemPetsi> GetItems(){ return items; }
        public void SetItemList(List<CatalogItemPetsi> newItems)
        {
            items = newItems;
            SaveMainModel();
            NotifyModelServices();
        }
        public FileBehavior GetFileBehavior(){ return fileBehavior; }
        public override void ClearModel() { items.Clear(); }

        public override void AddOrder(ModelUnitBase item)
        {
            int count = items.Count;
            items.Add((CatalogItemPetsi)item);
            if(count+1 != items.Count)
            {
                SystemLogger.Log("cmp AddOrder failure: ");
            }
            else
            {
                SaveMainModel();
                NotifyModelServices();
            }
        }

        public void SetCategoryList(List<(string, string)> categories){ Categories = categories; }

        public List<(string, string)> GetCategories(){return Categories;}

        public void AddModelService(ServiceBase service){ ServiceListeners.Add(service); }

        public void RemoveModelService(ServiceBase service){ ServiceListeners.Remove(service); }

        public void NotifyModelServices()
        {
            foreach(ServiceBase service in ServiceListeners) { service.Update(this); }
        }

        public override void Complete()
        {
            FinalizeMainModel();
            SaveMainModel();
            NotifyModelServices();
        }

        public void UpdateModel()
        {
            SaveMainModel();
            NotifyModelServices();
        }
        private void SaveMainModel()
        {
            GetFileBehavior().DataListToFile(Identifiers.MAIN_MODEL_CATALOG_FILE, GetItems());
        }
        private void FinalizeMainModel()
        {
            List<CatalogItemPetsi> mainList = fileBehavior.BuildDataListFile<CatalogItemPetsi>(Identifiers.MAIN_MODEL_CATALOG_FILE);
            List<CatalogItemPetsi> squareList = new List<CatalogItemPetsi>(GetItems());
            List<CatalogItemPetsi> newList = new List<CatalogItemPetsi>(mainList);

            foreach(CatalogItemPetsi squareItem in squareList)
            {
                if(!newList.Contains(squareItem))
                {
                    newList.Add(squareItem);
                }
            }
            SetItemList(RemoveDuplicates(newList));
        }

        private List<CatalogItemPetsi> RemoveDuplicates(List<CatalogItemPetsi> newList)
        {
            Dictionary<string, CatalogItemPetsi> dict = new Dictionary<string, CatalogItemPetsi>();
            foreach(CatalogItemPetsi item in newList)
            {
                if(item.ItemName.Contains("(1)"))
                {
                    continue;
                }
                if (dict.ContainsKey(item.ItemName))
                {
                    dict[item.ItemName].Alt_CatalogObjId.Add(item.CatalogObjectId);
                }
                else
                {
                    dict[item.ItemName] = item;
                }

                /* //Removed variation tuples that were errantly created.
                   //Items from square do not need UserbasedIds which were uninentionally created and need to be removed
                   //Items that have userbasedIds got them because the item didnt exist in square's catalog, these shouldn't be pruned
                foreach(var variation in item.VariationList.ToList())
                {
                    if (!variation.variationId.Contains("userbased")) //if an item has square catalog Ids, they dont need userGenerated Ids and needs to be pruned
                    {
                        List<(string variationId, string variationName)> copy = new List<(string variationId, string variationName)>(item.VariationList);
                        foreach (var var in copy)
                        {
                            if(var.variationId.Contains("userbased")) //removes all userbased IDs
                            {
                                item.VariationList.Remove(var);
                            }
                        }
                        break;
                    }
                }
                */
            }
            return dict.Values.ToList();
        }

        public override string GetModelName()
        {
            return ModelName;
        }

        public override void SetModelName(string modelName)
        {
            ModelName = modelName;
        }

        /// <summary>
        /// Returns all items where input is contained within the itemName variable
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public List<CatalogItemPetsi> SearchByItemName(string searchTerm)
        {
            List<CatalogItemPetsi> result = new List<CatalogItemPetsi>();
            string itemName;
            foreach (CatalogItemPetsi item in items)
            {
                itemName = item.ItemName;
                if (itemName.Contains(searchTerm.ToLower()) || item.NaturalNameContains(searchTerm.ToLower()))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public override void CaptureEnvironment(FileBehavior reportFb)
        {
            reportFb.DataListToFile(Identifiers.ENV_CMP, items);
            //Categories?
        }

        public void InitialLabelMapBoot()
        {
            List<(string id, string path)> cuties = LoadLabels.GetCutieInitialMap();
            List<(string id, string path)> pie = LoadLabels.GetStandardInitialMap();
            foreach(CatalogItemPetsi item in items)
            {
                foreach((string id, string filename) entry in cuties)
                {
                    if (item.CatalogObjectId == entry.id)
                    {
                        item.CutieLabelFilePath = entry.filename;
                    }

                }
                foreach ((string id, string filename) entry in pie)
                {
                    if (item.CatalogObjectId == entry.id)
                    {
                        item.StandardLabelFilePath = entry.filename;
                    }
                }
            }
            SaveMainModel();
        }

        public void RemoveItem(CatalogItemPetsi order)
        {
            int count = items.Count;
            items.Remove(order);
            if (count - 1 == items.Count)
            {
                UpdateModel();
            }
            else
            {
                SystemLogger.Log("CatalogModel remove item failed, original count:  " + count + " after operation: " + items.Count);
            }
        }

        public void ModifyItem(CatalogItemPetsi catalogItem)
        {
            int index = 0;
            bool isFound = false;
            foreach (CatalogItemPetsi item in items)
            {
                if (item.CatalogObjectId == catalogItem.CatalogObjectId)
                {
                    index = items.IndexOf(item);
                    isFound = true;
                    break;
                }
            }
            if(isFound)
            {
                items[index] = catalogItem;
                UpdateModel();
            }
            else
            {
                SystemLogger.Log("CatalogModelPetsi modifyItem not found: " + catalogItem.ItemName + " " + ": " + catalogItem.CatalogObjectId);
            }
        }
    }
}
