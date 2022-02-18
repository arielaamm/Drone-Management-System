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
    public partial class AddPage : Page
    {   
        private readonly BlApi.IBL bl;
        public AddPage(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            ModelSeletor.ItemsSource = Enum.GetValues(typeof(BO.Model));
            MaxWeightSeletor.ItemsSource = Enum.GetValues(typeof(BO.Weight));
            List<int> stationToLists = new();
            foreach (var item in bl.FreeChargeslots())
            {
                stationToLists.Add(item.ID);
            }
            if (stationToLists.Count == 0)
            {
                MessageBox.Show("There is no more room for a new drone");
            }
            StartingstationSeletor.ItemsSource = stationToLists;
        }
        BO.Drone drone = new();

        private void ModelSeletor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            drone.Model = "" + (BO.Model)cb.SelectedItem;
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
            int i = 0;
            int c = bl.Drones().Count();
            try
            {
                i = (int)drone.ID; //מעביר ככה את האיידיי של התחנה בלי בלגן ואז דורס אותו  לא למחוק !!!
                drone.ID = int.Parse(TextBoxID.Text);
                drone.Battery = 100;
                drone.HasParcel = false;
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
                new DroneListWindow(bl).Show();
                new DroneWindow(bl,(int)drone.ID).Show();
            }
        }
    }
}
