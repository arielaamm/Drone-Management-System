using System;
using System.Collections.Generic;
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
using BlApi;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

namespace PL
{
    /// <summary>
    /// Interaction logic for DronePanel.xaml
    /// </summary>
    public partial class DronePanel : Window
    {
        private readonly BlApi.IBL bl = BL.BL.GetInstance();
        public bool IsOpened { get; private set; } = true;
        ObservableCollection<BO.DroneToList> Drones = new ObservableCollection<BO.DroneToList>();
        BackgroundWorker stationPanelWorker = new BackgroundWorker();
        Thread stationPanelBackgroundThread = null;
        int StationCode = 1;//Drone id
        public DronePanel()
        {
            InitializeComponent();
            yellowSign.DataContext = station;
            StationCode = station.StationCode;
            this.Title += $" - {station.Name}, {StationCode}";

            stationPanelWorker.WorkerReportsProgress = true;
            stationPanelWorker.WorkerSupportsCancellation = true;
            stationPanelWorker.DoWork += StationPanelWorker_DoWork;
            stationPanelWorker.ProgressChanged += StationPanelWorker_ProgressChanged;
            stationPanelWorker.RunWorkerAsync();
        }
        private void StationPanelWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var line = (BO.DroneToList)e.UserState;
            if (line.ID == -1) lblLastLine.Text = "Last line: " + line.LineNumber;
            else if (line.ID == -2) lblLastLine.Text = "";
            else
            {
                int index = Drones.ToList().FindIndex(i => i.ID == line.ID &&
                                                       i.StartTime == line.StartTime);
                if (index != -1) Drones[index] = line;
                else Drones.Add(line);

                for (int i = 0; i < Drones.Count; i++)
                    if (Drones[i].ArrivalTime <= TimeSpan.Zero)
                        Drones.RemoveAt(i);

                Drones = new ObservableCollection<BO.DroneToList>(Drones.OrderBy(x => x.ArrivalTime));
                ObservableCollection<BO.DroneToList> topFive = new ObservableCollection<BO.DroneToList>();
                for (int i = 0; i < Math.Min(5, Drones.Count); i++) topFive.Add(Drones[i]);
                lstLineTimings.ItemsSource = topFive;
            }
        }

        private void StationPanelWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            stationPanelBackgroundThread = Thread.CurrentThread;
            BL.SetStationPanel(StationCode, (line) =>
            {
                try
                {
                    stationPanelWorker.ReportProgress(1, line);
                }
                catch (Exception) { }
            });
            while (!stationPanelWorker.CancellationPending)
                try { Thread.Sleep(Timeout.Infinite); }
                catch (ThreadInterruptedException) { }
            BL.SetStationPanel(StationCode, (line) => { }, -1);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            stationPanelWorker?.CancelAsync();
            stationPanelBackgroundThread?.Interrupt();
            IsOpened = false;
        }
    }
    
}
