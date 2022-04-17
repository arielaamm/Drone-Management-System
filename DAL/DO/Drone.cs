namespace DO
{
    /// <summary>
    /// Drone in DAL.
    /// </summary>
    public struct Drone
    {
        /// <summary>
        /// Gets or sets a value indicating whether IsActive.
        /// </summary>
        public bool IsActive { set; get; }

        /// <summary>
        /// Gets or sets a value indicating whether haveParcel.
        /// </summary>
        public bool haveParcel { set; get; }

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
        /// Gets or sets the Longitude.
        /// </summary>
        public double Longitude { set; get; }

        /// <summary>
        /// Gets or sets the Lattitude.
        /// </summary>
        public double Lattitude { set; get; }
    }
}
