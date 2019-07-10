using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GeoMapConverter.GeoRam.GeoMap.Enums;

namespace GeoMapConverter.GeoRam.GeoMap
{
    [Serializable]
    public class GeoMapSymbol
    {
        public string SymbolId { get; set; }

        public string SymbolCode { get; set; }

        public SymbolTypes SymbolStyle { get; set; }

        [XmlElement("BCGGroup", DataType = "integer")]
        public string BcgGroup { get; set; }

        public Colors Color { get; set; }

        [XmlElement(DataType = "integer")] public string FontSize { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public double XSpherical { get; set; }

        public double YSpherical { get; set; }

        public double ZSpherical { get; set; }

        [XmlArrayItem("FilterGroup", DataType = "integer", IsNullable = false)]
        public List<string> GeoSymbolFiltersList { get; set; }

        public GeoMapText GeoMapText { get; set; }
    }
}