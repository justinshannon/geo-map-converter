using GeoMapConverter.GeoRam.GeoMap.Enums;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.GeoRam.GeoMap
{
    [Serializable]
    public class DefaultLineProperties
    {
        public Vatsim.LineStyle LineStyle { get; set; }

        [XmlElement("BCGGroup")]
        public string BcgGroup { get; set; }

        public Colors Color { get; set; }

        public string Thickness { get; set; }

        [XmlArray("GeoLineFilters")]
        [XmlArrayItem("FilterGroup")]
        public string[] GeoLineFilters { get; set; }
    }
}