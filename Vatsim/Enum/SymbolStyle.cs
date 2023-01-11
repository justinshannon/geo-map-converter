using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMapConverter.Vatsim
{
    [Serializable]
    public enum SymbolStyle
    {
		Obstruction1,
		Obstruction2,
		Heliport,
		Nuclear,
		EmergencyAirport,
		Radar,
		IAF,
		RNAVOnlyWaypoint,
		RNAV,
		AirwayIntersections,
		NDB,
		VOR,
		OtherWaypoints,
		Airport,
		SatelliteAirport,
		TACAN,
		DME
	}
}
