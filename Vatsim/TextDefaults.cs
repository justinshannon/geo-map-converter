using System;
using System.Xml.Serialization;

namespace GeoMapConverter.Vatsim
{
	[Serializable]
	public class TextDefaults : ElementDefaults
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

		public TextDefaults()
		{
			Size = 1;
		}
	}
}