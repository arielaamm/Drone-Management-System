using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace DAL
{
    public class Serializer<T> where T : new()
    {
        public IEnumerable<T> Elements { get; private set; }

        public string Path { get; set; }

        XmlSerializer serializer;


        public Serializer(string path)
        {
            Path = path;
            serializer = new XmlSerializer(new List<T>().GetType());
            GetElementsFromXml();
        }

        public IEnumerable<T> GetElementsFromXml()
        {
            if (!File.Exists(Path)) WriteEnumerable(new List<T>());
            XmlReader reader = new XmlTextReader(Path);
            IEnumerable<T> enumerable()
            {
                foreach (var item in (IEnumerable<T>)serializer.Deserialize(reader))
                {
                    yield return item;
                }
            }

            Elements = enumerable();
            reader.Close();
            return Elements;
        }

        public void WriteEnumerable(IEnumerable<T> enumerable)
        {
            StreamWriter writer = new StreamWriter(Path);
            serializer.Serialize(writer, enumerable);
            writer.Close();
        }

        public void Add(T obj, Predicate<T> pred)//, Exception alreadyExistException)
        {
            List<T> list = GetElementsFromXml().ToList();
            if (list.FindIndex(pred) != -1) //throw alreadyExistException;
            list.Add(obj);
            WriteEnumerable(from item in list select item);
        }

        public void Update(T obj, Predicate<T> pred)//, Exception doesNotExistException)
        {
            List<T> list = GetElementsFromXml().ToList();
            int index = list.FindIndex(pred);
            if (index == -1)// throw doesNotExistException;
            list[index] = obj;
            WriteEnumerable(from item in list select item);
        }
    }
    class XMLTools
    {
        static string dir = @"xml\";
        static XMLTools()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        #region SaveLoadWithXElement
        public static void SaveListToXMLElement(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(dir + filePath);
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }

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
                    XElement rootElem = new XElement(dir + filePath);
                    rootElem.Save(dir + filePath);
                    return rootElem;
                }
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion

        #region SaveLoadWithXMLSerializer
        public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(dir + filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
        public static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(dir + filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion
        //free style funcsעכעכ
    }
}