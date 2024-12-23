﻿using Petsi.Events.ReportEvents;
using Petsi.Reports.PageBuilder;

namespace Petsi.Reports.ReportBuilder
{
    /// <inheritdoc/>
    public class ReportBuilderBackList : ReportBuilderBase
    {
        /// <inheritdoc/>
        public ReportBuilderBackList(Report report) : base(report)
        {
            BackListOverflowEvent.Instance.BacklistPieOverflow += AddPieOverflowPageBuilder;
            BackListOverflowEvent.Instance.BacklistPastryOverflow += AddPastryOverflowPageBuilder;
            ConfigureBuilders();
        }

        protected override void ConfigureBuilders()
        {
            pageBuilders.Add(new PageBuilderBackListPie(_report, null));
            pageBuilders.Add(new PageBuilderBackListPastry(_report, null));
        }

        private void AddPieOverflowPageBuilder(object sender, EventArgs e)
        {
            //pageBuilders.Add(new PageBuilderPieOverflow(_report));
        }
        private void AddPastryOverflowPageBuilder(object sender, EventArgs e)
        {
            //pageBuilders.Add(new PageBuilderPastryOverflow(_report));
        }
    }
}
