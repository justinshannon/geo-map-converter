using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.Vatsim
{
    [Serializable]
    public class BcgMenuItem
    {
        [XmlAttribute("Label")]
        public string Label { get; set; }

        [XmlIgnore]
        public int MenuPosition { get; set; }

        [XmlIgnore]
        public int BcgGroups { get; set; }

        [XmlIgnore]
        public int LocalId { get; set; }

        public BcgMenuItem()
        {
            Label = "";
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Label) ? "(no label)" : Label;
        }
    }
}