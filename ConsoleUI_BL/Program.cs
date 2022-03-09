using System;
using System.Runtime.Serialization;
using DAL;
using BLExceptions;
using BO;
using BlApi;

namespace ConsoleUI_BL
{
    class Program
    {
        /// </summary>
        /// <param int="id"></param>
        /// <param string="type"></param>
        /// <returns>dont find => -1, find => the index</returns>
        public static int ChackID(int id, string type)
        {
            //switch (type)
            //{
            //    case "s":
            //        return DataSource.stations.FindIndex(i => i.ID == id);
            //    case "d":
            //        return DataSource.drones.FindIndex(i => i.ID == id);
            //    case "c":
            //        return DataSource.customers.FindIndex(i => i.ID == id);
            //}
            return 0;
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param BL="p"></param>
        public static void FunAddition(BL.BL p)
        {
            Console.WriteLine("OK, what do you want to add ? choose");
            Console.WriteLine("add station => 1\nadd drone => 2\nadd customer => 3\nadd parcel => 4");
            string t;
            t = Console.ReadLine();
            string type;
            switch (t)
            {
                #region Add station
                case "Add station" or "1":
                    Console.WriteLine("enter id station ,station name ,location ,how meny charge slots are");
                    int idStation = int.Parse(Console.ReadLine());
                    if (ChackID(idStation, "s") != -1)
                    {
                        throw new AlreadyExistException($"this id {idStation} already exist");
                    }
                    string nameStation = Console.ReadLine();
                    Location locationStation = new()
                    {
                        Longitude = double.Parse(Console.ReadLine()),
                        Lattitude = double.Parse(Console.ReadLine()),
                    };

                    int numChargeSlotsStation = int.Parse(Console.ReadLine());
                    Station station = new Station { ID = idStation, StationName = nameStation, ChargeSlots = numChargeSlotsStation, Position = locationStation };
                    p.AddStation(station);
                    break;
                #endregion
                #region Add drone
                case "Add drone" or "2":
                    Console.WriteLine("enter id, Model name, weight(LIGHT = 1, MEDIUM = 2, HEAVY = 3), ID of the starting station ");
                    int idDrone = int.Parse(Console.ReadLine());
                    if (ChackID(idDrone, "d") != -1)
                    {
                        throw new AlreadyExistException($"this id {idDrone} already exist");
                    }
                    string nameDrone = Console.ReadLine();
                    Weight weightDrone = (Weight)int.Parse(Console.ReadLine());
                    if (((int)weightDrone < 1) || ((int)weightDrone > 3))
                        throw new PutTheRightNumberException($"this weight {weightDrone} is not in the right form");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("s");
                        Console.WriteLine("enter Station's id new");
                    }
                    int IDStartingStation = int.Parse(Console.ReadLine());
                    Drone drone = new Drone { ID = idDrone, Model = nameDrone, Weight = weightDrone };
                    p.AddDrone(drone, IDStartingStation);
                    break;
                #endregion
                #region Add customer
                case "add customer" or "3":
                    Console.WriteLine("enter id customer, customer name, customer phone number, customer location");
                    int idCustomer = int.Parse(Console.ReadLine());
                    if (ChackID(idCustomer, "c") != -1)
                    {
                        throw new AlreadyExistException($"this id {idCustomer} already exist");
                    }
                    string nameCustomer = Console.ReadLine();
                    string phoneCustomer = Console.ReadLine();
                    if (!((phoneCustomer.Length == 10) && (phoneCustomer[0] == 48) && (phoneCustomer[1] == 53)))
                        throw new NotTheRightFormException($"this phone number {phoneCustomer} is not in the right form");
                    Location locationCustomer = new()
                    {
                        Longitude = double.Parse(Console.ReadLine()),
                        Lattitude = double.Parse(Console.ReadLine()),
                    };
                    Customer customer = new Customer { ID = idCustomer, CustomerName = nameCustomer, Phone = phoneCustomer, Position = locationCustomer };
                    p.AddCustomer(customer);
                    break;
                #endregion
                #region Add parcel
                case "add parcel" or "4":
                    Console.WriteLine("enter SenderId, TargetId, weight(LIGHT = 1, MEDIUM = 2, HEAVY = 3), priority(REGULAR = 1, FAST = 2, SOS = 3 )");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("c");
                        Console.WriteLine("enter customer's id new");
                    }
                    CustomerInParcel SenderIdParcel=new();
                    CustomerInParcel TargetIdParcel = new();
                    SenderIdParcel.ID= int.Parse(Console.ReadLine());
                    TargetIdParcel.ID = int.Parse(Console.ReadLine());
                    Weight weightParcel = (Weight)(int.Parse(Console.ReadLine()));

                    if (((int)weightParcel < 1) || ((int)weightParcel > 3))
                        throw new PutTheRightNumberException($"this weight {weightParcel} is not in the right form");
                    int temp = int.Parse(Console.ReadLine());
                    if ((temp < 1) || (temp > 3))
                        throw new PutTheRightNumberException($"this priority {temp} is not in the right form");
                    Priority priorityParcel = (Priority)temp;
                    Parcel parcel = new Parcel { sender = SenderIdParcel, target = TargetIdParcel, Weight = weightParcel, Priority = priorityParcel };
                    p.AddParcel(parcel);
                    break;
                    #endregion
            }
        }
        /// <summary>
        /// up date
        /// </summary>
        /// <param BL="p"></param>
        public static void FunUpdating(BL.BL p)
        {
            Console.WriteLine("OK, what do you want to update ? choose");
            Console.WriteLine("Updata drone model => 1\nUpdata Station => 2 \nUpdate Customer => 3\nSend for loadingor => 4\nRelease from charging => 5");
            Console.WriteLine("Assign a parcel to a drone => 6\nCollection of a parcel by drone = > 7\nDelivery of a parcel by drone => 8");
            string t;
            t = Console.ReadLine();
            string type;
            switch (t)
            {
                #region Updata drone model
                case "Updata drone model" or "1":
                    Console.WriteLine("enter drone's id");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("d");
                        Console.WriteLine("enter drone's id new");
                    }
                    int idUpDataDrone = Int32.Parse(Console.ReadLine());
                    if (ChackID(idUpDataDrone, "d") == -1)
                    {
                        throw new DoesNotExistException($"this id {idUpDataDrone} dont exist");

                    }
                    Console.WriteLine("enter the new model");
                    string newModelUpDataDrone = Console.ReadLine();
                    Drone d = new();
                    d = p.FindDrone(idUpDataDrone);
                    d.Model = newModelUpDataDrone;
                    p.UpdateDrone(d);
                    break;
                #endregion
                #region Updata Station
                case "UpdataStation" or "2":
                    Console.WriteLine("enter Station's id");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("s");
                        Console.WriteLine("enter Station's id new");

                    }
                    int idStationUpdata = Int32.Parse(Console.ReadLine());
                    if (ChackID(idStationUpdata, "d") != -1)
                    {
                        throw new DoesNotExistException($"this id {idStationUpdata} dont exist");

                    }
                    Console.WriteLine("if you want to updata the station name enter 1\nelse press any key");
                    int i = int.Parse(Console.ReadLine());
#nullable enable
                    string? nameStationUpData = null;
#nullable disable
                    if (i == 1)
                    {
                        nameStationUpData = Console.ReadLine();
                    }
                    Console.WriteLine("if you want to updata the station chargeslot enter 1\nelse press any key");
                    i = int.Parse(Console.ReadLine());
                    int chargeSlotsStationuUpData = 0;
                    if (i == 1)
                    {
                        chargeSlotsStationuUpData = int.Parse(Console.ReadLine());
                    }
                    Station station = new();
                    station = p.FindStation(idStationUpdata);
                    station.StationName = nameStationUpData;
                    station.ChargeSlots = chargeSlotsStationuUpData;
                    p.UpdateStation(station);
                    break;
                #endregion
                #region Update Customer
                case "UpdateCustomer" or "3":

