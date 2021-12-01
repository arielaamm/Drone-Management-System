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
        public static void AddDrone(string name, WEIGHT Weight, double Buttery)
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
        public static void AddCustomer(string name, string phone)
        {
            Customer c = new Customer();
            c.CustomerName = (string)name;
            c.Longitude = random.NextDouble() * (33.289273 - 29.494665) + 29.494665;
            c.Lattitude = random.NextDouble() * (35.569495 - 34.904675) + 34.904675;
            c.ID = 11111111 + DataSource.staticId;
            c.Phone = (string)phone;
            DataSource.staticId++;
            DataSource.customers.Add(c);
        }
        public static void AddParcel(int SenderId, WEIGHT Weight, PRIORITY Priority, DateTime Requested)
        {
            // שים לב מה ששמתי בהערה לא יכול להתקבל מראש זה משהו שאנחנו עושים זה כל הרעיון של משלוחים....
            Parcel parcel = new Parcel();
            parcel.ID = 11111111 + DataSource.staticId;
            parcel.SenderId = SenderId;
            //parcel.TargetId = TargetId;
            parcel.Weight = Weight;
            parcel.Priority = Priority;
            //parcel.DroneId = DroneId;
            parcel.Requested = Requested;
            //parcel.Scheduled =     Scheduled;
            //parcel.PickedUp = PickedUp;
            //parcel.Deliverd = Deliverd;
            DataSource.staticId++;
            DataSource.parcels.Add(parcel);
        }
        #endregion
        #region print(3)

        public static Station FindStation(int id)
        {
            Station s = new Station();
            foreach (var i in DataSource.stations)
            {
                if (i.ID == id)
                {
                    return i;
                }
            }
            return s;
        }
        public static Drone FindDrone(int id)
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
        public static Customer FindCustomers(int id)
        {
            Customer c = new Customer();

            foreach (var i in DataSource.customers)
            {
                if (i.ID == id)
                {
                    return i;
                }
            }
            return c;
        }
        public static Parcel FindParcel(int id)
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
        #region print lists (4)
        public static List<Station> Stationlist() => DataSource.stations;
        public static List<Customer> Customerlist() => DataSource.customers;
        public static List<Parcel> Parcellist() => DataSource.parcels;
        public static List<Drone> Dronelist() => DataSource.drones;
        public static List<Parcel> Parcelnotassociatedlist()
        {
            List<Parcel> notassociated = new();
            foreach (var i in DataSource.parcels)
            {
                if (i.DroneId==0) // חבילה שלא שויכה לרחפן מוגדרת בקונפיג שה אידי של הרחפן שלה הוא 0
                {
                    notassociated.Add(i);
                }
            }
            return notassociated;
        }
        public static List<Station> Freechargeslotslist()
        {
            List<Station> Freechargeslots = new();
            foreach (var i in DataSource.stations)
            {
                if (i.ChargeSlots > 0) // חבילה שלא שויכה לרחפן מוגדרת בקונפיג שה אידי של הרחפן שלה הוא 0
                {
                    Freechargeslots.Add(i);
                }
            }
            return Freechargeslots;
        }
        public static void AttacheDrone(Parcel parcel)
        {
            foreach (var i in DataSource.drones)
            {
                if ((i.Status == 0)&&(i.Weight> parcel.Weight))//battery?
                {
                        parcel.DroneId = i.ID;
                        parcel.Scheduled = DateTime.Now;
                }
            }
        }
        public static void PickParcel(Parcel parcel)
        {
            int keeper = 0;
            foreach (var i in DataSource.drones)
            {
                
                if (i.ID == parcel.DroneId)
                {
                    parcel.PickedUp = DateTime.Now;
                    keeper = i.ID;
                }
            }
            FindDrone(keeper).Status = 1;

        }
        #endregion
    }
}
