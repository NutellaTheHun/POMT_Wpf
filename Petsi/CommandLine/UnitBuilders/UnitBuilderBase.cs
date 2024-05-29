using Petsi.Interfaces;

namespace Petsi.CommandLine.UnitBuilders
{
    public abstract class UnitBuilderBase : ICommandable
    {
        public abstract Task Actions(Stack<ICommandable> contextChain, string actionIdentifier);
        public abstract void CommandFrameView();
        public abstract string GetComponentName();
    }
}
