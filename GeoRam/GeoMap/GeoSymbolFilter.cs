using System.Xml.Serialization;

namespace GeoMapConverter.GeoRam.GeoMap
{
    public class GeoSymbolFilter
    {
        [XmlElement("FilterGroup")]
        public int Id { get; set; }
    }
}