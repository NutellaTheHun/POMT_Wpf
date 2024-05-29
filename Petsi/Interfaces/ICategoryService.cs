namespace Petsi.Interfaces
{
    public interface ICategoryService
    {
        public List<string> GetCategoryNames();
        public string GetItemCategoryId(string categoryName);
        public string GetCategoryId(int categoryIndex);
    }
}
