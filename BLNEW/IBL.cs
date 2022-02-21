using BO;
using System;
using System.Collections.Generic;

namespace BlApi
{
    public interface IBL
    {
        void AddStation(Station station);
        void AddDrone(Drone drone, int IDStarting);
        void AddCustomer(Customer customer);
        void AddParcel(Parcel parcel);
        void UpdateDrone(Drone drone);
        #nullable enable
        void UpdateStation(Station station);
        void UpdateCustomer(Customer customer);
        #nullable disable
        void DroneToCharge(int id);
        void DroneOutCharge(int id, double time);
        void AttacheDrone(int id);
        void PickUpParcel(int id);
        void Parceldelivery(int id);
        Station FindStation(int id);
        Drone FindDrone(int id);
        Parcel Findparcel(int id);
        Customer Findcustomer(int id);
        IEnumerable<StationToList> Stations();
        IEnumerable<DroneToList> Drones();
        IEnumerable<ParcelToList> Parcels();
        IEnumerable<CustomerToList> Customers();
        IEnumerable<ParcelToList> ParcelsNotAssociated();
        IEnumerable<StationToList> FreeChargeslots();
    }
}
