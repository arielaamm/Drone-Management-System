using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for SendParcelWindow.xaml
    /// </summary>
    public partial class SendParcelWindow : Window

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
        public SendParcelWindow(BlApi.IBL bl, BO.Customer customer)
        {
            InitializeComponent();
            this.customer = customer;
            this.bl = bl;
            PrioritySelector.ItemsSource = Enum.GetValues(typeof(BO.Priority));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(BO.Weight));
            var name = from n in bl.Customers()
                       where n.ID != customer.ID
                       select n.CustomerName;
            TargetSelector.ItemsSource = name;
        }

        private readonly BO.Parcel parcel = new();
        private void Button_Click_Add_Parcel(object sender, RoutedEventArgs e)
        {
            int t = bl.Parcels().Count();
            try
            {
                parcel.ID = int.Parse(TextBoxID.Text);
                parcel.Weight = (BO.Weight)WeightSelector.SelectedItem;
                parcel.Priority = (BO.Priority)PrioritySelector.SelectedItem;
                BO.CustomerInParcel customerInParcelSender = new BO.CustomerInParcel();
                BO.CustomerInParcel customerInParcelTarget = new BO.CustomerInParcel();
                customerInParcelSender.ID = bl.Customers().ToList().Find(c => c.ID == customer.ID).ID;
                parcel.sender = customerInParcelSender;
                customerInParcelTarget.ID = bl.Customers().ToList().Find(c => c.CustomerName == (string)TargetSelector.SelectedItem).ID;
                parcel.target = customerInParcelTarget;
                if (parcel.sender.ID == parcel.target.ID)
                    MessageBox.Show("you can't send a parcel to your self, try agine");
                else
                {
                    bl.AddParcel(parcel);
                    bl.AttacheDroneParcelID(parcel.ID);
                    System.Threading.Thread.Sleep(100);
                    bl.PickUpParcel(parcel.ID);
                    System.Threading.Thread.Sleep(100);
                    bl.Parceldelivery(parcel.ID);
                }
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
            if (t < bl.Parcels().Count())
            {
                Close();
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new UserWindow(bl, customer);
            Close();
        }
    }
}