                    Console.WriteLine("enter Customer's id");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("c");
                        Console.WriteLine("enter Customer's id new");

                    }
                    int idCustomerUpdata = Int32.Parse(Console.ReadLine());
                    Customer customer = new();
                    customer = p.Findcustomer(idCustomerUpdata);
                    if (ChackID(idCustomerUpdata, "d") == -1)
                    {
                        throw new DoesNotExistException($"this id {idCustomerUpdata} dont exist");

                    }
                    Console.WriteLine("if you want to updata the customer name enter 1\nelse press ayn key");
                    int e = int.Parse(Console.ReadLine());
#nullable enable

                    if (e == 1)
                    {                    
                        string? nameCustomerUpData = null;
                        nameCustomerUpData = Console.ReadLine();
                        customer.CustomerName = nameCustomerUpData;
                    }
                    #nullable disable
                    Console.WriteLine("if you want to updata the customer phone number enter 1\nelse press ayn key");
                    e = int.Parse(Console.ReadLine());
                    if (e == 1)
                    {           
                        string PhoneNumberCustomerUpData = "null";
                        PhoneNumberCustomerUpData = Console.ReadLine();         
                        customer.Phone = PhoneNumberCustomerUpData;

                    }
                    p.UpdateCustomer(customer);
                    break;
                #endregion
                # region Send for loadingor
                case "send for loadingor" or "4":
                    Console.WriteLine("enter droneID");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {

                        Viewid("d");
                        Console.WriteLine("enter parcel's droneid new");
                    }
                    int droneID = Int32.Parse(Console.ReadLine());
                    if (ChackID(droneID, "d") == -1)
                    {
                        throw new DoesNotExistException($"this id {droneID} dont exist");

                    }
                    p.DroneToCharge(droneID);
                    break;
                #endregion
                //#region Release from charging
                //case "release from charging" or "5":
                //    Console.WriteLine("enter droneID");
                //    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                //    type = Console.ReadLine();
                //    if (type == "1")
                //    {
                //        Viewid("d");
                //        Console.WriteLine("enter droneId new");

                //    }
                //    int idDroneReleaseFromCharge = Int32.Parse(Console.ReadLine());
                //    if (ChackID(idDroneReleaseFromCharge, "d") == -1)
                //    {
                //        throw new DoesNotExistException($"this id {idDroneReleaseFromCharge} dont exist");

                //    }
                //    Console.WriteLine("how many hour the drone has charged (in full hours)");
                //    int timeInCharge = int.Parse(Console.ReadLine());
                //    p.DroneOutCharge(idDroneReleaseFromCharge, timeInCharge);
                //    break;
                //#endregion
                //# region Assign a parcel to a drone
                //case "Attache drone to parcel" or "6":
                //    Console.WriteLine("enter drone's id");
                //    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                //    type = Console.ReadLine();
                //    if (type == "1")
                //    {
                //        Viewid("p");
                //        Console.WriteLine("enter drone's id new");

                //    }
                //    int idDroneAttache = Int32.Parse(Console.ReadLine());
                //    if (ChackID(idDroneAttache, "d") == -1)
                //        throw new DoesNotExistException($"this id {idDroneAttache} dont exist");
                //    if (p.FindDrone(idDroneAttache).HasParcel)
                //        throw new DroneAloreadyAttached($"this drone is already attached");
                //    p.AttacheDrone(idDroneAttache);
                //    break;
                //#endregion
                //#region Collection of a parcel by drone
                //case "PickUp parcel" or "7":
                //    Console.WriteLine("enter drone's id");
                //    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                //    type = Console.ReadLine();
                //    if (type == "1")
                //    {
                //        Viewid("p");
                //        Console.WriteLine("enter drone's id new");

                //    }
                //    int idDronePickUp = Int32.Parse(Console.ReadLine());
                //    if (ChackID(idDronePickUp, "d") == -1)
                //    {
                //        throw new DoesNotExistException($"this id {idDronePickUp} dont exist");

                //    }
                //    p.PickUpParcel(idDronePickUp);
                //    break;
                //#endregion
                //#region Delivery of a parcel by drone
                //case "delivery parcel" or "8":
                //    Console.WriteLine("enter drone's id");
                //    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                //    type = Console.ReadLine();
                //    if (type == "1")
                //    {
                //        Viewid("p");
                //        Console.WriteLine("enter drone's id new");

                //    }
                //    int idDroneDelivery = Int32.Parse(Console.ReadLine());
                //    if (ChackID(idDroneDelivery, "d") == -1)
                //    {
                //        throw new DoesNotExistException($"this id {idDroneDelivery} dont exist");

                //    }
                //    p.Parceldelivery(idDroneDelivery);
                //    break;
                //    #endregion
            }
        }
        /// <summary>
        /// Display one obj
        /// </summary>
        /// <param BL="p"></param>
        public static void FunDisplay(BL.BL p)
        {
            Console.WriteLine("OK, what do you want to see ? choose");
            Console.WriteLine("Station => 1\nDrone => 2\nCustomer => 3\nParcel => 4");
            string t;
            t = Console.ReadLine();
            switch (t)
            {
                //        #region Station
                //        case "Station" or "1":
                //            Console.WriteLine("enter id station");
                //            int idStation = int.Parse(Console.ReadLine());
                //            if (ChackID(idStation, "s") == -1)
                //            {
                //                throw new DoesNotExistException($"this id {idStation} dont exist");
                //            }
                //            Station s = p.FindStation(idStation);
                //            Console.WriteLine(
                //                $"ID: {s.ID}.\n" +
                //                $"StationName: {s.StationName}.\n" +
                //                $"Location: {s.location.Lattitude},{s.location.Longitude}.\n" +
                //                $"FreeChargeSlots: {s.FreeChargeSlots}.\n" +
                //                $"Drone Charging In Station:");
                //            if (s.DroneChargingInStation.Count != 0)
                //            {
                //                foreach (var item in s.DroneChargingInStation)
                //                {
                //                    Console.WriteLine(
                //                        $"\t-ID: {item.ID}.\n" +
                //                        $"\t-Buttery: {item.Buttery}.");
                //                }
                //            }
                //            else
                //                Console.WriteLine("\t-None!");
                //            break;
                //        #endregion
                //        #region Drone
                case "Drone" or "2":
                    Console.WriteLine("enter id");
                    int idDrone = int.Parse(Console.ReadLine());
                    if (ChackID(idDrone, "d") == -1)
                    {
                        throw new DoesNotExistException($"this id {idDrone} dont exist");
                    }
                    Drone d = p.FindDrone(idDrone);
                    Console.WriteLine(
                        $"ID: {d.ID}.\n" +
                        $"Model: {d.Model}.\n" +
                        $"Weight: {d.Weight}.\n" +
                        $"Status: {d.Status}.\n" +
                        $"Buttery: {d.Battery}.\n" +
                        $"Location: {d.Position.Lattitude},{d.Position.Longitude}.\n" +
                        "Parcel In Transactining");
                    Console.WriteLine(
                        $"\tID: {d.Parcel.ID}.\n" +
                        $"\tParcel Status: {d.Parcel.ParcelStatus}.\n" +
                        $"\tpriority: {d.Parcel.priority}.\n" +
                        $"\tweight: {d.Parcel.weight}.\n" +
                        $"\tThe sender fo parcel:" +
                        $"\t\tSender ID: {d.Parcel.sender.ID}.\n" +
                        $"\t\tSender name: {d.Parcel.sender.CustomerName}.\n" +
                        $"\t\tSender Location: {d.Parcel.LocationOfSender.Lattitude}," +
                        $"{d.Parcel.LocationOfSender.Longitude}\n" +
                        $"\tThe receiver fo parcel:" +
                        $"\t\tReceiver ID: {d.Parcel.target.ID}.\n" +
                        $"\t\tReceiver name: {d.Parcel.target.CustomerName}.\n" +
                        $"\t\tReceiver Location: {d.Parcel.LocationOftarget.Lattitude}," +
                        $"{d.Parcel.LocationOftarget.Longitude}\n" +
                        $"distance: {d.Parcel.distance}.\n"
                        );
                    break;
                    //        #endregion
                    //        #region Customer
                    //        case "Customer" or "3":
                    //            Console.WriteLine("enter id");
                    //            int idCustomer = int.Parse(Console.ReadLine());
                    //            if (ChackID(idCustomer, "c") != 1)
                    //            {
                    //                throw new DoesNotExistException($"this id {idCustomer} dont exist");
                    //            }
                    //            Customer c = p.Findcustomer(idCustomer);
                    //            Console.WriteLine(
                    //                $"ID: {c.ID}.\n" +
                    //                $"Customer Name: {c.CustomerName}.\n" +
                    //                $"Phone: {c.Phone}.\n" +
                    //                $"Location: {c.location.Lattitude},{c.location.Longitude}\n" +
                    //                $"Parcel from {c.CustomerName}");
                    //            foreach (var item in c.fromCustomer)
                    //            {
                    //                Console.WriteLine(
                    //                $"\tID: {item.ID}.\n" +
                    //                $"\tweight: {item.Weight}.\n" +
                    //                $"\tpriority: {item.Priority}.\n" +
                    //                $"\tStatus: {item.Status}.\n" +
                    //                $"\tThe sender fo parcel:" +
                    //                $"\t\tSender ID: {item.Sender.ID}.\n" +
                    //                $"\t\tSender name: {item.Sender.CustomerName}.\n" +
                    //                $"\tThe receiver fo parcel:" +
                    //                $"\t\tReceiver ID: {item.Target.ID}.\n" +
                    //                $"\t\tReceiver name: {item.Target.CustomerName}.");
                    //            }
                    //            Console.WriteLine($"Parcel to {c.CustomerName}");
                    //            foreach (var item in c.toCustomer)
                    //            {
                    //                Console.WriteLine(
                    //                    $"\tID: {item.ID}.\n" +
                    //                    $"\tweight: {item.Weight}.\n" +
                    //                    $"\tpriority: {item.Priority}.\n" +
                    //                    $"\tStatus: {item.Status}.\n" +
                    //                    $"\tThe sender fo parcel:" +
                    //                    $"\t\tSender ID: {item.Sender.ID}.\n" +
                    //                    $"\t\tSender name: {item.Sender.CustomerName}.\n" +
                    //                    $"\tThe receiver fo parcel:" +
                    //                    $"\t\tReceiver ID: {item.Target.ID}.\n" +
                    //                    $"\t\tReceiver name: {item.Target.CustomerName}.");
                    //            };
                    //            break;
                    //        #endregion
                    //        #region Parcel
                    //        case "Parcel" or "4":
                    //            Console.WriteLine("enter Id");
                    //            int idParcel = int.Parse(Console.ReadLine());
                    //            if (ChackID(idParcel, "p") != 1)
                    //            {
                    //                throw new DoesNotExistException($"this id {idParcel} dont exist");
                    //            }
                    //            Parcel parcel = p.Findparcel(idParcel);
                    //            Console.WriteLine(
                    //                $"ID: {parcel.ID}.\n" +
                    //                "Sender:\n" +
                    //                $"\tSender ID: {parcel.sender.ID}\n" +
                    //                $"\tSende Name: {parcel.sender.CustomerName}.\n" +
                    //                "Receiver:\n" +
                    //                $"\tReceiver ID: {parcel.target.ID}.\n" +
                    //                $"\tReceiver name: {parcel.target.CustomerName}." +
                    //                $"weight: {parcel.Weight}.\n" +
                    //                $"priority: {parcel.Priority}.\n" +
                    //                "The Drone of the Parcel:\n" +
                    //                $"\tID: {parcel.Drone.ID}.\n" +
                    //                $"\tButtery: {parcel.Drone.Buttery}.\n" +
                    //                $"\tLocation: {parcel.Drone.Position.Lattitude}," +
                    //                $"{parcel.Drone.Position.Longitude}\n" +
                    //                $"Requested: {parcel.Requested}.\n" +
                    //                $"Scheduled: {parcel.Scheduled}.\n" +
                    //                $"PickedUp: {parcel.PickedUp}.\n" +
                    //                $"Deliverd: {parcel.Deliverd}.\n");
                    //            break;
                    //            #endregion
            }
        }
        /// <summary>
        /// view full list
        /// </summary>
        /// <param BL="p"></param>
        //public static void FunListview(BL.BL p)
        //{
        //    Console.WriteLine("OK, what do you want to see ? choose");
        //    Console.WriteLine("Station list => 1\nDrone list => 2\nCustomer list => 3\nParcel list => 4\nParcel not associated => 5\nFree chargeslots => 6");
        //    string t;
        //    t = Console.ReadLine();
        //    switch (t)
        //    {
        //        #region Stations
        //        case "Stations" or "1":
        //            foreach (var item in p.Stations())
        //            {
        //                Console.WriteLine(
        //                $"ID: {item.ID}.\n" +
        //                $"StationName: {item.StationName}.\n" +
        //                $"Location: {item.location.Lattitude},{item.location.Longitude}.\n" +
        //                $"FreeChargeSlots: {item.FreeChargeSlots}.\n" +
        //                $"Drone Charging In Station:");
        //                if (item.DroneChargingInStation.Count != 0)
        //                {
        //                    foreach (var item1 in item.DroneChargingInStation)
        //                    {
        //                        Console.WriteLine(
        //                            $"\t-ID: {item1.ID}.\n" +
        //                            $"\t-Buttery: {item1.Buttery}.");
        //                    }
        //                }
        //                else
        //                    Console.WriteLine("\t-None!");
        //            }
        //            break;
        //        #endregion
        //        #region Drones
        //        case "Drones" or "2":
        //            foreach (var item in p.Drones())
        //            {
        //                Console.WriteLine(
        //                    $"ID: {item.ID}.\n" +
        //                    $"Model: {item.Model}.\n" +
        //                    $"Weight: {item.Weight}.\n" +
        //                    $"Status: {item.Status}.\n" +
        //                    $"Buttery: {item.Battery}.\n" +
        //                    $"Location: {item.Position.Lattitude},{item.Position.Longitude}.\n");
        //                try
        //                {
        //                    Console.WriteLine(
        //                        "Parcel In Transactining" +
        //                        $"\tID: {item.Parcel.ID}.\n" +
        //                        $"\tParcel Status: {item.Parcel.ParcelStatus}.\n" +
        //                        $"\tpriority: {item.Parcel.priority}.\n" +
        //                        $"\tweight: {item.Parcel.weight}.\n" +
        //                        $"\tThe sender fo parcel:" +
        //                        $"\t\tSender ID: {item.Parcel.sender.ID}.\n" +
        //                        $"\t\tSender name: {item.Parcel.sender.CustomerName}.\n" +
        //                        $"\t\tSender Location: {item.Parcel.Lsender.Lattitude}," +
        //                        $"{item.Parcel.Lsender.Longitude}\n" +
        //                        $"\tThe receiver fo parcel:" +
        //                        $"\t\tReceiver ID: {item.Parcel.target.ID}.\n" +
        //                        $"\t\tReceiver name: {item.Parcel.target.CustomerName}.\n" +
        //                        $"\t\tReceiver Location: {item.Parcel.Ltarget.Lattitude}," +
        //                        $"{item.Parcel.Ltarget.Longitude}\n" +
        //                        $"distance: {item.Parcel.distance}.\n"
        //                        );
        //                }
        //                catch (Exception)
        //                {
        //                    continue;
        //                }
        //            }
        //            break;
        //        #endregion
        //        #region Customers
        //        case "Customers" or "3":
        //            foreach (var item in p.Customers())
        //            {
        //                Console.WriteLine(
        //                    $"ID: {item.ID}.\n" +
        //                    $"Customer Name: {item.CustomerName}.\n" +
        //                    $"Phone: {item.Phone}.\n" +
        //                    $"Location: {item.location.Lattitude},{item.location.Longitude}\n" +
        //                    $"Parcel from {item.CustomerName}");
        //                foreach (var item1 in item.fromCustomer)
        //                {
        //                    try
        //                    {
        //                        Console.WriteLine(
        //                        $"\tID: {item1.ID}.\n" +
        //                        $"\tweight: {item1.weight}.\n" +
        //                        $"\tpriority: {item1.priority}.\n" +
        //                        $"\tStatus: {item1.status}.\n" +
        //                        $"\tThe sender fo parcel:\n" +
        //                        $"\t\tSender ID: {item1.sender.ID}.\n" +
        //                        $"\t\tSender name: {item1.sender.CustomerName}.\n" +
        //                        $"\tThe receiver fo parcel:\n" +
        //                        $"\t\tReceiver ID: {item1.target.ID}.\n" +
        //                        $"\t\tReceiver name: {item1.target.CustomerName}.");
        //                    }
        //                    catch (Exception)
        //                    {
        //                        continue;
        //                    }
        //                }
        //                Console.WriteLine($"Parcel to {item.CustomerName}");
        //                foreach (var item2 in item.toCustomer)
        //                {
        //                    try
        //                    {
        //                        Console.WriteLine(
        //                            $"\tID: {item2.ID}.\n" +
        //                            $"\tweight: {item2.weight}.\n" +
        //                            $"\tpriority: {item2.priority}.\n" +
        //                            $"\tStatus: {item2.status}.\n" +
        //                            $"\tThe sender fo parcel:\n" +
        //                            $"\t\tSender ID: {item2.sender.ID}.\n" +
        //                            $"\t\tSender name: {item2.sender.CustomerName}.\n" +
        //                            $"\tThe receiver fo parcel:\n" +
        //                            $"\t\tReceiver ID: {item2.target.ID}.\n" +
        //                            $"\t\tReceiver name: {item2.target.CustomerName}.");
        //                    }
        //                    catch (Exception)
        //                    {
        //                        continue;
        //                    }
        //                };
        //            }
        //            break;
        //        #endregion
        //        #region Parcels
        //        case "Parcels" or "4":
        //            foreach (var item in p.Parcels())
        //            {
        //                Console.WriteLine(
        //                    $"ID: {item.ID}.\n" +
        //                    "Sender:\n" +
        //                    $"\tSender ID: {item.sender.ID}\n" +
        //                    $"\tSende Name: {item.sender.CustomerName}.\n" +
        //                    "Receiver:\n" +
        //                    $"\tReceiver ID: {item.target.ID}.\n" +
        //                    $"\tReceiver name: {item.target.CustomerName}.\n" +
        //                    $"weight: {item.Weight}.\n" +
        //                    $"priority: {item.Priority}.\n" +
        //                    "The Drone of the Parcel:\n" +
        //                    $"\tID: {item.Drone.ID}.\n" +
        //                    $"\tButtery: {item.Drone.Buttery}.\n" +
        //                    $"\tLocation: {item.Drone.current.Lattitude},\n" +
        //                    $"{item.Drone.current.Longitude}\n" +
        //                    $"Requested: {item.Requested}.\n" +
        //                    $"Scheduled: {item.Scheduled}.\n" +
        //                    $"PickedUp: {item.PickedUp}.\n" +
        //                    $"Deliverd: {item.Deliverd}.\n");
        //            }
        //            break;
        //        #endregion
        //        #region Parcel not associated
        //        case "not associated" or "5":
        //            foreach (var item in p.ParcelsNotAssociated())
        //            {
        //                Console.WriteLine(
        //                    $"ID: {item.ID}.\n" +
        //                    "Sender:\n" +
        //                    $"\tSender ID: {item.sender.ID}\n" +
        //                    $"\tSende Name: {item.sender.CustomerName}.\n" +
        //                    "Receiver:\n" +
        //                    $"\tReceiver ID: {item.target.ID}.\n" +
        //                    $"\tReceiver name: {item.target.CustomerName}." +
        //                    $"weight: {item.Weight}.\n" +
        //                    $"priority: {item.Priority}.\n" +
        //                    "The Drone of the Parcel:\n" +
        //                    $"\tID: {item.Drone.ID}.\n" +
        //                    $"\tButtery: {item.Drone.Buttery}.\n" +
        //                    $"\tLocation: {item.Drone.Position.Lattitude}," +
        //                    $"{item.Drone.Position.Longitude}\n" +
        //                    $"Requested: {item.Requested}.\n" +
        //                    $"Scheduled: {item.Scheduled}.\n" +
        //                    $"PickedUp: {item.PickedUp}.\n" +
        //                    $"Deliverd: {item.Deliverd}.\n");
        //            }
        //            break;
        //        #endregion
        //        #region Free chargeslots
        //        case "free chargeslots" or "6":
        //            foreach (var item in p.FreeChargeslots())
        //            {
        //                Console.WriteLine(
        //                $"ID: {item.ID}.\n" +
        //                $"StationName: {item.StationName}.\n" +
        //                $"Location: {item.location.Lattitude},{item.location.Longitude}.\n" +
        //                $"FreeChargeSlots{item.FreeChargeSlots}.\n" +
        //                $"Drone Charging In Station:");
        //                if (item.DroneChargingInStation.Count != 0)
        //                {
        //                    foreach (var item1 in item.DroneChargingInStation)
        //                    {
        //                        Console.WriteLine(
        //                            $"\t-ID: {item1.ID}.\n" +
        //                            $"\t-Buttery: {item1.Buttery}.");
        //                    }
        //                }
        //                else
        //                    Console.WriteLine("\t-None!");
        //            }
        //            break;
        //            #endregion
        //    }
        //}
        static void Main(string[] args)
        {
            BL.BL p = BL.BL.GetInstance();
            string Option = "0";
            Console.WriteLine("Hey you got to ariel&babauv drone's");
            Console.WriteLine("Choose what you want to do");
            Console.WriteLine("Type what you want to do from the list below");
            do
            {
                Console.WriteLine("Addition(= 1), Updating(= 2), Display(= 3), List view(= 4) or Exit(= 5)");
                Option = Console.ReadLine();
                try
                {
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
                        //case "List view" or "4":
                        //    FunListview(p);
                        //    break;
                        case "Exit" or "5":
                                Console.WriteLine("Thank you have a good day");
                                break;
                            default:
                                break;
                        };
                }
                catch (Exception ex)//catch all the exception and try agine
                {
                    Console.WriteLine($"{ex.Message}");
                    Console.WriteLine("we doing to do it again");
                }
            }
            while (Option != "5");
        }
        /// <summary>
        /// display the id of all the same obj
        /// </summary>
        /// <param string="type"></param>
        public static void Viewid(string type)
        {
            //int count = 0;
            //switch (type)
            //{
            //    case "p":
            //        foreach (var i in DataSource.parcels)
            //        {
            //            count++;
            //            Console.WriteLine($"id:{count} = {i.ID}");
            //        }
            //        break;
            //    case "d":
            //        foreach (var i in DataSource.drones)
            //        {
            //            count++;
            //            Console.WriteLine($"id:{count} = {i.ID}");
            //        }
            //        break;
            //    case "c":
            //        foreach (var i in DataSource.customers)
            //        {
            //            count++;
            //            Console.WriteLine($"id:{count} = {i.ID}");
            //        }
            //        break;
            //    case "s":
            //        foreach (var i in DataSource.stations)
            //        {
            //            count++;
            //            Console.WriteLine($"id:{count} = {i.ID}");
            //        }
            //        break;
            //}
        }
    }
}

