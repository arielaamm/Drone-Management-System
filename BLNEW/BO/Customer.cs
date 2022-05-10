using System.Collections.Generic;

namespace BO
{
    /// <summary>
    /// Customer in BL.
    /// </summary>
    public class Customer
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
        /// Gets or sets the CustomerName.
        /// </summary>
        public string CustomerName { set; get; }

        /// <summary>
        /// Gets or sets the Phone.
        /// </summary>
        public string Phone { set; get; }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        public string Password { set; get; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        public string Email { set; get; }

        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        public Location Position { set; get; }

        /// <summary>
        /// Gets or sets the fromCustomer.
        /// </summary>
        public List<ParcelInCustomer> fromCustomer { set; get; }

        /// <summary>
        /// Gets or sets the toCustomer.
        /// </summary>
        public List<ParcelInCustomer> toCustomer { set; get; }
    }
}
