using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace GeoMapConverter.Vatsim
{
    [Serializable]
    public class GeoMap
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("LabelLine1")]
        public string LabelLine1 { get; set; }

        [XmlAttribute("LabelLine2")]
        public string LabelLine2 { get; set; }

        [XmlAttribute("BcgMenuName")]
        public string BcgMenuName { get; set; }

        [XmlAttribute("FilterMenuName")]
        public string FilterMenuName { get; set; }

        public List<GeoMapObject> Objects { get; set; }

        [XmlIgnore]
        public string BcgGroup { get; set; }

        [XmlIgnore]
        public string FilterGroup => Objects.Select(t => t.Filter).FirstOrDefault();

        public GeoMap()
        {
            Name = "";
            LabelLine1 = "";
            LabelLine2 = "";
            BcgMenuName = "";
            FilterMenuName = "";
            Objects = new List<GeoMapObject>();
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? "(unamed)" : Name;
        }
    }
}