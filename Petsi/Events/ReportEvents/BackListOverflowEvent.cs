using Petsi.Units;

namespace Petsi.Events.ReportEvents
{
    public class BackListOverflowEvent
    {
        public static BackListOverflowEvent instance;
        private BackListOverflowEvent()
        {

        }

        public static BackListOverflowEvent Instance
        {
            get
            {
                if (instance == null) { instance = new BackListOverflowEvent(); }
                return instance;
            }
        }

        public delegate void PieOverflowEvent(object sender, EventArgs e);

        public event PieOverflowEvent BacklistPieOverflow;
        public static void OnPieOverflow(List<PetsiOrderLineItem> items)
        {
            Instance.BacklistPieOverflow?.Invoke(Instance, new BackListOverflowEventArgs(items));
        }

        public delegate void PastryOverflowEvent(object sender, EventArgs e);

        public event PieOverflowEvent BacklistPastryOverflow;
        public static void OnPastryOverflow(List<PetsiOrderLineItem> items)
        {
            Instance.BacklistPastryOverflow?.Invoke(Instance, new BackListOverflowEventArgs(items));
        }
    }

    public class BackListOverflowEventArgs : EventArgs
    {
        public List<PetsiOrderLineItem> OverflowItems { get; set; }
        public BackListOverflowEventArgs(List<PetsiOrderLineItem>  items){ OverflowItems = items; }
    }
}
