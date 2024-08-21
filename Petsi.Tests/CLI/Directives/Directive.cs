
namespace Petsi.Tests.CLI.Directives
{
    public abstract class Directive
    {
        public string name;
        public int argSize;

        public abstract void Execute(string[] args, Executor executor);

        public abstract string Description();
    }
}
