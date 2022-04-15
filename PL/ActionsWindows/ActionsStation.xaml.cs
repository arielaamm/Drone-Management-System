using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for ActionsStation.xaml
    /// </summary>
    public partial class ActionsStation : Page
    {
        private readonly BlApi.IBL bl;
        private readonly int id;
        private readonly Window parent;
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
