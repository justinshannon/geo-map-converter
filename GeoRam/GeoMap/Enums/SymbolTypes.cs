using System;

namespace GeoMapConverter.GeoRam.GeoMap.Enums
{
    [Serializable]
    public enum SymbolTypes
    {
        VOR,
        TACAN,
        OtherWaypoints,
        NDB,
        Airport,
        EmergencyAirport,
        SatelliteAirport,
        Obstruction1,
        Obstruction2,
        Heliport,
        Nuclear,
        Radar,
        IAF,
        RNAVOnlyWaypoint,
        RNAV,
        AirwayIntersections
    }
}