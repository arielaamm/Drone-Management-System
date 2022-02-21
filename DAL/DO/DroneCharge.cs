using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Record fo drone charge in station in DAL
    /// </summary>
    public struct DroneCharge
    {
        public int? DroneId { set; get; }
        public int StationId { set; get; }

    }
}
