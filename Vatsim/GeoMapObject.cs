using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace GeoMapConverter.Vatsim
{
	[Serializable]
	public class GeoMapObject
	{
		[XmlAttribute]
		public string Description { get; set; }

		[XmlAttribute]
		public bool TdmOnly { get; set; }

		public LineDefaults LineDefaults { get; set; }

		public TextDefaults TextDefaults { get; set; }

		public SymbolDefaults SymbolDefaults { get; set; }

		public List<Element> Elements { get; set; }

        [XmlIgnore]
        public List<int> FilterList { get; set; } = new List<int>();

        [XmlIgnore]
        public string Filter => string.Join(",", FilterList);

		[XmlIgnore]
		public bool HasElements => Elements != null && Elements.Count > 0;

        public GeoMapObject()
        {
            Description = "";
            Elements = new List<Element>();
        }

        public override string ToString()
		{
			return string.IsNullOrEmpty(Description) ? "(no description)" : Description;
		}
	}
}
