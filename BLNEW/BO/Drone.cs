using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO
{
    /// <summary>
    /// Drone in BL
    /// </summary>
    public class Drone
    {
        public bool IsActive { set; get; }
        public bool HaveParcel { set; get; }
        public int ?ID { get; set; }
        public Model Model { set; get; }
        public Weight Weight { set; get; }
        public Status Status { set; get; }
        public double Battery { set; get; }
        public Location Position { set; get; }
        public ParcelTransactioning Parcel { set; get; }
        //done
    }
}
