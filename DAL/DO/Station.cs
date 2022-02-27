using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DO
{
    /// <summary>
    /// Station in DAL
    /// </summary>
    public struct Station
    {
        public int ? ID { set; get; }
        public string StationName { set; get; }
        public double Longitude { set; get; }
        public double Lattitude { set; get; }
        public int ChargeSlots { set; get; }
        public int BusyChargeSlots { set; get; }
    }
}
