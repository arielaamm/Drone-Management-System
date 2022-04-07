using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Drone in DAL
    /// </summary>
    public struct Drone
    {
        public bool IsActive { set; get; }
        public bool haveParcel { set; get; }
        public int? ID { get; set; }
        public Model Model { set; get;}
        public Weight Weight { set; get; }
        public Status Status { set; get; }
        public double Battery { set; get; }
        public double Longitude { set; get; }
        public double Lattitude { set; get; }

    } 
}
