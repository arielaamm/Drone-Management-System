using BO;
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
        public class ParcelClass
        {

            public ParcelClass(int iD, string customerName, StatusParcel status)
            {
                ID=iD;
                Name=customerName;
                Status=status;
            }
            public int? ID { set; get; }
            public string Name { set; get; }
            public BO.StatusParcel Status { set; get; }
        }
        internal ObservableCollection<BO.CustomerToList> Customer
        {
            get => (ObservableCollection<BO.CustomerToList>)GetValue(customersDependency);
            set => SetValue(customersDependency, value);
        }

        private static readonly DependencyProperty customersDependency = DependencyProperty.Register(
            nameof(Customer),
            typeof(ObservableCollection<BO.CustomerToList>),
            typeof(Window));
        internal ObservableCollection<ParcelClass> ParcelsToMe
        {
            get => (ObservableCollection<ParcelClass>)GetValue(ParcelsToMeDependency);
            set => SetValue(ParcelsToMeDependency, value);
        }

        private static readonly DependencyProperty ParcelsToMeDependency = DependencyProperty.Register(
            nameof(ParcelsToMe),
            typeof(ObservableCollection<ParcelClass>),
            typeof(Window));
        internal ObservableCollection<ParcelClass> ParcelsFromMe
        {
            get => (ObservableCollection<ParcelClass>)GetValue(ParcelsFromMeDependency);
            set => SetValue(ParcelsFromMeDependency, value);
        }

        private static readonly DependencyProperty ParcelsFromMeDependency = DependencyProperty.Register(
            nameof(ParcelsFromMe),
            typeof(ObservableCollection<ParcelClass>),
            typeof(Window));

        public CustomerWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            Customer = new(this.bl.Customers());
            customerpage.Content = new AddCustomer(bl, this);
            DataGirdParcelToMe.Visibility = Visibility.Hidden;
            DataGirdParcelFromMe.Visibility = Visibility.Hidden;


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
                ParcelsToMe = new(from p in bl.Parcels()
                                  where p.TargetName == t.FirstOrDefault().CustomerName
                                  select new ParcelClass(p.ID, p.SenderName, p.status));
                ParcelsFromMe = new(from p in bl.Parcels()
                                    where p.SenderName == t.FirstOrDefault().CustomerName
                                    select new ParcelClass(p.ID, p.TargetName, p.status));
                if (ParcelsToMe.Count != 0)
                    DataGirdParcelToMe.Visibility = Visibility.Visible;
                if (ParcelsFromMe.Count != 0)
                    DataGirdParcelFromMe.Visibility = Visibility.Visible;
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
