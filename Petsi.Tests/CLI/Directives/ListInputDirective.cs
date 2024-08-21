
namespace Petsi.Tests.CLI.Directives
{
    public class ListInputDirective : Directive
    {
        public ListInputDirective() 
        {
            name = "lsi";
            argSize = 0;
        }
        public override string Description()
        {
            return "List all existing input files.";
        }

        public override void Execute(string[] args, Executor executor)
        {
            string[] fnames = executor.fb.GetDirectoryFileNames();
            foreach(string fname in fnames) {Console.WriteLine(fname); }
        }
    }
}
