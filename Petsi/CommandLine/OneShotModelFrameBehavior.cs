using Petsi.Interfaces;
using Petsi.Models;

namespace Petsi.CommandLine
{
    public class OneShotModelFrameBehavior : FrameBehaviorBase
    {
        public OneShotModelFrameBehavior(OneShotModel model)
        {
        }
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
