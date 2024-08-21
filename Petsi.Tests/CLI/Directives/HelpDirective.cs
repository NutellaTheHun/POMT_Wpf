namespace Petsi.Tests.CLI.Directives
{
    public class HelpDirective : Directive
    {
        public HelpDirective()
        {
            name = "help";
            argSize = 0;
        }
        public override string Description()
        {
            return "Displays the description of each command.";
        }
        public override void Execute(string[] args, Executor executor)
        {
            List<Directive> directives = executor.directives.Values.ToList();
            foreach (var directive in directives)
            {
                Console.WriteLine($"\t{directive.name} : {directive.Description()}");
            }
        }


    }
}
