using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using IBL;
using IDAL;
using IDAL.DO;

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
    }

}
