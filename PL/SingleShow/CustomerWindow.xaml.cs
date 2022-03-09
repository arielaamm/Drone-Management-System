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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private readonly BlApi.IBL bl = BL.BL.GetInstance();

        internal ObservableCollection<BO.CustomerToList> Customer
        {
            get => (ObservableCollection<BO.CustomerToList>)GetValue(customersDependency);
            set => SetValue(customersDependency, value);
        }
        static readonly DependencyProperty customersDependency = DependencyProperty.Register(
            nameof(Customer),
            typeof(ObservableCollection<BO.CustomerToList>),
            typeof(Window));

        public CustomerWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            Customer = new(this.bl.Customers());
            customerpage.Content = new AddCustomer(bl, this);
        }
        public CustomerWindow(BlApi.IBL bl, int? id)
        {
            if (id == null)
            {
                new CustomerListWindow(bl).Show();
                this.Close();
            }
            else
            {
                InitializeComponent();
                this.bl = bl;
                var t = this.bl.Customers().Where(a => id == a.ID);
                Customer = new(t);
                customerpage.Content = new ActionsCustomer(bl, (int)id, this);
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
