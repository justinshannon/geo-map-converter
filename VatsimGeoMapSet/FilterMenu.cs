using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.VatsimGeoMapSet
{
    [Serializable]
    public class FilterMenu
    {
        [XmlAttribute] public string Name { get; set; }
        [XmlArray("Items")] public List<FilterLabel> FilterLabels { get; set; }
    }

    [XmlType("FilterMenuItem")]
    public class FilterLabel
    {
        [XmlAttribute] public string LabelLine1 { get; set; }
        [XmlAttribute] public string LabelLine2 { get; set; }
    }
}