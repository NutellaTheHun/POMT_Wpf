using Petsi.Interfaces;

namespace Petsi.CommandLine
{
    internal class PeriodicOrderInputFrameBehavior : FrameBehaviorBase
    {
        public override Task Actions(Stack<ICommandable> contextChain, string actionIdentifier)
        {
            throw new NotImplementedException();
        }

        public override void CommandFrameView()
        {
            throw new NotImplementedException();
        }

        public override string GetComponentName()
        {
            throw new NotImplementedException();
        }
    }
}
