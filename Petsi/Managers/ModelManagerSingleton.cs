using Petsi.Interfaces;
using Petsi.Models;

namespace Petsi.Managers
{
    /// <summary>
    /// Orignally implemented for CommandFrame functionality, managers became main way of refrencing services and models.
    /// </summary>
    public class ModelManagerSingleton : IModelManagerRegistry
    {
        private static ModelManagerSingleton instance;
        private List<ModelBase> _models;
        private ModelManagerSingleton() { _models = new List<ModelBase>(); }

        public static ModelManagerSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new ModelManagerSingleton();
            }
            return instance;
        }
        public ModelBase GetModel(string targetModelName)
        {
            return _models.Find(x => x.GetModelName() == targetModelName);
        }
        public void AddModel(ModelBase model) { _models.Add(model); }

        public void Register(ModelBase model)
        {
            _models.Add(model);
        }

        public void Deregister(ModelBase model)
        {
            _models.Remove(model);
        }
    }
}
