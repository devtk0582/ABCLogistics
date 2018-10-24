using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABCLogisticsMapApp
{
    public class GeoLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public GeoLocation(double lat, double lng)
        {
            Latitude = lat;
            Longitude = lng;
        }
    }
}
