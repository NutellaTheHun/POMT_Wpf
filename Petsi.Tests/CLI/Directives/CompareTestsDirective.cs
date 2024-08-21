
namespace Petsi.Tests.CLI.Directives
{
    public class CompareTestsDirective : Directive
    {
        public override string Description()
        {
            return "Not implemented.";
        }

        public override void Execute(string[] args, Executor executor)
        {
            Console.WriteLine("CompareTests executed.");
        }
    }
}
