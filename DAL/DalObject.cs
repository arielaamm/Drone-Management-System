using DALExceptionscs;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Randon = System.Random;

namespace DAL
{
    public sealed class DalObject : DalApi.IDal
    {
        /// <summary>
        /// Prevents a default instance of the class from being created.
        /// </summary>
        private DalObject()
        {
            DataSource.Initialize();
        }

        /// <summary>
        /// Defines the instance.
        /// </summary>
        public static DalObject instance = null;

        /// <summary>
        /// The GetInstance.
        /// </summary>
        /// <returns>The <see cref="DalObject"/>.</returns>
        public static DalObject GetInstance()
        {
            if (instance == null)
                instance = new DalObject();
            return instance;
        }

        /// <summary>
        /// The Power.
        /// </summary>
        /// <returns>The <see cref="double[]"/>.</returns>
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

        /// <summary>
        /// The AddStation.
        /// </summary>
        /// <param name="s">The s<see cref="Station"/>.</param>
        public void AddStation(Station s)
        {
            int index = DataSource.stations.FindIndex(i => i.ID == s.ID);
            if (index != -1)
                throw new AlreadyExistException("Already exist in the system");
            Config.staticId++;
            DataSource.stations.Add(s);
        }

        /// <summary>
        /// The AddDrone.
        /// </summary>
        /// <param name="d">The d<see cref="Drone"/>.</param>
        public void AddDrone(Drone d)
        {
            int index = DataSource.drones.FindIndex(i => i.ID == d.ID);
            if (index != -1)
                throw new AlreadyExistException("Already exist in the system");
            Config.staticId++;
            DataSource.drones.Add(d);
            index = DataSource.stations.FindIndex(i => i.ChargeSlots - i.BusyChargeSlots > 0);
            Station s = new();
            s = DataSource.stations[index];
            s.BusyChargeSlots++;
            DataSource.stations[index] = s;
            DroneCharge temp = new DroneCharge()
            {
                DroneId = d.ID,
                StationId = (int)s.ID,
            };
            AddDroneCharge(temp);
        }

        /// <summary>
        /// The AddCustomer.
        /// </summary>
        /// <param name="c">The c<see cref="Customer"/>.</param>
        public void AddCustomer(Customer c)
        {
            int index = DataSource.customers.FindIndex(i => i.ID == c.ID);
            if (index != -1)
                throw new AlreadyExistException("Already exist in the system");
            Config.staticId++;
            DataSource.customers.Add(c);
        }

        /// <summary>
        /// The AddParcel.
        /// </summary>
        /// <param name="p">The p<see cref="Parcel"/>.</param>
        public void AddParcel(Parcel p)
        {
            int index = DataSource.parcels.FindIndex(i => i.ID == p.ID);
            if (index != -1)
                throw new AlreadyExistException("Already exist in the system");
            DataSource.parcels.Add(p);
        }

        /// <summary>
        /// The AddDroneCharge.
        /// </summary>
        /// <param name="DroneId">The DroneId<see cref="int"/>.</param>
        /// <param name="StationId">The StationId<see cref="int"/>.</param>
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

        /// <summary>
        /// The AddDroneCharge.
        /// </summary>
        /// <param name="d">The d<see cref="DroneCharge"/>.</param>
        public void AddDroneCharge(DroneCharge d)
        {
            int index = DataSource.droneCharges.FindIndex(i => i.DroneId == d.DroneId);
            if (index != -1)
                throw new AlreadyExistException("Already exist in the system");
            DataSource.droneCharges.Add(d);
        }

        /// <summary>
        /// The UpdateDrone.
        /// </summary>
        /// <param name="drone">The drone<see cref="Drone"/>.</param>
        public void UpdateDrone(Drone drone)
        {
            int index = DataSource.drones.FindIndex(i => i.ID == drone.ID);
            DataSource.drones[index] = drone;
        }

        /// <summary>
        /// The UpdateStation.
        /// </summary>
        /// <param name="station">The station<see cref="Station"/>.</param>
        public void UpdateStation(Station station)
        {
            if (DataSource.stations.TrueForAll(i => i.StationName != station.StationName))
                throw new NameIsUsedException($"This name {station.StationName} is used");
            int index = DataSource.stations.FindIndex(i => i.ID == station.ID);
            DataSource.stations[index] = station;
        }

