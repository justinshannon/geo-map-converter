using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GeoMapConverter.GeoRam.GeoMap.Enums;

namespace GeoMapConverter.GeoRam.GeoMap
{
    [Serializable]
    public class DefaultSymbolProperties
    {
        public Vatsim.SymbolStyle SymbolStyle { get; set; }

        [XmlElement("BCGGroup")]
        public int BcgGroup { get; set; }

        public int FontSize { get; set; }

        [XmlElement("GeoSymbolFilters")]
        public GeoSymbolFilter GeoSymbolFilters { get; set; }

        public DefaultSymbolProperties()
        {
            GeoSymbolFilters = new GeoSymbolFilter();
        }
    }
}