using IDAL.DO;
using System;
using System.Collections.Generic;
using DAL;
namespace ConsoleUI
{
    class Program   
    {
        public static void FunAddition()
        {
            Console.WriteLine("OK, what do you want to add ? choose");
            Console.WriteLine("add station(= 1) \nadd drone(= 2) \nadd customer(= 3) \nadd parcel(= 4)");
            string t;
            t = Console.ReadLine();
            switch (t)
            {
            case "add station" or "1":
                Console.WriteLine("enter station name and how meny charge slots are");
                string name;
                name = Console.ReadLine();
                int num = Convert.ToInt32(Console.ReadLine());
                DAL.DalObject.AddStation(name,num);
                break;
            case "add drone" or "2":
                Console.WriteLine("enter Model name, weight(1,2,3), battery");
                string m = Console.ReadLine();
                int w = Convert.ToInt32(Console.ReadLine());
                double b = Convert.ToDouble(Console.ReadLine());
                DAL.DalObject.AddDrone(m, (WEIGHT)w, b);
                break;
            case "add customer" or "3":
                Console.WriteLine("enter customer name and a phone numer");
                string n = Console.ReadLine();
                string p = Console.ReadLine();
                DAL.DalObject.AddCustomer(n, p);
                break;
            case "add parcel" or "4":
                Console.WriteLine("enter SenderId, TargetId, weight(1,2,3), priority(1,2,3)");
                int s = Convert.ToInt32(Console.ReadLine());
                int tar = Convert.ToInt32(Console.ReadLine());
                int weight = Convert.ToInt32(Console.ReadLine());
                int priority = Convert.ToInt32(Console.ReadLine());
                DAL.DalObject.AddParcel(s,tar, (WEIGHT)weight, (PRIORITY)priority, DateTime.Now);
                break;
            }
        }
        public static void FunUpdating()
        {
            Console.WriteLine("OK, what do you want to update ? choose");
            Console.WriteLine("parcel to drone \npackage collection \ndelivery package \nsend for loadingor \nrelease from charging");
        }
        public static void FunDisplay()
        {
            Console.WriteLine("OK, what do you want to display ? choose");
            Console.WriteLine("Station(1), Drone(2), Customer(3), Parcel(4)");
            string t;
            t = Console.ReadLine();
            Console.WriteLine("enter id");
            int id = Convert.ToInt32(Console.ReadLine());
            switch (t)
            {
            case "Station" or "1":
                Station s = DAL.DalObject.FindStation(id);
                Console.WriteLine($"ID: {s.ID}\nStationName: {s.StationName}\nLongitude: {s.Longitude}\nLattitude: {s.Lattitude}\nChargeSlots: {s.ChargeSlots}");
                break;
            case "Drone" or "2":
                Drone d = DAL.DalObject.FindDrone(id);
                Console.WriteLine($"ID: {d.ID}\nModel: {d.Model}\nWeight: {d.Weight}\nStatus: {d.Status}\nButtery: {d.Buttery}");
                break;
            case "Customer" or "3":
                Customer c = DAL.DalObject.FindCustomers(id);
                Console.WriteLine($"ID: {c.ID}\nCustomerName: {c.CustomerName}\nPhone: {c.Phone}\nLongitude: {c.Longitude}\nLattitude: {c.Lattitude}");
                break;
            case "Parcel" or "4":
                Parcel p = DAL.DalObject.FindParcel(id);
                Console.WriteLine($"ID: {p.ID}\nSenderId: {p.SenderId}\nTargetId: {p.TargetId}\nWeight: {p.Weight}\nPriority: {p.Priority}");
                Console.WriteLine($"DroneId: {p.DroneId}\nRequested: {p.Requested}\nScheduled: {p.Scheduled}\nPickedUp: {p.PickedUp}\nDeliverd: {p.Deliverd}");
                break;
            }

        }
        public static void FunListview()
        {
            Console.WriteLine("OK, which list you what to see ? choose");
            Console.WriteLine("List Station(1), List Drone(2), List Customer(3), List Parcel(4)");
            string t;
            t = Console.ReadLine();
            int id = Convert.ToInt32(Console.ReadLine());
            switch (t)
            {
                case "List Station" or "1":
                    List<Station> st = DAL.DalObject.Stationlist();
                    foreach (var s in st)
                    {
                        Console.WriteLine($"ID: {s.ID}\nStationName: {s.StationName}\nLongitude: {s.Longitude}\nLattitude: {s.Lattitude}\nChargeSlots: {s.ChargeSlots}");
                    }
                    break;
                case "List Drone" or "2":
                    List<Drone> dr = DAL.DalObject.Dronelist();
                    foreach (var d in dr)
                    {
                        Console.WriteLine($"ID: {d.ID}\nModel: {d.Model}\nWeight: {d.Weight}\nStatus: {d.Status}\nButtery: {d.Buttery}");
                    }
                    break;
                case "List Customer" or "3":
                    List<Customer> cu = DAL.DalObject.Customerlist();
                    foreach (var c in cu)
                    {
                        Console.WriteLine($"ID: {c.ID}\nCustomerName: {c.CustomerName}\nPhone: {c.Phone}\nLongitude: {c.Longitude}\nLattitude: {c.Lattitude}");
                    }
                    break;
                case "List Parcel" or "4":
                    List<Parcel> pa = DAL.DalObject.Parcellist();
                    foreach (var p in pa)
                    {
                        Console.WriteLine($"ID: {p.ID}\nSenderId: {p.SenderId}\nTargetId: {p.TargetId}\nWeight: {p.Weight}\nPriority: {p.Priority}");
                        Console.WriteLine($"DroneId: {p.DroneId}\nRequested: {p.Requested}\nScheduled: {p.Scheduled}\nPickedUp: {p.PickedUp}\nDeliverd: {p.Deliverd}");
                    }
                    break;
            }
        }
        static void Main(string[] args)
        {
            var P = new DataSource();
            DataSource.Initialize();
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
                        FunAddition();
                        break;
                    case "Updating" or "2":
                        FunUpdating();
                        break;
                    case "Display" or "3":
                        FunDisplay();
                        break;
                    case "List view" or "4":
                        FunListview();
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
    }
}
