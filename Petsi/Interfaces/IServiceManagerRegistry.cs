using Petsi.Services;

namespace Petsi.Interfaces
{
    public interface IServiceManagerRegistry
    {
        /// <summary>
        /// IServicePublisher method
        /// </summary>
        /// <param name="service"></param>
        public void Register(ServiceBase service);
        /// <summary>
        /// IServicePublisher method
        /// </summary>
        /// <param name="service"></param>
        public void Deregister(ServiceBase service);
        /// <summary>
        /// IServicePublisher method
        /// </summary>
        /// <param name="service"></param>
        public void Notify();
    }
}
