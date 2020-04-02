using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GeoMapConverter.GeoRam.GeoMap.Enums;

namespace GeoMapConverter.GeoRam.GeoMap
{
    [Serializable]
    public class GeoMapLine
    {
        public string LineObjectId { get; set; }

        public string StartLatitude { get; set; }

        public string StartLongitude { get; set; }

        public string EndLatitude { get; set; }

        public string EndLongitude { get; set; }

        public double StartXSpherical { get; set; }

        public double StartYSpherical { get; set; }

        public double StartZSpherical { get; set; }

        public double EndXSpherical { get; set; }

        public double EndYSpherical { get; set; }

        public double EndZSpherical { get; set; }
    }
}