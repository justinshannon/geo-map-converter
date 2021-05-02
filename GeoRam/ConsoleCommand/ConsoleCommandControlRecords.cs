using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.GeoRam.ConsoleCommand
{
    [Serializable]
    [XmlRoot("ConsoleCommandControl_Records")]
    public class ConsoleCommandControlRecords
    {
        [XmlElement("MapBrightnessMenu")]
        public List<MapBrightnessMenu> MapBrightnessMenus { get; set; }

        [XmlElement("MapFilterMenu")]
        public List<MapFilterMenu> MapFilterMenus { get; set; }
    }
}
