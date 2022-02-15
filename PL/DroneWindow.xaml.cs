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
using System.Text.RegularExpressions;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
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

        internal ObservableCollection<IBL.BO.DroneToList> DronesList
        {
            get => (ObservableCollection<IBL.BO.DroneToList>)GetValue(dronesDependency);
            set => SetValue(dronesDependency, value);
        }
        static readonly DependencyProperty dronesDependency = DependencyProperty.Register(
            nameof(DronesList),
            typeof(ObservableCollection<IBL.BO.DroneToList>),
            typeof(Window));

        private DroneWindow(IBL.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            List<int> stationToLists = new();
            DronesList = new(this.bl.Drones());
            Main.Content = new AddPage(bl);
        }
        protected static DroneWindow instance1 = null;
        public static DroneWindow GetInstance()
        {
            IBL.IBL bl = BL.BL.GetInstance();
            if (instance1 == null)
                instance1 = new DroneWindow(bl);
            return instance1;
        }
        private DroneWindow(IBL.IBL bl,int ID)
        {
            InitializeComponent();
            this.bl = bl;
            var w = this.bl.Drones().ToList().Find(delegate (IBL.BO.DroneToList D) { return (D.ID == ID); });
            List<IBL.BO.DroneToList> a = new();
            a.Add(w);
            DronesList = new(a);
            Main.Content = new ActionsPage(bl, ID);
        }
        protected static DroneWindow instance2 = null;
        public static DroneWindow GetInstance(int ID)
        {
            IBL.IBL bl = BL.BL.GetInstance();
            if (instance2 == null)
                instance2 = new DroneWindow(bl,ID);
            return instance2;
        }
    }
}
