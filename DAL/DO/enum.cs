using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Enum file
    /// </summary>
    public enum Weight
    { /// <summary>
      /// Defines the LIGHT.
      /// </summary>
        LIGHT = 0,
        /// <summary>
        /// Defines the MEDIUM.
        /// </summary>
        MEDIUM = 1,
        /// <summary>
        /// Defines the HEAVY.
        /// </summary>
        HEAVY = 2
    }

    /// <summary>
    /// Defines the Priority.
    /// </summary>
    public enum Priority
    { /// <summary>
      /// Defines the REGULAR.
      /// </summary>
        REGULAR = 0,
        /// <summary>
        /// Defines the FAST.
        /// </summary>
        FAST = 1,
        /// <summary>
        /// Defines the SOS.
        /// </summary>
        SOS = 2
    }

    /// <summary>
    /// Defines the Model.
    /// </summary>
    public enum Model
    { /// <summary>
      /// Defines the MDL65.
      /// </summary>
        MDL65 = 0,
        /// <summary>
        /// Defines the MDL45.
        /// </summary>
        MDL45 = 1,
        /// <summary>
        /// Defines the MDL78.
        /// </summary>
        MDL78 = 2
    }

    /// <summary>
    /// Defines the Status.
    /// </summary>
    public enum Status
    { /// <summary>
      /// Defines the CREAT.
      /// </summary>
        CREAT = 0,
        /// <summary>
        /// Defines the BELONG.
        /// </summary>
        BELONG = 1,
        /// <summary>
        /// Defines the PICKUP.
        /// </summary>
        PICKUP = 2,
        /// <summary>
        /// Defines the MAINTENANCE.
        /// </summary>
        MAINTENANCE = 3
    }

    /// <summary>
    /// Defines the StatusParcel.
    /// </summary>
    public enum StatusParcel
    { /// <summary>
      /// Defines the CREAT.
      /// </summary>
        CREAT = 0,
        /// <summary>
        /// Defines the BELONG.
        /// </summary>
        BELONG = 1,
        /// <summary>
        /// Defines the PICKUP.
        /// </summary>
        PICKUP = 2,
        /// <summary>
        /// Defines the DELIVERD.
        /// </summary>
        DELIVERD = 3,
    }
}
