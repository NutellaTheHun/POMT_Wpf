using Petsi.CommandLine;
using Petsi.Filing;
using Petsi.Managers;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Input
{
    public class OneShotInput : ModelInputBase
    {
        List<PetsiOrder> Items { get; set;}
        //CSVHandler csvh;
        OneShotInputFrameBehavior frameBehavior;
        FileBehavior fileBehavior;

        public OneShotInput()
        {
            Items = new List<PetsiOrder>();
            //csvh = new CSVHandler(PetsiConfig.GetInstance().GetFilepath("onOrderPath"));
            frameBehavior = new OneShotInputFrameBehavior(this);
            fileBehavior = new FileBehavior("OneShotInput");

            SetModel(ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_ORDERS));
            SetInputName(Identifiers.ONE_SHOT_INPUT);
            InputManagerSingleton.GetInstance().Register(this);
            EnvironCaptureRegistrySingleton.GetInstance().Register(this);
            //CommandFrame.GetInstance().RegisterFrame("osi", frameBehavior);
        }

        public override void CaptureEnvironment(FileBehavior reportFb)
        {
            fileBehavior.DataListToFile(Identifiers.ENV_OSI, Items);
        }

        public override async Task Execute()
        {
            Items = fileBehavior.BuildDataListFile<PetsiOrder>("mainOneShotInput");
            foreach (PetsiOrder item in Items)
            {
                Model.AddData(item);
            }
        }

        public void SaveModel()
        {
            fileBehavior.DataListToFile("mainOneShotInput", Items);
        }

        public override FrameBehaviorBase GetFrameBehavior()
        {
            throw new NotImplementedException();
        }
    }
}
