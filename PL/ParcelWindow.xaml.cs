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

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelListWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        private readonly BlApi.IBL bl = BL.BL.GetInstance();

        internal ObservableCollection<BO.ParcelToList> ParcelsList
        {
            get => (ObservableCollection<BO.ParcelToList>)GetValue(parcelsDependency);
            set => SetValue(parcelsDependency, value);
        }
        static readonly DependencyProperty parcelsDependency = DependencyProperty.Register(
            nameof(ParcelsList),
            typeof(ObservableCollection<BO.ParcelToList>),
            typeof(Window));
        public ParcelWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            ParcelsList = new(this.bl.Parcels());
            Main.Content = new AddPage(bl, this);

        }
        public ParcelWindow(BlApi.IBL bl, int? id)
        {
            if (id == null)
            {
                new ParcelListWindow(bl).Show();
                this.Close();
            }
            else
            {
                InitializeComponent();
                //this.bl = bl;
                //var w = this.bl.Drones().ToList().Find(delegate (BO.DroneToList D) { return (D.ID == ID); });
                //List<BO.DroneToList> a = new();
                //a.Add(w);
                //DronesList = new(a);
                //Main.Content = new ActionsPage(bl, (int)ID);
                this.bl = bl;
                var t = this.bl.Parcels().Where(a => id == a.ID);
                ParcelsList = new(t);
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

