using BLExceptions;
using BO;
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
        public int? id;
        public BackgroundWorker DroneWorker = new BackgroundWorker();
        ObservableCollection<BO.DroneToList> drone;

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
                this.id = id;
                InitializeComponent();
                this.bl = bl;
                d = this.bl.FindDrone((int)id);
                UpDateAction();
                dronepage.Content = new ActionsDrone(bl, (int)id, this);
            }
        }
        internal new void Close() => base.Close();
        private void GridTitleBar_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(bl).Show();
            Close();

        }

        private readonly BO.Drone d;
        private void Button_Click_ON(object sender, RoutedEventArgs e)
        {
            DroneWorker.DoWork += Worker_DoWork;
            DroneWorker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            DroneWorker.WorkerReportsProgress = true;
            DroneWorker.WorkerSupportsCancellation = true;
            DroneWorker.ProgressChanged += Worker_ProgressChanged;

            Automatic.Click -= Button_Click_ON;
            Automatic.Click += Button_Click_OFF;
            Automatic.Content = "Regular";
            run = true;

            DroneWorker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                MessageBox.Show($"Drone {id}: I'm done");
            }
            else if (e.Error != null)
            {
                MessageBox.Show("problem, he keep running");
            }
        }


        private void Button_Click_OFF(object sender, RoutedEventArgs e)
        {
            Automatic.Click += Button_Click_ON;
            Automatic.Click -= Button_Click_OFF;
            Automatic.Content = "Automatic";
            run = false;
            MessageBox.Show("please wait will the drone finish his last act");
            if (DroneWorker.WorkerSupportsCancellation == true)
                // Cancel the asynchronous operation.
                DroneWorker.CancelAsync();
        }


        private bool GetRun() => run;
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Action display = ReportProgress;
            try
            {

                bl.Uploader((int)d.ID, display, GetRun);
            }
            catch (DontHaveEnoughPowerException ex)
            {
                run = false;
                MessageBox.Show(ex.Message.ToString());
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }

        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e) 
        {
            Drone = drone;
        }
        public void ReportProgress()
        {
            drone = new(bl.Drones().Where(a => id == a.ID));
            DroneWorker.ReportProgress(1, null);
        }

        private void UpDateAction()
        {
            Drone = new(bl.Drones().Where(a => id == a.ID));
            if (!run)
                MessageBox.Show($"Drone {id}: I'm done");
        }
    }
}
