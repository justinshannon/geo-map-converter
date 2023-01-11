using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.Vatsim
{
	[Serializable]
	public class Line : Element
	{
		[XmlAttribute]
		public LineStyle Style { get; set; }

		[XmlIgnore]
		public bool StyleSpecified { get; set; }

		[XmlAttribute]
		public int Thickness { get; set; }

		[XmlIgnore]
		public bool ThicknessSpecified { get; set; }

		[XmlAttribute]
		public double StartLat { get; set; }

		[XmlAttribute]
		public double StartLon { get; set; }

		[XmlAttribute]
		public double EndLat { get; set; }

		[XmlAttribute]
		public double EndLon { get; set; }

		[XmlIgnore]
		public int BcgGroup { get; set; }

		[XmlIgnore]
		public List<int> FilterGroup { get; set; }
	}
}