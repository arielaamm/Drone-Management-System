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
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private readonly IBL.IBL bl;
        internal ObservableCollection<IBL.BO.DroneToList> DronesList
        {
            get => (ObservableCollection<IBL.BO.DroneToList>)GetValue(dronesDependency);
            set => SetValue(dronesDependency, value);
        }
        static readonly DependencyProperty dronesDependency = DependencyProperty.Register(
            nameof(DronesList),
            typeof(ObservableCollection<IBL.BO.DroneToList>),
            typeof(Window));

        public DroneWindow(IBL.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            ModelSeletor.ItemsSource = Enum.GetValues(typeof(IBL.BO.Model));
            MaxWeightSeletor.ItemsSource = Enum.GetValues(typeof(IBL.BO.Weight));
            DronesList = new(this.bl.Drones());
        }
    }
}
