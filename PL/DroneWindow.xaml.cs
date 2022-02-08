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
using System.Text.RegularExpressions;


namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private readonly IBL.IBL bl;
        internal ObservableCollection<IBL.BO.DroneToList> DronesList
        {
            get => (ObservableCollection<IBL.BO.DroneToList>)GetValue(dronesDependency);
            set => SetValue(dronesDependency, value);
        }
        static readonly DependencyProperty dronesDependency = DependencyProperty.Register(
            nameof(DronesList),
            typeof(ObservableCollection<IBL.BO.DroneToList>),
            typeof(Window));

        public DroneWindow(IBL.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            ModelSeletor.ItemsSource = Enum.GetValues(typeof(IBL.BO.Model));
            MaxWeightSeletor.ItemsSource = Enum.GetValues(typeof(IBL.BO.Weight));
            List<int> stationToLists = new();
            foreach (var item in bl.Stations())
            {
                stationToLists.Add(item.ID);
            }
            StartingstationSeletor.ItemsSource = stationToLists;
            DronesList = new(this.bl.Drones());
        }
        IBL.BO.Drone drone=new();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i = (int)drone.ID;//מעביר ככה את האיידיי של התחנה בלי בלגן ואז דורס אותו  לא למחוק !!!
            drone.ID = int.Parse(TextBoxID.Text);
            bl.AddDrone(drone, i);
            DronesList = new(bl.Drones());

        }

        private void MaxWeightSeletor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            drone.Weight = (IBL.BO.Weight)cb.SelectedItem;
        }

        private void ModelSeletor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            drone.Model = ""+(IBL.BO.Model)cb.SelectedItem;
        }

        private void StartingstationSeletor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            drone.ID = (int)cb.SelectedItem;
        }
    }
}
