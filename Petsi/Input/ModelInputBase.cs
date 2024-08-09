
using Petsi.Filing;
using Petsi.Interfaces;
using Petsi.Models;

namespace Petsi.Input
{
    /// <summary>
    /// Base structure of Input components. Intended to be used as a middlepoint of receiving input data and giving it to models. 
    /// </summary>
    public abstract class ModelInputBase : IEnvironCapturable
    {
        protected ModelBase Model { get; set; }
        protected string InputName { get; set; }
        protected virtual void SetInputName(string Inputname) {  InputName = Inputname; }
        public virtual string GetInputName() { return InputName; }
        protected virtual void SetModel(ModelBase targetModel){Model = targetModel; }
        public abstract Task Execute();
        public abstract void CaptureEnvironment(FileBehavior reportFb);
    }
}
