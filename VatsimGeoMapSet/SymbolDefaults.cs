using System.Xml.Serialization;
using GeoMapConverter.GeoRam.GeoMap.Enums;

namespace GeoMapConverter.VatsimGeoMapSet
{
    public class SymbolDefaults : ElementDefaults
    {
        [XmlAttribute] public SymbolTypes Style { get; set; }
        [XmlAttribute] public string Size { get; set; }
    }
}