using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    /// <summary>
    /// Defines the <see cref="BLFactory" />.
    /// </summary>
    public static class BLFactory
    {
        /// <summary>
        /// The GetBL.
        /// </summary>
        /// <param name="type">The type<see cref="string"/>.</param>
        /// <returns>The <see cref="IBL"/>.</returns>
        public static IBL GetBL(string type) => type switch
        {
            "BL" => BL.BL.GetInstance(),
            _ => throw new NotImplementedException()
        };
    }
}
