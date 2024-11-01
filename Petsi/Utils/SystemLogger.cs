
namespace Petsi.Utils
{
    public class SystemLogger
    {
        private static SystemLogger _instance;

        private static readonly object _lock = new();
        private Queue<LogRequest> logQueue;
        private bool _processing;

        private SystemLogger()
        {
            logQueue = new Queue<LogRequest>();
            _processing = false;
        }

        public static SystemLogger Instance()
        {
            if (_instance == null)
            {
                _instance = new SystemLogger();
            }
            return _instance;
        }

        private class LogRequest 
        {
            public string message { get; set; }
            public string logType { get; set; }
            public string? sender { get; set; }
            public LogRequest(string message, string logType, string? sender)
            {
                this.message = message;
                this.sender = sender;
                this.logType = logType;
            }
        }

        private static void ProccessLogRequest()
        {
            Instance()._processing = true;
            LogRequest lr;
            while (Instance()._processing)
            {
                lock (_lock)
                {
                    lr = Instance().logQueue.Dequeue();
                }
                
                string fp = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_ERROR_LOG_PATH);
                if (fp == null || fp == "") { return; }

                lock (_lock)
                {
                    if (lr.sender == null)
                    {
                        File.AppendAllText(fp, $"{DateTime.Now.ToString()} : {PetsiConfig.appRuntimeId} : [{lr.logType}] {lr.message}\n");
                    }
                    else
                    {
                        File.AppendAllText(fp, $"{DateTime.Now.ToString()} : {PetsiConfig.appRuntimeId} : [{lr.logType}] {lr.sender} : {lr.message}\n");
                    }
                }
                if (Instance().logQueue.Count == 0)
                {
                    Instance()._processing = false;
                }
            }
        }

        private static void AddLogRequest(string? sender, string message, string logType)
        {
            lock (_lock)
            {
                Instance().logQueue.Enqueue(new LogRequest(message, logType, sender));
                if (!Instance()._processing)
                {
                    ProccessLogRequest();
                }
            }
        }

        public static void LogStatus(string message)
        {
            RefreshLogCheck();
            AddLogRequest(null, message, "S");
            /*
            string fp = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_ERROR_LOG_PATH);
            if (fp == null || fp == "") { return; }

            lock (_lock)
            {
                File.AppendAllText(fp, $"{DateTime.Now.ToString()} : {PetsiConfig.appRuntimeId} : [S] {message}\n");
            }*/
        }

        public static void LogError(string errorMessage, string sender)
        {
            RefreshLogCheck();
            AddLogRequest(sender, errorMessage, "E");
            /*
            string fp = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_ERROR_LOG_PATH);
            if(fp == null || fp == ""){ return; }

            lock (_lock)
            {
                File.AppendAllText(fp, $"{DateTime.Now.ToString()} : {PetsiConfig.appRuntimeId} : [E] {sender} : {errorMessage}\n");
            }*/
        }

        public static void LogWarning(string message)
        {
            RefreshLogCheck();
            AddLogRequest(null, message, "W");
            /*
            string fp = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_ERROR_LOG_PATH);
            if (fp == null || fp == "") { return; }

            lock (_lock)
            {
                File.AppendAllText(fp, $"{DateTime.Now.ToString()} : {PetsiConfig.appRuntimeId} : [W] {message}\n");
            }*/
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
