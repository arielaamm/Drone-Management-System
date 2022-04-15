using System.Collections.Generic;

namespace BO
{
    /// <summary>
    /// Station in BL.
    /// </summary>
    public class Station
    {
        /// <summary>
        /// Gets or sets a value indicating whether IsActive.
        /// </summary>
        public bool IsActive { set; get; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// Gets or sets the StationName.
        /// </summary>
        public string StationName { set; get; }

        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        public Location Position { set; get; }

        /// <summary>
        /// Gets or sets the ChargeSlots.
        /// </summary>
        public int ChargeSlots { set; get; }

        /// <summary>
        /// Gets or sets the DroneChargingInStation.
        /// </summary>
        public List<DroneCharging> DroneChargingInStation { set; get; }
    }
}
