namespace BO
{
    /// <summary>
    /// Drone in BL.
    /// </summary>
    public class Drone
    {
        /// <summary>
        /// Gets or sets a value indicating whether IsActive.
        /// </summary>
        public bool IsActive { set; get; }

        /// <summary>
        /// Gets or sets a value indicating whether HaveParcel.
        /// </summary>
        public bool HaveParcel { set; get; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// Gets or sets the Model.
        /// </summary>
        public Model Model { set; get; }

        /// <summary>
        /// Gets or sets the Weight.
        /// </summary>
        public Weight Weight { set; get; }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public Status Status { set; get; }

        /// <summary>
        /// Gets or sets the Battery.
        /// </summary>
        public double Battery { set; get; }

        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        public Location Position { set; get; }

        /// <summary>
        /// Gets or sets the Parcel.
        /// </summary>
        public ParcelTransactioning Parcel { set; get; }
    }
}
