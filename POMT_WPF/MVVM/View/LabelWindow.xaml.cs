using Petsi.Utils;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for LabelWindow.xaml
    /// </summary>
    public partial class LabelWindow : Window
    {
        LabelWindowViewModel viewModel;
        DateTime targetDate;

        enum LabelTypes
        {
            Standard,
            Small,
            Round
        }
        LabelTypes selectedType;


        public LabelWindow()
        {
            viewModel = new LabelWindowViewModel();
            DataContext = this;
            InitializeComponent();
        }
        private void Standard_ButtonClick(Object sender, RoutedEventArgs e)
        {
            selectedType = LabelTypes.Standard;
        }
        private void Small_ButtonClick(Object sender, RoutedEventArgs e)
        {
            selectedType = LabelTypes.Small;
        }
        private void Round_ButtonClick(Object sender, RoutedEventArgs e)
        {
            selectedType = LabelTypes.Round;
        }
        private void Print_ButtonClick(Object sender, RoutedEventArgs e)
        {
            if (datePicker.SelectedDate == null) 
            { PetsiOrderFormErrorWindow error = new PetsiOrderFormErrorWindow("Please select a date."); return; }

            switch (selectedType)
            {
                case LabelTypes.Standard:
                    viewModel.PrintStandard(targetDate);
                break;

                case LabelTypes.Small:
                    viewModel.PrintSmall(targetDate);
                break;

                case LabelTypes.Round:
                    viewModel.PrintRound(targetDate);
                break;

                default:
                    SystemLogger.Log("LABELWINDOW: PRINT_BUTTONCLICK SWITCHCASE DEFAULT");
                break;
            }
        }
        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
