using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.GeoRam.ConsoleCommand
{
    [Serializable]
    public class MapBcgButton
    {
        /// <summary>
        /// Gets or sets the position of the button
        /// </summary>
        public int MenuPosition { get; set; }

        /// <summary>
        /// Gets or sets the label for the BCG button
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets a collection of Geomap BCG groups
        /// </summary>
        //[XmlElement("MapBCGGroups")]
        [XmlArray("MapBCGGroups")]
        [XmlArrayItem("MapBCGGroup")]
        public string[] MapBcgGroups { get; set; }
    }
}