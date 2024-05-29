namespace Petsi.Utils
{
    public class PetsiConfig
    {
        private static PetsiConfig _instance;
        List<(string,string)> variables;
        static readonly string hardPath = "D:/Git-Repos/Petsi/";
        static readonly string configPath = "D:/Git-Repos/Petsi/Petsi/config.txt";
        private PetsiConfig()
        {
            variables = new List<(string,string)>();
            InitVariables();
        }
        public static PetsiConfig GetInstance() { if(_instance == null) { _instance = new PetsiConfig(); }; return _instance; }
        public string GetVariable(string key)
        {
            var variable = variables.Find(x => x.Item1 == key);
            return hardPath+variable.Item2;
        }
        public void InitVariables()
        {
            using (StreamReader sr = new StreamReader(configPath))
            {
                string[] args;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    args = line.Split("=");
                    variables.Add((args[0], args[1]));
                }
            }
        }
    }
}
