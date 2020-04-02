using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GeoMapConverter.GeoRam.GeoMap.Enums;

namespace GeoMapConverter.GeoRam.GeoMap
{
    [Serializable]
    public class GeoMapObject
    {
        [XmlElement(DataType = "integer")]
        public string MapGroupId { get; set; }

        public ObjectTypes MapObjectType { get; set; }

        public DefaultLineProperties DefaultLineProperties { get; set; }

        public DefaultSymbolProperties DefaultSymbolProperties { get; set; }

        public TextDefaultProperties TextDefaultProperties { get; set; }

        [XmlElement("GeoMapSymbol")]
        public List<GeoMapSymbol> GeoMapSymbolList { get; set; }

        [XmlElement("GeoMapLine")]
        public List<GeoMapLine> GeoMapLineList { get; set; }

        [XmlElement("GeoMapText")]
        public List<GeoMapText> GeoMapTextList { get; set; }
    }
}