using POMT_WPF.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            var converter = new BrushConverter();
            ObservableCollection<Member> members = new ObservableCollection<Member>();

            //Create DataGrid Items info
            members.Add(new Member { Number = "1", Character = "v", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Steve Gran", Position = "Coach", Email = "213@fasw.com", Phone = "123-432-1265" });
            members.Add(new Member { Number = "2", Character = "x", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "Reza Alavi", Position = "Admin", Email = "asd2@gasdf.com", Phone = "144-324-4613" });
            members.Add(new Member { Number = "3", Character = "a", BgColor = (Brush)converter.ConvertFromString("#ff8f00"), Name = "Charles Dow", Position = "Coach", Email = "34tds@gmail.com", Phone = "758-452-2176" });
            members.Add(new Member { Number = "4", Character = "e", BgColor = (Brush)converter.ConvertFromString("#ff5252"), Name = "Krill Cenia", Position = "Manager", Email = "aaadaw@gmail.com", Phone = "741-164-3947" });
            members.Add(new Member { Number = "5", Character = "r", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "Crud joe", Position = "Admin", Email = "2e21e@gaa.com", Phone = "123-599-8567" });
            members.Add(new Member { Number = "6", Character = "g", BgColor = (Brush)converter.ConvertFromString("#ff6d00"), Name = "Ali Ali", Position = "Coach", Email = "34td@gmail.com", Phone = "543-648-7386" });
            members.Add(new Member { Number = "7", Character = "a", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "Gran Fromo", Position = "Manager", Email = "asd2@asd.com", Phone = "354-786-4354" });
            members.Add(new Member { Number = "8", Character = "q", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "Haliburton Waloby", Position = "Manager", Email = "aawfffwe@gcca.com", Phone = "378-375-4835" });
            members.Add(new Member { Number = "9", Character = "j", BgColor = (Brush)converter.ConvertFromString("#1e62fa"), Name = "graten frow", Position = "Coach", Email = "asd2@gdaa.com", Phone = "673-865-1531" });
            members.Add(new Member { Number = "10", Character = "p", BgColor = (Brush)converter.ConvertFromString("#1e62fa"), Name = "halis maloy", Position = "Admin", Email = "356ffa@gaa.com", Phone = "513-791-9718" });

            members.Add(new Member { Number = "1", Character = "v", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Steve Gran", Position = "Coach", Email = "213@fasw.com", Phone = "123-432-1265" });
            members.Add(new Member { Number = "2", Character = "x", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "Reza Alavi", Position = "Admin", Email = "asd2@gasdf.com", Phone = "144-324-4613" });
            members.Add(new Member { Number = "3", Character = "a", BgColor = (Brush)converter.ConvertFromString("#ff8f00"), Name = "Charles Dow", Position = "Coach", Email = "34tds@gmail.com", Phone = "758-452-2176" });
            members.Add(new Member { Number = "4", Character = "e", BgColor = (Brush)converter.ConvertFromString("#ff5252"), Name = "Krill Cenia", Position = "Manager", Email = "aaadaw@gmail.com", Phone = "741-164-3947" });
            members.Add(new Member { Number = "5", Character = "r", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "Crud joe", Position = "Admin", Email = "2e21e@gaa.com", Phone = "123-599-8567" });
            members.Add(new Member { Number = "6", Character = "g", BgColor = (Brush)converter.ConvertFromString("#ff6d00"), Name = "Ali Ali", Position = "Coach", Email = "34td@gmail.com", Phone = "543-648-7386" });
            members.Add(new Member { Number = "7", Character = "a", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "Gran Fromo", Position = "Manager", Email = "asd2@asd.com", Phone = "354-786-4354" });
            members.Add(new Member { Number = "8", Character = "q", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "Haliburton Waloby", Position = "Manager", Email = "aawfffwe@gcca.com", Phone = "378-375-4835" });
            members.Add(new Member { Number = "9", Character = "j", BgColor = (Brush)converter.ConvertFromString("#1e62fa"), Name = "graten frow", Position = "Coach", Email = "asd2@gdaa.com", Phone = "673-865-1531" });
            members.Add(new Member { Number = "10", Character = "p", BgColor = (Brush)converter.ConvertFromString("#1e62fa"), Name = "halis maloy", Position = "Admin", Email = "356ffa@gaa.com", Phone = "513-791-9718" });

            members.Add(new Member { Number = "1", Character = "v", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Steve Gran", Position = "Coach", Email = "213@fasw.com", Phone = "123-432-1265" });
            members.Add(new Member { Number = "2", Character = "x", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "Reza Alavi", Position = "Admin", Email = "asd2@gasdf.com", Phone = "144-324-4613" });
            members.Add(new Member { Number = "3", Character = "a", BgColor = (Brush)converter.ConvertFromString("#ff8f00"), Name = "Charles Dow", Position = "Coach", Email = "34tds@gmail.com", Phone = "758-452-2176" });
            members.Add(new Member { Number = "4", Character = "e", BgColor = (Brush)converter.ConvertFromString("#ff5252"), Name = "Krill Cenia", Position = "Manager", Email = "aaadaw@gmail.com", Phone = "741-164-3947" });
            members.Add(new Member { Number = "5", Character = "r", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "Crud joe", Position = "Admin", Email = "2e21e@gaa.com", Phone = "123-599-8567" });
            members.Add(new Member { Number = "6", Character = "g", BgColor = (Brush)converter.ConvertFromString("#ff6d00"), Name = "Ali Ali", Position = "Coach", Email = "34td@gmail.com", Phone = "543-648-7386" });
            members.Add(new Member { Number = "7", Character = "a", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "Gran Fromo", Position = "Manager", Email = "asd2@asd.com", Phone = "354-786-4354" });
            members.Add(new Member { Number = "8", Character = "q", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "Haliburton Waloby", Position = "Manager", Email = "aawfffwe@gcca.com", Phone = "378-375-4835" });
            members.Add(new Member { Number = "9", Character = "j", BgColor = (Brush)converter.ConvertFromString("#1e62fa"), Name = "graten frow", Position = "Coach", Email = "asd2@gdaa.com", Phone = "673-865-1531" });
            members.Add(new Member { Number = "10", Character = "p", BgColor = (Brush)converter.ConvertFromString("#1e62fa"), Name = "halis maloy", Position = "Admin", Email = "356ffa@gaa.com", Phone = "513-791-9718" });

            members.Add(new Member { Number = "1", Character = "v", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "Steve Gran", Position = "Coach", Email = "213@fasw.com", Phone = "123-432-1265" });
            members.Add(new Member { Number = "2", Character = "x", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "Reza Alavi", Position = "Admin", Email = "asd2@gasdf.com", Phone = "144-324-4613" });
            members.Add(new Member { Number = "3", Character = "a", BgColor = (Brush)converter.ConvertFromString("#ff8f00"), Name = "Charles Dow", Position = "Coach", Email = "34tds@gmail.com", Phone = "758-452-2176" });
            members.Add(new Member { Number = "4", Character = "e", BgColor = (Brush)converter.ConvertFromString("#ff5252"), Name = "Krill Cenia", Position = "Manager", Email = "aaadaw@gmail.com", Phone = "741-164-3947" });
            members.Add(new Member { Number = "5", Character = "r", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "Crud joe", Position = "Admin", Email = "2e21e@gaa.com", Phone = "123-599-8567" });
            members.Add(new Member { Number = "6", Character = "g", BgColor = (Brush)converter.ConvertFromString("#ff6d00"), Name = "Ali Ali", Position = "Coach", Email = "34td@gmail.com", Phone = "543-648-7386" });
            members.Add(new Member { Number = "7", Character = "a", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "Gran Fromo", Position = "Manager", Email = "asd2@asd.com", Phone = "354-786-4354" });
            members.Add(new Member { Number = "8", Character = "q", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "Haliburton Waloby", Position = "Manager", Email = "aawfffwe@gcca.com", Phone = "378-375-4835" });
            members.Add(new Member { Number = "9", Character = "j", BgColor = (Brush)converter.ConvertFromString("#1e62fa"), Name = "graten frow", Position = "Coach", Email = "asd2@gdaa.com", Phone = "673-865-1531" });
            members.Add(new Member { Number = "10", Character = "p", BgColor = (Brush)converter.ConvertFromString("#1e62fa"), Name = "halis maloy", Position = "Admin", Email = "356ffa@gaa.com", Phone = "513-791-9718" });

            dashboardDataGrid.ItemsSource = members;
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

    public class Member
    {
        public string Character { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Brush BgColor { get; set; }
    }
}