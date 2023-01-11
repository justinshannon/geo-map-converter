using System;
using System.Xml.Serialization;

namespace GeoMapConverter.Vatsim
{
	[Serializable]
	public class Symbol : Element
	{
		[XmlAttribute]
		public Vatsim.SymbolStyle Style { get; set; }

		[XmlIgnore]
		public bool StyleSpecified { get; set; }

		[XmlAttribute]
		public int Size { get; set; }

		[XmlIgnore]
		public bool SizeSpecified { get; set; }

		[XmlAttribute]
		public double Lat { get; set; }

		[XmlAttribute]
		public double Lon { get; set; }
	}
}