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
using System.Windows.Navigation;

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

        private readonly BlApi.IBL bl = BL.BL.GetInstance();

        internal ObservableCollection<BO.DroneToList> DronesList
        {
            get => (ObservableCollection<BO.DroneToList>)GetValue(dronesDependency);
            set => SetValue(dronesDependency, value);
        }
        static readonly DependencyProperty dronesDependency = DependencyProperty.Register(
            nameof(DronesList),
            typeof(ObservableCollection<BO.DroneToList>),
            typeof(Window));

        public DroneWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            DronesList = new(this.bl.Drones());
            Main.Content = new AddPage(bl,this);
        }
        public DroneWindow(BlApi.IBL bl, int? id)
        {
            if (id == null)
            {
                new DroneListWindow(bl).Show();
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
                var t = this.bl.Drones().Where(a => id == a.Id);
                DronesList = new(t);
                Main.Content = new ActionsPage(bl, (int)id, this);
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
