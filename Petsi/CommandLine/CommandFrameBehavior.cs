using Petsi.Interfaces;

namespace Petsi.CommandLine
{
    public class CommandFrameBehavior : FrameBehaviorBase
    {
        CommandFrame comp;
        public CommandFrameBehavior(CommandFrame cf)
        {
            comp = cf;
        }
        public override Task Actions(Stack<ICommandable> contextChain, string actionIdentifier)
        {
            if(actionIdentifier == null) { return Task.CompletedTask; }

            string[] args = actionIdentifier.ToLower().Split(' ');
            switch (args[0])
            {
                case "view":
                    comp.ViewComponents();
                    break;
                case "select":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Select command needs specified component,\n example: \" select <comp name> \"");
                        break;
                    }
                    comp.OpenComponentView(args[1]);
                    break;
                case "build":
                    Console.WriteLine("     #BUILD");
                    break;
                case "help":
                    PrintHelp();
                    break;
                case "init":
                    comp.RunCommand("sci");
                    comp.RunCommand("execute");
                    comp.RunCommand("back");
                    comp.RunCommand("soi");
                    comp.RunCommand("execute");
                    comp.RunCommand("back");
                    comp.RunCommand("wsi");
                    comp.RunCommand("execute");
                    comp.RunCommand("back");

                    break;
                case "test":
                    comp.RunCommand("sci");
                    comp.RunCommand("execute");
                    comp.RunCommand("back");
                    comp.RunCommand("cmp");
                    comp.RunCommand("build vtest");
                    comp.RunCommand("back");
                    comp.RunCommand("soi");
                    comp.RunCommand("execute");
                    break;

                default:
                    comp.OpenComponentView(args[0]);
                    break;
            }
            return Task.CompletedTask;
        }
        public override void CommandFrameView()
        {
            PrintHelp();
        }

        public override string GetComponentName()
        {
            return "Command Frame";
        }
        private void PrintHelp()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("     view: produces list of active components");
            Console.WriteLine("     select <compName>: changes context to specified component");
            Console.WriteLine("     build: not implemented");
            Console.WriteLine("     help: lists commands");
            Console.WriteLine("     init: build macro");
        }
    }
}
