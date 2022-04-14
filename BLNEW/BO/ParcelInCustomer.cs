using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Parcel in Customer 
    /// sub object.
    /// </summary>
    public class ParcelInCustomer
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// Gets or sets the Weight.
        /// </summary>
        public Weight Weight { set; get; }

        /// <summary>
        /// Gets or sets the Priority.
        /// </summary>
        public Priority Priority { set; get; }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public StatusParcel Status { set; get; }

        /// <summary>
        /// Gets or sets the Sender.
        /// </summary>
        public CustomerInParcel Sender { set; get; }

        /// <summary>
        /// Gets or sets the Target.
        /// </summary>
        public CustomerInParcel Target { set; get; }
    }
}
