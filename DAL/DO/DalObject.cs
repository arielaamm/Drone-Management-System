using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randon = System.Random;
namespace DAL
{
    namespace DalObject
    {
        //הוספתי מספר לאי די אם אתה רוצה שיטה אחרת אפשר גם
        public class DataSource
        {
            public static int sta = 123456;// מספר בשביל מזהה החבילה
            public static int staticId = 1;// מספר לאי די

            public double GetRandomNumber(double minimum, double maximum)
            {
                Random random = new Random();
                return random.NextDouble() * (maximum - minimum) + minimum;
            }
            public static Drone[] drones = new Drone[10];
            public static Station[] station = new Station[5];
            public static Customers[] customers = new Customers[100];
            public static Parcel[] parcels = new Parcel[1000];

            internal class Config {///static class or not?
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
                        ID = 11111111 + staticId,
                        Model = "" + (rnd.Next(0, 100)),
                        Buttery = 100,
                    };

                }

                Config.DroneIndex = 5;

                for (int i = 0; i < 2; i++)
                {
                    station[i] = new Station()
                    {
                        ID = 11111111 + staticId,
                        StationName = "Station" + i+1,
                        Longitude = GetRandomNumber(33.289273, 29.494665),
                        Lattitude = GetRandomNumber(35.569495, 34.904675),
                        ChargeSlots = 0,
                    };
                }

                Config.StationIndex = 2;

                for (int i = 0; i < 10; i++)
                {
                    customers[i] = new Customers()
                    {
                        ID = 11111111 + staticId,
                        CustomerName = "Customer" + i,
                        Phone = "05" + rnd.Next(10000000, 99999999),
                        Longitude = GetRandomNumber(33.289273, 29.494665),
                        Lattitude = GetRandomNumber(35.569495, 34.904675),
                    };
                }

                Config.CustomersIndex = 10;

                for (int i = 0; i < 10; i++)
                {
                    parcels[i] = new Parcel()
                    {
                        ID = 11111111 + staticId,
 /*                     SenderId = customers[i].ID,
                        TargetId = customers[i].ID,
                        DroneId = drones[(i + 1 > 6 ? 0 : i + 1)].ID    אין שום צורך*/
                    };
                }

                Config.ParcelIndex = 10;
                sta++;
                staticId+=2;
                Config.Idforparcel = sta;
            }
        }

        /*מימושים*/

    }
}
