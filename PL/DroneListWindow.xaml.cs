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
using System.Windows.Shapes;
namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        private IBL.IBL bl;
        internal ObservableCollection<IBL.BO.DroneToList> Drones
        {
            get => (ObservableCollection<IBL.BO.DroneToList>)GetValue(dronesDependency);
            set => SetValue(dronesDependency, value);
        }
        static readonly DependencyProperty dronesDependency = DependencyProperty.Register(
            nameof(Drones),
            typeof(ObservableCollection<IBL.BO.DroneToList>),
            typeof(Window));

        public DroneListWindow(IBL.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            WightsSeletor.ItemsSource = Enum.GetValues(typeof(IBL.BO.Weight));
            StatusSeletor.ItemsSource = Enum.GetValues(typeof(IBL.BO.Status));
            Drones = new(this.bl.Drones());
        }
        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                Drones = new(bl.Drones());
            else
            {
                Drones = new();
                foreach (var drone in bl.Drones())
                    if (drone.Weight == (IBL.BO.Weight)cb.SelectedItem)
                        Drones.Add(drone);
            }
        }
        private void StatusSeletor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
