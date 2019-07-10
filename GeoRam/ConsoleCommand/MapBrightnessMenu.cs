using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.GeoRam.ConsoleCommand
{
    [Serializable]
    [XmlRoot("MapBrightnessMenu")]
    public class MapBrightnessMenu
    {
        /// <summary>
        /// Gets or sets the name of the Map Brightness Menu
        /// </summary>
        [XmlElement("BCGMenuName")]
        public string BcgMenuName { get; set; }

        /// <summary>
        /// Gets or sets a collection of Map BCG Buttons
        /// </summary>
        [XmlElement("MapBCGButton")]
        public List<MapBcgButton> MapBcgButtons { get; set; }
    }
}