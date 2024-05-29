using Petsi.Models;

namespace Petsi.Interfaces
{
    public interface IModelManagerRegistry
    {
        /// <summary>
        /// IServicePublisher method
        /// </summary>
        /// <param name="service"></param>
        public void Register(ModelBase model);
        /// <summary>
        /// IServicePublisher method
        /// </summary>
        /// <param name="service"></param>
        public void Deregister(ModelBase model);
    }
}
