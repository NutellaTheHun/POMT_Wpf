namespace Petsi.Tests.CLI
{
    public class Runner
    {
        public static void Main()
        {
            Executor executor = new Executor();
            string[] args;
            do
            {
                Console.Write("> ");
                args = Console.ReadLine().Split(" ");
                executor.Parse(args);
            } while (args[0] != "exit");
        }
    }
}
