using System.Collections.Generic;

namespace DalApi
{
    /// <summary>
    /// Defines the <see cref="IDal" />.
    /// </summary>
    public interface IDal //מועלה
    {
        /// <summary>
        /// The Power.
        /// </summary>
        /// <returns>The <see cref="double[]"/>.</returns>
        double[] Power();

        /// <summary>
        /// The AddStation.
        /// </summary>
        /// <param name="s">The s<see cref="DO.Station"/>.</param>
        void AddStation(DO.Station s);

        /// <summary>
        /// The AddDrone.
        /// </summary>
        /// <param name="d">The d<see cref="DO.Drone"/>.</param>
        void AddDrone(DO.Drone d);

        /// <summary>
        /// The AddCustomer.
        /// </summary>
        /// <param name="c">The c<see cref="DO.Customer"/>.</param>
        void AddCustomer(DO.Customer c);

        /// <summary>
        /// The AddParcel.
        /// </summary>
        /// <param name="parcel">The parcel<see cref="DO.Parcel"/>.</param>
        void AddParcel(DO.Parcel parcel);

        /// <summary>
        /// The AddDroneCharge.
        /// </summary>
        /// <param name="d">The d<see cref="DO.DroneCharge"/>.</param>
        void AddDroneCharge(DO.DroneCharge d);

        /// <summary>
        /// The AddDroneCharge.
        /// </summary>
        /// <param name="DroneId">The DroneId<see cref="int"/>.</param>
        /// <param name="StationId">The StationId<see cref="int"/>.</param>
        void AddDroneCharge(int DroneId, int StationId);

        /// <summary>
        /// The UpdateDrone.
        /// </summary>
        /// <param name="drone">The drone<see cref="DO.Drone"/>.</param>
        public void UpdateDrone(DO.Drone drone);

        /// <summary>
        /// The UpdateStation.
        /// </summary>
        /// <param name="station">The station<see cref="DO.Station"/>.</param>
        public void UpdateStation(DO.Station station);

        /// <summary>
        /// The UpdateParcel.
        /// </summary>
        /// <param name="parcel">The parcel<see cref="DO.Parcel"/>.</param>
        public void UpdateParcel(DO.Parcel parcel);

        /// <summary>
        /// The UpdateCustomer.
        /// </summary>
        /// <param name="customer">The customer<see cref="DO.Customer"/>.</param>
        public void UpdateCustomer(DO.Customer customer);

        /// <summary>
        /// The AttacheDrone.
        /// </summary>
        /// <param name="parcelID">The parcelID<see cref="int"/>.</param>
        void AttacheDrone(int parcelID);

        /// <summary>
        /// The PickupParcel.
        /// </summary>
        /// <param name="parcelID">The parcelID<see cref="int"/>.</param>
        void PickupParcel(int parcelID);

        /// <summary>
        /// The DeliverdParcel.
        /// </summary>
        /// <param name="parcelID">The parcelID<see cref="int"/>.</param>
        void DeliverdParcel(int parcelID);

        /// <summary>
        /// The DroneToCharge.
        /// </summary>
        /// <param name="droneID">The droneID<see cref="int"/>.</param>
        /// <param name="stationID">The stationID<see cref="int"/>.</param>
        void DroneToCharge(int droneID, int stationID);

        /// <summary>
        /// The DroneOutCharge.
        /// </summary>
        /// <param name="droneID">The droneID<see cref="int"/>.</param>
        void DroneOutCharge(int droneID);

        /// <summary>
        /// The DroneOutCharge.
        /// </summary>
        /// <param name="droneID">The droneID<see cref="int"/>.</param>
        /// <param name="time">The time<see cref="double"/>.</param>
        void DroneOutCharge(int droneID, double time);

        /// <summary>
        /// The FindStation.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="DO.Station"/>.</returns>
        DO.Station FindStation(int id);

        /// <summary>
        /// The FindDrone.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="DO.Drone"/>.</returns>
        DO.Drone FindDrone(int id);

        /// <summary>
        /// The FindCustomers.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="DO.Customer"/>.</returns>
        DO.Customer FindCustomers(int id);

        /// <summary>
        /// The FindParcel.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="DO.Parcel"/>.</returns>
        DO.Parcel FindParcel(int id);

        /// <summary>
        /// The Stationlist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{DO.Station}"/>.</returns>
        IEnumerable<DO.Station> Stationlist();

        /// <summary>
        /// The Customerlist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{DO.Customer}"/>.</returns>
        IEnumerable<DO.Customer> Customerlist();

        /// <summary>
        /// The Parcellist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{DO.Parcel}"/>.</returns>
        IEnumerable<DO.Parcel> Parcellist();

        /// <summary>
        /// The Dronelist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{DO.Drone}"/>.</returns>
        IEnumerable<DO.Drone> Dronelist();

        /// <summary>
        /// The ParcelNotAssociatedList.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{DO.Parcel}"/>.</returns>
        IEnumerable<DO.Parcel> ParcelNotAssociatedList();

        /// <summary>
        /// The Freechargeslotslist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{DO.Station}"/>.</returns>
        IEnumerable<DO.Station> Freechargeslotslist();

        /// <summary>
        /// The DroneChargelist.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{DO.DroneCharge}"/>.</returns>
        IEnumerable<DO.DroneCharge> DroneChargelist();

        /// <summary>
        /// The DeleteParcel.
        /// </summary>
        /// <param name="parcel">The parcel<see cref="DO.Parcel"/>.</param>
        public void DeleteParcel(DO.Parcel parcel);

        /// <summary>
        /// The DeleteStation.
        /// </summary>
        /// <param name="station">The station<see cref="DO.Station"/>.</param>
        public void DeleteStation(DO.Station station);

        /// <summary>
        /// The DeleteCustomer.
        /// </summary>
        /// <param name="customer">The customer<see cref="DO.Customer"/>.</param>
        public void DeleteCustomer(DO.Customer customer);

        /// <summary>
        /// The DeleteDrone.
        /// </summary>
        /// <param name="drone">The drone<see cref="DO.Drone"/>.</param>
        public void DeleteDrone(DO.Drone drone);
    }
}
