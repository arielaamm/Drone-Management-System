using BLExceptions;
using System;
using System.ComponentModel;
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
        public BackgroundWorker DroneWorker = new BackgroundWorker();
        public bool run = false;

        private readonly BlApi.IBL bl = BL.BL.GetInstance();
        private readonly BO.Customer customer;
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
                    MessageBox.Show("you can't send a parcel to your self, try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {

                    if (!(from d in bl.Drones()
                          where bl.FindDrone(d.ID).HaveParcel == false
                          select d).Any())
                        MessageBox.Show("Where is on free drone please try again letter", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                    {
                        bl.AddParcel(parcel);
                        MessageBox.Show("The new package is going to be shipped with the first free drone");
                        Start();
                    }
                }
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
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new UserWindow(bl, customer).Show();
            Close();
        }

        public void Start()
        {
            DroneWorker.DoWork += Worker_DoWork;
            DroneWorker.RunWorkerAsync();
            DroneWorker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            DroneWorker.WorkerReportsProgress = true;
            DroneWorker.WorkerSupportsCancellation = true;
            run = true;
            void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                if (e.Cancelled == true)
                {
                    MessageBox.Show("your parcel arrived to her target");
                }
                else if (e.Error != null)
                {
                    MessageBox.Show("problem, he keep running");
                }
            }
            bool GetRun() => run;
            void Worker_DoWork(object sender, DoWorkEventArgs e)
            {
                foreach (var item in bl.Drones())
                {
                    Action display = foo;
                    try
                    {
                        bl.Uploader(item.ID, display, GetRun);
                    }
                    catch (DontHaveEnoughPowerException ex)
                    {
                        run = false;
                        MessageBox.Show(ex.Message.ToString());
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message.ToString()); }
                }

            }
            void foo() { }
        }
    }
}


