using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Customer To List
    /// Display objrct for the next layer.
    /// </summary>
    public class CustomerToList
    {
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
        /// Gets or sets the NumFoParcelSentAndDelivered.
        /// </summary>
        public int NumFoParcelSentAndDelivered { set; get; }

        /// <summary>
        /// Gets or sets the NumFoParcelSent.
        /// </summary>
        public int NumFoParcelSent { set; get; }

        /// <summary>
        /// Gets or sets the NumFoParcelReceived.
        /// </summary>
        public int NumFoParcelReceived { set; get; }

        /// <summary>
        /// Gets or sets the NumFoParcelOnWay.
        /// </summary>
        public int NumFoParcelOnWay { set; get; }
    }
}
