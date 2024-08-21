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
            directives.Add("pso", new PullSquareInputDirective());
            directives.Add("puo", new PullUserInputDirective());
            directives.Add("merge", new MergeInputDirective());
            directives.Add("compare", new CompareTestsDirective());
            directives.Add("run", new RunTestDirective());

            fb = new FileBehavior("ExecutorTestingEnv");
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
