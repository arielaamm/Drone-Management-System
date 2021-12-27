using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using IBL;
using IDAL;
using IDAL.DO;
using BL.BO;

namespace BL
{
    public class BL : IBL.IBL
    {
        public BL()
        {
            IDal dal;
            dal = new DalObject();
            IEnumerable<IDAL.DO.Drone> doDrone = (List<IDAL.DO.Drone>)dal.Dronelist();
            //לעבוד על הבנאי
            
        }
        //add functaions:
        //---------------------------------------------------------------------------------
        public void AddStation(int id, string name, Location location, int ChargeSlots)
        {

        }
        public void AddDrone(int id, string name, BO.WEIGHT weight,int IDStarting)
        {

        }
        public void AddCustomer(int id, string name, int PhoneNumber, Location location)
        {

        }
        public void AddParcel(int SenderId, int TargetId, BO.WEIGHT weight, BO.PRIORITY Priority)
        {

        }
        //---------------------------------------------------------------------------------
        //updating functions:
        //---------------------------------------------------------------------------------
        public void UpdateDrone(int id, string name)//moudle???????
        {

        }
        public void UpdateStation(int id, int chargeslots)
        {

        }
        public void UpdateStation(int id, string name)
        {

        }
        public void UpdateCustomer(int id, string NewName)
        {

        }
        public void UpdateCustomer(int id, int NewPhoneNumber)
        {

        }
        //---------------------------------------------------------------------------------
        //charging functions:
        //---------------------------------------------------------------------------------
      //  ......
        //---------------------------------------------------------------------------------
        //pick-up functions:
        //---------------------------------------------------------------------------------




    }


}