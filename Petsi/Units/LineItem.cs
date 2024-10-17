using Petsi.Managers;
using Petsi.Services;
using Petsi.Utils;
using Square.Models;

namespace Petsi.Units
{
    public class LineItem
    {
        /// <summary>
        /// OrderModel's BatchOrdersToOrderItems function corrects this variable to match Catalog api's catalog_obj_id. 
        /// Order api use's catalog api's variation id as it's catalog_obj_id.
        /// </summary>
        public string CatalogObjectId { get; set; }
        public string ItemName { get; set; }

        /// <summary>
        /// OrderModel's BatchOrdersToOrderItems function uses order api's catalog_obj_id as variation id. 
        /// Required to have matching ids across catalog and order models.
        /// </summary>
        public string VariationId { get; set; }

        /// <summary>
        /// <para>For pie category, variation name will be sizing: "Small - 5" serves 1-2,"Medium - 8"" serves 4-5", "Large 10" serves 8-10" </para>
        /// <para>For other category like pastry, variation name should be "Regular" </para>
        /// </summary>
        public string VariationName { get; set; }
        public string Quantity { get; set; }

        public LineItem() { }
        public LineItem(
            string catalogObjectId, string itemName,
            string variationId, string variationName,
            string quantity
            )
        {
            CatalogObjectId = catalogObjectId;
            ItemName = itemName;
            VariationId = variationId;
            VariationName = variationName;
            Quantity = quantity;
        }

        public PetsiOrderLineItem ToOnOrderLineItems()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Format(
                "   catalog obj id: " + CatalogObjectId + ", variation Id: " + VariationId + "\n" +
                "   " + ItemName + " " + VariationName + " " + Quantity
                );
        }

        public bool IsTakeNBake(CategoryService category, CatalogService catalog)
        {
            CatalogItemPetsi item = catalog.GetCatalogItemById(CatalogObjectId);
            if(item == null) //Merchandise items from square are transformed in SquareOrderInput, and the items aren't reflected or captured in the catalog
            {
                return false;
            }
            string x = category.GetCategoryIdByCategoryName("Take and Bake");
            return item.CategoryId == x;
        }

        public CatalogItemPetsi ToCatalogItemPetsi(string categoryId)
        {
            CatalogItemPetsi result = new CatalogItemPetsi(categoryId, CatalogObjectId, ItemName);
            result.VariationList.Add((VariationId, VariationName));

            return result;
        }
    }
}
