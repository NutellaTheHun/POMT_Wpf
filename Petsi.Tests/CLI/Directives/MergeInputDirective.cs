
namespace Petsi.Tests.CLI.Directives
{
    public class MergeInputDirective : Directive
    {
        public MergeInputDirective() 
        {
            name = "merge";
            argSize = 2;
        }
        public override string Description()
        {
            return "merge <fileName> <InputFileIndex> {InputFileIndex...} Merge a varying number of input files into one file";
        }

        public override void Execute(string[] args, Executor executor)
        {
            int argLength = args.Length;
            string fName = args[1];
            List<string> iFiles = new List<string>();
            for (int i = 2; i < argLength; i++)
            {
                //iFiles
            }
        }
    }
}
