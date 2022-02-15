using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        //#region disable Close Button
        //protected override void OnSourceInitialized(EventArgs e)
        //{
        //    base.OnSourceInitialized(e);

        //    HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;

        //    if (hwndSource != null)
        //    {
        //        hwndSource.AddHook(HwndSourceHook);
        //    }

        //}

        //private bool allowClosing = false;

        //[DllImport("user32.dll")]
        //private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        //[DllImport("user32.dll")]
        //private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        //private const uint MF_BYCOMMAND = 0x00000000;
        //private const uint MF_GRAYED = 0x00000001;

        //private const uint SC_CLOSE = 0xF060;

        //private const int WM_SHOWWINDOW = 0x00000018;
        //private const int WM_CLOSE = 0x10;

        //private IntPtr HwndSourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        //{
        //    switch (msg)
        //    {
        //        case WM_SHOWWINDOW:
        //            {
        //                IntPtr hMenu = GetSystemMenu(hwnd, false);
        //                if (hMenu != IntPtr.Zero)
        //                {
        //                    EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
        //                }
        //            }
        //            break;
        //        case WM_CLOSE:
        //            if (!allowClosing)
        //            {
        //                handled = true;
        //            }
        //            break;
        //    }
        //    return IntPtr.Zero;
        //}
        //#endregion

        private readonly IBL.IBL bl = BL.BL.GetInstance();

        internal ObservableCollection<IBL.BO.DroneToList> Drones
        {
            get => (ObservableCollection<IBL.BO.DroneToList>)GetValue(dronesDependency);
            set => SetValue(dronesDependency, value);
        }
        static readonly DependencyProperty dronesDependency = DependencyProperty.Register(
            nameof(Drones),
            typeof(ObservableCollection<IBL.BO.DroneToList>),
            typeof(Window));

        private DroneListWindow(IBL.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            WightsSeletor.ItemsSource = Enum.GetValues(typeof(IBL.BO.Weight));
            StatusSeletor.ItemsSource = Enum.GetValues(typeof(IBL.BO.Status));
            Drones = new(this.bl.Drones());
        }
        protected static DroneListWindow instance = null;
        public static DroneListWindow GetInstance()
        {
            IBL.IBL bl = BL.BL.GetInstance();
            if (instance == null)
                instance = new DroneListWindow(bl);
            return instance;
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                Drones = new(bl.Drones());
            else
            {
                Drones = new();
                foreach (var drone in bl.Drones())
                    if (drone.Weight == (IBL.BO.Weight)cb.SelectedItem)
                        Drones.Add(drone);
            }
        }

        private void StatusSeletor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                Drones = new(bl.Drones());
            else
            {
                Drones = new();
                foreach (var drone in bl.Drones())
                    if (drone.Status == (IBL.BO.Status)cb.SelectedItem)
                        Drones.Add(drone);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DroneWindow obj = PL.DroneWindow.GetInstance();
            obj.Show();
        }

        private void mousedoubleclick(object sender, MouseButtonEventArgs e)
        {
            var cb = sender as DataGrid;
            IBL.BO.DroneToList a = (IBL.BO.DroneToList)cb.SelectedValue;
            this.Close();
            DroneWindow obj = PL.DroneWindow.GetInstance(a.ID);
            obj.Show();
        }
    }
}