        /// <summary>
        /// The UpdateParcel.
        /// </summary>
        /// <param name="parcel">The parcel<see cref="Parcel"/>.</param>
        public void UpdateParcel(Parcel parcel)
        {
            int index = DataSource.parcels.FindIndex(i => i.ID == parcel.ID);
            DataSource.parcels[index] = parcel;
        }

        /// <summary>
        /// The UpdateCustemer.
        /// </summary>
        /// <param name="customer">The customer<see cref="Customer"/>.</param>
        public void UpdateCustemer(Customer customer)
        {
            if (DataSource.customers.TrueForAll(i => i.CustomerName != customer.CustomerName))
                throw new NameIsUsedException($"This name {customer.CustomerName} is used");
            if (DataSource.customers.TrueForAll(i => i.Phone != customer.Phone))
                throw new PhoneIsUsedException($"This phone {customer.Phone} is used");
            int index = DataSource.customers.FindIndex(i => i.ID == customer.ID);
            DataSource.customers[index] = customer;
        }

        /// <summary>
        /// The AttacheDrone.
        /// </summary>
        /// <param name="parcelID">The parcelID<see cref="int"/>.</param>
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
            p.DroneId = (int)d.ID;
            p.Scheduled = DateTime.Now;
            DataSource.parcels[indexParcel] = p;
            DataSource.drones[indexDrone] = d;
        }

        /// <summary>
        /// The PickParcel.
        /// </summary>
        /// <param name="parcelID">The parcelID<see cref="int"/>.</param>
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
        }

        /// <summary>
        /// The ParcelToCustomer.
        /// </summary>
        /// <param name="parcelID">The parcelID<see cref="int"/>.</param>
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
        }

        /// <summary>
        /// The DroneToCharge.
        /// </summary>
        /// <param name="droneID">The droneID<see cref="int"/>.</param>
        /// <param name="stationID">The stationID<see cref="int"/>.</param>
        public void DroneToCharge(int droneID, int stationID)
        {
            Drone d = new();
            Station s = new();
            int index = DataSource.drones.FindIndex(i => i.ID == droneID);
            if (DataSource.drones[index].Status != 0)
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

        /// <summary>
        /// The DroneOutCharge.
        /// </summary>
        /// <param name="droneID">The droneID<see cref="int"/>.</param>
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

        /// <summary>
        /// The FindStation.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Station"/>.</returns>
        public Station FindStation(int id)
        {
            return DataSource.stations[DataSource.stations.FindIndex(i => i.ID == id)];
        }

        /// <summary>
        /// The FindDrone.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Drone"/>.</returns>
        public Drone FindDrone(int id)
        {
            return DataSource.drones[DataSource.drones.FindIndex(i => i.ID == id)];
        }

        /// <summary>
        /// The FindCustomers.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Customer"/>.</returns>
        public Customer FindCustomers(int id)
        {
            return DataSource.customers[DataSource.customers.FindIndex(i => i.ID == id)];
        }

        /// <summary>
        /// The FindParcel.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Parcel"/>.</returns>
        public Parcel FindParcel(int id)
        {
            return DataSource.parcels[DataSource.parcels.FindIndex(i => i.ID == id)];
        }

        /// <summary>
        /// The Stationlist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Station}"/>.</returns>
        public IEnumerable<Station> Stationlist() => DataSource.stations;

        /// <summary>
        /// The Customerlist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Customer}"/>.</returns>
        public IEnumerable<Customer> Customerlist() => DataSource.customers;

        /// <summary>
        /// The Parcellist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Parcel}"/>.</returns>
        public IEnumerable<Parcel> Parcellist() => DataSource.parcels;

        /// <summary>
        /// The Dronelist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Drone}"/>.</returns>
        public IEnumerable<Drone> Dronelist() => DataSource.drones;

        /// <summary>
        /// The DroneChargelist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{DroneCharge}"/>.</returns>
        public IEnumerable<DroneCharge> DroneChargelist() => DataSource.droneCharges;

        /// <summary>
        /// The ParcelNotAssociatedList.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Parcel}"/>.</returns>
        public IEnumerable<Parcel> ParcelNotAssociatedList()
        {
            return from Parcel in DataSource.parcels
                   where Parcel.DroneId == 0 || Parcel.DroneId == null
                   select Parcel;
        }

        /// <summary>
        /// The Freechargeslotslist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Station}"/>.</returns>
        public IEnumerable<Station> Freechargeslotslist()
        {
            return from Station in DataSource.stations
                   where Station.ChargeSlots - Station.BusyChargeSlots > 0
                   select Station;
        }
    }
}
