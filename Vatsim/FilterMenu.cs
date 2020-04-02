using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeoMapConverter.Vatsim
{
    [Serializable]
    [XmlType("FilterMenu")]
    public class FilterMenu
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlArray("Items")]
        [XmlArrayItem("FilterMenuItem")]
        public List<FilterMenuItem> Items { get; set; }

        public FilterMenu()
        {
            Name = "";
            Items = new List<FilterMenuItem>();
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? "(unamed)" : Name;
        }
    }
}
