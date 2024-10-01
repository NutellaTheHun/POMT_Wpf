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
        List<(string name, string id)> Categories;
        List<CatalogItemPetsi> catalogItems;
        List<ListCatalogResponse> squareResponses;
        FileBehavior fileBehavior;
        //FileBehavior environFileBehavior;
        bool isFileExecute;

        protected CatalogModelPetsi Model;

        private bool hasExecuted;

        public SquareCatalogInput(SquareClientFactory squareClient)
        {
            catalogItems = new List<CatalogItemPetsi>();
            Categories = new List<(string name, string id)>();
            SetModel(ModelManagerSingleton.GetInstance().GetCatalogModel());
            this.squareClient = squareClient;
            fileBehavior = new FileBehavior(Identifiers.SQUARE_CATALOG_INPUT);
            isFileExecute = false;
            hasExecuted = false;
            SetInputName(Identifiers.SQUARE_CATALOG_INPUT);
            EnvironCaptureRegistrySingleton.GetInstance().Register(this);
            InputManagerSingleton.GetInstance().Register(this);
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
                            newCatalogItem.VariationList.Add((variation.Id, variation.ItemVariationData.Name));
                            //result.Add(newCatalogItem);
                        }
                        result.Add(newCatalogItem);
                    }
                }
            }
            return result;
        }
 
        public List<ListCatalogResponse> GetSquareResponses() { return squareResponses; }
        public List<CatalogItemPetsi> GetCatalogItems(){ return catalogItems; }
        public void SetIsFileExecute(bool v){ isFileExecute = v; }
        public void SetSquareResponse(List<ListCatalogResponse> responseList){ squareResponses = responseList; }
        public FileBehavior GetFileBehavior(){ return fileBehavior;}
        public void SetCatalogItems(List<CatalogItemPetsi> itemList){ catalogItems = itemList; }
        public bool GetHasExecuted() { return hasExecuted; }
        //public void SetHasExecuted(bool v) { hasExecuted = v; }
        public override void CaptureEnvironment(FileBehavior reportFb){/*reportFb.DataListToFile(Identifiers.ENV_SCI, squareResponses);*/ reportFb.DataListToPureFilePath(Identifiers.ENV_SCI, squareResponses); }
    }
}
