using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.Vatsim
{
    [Serializable]
    public class BcgMenu
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlArray("Items")]
        [XmlArrayItem("BcgMenuItem")]
        public List<BcgMenuItem> Items { get; set; }

        public BcgMenu()
        {
            Name = "";
            Items = new List<BcgMenuItem>();
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? "(unamed)" : Name;
        }
    }
}