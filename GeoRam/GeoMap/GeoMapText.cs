using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GeoMapConverter.GeoRam.GeoMap.Enums;

namespace GeoMapConverter.GeoRam.GeoMap
{
    [Serializable]
    public class GeoMapText
    {
        public string TextObjectId { get; set; }

        [XmlElement("BCGGroup")]
        public int BcgGroup { get; set; }

        public Colors Color { get; set; }

        public int FontSize { get; set; }

        public bool Underline { get; set; }

        public bool DisplaySetting { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public double XSpherical { get; set; }

        public double YSpherical { get; set; }

        public double ZSpherical { get; set; }

        [XmlElement("FilterGroup")]
        public GeoTextFilter GeoTextFilterList { get; set; }

        [XmlArrayItem("TextLine", IsNullable = false)]
        public List<string> GeoTextStrings { get; set; }

        public GeoMapText()
        {
            GeoTextFilterList = new GeoTextFilter();
        }
    }
}