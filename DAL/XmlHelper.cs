using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    /// <summary>
    /// Defines the <see cref="XmlHelper" />.
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// The BuildElementToXml.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="obj">The obj<see cref="T"/>.</param>
        /// <returns>The <see cref="XElement"/>.</returns>
        public static XElement BuildElementToXml<T>(T obj)
        {
            var a = new XElement(typeof(T).Name, from property in typeof(T).GetProperties()
                                                 select new XElement(property.Name,
                                                                     property.GetValue(obj).ToString()));
            return a;
        }

        /// <summary>
        /// The SaveToXml.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="enumerable">The enumerable<see cref="IEnumerable{T}"/>.</param>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="rootName">The rootName<see cref="string"/>.</param>
        public static void SaveToXml<T>(this IEnumerable<T> enumerable, string path, string rootName = "Root")
        {
            try
            {
                XElement root = new XElement(rootName, from item in enumerable
                                                       select BuildElementToXml(item));
                root.Save(path);
            }
            catch (Exception ex) { throw new DALExceptionscs.XmlWriteException(ex.Message, ex); }
        }
    }
}
