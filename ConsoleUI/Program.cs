using DAL;
using DalApi;
using DO;
using System;
namespace ConsoleUI
{
    internal class Program
    {
        private static readonly DalApi.IDal p = DalObject.GetInstance();

        public static void Viewid(string type)
        {
            int count = 0;
            switch (type)
            {
                case "p":
                    foreach (var i in p.Parcellist())
                    {
                        count++;
                        Console.WriteLine($"id:{count} = {i.ID}");
                    }
                    count = 0;
                    break;
                case "d":
                    foreach (var i in p.Dronelist())
                    {
                        count++;
                        Console.WriteLine($"id:{count} = {i.ID}");
                    }
                    count = 0;
                    break;
                case "c":
                    foreach (var i in p.Customerlist())
                    {
                        count++;
                        Console.WriteLine($"id:{count} = {i.ID}");
                    }
                    count = 0;
                    break;
                case "s":
                    foreach (var i in p.Stationlist())
                    {
                        count++;
                        Console.WriteLine($"id:{count} = {i.ID}");
                    }
                    count = 0;
                    break;
            }
        }
        public static void FunAddition(IDal pro)
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
                    int num = int.Parse(Console.ReadLine());
                    Station station = new() { StationName = name, IsActive = true, ChargeSlots = num };
                    pro.AddStation(station);
                    break;
                case "add drone" or "2":
                    Console.WriteLine("enter Model name(1,2,3), weight(1,2,3), battery");
                    int model = int.Parse(Console.ReadLine());
                    int weight = int.Parse(Console.ReadLine());
                    double battery = double.Parse(Console.ReadLine());
                    Drone drone = new() { IsActive = true, Model = (Model)model, Weight = (Weight)weight, Battery = battery };
                    pro.AddDrone(drone);
                    break;
                case "add customer" or "3":
                    Console.WriteLine("enter customer name and a phone numer");
                    string customername = Console.ReadLine();
                    string phone = Console.ReadLine();
                    Customer customer = new() { CustomerName = customername, Phone  = phone, IsActive = true };
                    pro.AddCustomer(customer);
                    break;
                case "add parcel" or "4":
                    Console.WriteLine("enter SenderId, TargetId, weight(1,2,3), priority(1,2,3)");
                    int senderid = int.Parse(Console.ReadLine());
                    int targetid = int.Parse(Console.ReadLine());
                    int parcelweight = int.Parse(Console.ReadLine());
                    int priority = int.Parse(Console.ReadLine());
                    Parcel parcel = new() { SenderId = senderid, TargetId = targetid, Weight =  (Weight)parcelweight, Priority =  (Priority)priority, Requested= DateTime.Now };
                    pro.AddParcel(parcel);
                    break;
            }
        }

        public static void FunUpdating(IDal pro)
        {
            Console.WriteLine("OK, what do you want to update ? choose");
            Console.WriteLine("Attache drone to parcel(1)\nPick parcel(2) \ndelivery package(3) \nsend for loadingor(4) \nrelease from charging(5)");
            string t;
            t = Console.ReadLine();
            string type;
            switch (t)
            {

                case "Attache drone to parcel" or "1":
                    Console.WriteLine("enter parcel's id");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("p");
                    }
                    Console.WriteLine("enter parcel's id new");
                    int num1 = int.Parse(Console.ReadLine());
                    pro.AttacheDrone(num1);
                    break;
                case "Pick parcel" or "2":
                    Console.WriteLine("enter parcel's id");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("p");
                    }
                    Console.WriteLine("enter parcel's id new");
                    int num2 = int.Parse(Console.ReadLine());
                    pro.PickupParcel(num2);
                    break;
                case "delivery package" or "3":
                    Console.WriteLine("enter parcel's id");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("p");
                    }
                    Console.WriteLine("enter parcel's id new");
                    int num3 = int.Parse(Console.ReadLine());
                    pro.DeliverdParcel(num3);
                    break;
                case "send for loadingor" or "4":
                    Console.WriteLine("enter droneID");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("d");
                    }
                    Console.WriteLine("enter parcel's droneid new");
                    int droneID = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter parcel's stationID new");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("p");
                    }
                    Console.WriteLine("enter parcel's stationID new");
                    int stationID = int.Parse(Console.ReadLine());
                    pro.DroneToCharge(droneID, stationID);
                    break;
                case "release from charging" or "5":
                    Console.WriteLine("enter droneID");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("d");
                    }
                    Console.WriteLine("enter droneid new");
                    int d = int.Parse(Console.ReadLine());
                    pro.DroneOutCharge(d);
                    break;
            }
        }

        public static void FunDisplay(DalApi.IDal pro)
        {
            Console.WriteLine("OK, what do you want to display ? choose");
            Console.WriteLine("Station(1), Drone(2), Customer(3), Parcel(4)");
            string t, type;
            t = Console.ReadLine();
            int id;
            switch (t)
            {
                case "Station" or "1":
                    Console.WriteLine("enter ID");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("s");
                    }
                    Console.WriteLine("enter id new");
                    id = int.Parse(Console.ReadLine());
                    Station s = pro.FindStation(id);
                    Console.WriteLine($"ID: {s.ID}\nStationName: {s.StationName}\nLongitude: {s.Longitude}\nLattitude: {s.Lattitude}\nChargeSlots: {s.ChargeSlots}");
                    break;
                case "Drone" or "2":
                    Console.WriteLine("enter ID");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("d");
                    }
                    Console.WriteLine("enter id new");
                    id = int.Parse(Console.ReadLine());
                    Drone d = pro.FindDrone(id);
                    Console.WriteLine($"ID: {d.ID}\nModel: {d.Model}\nWeight: {d.Weight}\nStatus: {d.Status}\nButtery: {d.Battery}");
                    break;
                case "Customer" or "3":
                    Console.WriteLine("enter ID");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("c");
                    }
                    Console.WriteLine("enter id new");
                    id = int.Parse(Console.ReadLine());
                    Customer c = pro.FindCustomers(id);
                    Console.WriteLine($"ID: {c.ID}\nCustomerName: {c.CustomerName}\nPhone: {c.Phone}\nLongitude: {c.Longitude}\nLattitude: {c.Lattitude}");
                    break;
                case "Parcel" or "4":
                    Console.WriteLine("enter ID");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("p");
                    }
                    Console.WriteLine("enter id new");
                    id = int.Parse(Console.ReadLine());
                    Parcel p = pro.FindParcel(id);
                    Console.WriteLine($"ID: {p.ID}\nSenderId: {p.SenderId}\nTargetId: {p.TargetId}\nWeight: {p.Weight}\nPriority: {p.Priority}");
                    Console.WriteLine($"DroneId: {p.DroneId}\nRequested: {p.Requested}\nScheduled: {p.Scheduled}\nPickedUp: {p.PickedUp}\nDeliverd: {p.Deliverd}");
                    break;
            }

        }

        public static void FunListview(IDal pro)
        {
            Console.WriteLine("OK, which list you what to see ? choose");
            Console.WriteLine("List Station(1), List Drone(2), List Customer(3), List Parcel(4), List not associated(5), List free chargeslots(6)");
            string t;
            t = Console.ReadLine();
            int id = int.Parse(Console.ReadLine());
            switch (t)
            {
                case "List Station" or "1":
                    foreach (var s in pro.Stationlist())
                    {
                        Console.WriteLine($"ID: {s.ID}\nStationName: {s.StationName}\nLongitude: {s.Longitude}\nLattitude: {s.Lattitude}\nChargeSlots: {s.ChargeSlots}\n");
                    }
                    break;
                case "List Drone" or "2":
                    foreach (var d in pro.Dronelist())
                    {
                        Console.WriteLine($"ID: {d.ID}\nModel: {d.Model}\nWeight: {d.Weight}\nStatus: {d.Status}\nButtery: {d.Battery}\n");
                    }
                    break;
                case "List Customer" or "3":
                    foreach (var c in pro.Customerlist())
                    {
                        Console.WriteLine($"ID: {c.ID}\nCustomerName: {c.CustomerName}\nPhone: {c.Phone}\nLongitude: {c.Longitude}\nLattitude: {c.Lattitude}\n");
                    }
                    break;
                case "List Parcel" or "4":
                    foreach (var p in pro.Parcellist())
                    {
                        Console.WriteLine($"ID: {p.ID}\nSenderId: {p.SenderId}\nTargetId: {p.TargetId}\nWeight: {p.Weight}\nPriority: {p.Priority}");
                        Console.WriteLine($"DroneId: {p.DroneId}\nRequested: {p.Requested}\nScheduled: {p.Scheduled}\nPickedUp: {p.PickedUp}\nDeliverd: {p.Deliverd}");
                    }
                    break;
                case "List not associated" or "5":
                    foreach (var p in pro.Parcellist())
                    {
                        Console.WriteLine($"ID: {p.ID}\nSenderId: {p.SenderId}\nTargetId: {p.TargetId}\nWeight: {p.Weight}\nPriority: {p.Priority}");
                        Console.WriteLine($"DroneId: {p.DroneId}\nRequested: {p.Requested}\nScheduled: {p.Scheduled}\nPickedUp: {p.PickedUp}\nDeliverd: {p.Deliverd}\n");
                    }
                    break;
                case "List free chargeslots" or "6":
                    foreach (var s in pro.Stationlist())
                    {
                        Console.WriteLine($"ID: {s.ID}\nStationName: {s.StationName}\nLongitude: {s.Longitude}\nLattitude: {s.Lattitude}\nChargeSlots: {s.ChargeSlots}\n");
                    }
                    break;
            }
        }

        private static void Main(string[] args)
        {
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
    }
}
