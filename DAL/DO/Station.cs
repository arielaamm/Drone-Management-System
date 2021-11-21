using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IDAL.DO
{
    struct Station
    {
        public int ID { set; get; }
        public string StationName { set; get; }
        public double Longitude { set; get; }
        public double Lattitude { set; get; }
        public int ChargeSlots { set; get; }
    }
}
