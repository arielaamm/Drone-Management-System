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
    public partial class ActionsDrone : Page
    {
        readonly BlApi.IBL bl;
        readonly int id;
        DateTime time;
        readonly Window parent;
        public ActionsDrone(BlApi.IBL bl,int id, Window parent)
        {            
            InitializeComponent();
            this.parent = parent;
            this.bl = bl;
            this.id = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e) => parent.Close();

        private void ReleRelease_from_charging(object sender, RoutedEventArgs e)
        {
            double t = (time - DateTime.Now).Minutes;

            try
            {
                bl.DroneOutCharge(id, t);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Inserting_for_charging(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DroneToCharge(id);
                time = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AttacheDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AttacheDrone(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //לשים סגירה
        }

        private void PickUpParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.PickUpParcel(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //לשים סגירה
        }

        private void Deliverparcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Parceldelivery(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //לשים סגירה
        }
    }
}
