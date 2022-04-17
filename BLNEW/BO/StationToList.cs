namespace BO
{
    /// <summary>
    /// Station To List
    /// Display objrct for the next layer.
    /// </summary>
    public class StationToList
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// Gets or sets the StationName.
        /// </summary>
        public string StationName { set; get; }

        /// <summary>
        /// Gets or sets the FreeChargeSlots.
        /// </summary>
        public int FreeChargeSlots { set; get; }

        /// <summary>
        /// Gets or sets the UsedChargeSlots.
        /// </summary>
        public int UsedChargeSlots { set; get; }
    }
}
