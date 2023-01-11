using GeoMapConverter.GeoRam.GeoMap.Enums;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.GeoRam.GeoMap
{
    [Serializable]
    public class DefaultLineProperties
    {
        public Vatsim.LineStyle LineStyle { get; set; }

        [XmlElement("BCGGroup")]
        public int BcgGroup { get; set; }

        public int Thickness { get; set; }

        [XmlElement("GeoLineFilters")]
        public List<FilterGroup> GeoLineFilters { get; set; }

        public DefaultLineProperties()
        {
            GeoLineFilters = new List<FilterGroup>();
        }
    }

    public class FilterGroup
    {
        [XmlElement("FilterGroup")]
        public int Id { get; set; }
    }
}