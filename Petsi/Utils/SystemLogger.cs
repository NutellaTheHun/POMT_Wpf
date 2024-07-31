
namespace Petsi.Utils
{
    public static class SystemLogger
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        public static void LogError(string errorMessage, string sender)
        {
            string fp = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_ERROR_LOG_PATH);

            if(fp == null || fp == ""){ return; }

            File.AppendAllText(fp, DateTime.Now.ToString() + " " + sender + " : " + errorMessage + "\n");
        }
    }
}
