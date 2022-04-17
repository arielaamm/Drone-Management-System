namespace BO
{
    /// <summary>
    /// Parcel In Transaction
    /// sub object.
    /// </summary>
    public class ParcelTransactioning
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int? ID { set; get; }

        /// <summary>
        /// Gets or sets a value indicating whether ParcelStatus.
        /// on the way - true, waiting to drone arrival - false
        /// </summary>
        public bool ParcelStatus { set; get; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        public Priority priority { set; get; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        public Weight weight { set; get; }

        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        public CustomerInParcel sender { set; get; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public CustomerInParcel target { set; get; }

        /// <summary>
        /// Gets or sets the LocationOfSender.
        /// </summary>
        public Location LocationOfSender { set; get; }

        /// <summary>
        /// Gets or sets the LocationOftarget.
        /// </summary>
        public Location LocationOftarget { set; get; }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        public double distance { set; get; }
    }
}
