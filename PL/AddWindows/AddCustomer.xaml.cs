using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Page
    {
        Window parent;
        private readonly BlApi.IBL bl;

        public AddCustomer(BlApi.IBL bl, Window parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.bl = bl;
        }
        private void Button_Click(object sender, RoutedEventArgs e) => parent.Close();
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void LetterValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[A-Za-z ]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click_Add_Customer(object sender, RoutedEventArgs e)
        {
            int c = bl.Customers().Count();
            try
            {
                while (TextBoxPhone.Text.Length != 10 && TextBoxPhone.Text[0] != '0' && TextBoxPhone.Text[1] != '5')
                { MessageBox.Show("Please enter a valid personal phone number"); }
                BO.Location location = new BO.Location()
                {
                    Lattitude = double.Parse(TextBoxLattitude.Text),
                    Longitude = double.Parse(TextBoxLongitude.Text)
                };
                BO.Customer customer = new BO.Customer()
                {
                    ID = int.Parse(TextBoxID.Text),
                    CustomerName = TextBoxName.Text,
                    Position = location,
                    Phone = TextBoxPhone.Text,
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
            if (c < bl.Customers().Count())
            {
                MessageBox.Show("The customer successfully added");
                
                new CustomerWindow(bl, int.Parse(TextBoxID.Text)).Show();
                parent.Close();
            }
        }
    }
}
