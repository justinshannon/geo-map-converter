using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace GeoMapConverter.Vatsim
{
    [Serializable]
    [XmlInclude(typeof(SymbolDefaults))]
    [XmlInclude(typeof(TextDefaults))]
    [XmlInclude(typeof(LineDefaults))]
    public class ElementDefaults
    {
        [XmlIgnore]
        public List<int> FilterList { get; set; } = new List<int>();

        [XmlAttribute("Bcg")]
        public int BcgGroup { get; set; }

        [XmlAttribute("Filters")]
        public string Filters
        {
            get => FilterList.Count == 0 ? string.Join(",", Enumerable.Range(1, 20)) : string.Join(",", FilterList);
            set => throw new NotSupportedException();
        }
    }
}
