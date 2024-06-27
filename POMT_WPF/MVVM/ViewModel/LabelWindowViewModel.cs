﻿
using Petsi.Managers;
using Petsi.Services;
using Petsi.Utils;

namespace POMT_WPF.MVVM.ViewModel
{
    public class LabelWindowViewModel : ViewModelBase
    {
        LabelService ls;

        public LabelWindowViewModel()
        {
            ls = (LabelService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_LABEL);
        }

        public void PrintRound(DateTime targetDate)
        {
            ls.Print_Round(targetDate);
        }

        public void PrintSmall(DateTime targetDate)
        {
            ls.Print_2x1(targetDate);
        }

        public void PrintStandard(DateTime targetDate)
        {
            ls.Print_4x2(targetDate);
        }
    }
}