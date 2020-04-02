using System;
using System.Xml.Serialization;

namespace GeoMapConverter.Vatsim
{
	[Serializable]
	public class Text : Element
	{
		[XmlAttribute]
		public int Size { get; set; }

		[XmlAttribute]
		public bool Underline { get; set; }

		[XmlAttribute]
		public bool Opaque { get; set; }

		[XmlAttribute]
		public int XOffset { get; set; }

		[XmlAttribute]
		public int YOffset { get; set; }

		[XmlAttribute]
		public double Lat { get; set; }

		[XmlAttribute]
		public double Lon { get; set; }

		[XmlAttribute]
		public string Lines { get; set; }
	}
}
