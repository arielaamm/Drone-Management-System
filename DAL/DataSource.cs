using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using DAL;
namespace DAL
{

    public class DataSource
    {
        public static int sta = 123456;
        public static int staticId = 1;
        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        public static List<Drone> drones = new();
        public static List<DroneCharge> droneCharges = new();
        public static List<Station> stations = new();
        public static List<Customer> customers = new();
        public static List<Parcel> parcels = new();

        internal class Config
        {
            public static int Idforparcel = 0;
            internal static double free { get { return 5; } }//כמה בטריה לקילומטר כשהוא לא סוחב כלום
            internal static double light { get { return 7; } }
            internal static double medium { get { return 10; } }
            internal static double heavy { get { return 12; } }
            internal static int ChargePerHour { get { return 50; } } // 50 אחוז בשעה
        }

        public static void Initialize()
        {
            Random rnd = new Random();
            for (int i = 0; i < 2; i++)
            {
                Station s = new Station()
                {
                    ID = staticId,
                    StationName = "Station" +i,
                    Longitude = GetRandomNumber(33.289273, 29.494665),
                    Lattitude = GetRandomNumber(35.569495, 34.904675),
                    ChargeSlots = 5,
                };
                staticId++;
                stations.Add(s);
            }
            for (int i = 0; i < 5; i++)
            {

                Drone d = new Drone()
                {
                    ID = staticId,
                    Model = "MBL" + (rnd.Next(0, 100)),
                    Buttery = 100,
                    haveParcel = false,
                    Lattitude = stations[rnd.Next(0,stations.Count-1)].Lattitude,
                    Longitude = stations[rnd.Next(0, stations.Count - 1)].Longitude,
                };
                Station s = new Station();
                foreach (var item in stations)
                {
                    if (item.ChargeSlots != 0)
                    {
                        s = item;
                        break;
                    }
                }
                DroneCharge temp = new()
                { 
                    DroneId = staticId,
                    StationId = (int)s.ID,
                };
                s.ChargeSlots--;
                droneCharges.Add(temp);
                drones.Add(d);
                staticId++;
            }

            
            for (int i = 0; i < 10; i++)
            {
                Customer c = new Customer()
                {
                    ID = staticId,
                    CustomerName = "Customer" + i,
                    Phone = "05" + rnd.Next(10000000, 99999999),
                    Longitude = GetRandomNumber(33.289273, 29.494665),
                    Lattitude = GetRandomNumber(35.569495, 34.904675),
                };
                staticId++;
                customers.Add(c);
            }

            for (int i = 0; i < 10; i++)
            {
                int sID = rnd.Next(0, 10);
                int tID = rnd.Next(0, 10);
                while (tID==sID)
                {
                    tID = rnd.Next(0, 10);
                }

                Parcel p = new Parcel()
                {
                    ID = staticId,
                    SenderId = (int)customers[sID].ID,
                    TargetId = (int)customers[tID].ID,
                    Weight = (WEIGHT)rnd.Next(0, 3),
                    Priority = (PRIORITY)rnd.Next(1, 3),
                    Requested = DateTime.Now,
                    DroneId = drones[rnd.Next(0, drones.Count - 1)].ID,
                    Scheduled = null,
                    PickedUp = null,
                    Deliverd = null,
                };
                staticId++;
                parcels.Add(p);
            }
            sta++;
            Config.Idforparcel = sta;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
