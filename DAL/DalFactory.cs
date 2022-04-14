using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    /// <summary>
    /// Defines the <see cref="DalFactory" />.
    /// </summary>
    public static class DalFactory
    {
        /// <summary>
        /// The GetDal.
        /// </summary>
        /// <param name="type">The type<see cref="string"/>.</param>
        /// <returns>The <see cref="IDal"/>.</returns>
        public static IDal GetDal(string type) => type switch
        {
            "DalObject" => DalObject.GetInstance(),
            "DalXml" => DalXml.GetInstance(),
            _ => throw new NotImplementedException()
        };
    }
}
