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
        [XmlElement(DataType = "integer")]
        public string MenuPosition { get; set; }

        /// <summary>
        /// Gets or sets the label for the BCG button
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets a collection of Geomap BCG groups
        /// </summary>
        [XmlArrayItem("MapBCGGroup", DataType = "integer", IsNullable = false)]
        public List<string> MapBcgGroups { get; set; }
    }
}