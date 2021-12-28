using System;
using BL.BO;
namespace ConsoleUI_BL
{
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
                        Longitude = double.Parse(Console.ReadLine()),
                        Lattitude = double.Parse(Console.ReadLine()),
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
                    Location locationCustomer = new()
                    {
                        Longitude = double.Parse(Console.ReadLine()),
                        Lattitude = double.Parse(Console.ReadLine()),
                    };
                    p.AddCustomer(idCustomer, nameCustomer, phoneCustomer, locationCustomer);
                    break;
                case "add parcel" or "4":
                    Console.WriteLine("enter SenderId, TargetId, weight(LIGHT = 1, MEDIUM = 2, HEAVY = 3), priority(REGULAR = 1, FAST = 2, SOS = 3 )");
                    int SenderIdParcel = int.Parse(Console.ReadLine());
                    int TargetIdParcel = int.Parse(Console.ReadLine());
                    int weightParcel = int.Parse(Console.ReadLine());
                    int priorityParcel = int.Parse(Console.ReadLine());
                    p.AddParcel(SenderIdParcel, TargetIdParcel, (WEIGHT)weightParcel, (PRIORITY)priorityParcel);
                    break;
            }
        }
        private static void FunUpdating(BL.BL p)
        {
            Console.WriteLine("OK, what do you want to update ? choose");
            Console.WriteLine("Updata drone model(1)\nUpdata Station(2) \nUpdate Customer(3) \nsend for loadingor(4) \nrelease from charging(5)");
            string t;
            t = Console.ReadLine();
            string type;
            switch (t)
            {

                case "Updata drone model" or "1":
                    Console.WriteLine("enter drone's id");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("p");
                    }
                    Console.WriteLine("enter drone's id new");
                    int idUpDataDrone = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("enter the new model");
                    string newModelUpDataDrone = Console.ReadLine();
                    p.UpdateDrone(idUpDataDrone, newModelUpDataDrone);
                    break;
                case "UpdataStation" or "2":
                    Console.WriteLine("enter Station's id");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("p");
                    }
                    Console.WriteLine("enter Station's id new");
                    int idStationUpdata = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("if you want to updata the station name enter 1\nelse press ayn key");
                    int i = int.Parse(Console.ReadLine());
#nullable enable
                    string? nameStationUpData = null;
#nullable disable
                    if (i == 1)
                    {
                        nameStationUpData = Console.ReadLine();
                    }
                    Console.WriteLine("if you want to updata the station chargeslot enter 1\nelse press ayn key");
                    i = int.Parse(Console.ReadLine());
                    int? chargeSlotsStationuUpData = null;
                    if (i == 1)
                    {
                        chargeSlotsStationuUpData = int.Parse(Console.ReadLine());
                    }
                    p.UpdateStation(idStationUpdata, nameStationUpData, chargeSlotsStationuUpData);
                    break;
                case "UpdateCustomer" or "3":
                    Console.WriteLine("enter Customer's id");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("p");
                    }
                    Console.WriteLine("enter Customer's id new");
                    int idCustomerUpdata = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("if you want to updata the customer name enter 1\nelse press ayn key");
                    int e = int.Parse(Console.ReadLine());
#nullable enable
                    string? nameCustomerUpData = null;
#nullable disable
                    if (e == 1)
                    {
                        nameCustomerUpData = Console.ReadLine();
                    }
                    Console.WriteLine("if you want to updata the customer phone number enter 1\nelse press ayn key");
                    e = int.Parse(Console.ReadLine());
                    int? PhoneNumberCustomerUpData = null;
                    if (e == 1)
                    {
                        PhoneNumberCustomerUpData = int.Parse(Console.ReadLine());
                    }
                    p.UpdateStation(idCustomerUpdata, nameCustomerUpData, PhoneNumberCustomerUpData);
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
                    int droneID = Int32.Parse(Console.ReadLine());
                    p.DroneToCharge(droneID);
                    break;
                case "release from charging" or "5":
                    Console.WriteLine("enter droneID");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("d");
                    }
                    Console.WriteLine("enter droneId new");
                    int idDroneReleaseFromCharge = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("how many hour the drone has charged (in full hours)");
                    TimeSpan timeInCharge = TimeSpan.FromHours(int.Parse(Console.ReadLine()));
                    p.DroneOutCharge(idDroneReleaseFromCharge, timeInCharge);
                    break;
              }
        }
        private static void FunListview(BL.BL p)
        {
        }

        private static void FunDisplay(BL.BL p)
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

