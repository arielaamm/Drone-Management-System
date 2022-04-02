using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DALExceptionscs;

namespace DAL
{
    public static class XmlHelper
    {
        public static XElement BuildElementToXml<T>(T obj)
        {
            return new XElement(typeof(T).Name, from property in typeof(T).GetProperties()
                                                select new XElement(property.Name,
                                                                    property.GetValue(obj)
                                                                    .ToString()));
        }

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
