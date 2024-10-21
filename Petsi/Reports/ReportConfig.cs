
using Petsi.Units;

namespace Petsi.Reports
{
    public class ReportConfig
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsPrint { get; set; }
        public bool IsExport { get; set; }
        public bool RetailFilter { get; set; }
        public bool SquareFilter { get; set; }
        public bool WholesaleFilter { get; set; }
        public bool SpecialFilter { get; set; }
        public bool EzCaterFilter { get; set; }
        public bool FarmerFilter { get; set; }
        public string? ReportName { get; set; }
        public List<BackListItem>? Template { get; set; }

        public ReportConfig(
            DateTime? startDate, DateTime? endDate, 
            bool isPrint,         bool isExport,        
            bool retailFilter,    bool squareFilter,  
            bool wholesaleFilter, bool specialFilter, 
            bool ezCaterFilter,   bool farmerFilter, 
            string? reportName,
            List<BackListItem>? template)
        {
            StartDate = startDate;
            EndDate = endDate;
            IsPrint = isPrint;
            IsExport = isExport;
            RetailFilter = retailFilter;
            SquareFilter = squareFilter;
            WholesaleFilter = wholesaleFilter;
            SpecialFilter = specialFilter;
            EzCaterFilter = ezCaterFilter;
            FarmerFilter = farmerFilter;
            ReportName = reportName;
            Template = template;
        }
    }
}
