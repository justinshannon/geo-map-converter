using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GeoMapConverter.GeoRam.GeoMap.Enums;

namespace GeoMapConverter.GeoRam.GeoMap
{
    [Serializable]
    public class TextDefaultProperties
    {
        [XmlElement("BCGGroup", DataType = "integer")]
        public string BcgGroup { get; set; }

        public Colors Color { get; set; }

        [XmlElement(DataType = "integer")] public string FontSize { get; set; }

        public bool Underline { get; set; }

        public bool DisplaySetting { get; set; }

        [XmlElement(DataType = "integer")] public string XPixelOffset { get; set; }

        [XmlElement(DataType = "integer")] public string YPixelOffset { get; set; }

        [XmlArrayItem("FilterGroup", DataType = "integer", IsNullable = false)]
        public List<string> GeoTextFilters { get; set; }
    }
}