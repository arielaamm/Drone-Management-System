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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ActionsPage.xaml
    /// </summary>
    public partial class ActionsPage : Page
    {
        IBL.IBL bl;
        int ID;
        DateTime time;
        public ActionsPage(IBL.IBL bl,int id)
        {
            InitializeComponent();
            this.bl = bl;
            ID = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReleRelease_from_charging(object sender, RoutedEventArgs e)
        {
            double t = (time - DateTime.Now).Minutes;
            bl.DroneOutCharge(ID, t);
        }

        private void Inserting_for_charging(object sender, RoutedEventArgs e)
        {
            bl.DroneToCharge(ID);
            time = DateTime.Now;
        }

        private void AttacheDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AttacheDrone(ID);
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() == "BLExceptions.ThereIsNoParcel")
                    MessageBox.Show(ex.Message);
            }
            //לשים סגירה
        }

        private void PickUpParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.PickUpParcel(ID);
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() == "BLExceptions.ParcelPastErroeException")
                    MessageBox.Show(ex.Message);
            }
            //לשים סגירה
        }

        private void deliverparcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Parceldelivery(ID);
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() == "BLExceptions.ParcelPastErroeException")
                    MessageBox.Show(ex.Message);
            }
            //לשים סגירה
        }
    }
}
