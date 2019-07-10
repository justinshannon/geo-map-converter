using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GeoMapConverter.GeoRam.GeoMap.Enums;

namespace GeoMapConverter.GeoRam.GeoMap
{
    [Serializable]
    public class DefaultSymbolProperties
    {
        public SymbolTypes SymbolStyle { get; set; }

        [XmlElement("BCGGroup", DataType = "integer")]
        public string BcgGroup { get; set; }

        public Colors Color { get; set; }

        [XmlElement(DataType = "integer")] public string FontSize { get; set; }

        [XmlArrayItem("FilterGroup", DataType = "integer", IsNullable = false)]
        public List<string> GeoSymbolFilters { get; set; }
    }
}