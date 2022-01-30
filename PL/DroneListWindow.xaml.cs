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
using System.Windows.Shapes;
namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        private IBL.IBL pram;
        public DroneListWindow(IBL.IBL bl)
        {
            InitializeComponent();
            pram = bl;
            WightsSeletor.ItemsSource =Enum.GetNames(typeof(IBL.BO.WEIGHT));
            StatusSeletor.ItemsSource = Enum.GetNames(typeof(IBL.BO.STATUS));
            this.DroneListView.DataContext = pram.drones();
        }
        private void WightsSeletor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void StatusSeletor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
