using Petsi.Reports.PageBuilder;

namespace Petsi.Reports.ReportBuilder
{
    public class ReportBuilderBackListPie : ReportBuilderBase
    {
        public ReportBuilderBackListPie(Report report) : base(report) { }
  
        protected override void ConfigureBuilders()
        {
            pageBuilders.Add(new PageBuilderBackListPie(_report));
        }
    }
}
