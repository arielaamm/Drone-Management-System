using BlApi;
using System;
using System.Linq;
using System.Windows;

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
            new SignInWindow(bl);
            Close();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            var c = bl.Customers();
            var list = (from i in c
                        select i.ID).ToList();
            int rand = rnd.Next();

            while (!list.TrueForAll(i => i != rand))
            {
                rand = rnd.Next();
            }
            try
            {
                while (txtPhone.Text.Length != 10 && txtPhone.Text[0] != '0' && txtPhone.Text[1] != '5')
                { MessageBox.Show("Please enter a valid personal phone number"); }
                BO.Location location = new BO.Location()
                {
                    Lattitude = double.Parse(txtLattitude.Text),
                    Longitude = double.Parse(txtLongitude.Text)
                };
                BO.Customer customer = new BO.Customer()
                {
                    ID = rand,
                    CustomerName = txtUsername.Text,
                    Position = location,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Password,
                    IsActive = true,
                };
                bl.AddCustomer(customer);
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() == "BLExceptions.AlreadyExistException")
                {
                    MessageBox.Show(ex.Message + ", enter anther ID");

                }
                else
                    MessageBox.Show("Enter the data in the necessary places");
            }
            if (c.Count() < bl.Customers().Count())
            {
                MessageBox.Show("The customer successfully added");
                new UserWindow(bl);
                Close();
            }
        }
    }
}
