using System;
using System.Runtime.Serialization;
using BL.BO;
using DAL;
using BLExceptions;

namespace ConsoleUI_BL
{
    class Program
    {
        public static int chackID (int id,string type)
        {
            switch (type)
            {
                case "s":
                    return DataSource.stations.FindIndex(i => i.ID == id);
                case "d":
                    return DataSource.drones.FindIndex(i => i.ID == id);
                case "c":
                    return DataSource.customers.FindIndex(i => i.ID == id);
            }
            return 0;
        }
        public static void FunAddition(BL.BL p)
        {
            Console.WriteLine("OK, what do you want to add ? choose");
            Console.WriteLine("add station => 1\nadd drone => 2\nadd customer => 3\nadd parcel => 4");
            string t;
            t = Console.ReadLine();
            switch (t)
            {
                #region Add station
                case "Add station" or "1":
                    Console.WriteLine("enter id station ,station name ,location ,how meny charge slots are");
                    int idStation = int.Parse(Console.ReadLine());
                    if (chackID(idStation, "s")!=-1)
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
                    p.AddStation(idStation, nameStation, locationStation, numChargeSlotsStation);
                    break;
                #endregion
                #region Add drone
                case "Add drone" or "2":
                    Console.WriteLine("enter id, Model name, weight(LIGHT = 1, MEDIUM = 2, HEAVY = 3), ID of the starting station ");
                    int idDrone = int.Parse(Console.ReadLine());
                    if (chackID(idDrone, "d") != 1)
                    {
                        throw new AlreadyExistException($"this id {idDrone} already exist");
                    }
                    string nameDrone = Console.ReadLine();
                    BL.BO.WEIGHT weightDrone = (BL.BO.WEIGHT)int.Parse(Console.ReadLine()); 
                    if (((int)weightDrone != 1) || ((int)weightDrone != 2) || ((int)weightDrone != 3))
                        throw new PutTheRightNumber($"this weight {weightDrone} is not in the right form");

                    int IDStartingStation = int.Parse(Console.ReadLine());
                    p.AddDrone(idDrone, nameDrone, weightDrone, IDStartingStation);
                    break;
                #endregion
                #region Add customer
                case "add customer" or "3":
                    Console.WriteLine("enter id customer, customer name, customer phone number, customer location");
                    int idCustomer = int.Parse(Console.ReadLine());
                    if (chackID(idCustomer, "c") != 1)
                    {
                        throw new AlreadyExistException($"this id {idCustomer} already exist");
                    }
                    string nameCustomer = Console.ReadLine();
                    string phoneCustomer = Console.ReadLine();
                    if ((phoneCustomer.Length==10)&&(phoneCustomer[0]=='0')&&(phoneCustomer[1]=='5')) 
                        throw new NotTheRightForm($"this phone number {phoneCustomer} is not in the right form");
                    Location locationCustomer = new()
                    {
                        Longitude = double.Parse(Console.ReadLine()),
                        Lattitude = double.Parse(Console.ReadLine()),
                    };
                    p.AddCustomer(idCustomer, nameCustomer, phoneCustomer, locationCustomer);
                    break;
                #endregion
                #region Add parcel
                case "add parcel" or "4":
                    Console.WriteLine("enter SenderId, TargetId, weight(LIGHT = 1, MEDIUM = 2, HEAVY = 3), priority(REGULAR = 1, FAST = 2, SOS = 3 )");
                    int SenderIdParcel = int.Parse(Console.ReadLine());
                    int TargetIdParcel = int.Parse(Console.ReadLine());
                    BL.BO.WEIGHT weightParcel = (BL.BO.WEIGHT)(int.Parse(Console.ReadLine()));
                    if(((int)weightParcel !=1) || ((int)weightParcel != 2) || ((int)weightParcel != 3))
                        throw new PutTheRightNumber($"this weight {weightParcel} is not in the right form");
                    int temp = int.Parse(Console.ReadLine());
                    if ((temp != 1) || (temp != 2) || (temp != 3))
                        throw new PutTheRightNumber($"this priority {temp} is not in the right form");
                    BL.BO.PRIORITY priorityParcel = (BL.BO.PRIORITY)temp;
                    p.AddParcel(SenderIdParcel, TargetIdParcel, weightParcel, priorityParcel);
                    break;
                #endregion
            }
        }
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
                        Viewid("p");
                        Console.WriteLine("enter drone's id new");
                    }
                    int idUpDataDrone = Int32.Parse(Console.ReadLine());
                    if (chackID(idUpDataDrone,"d")==-1)
                    {
                        throw new DoesNotExistException($"this id {idUpDataDrone} dont exist");

                    }
                    Console.WriteLine("enter the new model");
                    string newModelUpDataDrone = Console.ReadLine();
                    p.UpdateDrone(idUpDataDrone, newModelUpDataDrone);
                    break;
                #endregion
                #region Updata Station
                case "UpdataStation" or "2":
                    Console.WriteLine("enter Station's id");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("p");   
                        Console.WriteLine("enter Station's id new");

                    }
                    int idStationUpdata = Int32.Parse(Console.ReadLine());
                    if (chackID(idStationUpdata, "d") == -1)
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
                    p.UpdateStation(idStationUpdata, nameStationUpData, chargeSlotsStationuUpData);
                    break;
                #endregion
                #region Update Customer
                case "UpdateCustomer" or "3":
                    Console.WriteLine("enter Customer's id");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("p");    
                        Console.WriteLine("enter Customer's id new");

                    }
                    int idCustomerUpdata = Int32.Parse(Console.ReadLine());
                    if (chackID(idCustomerUpdata, "d") == -1)
                    {
                        throw new DoesNotExistException($"this id {idCustomerUpdata} dont exist");

                    }
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
                    string PhoneNumberCustomerUpData = "null";
                    if (e == 1)
                    {
                        PhoneNumberCustomerUpData = Console.ReadLine();
                    }
                    p.UpdateCustomer(idCustomerUpdata, nameCustomerUpData, PhoneNumberCustomerUpData);
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
                    if (chackID(droneID, "d") == -1)
                    {
                        throw new DoesNotExistException($"this id {droneID} dont exist");

                    }
                    p.DroneToCharge(droneID);
                    break;
                #endregion
                #region Release from charging
                case "release from charging" or "5":
                    Console.WriteLine("enter droneID");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("d");    
                        Console.WriteLine("enter droneId new");

                    }
                    int idDroneReleaseFromCharge = Int32.Parse(Console.ReadLine());
                    if (chackID(idDroneReleaseFromCharge, "d") == -1)
                    {
                        throw new DoesNotExistException($"this id {idDroneReleaseFromCharge} dont exist");

                    }
                    Console.WriteLine("how many hour the drone has charged (in full hours)");
                    TimeSpan timeInCharge = TimeSpan.FromHours(int.Parse(Console.ReadLine()));
                    p.DroneOutCharge(idDroneReleaseFromCharge, timeInCharge);
                    break;
                #endregion
                # region Assign a parcel to a drone
                case "Attache drone to parcel" or "6":
                    Console.WriteLine("enter drone's id");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("p");        
                        Console.WriteLine("enter drone's id new");

                    }
                    int idDroneAttache = Int32.Parse(Console.ReadLine());
                    if (chackID(idDroneAttache, "d") == -1)
                        throw new DoesNotExistException($"this id {idDroneAttache} dont exist");
                    if (p.findDrone(idDroneAttache).haveParcel)
                        throw new DroneAloreadyAttached($"this drone is already attached");
                    p.AttacheDrone(idDroneAttache);
                    break;
                #endregion
                #region Collection of a parcel by drone
                case "PickUp parcel" or "7":
                    Console.WriteLine("enter drone's id");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("p");          
                        Console.WriteLine("enter drone's id new");

                    }
                    int idDronePickUp = Int32.Parse(Console.ReadLine());
                    if (chackID(idDronePickUp, "d") == -1)
                    {
                        throw new DoesNotExistException($"this id {idDronePickUp} dont exist");

                    }
                    p.PickUpParcel(idDronePickUp);
                    break;
                #endregion
                #region Delivery of a parcel by drone
                case "delivery parcel" or "8":
                    Console.WriteLine("enter drone's id");
                    Console.WriteLine("if you want to see the id list prees 1 else press any key");
                    type = Console.ReadLine();
                    if (type == "1")
                    {
                        Viewid("p");    
                        Console.WriteLine("enter drone's id new");

                    }
                    int idDroneDelivery = Int32.Parse(Console.ReadLine());
                    if (chackID(idDroneDelivery, "d") == -1)
                    {
                        throw new DoesNotExistException($"this id {idDroneDelivery} dont exist");

                    }
                    p.Parceldelivery(idDroneDelivery);
                    break;
                    #endregion
            }
        }
        public static void FunDisplay(BL.BL p)
        {
            Console.WriteLine("OK, what do you want to see ? choose");
            Console.WriteLine("Station => 1\nDrone => 2\nCustomer => 3\nParcel => 4");
            string t;
            t = Console.ReadLine();
            switch (t)
            {
                #region Station
                case "Station" or "1":
                    Console.WriteLine("enter id station");
                    int idStation = int.Parse(Console.ReadLine());
                    if (chackID(idStation, "s") == -1)
                    {
                        throw new DoesNotExistException($"this id {idStation} dont exist");
                    }
                    Station s = p.findStation(idStation);
                    Console.WriteLine(
                        $"ID: {s.ID}.\n" +
                        $"StationName: {s.StationName}.\n" +
                        $"Location: {s.location.Lattitude},{s.location.Longitude}.\n" +
                        $"FreeChargeSlots: {s.FreeChargeSlots}.\n" + 
                        $"Drone Charging In Station:");
                    if (s.DroneChargingInStation.Count!=0)
                    {
                        foreach (var item in s.DroneChargingInStation)
                        {
                            Console.WriteLine(
                                $"\t-ID: {item.ID}.\n" +
                                $"\t-Buttery: {item.Buttery}.");
                        }
                    }
                    else
                        Console.WriteLine("\t-None!");
                    break;
                #endregion
                #region Drone
                case "Drone" or "2":
                    Console.WriteLine("enter id");
                    int idDrone = int.Parse(Console.ReadLine());
                    if (chackID(idDrone, "d") != 1)
                    {
                        throw new DoesNotExistException($"this id {idDrone} dont exist");
                    }
                    Drone d = p.findDrone(idDrone);
                    Console.WriteLine(
                        $"ID: {d.ID}.\n" +
                        $"Model: {d.Model}.\n" +
                        $"Weight: {d.Weight}.\n" + 
                        $"Status: {d.Status}.\n" +
                        $"Buttery: {d.Buttery}.\n" +
                        $"Location: {d.current.Lattitude},{d.current.Longitude}.\n" +
                        "Parcel In Transactining");
                    Console.WriteLine(
                        $"\tID: {d.parcel.ID}.\n" +
                        $"\tParcel Status: {d.parcel.ParcelStatus}.\n" +
                        $"\tpriority: {d.parcel.priority}.\n" +
                        $"\tweight: {d.parcel.weight}.\n" +
                        $"\tThe sender fo parcel:" +
                        $"\t\tSender ID: {d.parcel.sender.ID}.\n" + 
                        $"\t\tSender name: {d.parcel.sender.CustomerName}.\n" +
                        $"\t\tSender Location: {d.parcel.Lsender.Lattitude}," +
                        $"{d.parcel.Lsender.Longitude}\n" +
                        $"\tThe receiver fo parcel:" +
                        $"\t\tReceiver ID: {d.parcel.target.ID}.\n" +
                        $"\t\tReceiver name: {d.parcel.target.CustomerName}.\n" +
                        $"\t\tReceiver Location: {d.parcel.Ltarget.Lattitude}," +
                        $"{d.parcel.Ltarget.Longitude}\n" +
                        $"distance: {d.parcel.distance}.\n" 
                        );
                    break;
                #endregion
                #region Customer
                case "Customer" or "3":
                    Console.WriteLine("enter id");
                    int idCustomer = int.Parse(Console.ReadLine());
                    if (chackID(idCustomer, "c") != 1)
                    {
                        throw new DoesNotExistException($"this id {idCustomer} dont exist");
                    }
                    Customer c = p.findCustomer(idCustomer);
                    Console.WriteLine(
                        $"ID: {c.ID}.\n" +
                        $"Customer Name: {c.CustomerName}.\n" +
                        $"Phone: {c.Phone}.\n" +
                        $"Location: {c.location.Lattitude},{c.location.Longitude}\n" +
                        $"Parcel from {c.CustomerName}");
                    foreach (var item in c.fromCustomer)
                    {
                        Console.WriteLine(
                        $"\tID: {item.ID}.\n" +
                        $"\tweight: {item.weight}.\n" +                    
                        $"\tpriority: {item.priority}.\n" +
                        $"\tStatus: {item.status}.\n" +
                        $"\tThe sender fo parcel:" +
                        $"\t\tSender ID: {item.sender.ID}.\n" +
                        $"\t\tSender name: {item.sender.CustomerName}.\n" +
                        $"\tThe receiver fo parcel:" +
                        $"\t\tReceiver ID: {item.target.ID}.\n" +
                        $"\t\tReceiver name: {item.target.CustomerName}.");
                    }
                    Console.WriteLine($"Parcel to {c.CustomerName}");
                    foreach (var item in c.toCustomer)
                    {
                        Console.WriteLine(
                            $"\tID: {item.ID}.\n" +
                            $"\tweight: {item.weight}.\n" +
                            $"\tpriority: {item.priority}.\n" +
                            $"\tStatus: {item.status}.\n" +
                            $"\tThe sender fo parcel:" +
                            $"\t\tSender ID: {item.sender.ID}.\n" +
                            $"\t\tSender name: {item.sender.CustomerName}.\n" +
                            $"\tThe receiver fo parcel:" +
                            $"\t\tReceiver ID: {item.target.ID}.\n" +
                            $"\t\tReceiver name: {item.target.CustomerName}.");
                    };
                    break;
                #endregion
                #region Parcel
                case "Parcel" or "4":
                    Console.WriteLine("enter Id");
                    int idParcel = int.Parse(Console.ReadLine());
                    if (chackID(idParcel, "p") != 1)
                    {
                        throw new DoesNotExistException($"this id {idParcel} dont exist");
                    }
                    Parcel parcel = p.findParcel(idParcel);
                    Console.WriteLine(
                        $"ID: {parcel.ID}.\n" +
                        "Sender:\n" +
                        $"\tSender ID: {parcel.sender.ID}\n" +
                        $"\tSende Name: {parcel.sender.CustomerName}.\n" +
                        "Receiver:\n" +
                        $"\tReceiver ID: {parcel.target.ID}.\n" +
                        $"\tReceiver name: {parcel.target.CustomerName}." +
                        $"weight: {parcel.Weight}.\n" +
                        $"priority: {parcel.Priority}.\n" +
                        "The Drone of the Parcel:\n" +
                        $"\tID: {parcel.Drone.ID}.\n" +
                        $"\tButtery: {parcel.Drone.Buttery}.\n" +
                        $"\tLocation: {parcel.Drone.current.Lattitude}," +
                        $"{parcel.Drone.current.Longitude}\n" +
                        $"Requested: {parcel.Requested}.\n" +
                        $"Scheduled: {parcel.Scheduled}.\n" +
                        $"PickedUp: {parcel.PickedUp}.\n" +
                        $"Deliverd: {parcel.Deliverd}.\n");
                    break;
                 #endregion
            }
        }

        public static void FunListview(BL.BL p)
        {
            Console.WriteLine("OK, what do you want to see ? choose");
            Console.WriteLine("Station list => 1\nDrone list => 2\nCustomer list => 3\nParcel list => 4\nParcel not associated => 5\nFree chargeslots => 6");
            string t;
            t = Console.ReadLine();
            switch (t)
            {
                #region Stations
                case "Stations" or "1":
                    foreach (var item in p.stations())
                    {
                        Console.WriteLine(
                        $"ID: {item.ID}.\n" +
                        $"StationName: {item.StationName}.\n" +
                        $"Location: {item.location.Lattitude},{item.location.Longitude}.\n" +
                        $"FreeChargeSlots: {item.FreeChargeSlots}.\n" +
                        $"Drone Charging In Station:");
                        if (item.DroneChargingInStation.Count != 0)
                        {
                            foreach (var item1 in item.DroneChargingInStation)
                            {
                                Console.WriteLine(
                                    $"\t-ID: {item1.ID}.\n" +
                                    $"\t-Buttery: {item1.Buttery}.");
                            }
                        }
                        else
                            Console.WriteLine("\t-None!");
                    }
                    break;
                #endregion
                #region Drones
                case "Drones" or "2":
                    foreach (var item in p.drones())
                    {
                        Console.WriteLine(
                            $"ID: {item.ID}.\n" +
                            $"Model: {item.Model}.\n" +
                            $"Weight: {item.Weight}.\n" +
                            $"Status: {item.Status}.\n" +
                            $"Buttery: {item.Buttery}.\n" +
                            $"Location: {item.current.Lattitude},{item.current.Longitude}.\n" +
                            "Parcel In Transactining");
                        Console.WriteLine(
                            $"\tID: {item.parcel.ID}.\n" +
                            $"\tParcel Status: {item.parcel.ParcelStatus}.\n" +
                            $"\tpriority: {item.parcel.priority}.\n" +
                            $"\tweight: {item.parcel.weight}.\n" +
                            $"\tThe sender fo parcel:" +
                            $"\t\tSender ID: {item.parcel.sender.ID}.\n" +
                            $"\t\tSender name: {item.parcel.sender.CustomerName}.\n" +
                            $"\t\tSender Location: {item.parcel.Lsender.Lattitude}," +
                            $"{item.parcel.Lsender.Longitude}\n" +
                            $"\tThe receiver fo parcel:" +
                            $"\t\tReceiver ID: {item.parcel.target.ID}.\n" +
                            $"\t\tReceiver name: {item.parcel.target.CustomerName}.\n" +
                            $"\t\tReceiver Location: {item.parcel.Ltarget.Lattitude}," +
                            $"{item.parcel.Ltarget.Longitude}\n" +
                            $"distance: {item.parcel.distance}.\n"
                            );
                    }
                    break;
                #endregion
                #region Customers
                case "Customers" or "3":
                    foreach (var item in p.customers())
                    {
                        Console.WriteLine(
                            $"ID: {item.ID}.\n" +
                            $"Customer Name: {item.CustomerName}.\n" +
                            $"Phone: {item.Phone}.\n" +
                            $"Location: {item.location.Lattitude},{item.location.Longitude}\n" +
                            $"Parcel from {item.CustomerName}");
                        foreach (var item1 in item.fromCustomer)
                        {
                            Console.WriteLine(
                            $"\tID: {item1.ID}.\n" +
                            $"\tweight: {item1.weight}.\n" +
                            $"\tpriority: {item1.priority}.\n" +
                            $"\tStatus: {item1.status}.\n" +
                            $"\tThe sender fo parcel:" +
                            $"\t\tSender ID: {item1.sender.ID}.\n" +
                            $"\t\tSender name: {item1.sender.CustomerName}.\n" +
                            $"\tThe receiver fo parcel:" +
                            $"\t\tReceiver ID: {item1.target.ID}.\n" +
                            $"\t\tReceiver name: {item1.target.CustomerName}.");
                        }
                        Console.WriteLine($"Parcel to {item.CustomerName}");
                        foreach (var item2 in item.toCustomer)
                        {
                            Console.WriteLine(
                                $"\tID: {item2.ID}.\n" +
                                $"\tweight: {item2.weight}.\n" +
                                $"\tpriority: {item2.priority}.\n" +
                                $"\tStatus: {item2.status}.\n" +
                                $"\tThe sender fo parcel:" +
                                $"\t\tSender ID: {item2.sender.ID}.\n" +
                                $"\t\tSender name: {item2.sender.CustomerName}.\n" +
                                $"\tThe receiver fo parcel:" +
                                $"\t\tReceiver ID: {item2.target.ID}.\n" +
                                $"\t\tReceiver name: {item2.target.CustomerName}.");
                        };
                    }     
                    break;
                #endregion
                #region Parcels
                case "Parcels" or "4":
                    foreach (var item in  p.parcels())
                    {
                        Console.WriteLine(
                            $"ID: {item.ID}.\n" +
                            "Sender:\n" +
                            $"\tSender ID: {item.sender.ID}\n" +
                            $"\tSende Name: {item.sender.CustomerName}.\n" +
                            "Receiver:\n" +
                            $"\tReceiver ID: {item.target.ID}.\n" +
                            $"\tReceiver name: {item.target.CustomerName}." +
                            $"weight: {item.Weight}.\n" +
                            $"priority: {item.Priority}.\n" +
                            "The Drone of the Parcel:\n" +
                            $"\tID: {item.Drone.ID}.\n" +
                            $"\tButtery: {item.Drone.Buttery}.\n" +
                            $"\tLocation: {item.Drone.current.Lattitude}," +
                            $"{item.Drone.current.Longitude}\n" +
                            $"Requested: {item.Requested}.\n" +
                            $"Scheduled: {item.Scheduled}.\n" +
                            $"PickedUp: {item.PickedUp}.\n" +
                            $"Deliverd: {item.Deliverd}.\n");
                    }
                    break;
                #endregion
                #region Parcel not associated
                case "not associated" or "5":
                    foreach (var item in p.parcelsNotAssociated())
                    {
                        Console.WriteLine(
                            $"ID: {item.ID}.\n" +
                            "Sender:\n" +
                            $"\tSender ID: {item.sender.ID}\n" +
                            $"\tSende Name: {item.sender.CustomerName}.\n" +
                            "Receiver:\n" +
                            $"\tReceiver ID: {item.target.ID}.\n" +
                            $"\tReceiver name: {item.target.CustomerName}." +
                            $"weight: {item.Weight}.\n" +
                            $"priority: {item.Priority}.\n" +
                            "The Drone of the Parcel:\n" +
                            $"\tID: {item.Drone.ID}.\n" +
                            $"\tButtery: {item.Drone.Buttery}.\n" +
                            $"\tLocation: {item.Drone.current.Lattitude}," +
                            $"{item.Drone.current.Longitude}\n" +
                            $"Requested: {item.Requested}.\n" +
                            $"Scheduled: {item.Scheduled}.\n" +
                            $"PickedUp: {item.PickedUp}.\n" +
                            $"Deliverd: {item.Deliverd}.\n");
                    }
                    break;
                #endregion
                #region Free chargeslots
                case "free chargeslots" or "6":
                    foreach (var item in p.FreeChargeslots())
                    {
                        Console.WriteLine(
                        $"ID: {item.ID}.\n" +
                        $"StationName: {item.StationName}.\n" +
                        $"Location: {item.location.Lattitude},{item.location.Longitude}.\n" +
                        $"FreeChargeSlots{item.FreeChargeSlots}.\n" +
                        $"Drone Charging In Station:");
                        if (item.DroneChargingInStation.Count != 0)
                        {
                            foreach (var item1 in item.DroneChargingInStation)
                            {
                                Console.WriteLine(
                                    $"\t-ID: {item1.ID}.\n" +
                                    $"\t-Buttery: {item1.Buttery}.");
                            }
                        }
                        else
                            Console.WriteLine("\t-None!");
                    }
                    break;
                #endregion
            }
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
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    Console.WriteLine("we doing to do it again");
                }
            }
            while (Option != "5");
        }

        public static void Viewid(string type)
        {
            int count = 0;
            switch (type)
            {
                case "p":
                    foreach (var i in DataSource.parcels)
                    {
                        count++;
                        Console.WriteLine($"id:{count} = {i.ID}");
                    }
                    count = 0;
                    break;
                case "d":
                    foreach (var i in DataSource.drones)
                    {
                        count++;
                        Console.WriteLine($"id:{count} = {i.ID}");
                    }
                    count = 0;
                    break;
                case "c":
                    foreach (var i in DataSource.customers)
                    {
                        count++;
                        Console.WriteLine($"id:{count} = {i.ID}");
                    }
                    count = 0;
                    break;
                case "s":
                    foreach (var i in DataSource.stations)
                    {
                        count++;
                        Console.WriteLine($"id:{count} = {i.ID}");
                    }
                    count = 0;
                    break;
            }
        } // לטפל בסוף
    }
}

