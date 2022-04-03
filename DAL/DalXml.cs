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


namespace Dal
{
    public sealed class DalXml : DalApi.IDal
    {
        private DalXml()
        {

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
        internal static readonly string path = Directory.GetCurrentDirectory() + @"\XML Files\{0}.xml";
        internal static readonly string configPath = String.Format(path, "config");
        internal readonly string DronesPath = String.Format(path, "DroneToList");
        internal static readonly string stationsPath = String.Format(path, "StationToList");
        internal static readonly string ParcelsPath = String.Format(path, "ParcelToList");
        internal static readonly string CustomersPath = String.Format(path, "CustomerToList");
        internal static readonly string DroneChargesPath = String.Format(path, "DroneChargesPath");

        
        Serializer<Station> sX = new Serializer<Station>(stationsPath);
        Serializer<Customer> cX = new Serializer<Customer>(ParcelsPath);
        Serializer<Parcel> pX  = new Serializer<Parcel>(CustomersPath);
        Serializer<DroneCharge> dX = new Serializer<DroneCharge>(DroneChargesPath);

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
            return false;
        }
        bool AddParcelChecker(Parcel parcel)
        {
            if (Customerlist().ToList().FindIndex(i => i.ID == parcel.ID) == -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
            return false;
        }
        bool AddStationChecker(Station s)
        {
            if (Customerlist().ToList().FindIndex(i => i.ID == s.ID) == -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
            return false;
        }
        bool AddDroneChargeChecker(DroneCharge d)
        {
            if (DroneChargelist().ToList().FindIndex(i => i.DroneId == d.DroneId) == -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
            return false;
        }
        bool UpdateCustomerChecker(Customer c)
        {
            if (Customerlist().ToList().FindIndex(i => i.ID == c.ID) != -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
            return false;
        }
        bool UpdateParcelChecker(Parcel parcel)
        {
            if (Customerlist().ToList().FindIndex(i => i.ID == parcel.ID) != -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
            return false;
        }
        bool UpdateStationChecker(Station s)
        {
            if (Customerlist().ToList().FindIndex(i => i.ID == s.ID) != -1)
                return true;
            throw new NameIsUsedException($"An existing user name or email on the system.");
            return false;
        }
        #endregion

        public void AddCustomer(Customer c)
        {
            var anInstanceofMyClass = new Serializer<Customer>(CustomersPath);
            anInstanceofMyClass.Add(c, AddCustomerChecker);
          //  throw new NotImplementedException();
        }

        public void AddParcel(Parcel parcel)
        {
            var anInstanceofMyClass = new Serializer<Parcel>(ParcelsPath);
            anInstanceofMyClass.Add(parcel, AddParcelChecker);
            //  throw new NotImplementedException();
        }
        public void AddStation(Station s)
        {

            var anInstanceofMyClass = new Serializer<Station>(stationsPath);
            anInstanceofMyClass.Add(s, AddStationChecker);
            //  throw new NotImplementedException();
        }
        public void AddDroneCharge(DroneCharge d)
        {
            var anInstanceofMyClass = new Serializer<DroneCharge>(DroneChargesPath);
            anInstanceofMyClass.Add(d, AddDroneChargeChecker);
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
            var anInstanceofMyClass = new Serializer<Station>(stationsPath);
            anInstanceofMyClass.Update(station, UpdateStationChecker);
        }

        public void UpdateParcel(Parcel parcel)
        {
            var anInstanceofMyClass = new Serializer<Parcel>(ParcelsPath);
            anInstanceofMyClass.Update(parcel, UpdateParcelChecker);
        }

        public void UpdateCustomer(Customer customer)
        {
            var anInstanceofMyClass = new Serializer<Customer>(CustomersPath);
            anInstanceofMyClass.Update(customer, UpdateCustomerChecker);
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
            return sX.GetElementsFromXml();
            //throw new NotImplementedException();
        }

        public IEnumerable<Customer> Customerlist()
        {
            return cX.GetElementsFromXml();
           // throw new NotImplementedException();
        }

        public IEnumerable<Parcel> Parcellist()
        {
            return pX.GetElementsFromXml();
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
            return dX.GetElementsFromXml();
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

