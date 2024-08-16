namespace Petsi.Tests.CLI.Directives
{
    public class HelpDirective : Directive
    {
        public HelpDirective()
        {
            name = "help";
            argSize = 1;
            description = "Displays the description of each command.";
        }
        public override void Execute(Executor executor)
        {
            List<Directive> directives = executor.directives.Values.ToList();
            foreach (var directive in directives)
            {
                Console.WriteLine($"\t{directive.name} : {directive.description}");
            }
        }
      
    }
}
