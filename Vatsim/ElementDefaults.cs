using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace GeoMapConverter.Vatsim
{
	[Serializable]
	[XmlInclude(typeof(SymbolDefaults))]
	[XmlInclude(typeof(TextDefaults))]
	[XmlInclude(typeof(LineDefaults))]
	public class ElementDefaults
	{
		[XmlAttribute("Bcg")]
		public string Bcg { get; set; }

		[XmlAttribute("Filters")]
		public string Filters { get; set; }

		/*[XmlIgnore]
		public string FilterList {
			get {
				if (FiltersList.Count > 0) {
					return string.Join(",", FiltersList);
				}
				return "";
			}
			set {
				if (!string.IsNullOrEmpty(value)) {
					Filters = value.Split(',').Select(int.Parse).ToList();
				}
			}
		}*/

		public ElementDefaults()
		{
			Filters = "";
			Bcg = "";
		}
	}
}
