using Petsi.Input;
using Petsi.Interfaces;

namespace Petsi.CommandLine
{
    public class OneShotInputFrameBehavior : FrameBehaviorBase
    {
        public OneShotInputFrameBehavior(OneShotInput input)
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
