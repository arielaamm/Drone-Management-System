using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for AddPage.xaml
    /// </summary>
    public partial class AddDrone : Page
    {   
        private readonly BlApi.IBL bl;
        Window parent;
        public AddDrone(BlApi.IBL bl, Window parent)
        {            
            InitializeComponent();
            this.bl = bl;
            this.parent = parent;
            ModelSeletor.ItemsSource = Enum.GetValues(typeof(BO.Model));
            MaxWeightSeletor.ItemsSource = Enum.GetValues(typeof(BO.Weight));
            List<int> stationToLists = new();
            List <BO.StationToList> stations=bl.FreeChargeslots().ToList();
            int i = 0;
            while (i!=bl.FreeChargeslots().Count())
            {
                stationToLists.Add(stations[i].ID);
                i++;
            }
            if (stationToLists.Count == 0)
            {
                MessageBox.Show("There is no more room for a new drone");
            }
            StartingstationSeletor.ItemsSource = stationToLists;
        }
        BO.Drone drone = new();

        private void Button_Click(object sender, RoutedEventArgs e) => parent.Close();
        private void ModelSeletor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            drone.Model = (BO.Model)cb.SelectedItem;
        }

        private void MaxWeightSeletor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            drone.Weight = (BO.Weight)cb.SelectedItem;
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

        private void Button_Click_Add_Drone(object sender, RoutedEventArgs e)
        {
            int c = bl.Drones().Count();
            try
            {
                int i = (int)drone.ID;
                drone.ID = int.Parse(TextBoxID.Text);
                bl.AddDrone(drone, i);
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() == "BLExceptions.AlreadyExistException")
                {
                    MessageBox.Show(ex.Message + ", enter anther ID");

                }
                else
                    MessageBox.Show("Enter the data in the necessary places");
            }
            if (c < bl.Drones().Count())
            {
                MessageBox.Show("The drone successfully added");
                parent.Close();
                new DroneWindow(bl,(int)drone.ID).Show();
            }
        }
    }
}
