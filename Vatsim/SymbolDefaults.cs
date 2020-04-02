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
        public string Size { get; set; }

        public SymbolDefaults()
        {
            Size = "1";
        }
    }
}