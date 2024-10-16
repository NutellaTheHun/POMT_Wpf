﻿using System.Windows;
namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        public bool ControlBool;
        public string Message { get; set; }
        public ConfirmationWindow(string? message)
        {
            InitializeComponent();
            DataContext = this;
            if (message == null) 
            {
                Message = "Are you sure?";
            }
            else
            {
                Message = message;
            }
            
        }

        private void Accept_ButtonClick(object sender, RoutedEventArgs e)
        {
            ControlBool = true;
            Close();
        }

        private void Reject_ButtonClick(Object sender, RoutedEventArgs e)
        {
            ControlBool = false;
            Close();
        }

        private void CloseWindow_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
