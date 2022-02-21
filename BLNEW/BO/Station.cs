using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Station in BL
    /// </summary>
    public class Station
    {
        public int ID { set; get; }
        public string StationName { set; get; }
        public Location Position { set; get; }
        public int FreeChargeSlots { set; get; }
        public List<DroneCharging> DroneChargingInStation { set; get; }
    }
}
