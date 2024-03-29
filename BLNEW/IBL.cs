﻿using BO;
using System;
using System.Collections.Generic;

namespace BlApi
{
    /// <summary>
    /// Defines the <see cref="IBL" />.
    /// </summary>
    public interface IBL
    {
        /// <summary>
        /// The AddStation.
        /// </summary>
        /// <param name="station">The station<see cref="Station"/>.</param>
        void AddStation(Station station);

        /// <summary>
        /// The AddDrone.
        /// </summary>
        /// <param name="drone">The drone<see cref="Drone"/>.</param>
        /// <param name="IDStarting">The IDStarting<see cref="int"/>.</param>
        void AddDrone(Drone drone, int IDStarting);

        /// <summary>
        /// The AddCustomer.
        /// </summary>
        /// <param name="customer">The customer<see cref="Customer"/>.</param>
        void AddCustomer(Customer customer);

        /// <summary>
        /// The AddParcel.
        /// </summary>
        /// <param name="parcel">The parcel<see cref="Parcel"/>.</param>
        void AddParcel(Parcel parcel);

        /// <summary>
        /// The UpdateParcel.
        /// </summary>
        /// <param name="parcel">The parcel<see cref="Parcel"/>.</param>
        void UpdateParcel(Parcel parcel);

        /// <summary>
        /// The UpdateDrone.
        /// </summary>
        /// <param name="drone">The drone<see cref="Drone"/>.</param>
        void UpdateDrone(Drone drone);

        /// <summary>
        /// The UpdateStation.
        /// </summary>
        /// <param name="station">The station<see cref="Station"/>.</param>
        void UpdateStation(Station station);

        /// <summary>
        /// The UpdateCustomer.
        /// </summary>
        /// <param name="customer">The customer<see cref="Customer"/>.</param>
        void UpdateCustomer(Customer customer);

        /// <summary>
        /// The DroneOutCharge.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        void DroneOutCharge(int id);

        /// <summary>
        /// The DroneToCharge.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <param name="b">The b<see cref="bool"/>.</param>
        void DroneToCharge(int id, bool b);

        /// <summary>
        /// The DroneOutCharge.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <param name="time">The time<see cref="double"/>.</param>
        void DroneOutCharge(int id, DateTime time);

        /// <summary>
        /// The AttacheDrone.
        /// </summary>
        /// <param name="DroneID">The DroneID<see cref="int"/>.</param>
        void AttacheDrone(int DroneID);

        /// <summary>
        /// The PickUpParcel.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        void PickUpParcel(int id);

        /// <summary>
        /// The Parcel delivery.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        void Parceldelivery(int id);

        /// <summary>
        /// The FindStation.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Station"/>.</returns>
        Station FindStation(int id);

        /// <summary>
        /// The FindDrone.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Drone"/>.</returns>
        Drone FindDrone(int id);

        /// <summary>
        /// The Find parcel.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Parcel"/>.</returns>
        Parcel Findparcel(int id);

        /// <summary>
        /// The Find customer.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Customer"/>.</returns>
        Customer Findcustomer(int id);

        /// <summary>
        /// The Stations.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{StationToList}"/>.</returns>
        IEnumerable<StationToList> Stations();

        /// <summary>
        /// The Drones.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{DroneToList}"/>.</returns>
        IEnumerable<DroneToList> Drones();

        /// <summary>
        /// The Parcels.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{ParcelToList}"/>.</returns>
        IEnumerable<ParcelToList> Parcels();

        /// <summary>
        /// The Customers.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{CustomerToList}"/>.</returns>
        IEnumerable<CustomerToList> Customers();

        /// <summary>
        /// The ParcelsNotAssociated.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{ParcelToList}"/>.</returns>
        IEnumerable<ParcelToList> ParcelsNotAssociated();

        /// <summary>
        /// The FreeChargeslots.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{StationToList}"/>.</returns>
        IEnumerable<StationToList> FreeChargeslots();

        /// <summary>
        /// The DeleteParcel.
        /// </summary>
        /// <param name="parcel">The parcel<see cref="Parcel"/>.</param>
        public void DeleteParcel(Parcel parcel);

        /// <summary>
        /// The DeleteStation.
        /// </summary>
        /// <param name="station">The station<see cref="Station"/>.</param>
        public void DeleteStation(Station station);

        /// <summary>
        /// The DeleteCustomer.
        /// </summary>
        /// <param name="customer">The customer<see cref="Customer"/>.</param>
        public void DeleteCustomer(Customer customer);

        /// <summary>
        /// The DeleteDrone.
        /// </summary>
        /// <param name="drone">The drone<see cref="Drone"/>.</param>
        public void DeleteDrone(Drone drone);

        /// <summary>
        /// The Uploader.
        /// </summary>
        /// <param name="droneId">The droneId<see cref="int"/>.</param>
        /// <param name="display">The display<see cref="Action"/>.</param>
        /// <param name="stopCheck">The stopCheck<see cref="Func{bool}"/>.</param>
        public void Uploader(int droneId, Action display, Func<bool> stopCheck);
    }
}
