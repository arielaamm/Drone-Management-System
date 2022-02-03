using IBL.BO;
using System;
using System.Collections.Generic;

namespace IBL
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
        //צריך לחשוב האם כדי להפוך את הבאים גם לצורה של השאר של קבלת ישויות 
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
