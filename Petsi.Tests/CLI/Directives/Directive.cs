
namespace Petsi.Tests.CLI.Directives
{
    public abstract class Directive
    {
        public string name;
        public int argSize;
        public string description;

        public abstract void Execute(Executor executor);
    }
}
