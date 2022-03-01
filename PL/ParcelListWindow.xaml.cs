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
    /// Interaction logic for ParcelListWindow.xaml
    /// </summary>
    public partial class ParcelListWindow : Window
    {
        private readonly BlApi.IBL bl = BL.BL.GetInstance();

        internal ObservableCollection<BO.ParcelToList> ParcelToList
        {
            get => (ObservableCollection<BO.ParcelToList>)GetValue(parcelsDependency);
            set => SetValue(parcelsDependency, value);
        }
        static readonly DependencyProperty parcelsDependency = DependencyProperty.Register(
            nameof(ParcelToList),
            typeof(ObservableCollection<BO.ParcelToList>),
            typeof(Window));
        public ParcelListWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            ParcelToList = new(this.bl.Parcels());
        }
    }
}
