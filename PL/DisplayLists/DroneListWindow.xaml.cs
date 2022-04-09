using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {

        private readonly BlApi.IBL bl = BL.BL.GetInstance();

        internal ObservableCollection<BO.DroneToList> DroneList
        {
            get => (ObservableCollection<BO.DroneToList>)GetValue(dronesDependency);
            set => SetValue(dronesDependency, value);
        }
        static readonly DependencyProperty dronesDependency = DependencyProperty.Register(
            nameof(DroneList),
            typeof(ObservableCollection<BO.DroneToList>),
            typeof(Window));

        public DroneListWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            WightsSeletor.ItemsSource = Enum.GetValues(typeof(BO.Weight));
            StatusSeletor.ItemsSource = Enum.GetValues(typeof(BO.Status));
            DroneList = new(this.bl.Drones());
        }
        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                DroneList = new(bl.Drones());
            else
            {
                DroneList = new();
                var a = from drone in bl.Drones()
                        where (drone.Weight == (BO.Weight)cb.SelectedItem)
                        select drone;
                DroneList = new ObservableCollection<BO.DroneToList>(a);
            }
        }

        private void StatusSeletor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                DroneList = new(bl.Drones());
            else
            {
                DroneList = new();
                var a = from drone in bl.Drones()
                        where (drone.Status == (BO.Status)cb.SelectedItem)
                        select drone;
                DroneList = new ObservableCollection<BO.DroneToList>(a);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reload();
            new DroneWindow(bl).Show();
        }

        private void mousedoubleclick(object sender, MouseButtonEventArgs e)
        {
            var cb = sender as DataGrid;
            BO.DroneToList a = (BO.DroneToList)cb.SelectedValue;
            try
            {
                new DroneWindow(bl, a.ID).Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Click on properties only please");
            }
        }
        private void Reload()
        {
            DroneList = new(bl.Drones());
        }
    }
}
