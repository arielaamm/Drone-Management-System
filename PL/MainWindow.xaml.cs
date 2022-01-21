using System;
using System.Collections.Generic;
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
using IBL;
namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL.IBL bl=new BL.BL();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_viewDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneListView(bl).Show();
        }
    }
}
