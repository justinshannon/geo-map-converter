using System.Windows.Forms;
using System.Xml.Serialization;
using GeoMapConverter.GeoRam.GeoMap.Enums;

namespace GeoMapConverter.VatsimGeoMapSet
{
    public class LineDefaults : ElementDefaults
    {
        [XmlAttribute] public LineTypes Style { get; set; }
        [XmlAttribute] public string Thickness { get; set; }
    }
}