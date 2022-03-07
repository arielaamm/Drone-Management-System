using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustumerListWindow.xaml
    /// </summary>
    public partial class CustumerListWindow : Window
    {
        private readonly BlApi.IBL bl = BL.BL.GetInstance();

        internal ObservableCollection<BO.CustomerToList> CustomersList
        {
            get => (ObservableCollection<BO.CustomerToList>)GetValue(customersDependency);
            set => SetValue(customersDependency, value);
        }
        static readonly DependencyProperty customersDependency = DependencyProperty.Register(
            nameof(CustomersList),
            typeof(ObservableCollection<BO.CustomerToList>),
            typeof(Window));
        public CustumerListWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            CustomersList = new(this.bl.Customers());
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new CustumerWindow(bl).Show();
        }
        private void mousedoubleclick(object sender, MouseButtonEventArgs e)
        {
            var cb = sender as DataGrid;
            BO.CustomerToList a = (BO.CustomerToList)cb.SelectedValue;
            try
            {
                new CustumerWindow(bl, a.ID).Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Click on properties only please");
            }
        }
    }
}
