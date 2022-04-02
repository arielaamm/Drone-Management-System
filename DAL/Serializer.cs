using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

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
            StreamReader reader = new StreamReader(Path, Encoding.UTF8);
            Elements = from item in (IEnumerable<T>)serializer.Deserialize(reader)
                       select item;
            reader.Close();
            return Elements;
        }

        public void WriteEnumerable(IEnumerable<T> enumerable)
        {
            StreamWriter writer = new StreamWriter(Path, false, Encoding.UTF8);
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

        public void Update(T obj, Predicate<T> pred, Exception doesNotExistException)
        {
            List<T> list = GetElementsFromXml().ToList();
            int index = list.FindIndex(pred);
            if (index == -1) throw doesNotExistException;
            list[index] = obj;
            WriteEnumerable(from item in list select item);
        }
    }
}