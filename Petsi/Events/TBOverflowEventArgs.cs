using Petsi.Units;

namespace Petsi.Events
{
    public class TBOverflowEventArgs : EventArgs
    {
        public List<PetsiOrderLineItem> OverflowList { get; set; }
        public TBOverflowEventArgs(List<PetsiOrderLineItem> input) { OverflowList = new List<PetsiOrderLineItem>(input); }
    }
}
