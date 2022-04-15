using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DAL
{
    /// <summary>
    /// Defines the <see cref="XMLTools" />.
    /// </summary>
    public class XMLTools
    {
        /// <summary>
        /// Defines the dir.
        /// </summary>
        public static readonly string dir = @"XML Files\";

        /// <summary>
        /// Initializes static members of the <see cref="XMLTools"/> class.
        /// </summary>
        static XMLTools()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        /// <summary>
        /// The SaveListToXMLElement.
        /// </summary>
        /// <param name="rootElem">The rootElem<see cref="XElement"/>.</param>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        public static void SaveListToXMLElement(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(dir + filePath);
            }
            catch (Exception ex)
            {
                throw new DALExceptionscs.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }

        /// <summary>
        /// The LoadListFromXMLElement.
        /// </summary>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        /// <returns>The <see cref="XElement"/>.</returns>
        public static XElement LoadListFromXMLElement(string filePath)
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    return XElement.Load(dir + filePath);
                }
                else
                {
                    XElement rootElem = new(dir + filePath);
                    rootElem.Save(dir + filePath);
                    return rootElem;
                }
            }
            catch (Exception ex)
            {
                throw new DALExceptionscs.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }

        /// <summary>
        /// The SaveListToXMLSerializer.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="list">The list<see cref="List{T}"/>.</param>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                FileStream file = new(dir + filePath, FileMode.Create);
                XmlSerializer x = new(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new DALExceptionscs.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }

        /// <summary>
        /// The LoadListFromXMLSerializer.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    List<T> list;
                    XmlSerializer x = new(typeof(List<T>));
                    FileStream file = new(dir + filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch (Exception ex)
            {
                throw new DALExceptionscs.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }

        /// <summary>
        /// The Add.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="obj">The obj<see cref="T"/>.</param>
        /// <param name="condition">The condition<see cref="bool"/>.</param>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        public static void Add<T>(T obj, bool condition, string filePath)
        {
            List<T> list = LoadListFromXMLSerializer<T>(filePath);
            if (condition)
                list.Add(obj);
            SaveListToXMLSerializer<T>(list, filePath);
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="obj">The obj<see cref="T"/>.</param>
        /// <param name="index">The index<see cref="int"/>.</param>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        public static void Update<T>(T obj, int index, string filePath)
        {
            List<T> list = LoadListFromXMLSerializer<T>(filePath);
            list[index] = obj;
            SaveListToXMLSerializer<T>(list, filePath);
        }
    }
}
