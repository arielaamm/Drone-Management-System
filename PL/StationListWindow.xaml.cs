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
    /// Interaction logic for StationListWindow.xaml
    /// </summary>
    public partial class StationListWindow : Window
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
        public StationListWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            StationsList = new(this.bl.Stations());
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new StationWindow(bl).Show();
        }
        private void mousedoubleclick(object sender, MouseButtonEventArgs e)
        {
            var cb = sender as DataGrid;
            BO.StationToList a = (BO.StationToList)cb.SelectedValue;
            try
            {
                new StationWindow(bl, a.ID).Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Click on properties only please");
            }
        }
    }
}
