using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GeoMapConverter.GeoRam.GeoMap.Enums;

namespace GeoMapConverter.GeoRam.GeoMap
{
    [Serializable]
    public class TextDefaultProperties
    {
        [XmlElement("BCGGroup")]
        public string BcgGroup { get; set; }

        public Colors Color { get; set; }

        public int FontSize { get; set; }

        public bool Underline { get; set; }

        public bool DisplaySetting { get; set; }

        public int XPixelOffset { get; set; }

        public int YPixelOffset { get; set; }

        [XmlArray("GeoTextFilters")]
        [XmlArrayItem("FilterGroup")]
        public string[] GeoTextFilters { get; set; }
    }
}