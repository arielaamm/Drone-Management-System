using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IDal
    {
        double[] power();
        void AddStation(string name, int num);
        void AddDrone(string name, DO.WEIGHT Weight, double Buttery);
        void AddCustomer(string name, string phone);
        void AddParcel(int SenderId, int TargetId, DO.WEIGHT Weight, DO.PRIORITY Priority, DateTime Requested);
        void AddDroneCharge(int DroneId, int StationId);
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

    }
}
