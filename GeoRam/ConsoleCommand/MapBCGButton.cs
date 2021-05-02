using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.GeoRam.ConsoleCommand
{
    [Serializable]
    public class MapBcgButton
    {
        public int MenuPosition { get; set; }

        public string Label { get; set; }

        [XmlElement("MapBCGGroups")]
        public MapBcgGroup MapBcgGroups { get; set; }
    }

    public class MapBcgGroup
    {
        [XmlElement("MapBCGGroup")]
        public int BcgGroup { get; set; }
    }
}