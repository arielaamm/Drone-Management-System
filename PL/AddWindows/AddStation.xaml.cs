using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Page
    {
        private readonly BlApi.IBL bl;
        private readonly Window parent;
        public AddStation(BlApi.IBL bl, Window parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.bl = bl;

        }

        private void Button_Click_Add_Station(object sender, RoutedEventArgs e)
        {

            int c = bl.Stations().Count();
            try
            {
                BO.Location location = new BO.Location()
                {
                    Lattitude = double.Parse(TextBoxLattitude.Text),
                    Longitude = double.Parse(TextBoxLongitude.Text)
                };
                BO.Station station = new BO.Station()
                {
                    ID = int.Parse(TextBoxID.Text),
                    StationName = TextBoxName.Text,
                    Position = location,
                    ChargeSlots = int.Parse(TextBoxChargeSlots.Text)
                };
                bl.AddStation(station);
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() == "BLExceptions.AlreadyExistException")
                {
                    MessageBox.Show(ex.Message + ", enter anther ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                    MessageBox.Show("Enter the data in the necessary places", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (c < bl.Stations().Count())
            {
                MessageBox.Show("The station successfully added");
                new StationWindow(bl, int.Parse(TextBoxID.Text)).Show();
                parent.Close();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) => parent.Close();
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void LetterValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^A-Za-z ]+$");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
