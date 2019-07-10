using System.Xml.Serialization;

namespace GeoMapConverter.VatsimGeoMapSet
{
    [XmlInclude(typeof(SymbolDefaults))]
    [XmlInclude(typeof(TextDefaults))]
    [XmlInclude(typeof(LineDefaults))]
    public class ElementDefaults
    {
        [XmlAttribute("Bcg")] public string BcgGroup { get; set; }
        [XmlAttribute] public string Filters { get; set; }
        public bool ShouldSerializeFilters => Filters != null;
    }
}