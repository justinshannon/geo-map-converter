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
		public int BcgGroup { get; set; }

        [XmlIgnore]
        public string Filter
        {
            get
            {
                if (LineDefaults != null)
                {
                    return LineDefaults.Filters.ToString();
                }

                if (TextDefaults != null)
                {
                    return TextDefaults.Filters.ToString();
                }

                if (SymbolDefaults != null)
                {
                    return SymbolDefaults.Filters.ToString();
                }

                return "";
            }
        }

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
