using GeoMapConverter.GeoRam.GeoMap.Enums;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.GeoRam.GeoMap
{
    [Serializable]
    public class DefaultLineProperties
    {
        public LineTypes LineStyle { get; set; }

        [XmlElement("BCGGroup", DataType = "integer")]
        public string BcgGroup { get; set; }

        public Colors Color { get; set; }

        [XmlElement(DataType = "integer")] public string Thickness { get; set; }

        [XmlArrayItem("FilterGroup", DataType = "integer", IsNullable = false)]
        public List<string> GeoLineFilters { get; set; }
    }
}