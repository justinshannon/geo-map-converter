using System.Xml.Serialization;

namespace GeoMapConverter.VatsimGeoMapSet
{
    public class Text : Element
    {
        [XmlIgnore] public int Size { get; set; }
        [XmlIgnore] public bool Underline { get; set; }
        [XmlIgnore] public bool Opaque { get; set; }
        [XmlIgnore] public int XOffset { get; set; }
        [XmlIgnore] public int YOffset { get; set; }
        [XmlAttribute] public double Lat { get; set; }
        [XmlAttribute] public double Lon { get; set; }
        [XmlAttribute] public string Lines { get; set; }
    }
}