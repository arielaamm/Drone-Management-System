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
        internal ObservableCollection<BO.StationToList> Station
        {
            get => (ObservableCollection<BO.StationToList>)GetValue(stationsDependency);
            set => SetValue(stationsDependency, value);
        }
        static readonly DependencyProperty stationsDependency = DependencyProperty.Register(
            nameof(Station),
            typeof(ObservableCollection<BO.StationToList>),
            typeof(Window));
        public StationWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            Station = new(this.bl.Stations());
            StationsPage.Content = new AddStation(bl, this);
        }
        public StationWindow(BlApi.IBL bl, int? id)
        {
            if (id == null)
            {
                new StationListWindow(bl).Show();
                this.Close();
            }
            else
            {
                InitializeComponent();
                this.bl = bl;
                var t = this.bl.Stations().Where(a => id == a.ID);
                Station = new(t);
                StationsPage.Content = new ActionsStation(bl, (int)id, this);

            }
        }
        internal new void Close() => base.Close();
        private void GridTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            new StationListWindow(bl).Show();
            this.Close();

        }
    }
}
