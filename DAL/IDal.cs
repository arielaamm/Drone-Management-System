using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IDal //מועלה
    {
        double[] Power();
        void AddStation(DO.Station s);
        void AddDrone(DO.Drone d);
        void AddCustomer(DO.Customer c);
        void AddParcel(DO.Parcel parcel);
        void AddDroneCharge(DO.DroneCharge d);
        void AddDroneCharge(int DroneId, int StationId);
        public void UpdateDrone(DO.Drone drone);
        public void UpdateStation(DO.Station station);
        public void UpdateParcel(DO.Parcel parcel);
        public void UpdateCustemer(DO.Customer customer);
        void AttacheDrone(int parcelID);
        void PickParcel(int parcelID);
        void ParcelToCustomer(int parcelID);
        void DroneToCharge(int droneID, int stationID);
        void DroneOutCharge(int droneID);
        DO.Station FindStation(int id);
        DO.Drone FindDrone(int id);
        DO.Customer FindCustomers(int id);
        DO.Parcel FindParcel(int id);
        IEnumerable<DO.Station> Stationlist();
        IEnumerable<DO.Customer> Customerlist();
        IEnumerable<DO.Parcel> Parcellist();
        IEnumerable<DO.Drone> Dronelist();
        IEnumerable<DO.Parcel> ParcelNotAssociatedList();
        IEnumerable<DO.Station> Freechargeslotslist();
        IEnumerable<DO.DroneCharge> DroneChargelist();

    }
}
