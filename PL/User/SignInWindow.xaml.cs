using System;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        private readonly BlApi.IBL bl = BL.BL.GetInstance();
        public SignInWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bl.Findcustomer(int.Parse(txtID.Text)).Password == txtPassword.Password && bl.Findcustomer(int.Parse(txtID.Text)).IsActive)
                {
                    new UserWindow(bl, bl.Findcustomer(int.Parse(txtID.Text))).Show();
                    Close();
                }
                else
                    MessageBox.Show("this isn't the right password");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
