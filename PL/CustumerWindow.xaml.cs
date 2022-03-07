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
    /// Interaction logic for CustumerWindow.xaml
    /// </summary>
    public partial class CustumerWindow : Window
    {
        private readonly BlApi.IBL bl = BL.BL.GetInstance();

        internal ObservableCollection<BO.CustomerToList> CustomersList
        {
            get => (ObservableCollection<BO.CustomerToList>)GetValue(customersDependency);
            set => SetValue(customersDependency, value);
        }
        static readonly DependencyProperty customersDependency = DependencyProperty.Register(
            nameof(CustomersList),
            typeof(ObservableCollection<BO.CustomerToList>),
            typeof(Window));

        public CustumerWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            CustomersList = new(this.bl.Customers());
            //Main.Content = new AddPage(bl, this);
        }
        public CustumerWindow(BlApi.IBL bl, int? id)
        {
            if (id == null)
            {
                new CustumerListWindow(bl).Show();
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
                var t = this.bl.Customers().Where(a => id == a.ID);
                CustomersList = new(t);
                //Main.Content = new ActionsPage(bl, (int)id, this);
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
