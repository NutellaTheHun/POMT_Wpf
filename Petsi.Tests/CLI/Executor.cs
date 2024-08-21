using Petsi.Filing;
using Petsi.Tests.CLI.Directives;

namespace Petsi.Tests.CLI
{
    public class Executor
    {
        public Dictionary<string, Directive> directives;
        public FileBehavior fb;
        
        public Executor()
        {
            directives = new Dictionary<string, Directive>();
            directives.Add("exit", new ExitDirective());
            directives.Add("help", new HelpDirective());
            directives.Add("psi", new PullSquareInputDirective());
            directives.Add("pui", new PullUserInputDirective());
            directives.Add("merge", new MergeInputDirective());
            directives.Add("compare", new CompareTestsDirective());
            directives.Add("run", new RunTestDirective());
            directives.Add("lsi", new ListInputDirective());
            directives.Add("lso", new ListOutputDirective());

            fb = new FileBehavior("ExecutorTestingEnv");
        }
        public void Parse(string[] args)
        {
            if (args.Length == 0) { return; }

            Directive dir;
            if (directives.TryGetValue(args[0], out dir))
            {
                if (args.Length + 1 >= dir.argSize)
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
