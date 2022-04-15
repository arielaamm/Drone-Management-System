using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private static readonly DependencyProperty parcelsDependency = DependencyProperty.Register(
            nameof(Parcellist),
            typeof(ObservableCollection<BO.ParcelToList>),
            typeof(Window));
        public ParcelListWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            WeightsSeletor.ItemsSource = Enum.GetValues(typeof(BO.Weight));
            PrioritySeletor.ItemsSource = Enum.GetValues(typeof(BO.Priority));
            StatusSeletor.ItemsSource = Enum.GetValues(typeof(BO.StatusParcel));
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
                        where (parcel.status == (BO.StatusParcel)cb.SelectedItem)
                        select parcel;
                Parcellist = new ObservableCollection<BO.ParcelToList>(a);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reload();
            new ParcelWindow(bl).Show();
            Close();
        }
        private void mousedoubleclick(object sender, MouseButtonEventArgs e)
        {
            var cb = sender as DataGrid;
            BO.ParcelToList a = (BO.ParcelToList)cb.SelectedValue;
            try
            {
                new ParcelWindow(bl, a.ID).Show();
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Click on properties only please");
            }
        }
        private void GridTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Reload()
        {
            Parcellist = new(bl.Parcels());
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow(bl).Show();
            Close();

        }
    }
}
