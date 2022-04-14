using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Parcel in BL.
    /// </summary>
    public class Parcel
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
        /// Gets or sets the sender.
        /// </summary>
        public CustomerInParcel sender { set; get; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public CustomerInParcel target { set; get; }

        /// <summary>
        /// Gets or sets the Weight.
        /// </summary>
        public Weight Weight { set; get; }

        /// <summary>
        /// Gets or sets the Priority.
        /// </summary>
        public Priority Priority { set; get; }

        /// <summary>
        /// Gets or sets the Drone.
        /// </summary>
        public DroneInParcel Drone { set; get; }

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
    }
}
