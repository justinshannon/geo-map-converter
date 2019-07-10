using System.Xml.Serialization;

namespace GeoMapConverter.VatsimGeoMapSet
{
    public class TextDefaults : ElementDefaults
    {
        [XmlAttribute] public string Size { get; set; }
        [XmlAttribute] public bool Underline { get; set; }
        [XmlAttribute] public bool Opaque { get; set; }
        [XmlAttribute] public string XOffset { get; set; }
        [XmlAttribute] public string YOffset { get; set; }
    }
}