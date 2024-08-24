using System.Text;
using Petsi.Interfaces;
using Petsi.Services;

namespace Petsi.Utils
{
    /// <summary>
    /// Responsible for system variables, facilitates getting and setting config variables and reflecting the file i/o with changes
    /// </summary>
    public class PetsiConfig : IStartupSubscriber
    {
        private static PetsiConfig _instance;
        //private static readonly object padlock = new object();
        

        List<(string Name,string Value)> variables;

        //static readonly string rootDir = System.AppDomain.CurrentDomain.BaseDirectory + "/petsiDir/";s

        public static string appRuntimeId;
        static readonly string rootDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\petsiDir";
        static readonly string configFile = "petsiConfig.txt";
        static readonly string configFilePath = rootDir + "\\" +  configFile;
        private PetsiConfig()
        {
            appRuntimeId = Guid.NewGuid().ToString().Substring(0,8);
            variables = new List<(string,string)>();
            InitConfig();
        }

        public static PetsiConfig GetInstance() 
        { 
           // lock(padlock)
           // {
                if (_instance == null)
                {
                    _instance = new PetsiConfig();
                };
                return _instance;
         //   }
        }

        private void InitConfig()
        {
            if(!Directory.Exists(rootDir)) 
            { 
                Directory.CreateDirectory(rootDir); 
                SystemLogger.LogStatus("PetsiConfig created root path at: " + rootDir);
            }
            //Creates new config file, signals to start startup process
            if (!File.Exists(configFilePath))
            { 
                SystemLogger.LogStatus("PetsiConfig file created at: " + configFilePath);
                InitializeConfiguration();             
            }
            else
            {
                //Normal boot up of loading variables from existing config file
                LoadVariables();
            }

            //signal to run startup service, service is started and signal is set to neutral, REGARDLESS OF SUCCESS
            //Once users sets square key and startup location, status is set to pending.
            if (GetVariable(Identifiers.SETTING_STARTUP_STATUS) == Identifiers.SETTING_STARTUP_STATUS_PENDING)
            {
                StartupService.Instance.Start(GetVariable(Identifiers.SETTING_STARTUP));
                SetVariable(Identifiers.SETTING_STARTUP_STATUS, Identifiers.SETTING_STARTUP_STATUS_NEUTRAL);
            }
        }

        private void InitializeConfiguration()
        {
            //Default Config Variables
            List<string> defaultVars = new List<string> 
            {
                Identifiers.SETTING_FILESERVICE_PATH,
                Identifiers.SETTING_ENVIRON_PATH,
                Identifiers.SETTING_DAYNUM,
                Identifiers.SETTING_REPORT_CNT_PATH,
                Identifiers.SETTING_REPORT_EXPORT_PATH,
                Identifiers.SETTING_CUTIE_LBL_PATH,
                Identifiers.SETTING_PIE_LBL_PATH,
                Identifiers.SETTING_LABEL_PRINTER,
                Identifiers.SETTING_STD_PRINTER,
                Identifiers.SETTING_PIE_TEMPLATE,
                Identifiers.SETTING_PASTRY_TEMPLATE,
                Identifiers.SETTING_STARTUP,
                Identifiers.SETTING_STARTUP_STATUS,
                Identifiers.SETTING_BACKUP_PATH,
                Identifiers.SETTING_ERROR_LOG_PATH,
                Identifiers.SETTING_ROOT_DIR
            };
            
            //Write Config file with defaul variables, some variables are initialized to start, empty variables are user managed
            StringBuilder sb = new StringBuilder();
            foreach (var variable in defaultVars)
            {
                if(variable == Identifiers.SETTING_REPORT_CNT_PATH)
                {
                    sb.AppendLine($"{variable}=0");
                    variables.Add((variable, "0"));
                }
                else if(variable == Identifiers.SETTING_FILESERVICE_PATH)
                {
                    sb.AppendLine($"{variable}="+ rootDir + "\\" + "fileService");
                    variables.Add((variable, rootDir + "\\" + "fileService"));
                }
                else if (variable == Identifiers.SETTING_STARTUP_STATUS)
                {
                    sb.AppendLine($"{variable}=" + Identifiers.SETTING_STARTUP_STATUS_INIT);
                    variables.Add((variable, Identifiers.SETTING_STARTUP_STATUS_INIT));
                }
                else if (variable == Identifiers.SETTING_ERROR_LOG_PATH)
                {
                    sb.AppendLine($"{variable}=" + rootDir + "\\" + "errorLog.txt");
                    variables.Add((variable, rootDir + "\\" + "errorLog.txt"));
                }
                else if (variable == Identifiers.SETTING_ROOT_DIR)
                {
                    sb.AppendLine($"{variable}=" + rootDir);
                    variables.Add((variable, rootDir));
                }
                else
                {
                    sb.AppendLine($"{variable}=");
                    variables.Add((variable, null));
                }
            }
            try
            {
                File.WriteAllText(configFilePath, sb.ToString());
            }
            catch (Exception ex) { ErrorService.RaiseExceptionHandlerError(ex.Message, "PetsiConfig, InitializeConfiguration"); }
            

            //Upon Initialization, Startup event queues user to set square key and startup location
            //the startup status is a signal to run the startup service, signal is then set to neutral
            ErrorService.Instance().RaiseNewStartupEvent();  
        }

       /// <summary>
       /// Returns a variable value from the variables list, if value is empty, returns empty string ("")
       /// </summary>
       /// <param name="key"></param>
       /// <returns></returns>
        public string GetVariable(string key)
        {
            var variable = variables.Find(x => x.Item1 == key);
            return variable.Item2;
        }

        /// <summary>
        /// Sets a currently existing variable in the variables list to a value and updates the config file.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetVariable(string key, string value)
        {
            var variable = variables.FirstOrDefault(v => v.Item1 == key);
            if (variable != default)
            {
                int index = variables.IndexOf(variable);
                variables[index] = (key, value);
            }
            UpdateConfigFile();
        }

        /// <summary>
        /// Updates Config file to reflect current state of the variables list
        /// </summary>
        private void UpdateConfigFile()
        {  
            StringBuilder sb = new StringBuilder();

            foreach (var variable in variables)
            {
                sb.AppendLine($"{variable.Item1}={variable.Item2}");
            }

            try
            {
                File.WriteAllText(configFilePath, sb.ToString());
            }
            catch (Exception ex)  { ErrorService.RaiseExceptionHandlerError(ex.Message, "PetsiConfig, UpdateConfigFile"); }

            WriteBackup();
        }

        private void WriteBackup()
        {
            string fp = GetVariable(Identifiers.SETTING_BACKUP_PATH);
            if (fp == null || fp == "") { return; }
            try
            {
                File.Copy(configFilePath, GetVariable(Identifiers.SETTING_BACKUP_PATH) + "\\"+ configFile, true);
            }
            catch(Exception ex) {  ErrorService.RaiseExceptionHandlerError(ex.Message, "PetsiConfig, WriteBackup"); }
        }

        /// <summary>
        /// Load Variables from existing config file
        /// </summary>
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

        public void LoadStartupFiles(List<(string fileName, string filePath)> FileList)
        {
            if (FileList == null || FileList.Count == 0) { return; }
            foreach (var file in FileList)
            {
                if(file.fileName == Identifiers.PETSI_CONFIG)
                {
                    try
                    {
                        File.Copy(file.filePath, configFilePath, true);
                    }
                    catch (Exception ex) { ErrorService.RaiseExceptionHandlerError(ex.Message, "PetsiConfig, LoadStartupFile"); }
                }
            }
        }
    }
}
