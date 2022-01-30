using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneToList
    {
        public int ID { get; set; }
        public string Model { set; get; }
        public Weight Weight { set; get; }
        public Status Status { set; get; }
        public double Buttery { set; get; }
        public Location current { set; get; }
        public int IdParcel  { set; get; }
        //done
    }
}
