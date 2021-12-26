using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using IBL;
using IDAL;
using IDAL.DO;
public class LocationToInput
{
    public double longitude { set; get; }
    public double lattitude { set; get; }
}
namespace BL
{
    public class BL : IBL.IBL
    {
        public BL()
        {
            IDal dal;
            dal = new DalObject();
            IEnumerable<IDAL.DO.Drone> doDrone = (List<IDAL.DO.Drone>)dal.Dronelist();
            IEnumerable<IDAL.DO.Parcel> doParcel = (List<IDAL.DO.Parcel>)dal.Parcellist();
            //int ChargePerHour = DataSource.ChargePerHour;
            foreach (var item in doParcel)
            {
                if ((item.DroneId != 0) & (item.Requested > item.Scheduled) && (item.Requested != DateTime.MinValue))
                {
                    Drone BlDrone = dal.FindDrone(item.DroneId);
                    BlDrone.Status = (STATUS)2;

                }
            }

        }
        public void AddStation(int id, string name, double longitude, double lattitude, int ChargeSlots)
        {

        }
        public void AddDrone(int id, string name, BO.WEIGHT weight,int IDStarting)
        {

        }
    }

}