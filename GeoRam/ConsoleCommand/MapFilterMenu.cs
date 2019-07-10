using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.GeoRam.ConsoleCommand
{
    /// <summary>
    /// The data for displaying Map Filter Buttons in the Geomap Menu
    /// </summary>
    [Serializable]
    [XmlRoot("MapFilterMenu", IsNullable = false)]
    public class MapFilterMenu
    {
        /// <summary>
        /// Gets or sets the name of the Geomap Filter Menu
        /// </summary>
        public string FilterMenuName { get; set; }

        /// <summary>
        /// Gets or sets a collection of Geomap Filter buttons
        /// </summary>
        [XmlElement("MapFilterButton")]
        public List<MapFilterButton> MapFilterButtons { get; set; }
    }
}