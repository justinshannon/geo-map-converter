using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeoMapConverter.Vatsim
{
    [Serializable]
    public class LineDefaults : ElementDefaults
    {
        [XmlAttribute("Style")]
        public LineStyle Style { get; set; }

        [XmlAttribute("Thickness")]
        public string Thickness { get; set; }

        public LineDefaults()
        {
            Thickness = "1";
        }
    }
}
