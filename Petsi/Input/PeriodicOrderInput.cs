using Petsi.CommandLine;
using Petsi.Filing;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Input
{
    public class PeriodicOrderInput : ModelInputBase
    {
        List<PetsiOrder> Items {  get; set; }
        PeriodicOrderInputFrameBehavior frameBehavior;
        FileBehavior fileBehavior;

        public PeriodicOrderInput()
        {
            Items = new List<PetsiOrder>();
            fileBehavior = new FileBehavior("PeriodicOrderInput");
        }
        public override void CaptureEnvironment(FileBehavior reportFb)
        {
            fileBehavior.DataListToFile(Identifiers.ENV_OSI, Items);
        }

        public override async Task Execute()
        {
            Items = fileBehavior.BuildDataListFile<PetsiOrder>(Identifiers.PERIODIC_ORDERS);
            foreach (PetsiOrder item in Items)
            {
                Model.AddData(item);
            }
        }

        public override FrameBehaviorBase GetFrameBehavior()
        {
            throw new NotImplementedException();
        }
    }
}
