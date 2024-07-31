using Square.Service.Interface;

namespace Square.Service
{
    public class SquareClientFactory : ISquareClientFactory
    {
        /// <summary>
        /// WARNING This filepath is hardcoded For PetsiConfig and The SquareKeyMissingWindow.xaml.cs
        /// </summary>
        static readonly string configDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\petsiDir\\";
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
            try
            {
                using (StreamReader sr = new StreamReader(configFp))
                {
                    result = sr.ReadLine();
                }
            }
            catch (Exception ex) 
            {

            }
            return result;
        }
    }
}
