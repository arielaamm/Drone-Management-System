using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randon = System.Random;
namespace IDAL.DO
{
    namespace DalObject
    {

        internal class DataSource
        {
            public static int sta = 123456;

            public double GetRandomNumber(double minimum, double maximum)
            {
                Random random = new Random();
                return random.NextDouble() * (maximum - minimum) + minimum;
            }
            public static Drone[] drones = new Drone[10];
            public static Station[] station = new Station[5];
            public static Customers[] customers = new Customers[100];
            public static Parcel[] parcel = new Parcel[1000];

            internal static class Config {
                public static int DroneIndex = 0;
                public static int StationIndex = 0;
                public static int CustomersIndex = 0;
                public static int ParcelIndex = 0;
                public static int Idforparcel = 0;
            }

            public void Initialize()
            {

                Random rnd = new Random();
                for (int i = 0; i < 5; i++)
                {
                    drones[i] = new Drone()
                    {
                        ID = rnd.Next(123456789, 999999999),
                        Model = "" + (rnd.Next(0, 100)),
                        Status = (STATUS)0,
                        Buttery = 100,
                    };

                }

                Config.DroneIndex = 5;

                for (int i = 0; i < 2; i++)
                {
                    station[i] = new Station()
                    {
                        ID = rnd.Next(123456789, 999999999),
                        StationName = "Station" + i+1,
                        Longitude = GetRandomNumber(33.289273, 29.494665),
                        Lattitude = GetRandomNumber(35.569495, 34.904675),
                        ChargeSlots = 0,
                    };
                }

                Config.DroneIndex = 2;

                for (int i = 0; i < 10; i++)
                {
                    customers[i] = new Customers()
                    {
                        ID = rnd.Next(123456789, 999999999),
                        CustomerName = "Customer" + i,
                        Phone = "05" + rnd.Next(10000000, 99999999),
                        Longitude = GetRandomNumber(33.289273, 29.494665),
                        Lattitude = GetRandomNumber(35.569495, 34.904675),
                    };
                }

                Config.DroneIndex = 10;

                for (int i = 0; i < 10; i++)
                {
                    parcel[i] = new Parcel()
                    {
                        ID = rnd.Next(123456789, 999999999),
                        SenderId = customers[i].ID,
                        TargetId = customers[i].ID,
                        Weight = 0,
                        DroneId = drones[(i + 1 > 6 ? 0 : i + 1)].ID
                    };
                }

                Config.DroneIndex = 10;
                sta++;
                Config.Idforparcel = sta;
            }
        }

        /*מימושים*/

    }
}
