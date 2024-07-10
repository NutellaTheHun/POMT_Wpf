using Square.Service.Interface;
using System.Text;


namespace Square.Service
{
    public class SquareClientFactory : ISquareClientFactory
    {
        static readonly string configDir = System.AppDomain.CurrentDomain.BaseDirectory + "/petsiDir/";
        static readonly string configFp = configDir + "squareConfig.txt";
        
        public bool BuildFailed;
        public SquareClientFactory() 
        {
            string key = GetAccessToken();
            if(key != null)
            {
                SqClient = new SquareClient.Builder()
                    .AccessToken(key)
                    .Environment(Environment.Production)
                    .Build();
            }
            else
            {
                BuildFailed = true;
            }
            /*
            SqClient = new SquareClient.Builder()
                .AccessToken(GetAccessToken())
                .Environment(Environment.Production)
                .Build();*/
        }

        public SquareClient SqClient { get; set; }

        private string? GetAccessToken()
        {
            /*
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
            */
            if (!Directory.Exists(configDir)) { Directory.CreateDirectory(configDir); }
            if (!File.Exists(configFp)) { File.Create(configFp); }
            string result = null;
            using (StreamReader sr = new StreamReader(configFp))
            {
                result = sr.ReadLine();
            }
            if(result == null) { }
            return result;
        }
    }
}
