using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BlApi.IBL bl = BLFactory.GetBL("BL");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DroneListButton_Click(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(bl).Show();
            this.Close();

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
            this.Close();

        }

        private void StationButton_Click(object sender, RoutedEventArgs e)
        {
            new StationListWindow(bl).Show();
            this.Close();

        }

        private void CustomerButton_Click(object sender, RoutedEventArgs e)
        {
            
            new CustomerListWindow(bl).Show();
            this.Close();
        }
    }
}
