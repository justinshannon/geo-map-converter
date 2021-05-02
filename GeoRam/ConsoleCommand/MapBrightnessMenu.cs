using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.GeoRam.ConsoleCommand
{
    [Serializable]
    [XmlRoot("MapBrightnessMenu")]
    public class MapBrightnessMenu
    {
        [XmlElement("BCGMenuName")]
        public string BcgMenuName { get; set; }

        [XmlElement("MapBCGButton")]
        public List<MapBcgButton> MapBcgButtons { get; set; }
    }
}