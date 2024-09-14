using Newtonsoft.Json;
using Petsi.Filing;
using Petsi.Interfaces;
using Petsi.Labels;
using Petsi.Managers;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;
using System.Collections.ObjectModel;

namespace Petsi.Models
{
    /// <summary
    ///  <para>Data model of products from squares catalog API. products are represented as CatalogItemPetsi data types. </para> 
    ///  <para>INTERFACES: ICatalogService</para> 
    /// </summary>
    public class CatalogModelPetsi : ModelBase, IModelInput, IModelPublishable, IStartupSubscriber
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
       
        FileBehavior fileBehavior;

        public CatalogModelPetsi()
        {
            items = new List<CatalogItemPetsi>();
            Categories = new List<(string name, string id)>();
            fileBehavior = new FileBehavior("CatalogModel");
            ServiceListeners = new List<ServiceBase>();

            StartupService.Instance.Register(this);

            SetModelName(Identifiers.MODEL_CATALOG);
            ModelManagerSingleton.GetInstance().Register(this);
            EnvironCaptureRegistrySingleton.GetInstance().Register(this);
        }

        /// <summary>
        /// For testing environments only
        /// </summary>
        /// <param name="serializedCatalogItems"></param>
        public CatalogModelPetsi(List<CatalogItemPetsi> serializedCatalogItems, List<(string name, string id)> categories)
        {
            //Testing Injection
            items = new List<CatalogItemPetsi>(serializedCatalogItems);
            Categories = new List<(string name, string id)>(categories);
            fileBehavior = new FileBehavior("TEST_CatalogModel");

            ServiceListeners = new List<ServiceBase>();

            StartupService.Instance.Register(this);

            SetModelName(Identifiers.TEST_MODEL_CATALOG);
            ModelManagerSingleton.GetInstance().Register(this);
            EnvironCaptureRegistrySingleton.GetInstance().Register(this);
        }
        public void UpdateModel(ObservableCollection<CatalogItemPetsi> catalogItems)
        {
            items = catalogItems.ToList();
            SaveMainModel();
            NotifyModelServices();
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
                SystemLogger.LogError($"Duplicate newItem {newItem.ItemName} entered to catalog while handling new item from soi", "CMP AddNewItem()");
            }
        }
        public List<CatalogItemPetsi> GetItems(){ return items; }
        public void SetItemList(List<CatalogItemPetsi> newItems)
        {
            items = newItems;
            SaveMainModel();
            NotifyModelServices();
        }
        public FileBehavior GetFileBehavior(){ return fileBehavior; }
        public override void ClearModel() {/* items.Clear();*/ }

        public override void AddItem(ModelUnitBase item)
        {
            SystemLogger.LogStatus($"Cmp Additem {((CatalogItemPetsi)item).ItemName} initiated");
            int count = items.Count;
            items.Add((CatalogItemPetsi)item);
            if(count+1 != items.Count)
            {
                SystemLogger.LogError($"AddOrder failed to add {((CatalogItemPetsi)item).ItemName}, count not incremented ", "Cmp AddItem()");
            }
            else
            {
                SaveMainModel();
                NotifyModelServices();
            }
        }
        public void RemoveItem(CatalogItemPetsi item)
        {
            SystemLogger.LogStatus($"Cmp RemoveItem {item.ItemName} initiated");
            int count = items.Count;
            items.Remove(item);
            if (count - 1 == items.Count)
            {
                UpdateModel();
            }
            else
            {
                SystemLogger.LogError($"remove {item.ItemName} from catalog failed, original count: {count} after operation: {items.Count}", "Cmp RemoveItem()");
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
            fileBehavior.DataListToFile(Identifiers.MAIN_MODEL_CATALOG_FILE, GetItems());
            fileBehavior.DataListToFile(Identifiers.MAIN_MODEL_CATALOG_CATEGORIES_FILE, Categories);
            SaveBackup();
        }

        private void SaveBackup()
        {
            string backupFp = null;
            backupFp = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_BACKUP_PATH);
            if (backupFp != null && backupFp != "") 
            {
                try
                {
                    File.WriteAllText(backupFp + "\\" + Identifiers.MAIN_MODEL_CATALOG_FILE, JsonConvert.SerializeObject(items));
                }
                catch (Exception ex) { ErrorService.RaiseExceptionHandlerError(ex.Message, "CatalogModel, SaveBackup"); }
            }
        }

        private void FinalizeMainModel()
        {
            List<CatalogItemPetsi> mainList = fileBehavior.BuildDataListFile<CatalogItemPetsi>(Identifiers.MAIN_MODEL_CATALOG_FILE);
            List<CatalogItemPetsi> squareList = new List<CatalogItemPetsi>(GetItems());
            List<CatalogItemPetsi> newList;
            if (mainList != null) {newList = new List<CatalogItemPetsi>(mainList); }
            else { newList = new List<CatalogItemPetsi>();}
 

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

        public override void CaptureEnvironment(FileBehavior reportFb)
        {
            //reportFb.DataListToFile(Identifiers.ENV_CMP, items);
            reportFb.DataListToPureFilePath(Identifiers.ENV_CMP, items);
            //Categories?
        }

        public void InitialLabelMapBoot()
        {
            SystemLogger.LogStatus("Cmp InitLabelMapBoot executed");
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

        public void LoadStartupFiles(List<(string fileName, string filePath)> FileList)
        {
            if(FileList == null || FileList.Count == 0) { return; }
            foreach (var fileListing in FileList)
            {
                if(fileListing.fileName == Identifiers.MAIN_MODEL_CATALOG_FILE)
                {
                    StartupLoadCatalog(fileListing.filePath);
                    fileBehavior.DataListToFile(Identifiers.MAIN_MODEL_CATALOG_FILE, GetItems());
                    StartupService.Instance.Deregister(this);
                }
            }
        }

        private void StartupLoadCatalog(string filePath)
        {
            string input;
            if (File.Exists(filePath))
            {
                input = File.ReadAllText(filePath);
                items = JsonConvert.DeserializeObject<List<CatalogItemPetsi>>(input);
            }        
        }
    }
}
