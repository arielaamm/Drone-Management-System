using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DAL;
namespace DAL
{
    internal class Config
    {
        internal static double Free { get { return 5; } }//כמה בטריה לקילומטר כשהוא לא סוחב כלום
        internal static double Light { get { return 7; } }
        internal static double Medium { get { return 10; } }
        internal static double Heavy { get { return 12; } }
        internal static int ChargePerHour { get { return 50; } } // 50 אחוז בשעה
        public static int staticId = 1;
    }
    public class DataSource
    {

        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        internal static List<Drone> drones = new();
        internal static List<DroneCharge> droneCharges = new();
        internal static List<Station> stations = new();
        internal static List<Customer> customers = new();
        internal static List<Parcel> parcels = new();
        public static void Initialize()
        {
            Random rnd = new();
            for (int i = 0; i < 2; i++)
            {
                Station s = new()
                {
                    IsActive = true,
                    ID = Config.staticId,
                    StationName = "Station" +i,
                    Longitude = GetRandomNumber(5, 0),
                    Lattitude = GetRandomNumber(7, 0),
                    ChargeSlots = 5,
                };
                Config.staticId++;
                stations.Add(s);
            }
            for (int i = 0; i < 5; i++)
            {       
                int counter= rnd.Next(0, 2);
                DroneCharge temp = new()
                {
                    DroneId = Config.staticId,
                    StationId = (int)stations[counter].ID,
                };
                int index = stations.FindIndex(i => i.ID == temp.StationId);
                Station station = stations[index];
                station.BusyChargeSlots++;
                stations[index] = station;
                Drone d = new()
                {
                    IsActive = true,
                    ID = Config.staticId,
                    Model = (Model)rnd.Next(0, 3),
                    Battery = 100,
                    haveParcel = false,
                    Lattitude = stations[counter].Lattitude,
                    Longitude = stations[counter].Longitude,
                    Status = Status.MAINTENANCE
                };
                droneCharges.Add(temp);
                drones.Add(d);
                Config.staticId++;
            }

            
            for (int i = 0; i < 10; i++)
            {
                Customer c = new()
                {
                    ID = Config.staticId,
                    CustomerName = "Customer" + i,
                    Phone = "05" + rnd.Next(10000000, 99999999),
                    Longitude = GetRandomNumber(5, 0),
                    Lattitude = GetRandomNumber(7, 0),
                    IsActive = true,
                };
                Config.staticId++;
                customers.Add(c);
            }
            for (int i = 0; i < 10; i++)
            {
                int sID = rnd.Next(0, 10);
                int tID = rnd.Next(0, 10);
                while (sID == tID)
                {
                    tID = rnd.Next(0, 10);
                }
                while (tID==sID)
                {
                    tID = rnd.Next(0, 10);
                }

                Parcel p = new()
                {
                    IsActive = true,
                    ID = Config.staticId,
                    SenderId = (int)customers[sID].ID,
                    TargetId = (int)customers[tID].ID,
                    Weight = (Weight)rnd.Next(0, 3),
                    Priority = (Priority)rnd.Next(0, 3),
                    Requested = DateTime.Now,
                    Scheduled = null,
                    PickedUp = null,
                    Deliverd = null,
                    DroneId = 0,
                    Status = StatusParcel.CREAT,
                };
                int ? temp = rnd.Next(0, 2);
                if (temp == 0)
                {
                    temp = rnd.Next(0, drones.Count);
                    try
                    {
                        temp = (from d in drones
                                where d.haveParcel == false
                                select d).FirstOrDefault().ID;
                    }
                    catch { temp = null; }
                    if (null != temp)
                    {
                        p.DroneId = (int)temp;
                        int index = drones.FindIndex(i => i.ID == temp);
                        Drone drone = drones[index];
                        p.Status = StatusParcel.BELONG;
                        p.Scheduled = DateTime.Now;
                        drone.Status = Status.BELONG;
                        drone.haveParcel = true;
                        drones[index] = drone;
                    }

                }

                Config.staticId++;
                parcels.Add(p);
            }
            //for (int i = 0; i < droneCharges.Count; i++)
            //{
            //    Station s = stations.Find(delegate (Station p) { return droneCharges[i].StationId == p.ID; });
            //    s.ChargeSlots--;
            //    stations.Remove(stations.Find(delegate (Station p) { return droneCharges[i].StationId == p.ID; }));
            //    stations.Add(s);
            //}
        }
    }
}
