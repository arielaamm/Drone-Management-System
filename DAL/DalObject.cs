using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randon = System.Random;
using DALExceptionscs;
using System.Runtime.Serialization;

namespace DAL
{
    //static
    public sealed class DalObject : DalApi.IDal
    {
        private DalObject() { DataSource.Initialize(); }

        static DalObject instance = null;
        public static DalObject GetInstance()
        {
            if (instance == null)
                instance = new DalObject();
            return instance;
        }
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
        #region add (1)
        public void AddStation(Station s)
        {
            int index = DataSource.stations.FindIndex(i => i.ID == s.ID);
            if (index != -1)
                throw new AlreadyExistException("Already exist in the system");
            Config.staticId++;
            DataSource.stations.Add(s);
        }

        public void AddDrone(Drone d)
        {
            int index = DataSource.drones.FindIndex(i => i.ID == d.ID);
            if (index != -1)
                throw new AlreadyExistException ("Already exist in the system");    
            Config.staticId++;
            DataSource.drones.Add(d);
            index = DataSource.stations.FindIndex(i => i.ChargeSlots-i.BusyChargeSlots > 0);
            Station s = new();
            s = DataSource.stations[index];
            s.BusyChargeSlots++;
            DataSource.stations[index]=s;
            DroneCharge temp = new DroneCharge()
            {
                DroneId = d.ID,
                StationId = (int)s.ID,
            };
            AddDroneCharge(temp);
        }

        public void AddCustomer(Customer c) 
        {
            int index = DataSource.customers.FindIndex(i => i.ID == c.ID);
            if (index != -1)
                throw new AlreadyExistException("Already exist in the system");
            Config.staticId++;
            DataSource.customers.Add(c);
        }

        public void AddParcel(Parcel p)
        {
            p.ID = Config.staticId;
            int index = DataSource.parcels.FindIndex(i => i.ID == p.ID);
            if (index != -1)
                throw new AlreadyExistException("Already exist in the system");
            Config.staticId++;
            DataSource.parcels.Add(p);
        }

        public void AddDroneCharge(int DroneId, int StationId)
        {
            DroneCharge d = new DroneCharge();
            d.DroneId = DroneId;
            int index = DataSource.droneCharges.FindIndex(i => i.DroneId == DroneId);
            if (index != -1)
                throw new AlreadyExistException("Already exist in the system");
            d.StationId = StationId;
            DataSource.droneCharges.Add(d);
        }

        public void AddDroneCharge(DroneCharge d)
        {
            int index = DataSource.droneCharges.FindIndex(i => i.DroneId == d.DroneId);
            if (index != -1)
                throw new AlreadyExistException("Already exist in the system");
            DataSource.droneCharges.Add(d);
        }
        #endregion
        #region update (2)
        public void UpdateDrone(Drone drone) 
        {
            int index = DataSource.drones.FindIndex(i => i.ID==drone.ID);
            DataSource.drones[index] = drone;
        }

        public void UpdateStation(Station station)
        {
            if (DataSource.stations.TrueForAll(i=>i.StationName!=station.StationName))
                throw new NameIsUsedException($"This name {station.StationName} is used");
            int index = DataSource.stations.FindIndex(i => i.ID == station.ID);
            DataSource.stations[index] = station;
        }
        public void UpdateParcel(Parcel parcel)
        {
            int index = DataSource.parcels.FindIndex(i => i.ID == parcel.ID);
            DataSource.parcels[index] = parcel;
        }
        public void UpdateCustemer(Customer customer)
        {
            if (DataSource.customers.TrueForAll(i => i.CustomerName != customer.CustomerName))
                throw new NameIsUsedException($"This name {customer.CustomerName} is used");
            if (DataSource.customers.TrueForAll(i => i.Phone != customer.Phone))
                throw new PhoneIsUsedException($"This phone {customer.Phone} is used");
            int index = DataSource.customers.FindIndex(i => i.ID == customer.ID);
            DataSource.customers[index] = customer;
        }
        public void AttacheDrone(int parcelID)
        {
            int indexDrone = DataSource.drones.FindIndex(i => i.Status == Status.CREAT || i.Status == Status.CREAT);
            Drone d = new();
            d = DataSource.drones[indexDrone];
            d.Status = Status.BELONG;
            d.haveParcel = true;

            int indexParcel = DataSource.parcels.FindIndex(i => i.ID == parcelID);
            Parcel p = new();
            p = DataSource.parcels[indexParcel];
            p.DroneId = d.ID;
            p.Scheduled = DateTime.Now;
            DataSource.parcels[indexParcel] = p;
            DataSource.drones[indexDrone] = d;

        }

