using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for ActionsCustomer.xaml
    /// </summary>
    public partial class ActionsCustomer : Page
    {
        private readonly BlApi.IBL bl;
        private readonly int id;
        private readonly Window parent;
        public ActionsCustomer(BlApi.IBL bl, int id, Window parent)
        {
            InitializeComponent();
            this.bl = bl;
            this.parent = parent;
            this.id = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new CustomerListWindow(bl).Show();
            parent.Close();
        }

        private void UpdateBnt_Click(object sender, RoutedEventArgs e)
        {
            BO.Customer customer = bl.Findcustomer(id);
            if (TextBoxName.Text != "")
                customer.CustomerName = TextBoxName.Text;
            if (TextBoxPhone.Text != "")
                customer.Phone = TextBoxPhone.Text;
            bl.UpdateCustomer(customer);
        }
    }
}
