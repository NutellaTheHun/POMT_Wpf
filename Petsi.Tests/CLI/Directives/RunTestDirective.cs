
namespace Petsi.Tests.CLI.Directives
{
    public class RunTestDirective : Directive
    {
        public RunTestDirective() 
        {
            name = "runtest";
            argSize = 0;
        }
        public override string Description()
        {
            return "Not implemented.";
        }

        public override void Execute(string[] args, Executor executor)
        {
            Console.WriteLine("RunTest executed.");
        }
    }
}
