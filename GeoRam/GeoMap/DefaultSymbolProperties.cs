using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GeoMapConverter.GeoRam.GeoMap.Enums;

namespace GeoMapConverter.GeoRam.GeoMap
{
    [Serializable]
    public class DefaultSymbolProperties
    {
        public Vatsim.SymbolStyle SymbolStyle { get; set; }

        [XmlElement("BCGGroup")]
        public string BcgGroup { get; set; }

        public Colors Color { get; set; }

        public string FontSize { get; set; }

        [XmlArray("GeoSymbolFilters")]
        [XmlArrayItem("FilterGroup")]
        public string[] GeoSymbolFilters { get; set; }
    }
}