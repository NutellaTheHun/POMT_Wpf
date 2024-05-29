using Petsi.Models;

namespace Petsi.Interfaces
{
    public interface IModelSubsciber
    {
        /// <summary>
        /// IServiceSubscriber method
        /// </summary>
        /// <param name="model"></param>
        public void Update(ModelBase model);
    }
}
