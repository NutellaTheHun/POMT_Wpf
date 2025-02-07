﻿using Square.Exceptions;
using Square.Models;
using Square.Service;
using Petsi.Units;
using Petsi.Interfaces;
using Petsi.Utils;
using Petsi.Filing;
using Petsi.Managers;
using SystemLogging.Service;

namespace Petsi.Input
{
    public class SquareOrderInput : ModelInputBase
    {
        public static int LoggerOrderIdCount;

        List<SquareOrderItem> Orders { get; set; }
        List<BatchRetrieveOrdersResponse> squareResponses;
        SquareClientFactory squareClient;
        ICatalogService catalogLookup;
        ICategoryService categoryLookup;
        FileBehavior fileBehavior;
        bool isFileExecute;
        bool hasExecuted;
        public SquareOrderInput(SquareClientFactory squareClient)
        {
            LoggerOrderIdCount = 0;
            //SetModel(ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS));
            SetModel(ModelManagerSingleton.GetInstance().GetOrderModel());
            Orders = new List<SquareOrderItem>();

            catalogLookup = (ICatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            categoryLookup = (ICategoryService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATEGORY);

            this.squareClient = squareClient;
            fileBehavior = new FileBehavior(Identifiers.SQUARE_ORDER_INPUT);
            isFileExecute = false;
            hasExecuted = true;
            SetInputName(Identifiers.SQUARE_ORDER_INPUT);
            EnvironCaptureRegistrySingleton.GetInstance().Register(this);
            InputManagerSingleton.GetInstance().Register(this);
        }

        /// <summary>
        /// Has Test Execute mirror method (its separate, remember to update)
        /// </summary>
        /// <returns></returns>
        public override async Task Execute()
        {
            if (!isFileExecute)//if (hasExecuted)
            {
                squareResponses = await AsyncGetBatchOrderResponses();
                Orders = BatchOrdersToOrderItems();
            }
            foreach (var element in Orders)
            {
                Model.AddData(element.ToPetsiOrder());
            }
            hasExecuted = true;

            squareResponses = null;
            Orders = null;

            Model.Complete();
        }

        /// <summary>
        /// For Testing Only
        /// </summary>
        /// <param name="testFile"></param>
        /// <returns></returns>
        public void TestExecute(BatchRetrieveOrdersResponse testFile)
        {
            if (!isFileExecute)//if (hasExecuted)
            {
                //squareResponses = await AsyncGetBatchOrderResponses();
                squareResponses = new List<BatchRetrieveOrdersResponse>
                {
                    testFile
                };
                Orders = BatchOrdersToOrderItems();
            }
            foreach (var element in Orders)
            {
                Model.AddData(element.ToPetsiOrder());
            }
            hasExecuted = true;

            squareResponses = null;
            Orders = null;

            Model.Complete();
        }
        private async Task<List<BatchRetrieveOrdersResponse>> AsyncGetBatchOrderResponses()
        {
            List<BatchRetrieveOrdersResponse> result = new List<BatchRetrieveOrdersResponse>();
            List<string> orderIds = new List<string>();
            BatchRetrieveOrdersResponse bror;
            SearchOrdersResponse sor;
            /*
              string tempCursor = null;
            do
            {
                sor = await AsyncSquareSearchOrders(squareClient, tempCursor);
                tempCursor = sor.Cursor;

                orderIds.AddRange(sor.OrderEntries.Select(entry => entry.OrderId));

                bror = await AsyncSquareBatchRetrieveOrders(squareClient, orderIds);
                result.Add(bror);

                orderIds.Clear();
            }
            while (tempCursor != null);
             */
            
            //Village
            string tempCursor = null;
            do
            {
                sor = await AsyncSquareSearchOrders_LocId(squareClient, tempCursor, "L33TZWGMCAGX5");
                tempCursor = sor.Cursor;

                orderIds.AddRange(sor.OrderEntries.Select(entry => entry.OrderId));

                bror = await AsyncSquareBatchRetrieveOrders_LocId(squareClient, orderIds, "L33TZWGMCAGX5");
                result.Add(bror);

                orderIds.Clear();
            }
            while (tempCursor != null);

            //Chill
            do
            {
                sor = await AsyncSquareSearchOrders_LocId(squareClient, tempCursor, "LMM3H2WYN5K4W");
                tempCursor = sor.Cursor;

                orderIds.AddRange(sor.OrderEntries.Select(entry => entry.OrderId));

                bror = await AsyncSquareBatchRetrieveOrders_LocId(squareClient, orderIds, "LMM3H2WYN5K4W");
                result.Add(bror);

                orderIds.Clear();
            }
            while (tempCursor != null);
            return result;
        }
        public List<SquareOrderItem> BatchOrdersToOrderItems()
        {
            List<SquareOrderItem> listResult = new List<SquareOrderItem>();
            foreach (BatchRetrieveOrdersResponse bror in squareResponses)
            {
                foreach (var orderItem in bror.Orders)
                {
                    if (orderItem.Fulfillments[0].Type == Identifiers.FULFILLMENT_PICKUP)
                    {
                        //Time filter
                        if (DateTime.Parse(orderItem.Fulfillments[0].PickupDetails.PickupAt).Date < PetsiConfig.GetCurrentDate())
                            {
                            continue;
                        }
                    }
                    else if (orderItem.Fulfillments[0].Type == Identifiers.FULFILLMENT_DELIVERY)
                    {
                        //Time filter
                        if (DateTime.Parse(orderItem.Fulfillments[0].DeliveryDetails.DeliverAt).Date < PetsiConfig.GetCurrentDate())
                        {
                           continue;
                        }
                    }
                    
                    if (orderItem.Tenders != null)
                    {
                        if (orderItem.Tenders[0].Type == "CARD")
                        {
                            if (orderItem.Tenders[0].CardDetails.Status != "CAPTURED")
                            {
                                continue;
                            }
                        }
                    }        

                    SquareOrderItem item = new SquareOrderItem();
                    item.LineItems = new List<LineItem>();

                    item.Id = orderItem.Id;
                    item.LocationId = orderItem.LocationId;
                    item.Uid = orderItem.Fulfillments[0].Uid;
                    item.FulfillmentType = orderItem.Fulfillments[0].Type;
                    if (item.FulfillmentType == Identifiers.FULFILLMENT_PICKUP)
                    {
                        item.Pickup_time = orderItem.Fulfillments[0].PickupDetails.PickupAt;
                        item.Note = orderItem.Fulfillments[0].PickupDetails.Note;
                        item.RecipientName = orderItem.Fulfillments[0].PickupDetails.Recipient.DisplayName;
                    }
                    else if (item.FulfillmentType == Identifiers.FULFILLMENT_DELIVERY)
                    {
                        item.Pickup_time = orderItem.Fulfillments[0].DeliveryDetails.DeliverAt;
                        item.Note = orderItem.Fulfillments[0].DeliveryDetails.Note;
                        item.RecipientName = orderItem.Fulfillments[0].DeliveryDetails.Recipient.DisplayName;
                        item.Address = BuildAddressLine(orderItem.Fulfillments[0].DeliveryDetails.Recipient.Address);
                        item.PhoneNumber = orderItem.Fulfillments[0].DeliveryDetails.Recipient.PhoneNumber;
                    }

                    foreach (var lineItem in orderItem.LineItems)
                    {
                        item.LineItems.AddRange(ParseOrderLineItem(lineItem));
                    }
                    listResult.Add(item);
                }
            }
            return listResult;
        }
        private string BuildAddressLine(Address deliveryDetails)
        {
            string addr1 = deliveryDetails.AddressLine1;
            string addr2 = deliveryDetails.AddressLine2;
            string locality = deliveryDetails.Locality;
            string state = deliveryDetails.AdministrativeDistrictLevel1;
            string zip = deliveryDetails.PostalCode;
            return $"{addr1} {addr2} {locality}, {state} {zip}";
        }
        private List<LineItem> ParseOrderLineItem(OrderLineItem squareOrderlineItem)
        {

            List<LineItem> lineItems = new List<LineItem>();
            //If item is a box of..., must be parsed in modifers section to build line item.
            if (squareOrderlineItem.CatalogObjectId == Identifiers.MODIFY_BOX_OF_6_COOKIES
                ||
                squareOrderlineItem.CatalogObjectId == Identifiers.MODIFY_BOX_OF_6_MUFFINS
                ||
                squareOrderlineItem.CatalogObjectId == Identifiers.MODIFY_BOX_OF_6_SCONES)
            {
                string catalogObjId, varId;
                foreach (var modifier in squareOrderlineItem.Modifiers)
                {
                    if(modifier.Name == Identifiers.MODIFY_NAME_NOTE_CARD) { continue; }

                    LineItem boxedli = new LineItem();
                    boxedli.ItemName = catalogLookup.ValidateModifyItemName(modifier.Name); //Will create a new item in catalog if no match is found.

                    boxedli.CatalogObjectId = catalogLookup.GetCatalogObjectId(boxedli.ItemName);
                    if(boxedli.CatalogObjectId == "")//will return "" if ValidateModifyItemName finds multiple results.
                    {
                        //Temporary ID for an item when a multiItem match event occurs, is resolved after user intervention window. (NotifyCatalogValidateMultiItemView.cs)
                        boxedli.CatalogObjectId = Identifiers.SOI_MULTI_ITEM_MATCH_EVENT_ID_SIG;
                    }

                    boxedli.VariationId = "modifierItem"+ boxedli.ItemName;
                    boxedli.VariationName = Identifiers.SIZE_REGULAR;

                    boxedli.Quantity = (int.Parse(modifier.Quantity) * int.Parse(squareOrderlineItem.Quantity)).ToString();

                    lineItems.Add(boxedli);
                }
            }
            //If a scone, must be parsed like a box of... item, except scone flavors dont exist in catalog.
            else if (squareOrderlineItem.CatalogObjectId == Identifiers.MODIFY_SCONE)
            {
                foreach (var modifier in squareOrderlineItem.Modifiers)
                {
                    LineItem sconeLi = new LineItem();
                    sconeLi.ItemName = catalogLookup.ValidateModifyItemName(modifier.Name); //Will create a new item in catalog if no match is found.

                    sconeLi.VariationId = squareOrderlineItem.CatalogObjectId;

                    sconeLi.CatalogObjectId = catalogLookup.GetCatalogObjectId(sconeLi.ItemName);
                    if (sconeLi.CatalogObjectId == "") //will return "" if ValidateModifyItemName finds multiple results.
                    {
                        //Temporary ID for an item when a multiItem match event occurs, is resolved after user intervention window. (NotifyCatalogValidateMultiItemView.cs)
                        sconeLi.CatalogObjectId = Identifiers.SOI_MULTI_ITEM_MATCH_EVENT_ID_SIG;
                    }

                    sconeLi.VariationName = Identifiers.SIZE_REGULAR;
                   
                    sconeLi.Quantity = squareOrderlineItem.Quantity;
                    lineItems.Add(sconeLi);
                }
            }
            else if (squareOrderlineItem.VariationName == Identifiers.BOX_OF_SCONES_VARIATION)
            {
                LineItem sconeLi = new LineItem();
                sconeLi.ItemName = "Special! Strawberry Basil Scones";
                sconeLi.VariationId = squareOrderlineItem.CatalogObjectId;
                sconeLi.CatalogObjectId = sconeLi.ItemName;
                sconeLi.VariationName = Identifiers.SIZE_REGULAR;

                
                sconeLi.Quantity = (6 * int.Parse(squareOrderlineItem.Quantity)).ToString();
                lineItems.Add(sconeLi);
            }
            else if (squareOrderlineItem.CatalogObjectId == Identifiers.BOX_OF_6_BACON_BISCUITS)
            {
                LineItem bisc = new LineItem();
                bisc.ItemName = "Bacon Cheddar Scallion Biscuit";
                bisc.CatalogObjectId = catalogLookup.GetCatalogObjectId(bisc.ItemName);
                bisc.VariationId = "VWA4YKGMLWXG5P5B3CE2NFBE";
                bisc.VariationName = Identifiers.SIZE_REGULAR;
                bisc.Quantity = (6*int.Parse(squareOrderlineItem.Quantity)).ToString();
                
                lineItems.Add(bisc);
            }
            else if (squareOrderlineItem.CatalogObjectId == Identifiers.BOX_OF_6_BLUEBERRY_MUFFINS)
            {
                LineItem muff = new LineItem();
                muff.ItemName = "Blueberry Muffin";
                muff.CatalogObjectId = catalogLookup.GetCatalogObjectId(muff.ItemName);
                muff.VariationId = "63N53J6WJ42OE4QAVHDVIZW4";
                muff.VariationName = Identifiers.SIZE_REGULAR;
                muff.Quantity = (6 * int.Parse(squareOrderlineItem.Quantity)).ToString();
                
                lineItems.Add(muff);
            }
            else if(categoryLookup.GetCategoryId(squareOrderlineItem.CatalogObjectId) == Identifiers.CATEGORY_MERCH)
            {
                LineItem merch = new LineItem();
                if(squareOrderlineItem.VariationName != "Regular")
                {
                    
                     //Creme De Liqueur Ice Cream: 
                     //   --Vanilla Horchata, Pistachio Amaretto, ...
                    
                    if (squareOrderlineItem.Name == "Creme De Liqueur Ice Cream")
                    {
                        merch.ItemName = squareOrderlineItem.VariationName;
                        merch.CatalogObjectId = squareOrderlineItem.CatalogObjectId;
                        merch.VariationId = squareOrderlineItem.CatalogObjectId;
                        merch.VariationName = "Regular";
                        merch.Quantity = squareOrderlineItem.Quantity;
                    }
                    
                    //Petsi Pies Hats: 
                    //   --Orange (Eat More Pie)
                    //   --Blue (Pies Is Love)
                    
                    else if (squareOrderlineItem.Name == "Petsi Pies Hats")
                    {
                        merch.ItemName = $"Petsi Pie Hat {squareOrderlineItem.VariationName.Split(" ")[0]}";
                        merch.CatalogObjectId = squareOrderlineItem.CatalogObjectId;
                        merch.VariationId = squareOrderlineItem.CatalogObjectId;
                        merch.VariationName = "Regular";
                        merch.Quantity = squareOrderlineItem.Quantity;
                    }
                    
                    //Tees: 
                     // --Small Crew Tee - "Delicious"
                     // --Small Crew Neck Tee - "Pie Is Love"
                     // --Med V-Neck Tee "Pie Is Love"
                     // --Med Crew Neck Tee - "Delicious"
                     // --Large V-Neck Tee - "Pie is Love"
                     // --Large Crew Neck Tee - "Deliciouso"
                    //  --XL V-Neck Tee - "Pie is Love"
                     // --XL Crew Neck Tee - "Delicious"
                     // --2XL Crew Tee - "Delicious"
                    
                    else if(squareOrderlineItem.Name == "Tees")
                    {
                        merch.ItemName = squareOrderlineItem.VariationName;
                        merch.CatalogObjectId = squareOrderlineItem.CatalogObjectId;
                        merch.VariationId = squareOrderlineItem.CatalogObjectId;
                        merch.VariationName = "Regular";
                        merch.Quantity = squareOrderlineItem.Quantity;
                    }
                    
                    //Petsi T-Shirt: 
                    //  --XS, SMALL, MEDIUM, LARGE, XL, 2XL, 3XL
                    
                    else if(squareOrderlineItem.Name == "Petsi T-Shirt")
                    {
                        merch.ItemName = $"{squareOrderlineItem.Name} {squareOrderlineItem.VariationName}";
                        merch.CatalogObjectId = squareOrderlineItem.CatalogObjectId;
                        merch.VariationId = squareOrderlineItem.CatalogObjectId;
                        merch.VariationName = "Regular";
                        merch.Quantity = squareOrderlineItem.Quantity;
                    }
                    else
                    {
                        Logger.LogError("Merch name not matched", "SquareOrderInput ParseOrderLineItem()");
                    }
                }
                else
                {
                    //Note Card by Sarah Dudek (no variation =  regular?)
                    //Oven Mitt ````
                    //Petsi Pie Server
                    //Tote Bag
                    //Travel Mug
                    merch.ItemName = squareOrderlineItem.Name;
                    merch.VariationId = squareOrderlineItem.CatalogObjectId;
                    merch.CatalogObjectId = catalogLookup.GetCatalogObjectId(merch.VariationId);
                    merch.VariationName = squareOrderlineItem.VariationName;
                    merch.Quantity = squareOrderlineItem.Quantity;
                }
                lineItems.Add(merch);
                ModelManagerSingleton.GetInstance().GetCatalogModel().TryUpdateSquareMerchItem(merch);
            }

            //all other "standard" items
            else
            {
                LineItem stdli = new LineItem();
                stdli.VariationId = squareOrderlineItem.CatalogObjectId;
                stdli.CatalogObjectId = catalogLookup.GetCatalogObjectId(stdli.VariationId);
                //check if returns "" and handle?

                stdli.VariationName = squareOrderlineItem.VariationName;
                stdli.ItemName = squareOrderlineItem.Name;
                stdli.Quantity = squareOrderlineItem.Quantity;
                lineItems.Add(stdli);
            }
            if(squareOrderlineItem.Modifiers != null)
            {
                foreach (var modifier in squareOrderlineItem.Modifiers)
                {
                    if (modifier.Name == Identifiers.MODIFY_NAME_NOTE_CARD)
                    {
                        lineItems.Add(catalogLookup.GetRegularItem("Note Card by Sarah Dudek", 1));
                    }
                }     
            }
            return lineItems;
        }
        /// <summary>
        /// Returned object contains a list of objects each containing an order ID. Max limit of objects is set to 100. 
        /// Limit is set in BuildSearchOrdersRequestBody, the 100 limit is necessary for squares BatchRetrieveOrders api call can only
        /// call 100 orders at a time.
        /// </summary>
        /// <param name="squareClient"></param>
        /// <param name="cursor"></param>
        /// <returns></returns>
        private async Task<SearchOrdersResponse> AsyncSquareSearchOrders(SquareClientFactory squareClient, string? cursor)
        {
            SearchOrdersResponse? result = null;

            SearchOrdersRequest body = BuildSearchOrdersRequestBody(
                cursor,
                new List<string> { "L33TZWGMCAGX5", "LMM3H2WYN5K4W" },
                new List<string> { "OPEN"/*, "COMPLETED"*/ },
                new List<string> { "PICKUP", "DELIVERY" }
                );

            try
            {
                result = await squareClient.SqClient.OrdersApi.SearchOrdersAsync(body: body);
            }
            catch (ApiException e)
            {
                Console.WriteLine("Failed to make the request");
                Console.WriteLine($"Response Code: {e.ResponseCode}");
                Console.WriteLine($"Exception: {e.Message}");
            }
            return result;
        }

        private async Task<SearchOrdersResponse> AsyncSquareSearchOrders_LocId(SquareClientFactory squareClient, string? cursor, string locationId)
        {
            SearchOrdersResponse? result = null;
            SearchOrdersRequest body = BuildSearchOrdersRequestBody(
                cursor,
                new List<string> { locationId },
                //new List<string> { "L33TZWGMCAGX5", "LMM3H2WYN5K4W" },
                new List<string> { "OPEN"/*, "COMPLETED"*/ },
                new List<string> { "PICKUP", "DELIVERY" }
                );

            try
            {
                result = await squareClient.SqClient.OrdersApi.SearchOrdersAsync(body: body);
            }
            catch (ApiException e)
            {
                Logger.LogError("Failed to make the request", "AsyncSquareSearchOrders");
                Logger.LogError($"Response Code: {e.ResponseCode}", "AsyncSquareSearchOrders");
                Logger.LogError($"Exception: {e.Message}", "AsyncSquareSearchOrders");
            }
            return result;
        }
        /// <summary>
        /// Returned object contains all the order information from the given list of order IDs, max amount of IDs per call is 100.
        /// Limit is controlled in BuildSearchOrdersRequestBody function which is called in AsyncSquareSearchOrders function.
        /// </summary>
        /// <param name="squareClient"></param>
        /// <param name="sourceOrderIds"></param>
        /// <returns></returns>
        public async Task<BatchRetrieveOrdersResponse> AsyncSquareBatchRetrieveOrders(SquareClientFactory squareClient, List<string> sourceOrderIds)
        {
            BatchRetrieveOrdersResponse? result = null;

            var body = new BatchRetrieveOrdersRequest.Builder(orderIds: sourceOrderIds)
              .LocationId("L33TZWGMCAGX5")
              .Build();

            try
            {
                result = await squareClient.SqClient.OrdersApi.BatchRetrieveOrdersAsync(body: body);
            }
            catch (ApiException e)
            {
                Console.WriteLine("Failed to make the request");
                Console.WriteLine($"Response Code: {e.ResponseCode}");
                Console.WriteLine($"Exception: {e.Message}");
            }
            /*
            BatchRetrieveOrdersResponse? chillResult = null;

            var chillBody = new BatchRetrieveOrdersRequest.Builder(orderIds: sourceOrderIds)
              .LocationId("LMM3H2WYN5K4W")
              .Build();

            try
            {
                chillResult = await squareClient.SqClient.OrdersApi.BatchRetrieveOrdersAsync(body: chillBody);
            }
            catch (ApiException e)
            {
                Console.WriteLine("Failed to make the request");
                Console.WriteLine($"Response Code: {e.ResponseCode}");
                Console.WriteLine($"Exception: {e.Message}");
            }

            villageResult.Orders.Add(chillResult.Orders as Order);*/
            return result;
        }

        public async Task<BatchRetrieveOrdersResponse> AsyncSquareBatchRetrieveOrders_LocId(SquareClientFactory squareClient, List<string> sourceOrderIds, string locationId)
        {
            BatchRetrieveOrdersResponse? result = null;

            var body = new BatchRetrieveOrdersRequest.Builder(orderIds: sourceOrderIds)
              .LocationId(locationId)
              .Build();

            try
            {
                result = await squareClient.SqClient.OrdersApi.BatchRetrieveOrdersAsync(body: body);
            }
            catch (ApiException e)
            {
                Console.WriteLine("Failed to make the request");
                Console.WriteLine($"Response Code: {e.ResponseCode}");
                Console.WriteLine($"Exception: {e.Message}");
            }
            return result;
        }
        private SearchOrdersRequest BuildSearchOrdersRequestBody(string? cursor, List<string> locationIds, List<string> states, List<string> fulfillmentTypes)
        {
            var stateFilter = new SearchOrdersStateFilter.Builder(states: states)
              .Build();

            var fulfillmentFilter = new SearchOrdersFulfillmentFilter.Builder()
                .FulfillmentTypes(fulfillmentTypes)
                .Build();

            var filter = new SearchOrdersFilter.Builder()
              .StateFilter(stateFilter)
              //.DateTimeFilter(dateTimeFilter)
              .FulfillmentFilter(fulfillmentFilter)
              .Build();

            var query = new SearchOrdersQuery.Builder()
              .Filter(filter)
              .Build();


            SearchOrdersRequest.Builder body = new SearchOrdersRequest.Builder()
            .LocationIds(locationIds)
            .Query(query)
            .Limit(100)
            .ReturnEntries(true);

            if (cursor != null)
            {
                body.Cursor(cursor);
            }

            return body.Build();
        }
        private async Task AsyncInitialize()
        {
            List<string> orderIds = new List<string>();
            BatchRetrieveOrdersResponse bror;
            SearchOrdersResponse sor;
            string tempCursor = null;
            do
            {
                sor = await AsyncSquareSearchOrders(squareClient, tempCursor);
                tempCursor = sor.Cursor;

                orderIds.AddRange(sor.OrderEntries.Select(entry => entry.OrderId));


                bror = await AsyncSquareBatchRetrieveOrders(squareClient, orderIds);

                orderIds.Clear();
            }
            while (tempCursor != null);
            LoggerOrderIdCount = Orders.Count;
        }
        public FileBehavior GetFileBehavior(){  return fileBehavior; }
        public List<SquareOrderItem> GetOrders() { return Orders; }
        public void SetOrders(List<SquareOrderItem> newOrders) { Orders = newOrders; }
        public List<BatchRetrieveOrdersResponse> GetSquareResponses() { return squareResponses; }
        public void SetSquareResponses(List<BatchRetrieveOrdersResponse> newResponse) { squareResponses = newResponse; }
        public void SetIsFileExecute(bool v) { isFileExecute = v; }
        public bool GetHasExecuted() { return hasExecuted ; }
        //public void SetHasExecuted(bool v) {  hasExecuted = v; }

        private string ToRFC3339(string date) => date + "T00:00:00";
        public override void CaptureEnvironment(FileBehavior reportFb) {/*reportFb.DataListToFile(Identifiers.ENV_SOI, squareResponses);*/ reportFb.DataListToPureFilePath(Identifiers.ENV_SOI, squareResponses); }

        /// <summary>
        /// Wrapper for Square API call Search Orders https://developer.squareup.com/reference/square/orders-api/search-orders,
        /// Paginates per 100 items
        /// </summary>
        /// <param name="squareClient"></param>
        /// <param name="locationIds"></param>
        /// <param name="states"></param>
        /// <param name="createStartAt">format YYYY-MM-DD"</param>
        /// <param name="createEndAt">format YYYY-MM-DD</param>
        /// <returns></returns>
        public async Task<SearchOrdersResponse?> NEWAsyncSquareSearchOrders(SquareClientFactory squareClient, string? cursor, List<string> locationIds, List<string> states, string? createStartAt, string? createEndAt)
        {
            var stateFilter = new SearchOrdersStateFilter.Builder(states: states)
              .Build();

            var createdAtBuilder = new TimeRange.Builder();

            if(createStartAt != null){ createdAtBuilder.StartAt(ToRFC3339(createStartAt)); }
            if (createEndAt != null){ createdAtBuilder.EndAt(ToRFC3339(createEndAt)); }

            var createdAt = createdAtBuilder.Build();

            var dateTimeFilterBuilder = new SearchOrdersDateTimeFilter.Builder();
            if(createStartAt != null || createEndAt!= null)
            {
                dateTimeFilterBuilder.CreatedAt(createdAt);
            }
            var dateTimeFilter = dateTimeFilterBuilder.Build();

            var filter = new SearchOrdersFilter.Builder()
              .StateFilter(stateFilter)
              .DateTimeFilter(dateTimeFilter)
              .Build();

            var query = new SearchOrdersQuery.Builder()
              .Filter(filter)
              .Build();

            var bodyBuilder = new SearchOrdersRequest.Builder()
              .LocationIds(locationIds)
              .Query(query)
              .Limit(100)
              .ReturnEntries(true);

            if (cursor != null) { bodyBuilder.Cursor(cursor); }

            var body = bodyBuilder.Build();

            try
            {
                return await squareClient.SqClient.OrdersApi.SearchOrdersAsync(body: body);
            }
            catch (ApiException e)
            {
                throw new Exception($"Failed to make the request \n Response Code: {e.ResponseCode} \n Exception: {e.Message}");
            }
        }

        /// <summary>
        /// Wrapper for Square Order API call BatchRetrieve Orders https://developer.squareup.com/reference/square/orders-api/batch-retrieve-orders
        /// </summary>
        /// <param name="squareClient"></param>
        /// <param name="orderIds"></param>
        /// <param name="locationId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<BatchRetrieveOrdersResponse> NEWAsyncSquareBatchRetrieveOrders(SquareClientFactory squareClient, List<string> orderIds, string locationId)
        {
            var body = new BatchRetrieveOrdersRequest.Builder(orderIds: orderIds)
              .LocationId(locationId)
              .Build();
            try
            {
                return await squareClient.SqClient.OrdersApi.BatchRetrieveOrdersAsync(body: body);
            }
            catch (ApiException e)
            {
                throw new Exception($"Failed to make the request \n Response Code: {e.ResponseCode} \n Exception: {e.Message}");
            }
        }

        public async Task<List<PetsiOrder>> AsyncGetSquareOrders(SquareClientFactory squareClient, List<string> locationIds, List<string> states, string? createStartAt, string? createEndAt, string? fulfillStartAt, string? fulfillEndAt)
        {
            List<PetsiOrder> result = new List<PetsiOrder>();
            List<string> orderIds = new List<string>();
            SearchOrdersResponse? sor;
            BatchRetrieveOrdersResponse bror;
            string tempCursor = null;

            foreach (string locId in locationIds) 
            {
                do
                {
                    Console.WriteLine($"SearchOrders : cursor: {tempCursor}");
                    sor = await NEWAsyncSquareSearchOrders(squareClient, tempCursor, new List<string>{ locId }, states, createStartAt, createEndAt);
                    tempCursor = sor.Cursor;

                    orderIds.AddRange(sor.OrderEntries.Select(entry => entry.OrderId));

                    Console.WriteLine($"RetrieveOrders start");
                    bror = await NEWAsyncSquareBatchRetrieveOrders(squareClient, orderIds, locId);
                    Console.WriteLine($"BuildOrders start");
                    result.AddRange(BuildPetsiOrders(bror, fulfillStartAt, fulfillEndAt));
                    
                    orderIds.Clear();

                } while (tempCursor != null);
            }
           
            return result;
        }

        private List<PetsiOrder> BuildPetsiOrders(BatchRetrieveOrdersResponse bror, string? fulfillStartAt, string? fulfillEndAt) 
        {
            List<PetsiOrder> result = new List<PetsiOrder>();
            foreach (var orderItem in bror.Orders)
            {   
                //To restrictive
                if(fulfillStartAt != null && fulfillEndAt != null)
                {
                    if(orderItem.Fulfillments == null) { continue; }

                    if (orderItem.Fulfillments[0].Type == Identifiers.FULFILLMENT_PICKUP)
                    {
                        //Time filter
                        /*
                        if (DateTime.Parse(orderItem.Fulfillments[0].PickupDetails.PickupAt).Date < DateTime.Now.Date)
                        {
                            continue;
                        }*/
                        if (DateTime.Parse(orderItem.Fulfillments[0].PickupDetails.PickupAt).Date < DateTime.Parse(fulfillStartAt).Date)
                        {
                            continue;
                        }
                        if (DateTime.Parse(orderItem.Fulfillments[0].PickupDetails.PickupAt).Date > DateTime.Parse(fulfillEndAt).Date)
                        {
                            continue;
                        }
                    }
                    else if (orderItem.Fulfillments[0].Type == Identifiers.FULFILLMENT_DELIVERY)
                    {
                        //Time filter
                        /*
                        if (DateTime.Parse(orderItem.Fulfillments[0].DeliveryDetails.DeliverAt).Date < DateTime.Now.Date)
                        {
                            continue;
                        }*/
                        if (DateTime.Parse(orderItem.Fulfillments[0].DeliveryDetails.DeliverAt).Date < DateTime.Parse(fulfillStartAt).Date)
                        {
                            continue;
                        }
                        if (DateTime.Parse(orderItem.Fulfillments[0].DeliveryDetails.DeliverAt).Date > DateTime.Parse(fulfillEndAt).Date)
                        {
                            continue;
                        }
                    }
                }

                if (orderItem.Tenders != null)
                {
                    if (orderItem.Tenders[0].Type == "CARD")
                    {
                        if (orderItem.Tenders[0].CardDetails.Status != "CAPTURED")
                        {
                            continue;
                        }
                    }
                }

                SquareOrderItem item = new SquareOrderItem();
                item.LineItems = new List<LineItem>();

                item.Id = orderItem.Id;
                item.LocationId = orderItem.LocationId;
                item.Uid = orderItem.Fulfillments[0].Uid;
                item.FulfillmentType = orderItem.Fulfillments[0].Type;
                if (item.FulfillmentType == Identifiers.FULFILLMENT_PICKUP)
                {
                    item.Pickup_time = orderItem.Fulfillments[0].PickupDetails.PickupAt;
                    item.Note = orderItem.Fulfillments[0].PickupDetails.Note;
                    item.RecipientName = orderItem.Fulfillments[0].PickupDetails.Recipient.DisplayName;
                }
                else if (item.FulfillmentType == Identifiers.FULFILLMENT_DELIVERY)
                {
                    item.Pickup_time = orderItem.Fulfillments[0].DeliveryDetails.DeliverAt;
                    item.Note = orderItem.Fulfillments[0].DeliveryDetails.Note;
                    item.RecipientName = orderItem.Fulfillments[0].DeliveryDetails.Recipient.DisplayName;
                    //del address
                }

                foreach (var lineItem in orderItem.LineItems)
                {
                    item.LineItems.AddRange(ParseOrderLineItem(lineItem));
                }

                result.Add(item.ToPetsiOrder());
            }

            return result;
        }

    }
}


