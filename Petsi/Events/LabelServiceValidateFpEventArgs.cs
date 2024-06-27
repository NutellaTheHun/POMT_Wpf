
namespace Petsi.Events
{
    public class LabelServiceValidateFpEventArgs : EventArgs
    {
        public string CatalogId { get; set; }
        public string Filepath { get; set; }
        public string PieType { get; set; }
        public LabelServiceValidateFpEventArgs(string catalogId, string filepath, string pieType)
        {
            CatalogId = catalogId; Filepath = filepath; PieType = pieType;
        }
    }
}
