﻿using System.Windows;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BlApi.IBL bl = BL.BL.GetInstance();
        public MainWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        private void DroneListButton_Click(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(bl).Show();
            Close();

        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GridTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ParcelButton_Click(object sender, RoutedEventArgs e)
        {
            new ParcelListWindow(bl).Show();
            Close();

        }

        private void StationButton_Click(object sender, RoutedEventArgs e)
        {
            new StationListWindow(bl).Show();
            Close();

        }

        private void CustomerButton_Click(object sender, RoutedEventArgs e)
        {

            new CustomerListWindow(bl).Show();
            Close();
        }
    }
}
