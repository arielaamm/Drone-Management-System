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
    /// Interaction logic for ActionsStation.xaml
    /// </summary>
    public partial class ActionsStation : Page
    {
        BlApi.IBL bl;
        int id;
        Window parent;
        public ActionsStation(BlApi.IBL bl, int id, Window parent)
        {
            InitializeComponent();
            this.bl = bl;
            this.id = id;
            this.parent = parent;
        }
        private void Button_Click(object sender, RoutedEventArgs e) => parent.Close();
    }
}
