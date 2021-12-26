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
            //לעבוד על הבנאי

        }
        public void AddStation(int id, string name, Location location, int ChargeSlots)
        {

        }
        public void AddDrone(int id, string name, BO.WEIGHT weight,int IDStarting)
        {

        }
    }

}