using System;
using System.Text.RegularExpressions;

namespace GeoMapConverter.Utils
{
    public static class StringUtils
    {
        public static double ToDecimalDegrees(string value, bool isLat = true)
        {
            double deg;
            double min;
            double sec;

            var multiplier = value.Contains("S") || value.Contains("W") ? -1 : 1;
            value = Regex.Replace(value, "[^0-9.]", "");

            if (isLat)
            {
                deg = double.Parse(value.Substring(0, 2));
                min = double.Parse(value.Substring(2, 2));
                sec = double.Parse(value.Substring(4, 2) + "." + value.Substring(6, 2));
                return Math.Round((deg + min / 60 + sec / 3600) * multiplier, 6);
            }

            deg = double.Parse(value.Substring(0, 3));
            min = double.Parse(value.Substring(3, 2));
            sec = double.Parse(value.Substring(5, 2) + "." + value.Substring(7, 2));
            return Math.Round((deg + min / 60 + sec / 3600) * multiplier, 6);
        }
    }
}
