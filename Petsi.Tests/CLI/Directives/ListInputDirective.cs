
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
            List<string> inputFileNames = new List<string>();
            foreach (string fname in fnames)
            {
                if (fname.Contains("i%"))
                {
                    inputFileNames.Add(Path.GetFileName(fname).Substring(2));
                }
            }
            int i = 0;
            Console.WriteLine("Input Files:");
            foreach (string name in inputFileNames)
            {
                Console.WriteLine($"\t[{i}] {name}");
                i++;
            }
        }
    }
}
