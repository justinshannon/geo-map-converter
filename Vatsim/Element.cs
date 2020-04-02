using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeoMapConverter.Vatsim
{
    [Serializable]
    [XmlInclude(typeof(Line))]
    [XmlInclude(typeof(Symbol))]
    [XmlInclude(typeof(Text))]
    public class Element
    {
        [XmlAttribute]
        public int Bcg { get; set; }

        [XmlIgnore]
        public bool BcgSpecified { get; set; }

        [XmlIgnore]
        public List<int> Filters { get; set; }

        [XmlAttribute("Filters")]
        public string FilterList {
            get {
                if (Filters.Count > 0) {
                    return string.Join(",", Filters);
                }
                return "";
            }
            set {
                if (!string.IsNullOrEmpty(value)) {
                    Filters = value.Split(',').Select(int.Parse).ToList();
                }
            }
        }

        public Element()
        {
            Filters = new List<int>();
        }
    }
}
