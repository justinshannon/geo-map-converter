using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeoMapConverter.GeoRam.GeoMap
{
    [Serializable]
    [XmlRoot("GeoMapRecord", IsNullable = false)]
    public class GeoMapRecord
    {
        public string GeomapId { get; set; }
        [XmlElement("BCGMenuName")] public string BcgMenuName { get; set; }
        public string FilterMenuName { get; set; }
        public string LabelLine1 { get; set; }
        public string LabelLine2 { get; set; }
        public string MinLatitude { get; set; }
        public string MaxLatitude { get; set; }
        public string MinLongitude { get; set; }
        public string MaxLongitude { get; set; }
        public double MinXSpherical { get; set; }
        public double MinYSpherical { get; set; }
        public double MinZSpherical { get; set; }
        public double MaxXSpherical { get; set; }
        public double MaxYSpherical { get; set; }
        public double MaxZSpherical { get; set; }
        [XmlElement("GeoMapObjectType")] public List<GeoMapObject> GeoMapObjectList { get; set; }
    }
}