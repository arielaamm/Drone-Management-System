using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Customer in DAL.
    /// </summary>
    public struct Customer
    {
        /// <summary>
        /// Gets or sets a value indicating whether IsActive.
        /// </summary>
        public bool IsActive { set; get; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int? ID { set; get; }

        /// <summary>
        /// Gets or sets the CustomerName.
        /// </summary>
        public string CustomerName { set; get; }

        /// <summary>
        /// Gets or sets the Phone.
        /// </summary>
        public string Phone { set; get; }

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
