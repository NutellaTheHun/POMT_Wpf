using Petsi.Input;
using Petsi.Interfaces;

namespace Petsi.Managers
{
    /// <summary>
    /// Orignally implemented for CommandFrame functionality, managers became main way of refrencing services and models.
    /// </summary>
    public class InputManagerSingleton : IInputManagerRegistry
    {
        private static InputManagerSingleton _instance;
        private List<ModelInputBase> _inputList;

        private InputManagerSingleton() { _inputList = new List<ModelInputBase>(); }
        public static InputManagerSingleton GetInstance() { if (_instance == null) _instance = new InputManagerSingleton(); return _instance; }

        public ModelInputBase GetInputComponent(string inputName)
        {
            return _inputList.Find(x => x.GetInputName() == inputName);
        }
        public void Register(ModelInputBase inputComp)
        {
            _inputList.Add(inputComp);
        }

        public void Deregister(ModelInputBase inputComp)
        {
            _inputList.Remove(inputComp);
        }

        public void Notify()
        {
            throw new NotImplementedException();
        }
    }
}
