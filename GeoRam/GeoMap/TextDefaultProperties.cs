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
        public int BcgGroup { get; set; }

        public Colors Color { get; set; }

        public int FontSize { get; set; }

        public bool Underline { get; set; }

        public bool DisplaySetting { get; set; }

        public int XPixelOffset { get; set; }

        public int YPixelOffset { get; set; }

        [XmlElement("GeoTextFilters")]
        public List<GeoTextFilter> GeoTextFilters { get; set; }

        public TextDefaultProperties()
        {
            GeoTextFilters = new List<GeoTextFilter>();
        }
    }

    public class GeoTextFilter
    {
        [XmlElement("FilterGroup")]
        public int Id { get; set; }
    }
}