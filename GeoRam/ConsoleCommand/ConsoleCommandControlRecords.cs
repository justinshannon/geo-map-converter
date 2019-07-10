using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeoMapConverter.GeoRam.ConsoleCommand
{
    [Serializable]
    [XmlRoot("ConsoleCommandControl_Records")]
    public class ConsoleCommandControlRecords
    {
        /// <summary>
        /// Gets or sets a collection of Map Brightness Menus
        /// </summary>
        [XmlElement("MapBrightnessMenu")]
        public List<MapBrightnessMenu> MapBrightnessMenus { get; set; }

        /// <summary>
        /// Gets or sets a collection of map filter menus
        /// </summary>
        [XmlElement("MapFilterMenu")]
        public List<MapFilterMenu> MapFilterMenus { get; set; }
    }
}
