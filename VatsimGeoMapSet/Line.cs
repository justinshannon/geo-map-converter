using System.Xml.Serialization;

namespace GeoMapConverter.VatsimGeoMapSet
{
    public class Line : Element
    {
        [XmlIgnore] public LineStyle Style { get; set; }
        [XmlIgnore] public int Thickness { get; set; }
        [XmlAttribute] public double StartLat { get; set; }
        [XmlAttribute] public double StartLon { get; set; }
        [XmlAttribute] public double EndLat { get; set; }
        [XmlAttribute] public double EndLon { get; set; }
    }
}