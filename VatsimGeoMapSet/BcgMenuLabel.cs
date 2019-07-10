using System;
using System.Xml.Serialization;

namespace GeoMapConverter.VatsimGeoMapSet
{
    [Serializable]
    public class BcgMenuLabel
    {
        [XmlIgnore] public string Group { get; set; }

        [XmlAttribute("Label")] public string Label { get; set; }
    }
}