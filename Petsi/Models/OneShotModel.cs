using Petsi.CommandLine;
using Petsi.Filing;
using Petsi.Interfaces;
using Petsi.Managers;
using Petsi.Units;


namespace Petsi.Models
{
    public class OneShotModel : ModelBase, IModelInput
    {
        List<PetsiOrder> OneShotOrders { get; set; }
        OneShotModelFrameBehavior frameBehavior;
        FileBehavior fileBehavior;
        public OneShotModel()
        {
            OneShotOrders = new List<PetsiOrder>();
            SetModelName("OneShotModel");
            frameBehavior = new OneShotModelFrameBehavior(this);
            fileBehavior = new FileBehavior("OneShotModel");
            ModelManagerSingleton.GetInstance().Register(this);
            //CommandFrame.GetInstance().RegisterFrame("osi", frameBehavior);
            EnvironCaptureRegistrySingleton.GetInstance().Register(this);
        }
        public override void AddData(ModelUnitBase unit)
        {
            OneShotOrders.Add((PetsiOrder)unit);
        }

        public override void AddOrder(ModelUnitBase item)
        {
            OneShotOrders.Add((PetsiOrder)item);
        }

        public override void CaptureEnvironment(FileBehavior reportFb)
        {
            fileBehavior.DataListToFile("OneShotModel", OneShotOrders);
        }

        public override void ClearModel()
        {
            OneShotOrders.Clear();
        }

        public override void Complete()
        {
            
        }

        public override FrameBehaviorBase GetFrameBehavior()
        {
            throw new NotImplementedException();
        }

        public override string GetModelName()
        {
            return "OneShotModel";
        }

        public override void SetModelName(string modelName)
        {
            ModelName = modelName;
        }
    }
}
