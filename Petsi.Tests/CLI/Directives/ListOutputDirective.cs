
namespace Petsi.Tests.CLI.Directives
{
    public class ListOutputDirective : Directive
    {
        public ListOutputDirective() 
        {
            name = "lso";
            argSize = 0;
        }
        public override string Description()
        {
            return "List all output test files.";
        }

        public override void Execute(string[] args, Executor executor)
        {
            Console.WriteLine("ListOutput executed.");
        }
    }
}
