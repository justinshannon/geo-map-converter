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
        [XmlAttribute("Bcg")]
        public int BcgGroup { get; set; }

        [XmlAttribute("Filters")]
        public int Filters { get; set; }
    }
}
