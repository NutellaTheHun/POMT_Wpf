using Petsi.CommandLine;
using Petsi.Filing;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;
using Square.Exceptions;
using Square.Models;
using Square.Service;

namespace Petsi.Input
{
    public class SquareCatalogInput : ModelInputBase
    {
        SquareClientFactory squareClient;
        List<(string, string)> Categories;
        List<CatalogItemPetsi> catalogItems;
        List<ListCatalogResponse> squareResponses;
        FileBehavior dataFileBehavior;
        FileBehavior environFileBehavior;
        bool isFileExecute;
        CatalogInputFrameBehavior frameBehavior;

        protected CatalogModelPetsi Model;

        public static int LoggerCatalogObjectsProcessedCount;
        public static int LoggerCatalogObjectsNullItemDataCount;
        public static int LoggerPaginations;

        private bool hasExecuted;

        public SquareCatalogInput(SquareClientFactory squareClient)
        {
            LoggerCatalogObjectsProcessedCount = 0;
            LoggerCatalogObjectsNullItemDataCount = 0;
            LoggerPaginations = 0;

            frameBehavior = new CatalogInputFrameBehavior(this);
            catalogItems = new List<CatalogItemPetsi>();
            Categories = new List<(string, string)>();
            SetModel(ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG));
            this.squareClient = squareClient;
            dataFileBehavior = new FileBehavior("SquareCatalogInput");
            isFileExecute = false;
            hasExecuted = false;
            SetInputName(Identifiers.SQUARE_CATALOG_INPUT);
            EnvironCaptureRegistrySingleton.GetInstance().Register(this);
            InputManagerSingleton.GetInstance().Register(this);
            CommandFrame.GetInstance().RegisterFrame("sci", frameBehavior);
        }
        protected override void SetModel(ModelBase targetModel)
        {
            Model = (CatalogModelPetsi)targetModel;
        }
        public override async Task Execute()
        {
            if (!isFileExecute)
            {
                squareResponses = await AsyncGetSquareCatalogResponses(squareClient, null);
                catalogItems = CatalogResponseToCatalogPetsiItems(squareResponses);
            }

            foreach (CatalogItemPetsi catalogItem in catalogItems)
            {
                Model.AddData(catalogItem);
            }
            Model.SetCategoryList(Categories);
            hasExecuted = true;
            Model.Complete();
        }
        private async Task<List<ListCatalogResponse>> AsyncGetSquareCatalogResponses(SquareClientFactory scf, string? cursor)
        {
            List<ListCatalogResponse> result = new List<ListCatalogResponse>();
            string currentCursor = cursor;
            do
            {
                try
                {
                    ListCatalogResponse response = await scf.SqClient.CatalogApi.ListCatalogAsync(currentCursor);
                    currentCursor = response.Cursor;
                    result.Add(response);
                    LoggerPaginations++;
                }
                catch (ApiException e)
                {
                    Console.WriteLine("Failed to make the request");
                    Console.WriteLine($"Response Code: {e.ResponseCode}");
                    Console.WriteLine($"Exception: {e.Message}");
                }
            } while (currentCursor != null);
            return result;
        }
        public List<CatalogItemPetsi> CatalogResponseToCatalogPetsiItems(List<ListCatalogResponse> responses)
        {
            List<CatalogItemPetsi> result = new List<CatalogItemPetsi>();
            foreach (ListCatalogResponse response in responses)
            {
                foreach (var sqrCatalogItem in response.Objects)
                {
                    if(sqrCatalogItem.Type == "CATEGORY")
                    {
                        Categories.Add((sqrCatalogItem.CategoryData.Name, sqrCatalogItem.Id));
                    }
                    if (sqrCatalogItem.Type == "ITEM")
                    //if (sqrCatalogItem.ItemData != null)
                    {
                        //--
                        string categoryId = "";
                        if (sqrCatalogItem.ItemData.Categories != null)
                        {
                            categoryId = sqrCatalogItem.ItemData.Categories[0].Id;
                        }
                        //--
                        var newCatalogItem = new CatalogItemPetsi(categoryId, sqrCatalogItem.Id, sqrCatalogItem.ItemData.Name);
                        foreach (var variation in sqrCatalogItem.ItemData.Variations)
                        {
                            newCatalogItem.Variations.Add(variation.Id, variation.ItemVariationData.Name);
                            result.Add(newCatalogItem);
                        }
                        LoggerCatalogObjectsProcessedCount++;
                    }
                    else
                    {
                        LoggerCatalogObjectsNullItemDataCount++;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Pulls catalog data from squares API using ListCatalog. Uses pagination to pull in batches, controlled with cursor string.
        /// </summary>
        /// <param name="scf"> Builds a square client, used to make square API calls</param>
        /// <param name="cursor">ListCatalog returns 100 listings max, if there're more items, a cursor string is returned, 
        /// if ListCatalog is called with cursor string as param the next batch is returned</param>
        /// <returns></returns>
        private async Task AsyncGetSquareCatalog(SquareClientFactory scf, string? cursor) //add cursor repeat
        {
            string currentCursor = cursor;
            do
            {
                try
                {
                    ListCatalogResponse result = await scf.SqClient.CatalogApi.ListCatalogAsync(currentCursor);

                    //Used to get ListCatalogResponse as a file for testing.
                    //ToFile.Send(result, "D:/Git-Repos/Petsi/Petsi/tests/lcr.txt");

                    currentCursor = result.Cursor;
                    foreach (var sqrCatalogItem in result.Objects)
                    {
                        if (sqrCatalogItem.ItemData != null)
                        {
                            //--
                            string categoryId = "";
                            if (sqrCatalogItem.ItemData.Categories != null)
                            {
                                categoryId = sqrCatalogItem.ItemData.Categories[0].Id;
                            }
                            //--
                            var newCatalogItem = new CatalogItemPetsi(categoryId, sqrCatalogItem.Id, sqrCatalogItem.ItemData.Name);
                            foreach (var variation in sqrCatalogItem.ItemData.Variations)
                            {
                                newCatalogItem.Variations.Add(variation.Id, variation.ItemVariationData.Name);
                                catalogItems.Add(newCatalogItem);
                            }
                            LoggerCatalogObjectsProcessedCount++;
                        }
                        else
                        {
                            LoggerCatalogObjectsNullItemDataCount++;
                        }
                    }
                }
                catch (ApiException e)
                {
                    Console.WriteLine("Failed to make the request");
                    Console.WriteLine($"Response Code: {e.ResponseCode}");
                    Console.WriteLine($"Exception: {e.Message}");
                }
                LoggerPaginations++;
            } while (currentCursor != null);

        }
        public List<ListCatalogResponse> GetSquareResponses() { return squareResponses; }
        public List<CatalogItemPetsi> GetCatalogItems(){ return catalogItems; }
        public void SetIsFileExecute(bool v){ isFileExecute = v; }
        public void SetSquareResponse(List<ListCatalogResponse> responseList){ squareResponses = responseList; }
        public FileBehavior GetFileBehavior(){ return dataFileBehavior;}
        public void SetCatalogItems(List<CatalogItemPetsi> itemList){ catalogItems = itemList; }
        public override FrameBehaviorBase GetFrameBehavior(){ return frameBehavior; }
        public bool GetHasExecuted() { return hasExecuted; }
        public void SetHasExecuted(bool v) { hasExecuted = v; }
        public override void CaptureEnvironment(FileBehavior reportFb){reportFb.DataListToFile(Identifiers.ENV_SCI, squareResponses); }
    }
}
