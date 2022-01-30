using IBL.BO;
using System;
using System.Collections.Generic;

namespace IBL
{
    public interface IBL
    {
        void AddStation(Station station);
        void AddDrone(Drone drone);
        void AddCustomer(Customer customer);
        void AddParcel(int SenderId, int TargetId, Weight weight, Priority priority);
        void UpdateDrone(Drone drone);
        #nullable enable
        void UpdateStation(int id, string? name, int TotalChargeslots);
        void UpdateCustomer(int id, string? NewName, string? NewPhoneNumber);
        #nullable disable
        void DroneToCharge(int id);
        void DroneOutCharge(int id, int time);
        void AttacheDrone(int id);
        void PickUpParcel(int id);
        void Parceldelivery(int id);
        Station FindStation(int id);
        Drone FindDrone(int id);
        Parcel Findparcel(int id);
        Customer Findcustomer(int id);
        IEnumerable<StationToList> stations();
        IEnumerable<DroneToList> Drones();
        IEnumerable<ParcelToList> parcels();
        IEnumerable<CustomerToList> customers();
        IEnumerable<ParcelToList> parcelsNotAssociated();
        IEnumerable<StationToList> freeChargeslots();
    }
}
