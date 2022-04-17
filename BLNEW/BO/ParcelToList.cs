namespace BO
{
    /// <summary>
    /// Parcel To List
    /// Display objrct for the next layer.
    /// </summary>
    public class ParcelToList
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// Gets or sets the SenderName.
        /// </summary>
        public string SenderName { set; get; }

        /// <summary>
        /// Gets or sets the TargetName.
        /// </summary>
        public string TargetName { set; get; }

        /// <summary>
        /// Gets or sets the Weight.
        /// </summary>
        public Weight Weight { set; get; }

        /// <summary>
        /// Gets or sets the Priority.
        /// </summary>
        public Priority Priority { set; get; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public StatusParcel status { set; get; }
    }
}
