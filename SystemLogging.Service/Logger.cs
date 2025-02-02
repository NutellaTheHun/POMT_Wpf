
namespace SystemLogging.Service
{
    public class Logger
    {
        private static Logger _instance;

        private static readonly object _lock = new();
        private Queue<LogRequest> logQueue;
        private bool _processing;
        private string errorLogFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "petsiDir\\errorLog.txt");
        private string runtimeId;
        private Logger()
        {
            logQueue = new Queue<LogRequest>();
            _processing = false;
        }

        public static Logger Instance()
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }
            return _instance;
        }

        public static void SetRunTimeId(string id)
        {
            Instance().runtimeId = id;
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

                // UPDATE TO CORRECT CHECK
                //if (_instance.errorLogFilePath == null || _instance.errorLogFilePath == "") { return; }

                lock (_lock)
                {
                    if (lr.sender == null)
                    {
                        File.AppendAllText(_instance.errorLogFilePath, $"{DateTime.Now.ToString()} : {Instance().runtimeId} : [{lr.logType}] {lr.message}\n");
                    }
                    else
                    {
                        File.AppendAllText(_instance.errorLogFilePath, $"{DateTime.Now.ToString()} : {Instance().runtimeId} : [{lr.logType}] {lr.sender} : {lr.message}\n");
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
        }

        public static void LogError(string errorMessage, string sender)
        {
            RefreshLogCheck();
            AddLogRequest(sender, errorMessage, "E");
        }

        public static void LogWarning(string message)
        {
            RefreshLogCheck();
            AddLogRequest(null, message, "W");
        }

        static int daysUntilRefresh = 7;

        //UPDATE TO TRIM LINES, NOT RESET ENTIRE LOG
        private static void RefreshLogCheck()
        {
            DateTime creation = File.GetCreationTime(Instance().errorLogFilePath);
            DateTime refreshDate = creation.AddDays(daysUntilRefresh);
            if (DateTime.Today >= refreshDate)
            {

                lock (_lock)
                {
                    File.WriteAllText(Instance().errorLogFilePath, String.Empty);
                    File.SetCreationTime(Instance().errorLogFilePath, DateTime.Today);
                }
            }
        }
    }
}

