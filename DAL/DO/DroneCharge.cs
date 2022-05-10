using System;

namespace DO
{
    /// <summary>
    /// Record fo drone charge in station in DAL.
    /// </summary>
    public struct DroneCharge
    {
        /// <summary>
        /// Gets or sets the DroneId.
        /// </summary>
        public int? DroneId { set; get; }

        /// <summary>
        /// Gets or sets the StationId.
        /// </summary>
        public int StationId { set; get; }

        /// <summary>
        /// Gets or sets the Insert.
        /// </summary>
        public DateTime Insert { set; get; }
    }
}
