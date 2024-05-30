using POMT_WPF.MVVM.View;
using POMT_WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace POMT_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel mwvm = new MainWindowViewModel();
            //this.DataContext = mwvm;
            //DataContext = new MainWindowViewModel();
            dashboardDataGrid.ItemsSource = mwvm.Orders;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) { this.DragMove(); }
        }

        private bool isMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (isMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    isMaximized = false;
                }
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                isMaximized = true;
            }
        }

        private void membersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddOrder_ButtonClick(object sender, RoutedEventArgs e)
        {
            PetsiOrderWindow petsiOrderWin = new PetsiOrderWindow();
            petsiOrderWin.Show();
        }
        private void ReportWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            ReportWindow reportWin = new ReportWindow();
            reportWin.Show();
        }
        private void LabelWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            LabelWindow LabelWin = new LabelWindow();
            LabelWin.Show();
        }

        private void SettingsWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            SettingsWindow SettingsWin = new SettingsWindow();
            SettingsWin.Show();
        }

        private void CloseMainWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}