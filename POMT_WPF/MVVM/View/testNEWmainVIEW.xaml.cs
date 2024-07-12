using System.Windows;
using System.Windows.Input;
using Petsi.Services;
using POMT_WPF.MVVM.ViewModel;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for testNEWmainVIEW.xaml
    /// </summary>
    public partial class testNEWmainVIEW : Window
    {
        MainViewModel vm; 
        public testNEWmainVIEW()
        {
            vm = MainViewModel.Instance();
            DataContext = vm;
            InitializeComponent();
            ErrorService.Instance().ExceptionHandlerErrorEvent += ExceptionHandlerShow;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ExceptionHandlerShow(object sender, string errorMessage)
        {
            ExceptionHandlerShowWindow ErrWin = new ExceptionHandlerShowWindow(errorMessage);
            ErrWin.Show();
        }
    }
}
