using System;
using System.Collections.Generic;
using System.Linq;
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

        public BcgMenu ItemByGroupId(int id)
        {
            return Items.Any(t => t.BcgGroups == id) ? this : null;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? "(unamed)" : Name;
        }
    }
}