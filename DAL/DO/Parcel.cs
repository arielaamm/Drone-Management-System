using DateTime = System.DateTime;

namespace DO
{
    /// <summary>
    /// Parcel in DAL.
    /// </summary>
    public struct Parcel
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
        /// Gets or sets the SenderId.
        /// </summary>
        public int SenderId { set; get; }

        /// <summary>
        /// Gets or sets the TargetId.
        /// </summary>
        public int TargetId { set; get; }

        /// <summary>
        /// Gets or sets the Weight.
        /// </summary>
        public Weight Weight { set; get; }

        /// <summary>
        /// Gets or sets the Priority.
        /// </summary>
        public Priority Priority { set; get; }

        /// <summary>
        /// Gets or sets the DroneId.
        /// </summary>
        public int DroneId { set; get; }

        /// <summary>
        /// Gets or sets the Requested.
        /// </summary>
        public DateTime? Requested { set; get; }

        /// <summary>
        /// Gets or sets the Scheduled.
        /// </summary>
        public DateTime? Scheduled { set; get; }

        /// <summary>
        /// Gets or sets the PickedUp.
        /// </summary>
        public DateTime? PickedUp { set; get; }

        /// <summary>
        /// Gets or sets the Deliverd.
        /// </summary>
        public DateTime? Deliverd { set; get; }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public StatusParcel Status { set; get; }
    }
}
