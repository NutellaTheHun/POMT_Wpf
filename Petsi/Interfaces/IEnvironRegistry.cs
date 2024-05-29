using Petsi.Services;

namespace Petsi.Interfaces
{
    public interface IEnvironRegistry
    {
        /// <summary>
        /// IServicePublisher method
        /// </summary>
        /// <param name="service"></param>
        public void Register(IEnvironCapturable component);
        /// <summary>
        /// IServicePublisher method
        /// </summary>
        /// <param name="service"></param>
        public void Deregister(IEnvironCapturable component);
    }
}
