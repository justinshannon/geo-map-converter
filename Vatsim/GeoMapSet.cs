using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeoMapConverter.Vatsim
{
    [XmlRoot("GeoMapSet")]
    public class GeoMapSet
    {
        [XmlAttribute]
        public string DefaultMap { get; set; }

        [XmlArray("BcgMenus")]
        [XmlArrayItem("BcgMenu")]
        public List<BcgMenu> BcgMenus { get; set
                ; }

        [XmlArray("FilterMenus")]
        [XmlArrayItem("FilterMenu")]
        public List<FilterMenu> FilterMenus { get; set; }

        [XmlArray("GeoMaps")]
        [XmlArrayItem("GeoMap")]
        public List<GeoMap> GeoMaps { get; set; }

        public GeoMapSet()
        {
            DefaultMap = "";
            BcgMenus = new List<BcgMenu>();
            FilterMenus = new List<FilterMenu>();
            GeoMaps = new List<GeoMap>();
        }
    }
}
