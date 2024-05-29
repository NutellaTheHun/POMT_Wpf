using Petsi.Input;

namespace Petsi.Interfaces
{
    public interface IInputManagerRegistry
    {
        /// <summary>
        /// IServicePublisher method
        /// </summary>
        /// <param name="service"></param>
        public void Register(ModelInputBase inputComp);
        /// <summary>
        /// IServicePublisher method
        /// </summary>
        /// <param name="service"></param>
        public void Deregister(ModelInputBase inputComp);
        /// <summary>
        /// IServicePublisher method
        /// </summary>
        /// <param name="service"></param>
        public void Notify();
    }
}
