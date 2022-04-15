namespace BO
{
    /// <summary>
    /// Drone In Parcel
    /// sub object.
    /// </summary>
    public class DroneInParcel
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the Battery.
        /// </summary>
        public double Battery { set; get; }

        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        public Location Position { set; get; }
    }
}
