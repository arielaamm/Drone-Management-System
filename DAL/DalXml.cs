using DAL;
using DALExceptionscs;
using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace DAL
{
    public sealed class DalXml : DalApi.IDal
    {
        private DalXml()
        {
            initialization();
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

        static int GetID(XElement root, int add)
        {
            string tempString = root.Element("staticId").Value;
            int tempInt = Convert.ToInt32(tempString);
            root.Element("staticId").Value = (tempInt + add).ToString();
            root.Save(@"XML Files\config.xml");
            return tempInt + add;
        }

        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public void initialization()
        {
            Random rnd = new Random();
            XElement parm1, parm2, parm3, parm4, parm5, parm6, parm7, parm8, parm9, parm10, parm11, parm12;

            #region DroneCharges
            XElement DroneChargesRoot;
            string DroneChargesPath = @"XML Files\DroneChargesXml.xml";
            if (!File.Exists(DroneChargesPath))
                CreateFiles(out DroneChargesRoot, DroneChargesPath, "DroneCharges");
            else
                LoadData(out DroneChargesRoot, DroneChargesPath);
            DalXml.DroneChargesPath = DroneChargesPath;
            #endregion

            #region Config
            XElement ConfigRoot;
            string ConfigPath = @"XML Files\config.xml";
            if (!File.Exists(ConfigPath))
            {
                CreateFiles(out ConfigRoot, ConfigPath, "config");
                XElement staticId, free, light, medium, heavy, chargePerHour;
                staticId = new XElement("staticId", 0);
                free = new XElement("free", 5);
                light = new XElement("light", 7);
                medium = new XElement("medium", 10);
                heavy = new XElement("heavy", 12);
                chargePerHour = new XElement("chargePerHour", 50);
                ConfigRoot.Add(staticId, free, medium, heavy, chargePerHour);
                ConfigRoot.Save(ConfigPath);
            }
            else
                LoadData(out ConfigRoot, ConfigPath);
            DalXml.ConfigPath = ConfigPath;
            #endregion

            # region Stations
            XElement StationsRoot;
            string StationsPath = @"XML Files\StationsXml.xml";
            if (!File.Exists(StationsPath))
            {
                CreateFiles(out StationsRoot, StationsPath, "Stations");
                for (int i = 0; i < 2; i++)
                {
                    parm1 = new XElement("IsActive", true);
                    parm2 = new XElement("Id", GetID(ConfigRoot, 1));
                    parm3 = new XElement("StationName", "Station" + i);
                    parm4 = new XElement("Longitude", GetRandomNumber(15, 0));
                    parm5 = new XElement("Lattitude", GetRandomNumber(17, 0));
                    parm6 = new XElement("ChargeSlots", 5);
                    parm7 = new XElement("BusyChargeSlots", 0);
                    StationsRoot.Add(new XElement("Station", parm1, parm2, parm3, parm4, parm5, parm6, parm7));
                    StationsRoot.Save(StationsPath);
                }
            }
            else
                LoadData(out StationsRoot, StationsPath);
            DalXml.StationsPath = StationsPath;
            #endregion

            #region Drones
            XElement DronesRoot;
            string DronesPath = @"XML Files\DronesXml.xml";
            if (!File.Exists(DronesPath))
            {
                CreateFiles(out DronesRoot, DronesPath, "Drones");
                for (int i = 0; i < 5; i++)
                {
                    int temp = rnd.Next(1, 3);
                    XElement element = (from p in StationsRoot.Elements()
                                        where Convert.ToInt32(p.Element("Id").Value) == temp
                                        && (Convert.ToBoolean(p.Element("IsActive").Value))
                                        select p).FirstOrDefault();

                    parm1 = new XElement("Id", GetID(ConfigRoot, 1));
                    parm2 = new XElement("IsActive", true);
                    parm3 = new XElement("Model", (Model)rnd.Next(0, 3));
                    parm4 = new XElement("Battery", 100);
                    parm5 = new XElement("HaveParcel", false);
                    parm6 = new XElement("Status");
                    parm7 = new XElement("Weight");
                    parm8 = new XElement("Longitude", element.Element("Longitude").Value);
                    parm9 = new XElement("Lattitude", element.Element("Lattitude").Value);
                    DronesRoot.Add(new XElement("Drone", parm1, parm2, parm3, parm4, parm5, parm6, parm7, parm8, parm9));
                    DronesRoot.Save(DronesPath);
                    #region DroneCharge
                    if (File.Exists(DroneChargesPath))
                    {
                        parm11 = new XElement("DroneId", GetID(ConfigRoot, 0));
                        parm12 = new XElement("StationId", element.Element("Id").Value);
                        string tempString = element.Element("BusyChargeSlots").Value;
                        int tempInt = Convert.ToInt32(tempString);
                        element.Element("BusyChargeSlots").Value = (tempInt + 1).ToString();
                        StationsRoot.Save(StationsPath);
                        DroneChargesRoot.Add(new XElement("DroneCharge", parm11, parm12));
                        DroneChargesRoot.Save(DroneChargesPath);
                    }
                    #endregion
                }
            }
            else
                LoadData(out DronesRoot, DronesPath);
            this.DronesPath = DronesPath;
            #endregion
            
            #region Customers
            XElement CustomersRoot;
            string CustomersPath = @"XML Files\CustomersXml.xml";   
            if (!File.Exists(CustomersPath))
            {
                CreateFiles(out CustomersRoot, CustomersPath, "Customers");
                for (int i = 0; i < 10; i++)
                {
                    parm1 = new XElement("Id", GetID(ConfigRoot, 1));
                    parm2 = new XElement("IsActive", true);
                    parm3 = new XElement("CustomerName", "Customer" + i);
                    parm4 = new XElement("Phone",05 + rnd.Next(10000000, 99999999));
                    parm5 = new XElement("Longitude", GetRandomNumber(15, 0));
                    parm6 = new XElement("Lattitude", GetRandomNumber(17, 0));
                    CustomersRoot.Add(new XElement("Customer", parm1, parm2, parm4, parm3, parm5, parm6));
                    CustomersRoot.Save(CustomersPath);
                }
            }
            else
            {
                LoadData(out CustomersRoot, CustomersPath);
            }
            DalXml.CustomersPath = CustomersPath;
            #endregion
            
            #region Parcels
            XElement ParcelsRoot;
            string ParcelsPath = @"XML Files\ParcelsXml.xml";
            if (!File.Exists(ParcelsPath))
            {
                int SenderID = rnd.Next(0, 10);
                CreateFiles(out ParcelsRoot, ParcelsPath, "Parcels");
                for (int i = 0; i < 10; i++)
                {
                    int sID = rnd.Next(0, 10);
                    int tID = rnd.Next(0, 10);
                    while (sID == tID)
                    {
                        tID = rnd.Next(0, 10);
                    }
                    int temp = rnd.Next(1, 3);
                    List<string> customerNameList = new();
                    XElement element = (from p in DronesRoot.Elements()
                                        where ((Convert.ToBoolean(p.Element("HaveParcel").Value) == false) 
                                        && (Convert.ToBoolean(p.Element("IsActive").Value)))
                                        select p).FirstOrDefault();
                    foreach (var item in CustomersRoot.Elements())
                    {
                        customerNameList.Add(item.Element("CustomerName").Value);
                    }
                    XElement Target = (from p in CustomersRoot.Elements()
                                        where Convert.ToBoolean(p.Element("IsActive").Value)
                                        select p).FirstOrDefault();
                    parm1 = new XElement("Id", GetID(ConfigRoot, 1));
                    parm2 = new XElement("IsActive", true);
                    parm3 = new XElement("SenderId", customerNameList[sID]);
                    parm4 = new XElement("TargetId", customerNameList[tID]);
                    parm5 = new XElement("Priority", (Priority)rnd.Next(0, 3));
                    parm6 = new XElement("Weight", (Weight)rnd.Next(0, 3));
                    parm7 = new XElement("Status" , StatusParcel.CREAT);
                    parm8 = new XElement("DroneId");
                    if (element != null)
                    {
                        parm8 = new XElement("DroneId", element.Element("Id").Value);
                        element.Element("HaveParcel").Value = (true).ToString();
                        DronesRoot.Save(DronesPath);
                    }
                    parm9 = new XElement("Requested", DateTime.Now.ToString("g"));
                    parm10 = new XElement("Scheduled");
                    parm11 = new XElement("PickedUp");
                    parm12 = new XElement("Deliverd");
                    ParcelsRoot.Add(new XElement("Parcel", parm1, parm2, parm3, parm4, parm5,
                        parm6, parm7, parm8, parm9, parm10, parm11, parm12));
                    ParcelsRoot.Save(ParcelsPath);
                }
            }
            else
            {
                LoadData(out ParcelsRoot, ParcelsPath);
            }
            DalXml.ParcelsPath = ParcelsPath;
            #endregion



            
        }

        private void CreateFiles(out XElement elementRoot ,string path , string type)
        {
            elementRoot = new XElement(type);
            elementRoot.Save(path);
        }
        private void LoadData(out XElement elementRoot, string path)
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
        //internal static string path = Directory.GetCurrentDirectory() + @"\XML Files\{0}.xml";
        internal static string ConfigPath;
        internal string DronesPath;
        internal static string StationsPath;
        internal static string ParcelsPath;
        internal static string CustomersPath;
        internal static string DroneChargesPath;


        

        /*
        #region Files
        public IEnumerable<string> GetPathes()
        {
            yield return configPath;
            yield return DronesPath;
            yield return stationsPath;
            yield return ParcelsPath;
            yield return CustomersPath;
        }
        #endregion
        */


        public double[] Power()
        {
            double[] a = {
                DAL.Config.free,
                DAL.Config.light,
                DAL.Config.medium,
                DAL.Config.heavy,
                DAL.Config.ChargePerHour };
            return a;
        }

        public void AddDrone(Drone d)
        {
            XElement DronesRoot = LoadXml(DronesPath);
            int index = LoadDronesFromXML(DronesRoot).ToList().FindIndex(i => (i.ID == d.ID));
            if (index != -1)
                throw new NameIsUsedException($"An existing name is on the system.");
            try
            {
                DronesRoot.Add(DAL.XmlHelper.BuildElementToXml(d));
                DronesRoot.Save(DronesPath);
            }
            catch (Exception ex) { throw new XmlWriteException(ex.Message, ex); }

        }

        // throw new NotImplementedException();
        #region checkers
        bool AddCustomerChecker(Customer c)
        {
            if (Customerlist().ToList().FindIndex(i => i.ID == c.ID) == -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
        }
        bool AddParcelChecker(Parcel parcel)
        {
            if (Customerlist().ToList().FindIndex(i => i.ID == parcel.ID) == -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
        }
        bool AddStationChecker(Station s)
        {
            if (Customerlist().ToList().FindIndex(i => i.ID == s.ID) == -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
        }
        bool AddDroneChargeChecker(DroneCharge d)
        {
            if (DroneChargelist().ToList().FindIndex(i => i.DroneId == d.DroneId) == -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
        }
        bool UpdateCustomerChecker(Customer c)
        {
            if (Customerlist().ToList().FindIndex(i => i.ID == c.ID) != -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
        }
        bool UpdateParcelChecker(Parcel parcel)
        {
            if (Customerlist().ToList().FindIndex(i => i.ID == parcel.ID) != -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
        }
        bool UpdateStationChecker(Station s)
        {
            if (Customerlist().ToList().FindIndex(i => i.ID == s.ID) != -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
        }
        #endregion

        public void AddCustomer(Customer c)
        {
            var anInstanceofMyClass = new XMLTools();
            anInstanceofMyClass.Add<Customer>(c, AddCustomerChecker, CustomersPath);
            
          //  throw new NotImplementedException();
        }

        public void AddParcel(Parcel parcel)
        {
            var anInstanceofMyClass = new XMLTools();
            anInstanceofMyClass.Add<Parcel>(parcel, AddParcelChecker, ParcelsPath);
            
            //  throw new NotImplementedException();
        }
        public void AddStation(Station s)
        {
            var anInstanceofMyClass = new XMLTools();
            anInstanceofMyClass.Add<Station>(s, AddStationChecker, StationsPath);
            //  throw new NotImplementedException();
        }
        public void AddDroneCharge(DroneCharge d)
        {

            var anInstanceofMyClass = new XMLTools();
            anInstanceofMyClass.Add<DroneCharge>(d, AddDroneChargeChecker, DroneChargesPath);
        }

        public void AddDroneCharge(int DroneId, int StationId)
        {
            DroneCharge d = new DroneCharge() { DroneId = DroneId, StationId = StationId };
            AddDroneCharge(d);
            throw new NotImplementedException();
        }

        public void UpdateDrone(Drone drone)
        {
            XElement DronesRoot = LoadXml(DronesPath);
            int index = LoadDronesFromXML(DronesRoot).ToList().FindIndex(i => (i.ID == drone.ID));
            if (index == -1)
                throw new NameIsUsedException($"not An existing name is on the system.");
            try
            {
                DeleteDrone(drone);
                DronesRoot.Add(DAL.XmlHelper.BuildElementToXml(drone));
                DronesRoot.Save(DronesPath);
            }
            catch (Exception ex) { throw new XmlWriteException(ex.Message, ex); }
        }

        public void UpdateStation(Station station)
        {
            var anInstanceofMyClass = new XMLTools();
            anInstanceofMyClass.Update<Station>(station, UpdateStationChecker, StationsPath);
        }

        public void UpdateParcel(Parcel parcel)
        {
            var anInstanceofMyClass = new XMLTools();
            anInstanceofMyClass.Update<Parcel>(parcel, UpdateParcelChecker, ParcelsPath);
        }

        public void UpdateCustomer(Customer customer)
        {
            var anInstanceofMyClass = new XMLTools(); 
            anInstanceofMyClass.Update<Customer>(customer, UpdateCustomerChecker, CustomersPath);
        }

        public void AttacheDrone(int parcelID)
        {
            throw new NotImplementedException();
        }

        public void PickupParcel(int parcelID)
        {
            throw new NotImplementedException();
        }

        public void DeliverdParcel(int parcelID)
        {
            throw new NotImplementedException();
        }

        public void DroneToCharge(int droneID, int stationID)
        {
            throw new NotImplementedException();
        }

        public void DroneOutCharge(int droneID)
        {
            throw new NotImplementedException();
        }

        public Station FindStation(int id)
        {
          return  Stationlist().ToList().FindAll(i => i.ID == id && i.IsActive).FirstOrDefault();
        }

        public Drone FindDrone(int id)
        {
            return Dronelist().ToList().FindAll(i => i.ID == id && i.IsActive).FirstOrDefault();
        }

        public Customer FindCustomers(int id)
        {
            return Customerlist().ToList().FindAll(i => i.ID == id && i.IsActive).FirstOrDefault();
        }

        public Parcel FindParcel(int id)
        {
            return Parcellist().ToList().FindAll(i => i.ID == id && i.IsActive).FirstOrDefault();
        }

        public IEnumerable<Station> Stationlist()
        {
            return XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            //throw new NotImplementedException();
        }

        public IEnumerable<Customer> Customerlist()
        {
            return XMLTools.LoadListFromXMLSerializer<Customer>(CustomersPath);
            // throw new NotImplementedException();
        }

        public IEnumerable<Parcel> Parcellist()
        {
            return XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            //   throw new NotImplementedException();
        }

        public IEnumerable<Drone> Dronelist()
        {
            return from Drone in LoadDronesFromXML(LoadXml(DronesPath))
                   where Drone.IsActive
                   select new Drone
                   {
                       IsActive = Drone.IsActive,
                       haveParcel = Drone.haveParcel,
                       ID = Drone.ID,
                       Model = Drone.Model,
                       Weight = Drone.Weight,
                       Status = Drone.Status,
                       Battery = Drone.Battery,
                       Longitude = Drone.Longitude,
                       Lattitude = Drone.Lattitude,
                   };
            // throw new NotImplementedException();
        }

        public IEnumerable<Parcel> ParcelNotAssociatedList()
        {
            return from Parcel in Parcellist()
                   where Parcel.DroneId == 0 || Parcel.DroneId == 0 && Parcel.IsActive == true
                   select Parcel;
            //throw new NotImplementedException();
        }

        public IEnumerable<Station> Freechargeslotslist()
        {
            return from Station in Stationlist()
                   where Station.ChargeSlots - Station.BusyChargeSlots > 0 && Station.IsActive == true
                   select Station;
            //throw new NotImplementedException();
        }

        public IEnumerable<DroneCharge> DroneChargelist()
        {
            return XMLTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargesPath);
            //throw new NotImplementedException();
        }

        public void DeleteParcel(Parcel parcel)
        {
            parcel.IsActive = false;
            UpdateParcel(parcel);
        }

        public void DeleteStation(Station station)
        {
            station.IsActive = false;
            UpdateStation(station);
        }

        public void DeleteCustomer(Customer customer)
        {
            customer.IsActive = false;
            UpdateCustomer(customer);
        }

        public void DeleteDrone(Drone drone)
        {

            XElement DronesRoot = LoadXml(DronesPath);
            var drones = LoadDronesFromXML(DronesRoot).ToList();
            int index = drones.FindIndex(i => (i.ID == drone.ID));
            if (index == -1)
                throw new DoesNotExistException($"Drone does not exist");
            drone.IsActive = false;
            drones[index] = drone;
            drones.SaveToXml(DronesPath, DronesRoot.Name.ToString());
            //throw new NotImplementedException();
        }
        /// <summary>
        /// xml stuff
        /// </summary>
        /// <returns>xml stuff</returns>
        internal XElement LoadXml(string path)
        {
            try { return XElement.Load(path); }
            catch (Exception ex)
            {
                throw new XmlLoadException($"Could not find '{path}'" + $" Please make sure that file is exist.", ex);
            }
        }
        internal IEnumerable<Drone> LoadDronesFromXML(XElement DronesRoot)
        {
            List<Drone> Drones = new List<Drone>();
            foreach (var Drone in DronesRoot.Elements())
            {
                // try
                // {
                Drones.Add(new Drone
                {
                    IsActive = bool.Parse(Drone.Element("IsActive").Value),
                    haveParcel = bool.Parse(Drone.Element("haveParcel").Value),
                    ID = int.Parse(Drone.Element("ID").Value),
                    Model = Drone.Element("Model").Value,
                    Weight = (Weight)int.Parse((Drone.Element("Weight").Value)),
                    Status = (Status)int.Parse((Drone.Element("Status").Value)),
                    Battery = double.Parse(Drone.Element("Battery").Value),
                    Longitude = double.Parse(Drone.Element("Longitude").Value),
                    Lattitude = double.Parse(Drone.Element("Lattitude").Value),
                });
                //  }
                //  catch (ArgumentNullException ex) { throw new XmlParametersException("Argument is null", ex); }
                //  catch (ArgumentException ex) { throw new XmlParametersException("Argument is not valid!", ex); }
                //  catch (FormatException ex) { throw new XmlParametersException("The format does not match!", ex); }
                //  catch (OverflowException ex) { throw new XmlParametersException("Argument is not valid!", ex); }
                //  catch (Exception ex) { throw new AnErrorOccurredException(ex.Message, ex); }
            }
            return Drones;

        }


    }

}

