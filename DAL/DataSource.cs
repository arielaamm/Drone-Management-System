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
        }

        public static void Initialize()
        {
            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {

                Drone d = new Drone()
                {
                    ID = 11111111 + staticId,
                    Model = "" + (rnd.Next(0, 100)),
                    Buttery = 100,
                };
                drones.Add(d);
                staticId++;
            }

            for (int i = 0; i < 2; i++)
            {
                Station s = new Station()
                {
                    ID = 11111111 + staticId,
                    StationName = "Station" + i + 1,
                    Longitude = GetRandomNumber(33.289273, 29.494665),
                    Lattitude = GetRandomNumber(35.569495, 34.904675),
                    ChargeSlots = 0,
                };
                staticId++;
                stations.Add(s);
            }
            for (int i = 0; i < 10; i++)
            {
                Customer c = new Customer()
                {
                    ID = 11111111 + staticId,
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
                Parcel p = new Parcel()
                {
                    ID = 11111111 + staticId,
                    Requested = DateTime.Now,
                    DroneId = 0,
                };
                staticId++;
                parcels.Add(p);
            }
            Console.WriteLine("all good");
            sta++;
            Config.Idforparcel = sta;
        }
    }
}
