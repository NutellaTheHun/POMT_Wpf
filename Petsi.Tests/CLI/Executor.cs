using Petsi.Tests.CLI.Directives;

namespace Petsi.Tests.CLI
{
    public class Executor
    {
        public Dictionary<string, Directive> directives;
        public Executor()
        {
            directives = new Dictionary<string, Directive>();
            directives.Add("exit", new ExitDirective());
            directives.Add("help", new HelpDirective());
            directives.Add("pso", new PullSquareOrdersDirective());
        }
        public void Parse(string[] args)
        {
            if (args.Length == 0) { return; }

            Directive dir;
            if (directives.TryGetValue(args[0], out dir))
            {
                if (args.Length >= dir.argSize)
                {
                    dir.Execute(args, this);
                }
                else
                {
                    Console.WriteLine($"Insufficient command parameters");
                }
            }
            else
            {
                Console.WriteLine("Command Not Found.");
            }
        }
        
    }
}
