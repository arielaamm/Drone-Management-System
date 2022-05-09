using DALExceptionscs;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DAL
{
    /// <summary>
    /// Defines the <see cref="DalObject" />.
    /// </summary>
    public sealed class DalObject : DalApi.IDal
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="DalObject"/> class from being created.
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
        /// The AddStation.
        /// </summary>
        /// <param name="s">The s<see cref="Station"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station s)
        {
            int index = DataSource.stations.FindIndex(i => i.ID == s.ID);
            if (index != -1)
                throw new AlreadyExistException("Already exist in the system");
            Config.staticId++;
            s.IsActive = true;
            DataSource.stations.Add(s);
        }

        /// <summary>
        /// The AddDrone.
        /// </summary>
        /// <param name="d">The d<see cref="Customer"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone d)
        {
            int index = DataSource.drones.FindIndex(i => i.ID == d.ID);
            if (index != -1)
                throw new AlreadyExistException("Already exist in the system");
            Config.staticId++;
            d.IsActive = true;
            DataSource.drones.Add(d);
            index = DataSource.stations.FindIndex(i => i.ChargeSlots - i.BusyChargeSlots > 0);
            Station s = new();
            s = DataSource.stations[index];
            s.BusyChargeSlots++;
            DataSource.stations[index] = s;
            DroneCharge temp = new()
            {
                DroneId = d.ID,
                StationId = (int)s.ID,
                Insert = DateTime.Now,
            };
            AddDroneCharge(temp);
        }

        /// <summary>
        /// The AddCustomer.
        /// </summary>
        /// <param name="c">The c<see cref="Customer"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer c)
        {
            int index = DataSource.customers.FindIndex(i => i.ID == c.ID);
            if (index != -1)
                throw new AlreadyExistException("Already exist in the system");
            c.IsActive = true;
            Config.staticId++;
            DataSource.customers.Add(c);
        }

        /// <summary>
        /// The AddParcel.
        /// </summary>
        /// <param name="p">The p<see cref="Parcel"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(Parcel p)
        {
            int index = DataSource.parcels.FindIndex(i => i.ID == p.ID);
            if (index != -1)
                throw new AlreadyExistException("Already exist in the system");
            p.IsActive = true;
            DataSource.parcels.Add(p);
        }

        /// <summary>
        /// The AddDroneCharge.
        /// </summary>
        /// <param name="DroneId">The DroneId<see cref="int"/>.</param>
        /// <param name="StationId">The StationId<see cref="int"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(int DroneId, int StationId)
        {
            DroneCharge d = new()
            {
                DroneId = DroneId
            };
            int index = DataSource.droneCharges.FindIndex(i => i.DroneId == DroneId);
            if (index != -1)
                throw new AlreadyExistException("Already exist in the system");
            d.StationId = StationId;
            d.Insert = DateTime.Now;
            DataSource.droneCharges.Add(d);
        }

        /// <summary>
        /// The AddDroneCharge.
        /// </summary>
        /// <param name="d">The d<see cref="DroneCharge"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        /// <param name="drone">The drone<see cref="Customer"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(Drone drone)
        {
            int index = DataSource.drones.FindIndex(i => i.ID == drone.ID);
            DataSource.drones[index] = drone;
        }

        /// <summary>
        /// The UpdateStation.
        /// </summary>
        /// <param name="station">The station<see cref="Station"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(Parcel parcel)
        {
            int index = DataSource.parcels.FindIndex(i => i.ID == parcel.ID);
            DataSource.parcels[index] = parcel;
        }

        /// <summary>
        /// The UpdateCustemer.
        /// </summary>
        /// <param name="customer">The customer<see cref="Customer"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(Customer customer)
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AttacheDrone(int parcelID)
        {
            int indexDrone = Dronelist().ToList().FindIndex(i =>
                (i.Status == Status.FREE || i.Status == Status.MAINTENANCE)
                && i.haveParcel == false);
            Drone d = new();
            d = Dronelist().ToList()[indexDrone];
            if (d.IsActive == false)
                throw new DeleteException($"This drone can't attached: {d.ID}, he's offline");

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
        /// The PickParcel.
        /// </summary>
        /// <param name="parcelID">The parcelID<see cref="int"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PickupParcel(int parcelID)
        {
            int indexParcel = DataSource.parcels.FindIndex(i => i.ID == parcelID);
            Parcel p = new();
            p = DataSource.parcels[indexParcel];
            p.PickedUp = DateTime.Now;
            p.Status = StatusParcel.PICKUP;
            DataSource.parcels[indexParcel] = p;

            int indexDrone = DataSource.drones.FindIndex(i => i.ID == p.DroneId);
            Drone d = new();
            d = DataSource.drones[indexDrone];
            if (d.IsActive == false)
                throw new DeleteException($"This drone can't pickup: {d.ID}");
            double Distance = Math.Sqrt(Math.Pow(d.Lattitude - FindCustomers(p.SenderId).Lattitude, 2) +
                Math.Pow(d.Longitude - FindCustomers(p.SenderId).Longitude, 2));
            d.Battery -= Distance * Power()[(int)d.Status];
            d.Longitude = FindCustomers(p.SenderId).Longitude;
            d.Lattitude = FindCustomers(p.SenderId).Lattitude;
            DroneOutCharge((int)d.ID);
            d.Status = Status.PICKUP;
            DataSource.drones[indexDrone] = d;
        }

        /// <summary>
        /// The ParcelToCustomer.
        /// </summary>
        /// <param name="parcelID">The parcelID<see cref="int"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliverdParcel(int parcelID)
        {
            int indexParcel = DataSource.parcels.FindIndex(i => i.ID == parcelID);
            Parcel p = new();
            p = DataSource.parcels[indexParcel];
            int indexDrone = DataSource.drones.FindIndex(i => i.ID == p.DroneId);
            Drone d = new();
            d = DataSource.drones[indexDrone];
            if (d.IsActive == false)
                throw new DeleteException($"This drone can't deliver: {d.ID}");
            double Distance = Math.Sqrt(Math.Pow(FindCustomers(p.TargetId).Lattitude - FindCustomers(p.SenderId).Lattitude, 2) +
                Math.Pow(FindCustomers(p.TargetId).Lattitude - FindCustomers(p.SenderId).Longitude, 2));
            d.Battery -= Distance * Power()[(int)d.Status];
            d.Longitude = FindCustomers(p.TargetId).Longitude;
            d.Lattitude = FindCustomers(p.TargetId).Lattitude;
            d.Status = Status.FREE;
            d.haveParcel = false;
            DataSource.drones[indexDrone] = d;

            p.Deliverd = DateTime.Now;
            p.DroneId = 0;
            p.Status = StatusParcel.DELIVERD;
            DataSource.parcels[indexParcel] = p;

            
        }

        /// <summary>
        /// The DroneToCharge.
        /// </summary>
        /// <param name="droneID">The droneID<see cref="int"/>.</param>
        /// <param name="stationID">The stationID<see cref="int"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneToCharge(int droneID, int stationID)
        {

            int indexD = DataSource.drones.FindIndex(i => i.ID == droneID);
            if (DataSource.drones[indexD].Status != 0)
                throw new DroneInMiddleActionException("The drone is in the middle of the action");
            int indexS = DataSource.stations.FindIndex(i => i.ID == stationID);
            if (DataSource.stations[indexS].ChargeSlots > 0)
                throw new ThereAreNoRoomException("There is no more room to load another Drone");

            Station s = new();
            s = DataSource.stations[indexS];
            if (s.IsActive == false)
                throw new DeleteException($"This station is deleted: {s.ID}");
            s.BusyChargeSlots += 1;
            DataSource.stations[indexS] = s;

            Drone d = new();
            d = DataSource.drones[indexD];
            if (d.IsActive == false)
                throw new DeleteException($"This drone can't send to charge: {d.ID}");
            d.Status = Status.MAINTENANCE;
            double i = Power()[((int)d.Weight + 1) % 4];
            i *= Math.Sqrt(Math.Pow(d.Lattitude - s.Lattitude, 2) + Math.Pow(d.Longitude - s.Longitude, 2));
            d.Battery = Math.Ceiling(i);
            d.Lattitude = s.Lattitude;
            d.Longitude = s.Longitude;
            DataSource.drones[indexD] = d;

            AddDroneCharge(droneID, stationID);
        }

        /// <summary>
        /// The DroneOutCharge.
        /// </summary>
        /// <param name="droneID">The droneID<see cref="int"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneOutCharge(int droneID)
        {

            int index = DataSource.droneCharges.FindIndex(i => i.DroneId == droneID);
            if (index != -1)
            {
                index = DataSource.drones.FindIndex(i => i.ID == droneID);
                if (DataSource.drones[index].Status != Status.MAINTENANCE)
                    throw new DroneNotChargingException("The drone is not charging");
                Drone d = new();
                d = DataSource.drones[index];
                d.Battery = 100;
                d.Status = Status.FREE;
                DataSource.drones[index] = d;

                index = DataSource.droneCharges.FindIndex(i => i.DroneId == droneID);
                int indexStation = DataSource.stations.FindIndex(i => i.ID == DataSource.droneCharges[index].StationId);
                DataSource.droneCharges.RemoveAt(index);

                Station s = new();
                s = DataSource.stations[index];
                s.BusyChargeSlots -= 1;
                DataSource.stations[index] = s;
            }
        }

        /// <summary>
        /// The DroneOutCharge.
        /// </summary>
        /// <param name="droneID">The droneID<see cref="int"/>.</param>
        /// <param name="time">The time<see cref="double"/>.</param>
        public void DroneOutCharge(int droneID, DateTime time)
        {

            int index = DataSource.droneCharges.FindIndex(i => i.DroneId == droneID);
            if (index != -1)
            {
                index = DataSource.drones.FindIndex(i => i.ID == droneID);
                if (DataSource.drones[index].Status != Status.MAINTENANCE)
                    throw new DroneNotChargingException("The drone is not charging");
                Drone d = new();
                d = DataSource.drones[index];
                var t = time - DataSource.droneCharges.Find(i => i.DroneId == droneID).Insert;
                d.Battery = Power()[4] * t.TotalMinutes / 60;
                if (d.Battery > 100)
                {
                    d.Battery = 100;
                    d.Status = Status.FREE;//changed for simulator
                    DataSource.drones[index] = d;
                }
                index = DataSource.droneCharges.FindIndex(i => i.DroneId == droneID);
                int indexStation = DataSource.stations.FindIndex(i => i.ID == DataSource.droneCharges[index].StationId);
                DataSource.droneCharges.RemoveAt(index);

                Station s = new();
                s = DataSource.stations[index];
                s.BusyChargeSlots -= 1;
                DataSource.stations[index] = s;

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
            return DataSource.stations[DataSource.stations.FindIndex(i => i.ID == id && i.IsActive == true)];
        }

        /// <summary>
        /// The FindDrone.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Customer"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone FindDrone(int id)
        {
            return DataSource.drones[DataSource.drones.FindIndex(i => i.ID == id && i.IsActive == true)];
        }

        /// <summary>
        /// The FindCustomers.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Customer"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer FindCustomers(int id)
        {
            return DataSource.customers[DataSource.customers.FindIndex(i => i.ID == id && i.IsActive == true)];
        }

        /// <summary>
        /// The FindParcel.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Parcel"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel FindParcel(int id)
        {
            return DataSource.parcels[DataSource.parcels.FindIndex(i => i.ID == id && i.IsActive == true)];
        }

        /// <summary>
        /// The Stationlist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Station}"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> Stationlist() => from T in DataSource.stations
                                                     where T.IsActive == true
                                                     select T;

        /// <summary>
        /// The Customerlist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Customer}"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> Customerlist() => from T in DataSource.customers
                                                       where T.IsActive == true
                                                       select T;

        /// <summary>
        /// The Parcellist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Parcel}"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> Parcellist() => from T in DataSource.parcels
                                                   where T.IsActive == true
                                                   select T;

        /// <summary>
        /// The Dronelist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Drone}"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> Dronelist() => from T in DataSource.drones
                                                 where T.IsActive == true
                                                 select T;

        /// <summary>
        /// The DroneChargelist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{DroneCharge}"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> DroneChargelist() => DataSource.droneCharges;

        /// <summary>
        /// The ParcelNotAssociatedList.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Parcel}"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> ParcelNotAssociatedList()
        {
            return from Parcel in DataSource.parcels
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
            return from Station in DataSource.stations
                   where Station.ChargeSlots - Station.BusyChargeSlots > 0 && Station.IsActive == true
                   select Station;
        }

        /// <summary>
        /// The DeleteParcel.
        /// </summary>
        /// <param name="parcel">The parcel<see cref="Parcel"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(Parcel parcel)
        {
            parcel.IsActive = false;
            int index = DataSource.parcels.FindIndex(i => i.ID == parcel.ID);
            DataSource.parcels[index] = parcel;
        }

        /// <summary>
        /// The DeleteDrone.
        /// </summary>
        /// <param name="drone">The drone<see cref="Drone"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(Drone drone)
        {
            drone.IsActive = false;
            int index = DataSource.drones.FindIndex(i => i.ID == drone.ID);
            DataSource.drones[index] = drone;
        }

        /// <summary>
        /// The DeleteCustomer.
        /// </summary>
        /// <param name="customer">The customer<see cref="Customer"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(Customer customer)
        {
            customer.IsActive = false;
            int index = DataSource.customers.FindIndex(i => i.ID == customer.ID);
            DataSource.customers[index] = customer;
        }

        /// <summary>
        /// The DeleteStation.
        /// </summary>
        /// <param name="station">The station<see cref="Station"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(Station station)
        {
            station.IsActive = false;
            int index = DataSource.stations.FindIndex(i => i.ID == station.ID);
            DataSource.stations[index] = station;
        }
    }
}
