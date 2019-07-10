using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.GeoRam.ConsoleCommand
{
    [Serializable]
    public class MapFilterButton
    {
        /// <summary>
        /// Gets or sets the position of the Geomap filter button
        /// </summary>
        [XmlElement(DataType = "integer")]
        public string MenuPosition { get; set; }

        /// <summary>
        /// Gets or sets the label line 1 for the filter button
        /// </summary>
        public string LabelLine1 { get; set; }

        /// <summary>
        /// Gets or sets the label line 2 for the filter button
        /// </summary>
        public string LabelLine2 { get; set; }

        /// <summary>
        /// Gets or sets a collection of Geomap filter groups
        /// </summary>
        [XmlArrayItem("MapFilterGroup", DataType = "integer", IsNullable = false)]
        public List<string> MapFilterGroupsList { get; set; }

        [XmlIgnore] public string FilterButtonName => $"{LabelLine1} {LabelLine2}";
    }
}