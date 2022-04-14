using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Drone To List
    /// Display objrct for the next layer.
    /// </summary>
    public class DroneToList
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

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
        /// Gets or sets the Position.
        /// </summary>
        public Location Position { set; get; }

        /// <summary>
        /// Gets or sets the IdParcel.
        /// </summary>
        public int? IdParcel { set; get; }
    }
}
