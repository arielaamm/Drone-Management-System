using IDAL.DO;
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
        #region add (1)
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

            parcel.ID = ID;
            parcel.SenderId = SenderId;
            parcel.TargetId = TargetId;
            parcel.Weight = Weight;
            parcel.Priority = Priority;
            parcel.DroneId = DroneId;
            parcel.Requested = Requested;
            parcel.Scheduled = Scheduled;
            parcel.PickedUp = PickedUp;
            parcel.Deliverd = Deliverd;
            DataSource.parcels.Add(parcel);
        }
#endregion
        #region print(3)
        public static Station printeStation(int id)
        {
            Station s = new Station();
            foreach (var i in DataSource.stations)
                {
                    if (i.ID==id)
                    {
                        return i;
                    }
                }
            return s;
        }
        public static Drone printeDrone(int id)
        {
            Drone d = new Drone();
            foreach (var i in DataSource.drones)
            {
                if (i.ID == id)
                {
                    return i;
                }
            }
            return d;
        }
        public static Customers printeCustomers(int id)
        {
            Customers c = new Customers();
            foreach (var i in DataSource.customers)
            {
                if (i.ID == id)
                {
                    return i;
                }
            }
            return c;
        }
        public static Parcel printeParcel(int id)
        {
            Parcel p = new Parcel();
            foreach (var i in DataSource.parcels)
            {
                if (i.ID == id)
                {
                    return i;
                }
            }
            return p;
        }
        #endregion 
    }
}