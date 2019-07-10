using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GeoMapConverter.Utils
{
    public static class SerializationUtils
    {
        public static void SerializeObjectToFile<T>(T obj, string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                xmlSerializer.Serialize(streamWriter, obj);
            }
        }
        public static T DeserializeObjectFromFile<T>(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result;
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                T t = (T)((object)xmlSerializer.Deserialize(new XmlTextReader(fileStream)
                {
                    Normalization = false
                }));
                if (t is IDeserializationCallback)
                {
                    (t as IDeserializationCallback).OnDeserialize();
                }
                result = t;
            }
            return result;
        }
    }
}
