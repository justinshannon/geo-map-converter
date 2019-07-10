using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.VatsimGeoMapSet
{
    [Serializable]
    public class BcgMenu
    {
        [XmlAttribute] public string Name { get; set; }

        [XmlArray("Items")] public List<BcgMenuItem> BcgMenuItemList { get; set; }
    }

    [XmlType("BcgMenuItem")]
    public class BcgMenuItem
    {
        [XmlAttribute] public string Label { get; set; }
    }
}