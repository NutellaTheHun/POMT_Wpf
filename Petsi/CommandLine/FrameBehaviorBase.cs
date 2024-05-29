using Petsi.Interfaces;

namespace Petsi.CommandLine
{
    public abstract class FrameBehaviorBase : ICommandable
    {
        public abstract Task Actions(Stack<ICommandable> contextChain, string actionIdentifier);
        public abstract void CommandFrameView();
        public abstract string GetComponentName();
    }
}
