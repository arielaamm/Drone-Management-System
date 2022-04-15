using System.Collections.ObjectModel;
using System.Linq;
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
            var t = from i in bl.Customers()
                    where i.ID == int.Parse(txtID.Text) && IsActive
                    select false;

            if (t.Count()==0)
            {
                new UserWindow(bl);
                Close();
            }
        }
    }
}
