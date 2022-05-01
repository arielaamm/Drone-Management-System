using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

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

        private static readonly DependencyProperty parcelsDependency = DependencyProperty.Register(
            nameof(Parcel),
            typeof(ObservableCollection<BO.ParcelToList>),
            typeof(Window));
        public ParcelWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            Parcel = new(this.bl.Parcels());
            parcelPage.Content = new AddParcel(bl, this);
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

