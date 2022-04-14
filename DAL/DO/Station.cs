using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Station in DAL.
    /// </summary>
    public struct Station
    {
        /// <summary>
        /// Gets or sets a value indicating whether IsActive.
        /// </summary>
        public bool IsActive { set; get; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int? ID { set; get; }

        /// <summary>
        /// Gets or sets the StationName.
        /// </summary>
        public string StationName { set; get; }

        /// <summary>
        /// Gets or sets the Longitude.
        /// </summary>
        public double Longitude { set; get; }

        /// <summary>
        /// Gets or sets the Lattitude.
        /// </summary>
        public double Lattitude { set; get; }

        /// <summary>
        /// Gets or sets the ChargeSlots.
        /// </summary>
        public int ChargeSlots { set; get; }

        /// <summary>
        /// Gets or sets the BusyChargeSlots.
        /// </summary>
        public int BusyChargeSlots { set; get; }
    }
}
