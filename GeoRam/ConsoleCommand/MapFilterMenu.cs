using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.GeoRam.ConsoleCommand
{
    [Serializable]
    [XmlRoot("MapFilterMenu", IsNullable = false)]
    public class MapFilterMenu
    {
        public string FilterMenuName { get; set; }

        [XmlElement("MapFilterButton")]
        public List<MapFilterButton> MapFilterButtons { get; set; }
    }
}