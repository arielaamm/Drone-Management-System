﻿using System;
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
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        private readonly BlApi.IBL bl = BL.BL.GetInstance();

        internal ObservableCollection<BO.ParcelToList> ParcelsList
        {
            get => (ObservableCollection<BO.ParcelToList>)GetValue(parcelsDependency);
            set => SetValue(parcelsDependency, value);
        }
        static readonly DependencyProperty parcelsDependency = DependencyProperty.Register(
            nameof(ParcelsList),
            typeof(ObservableCollection<BO.DroneToList>),
            typeof(Window));
        public ParcelWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            ParcelsList = new(this.bl.Parcels());
        }
    }
}


        
      