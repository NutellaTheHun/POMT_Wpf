using DocumentFormat.OpenXml.Wordprocessing;
using Petsi.Managers;

namespace Petsi.Reports
{
    public class ReportPrintSession
    {
        private Queue<(Func<Task> reportRequest, ReportMetaData metaData)> _printQueue;
        private HashSet<ReportMetaData> _reportMetaData;
        private bool _active;

        public ReportPrintSession()
        {
            _printQueue = new Queue<(Func<Task> reportRequest, ReportMetaData metaData)>();
            _reportMetaData = new HashSet<ReportMetaData>();
            _active = false;
        }

        public async void Enqueue(Func<Task> reportRequest, string reportName, params object[] reportParams)
        {
            var metaData = ReportMetaData.ToMetaData(reportName, reportParams);

            if (_reportMetaData.Contains(metaData))
            {
                return;
            }

            _reportMetaData.Add(metaData);

            if (_printQueue.Count == 0 && !_active)
            {
                _active = true;
                var omp = ModelManagerSingleton.GetInstance().GetOrderModel();
                await omp.RefreshOrderModelAsync();
                _printQueue.Enqueue((reportRequest, metaData));
                await ExecutePrintRequest();
                return;
            }

            _printQueue.Enqueue((reportRequest, metaData));
            
            if (!_active)
            {
                await ExecutePrintRequest();
            }
        }

        private async Task ExecutePrintRequest()
        {
            _active = true;
            while (_printQueue.Count > 0)
            {
                var reportRequest = _printQueue.Dequeue();
                _reportMetaData.Remove(reportRequest.metaData);
                await reportRequest.reportRequest();
            }
            _active = false;
        }

        private class ReportMetaData
        {
            public string Name { get; set; }
            public string ReportParams { get; set; }

            public override bool Equals(object obj)
            {
                if (obj is ReportMetaData other)
                {
                    return Name == other.Name && ReportParams == other.ReportParams;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return (Name + ReportParams).GetHashCode();
            }

            public static ReportMetaData ToMetaData(string reportName, params object[] reportParams)
            {
                string paramSignature = string.Join(",", reportParams.Select(p => p?.ToString() ?? "null"));
                var metaData = new ReportMetaData
                {
                    Name = reportName,
                    ReportParams = paramSignature
                };
                return metaData;
            }
        }
    }
}
