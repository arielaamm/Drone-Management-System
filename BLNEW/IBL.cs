using IBL.BO;
using System;
using System.Collections.Generic;

namespace IBL
{
    public interface IBL
    {
        void AddStation(int id, string name, Location location, int ChargeSlots);
        void AddDrone(int id, string name, WEIGHT weight, int IDStarting);
        void AddCustomer(int id, string name, string PhoneNumber, Location location);
        void AddParcel(int SenderId, int TargetId, WEIGHT weight, PRIORITY Priority);
        void UpdateDrone(int id, string name);
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
        IEnumerable<Station> stations();
        IEnumerable<Drone> drones();
        IEnumerable<Parcel> parcels();
        IEnumerable<Customer> customers();
        IEnumerable<Parcel> parcelsNotAssociated();
        IEnumerable<Station> freeChargeslots();
    }
}
