using Petsi.Interfaces;
using Petsi.Models;
using Petsi.Utils;

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
        public ModelBase GetOrderModel()
        {
            var model = _models.Find(x => x.GetModelName() == Identifiers.MODEL_ORDERS);
            if(model == null)
            {
                model = _models.Find(x => x.GetModelName() == Identifiers.TEST_MODEL_ORDERS);
            }
            return model;
        }

        public ModelBase GetCatalogModel()
        {
            var model = _models.Find(x => x.GetModelName() == Identifiers.MODEL_CATALOG);
            if (model == null)
            {
                model = _models.Find(x => x.GetModelName() == Identifiers.TEST_MODEL_CATALOG);
            }
            return model;
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
