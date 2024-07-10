using System.Text;
using Petsi.Utils;

namespace Petsi.Utils
{
    public class PetsiConfig
    {
        private static PetsiConfig _instance;
        List<(string,string)> variables;
        static readonly string hardPath = "D:/Git-Repos/POMT_WPF/";
        static readonly string configPath = "D:/Git-Repos/POMT_WPF/Petsi/config.txt";
        static readonly string rootDir = System.AppDomain.CurrentDomain.BaseDirectory + "/petsiDir/";
        static readonly string configFile = "petsiConfig.txt";
        static readonly string configFilePath = rootDir + configFile;
        private PetsiConfig()
        {
            variables = new List<(string,string)>();
            InitConfig();
            //InitVariables();
        }

        public static PetsiConfig GetInstance() { if (_instance == null) { _instance = new PetsiConfig(); }; return _instance; }

        private void InitConfig()
        {
            if(!Directory.Exists(rootDir)) 
            { 
                Directory.CreateDirectory(rootDir); 
                SystemLogger.Log("PetsiConfig created root path at: " + rootDir); 
            }

            if (!File.Exists(configFilePath))
            { 
                File.Create(configFilePath);
                SystemLogger.Log("PetsiConfig file created at: " + configFilePath);
                InitConfigFile();             
            }
            else
            {
                LoadVariables();
            }
        }

        private void InitConfigFile()
        {
            List<string> defaultVars = new List<string> 
            {
                Identifiers.SETTING_FILESERVICE_PATH, //A
                Identifiers.SETTING_ENVIRON_PATH, //U
                Identifiers.SETTING_DAYNUM, //U
                Identifiers.SETTING_REPORT_CNT_PATH, //A
                Identifiers.SETTING_REPORT_EXPORT_PATH, //U
                Identifiers.SETTING_CUTIE_LBL_PATH, //U
                Identifiers.SETTING_PIE_LBL_PATH, //U
                //Identifiers.SETTING_LABEL_FP, //U
                Identifiers.SETTING_LABEL_PRINTER, //U
                Identifiers.SETTING_STD_PRINTER, //U
                Identifiers.SETTING_PIE_TEMPLATE, //U
                Identifiers.SETTING_PASTRY_TEMPLATE, //U
                Identifiers.SETTING_SQUARE //~AU
            };
            // Create a StringBuilder to store the updated contents
            StringBuilder sb = new StringBuilder();

            // Iterate through the variables list and append each variable to the StringBuilder
            foreach (var variable in defaultVars)
            {
                sb.AppendLine($"{variable}=");
                variables.Add((variable, null));
            }

            // Write the updated contents back to the config file
            File.WriteAllText(configPath, sb.ToString());
            
            SetValue(Identifiers.SETTING_REPORT_CNT_PATH, "0");
            SetValue(Identifiers.SETTING_FILESERVICE_PATH, rootDir+"fileService");
        }

        

        /// <summary>
        /// Returns variable with the filepath
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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
            File.WriteAllText(/*configPath*/configFilePath, sb.ToString());
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
        
        public void LoadVariables()
        {
            using (StreamReader sr = new StreamReader(configFilePath))
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
