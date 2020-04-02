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
		public List<string> Filters { get; set; }

		[XmlIgnore]
		public string FilterList {
			get {
				if (Filters.Count > 0) {
					return string.Join(",", Filters);
				}
				return "";
			}
			set {
				if (!string.IsNullOrEmpty(value)) {
					Filters = value.Split(',').Select(a => a).ToList();
				}
			}
		}

		[XmlIgnore]
		public List<string> Filters2 {
			get {
				List<string> temp = new List<string>();
				if (LineDefaults != null) {
					if (LineDefaults.Filters != null) {
						temp.Add(LineDefaults.Filters);
					}
				}
				if (SymbolDefaults != null) {
					if (SymbolDefaults.Filters != null) {
						temp.Add(SymbolDefaults.Filters);
					}
				}
				if (TextDefaults != null) {
					if (TextDefaults.Filters != null) {
						temp.Add(TextDefaults.Filters);
					}
				}
				return temp;
			}
		}

		public GeoMapObject()
		{
			Description = "";
			Elements = new List<Element>();
			Filters = new List<string>();
		}

		public override string ToString()
		{
			return string.IsNullOrEmpty(Description) ? "(no description)" : Description;
		}
	}
}
