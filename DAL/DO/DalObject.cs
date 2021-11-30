﻿using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randon = System.Random;
namespace DAL
{

        public class DalObject
        {
            static Random random = new Random();
            public static void AddStation(string name, int num)
            {
                Station s = new Station();
                s.StationName = (string)name;
                s.ChargeSlots = num;
                s.ID = 11111111 + DataSource.staticId;
                s.Longitude = random.NextDouble() * (33.289273 - 29.494665) + 29.494665;
                s.Lattitude = random.NextDouble() * (35.569495 - 34.904675) + 34.904675;
                DataSource.staticId++;
                DataSource.stations.Add(s);
            }
        public static void AddDrone(string name, int num, WEIGHT Weight,double Buttery)
        {
            Drone d = new Drone();
            d.Model = (string)name;
            d.Weight = Weight;
            d.Status = 0;
            d.ID = 11111111 + DataSource.staticId;
            d.Buttery = Buttery;
            DataSource.staticId++;
            DataSource.drones.Add(d);
        }
        public static void AddCustomer(string name, string phone, int num)
        {
            Customers c = new Customers();
            c.CustomerName = (string)name;
            c.Longitude = random.NextDouble() * (33.289273 - 29.494665) + 29.494665;
            c.Lattitude = random.NextDouble() * (35.569495 - 34.904675) + 34.904675;
            c.ID = 11111111 + DataSource.staticId;
            c.Phone = (string)phone;
            DataSource.staticId++;
            DataSource.customers.Add(c);
        }
        public static void AddParcel(int ID, int SenderId, int TargetId, WEIGHT Weight, PRIORITY Priority, int DroneId, DateTime Requested, DateTime Scheduled, DateTime PickedUp, DateTime Deliverd)
        {
            Parcel parcel = new Parcel();
           
            parcel.ID = 11111111 + DataSource.staticId;
            DataSource.staticId++;
            parcel.SenderId= 11111111 + DataSource.staticId;
            DataSource.staticId++;
            parcel.TargetId = 11111111 + DataSource.staticId;
            DataSource.staticId++;
            parcel.Weight = Weight;
            parcel.Priority = Priority;
            parcel.DroneId = 11111111 + DataSource.staticId;
            DataSource.staticId++;
            parcel.Requested = Requested;
            parcel.Scheduled = Scheduled;
            parcel.PickedUp = PickedUp;
            parcel.Deliverd = Deliverd;
            DataSource.parcels.Add(parcel);
        }
        public static void printeStation(int id)
        {
            Station s = new Station();
            s.ID = id;
            foreachid==DataSource.stations{
            }
)
            s.StationName = (string)name;
            s.ChargeSlots = num;
            s.ID = 11111111 + DataSource.staticId;
            s.Longitude = random.NextDouble() * (33.289273 - 29.494665) + 29.494665;
            s.Lattitude = random.NextDouble() * (35.569495 - 34.904675) + 34.904675;
            DataSource.staticId++;
            DataSource.stations.Add(s);
        }
    }
    
}