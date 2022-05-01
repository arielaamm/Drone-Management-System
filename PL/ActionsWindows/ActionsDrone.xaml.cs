using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for ActionsPage.xaml
    /// </summary>
    public partial class ActionsDrone : Page
    {
        private readonly BlApi.IBL bl;
        private readonly int id;
        private BO.Drone Drone;
        private DateTime time = DateTime.MinValue;
        private readonly Window parent;
        public ActionsDrone(BlApi.IBL bl, int id, Window parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.bl = bl;
            this.id = id;
            Drone = bl.FindDrone(id);
            if (Drone.Status == BO.Status.BELONG)
            {
                AttacheDrone.Visibility = Visibility.Hidden;
            }
            if (Drone.Status == BO.Status.PICKUP)
            {
                AttacheDrone.Visibility = Visibility.Hidden;
                PickUpParcel.Visibility = Visibility.Hidden;
            }
            if (Drone.Status == BO.Status.FREE)
            {
                ReleaseFromCharging.Visibility = Visibility.Hidden;
                txtReleaseFromCharging.Visibility = Visibility.Hidden;
            }
            if (Drone.Status == BO.Status.MAINTENANCE)
            {
                txtInsertingForCharging.Visibility = Visibility.Hidden;
                InsertingForCharging.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(bl).Show();
            parent.Close();
        }
        private void Release_from_charging(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Please wait while the drone Released...", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                if (time == DateTime.MinValue)
                    time = DateTime.Now;
                do
                {
                    Thread.Sleep(500);
                    Drone.Status = BO.Status.MAINTENANCE;
                    bl.UpdateDrone(Drone);
                    bl.DroneOutCharge((int)Drone.ID, (DateTime.Now - time).TotalMinutes);
                    Drone = bl.FindDrone(id);
                    Drone.Status = BO.Status.FREE;
                    bl.UpdateDrone(Drone);
                } while (Drone.Battery < 100);
                Reload();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Inserting_for_charging(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DroneToCharge(id);
                time = DateTime.Now;
                Reload();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AttacheDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AttacheDrone(id);
                Reload();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //לשים סגירה
        }

        private void PickUpParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Please wait while the drone Released...", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                if (time == DateTime.MinValue)
                    time = DateTime.Now;
                do
                {
                    System.Threading.Thread.Sleep(500);
                    Drone.Status = BO.Status.MAINTENANCE;
                    bl.UpdateDrone(Drone);
                    bl.DroneOutCharge((int)Drone.ID, (DateTime.Now - time).TotalMinutes);
                    Drone = bl.FindDrone(id);
                    Drone.Status = BO.Status.BELONG;
                    bl.UpdateDrone(Drone);
                } while (Drone.Battery < 100);
                bl.PickUpParcel(id);
                Reload();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //לשים סגירה
        }

        private void Deliverparcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Parceldelivery(id);
                Reload();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //לשים סגירה
        }
        private void Button_Click_Delete_Drone(object sender, RoutedEventArgs e)
        {
            try
            {
                var drone = bl.FindDrone(id);
                bl.DeleteDrone(drone);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            new DroneListWindow(bl).Show();
            parent.Close();
        }
        private void Reload()
        {
            new DroneWindow(bl, id).Show();
            parent.Close();
        }
    }
}
