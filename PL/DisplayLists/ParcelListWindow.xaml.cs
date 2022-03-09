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

        internal ObservableCollection<BO.ParcelToList> Parcellist
        {
            get => (ObservableCollection<BO.ParcelToList>)GetValue(parcelsDependency);
            set => SetValue(parcelsDependency, value);
        }
        static readonly DependencyProperty parcelsDependency = DependencyProperty.Register(
            nameof(Parcellist),
            typeof(ObservableCollection<BO.ParcelToList>),
            typeof(Window));
        public ParcelListWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            WeightsSeletor.ItemsSource = Enum.GetValues(typeof(BO.Weight));
            PrioritySeletor.ItemsSource = Enum.GetValues(typeof(BO.Priority));
            StatusSeletor.ItemsSource = Enum.GetValues(typeof(BO.Status));
            Parcellist = new(this.bl.Parcels());
        }
        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                Parcellist = new(bl.Parcels());
            else
            {
                Parcellist = new();
                var a = from parcel in bl.Parcels()
                        where (parcel.Weight == (BO.Weight)cb.SelectedItem)
                        select parcel;
                Parcellist = new ObservableCollection<BO.ParcelToList>(a);
            }
        }
        private void PrioritySeletor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                Parcellist = new(bl.Parcels());
            else
            {
                Parcellist = new();
                var a = from parcel in bl.Parcels()
                        where (parcel.Priority == (BO.Priority)cb.SelectedItem)
                        select parcel;
                Parcellist = new ObservableCollection<BO.ParcelToList>(a);
            }
        }
        private void StatusSeletor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                Parcellist = new(bl.Parcels());
            else
            {
                Parcellist = new();
                var a = from parcel in bl.Parcels()
                        where (parcel.status == (BO.Status)cb.SelectedItem)
                        select parcel;
                Parcellist = new ObservableCollection<BO.ParcelToList>(a);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reload();
            new ParcelWindow(bl).Show();
        }
        private void mousedoubleclick(object sender, MouseButtonEventArgs e)
        {
            var cb = sender as DataGrid;
            BO.ParcelToList a = (BO.ParcelToList)cb.SelectedValue;
            try
            {
                new ParcelWindow(bl, a.ID).Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Click on properties only please");
            }
        }
        private void Reload()
        {
            Parcellist = new(bl.Parcels());
        }
    }
}
