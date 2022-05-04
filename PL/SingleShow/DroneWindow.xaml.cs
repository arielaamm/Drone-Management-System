using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
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
        public bool run = true;
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

        private BackgroundWorker DroneWorker;
        private readonly BO.Drone d;
        private void Button_Click_ON(object sender, RoutedEventArgs e)
        {
            DroneWorker = new BackgroundWorker();
            DroneWorker.DoWork += Worker_DoWork;
            Automatic.Click -= Button_Click_ON;
            Automatic.Click += Button_Click_OFF;
            Automatic.Content = "Regular";
            run = true;
            DroneWorker.RunWorkerAsync();
            DroneWorker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            DroneWorker.WorkerReportsProgress = true;
            DroneWorker.WorkerSupportsCancellation = true;



        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                // e.Result throw System.InvalidOperationException
                //resultLabel.Content = "Canceled!";
            }
            else if (e.Error != null)
            {
                // e.Result throw System.Reflection.TargetInvocationException
                // resultLabel.Content = "Error: " + e.Error.Message; //Exception Message
            }
            else
            {

            }
        }


        private void Button_Click_OFF(object sender, RoutedEventArgs e)
        {
            Automatic.Click += Button_Click_ON;
            Automatic.Click -= Button_Click_OFF;
            Automatic.Content = "Automatic";
            run = false;
            if (DroneWorker.WorkerSupportsCancellation == true)
                // Cancel the asynchronous operation.
                DroneWorker.CancelAsync();
        }


        private bool GetRun()
        {
            Thread.Sleep(2500);
            return run;
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Action display = foo;
            try
            {
                bl.Uploader((int)d.ID, display, GetRun);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }
        //private object Worker_ProgressChanged(object sender, ProgressChangedEventArgs e) => e.UserState;
        public static void foo() { MessageBox.Show("sasasasa"); }

    }
}
