using Petsi.Interfaces;

namespace Petsi.CommandLine.ErrorHandlers
{
    public abstract class ErrorHandlerBase : FrameBehaviorBase
    {
        public abstract override Task Actions(Stack<ICommandable> contextChain, string actionIdentifier);
        public abstract override void CommandFrameView();

        public abstract override string GetComponentName();
    }
}
