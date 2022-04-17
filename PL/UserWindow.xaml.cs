using System.Collections.ObjectModel;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private readonly BlApi.IBL bl = BL.BL.GetInstance();
        private readonly BO.Customer customer;
        internal ObservableCollection<BO.CustomerToList> CustomersList
        {
            get => (ObservableCollection<BO.CustomerToList>)GetValue(customersDependency);
            set => SetValue(customersDependency, value);
        }

        private static readonly DependencyProperty customersDependency = DependencyProperty.Register(
            nameof(CustomersList),
            typeof(ObservableCollection<BO.CustomerToList>),
            typeof(Window));

        public UserWindow(BlApi.IBL bl, BO.Customer customer)
        {
            InitializeComponent();
            this.bl = bl;
            txtName.Text = "hello " + customer.CustomerName + "what do you want to do?";
            this.customer = customer;
        }

        private string bnt = "null";
        private void Changelocation_Click(object sender, RoutedEventArgs e)
        {
            TextBoxLattitude.Visibility = Visibility.Visible;
            TextBoxLongitude.Visibility = Visibility.Visible;
            Bnt.Visibility = Visibility.Visible;
            bnt = "Changelocation";
        }

        private void Changeemail_Click(object sender, RoutedEventArgs e)
        {
            TextBoxEmail.Visibility = Visibility.Visible;
            Bnt.Visibility = Visibility.Visible;
            bnt = "Changeemail";
        }

        private void SingOut_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow();
            Close();
        }

        private void Bnt_Click(object sender, RoutedEventArgs e)
        {
            if (bnt == "Changelocation")
            {
                customer.Position.Longitude = double.Parse(TextBoxLongitude.Text);
                customer.Position.Lattitude = double.Parse(TextBoxLattitude.Text);
                bl.UpdateCustomer(customer);
            }
            if (bnt == "Changeemail")
            {
                customer.Email = TextBoxEmail.Text;
                bl.UpdateCustomer(customer);
            }
        }

        private void SendParcel_Click(object sender, RoutedEventArgs e)
        {
            new SendParcelWindow(bl, customer);
            Close();
        }
    }
}
