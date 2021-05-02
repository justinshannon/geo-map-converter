using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GeoMapConverter.Vatsim
{
    [Serializable]
    [XmlType("FilterMenuItem")]
    public class FilterMenuItem
    {
        [XmlAttribute("LabelLine1")]
        public string LabelLine1 { get; set; }

        [XmlAttribute("LabelLine2")]
        public string LabelLine2 { get; set; }

        [XmlIgnore]
        public int MenuPosition { get; set; }

        [XmlIgnore]
        public int FilterGroup { get; set; }

        [XmlIgnore]
        public int OurId { get; set; }

        public FilterMenuItem()
        {
            LabelLine1 = "";
            LabelLine2 = "";
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(LabelLine1) && string.IsNullOrEmpty(LabelLine2)) {
                return "(no labels)";
            }
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(LabelLine1)) {
                sb.Append(LabelLine1);
            }
            if (!string.IsNullOrEmpty(LabelLine1) && !string.IsNullOrEmpty(LabelLine2)) {
                sb.Append(" ");
            }
            if (!string.IsNullOrEmpty(LabelLine2)) {
                sb.Append(LabelLine2);
            }
            return sb.ToString();
        }
    }
}
