using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ActionsCustomer.xaml
    /// </summary>
    public partial class ActionsCustomer : Page
    {
        readonly BlApi.IBL bl;
        readonly int id;
        readonly Window parent;
        public ActionsCustomer(BlApi.IBL bl, int id, Window parent)
        {
            InitializeComponent();
            this.bl = bl;
            this.parent = parent;
            this.id = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e) => parent.Close();

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
