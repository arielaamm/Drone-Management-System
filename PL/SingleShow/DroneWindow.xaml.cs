using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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

        private static readonly DependencyProperty dronesDependency = DependencyProperty.Register(
            nameof(Drone),
            typeof(ObservableCollection<BO.DroneToList>),
            typeof(Window));

        public DroneWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            Drone = new(this.bl.Drones());
            dronepage.Content = new AddDrone(bl, this);
        }
        public DroneWindow(BlApi.IBL bl, int? id)
        {
            if (id == null)
            {
                new DroneListWindow(bl).Show();
                Close();
            }
            else
            {
                InitializeComponent();
                this.bl = bl;
                d = this.bl.FindDrone((int)id);
                var t = this.bl.Drones().Where(a => id == a.ID);
                Drone = new(t);
                dronepage.Content = new ActionsDrone(bl, (int)id, this);
            }
        }
        internal new void Close() => base.Close();
        private void GridTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(bl).Show();
            Close();

        }
        BackgroundWorker DroneWorker;
        BO.Drone d;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DroneWorker = new BackgroundWorker();
            DroneWorker.DoWork += Worker_DoWork;
            DroneWorker.RunWorkerAsync(1);
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Action display = foo;
            this.bl.Uploader((int)d.ID, display, DroneWorker.CancellationPending == true);
        }
        //private object Worker_ProgressChanged(object sender, ProgressChangedEventArgs e) => e.UserState;
        private void foo(){Console.WriteLine("iii");}
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                // e.Result throw System.InvalidOperationException
                throw new EmptyException("Canceled!");//NOT THE RIGHT PROBLEM
            }
            else if (e.Error != null)
            {
                // e.Result throw System.Reflection.TargetInvocationException
                throw new EmptyException("Error: " + e.Error.Message);//NOT THE RIGHT PROBLEM "Error: " + e.Error.Message; //Exception Message
            }
            else
            {
                Close();
            }
        }

    }
}
