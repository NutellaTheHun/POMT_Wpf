using Petsi.Units;

namespace Petsi.Interfaces
{
    public interface ICatalogService
    {
        public string GetCatalogObjectId(string variationIdOrItemName);
        bool IsModifyItem(string catalogObjectId);
        string ValidateModifyItemName(string name);
        public bool NameExists(string input);
        public List<string> NameContains(string inputName);
        public string GenerateCatalogId();
        LineItem GetRegularItem(string v1, int v2);
        public List<string> GetItemIdsByCategory(string targetCategoryId);
        public CatalogItemPetsi GetCatalogItem(string itemName);
    }
}
