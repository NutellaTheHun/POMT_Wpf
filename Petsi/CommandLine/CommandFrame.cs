using Petsi.CommandLine.ErrorHandlers;
using Petsi.Interfaces;

namespace Petsi.CommandLine
{
    public class CommandFrame : ICommandFrameRegistry
    {
        static CommandFrame instance;
        CommandFrameBehavior frameBehavior;
        List<(string, ICommandable)> Components;
        ICommandable currentContext;
        Stack<ICommandable> contextChain;
        
        private CommandFrame()
        {
            frameBehavior = new CommandFrameBehavior(this);
            Components = new List<(string, ICommandable)>();
            contextChain = new Stack<ICommandable>();
            Components.Add(("command_frame", GetFrameBehavior()));
            OpenComponentView("command_frame");
        }
        public static CommandFrame GetInstance()
        {
            if (instance == null)
            {
                instance = new CommandFrame();
            }
            return instance;
        }
        public void InitComponents(params (string, ICommandable)[] components)
        {
            Components.Add(("command_frame", GetFrameBehavior()));
            Components.AddRange(components.ToList());
            OpenComponentView("command_frame");
        }
        public void RunCommand(string userArg)
        {
            currentContext = contextChain.Peek();
            if (userArg == "back")
            {
                if(contextChain.Count > 1)//if count is 1, context is at frame and should not pop.
                {
                    contextChain.Pop();
                    contextChain.Peek().CommandFrameView();
                }
            }
            else
            {
                currentContext.Actions(contextChain, userArg).Wait();
            }
        }
        public void ViewComponents()
        {
            for (int i = 0; i < Components.Count; i++)
            {
                Console.WriteLine("[" + i + "] :" + Components[i].Item1 + ", " + Components[i].Item2.GetComponentName());
            }
        }
        public void OpenComponentView(string componentIdentifier)
        {
            foreach (var component in Components)
            {
                if (component.Item1 == componentIdentifier)
                {
                    contextChain.Push(component.Item2);
                    contextChain.Peek().CommandFrameView(); //easiest way to display the component menu
                    return;
                }
            }
            Console.WriteLine("Component not found in CommandFrame or invalid command");
        }
        public string GetCurrentContextName()
        {
            if (contextChain != null)
            {
                return contextChain.Peek().GetComponentName();
            }
            else
            {
                return "null";
            }
        }
        public FrameBehaviorBase GetFrameBehavior()
        {
            return frameBehavior;
        }

        public void PrintCurrentContextFrameView()
        {
           currentContext.CommandFrameView();
        }

        public void InjectErrorHandlingFrame(ErrorHandlerBase errorFrame)
        {
            contextChain.Push(errorFrame);
            errorFrame.Actions(contextChain, "0");
        }

        public void RegisterFrame(string name, FrameBehaviorBase frame)
        {
            Components.Add((name, frame));
        }
    }
}
