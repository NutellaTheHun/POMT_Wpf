using Petsi.CommandLine;
using Petsi.Filing;
using Petsi.Interfaces;
using Petsi.Units;

namespace Petsi.Models
{
    public abstract class ModelBase : IModelInput, IEnvironCapturable
    {
        protected string ModelName;
        public abstract string GetModelName();
        public abstract void SetModelName(string modelName);
        public abstract void AddData(ModelUnitBase unit);
        public abstract FrameBehaviorBase GetFrameBehavior();
        public abstract void ClearModel();
        public abstract void AddOrder(ModelUnitBase item);
        public abstract void Complete();
        public abstract void CaptureEnvironment(FileBehavior reportFb);
    }
}
