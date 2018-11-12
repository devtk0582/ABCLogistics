using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarrierIntegrationCore.Booking
{
    public class BOCountries
    {
        BookingDataClassesDataContext context;
        private static List<string> euCountries = new List<string>(){
            "AT","BE","BG","CY", "CZ", "DK", "EE", "FI",
            "FR", "DE", "GR", "HU", "IE", "IT", "LV", "LT", "LU", "MT", "NL", "PL", "PT", "RO", "SK", "SI", "ES", "SE", "GB"};

        private static List<string> lbinCountries = new List<string>(){
            "AS", "FM", "GU", "MH", "MP", "PR", "US", "VI"
        };

        public BOCountries()
        {
            context = new BookingDataClassesDataContext();
        }

        public BookingCountry GetCountryCode(string countryInfo)
        {
            return (from country in context.BookingCountries
                    where country.CountryName.ToUpper() == countryInfo.ToUpper()
                       || country.Alpha2Code.ToUpper() == countryInfo.ToUpper()
                       || country.Alpha3Code.ToUpper() == countryInfo.ToUpper()
                    select country).SingleOrDefault();
        }

        public static bool IsEUCountries(string countryCode)
        {
            return euCountries.Any(o => o == countryCode);
        }

        public static bool IsLBINCountries(string countryCode)
        {
            return lbinCountries.Any(o => o == countryCode);
        }
    }
}
