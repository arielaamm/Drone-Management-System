
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
{
    public struct Drone
    {

        public int ID { get; set; }
        public string Model { set; get;}
        public WEIGHT Weight { set; get; }
        public STATUS Status { set; get; }
        public double Buttery { set; get; } 
    } 
}
