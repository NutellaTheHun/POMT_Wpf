using Petsi.CommandLine;

namespace Petsi.Interfaces
{
    public interface ICommandFrameRegistry
    {
        /// <summary>
        /// IServicePublisher method
        /// </summary>
        /// <param name="service"></param>
        public void RegisterFrame(string name, FrameBehaviorBase frame);
    }
}
