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
            foreach (var item in bl.FreeChargeslots())
            {
                stationToLists.Add(item.ID);
            }
            if (stationToLists.Count==0)
            {
                MessageBox.Show("There is no more room for a new drone");
                this.Close();
            }
            StartingstationSeletor.ItemsSource = stationToLists;
            DronesList = new(this.bl.Drones());
        }
        public DroneWindow(IBL.IBL bl,int ID)
        {
            InitializeComponent();
            this.bl = bl;
            IDLabel.IsEnabled = false;
            ModelLabel.IsEnabled = false;
            MaxWieghLabel.IsEnabled = false;
            Starting_StationLabel.IsEnabled = false;
            TextBoxID.IsEnabled = false;
            ModelSeletor.IsEnabled = false;
            MaxWeightSeletor.IsEnabled = false;
            StartingstationSeletor.IsEnabled = false;
            BntAdd.IsEnabled = false;
            DataGird.IsEnabled = false;

            DronesList = new(this.bl.Drones());
        }
        IBL.BO.Drone drone=new();
        private void Button_Click_Add_Drone(object sender, RoutedEventArgs e)
        {
            int i=0;
            int c = DronesList.Count;
            try
            { 
                i = (int)drone.ID; //מעביר ככה את האיידיי של התחנה בלי בלגן ואז דורס אותו  לא למחוק !!!
                drone.ID = int.Parse(TextBoxID.Text); 
                drone.Battery = 100;
                drone.HasParcel = false;
                bl.AddDrone(drone, i);
            }
            catch(Exception ex)
            {
                if (ex.GetType().ToString() == "BLExceptions.AlreadyExistException")
                    MessageBox.Show(ex.Message);
                else
                    MessageBox.Show("Enter the data in the necessary places");
            }
            this.Close();
            new DroneWindow(bl).Show();
            if (c< DronesList.Count)
            {
                MessageBox.Show("The drone successfully added");
            }
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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
