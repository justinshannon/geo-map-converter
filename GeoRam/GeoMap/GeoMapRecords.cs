using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeoMapConverter.GeoRam.GeoMap
{
    [Serializable]
    [XmlRoot("Geomaps_Records")]
    public class GeoMapRecords
    {
        /// <summary>
        /// Gets or sets a collection of Geomap Records
        /// </summary>
        [XmlElement("GeoMapRecord")]
        public List<GeoMapRecord> GeoMapRecordList { get; set; }
    }
}
