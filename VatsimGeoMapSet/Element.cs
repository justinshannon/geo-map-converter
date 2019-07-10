using System.Xml.Serialization;

namespace GeoMapConverter.VatsimGeoMapSet
{
    [XmlInclude(typeof(Line))]
    [XmlInclude(typeof(Symbol))]
    [XmlInclude(typeof(Text))]
    public class Element
    {
        [XmlIgnore] public string GeoMap { get; set; }
        [XmlIgnore] public int BcgGroup { get; set; }
        [XmlIgnore] public string SymbolId { get; set; }
        [XmlAttribute] public string Filters { get; set; }
        public bool ShouldSerializeFilters => Filters != null;
    }
}