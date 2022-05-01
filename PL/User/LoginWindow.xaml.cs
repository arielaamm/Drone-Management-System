using BlApi;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly BlApi.IBL bl = BLFactory.GetBL("BL");
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            new SignInWindow(bl).Show();
            Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+[^.]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void LetterValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^A-Za-z ]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == "00")
            {
                new MainWindow(bl).Show();
                Close();
            }
            else
            {
                Random rnd = new Random();
                var c = bl.Customers();
                BO.Customer customer = new BO.Customer();
                try
                {
                    while (txtPhone.Text.Length != 10 && txtPhone.Text[0] != '0' && txtPhone.Text[1] != '5')
                    { MessageBox.Show("Please enter a valid personal phone number", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                    BO.Location location = new BO.Location()
                    {
                        Lattitude = double.Parse(txtLattitude.Text),
                        Longitude = double.Parse(txtLongitude.Text)
                    };
                    customer.ID = int.Parse(txtID.Text);
                    customer.CustomerName = txtUsername.Text;
                    customer.Position = location;
                    customer.Phone = txtPhone.Text;
                    customer.Email = txtEmail.Text;
                    customer.Password = txtPassword.Password;
                    customer.IsActive = true;
                    bl.AddCustomer(customer);
                }
                catch (Exception ex)
                {
                    if (ex.GetType().ToString() == "BLExceptions.AlreadyExistException")
                    {
                        MessageBox.Show(ex.Message + ", enter anther ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                        MessageBox.Show("Enter the data in the necessary places", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                if (c.Count() < bl.Customers().Count())
                {
                    MessageBox.Show("The customer successfully added");
                    new UserWindow(bl, customer).Show();
                    Close();
                }
            }
        }
    }
}
