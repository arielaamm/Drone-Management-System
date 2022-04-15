using System.Collections.ObjectModel;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        private readonly BlApi.IBL bl = BL.BL.GetInstance();

        internal ObservableCollection<BO.CustomerToList> CustomersList
        {
            get => (ObservableCollection<BO.CustomerToList>)GetValue(customersDependency);
            set => SetValue(customersDependency, value);
        }

        private static readonly DependencyProperty customersDependency = DependencyProperty.Register(
            nameof(CustomersList),
            typeof(ObservableCollection<BO.CustomerToList>),
            typeof(Window));
        public SignInWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (bl.Findcustomer(int.Parse(txtID.Text)).Password == txtPassword.Password && bl.Findcustomer(int.Parse(txtID.Text)).IsActive)
                new UserWindow(bl, bl.Findcustomer(int.Parse(txtID.Text))).Show();
            else
                MessageBox.Show("this isn't the right password");
            Close();
        }
    }
}
