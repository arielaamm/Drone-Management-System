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
    /// Interaction logic for ActionsParcel.xaml
    /// </summary>
    public partial class ActionsParcel : Page
    {
        BlApi.IBL bl;
        int id;
        Window parent;
        public ActionsParcel(BlApi.IBL bl, int id, Window parent)
        {
            InitializeComponent();
            this.bl = bl;
            this.id = id;
            this.parent = parent;
            var names = from n in bl.Customers()
                       where n.CustomerName != bl.Findcustomer(id).CustomerName
                       select n.CustomerName;
            targetnameSelector.ItemsSource = names;
            PrioritySeletor.ItemsSource = Enum.GetValues(typeof(BO.Priority));

        }
        private void Button_Click(object sender, RoutedEventArgs e) => parent.Close();

        private void UpdateBnt_Click(object sender, RoutedEventArgs e)
        {
            BO.Parcel parcel = bl.Findparcel(id);
            if (targetnameSelector.SelectedItem != null)
            {
                int targetID = bl.Customers().ToList().Find(i => i.CustomerName == (string)targetnameSelector.SelectedItem).ID;
                BO.CustomerInParcel customer = new BO.CustomerInParcel() { CustomerName = (string)targetnameSelector.SelectedItem, ID = targetID };
                parcel.target = customer;
            }
            if (PrioritySeletor != null)
                parcel.Priority = (BO.Priority)PrioritySeletor.SelectedItem;

        }
    }
}
