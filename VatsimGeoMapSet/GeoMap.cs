using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.VatsimGeoMapSet
{
    [Serializable]
    public class GeoMap
    {
        public List<GeoMapObject> Objects { get; set; }
        [XmlAttribute] public string Name { get; set; }
        [XmlAttribute] public string LabelLine1 { get; set; }
        [XmlAttribute] public string LabelLine2 { get; set; }
        [XmlAttribute] public string BcgMenuName { get; set; }
        [XmlAttribute] public string FilterMenuName { get; set; }
        [XmlIgnore] public string GeoMapSetName { get; set; }

        public GeoMap()
        {
            Objects = new List<GeoMapObject>();
        }
    }
}