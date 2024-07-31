
namespace Petsi.Events
{
    public class LabelServiceValidateFpEventArgs : EventArgs
    {
        public string ItemName { get; set; }
        public string Filepath { get; set; }
        public string PieType { get; set; }
        public LabelServiceValidateFpEventArgs(string itemName, string filepath, string pieType)
        {
            ItemName = itemName; Filepath = filepath; PieType = pieType;
        }
    }
    public class LabelServiceInputLabelNotFoundArgs : EventArgs
    {
        public string ItemId { get; set; }

        public LabelServiceInputLabelNotFoundArgs(string itemId)
        {
            ItemId = itemId;
        }
    }
}
