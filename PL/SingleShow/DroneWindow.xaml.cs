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
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Navigation;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private readonly BlApi.IBL bl = BL.BL.GetInstance();

        internal ObservableCollection<BO.DroneToList> Drone
        {
            get => (ObservableCollection<BO.DroneToList>)GetValue(dronesDependency);
            set => SetValue(dronesDependency, value);
        }
        static readonly DependencyProperty dronesDependency = DependencyProperty.Register(
            nameof(Drone),
            typeof(ObservableCollection<BO.DroneToList>),
            typeof(Window));

        public DroneWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            Drone = new(this.bl.Drones());
            dronepage.Content = new AddDrone(bl,this);
        }
        public DroneWindow(BlApi.IBL bl, int? id)
        {
            if (id == null)
            {
                new DroneListWindow(bl).Show();
                this.Close();
            }
            else
            {
                InitializeComponent();
                this.bl = bl;
                var t = this.bl.Drones().Where(a => id == a.ID);
                Drone = new(t);
                dronepage.Content = new ActionsDrone(bl, (int)id, this);
            }
        }

        bool closing = false;
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => e.Cancel = closing;
        internal new void Close()
        {
            closing = true;
            base.Close();
        }
    }
}
