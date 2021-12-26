using System;
using BL.BO;
namespace ConsoleUI_BL
{
    public class LocationToInput
    {
        public double longitude { set; get; }
        public double lattitude { set; get; }
    }
    class Program
    {
        private static void FunAddition(BL.BL p)
        {
            Console.WriteLine("OK, what do you want to add ? choose");
            Console.WriteLine("add station(= 1) \nadd drone(= 2) \nadd customer(= 3) \nadd parcel(= 4)");
            string t;
            t = Console.ReadLine();
            switch (t)
            {
                case "add station" or "1":
                    Console.WriteLine("enter id station ,station name ,location ,how meny charge slots are");
                    int idStation = int.Parse(Console.ReadLine());
                    string nameStation = Console.ReadLine();
                    Location locationStation = new()
                    {
                        Longitude = int.Parse(Console.ReadLine()),
                        Lattitude = int.Parse(Console.ReadLine()),
                    };
                    int numChargeSlotsStation = int.Parse(Console.ReadLine());
                    p.AddStation(idStation, nameStation, locationStation, numChargeSlotsStation);
                    break;
                case "add drone" or "2":
                    Console.WriteLine("enter id, Model name, weight(LIGHT = 1, MEDIUM = 2, HEAVY = 3), ID of the starting station ");
                    int idDrone = int.Parse(Console.ReadLine());
                    string nameDrone = Console.ReadLine();
                    BL.BO.WEIGHT weightDrone = (BL.BO.WEIGHT)(int.Parse(Console.ReadLine()));
                    int IDStartingStation = int.Parse(Console.ReadLine());
                    p.AddDrone(idDrone, nameDrone, weightDrone, IDStartingStation);
                    break;
                case "add customer" or "3":
                    Console.WriteLine("enter id customer, customer name, customer phone number, customer location");
                    int idCustomer = int.Parse(Console.ReadLine());
                    string nameCustomer = Console.ReadLine();
                    string phoneCustomer = Console.ReadLine();
                    LocationToInput locationCustomer = new()
                    {
                        longitude = int.Parse(Console.ReadLine()),
                        lattitude = int.Parse(Console.ReadLine()),
                    };
                    //func
                    break;
                case "add parcel" or "4":
                    Console.WriteLine("enter SenderId, TargetId, weight(LIGHT = 1, MEDIUM = 2, HEAVY = 3), priority(REGULAR = 1, FAST = 2, SOS = 3 )");
                    int SenderIdParcel = int.Parse(Console.ReadLine());
                    int TargetIdParcel = int.Parse(Console.ReadLine());
                    int weightParcel = int.Parse(Console.ReadLine());
                    int priorityParcel = int.Parse(Console.ReadLine());
                    //func
                    break;
            }
        }
        private static void FunListview(BL.BL p)
        {
        }

        private static void FunDisplay(BL.BL p)
        {
        }

        private static void FunUpdating(BL.BL p)
        {
        }
        static void Main(string[] args)
        {
            BL.BL p = new BL.BL();
            string Option = "0";
            Console.WriteLine("Hey you got to ariel&babauv drone's");
            Console.WriteLine("Choose what you want to do");
            Console.WriteLine("Type what you want to do from the list below");
            do
            {
                Console.WriteLine("Addition(= 1), Updating(= 2), Display(= 3), List view(= 4) or Exit(= 5)");
                Option = Console.ReadLine();
                switch (Option)
                {
                    case "Addition" or "1":
                        FunAddition(p);
                        break;
                    case "Updating" or "2":
                        FunUpdating(p);
                        break;
                    case "Display" or "3":
                        FunDisplay(p);
                        break;
                    case "List view" or "4":
                        FunListview(p);
                        break;
                    case "Exit" or "5":
                        Console.WriteLine("Thank you have a good day");
                        break;
                    default:
                        break;
                }
            }
            while (Option != "5");
        }

        public static void Viewid(string type)
        {
            //int count = 0;
            //switch (type)
            //{
                //case "p":
                //    foreach (var i in DataSource.parcels)
                //    {
                //        count++;
                //        Console.WriteLine($"id:{count} = {i.ID}");
                //    }
                //    count = 0;
                //    break;
                //case "d":
                //    foreach (var i in DataSource.drones)
                //    {
                //        count++;
                //        Console.WriteLine($"id:{count} = {i.ID}");
                //    }
                //    count = 0;
                //    break;
                //case "c":
                //    foreach (var i in DataSource.customers)
                //    {
                //        count++;
                //        Console.WriteLine($"id:{count} = {i.ID}");
                //    }
                //    count = 0;
                //    break;
                //case "s":
                //    foreach (var i in DataSource.stations)
                //    {
                //        count++;
                //        Console.WriteLine($"id:{count} = {i.ID}");
                //    }
                //    count = 0;
                //    break;
            //}
        } // לטפל בסוף
    }
}
