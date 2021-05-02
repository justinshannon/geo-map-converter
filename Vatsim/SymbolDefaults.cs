using System;
using System.Xml.Serialization;

namespace GeoMapConverter.Vatsim
{
    [Serializable]
    public class SymbolDefaults : ElementDefaults
    {
        [XmlAttribute("Style")]
        public SymbolStyle Style { get; set; }

        [XmlAttribute]
        public int Size { get; set; } = 1;
    }
}