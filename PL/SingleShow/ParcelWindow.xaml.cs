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
    public partial class ParcelWindow : Window
    {
        private readonly BlApi.IBL bl = BL.BL.GetInstance();

        internal ObservableCollection<BO.ParcelToList> Parcel
        {
            get => (ObservableCollection<BO.ParcelToList>)GetValue(parcelsDependency);
            set => SetValue(parcelsDependency, value);
        }
        static readonly DependencyProperty parcelsDependency = DependencyProperty.Register(
            nameof(Parcel),
            typeof(ObservableCollection<BO.ParcelToList>),
            typeof(Window));
        public ParcelWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            Parcel = new(this.bl.Parcels());
            parcelPage.Content = new AddParcel(bl,this);
        }
        public ParcelWindow(BlApi.IBL bl, int? id)
        {
            if (id == null)
            {
                new ParcelListWindow(bl).Show();
                this.Close();
            }
            else
            {
                InitializeComponent();

                this.bl = bl;
                var t = this.bl.Parcels().Where(a => id == a.ID);
                Parcel = new(t);
                //parcelPage.Content = new ActionsParcel(bl, (int)id, this); //הורד כיוון שאין ל parcel פעולות

            }
        }

        internal new void Close() => base.Close();
        private void GridTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            new ParcelListWindow(bl).Show();
            Close();

        }
    }
}

