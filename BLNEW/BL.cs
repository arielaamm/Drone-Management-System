using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using IBL;
using IDAL;
//using IDAL.DO;
using BL.BO;
using BLExceptions;

namespace BL
{
    public class BL : IBL.IBL
    {    
        IDal dal = new DalObject();
        public BL()
        {

            IEnumerable<IDAL.DO.Parcel> p = dal.Parcellist();
            List<IDAL.DO.Drone> d = DataSource.drones;
            IDAL.DO.Drone t = new IDAL.DO.Drone();
            foreach (IDAL.DO.Parcel i in p)
            {
                foreach (IDAL.DO.Drone item in d)
                {                
                    if ((i.Deliverd==DateTime.MinValue)&&(i.DroneId==item.ID))
                    {
                        t=item;
                        t.Status = (IDAL.DO.STATUS)1;
                        //לא עשיתי צריך לעשות
                    }
                }
            }
        }
        //add functaions:
        //---------------------------------------------------------------------------------
        public void AddStation(int id, string name, Location location, int ChargeSlots)
        {
            IDAL.DO.Station tempStation = new IDAL.DO.Station()
            {
                ID = id,
                StationName = name,
                Longitude = location.Longitude,
                Lattitude = location.Lattitude,
                ChargeSlots = ChargeSlots,
            };
            try
            {
                dal.AddStation(tempStation);
            }
            catch (Exception ex)
            {
                throw new AlreadyExistException(ex.Message,ex);
            }
        }
        public void AddDrone(int id, string name, BO.WEIGHT weight,int IDStarting)
        {
            try
            {



                Random random = new Random();
                IDAL.DO.Drone tempDrone = new IDAL.DO.Drone()
                {
                    ID = id,
                    Model = name,
                    Weight = (IDAL.DO.WEIGHT)weight,
                    Buttery = random.Next(20, 40),
                };
                IDAL.DO.DroneCharge tempDroneCharge = new IDAL.DO.DroneCharge()
                {
                    DroneId = id,
                    StationId = IDStarting,
                };

                    dal.AddDrone(tempDrone);
                    dal.AddDroneCharge(tempDroneCharge);
            }
            catch
            {

            }

        }
        public void AddCustomer(int id, string name, string PhoneNumber, Location location)
        {
            IDAL.DO.Customer tempCustomer = new IDAL.DO.Customer()
            {
                ID = id,
                CustomerName = name,
                Longitude = location.Longitude,
                Lattitude = location.Lattitude,
                Phone = PhoneNumber,
            };
            try
            {
                dal.AddCustomer(tempCustomer);
            }
            catch (Exception ex)
            {
                throw new AlreadyExistException(ex.Message, ex);
            }
        }
        public void AddParcel(int SenderId, int TargetId, BO.WEIGHT weight, BO.PRIORITY Priority)
        {
            IDAL.DO.Parcel tempParcel = new IDAL.DO.Parcel()
            {
                SenderId = SenderId,
                TargetId = TargetId,
                Weight = (IDAL.DO.WEIGHT)weight,
                Priority = (IDAL.DO.PRIORITY)Priority,
                Requested = DateTime.Now,
                Scheduled =DateTime.MinValue,
                PickedUp = DateTime.MinValue,
                Deliverd = DateTime.MinValue,
                DroneId = null,

            };
            try
            {
                dal.AddParcel(tempParcel);
            }
            catch (Exception ex)
            {
                throw new AlreadyExistException(ex.Message, ex);
            }
        }
        //---------------------------------------------------------------------------------
        //updating functions:
        //---------------------------------------------------------------------------------
        public void UpdateDrone(int id, string name)//moudle???????
        {

        }
#nullable enable
        public void UpdateStation(int id,string ? name ,int ? freechargeslots)
        {

        }

        public void UpdateCustomer(int id, string ?NewName,int ? NewPhoneNumber)
        {

        }
#nullable disable
        public void DroneToCharge(int id)
        {

        }
        public void DroneOutCharge(int id,TimeSpan time)
        {

        }
        public void AttacheDrone(int id)
        {

        }
        public void PickUpParcel(int id)
        {

        }
        public void Parceldelivery(int id)
        {

        }
        //-----------------------------------------------------------------------------
        //display func
        //------------------------------------------------------------------------------
        
        //-----------------------------------------------------------------------------------
        //listView func
        //-----------------------------------------------------------------------------------------


    }


}