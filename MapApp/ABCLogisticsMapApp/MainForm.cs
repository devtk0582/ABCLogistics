using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using System.Reflection;

namespace ABCLogisticsMapApp
{
    public partial class MainForm : Form
    {
        public ILog logger;
        public MainForm()
        {
            InitializeComponent();
            logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        //public DistanceResponse GetDistance(DistanceRequestFromAddress fromAddress, DistanceRequestToAddress toAddress)
        //{
        //    DistanceResponse response = new DistanceResponse();
        //    try
        //    {
        //        if (string.IsNullOrEmpty(fromAddress.Address) || string.IsNullOrEmpty(fromAddress.City) || string.IsNullOrEmpty(fromAddress.State) || string.IsNullOrEmpty(fromAddress.ZipCode)
        //            || string.IsNullOrEmpty(toAddress.Address) || string.IsNullOrEmpty(toAddress.City) || string.IsNullOrEmpty(toAddress.State) || string.IsNullOrEmpty(toAddress.ZipCode)
        //            )
        //        {
        //            response.ErrorMessage = "Address / City / State / Zip are required";
        //            return response;
        //        }

        //        GeoLocation fromLocation = GeocodeAddress(fromAddress.Address, fromAddress.City, fromAddress.State, fromAddress.ZipCode);
        //        GeoLocation toLocation = GeocodeAddress(toAddress.Address, toAddress.City, toAddress.State, toAddress.ZipCode);

        //        if (fromLocation != null && toLocation != null)
        //        {
        //            response.Distance = GetRouteDistance(fromLocation, toLocation);

        //            if (response.Distance != "")
        //            {
        //                response.Status = "Y";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (m_log != null) m_log.Error(ex.ToString());
        //        response.ErrorMessage = ex.Message;
        //    }

        //    return response;
        //}

        private GeoLocation GeocodeAddress(string address, string city, string state, string zipcode)
        {
            GeoLocation result = null;
            BingGeocodeServiceReference.GeocodeRequest geocodeRequest = null;
            BingGeocodeServiceReference.GeocodeResponse geocodeResponse = null;

            try
            {
                geocodeRequest = new BingGeocodeServiceReference.GeocodeRequest();

                // Set the credentials using a valid Bing Maps key
                geocodeRequest.Credentials = new BingGeocodeServiceReference.Credentials();
                geocodeRequest.Credentials.ApplicationId = "xxx";

                // Set the full address query
                geocodeRequest.Address = new BingGeocodeServiceReference.Address()
                {
                    //AddressLine = address,
                    //AdminDistrict = state == "PR" ? "" : state,
                    CountryRegion = "US",
                    PostalCode = zipcode
                    //Locality = city
                };

                if (!string.IsNullOrEmpty(state))
                    geocodeRequest.Address.AdminDistrict = state == "PR" ? "" : state;

                if (!string.IsNullOrEmpty(city))
                    geocodeRequest.Address.Locality = city;

                // Set the options to only return high confidence results 
                //BingGeocodeServiceReference.ConfidenceFilter[] filters = new BingGeocodeServiceReference.ConfidenceFilter[1];
                //filters[0] = new BingGeocodeServiceReference.ConfidenceFilter();
                //filters[0].MinimumConfidence = BingGeocodeServiceReference.Confidence.High;

                // Add the filters to the options
                BingGeocodeServiceReference.GeocodeOptions geocodeOptions = new BingGeocodeServiceReference.GeocodeOptions();
                geocodeOptions.Count = 2;
                geocodeRequest.Options = geocodeOptions;


                // Make the geocode request
                BingGeocodeServiceReference.GeocodeServiceClient geocodeService = new BingGeocodeServiceReference.GeocodeServiceClient("BasicHttpBinding_IGeocodeService");

                geocodeResponse = geocodeService.Geocode(geocodeRequest);

                if (geocodeResponse.Results.Length > 0)
                {
                    result = new GeoLocation(Convert.ToDouble(geocodeResponse.Results[0].Locations[0].Latitude), Convert.ToDouble(geocodeResponse.Results[0].Locations[0].Longitude));
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                result = null;
            }
            finally
            {
                geocodeResponse = null;
                geocodeRequest = null;
            }
            return result;
        }

        //public double CalculateGeoDistance(double Lat1,
        //          double Long1, double Lat2, double Long2)
        //{
        //    /*
        //        The Haversine formula according to Dr. Math.
        //        http://mathforum.org/library/drmath/view/51879.html
                
        //        dlon = lon2 - lon1
        //        dlat = lat2 - lat1
        //        a = (sin(dlat/2))^2 + cos(lat1) * cos(lat2) * (sin(dlon/2))^2
        //        c = 2 * atan2(sqrt(a), sqrt(1-a)) 
        //        d = R * c
                
        //        Where
        //            * dlon is the change in longitude
        //            * dlat is the change in latitude
        //            * c is the great circle distance in Radians.
        //            * R is the radius of a spherical Earth.
        //            * The locations of the two points in 
        //                spherical coordinates (longitude and 
        //                latitude) are lon1,lat1 and lon2, lat2.
        //    */
        //    double dDistance = Double.MinValue;
        //    double dLat1InRad = Lat1 * (Math.PI / 180.0);
        //    double dLong1InRad = Long1 * (Math.PI / 180.0);
        //    double dLat2InRad = Lat2 * (Math.PI / 180.0);
        //    double dLong2InRad = Long2 * (Math.PI / 180.0);

        //    double dLongitude = dLong2InRad - dLong1InRad;
        //    double dLatitude = dLat2InRad - dLat1InRad;

        //    // Intermediate result a.
        //    double a = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
        //               Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
        //               Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

        //    // Intermediate result c (great circle distance in Radians).
        //    double c = 2.0 * Math.Asin(Math.Sqrt(a));

        //    // Distance.
        //    // const Double kEarthRadiusMiles = 3956.0;
        //    //const Double kEarthRadiusKms = 6376.5;
        //    dDistance = 3956.0 * c;

        //    return dDistance;
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            ABCLogisticsMapDataClassesDataContext context = new ABCLogisticsMapDataClassesDataContext();

            var zipcodes = (from o in context.NeedMiles
                            where o.miles == 0
                            select o).ToList();

            foreach (NeedMile zipcode in zipcodes)
            {
                double distance = GetRouteDistance(new GeoLocation(zipcode.FromLat.Value, zipcode.FromLng.Value), new GeoLocation(zipcode.ToLat.Value, zipcode.ToLng.Value));

                if (distance > 0)
                {
                    zipcode.miles = distance;
                    context.SubmitChanges();
                }
            }

            //context.SubmitChanges();

        }

        private double GetRouteDistance(GeoLocation fromAddress, GeoLocation toAddress)
        {
            double result = 0;
            try
            {
                BingRouteServiceReference.RouteRequest routeRequest = new BingRouteServiceReference.RouteRequest();

                // Set the credentials using a valid Bing Maps key
                routeRequest.Credentials = new BingRouteServiceReference.Credentials();
                routeRequest.Credentials.ApplicationId = "xxx";

                //Parse user data to create array of waypoints
                BingRouteServiceReference.Waypoint[] waypoints = new BingRouteServiceReference.Waypoint[2];
                waypoints[0] = new BingRouteServiceReference.Waypoint()
                {
                    Location = new BingRouteServiceReference.Location()
                    {
                        Latitude = Convert.ToDouble(fromAddress.Latitude),
                        Longitude = Convert.ToDouble(fromAddress.Longitude),
                    },
                    Description = "Start"
                };

                waypoints[1] = new BingRouteServiceReference.Waypoint()
                {
                    Location = new BingRouteServiceReference.Location()
                    {
                        Latitude = Convert.ToDouble(toAddress.Latitude),
                        Longitude = Convert.ToDouble(toAddress.Longitude),
                    },
                    Description = "End"
                };

                routeRequest.Waypoints = waypoints;

                routeRequest.UserProfile = new BingRouteServiceReference.UserProfile()
                {
                    DistanceUnit = BingRouteServiceReference.DistanceUnit.Mile
                };

                // Make the calculate route request
                BingRouteServiceReference.RouteServiceClient routeService = new BingRouteServiceReference.RouteServiceClient("BasicHttpBinding_IRouteService");
                BingRouteServiceReference.RouteResponse routeResponse = routeService.CalculateRoute(routeRequest);

                if (routeResponse.Result != null && routeResponse.Result.Summary != null && routeResponse.Result.Summary.Distance > 0)
                    result = routeResponse.Result.Summary.Distance;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                result = 0;
            }

            return result;
        }


    }
}
