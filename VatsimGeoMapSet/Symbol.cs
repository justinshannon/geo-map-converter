using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.VatsimGeoMapSet
{
    public class Symbol : Element
    {
        [XmlIgnore] public SymbolStyle Style { get; set; }
        [XmlAttribute] public double Lat { get; set; }
        [XmlAttribute] public double Lon { get; set; }
        [XmlElement] public List<Element> Elements { get; set; }
        [XmlAttribute] public string Size { get; set; }
    }
}