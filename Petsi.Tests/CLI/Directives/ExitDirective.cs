namespace Petsi.Tests.CLI.Directives
{
    public class ExitDirective : Directive
    {
        public ExitDirective()
        {
            name = "exit";
            argSize = 1;
        }

        public override string Description()
        {
            return "Terminates the program.";
        }
        public override void Execute(string[] args, Executor executor)
        {
            return;
        }
    }
}
