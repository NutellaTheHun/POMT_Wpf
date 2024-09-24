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

        /// <summary>
        /// Returns the production order model by it's set model name at runtime. If the production
        /// model isn't found it returns the test model (assuming if the program execution isn't production
        /// its for testing.
        /// </summary>
        /// <returns>An OrderModelPetsi object, either the "normal"/production instantiation, or the testing instantiatio</returns>
        public ModelBase GetOrderModel()
        {
            var model = _models.Find(x => x.GetModelName() == Identifiers.MODEL_ORDERS);
            if(model == null)
            {
                model = _models.Find(x => x.GetModelName() == Identifiers.TEST_MODEL_ORDERS);
            }
            return model;
        }

        /// <summary>
        /// Returns the production catalog model by it's set model name at runtime. If the production
        /// model isn't found it returns the test model (assuming if the program execution isn't production
        /// its for testing.
        /// </summary>
        /// <returns>An CatalogModelPetsi object, either the "normal"/production instantiation, or the testing instantiatio</returns>
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

        public static void Reset()
        {
            instance._models.Clear();
        }
    }
}
