
namespace Petsi.Utils
{
    public static class SystemLogger
    {
        public static void LogStatus(string message)
        {
            string fp = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_ERROR_LOG_PATH);
            if (fp == null || fp == "") { return; }
            File.AppendAllText(fp, $"{DateTime.Now.ToString()} : {PetsiConfig.appRuntimeId} : [S] {message}\n");
        }

        public static void LogError(string errorMessage, string sender)
        {
            string fp = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_ERROR_LOG_PATH);
            if(fp == null || fp == ""){ return; }
            File.AppendAllText(fp,$"{DateTime.Now.ToString()} : {PetsiConfig.appRuntimeId} : [E] {sender} : {errorMessage}\n");
        }

        public static void LogWarning(string message)
        {
            string fp = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_ERROR_LOG_PATH);
            if (fp == null || fp == "") { return; }
            File.AppendAllText(fp, $"{DateTime.Now.ToString()} : {PetsiConfig.appRuntimeId} : [W] {message}\n");
        }
    }
}
