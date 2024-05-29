using Petsi.Services;

namespace Petsi.Interfaces
{
    public interface IModelPublishable
    {
        /// <summary>
        /// IServicePublisher method
        /// </summary>
        /// <param name="service"></param>
        public void AddModelService(ServiceBase service);
        /// <summary>
        /// IServicePublisher method
        /// </summary>
        /// <param name="service"></param>
        public void RemoveModelService(ServiceBase service);
        /// <summary>
        /// IServicePublisher method
        /// </summary>
        /// <param name="service"></param>
        public void NotifyModelServices();
    }
}
