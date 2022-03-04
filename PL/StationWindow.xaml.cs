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
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        private readonly BlApi.IBL bl = BL.BL.GetInstance();

        internal ObservableCollection<BO.StationToList> StationsList
        {
            get => (ObservableCollection<BO.StationToList>)GetValue(stationsDependency);
            set => SetValue(stationsDependency, value);
        }
        static readonly DependencyProperty stationsDependency = DependencyProperty.Register(
            nameof(StationsList),
            typeof(ObservableCollection<BO.StationToList>),
            typeof(Window));
        public StationWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            StationsList = new(this.bl.Stations());
            //Main.Content = new AddPage(bl, this);
        }
        public StationWindow(BlApi.IBL bl, int? id)
        {
            if (id == null)
            {
                new ParcelListWindow(bl).Show();
                this.Close();
            }
            else
            {
                InitializeComponent();
                this.bl = bl;
             //   var t = this.bl.Drones().Where(a => id == a.ID);
             //   ParcelsList = new(t);
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

