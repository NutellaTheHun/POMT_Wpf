using System.Text;

namespace Petsi.Utils
{
    public class PetsiConfig
    {
        private static PetsiConfig _instance;
        List<(string,string)> variables;
        static readonly string hardPath = "D:/Git-Repos/POMT_WPF/";
        static readonly string configPath = "D:/Git-Repos/POMT_WPF/Petsi/config.txt";
        private PetsiConfig()
        {
            variables = new List<(string,string)>();
            InitVariables();
        }

        public static PetsiConfig GetInstance() { if(_instance == null) { _instance = new PetsiConfig(); }; return _instance; }

        public string GetFilepath(string key)
        {
            var variable = variables.Find(x => x.Item1 == key);
            return hardPath+variable.Item2;
        }
        public string GetVariable(string key)
        {
            var variable = variables.Find(x => x.Item1 == key);
            return variable.Item2;
        }

        public void SetValue(string key, string value)
        {
            var variable = variables.FirstOrDefault(v => v.Item1 == key);
            if (variable != default)
            {
                int index = variables.IndexOf(variable);
                variables[index] = (key, value);
            }
            UpdateConfigFile();
        }
        private void UpdateConfigFile()
        {
            // Create a StringBuilder to store the updated contents
            StringBuilder sb = new StringBuilder();

            // Iterate through the variables list and append each variable to the StringBuilder
            foreach (var variable in variables)
            {
                sb.AppendLine($"{variable.Item1}={variable.Item2}");
            }

            // Write the updated contents back to the config file
            File.WriteAllText(configPath, sb.ToString());
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
