namespace Petsi.Tests.CLI.Directives
{
    public class ExitDirective : Directive
    {
        public ExitDirective()
        {
            name = "exit";
            argSize = 1;
            description = "Terminates the program.";
        }
        public override void Execute(Executor executor)
        {
            return;
        }
    }
}
