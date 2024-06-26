﻿
using Petsi.Reports;

namespace POMT_WPF.MVVM.ViewModel
{
    public class ReportWindowViewModel : ViewModelBase
    {
        bool frontlist;
        bool backList;
        bool wsAgg;
        bool ws;

        public ReportWindowViewModel()
        {

        }
        public void SetFrontList()
        {
            AllFalse();
            frontlist = true;
        }
        public void SetBackList()
        {
            AllFalse();
            backList = true;
        }
        public void SetWsAggList()
        {
            AllFalse();
            wsAgg = true;
        }
        public void SetWsList()
        {
            AllFalse();
            ws = true;
        }
        private void AllFalse()
        {
            frontlist = false;
            backList = false;
            wsAgg = false;
            ws = false;
        }
        public void ProduceReport(DateTime dt1)
        {
            ReportDirector rd = new ReportDirector();
            if (frontlist)
            {
                rd.CreateFrontList(dt1);
            }
            else if (backList)
            {
                rd.CreateBackList(dt1, null);
            }
            else if (wsAgg)
            {
                rd.CreateWsDay(dt1);
            }
            else if (ws)
            {
                rd.CreateWsDayName(dt1);
            }
        }
    }
}
