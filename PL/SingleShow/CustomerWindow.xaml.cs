using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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

        private static readonly DependencyProperty customersDependency = DependencyProperty.Register(
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
                Close();
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

        internal new void Close() => base.Close();
        private void GridTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            new CustomerListWindow(bl).Show();
            Close();

        }
    }
}
