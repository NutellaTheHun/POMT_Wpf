
namespace Petsi.Tests.CLI.Directives
{
    public class MergeInputDirective : Directive
    {
        public MergeInputDirective() 
        {
            name = "mergeinput";
            argSize = 0;
        }
        public override string Description()
        {
            return "Not implemented.";
        }

        public override void Execute(string[] args, Executor executor)
        {
            Console.WriteLine("MergeInput executed.");
        }
    }
}
