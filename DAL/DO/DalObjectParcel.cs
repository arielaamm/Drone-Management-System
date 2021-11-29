using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using DAL;
namespace DAL
{
    namespace DalObject
    {
        class DalObjectParcel
        {
            public void adddrone()
            {
                Random rnd = new Random();
                int i = drones.Length;//תברר למה זה טעותתתת
                drones[i] = new Parcel();
                drones[i].Id = staticId;//ובטח מאותה סיבה גם זה יהיה אותה טעות
                drones[i].Model = "" + (rnd.Next(0, 100));
                drones[i].Buttery = 100;
            }

        }
    }
}
