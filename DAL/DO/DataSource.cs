using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace DAL
{
    //דברחם ששונו
    /* טיפלתי בהרבה בלגן שהיה פה סטטים וכל הקטע של מעבר ממערך לרשימה 
    יצרתי פונקציה ראשונה של הוספת בסיס כולל איתחול כלומר שאם אני מוסיף בסיס יהיה 3  בסיסים
    יש בעיה קטנה עם האי די זה כללי כלומר הוא רץ עד 5 ברחפנים ואז ממשיך ל 6 בחבילות וזה אומר שזה טיפה מבולגן
    אבל לא נורא לא להתעקב על זה זה כלום ולא חשוב !!!!
    */
    public class DataSource
    {
        public static int sta = 123456;// מספר בשביל מזהה החבילה
        public static int staticId = 1;// מספר לאי די

        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        public static List <Drone> drones = new();
        public static List <Station> stations = new();
        public static List <Customer> customers = new();
        public static List <Parcel> parcels = new();

        internal class Config
        {///static class or not?
            public static int DroneIndex = 0;
            public static int StationIndex = 0;
            public static int CustomersIndex = 0;
            public static int ParcelIndex = 0;
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

            Config.DroneIndex = 5;
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
            Config.StationIndex = 2;
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

            Config.CustomersIndex = 10;
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
            //Console.WriteLine("all good");  (בדיקה שהאיתחול עובר חלק (הוא עובר חלק
            Config.ParcelIndex = 10;
            sta++;
            Config.Idforparcel = sta;
        }
    }
}