        public void PickParcel(int parcelID)
        {
            int indexParcel = DataSource.parcels.FindIndex(i => i.ID == parcelID);
            Parcel p = new();
            p = DataSource.parcels[indexParcel];
            p.PickedUp = DateTime.Now;
            DataSource.parcels[indexParcel] = p;

            int indexDrone = DataSource.drones.FindIndex(i => i.ID == p.DroneId);
            Drone d = new();
            d = DataSource.drones[indexDrone];
            double distans = Math.Sqrt(Math.Pow(d.Lattitude - FindCustomers(p.SenderId).Lattitude, 2) +
                Math.Pow(d.Longitude - FindCustomers(p.SenderId).Longitude, 2));
            d.Battery = d.Battery - distans * Power()[(int)d.Status];
            d.Longitude = FindCustomers(p.SenderId).Longitude;
            d.Lattitude = FindCustomers(p.SenderId).Lattitude;
            d.Status = Status.PICKUP;
            DataSource.drones[indexDrone] = d;
            //Parcel p = new();
            //foreach (var i in DataSource.parcels)
            //{
            //    if (i.ID == parcelID)
            //    {
            //        p = i;
            //        break;
            //    }
            //}
            //int keeper = 0;
            //foreach (var i in DataSource.drones)
            //{

            //    if (i.ID == p.DroneId)
            //    {
            //        p.PickedUp = DateTime.Now;
            //        keeper = (int)i.ID;
            //        break;
            //    }
            //}
            //Drone d = FindDrone(keeper);
            //d.Status = (Status)2;

        }

        public void ParcelToCustomer(int parcelID)
        {
            int indexParcel = DataSource.parcels.FindIndex(i => i.ID == parcelID);
            Parcel p = new();
            p = DataSource.parcels[indexParcel];
            p.Deliverd = DateTime.Now;
            DataSource.parcels[indexParcel] = p;
            int indexDrone = DataSource.drones.FindIndex(i => i.ID == p.DroneId);
            Drone d = new();
            d = DataSource.drones[indexDrone];
            double distans = Math.Sqrt(Math.Pow(FindCustomers(p.TargetId).Lattitude - FindCustomers(p.SenderId).Lattitude, 2) +
                Math.Pow(FindCustomers(p.TargetId).Lattitude - FindCustomers(p.SenderId).Longitude, 2));
            d.Battery = d.Battery - distans * Power()[(int)d.Status];
            d.Longitude = FindCustomers(p.TargetId).Longitude;
            d.Lattitude = FindCustomers(p.TargetId).Lattitude;
            d.Status = Status.CREAT;
            DataSource.drones[indexDrone] = d;
            //Parcel p = new();
            //foreach (var i in DataSource.parcels)
            //{
            //    if (i.ID == parcelID)
            //    {
            //        p = i;
            //        break;
            //    }
            //}
            //int keeper = 0;
            //foreach (var i in DataSource.customers)
            //{
            //    if (i.ID == p.TargetId)
            //    {
            //        p.Deliverd = DateTime.Now;
            //        keeper = (int)p.DroneId;
            //        break;
            //    }
            //}
            //Drone d = FindDrone(keeper);
            //d.Status = (Status)0;
        }

        public void DroneToCharge(int droneID, int stationID)
        {
            Drone d = new();
            Station s = new();
            int index = DataSource.drones.FindIndex(i => i.ID == droneID);
            if (DataSource.drones[index].Status!=0)
                throw new DroneInMiddleActionException("The drone is in the middle of the action");
            d = DataSource.drones[index];
            d.Status = Status.MAINTENANCE;
            DataSource.drones[index] = d;
            index = DataSource.stations.FindIndex(i => i.ID == stationID);
            if (DataSource.stations[index].ChargeSlots > 0)
                throw new ThereAreNoRoomException("There is no more room to load another Drone");
            s = DataSource.stations[index];
            s.BusyChargeSlots++;
            DataSource.stations[index] = s;
            AddDroneCharge(droneID, stationID);
        }

        public void DroneOutCharge(int droneID)
        {
            Drone d = new();
            Station s = new();
            int index = DataSource.drones.FindIndex(i => i.ID == droneID);
            if (DataSource.drones[index].Status != 0)
                throw new DroneNotChargingException("The drone is not charging");
            d = DataSource.drones[index];
            d.Status = Status.CREAT;
            DataSource.drones[index] = d;

            index = DataSource.droneCharges.FindIndex(i => i.DroneId == droneID);
            int indexStation = DataSource.droneCharges[index].StationId;
            DataSource.droneCharges.RemoveAt(index);

            s = DataSource.stations[index];
            s.BusyChargeSlots--;
            DataSource.stations[index] = s;
        }

        #endregion
        #region print(3)

        public Station FindStation(int id)
        {
            return DataSource.stations[DataSource.stations.FindIndex(i => i.ID == id)];
        }

        public Drone FindDrone(int id)
        {
            return DataSource.drones[DataSource.drones.FindIndex(i => i.ID == id)];
        }

        public Customer FindCustomers(int id)
        {
            return DataSource.customers[DataSource.customers.FindIndex(i => i.ID == id)];
        }

        public Parcel FindParcel(int id)
        {
            return DataSource.parcels[DataSource.parcels.FindIndex(i => i.ID == id)];
        }

        #endregion
        #region print lists (4)
        public IEnumerable<Station> Stationlist() => DataSource.stations;

        public IEnumerable<Customer> Customerlist() => DataSource.customers;

        public IEnumerable<Parcel> Parcellist() => DataSource.parcels;

        public IEnumerable<Drone> Dronelist() => DataSource.drones;

        public IEnumerable<DroneCharge> DroneChargelist() => DataSource.droneCharges;

        public IEnumerable<Parcel> ParcelNotAssociatedList()
        {
            return from Parcel in DataSource.parcels
                   where Parcel.DroneId == 0 || Parcel.DroneId == null
                   select Parcel;
        }

        public IEnumerable<Station> Freechargeslotslist()
        {
            return from Station in DataSource.stations
                   where Station.ChargeSlots - Station.BusyChargeSlots > 0
                   select Station;
        }
        #endregion

    }
}

