using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.VatsimGeoMapSet
{
    [Serializable]
    public class GeoMapObject
    {
        public LineDefaults LineDefaults { get; set; }
        public TextDefaults TextDefaults { get; set; }
        public SymbolDefaults SymbolDefaults { get; set; }
        public List<Element> Elements { get; set; }
        [XmlAttribute] public string Description { get; set; }
        [XmlAttribute] public bool TdmOnly { get; set; }
        [XmlIgnore] public string GeoMapName { get; set; }
        [XmlIgnore] public string FilterGroup { get; set; }
    }
}