
namespace Petsi.Utils
{
    public static class SystemLogger
    {
        private static readonly object _lock = new();
        public static void LogStatus(string message)
        {
            RefreshLogCheck();
            string fp = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_ERROR_LOG_PATH);
            if (fp == null || fp == "") { return; }

            lock (_lock)
            {
                File.AppendAllText(fp, $"{DateTime.Now.ToString()} : {PetsiConfig.appRuntimeId} : [S] {message}\n");
            }
        }

        public static void LogError(string errorMessage, string sender)
        {
            RefreshLogCheck();
            string fp = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_ERROR_LOG_PATH);
            if(fp == null || fp == ""){ return; }

            lock (_lock)
            {
                File.AppendAllText(fp, $"{DateTime.Now.ToString()} : {PetsiConfig.appRuntimeId} : [E] {sender} : {errorMessage}\n");
            }
        }

        public static void LogWarning(string message)
        {
            RefreshLogCheck();
            string fp = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_ERROR_LOG_PATH);
            if (fp == null || fp == "") { return; }

            lock (_lock)
            {
                File.AppendAllText(fp, $"{DateTime.Now.ToString()} : {PetsiConfig.appRuntimeId} : [W] {message}\n");
            }
        }

        static int daysUntilRefresh = 7;
        private static void RefreshLogCheck()
        {
            string fp = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_ERROR_LOG_PATH);
            DateTime creation = File.GetCreationTime(fp);
            DateTime refreshDate = creation.AddDays(daysUntilRefresh);
            if(DateTime.Today >= refreshDate)
            {

                lock (_lock)
                {
                    File.WriteAllText(fp, String.Empty);
                    File.SetCreationTime(fp, DateTime.Today);
                }
            }
        }
    }
}
