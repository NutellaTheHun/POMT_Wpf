using Square.Service.Interface;

namespace Square.Service
{
    public class SquareClientFactory : ISquareClientFactory
    {
        public SquareClientFactory() 
        {
            SqClient = new SquareClient.Builder()
                .AccessToken(GetAccessToken())
                .Environment(Environment.Production)
                .Build();
        }

        public SquareClient SqClient { get; set; }

        private string? GetAccessToken()
        {
            string result = null;

            string path = "D:/Git-Repos/Petsi/Square.Service/config.txt";

            if(!File.Exists(path))
            {
                Console.WriteLine("config file not found");
            }
            using (StreamReader sr = new StreamReader(path))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }
    }
}
