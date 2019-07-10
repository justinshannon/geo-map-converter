using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeoMapConverter.VatsimGeoMapSet
{
    [XmlRoot("GeoMapSet")]
    public class GeoMapSet
    {
        public List<BcgMenu> BcgMenus { get; set; }
        public List<FilterMenu> FilterMenus { get; set; }
        public List<GeoMap> GeoMaps { get; set; }
        [XmlAttribute] public string DefaultMap { get; set; }

        public GeoMapSet()
        {
            BcgMenus = new List<BcgMenu>();
            FilterMenus = new List<FilterMenu>();
            GeoMaps = new List<GeoMap>();
        }
    }
}
