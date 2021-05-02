using System;
using System.Xml.Serialization;

namespace GeoMapConverter.GeoRam.ConsoleCommand
{
    [Serializable]
    public class MapFilterButton
    {
        public int MenuPosition { get; set; }

        public string LabelLine1 { get; set; }

        public string LabelLine2 { get; set; }

        [XmlElement("MapFilterGroups")]
        public MapFilterGroup MapFilterGroup { get; set; }

        [XmlIgnore]
        public string FilterButtonName => $"{LabelLine1} {LabelLine2}";
    }

    public class MapFilterGroup
    {
        [XmlElement("MapFilterGroup")]
        public int Group { get; set; }
    }
}