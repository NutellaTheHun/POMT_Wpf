﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Petsi.Events;
using Petsi.Services;
using Petsi.Units;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for NotifyTableBuilderOverFlow.xaml
    /// </summary>
    public partial class NotifyTableBuilderOverFlow : Window
    {
        private static NotifyTableBuilderOverFlow instance;
        public ObservableCollection<string> OverflowListNames = new ObservableCollection<string>();

        private NotifyTableBuilderOverFlow()
        {
            InitializeComponent();
            overFlowItemListBox.ItemsSource = OverflowListNames;
        }

        public static NotifyTableBuilderOverFlow Instance()
        {
            if (instance == null) { instance = new NotifyTableBuilderOverFlow(); }
            return instance;
        }

        public static void UpdateListNames(List<PetsiOrderLineItem> overflowList)
        {
            foreach (var item in overflowList)
            {
                instance.OverflowListNames.Add(item.ItemName);
            }
        }
        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
