using DALExceptionscs;
using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace DAL
{
    /// <summary>
    /// Defines the <see cref="DalXml" />.
    /// </summary>
    public sealed class DalXml : DalApi.IDal
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="DalXml"/> class from being created.
        /// </summary>
        private DalXml()
        {
            DAL.DataSource.Initialize();
            if (!File.Exists(dir + DroneChargesPath))
                XMLTools.SaveListToXMLSerializer(DataSource.droneCharges, DroneChargesPath);
            if (!File.Exists(dir + ParcelsPath))
                XMLTools.SaveListToXMLSerializer(DataSource.parcels, ParcelsPath);
            if (!File.Exists(dir + DronesPath))
                XMLTools.SaveListToXMLSerializer(DataSource.drones, DronesPath);
            if (!File.Exists(dir + StationsPath))
                XMLTools.SaveListToXMLSerializer(DataSource.stations, StationsPath);
            Initialization();
            Console.WriteLine("11111");
        }

        /// <summary>
        /// Defines the instance.
        /// </summary>
        public static DalXml instance = null;

        /// <summary>
        /// The GetInstance.
        /// </summary>
        /// <returns>The <see cref="DalXml"/>.</returns>
        public static DalXml GetInstance()
        {
            if (instance == null)
                instance = new DalXml();
            return instance;
        }

        /// <summary>
        /// Defines the dir.
        /// </summary>
        public static readonly string dir = @"XML Files\";

        /// <summary>
        /// Defines the ConfigPath.
        /// </summary>
        internal static string ConfigPath = @"Config.xml";

        /// <summary>
        /// Defines the DronesPath.
        /// </summary>
        internal string DronesPath = @"DronesXml.xml";

        /// <summary>
        /// Defines the StationsPath.
        /// </summary>
        internal static string StationsPath = @"StationsXml.xml";

        /// <summary>
        /// Defines the ParcelsPath.
        /// </summary>
        internal static string ParcelsPath = @"ParcelsXml.xml";

        /// <summary>
        /// Defines the CustomersPath.
        /// </summary>
        internal static string CustomersPath = @"CustomersXml.xml";

        /// <summary>
        /// Defines the DroneChargesPath.
        /// </summary>
        internal static string DroneChargesPath = @"DroneChargesXml.xml";

        //static int GetID(XElement root, int add)
        //{
        //    string tempString = root.Element("staticId").Value;
        //    int tempInt = Convert.ToInt32(tempString);
        //    root.Element("staticId").Value = (tempInt + add).ToString();
        //    root.Save(dir + @"config.xml");
        //    return tempInt + add;
        //}
        /// <summary>
        /// The GetRandomNumber.
        /// </summary>
        /// <param name="minimum">The minimum<see cref="double"/>.</param>
        /// <param name="maximum">The maximum<see cref="double"/>.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        /// <summary>
        /// The Initialization.
        /// </summary>
        public static void Initialization()
        {
            XElement id, isActive, customerName, phone, longitude, lattitude, password, email;

            XElement ConfigRoot;
            string ConfigPath = dir + @"config.xml";
            if (!File.Exists(ConfigPath))
            {
                CreateFiles(out ConfigRoot, ConfigPath, "config");
                XElement staticId, free, light, medium, heavy, chargePerHour;
                staticId = new XElement("staticId", Config.staticId);
                free = new XElement("free", Config.Free);
                light = new XElement("light", Config.Light);
                medium = new XElement("medium", Config.Medium);
                heavy = new XElement("heavy", Config.Heavy);
                chargePerHour = new XElement("chargePerHour", Config.ChargePerHour);
                ConfigRoot.Add(staticId, free, light, medium, heavy, chargePerHour);
                ConfigRoot.Save(ConfigPath);
            }
            else
                LoadData(out ConfigRoot, ConfigPath);
            DalXml.ConfigPath = ConfigPath;


            XElement CustomersRoot;
            string CustomersPath = dir + @"CustomersXml.xml";
            if (!File.Exists(CustomersPath))
            {
                CreateFiles(out CustomersRoot, CustomersPath, "Customers");
                for (int i = 0; i < 10; i++)
                {
                    id = new XElement("ID", DataSource.customers[i].ID);
                    isActive = new XElement("IsActive", DataSource.customers[i].IsActive);
                    customerName = new XElement("CustomerName", DataSource.customers[i].CustomerName);
                    phone = new XElement("Phone", DataSource.customers[i].Phone);
                    longitude = new XElement("Longitude", DataSource.customers[i].Longitude);
                    lattitude = new XElement("Lattitude", DataSource.customers[i].Lattitude);
                    password = new XElement("Password", DataSource.customers[i].Password);
                    email = new XElement("Email", DataSource.customers[i].Email);
                    CustomersRoot.Add(new XElement("Customer", id, isActive, phone, customerName, longitude, lattitude, password, email));
                    CustomersRoot.Save(CustomersPath);
                }
            }
            else
            {
                LoadData(out CustomersRoot, CustomersPath);
            }
        }

        /// <summary>
        /// The CreateFiles.
        /// </summary>
        /// <param name="elementRoot">The elementRoot<see cref="XElement"/>.</param>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="type">The type<see cref="string"/>.</param>
        private static void CreateFiles(out XElement elementRoot, string path, string type)
        {
            elementRoot = new XElement(type);
            elementRoot.Save(path);
        }

        /// <summary>
        /// The LoadData.
        /// </summary>
        /// <param name="elementRoot">The elementRoot<see cref="XElement"/>.</param>
        /// <param name="path">The path<see cref="string"/>.</param>
        private static void LoadData(out XElement elementRoot, string path)
        {
            elementRoot = null;
            try
            {
                elementRoot = XElement.Load(path);
            }
            catch
            {
                Console.WriteLine("File upload problem");
            }
        }

        /// <summary>
        /// The Power.
        /// </summary>
        /// <returns>The <see cref="double[]"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] Power()
        {
            double[] a = {
                DAL.Config.Free,
                DAL.Config.Light,
                DAL.Config.Medium,
                DAL.Config.Heavy,
                DAL.Config.ChargePerHour };
            return a;
        }

        /// <summary>
        /// The AddCustomer.
        /// </summary>
        /// <param name="c">The c<see cref="Customer"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer c)
        {
            XElement CustomersRoot = LoadXml(CustomersPath);
            int index = LoadCustomersFromXML(CustomersRoot).ToList().FindIndex(i => i.ID == c.ID);
            if (index != -1)
                throw new NameIsUsedException($"An existing name is on the system.");
            try
            {
                CustomersRoot.Add(XmlHelper.BuildElementToXml(c));

                CustomersRoot.Save(dir + CustomersPath);
            }
            catch (Exception ex) { throw new XmlWriteException(ex.Message, ex); }
        }

        /// <summary>
        /// The AddDroneChecker.
        /// </summary>
        /// <param name="d">The d<see cref="Drone"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool AddDroneChecker(Drone d)
        {
            if (Dronelist().ToList().FindIndex(i => i.ID == d.ID) == -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
        }

        /// <summary>
        /// The AddParcelChecker.
        /// </summary>
        /// <param name="parcel">The parcel<see cref="Parcel"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool AddParcelChecker(Parcel parcel)
        {
            if (Parcellist().ToList().FindIndex(i => i.ID == parcel.ID) == -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
        }

        /// <summary>
        /// The AddStationChecker.
        /// </summary>
        /// <param name="s">The s<see cref="Station"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool AddStationChecker(Station s)
        {
            if (Stationlist().ToList().FindIndex(i => i.ID == s.ID) == -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
        }

        /// <summary>
        /// The AddDroneChargeChecker.
        /// </summary>
        /// <param name="d">The d<see cref="DroneCharge"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool AddDroneChargeChecker(DroneCharge d)
        {
            if (DroneChargelist().ToList().FindIndex(i => i.DroneId == d.DroneId) == -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
        }

        /// <summary>
        /// The UpdateDroneChecker.
        /// </summary>
        /// <param name="c">The c<see cref="Drone"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int UpdateDroneChecker(Drone c)
        {
            int i = Dronelist().ToList().FindIndex(i => i.ID == c.ID);
            if (i != -1)
                return i;
            throw new NameIsUsedException($"An existing user name or email on the system.");
        }

        /// <summary>
        /// The UpdateParcelChecker.
        /// </summary>
        /// <param name="parcel">The parcel<see cref="Parcel"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int UpdateParcelChecker(Parcel parcel)
        {
            int i = Parcellist().ToList().FindIndex(i => i.ID == parcel.ID);
            if (i != -1)
                return i;
            throw new NameIsUsedException($"An existing user name or email on the system.");
        }

        /// <summary>
        /// The UpdateStationChecker.
        /// </summary>
        /// <param name="s">The s<see cref="Station"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int UpdateStationChecker(Station s)
        {
            int i = Stationlist().ToList().FindIndex(i => i.ID == s.ID);
            if (i != -1)
                return i;
            throw new NameIsUsedException($"An existing user name or email on the system.");
        }

        /// <summary>
        /// The AddDrone.
        /// </summary>
        /// <param name="d">The d<see cref="Drone"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone d)
        {
            XMLTools.Add(d, AddDroneChecker(d), DronesPath);
        }

        /// <summary>
        /// The AddParcel.
        /// </summary>
        /// <param name="parcel">The parcel<see cref="Parcel"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(Parcel parcel)
        {
            XMLTools.Add(parcel, AddParcelChecker(parcel), ParcelsPath);
        }

        /// <summary>
        /// The AddStation.
        /// </summary>
        /// <param name="s">The s<see cref="Station"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station s)
        {
            XMLTools.Add(s, AddStationChecker(s), StationsPath);
        }

        /// <summary>
        /// The AddDroneCharge.
        /// </summary>
        /// <param name="d">The d<see cref="DroneCharge"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(DroneCharge d)
        {

            XMLTools.Add(d, AddDroneChargeChecker(d), DroneChargesPath);
        }

        /// <summary>
        /// The AddDroneCharge.
        /// </summary>
        /// <param name="DroneId">The DroneId<see cref="int"/>.</param>
        /// <param name="StationId">The StationId<see cref="int"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(int DroneId, int StationId)
        {
            DroneCharge d = new() { DroneId = DroneId, StationId = StationId };
            AddDroneCharge(d);
        }

        /// <summary>
        /// The UpdateCustomer.
        /// </summary>
        /// <param name="customer">The customer<see cref="Customer"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(Customer customer)
        {
            XElement CustomersRoot = LoadXml(CustomersPath);
            int index = LoadCustomersFromXML(CustomersRoot).ToList().FindIndex(i => (i.ID == customer.ID));
            if (index == -1)
                throw new NameIsUsedException($"not An existing name is on the system.");
            try
            {
                var item = CustomersRoot.Elements().Where(i => int.Parse(i.Element("ID").Value) == customer.ID).FirstOrDefault();
                item.ReplaceWith(
                            new XElement("Customer",
                            new XElement("ID", customer.ID),
                            new XElement("IsActive", customer.IsActive),
                            new XElement("CustomerName", customer.CustomerName),
                            new XElement("Phone", customer.Phone),
                            new XElement("Longitude", customer.Longitude),
                            new XElement("Lattitude", customer.Lattitude)),
                            new XElement("Password", customer.Password),
                            new XElement("Email", customer.Email)
                            );
                CustomersRoot.Save(dir + CustomersPath);
            }
            catch (Exception ex) { throw new XmlWriteException(ex.Message, ex); }
        }

        /// <summary>
        /// The UpdateStation.
        /// </summary>
        /// <param name="station">The station<see cref="Station"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(Station station)
        {
            XMLTools.Update(station, UpdateStationChecker(station), StationsPath);
        }

        /// <summary>
        /// The UpdateParcel.
        /// </summary>
        /// <param name="parcel">The parcel<see cref="Parcel"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(Parcel parcel)
        {
            XMLTools.Update(parcel, UpdateParcelChecker(parcel), ParcelsPath);
        }

        /// <summary>
        /// The UpdateDrone.
        /// </summary>
        /// <param name="drone">The drone<see cref="Drone"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(Drone drone)
        {
            XMLTools.Update(drone, UpdateDroneChecker(drone), DronesPath);
        }

        /// <summary>
        /// The AttacheDrone.
        /// </summary>
        /// <param name="parcelID">The parcelID<see cref="int"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AttacheDrone(int parcelID)
        {
            int indexDrone = Dronelist().ToList().FindIndex(i =>
                (i.Status == Status.CREAT || i.Status == Status.MAINTENANCE)
                && i.haveParcel == false
                && i.IsActive == true);
            Drone d = new();
            d = Dronelist().ToList()[indexDrone];
            if (d.IsActive == false)
                throw new DeleteException($"This drone can't attached: {d.ID}");

            d.Status = Status.BELONG;
            d.haveParcel = true;
            UpdateDrone(d);

            int indexParcel = Parcellist().ToList().FindIndex(i => i.ID == parcelID);
            Parcel p = new();
            p = Parcellist().ToList()[indexParcel];
            p.DroneId = (int)d.ID;
            p.Status = StatusParcel.BELONG;
            p.Scheduled = DateTime.Now;
            UpdateParcel(p);
        }

        /// <summary>
        /// The PickupParcel.
        /// </summary>
        /// <param name="parcelID">The parcelID<see cref="int"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PickupParcel(int parcelID)
        {
            int indexParcel = Parcellist().ToList().FindIndex(i => i.ID == parcelID);
            Parcel p = new();
            p = Parcellist().ToList()[indexParcel];
            p.PickedUp = DateTime.Now;
            p.Status = StatusParcel.PICKUP;
            UpdateParcel(p);

            int indexDrone = Dronelist().ToList().FindIndex(i => i.ID == p.DroneId);
            Drone d = new();
            d = Dronelist().ToList()[indexDrone];
            if (d.IsActive == false)
                throw new DeleteException($"This drone can't pickup: {d.ID}");
            double Distance = Math.Sqrt(Math.Pow(d.Lattitude - FindCustomers(p.SenderId).Lattitude, 2) +
                Math.Pow(d.Longitude - FindCustomers(p.SenderId).Longitude, 2));
            d.Battery -= Distance * Power()[(int)d.Status];
            d.Longitude = FindCustomers(p.SenderId).Longitude;
            d.Lattitude = FindCustomers(p.SenderId).Lattitude;
            DroneOutCharge((int)d.ID);
            d.Status = Status.PICKUP;
            UpdateDrone(d);
        }

        /// <summary>
        /// The DeliverdParcel.
        /// </summary>
        /// <param name="parcelID">The parcelID<see cref="int"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliverdParcel(int parcelID)
        {
            int indexParcel = Parcellist().ToList().FindIndex(i => i.ID == parcelID);
            Parcel p = new();
            p = Parcellist().ToList()[indexParcel];
            p.Deliverd = DateTime.Now;
            p.DroneId = 0;
            p.Status = StatusParcel.DELIVERD;
            UpdateParcel(p);

            int indexDrone = Dronelist().ToList().FindIndex(i => i.ID == p.DroneId);
            Drone d = new();
            d = Dronelist().ToList()[indexDrone];
            if (d.IsActive == false)
                throw new DeleteException($"This drone can't deliver: {d.ID}");
            double Distance = Math.Sqrt(Math.Pow(FindCustomers(p.TargetId).Lattitude - FindCustomers(p.SenderId).Lattitude, 2) +
                Math.Pow(FindCustomers(p.TargetId).Longitude - FindCustomers(p.SenderId).Longitude, 2));
            d.Battery -= Distance * Power()[(int)d.Status];
            d.Longitude = FindCustomers(p.TargetId).Longitude;
            d.Lattitude = FindCustomers(p.TargetId).Lattitude;
            d.Status = Status.CREAT;
            d.haveParcel = false;
            UpdateDrone(d);
        }

        /// <summary>
        /// The DroneToCharge.
        /// </summary>
        /// <param name="droneID">The droneID<see cref="int"/>.</param>
        /// <param name="stationID">The stationID<see cref="int"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneToCharge(int droneID, int stationID)
        {

            int indexS = Stationlist().ToList().FindIndex(i => i.ID == stationID);
            if (Stationlist().ToList()[indexS].ChargeSlots <= 0)
                throw new ThereAreNoRoomException("There is no more room to load another Drone");
            int indexD = Dronelist().ToList().FindIndex(i => i.ID == droneID);
            if (Dronelist().ToList()[indexD].Status != Status.CREAT)
                throw new DroneInMiddleActionException("The drone is in the middle of the action");

            Station s = new();
            s = Stationlist().ToList()[indexS];
            if (s.IsActive == false)
                throw new DeleteException($"This station is deleted: {s.ID}");
            s.BusyChargeSlots++;
            UpdateStation(s);

            Drone d = new();
            d = Dronelist().ToList()[indexD];
            if (d.IsActive == false)
                throw new DeleteException($"This drone can't send to charge: {d.ID}");
            d.Status = Status.MAINTENANCE;
            double i = Power()[((int)d.Weight + 1) % 4];
            i *= Math.Sqrt(Math.Pow(d.Lattitude - s.Lattitude, 2) + Math.Pow(d.Longitude - s.Longitude, 2));
            d.Battery = Math.Ceiling(i);
            d.Lattitude = s.Lattitude;
            d.Longitude = s.Longitude;
            UpdateDrone(d);

            AddDroneCharge(droneID, stationID);
        }

        /// <summary>
        /// The DroneOutCharge.
        /// </summary>
        /// <param name="droneID">The droneID<see cref="int"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneOutCharge(int droneID)
        {

            int index = DroneChargelist().ToList().FindIndex(i => i.DroneId == droneID);
            if (index != -1)
            {
                index = Dronelist().ToList().FindIndex(i => i.ID == droneID);
                if (Dronelist().ToList()[index].Status != Status.MAINTENANCE)
                    throw new DroneNotChargingException("The drone is not charging at any station");
                Drone d = new();
                d = Dronelist().ToList()[index];
                d.Battery = 100;
                d.Status = Status.CREAT;
                UpdateDrone(d);

                index = DroneChargelist().ToList().FindIndex(i => i.DroneId == droneID);
                int indexStation = Stationlist().ToList().FindIndex(i => i.ID == DroneChargelist().ToList()[index].StationId);
                var a = DroneChargelist().ToList();
                a.RemoveAt(index);
                XMLTools.SaveListToXMLSerializer(a, DroneChargesPath);

                Station s = new();
                s = Stationlist().ToList()[indexStation];
                s.BusyChargeSlots -= 1;
                UpdateStation(s);
            }
        }

        /// <summary>
        /// The DroneOutCharge.
        /// </summary>
        /// <param name="droneID">The droneID<see cref="int"/>.</param>
        /// <param name="time">The time<see cref="double"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneOutCharge(int droneID, double time)
        {

            int index = DroneChargelist().ToList().FindIndex(i => i.DroneId == droneID);
            if (index != -1)
            {
                index = Dronelist().ToList().FindIndex(i => i.ID == droneID);
                if (Dronelist().ToList()[index].Status != Status.MAINTENANCE)
                    throw new DroneNotChargingException("The drone is not charging at any station");
                Drone d = new();
                d = Dronelist().ToList()[index];
                d.Battery = Power()[4] * time / 60;
                if (d.Battery > 100)
                    d.Battery = 100;
                d.Status = Status.CREAT;
                UpdateDrone(d);

                index = DroneChargelist().ToList().FindIndex(i => i.DroneId == droneID);
                int indexStation = Stationlist().ToList().FindIndex(i => i.ID == DroneChargelist().ToList()[index].StationId);
                var a = DroneChargelist().ToList();
                a.RemoveAt(index);
                XMLTools.SaveListToXMLSerializer(a, DroneChargesPath);

                Station s = new();
                s = Stationlist().ToList()[indexStation];
                s.BusyChargeSlots -= 1;
                UpdateStation(s);
            }
        }

        /// <summary>
        /// The FindStation.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Station"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station FindStation(int id)
        {
            return Stationlist().ToList().FindAll(i => i.ID == id && i.IsActive).FirstOrDefault();
        }

        /// <summary>
        /// The FindDrone.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Drone"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone FindDrone(int id)
        {
            return Dronelist().ToList().FindAll(i => i.ID == id && i.IsActive).FirstOrDefault();
        }

        /// <summary>
        /// The FindCustomers.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Customer"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer FindCustomers(int id)
        {
            return Customerlist().ToList().FindAll(i => i.ID == id && i.IsActive).FirstOrDefault();
        }

        /// <summary>
        /// The FindParcel.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Parcel"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel FindParcel(int id)
        {
            return Parcellist().ToList().FindAll(i => i.ID == id && i.IsActive).FirstOrDefault();
        }

        /// <summary>
        /// The Stationlist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Station}"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> Stationlist()
        {
            return XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
        }

        /// <summary>
        /// The Customerlist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Customer}"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> Customerlist()
        {
            return from Customer in LoadCustomersFromXML(LoadXml(CustomersPath))
                   where Customer.IsActive
                   select new Customer
                   {
                       IsActive = Customer.IsActive,
                       ID = Customer.ID,
                       Longitude = Customer.Longitude,
                       Lattitude = Customer.Lattitude,
                       CustomerName = Customer.CustomerName,
                       Phone = Customer.Phone,
                       Password = Customer.Password,
                       Email = Customer.Email,
                   };
        }

        /// <summary>
        /// The Parcellist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Parcel}"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> Parcellist()
        {
            return XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
        }

        /// <summary>
        /// The Dronelist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Drone}"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> Dronelist()
        {
            return XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);
        }

        /// <summary>
        /// The ParcelNotAssociatedList.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Parcel}"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> ParcelNotAssociatedList()
        {
            return from Parcel in Parcellist()
                   where Parcel.DroneId == 0 || Parcel.DroneId == 0 && Parcel.IsActive == true
                   select Parcel;
        }

        /// <summary>
        /// The Freechargeslotslist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Station}"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> Freechargeslotslist()
        {
            return from Station in Stationlist()
                   where Station.ChargeSlots - Station.BusyChargeSlots > 0 && Station.IsActive == true
                   select Station;
        }

        /// <summary>
        /// The DroneChargelist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{DroneCharge}"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> DroneChargelist()
        {
            return XMLTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargesPath);
        }

        /// <summary>
        /// The DeleteParcel.
        /// </summary>
        /// <param name="parcel">The parcel<see cref="Parcel"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(Parcel parcel)
        {
            parcel.IsActive = false;
            UpdateParcel(parcel);
        }

        /// <summary>
        /// The DeleteStation.
        /// </summary>
        /// <param name="station">The station<see cref="Station"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(Station station)
        {
            station.IsActive = false;
            UpdateStation(station);
        }

        /// <summary>
        /// The DeleteDrone.
        /// </summary>
        /// <param name="drone">The drone<see cref="Drone"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(Drone drone)
        {
            drone.IsActive = false;
            UpdateDrone(drone);
        }

        /// <summary>
        /// The DeleteCustomer.
        /// </summary>
        /// <param name="customer">The customer<see cref="Customer"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(Customer customer)
        {

            XElement CustomersRoot = LoadXml(CustomersPath);
            var Customer = LoadCustomersFromXML(CustomersRoot).ToList();
            int index = Customer.FindIndex(i => (i.ID == customer.ID));
            if (index == -1)
                throw new DoesNotExistException($"Drone does not exist");
            customer.IsActive = false;
            Customer[index] = customer;
            Customer.SaveToXml(CustomersPath, CustomersRoot.Name.ToString());
        }

        /// <summary>
        /// xml stuff.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <returns>xml stuff.</returns>
        internal static XElement LoadXml(string path)
        {
            try { return XElement.Load(dir + path); }
            catch (Exception ex)
            {
                throw new XmlLoadException($"Could not find '{path}'" + $" Please make sure that file is exist.", ex);
            }
        }

        /// <summary>
        /// The LoadCustomersFromXML.
        /// </summary>
        /// <param name="CustomersRoot">The CustomersRoot<see cref="XElement"/>.</param>
        /// <returns>The <see cref="IEnumerable{Customer}"/>.</returns>
        internal static IEnumerable<Customer> LoadCustomersFromXML(XElement CustomersRoot)
        {
            List<Customer> Customers = new();
            foreach (var Customer in CustomersRoot.Elements())
            {
                Customer c = new();
                c.IsActive = bool.Parse(Customer.Element("IsActive").Value);
                c.ID = int.Parse(Customer.Element("ID").Value);
                c.CustomerName = (Customer.Element("CustomerName").Value);
                c.Phone = (Customer.Element("Phone").Value);
                c.Longitude = double.Parse(Customer.Element("Longitude").Value);
                c.Lattitude = double.Parse(Customer.Element("Lattitude").Value);
                c.Email = Customer.Element("Email").Value;
                c.Password = Customer.Element("Password").Value;
                Customers.Add(c);
            }
            return Customers;
        }
    }
}
