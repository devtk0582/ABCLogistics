using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarrierIntegrationCore.Booking
{
    public class BOServices
    {
         BookingDataClassesDataContext context;

         public BOServices()
        {
            context = new BookingDataClassesDataContext();
        }

         public ServiceDetails GetServiceDetails(string originCountry, string destCountry, int customerId)
         {
             string originCountryCode = originCountry, destCountryCode = destCountry;

             if (originCountry != "US" || destCountry != "US")
             {
                 if (originCountry == "US" && destCountry != "CA" && destCountry != "MX" && destCountry != "US")
                 {
                     destCountryCode = "NONUS";
                 }
                 else if (destCountry == "US" && originCountry != "CA" && originCountry != "MX" && originCountry != "US")
                 {
                     originCountryCode = "NONUS";
                 }
                 else if ((originCountry == "CA" && destCountry != "US" && destCountry != "MX" && destCountry != "CA")
                     || (originCountry == "MX" && destCountry != "CA" && destCountry != "US" && destCountry != "MX")
                     || (destCountry == "CA" && originCountry != "MX" && originCountry != "US" && originCountry != "CA")
                     || (destCountry == "MX" && originCountry != "CA" && originCountry != "US" && originCountry != "MX")
                     || (originCountry != "MX" && originCountry != "CA" && originCountry != "US" && destCountry != "MX" && destCountry != "CA" && destCountry != "US"))
                 {
                     originCountryCode = "NONUS";
                     destCountryCode = "NONUS";
                 }
             }
             
             ServiceDetails serviceCredential = (from carrier in context.BookingCarriers
                                                 join service in context.BookingServices on carrier.CarrierID equals service.CarrierID
                                                 join condition in context.BookingConditions on service.ConditionID equals condition.BookingConditionID
                                                 where condition.OriginCountry == originCountryCode
                                                 && condition.DestCountry == destCountryCode
                                                 && carrier.BookingCustomerID == customerId
                                                 select new ServiceDetails()
                                                 {
                                                     ServiceID = service.BookingServiceID,
                                                     ServiceCode = service.ServiceCode,
                                                     ServiceName = service.ServiceName,
                                                     CarrierID = carrier.CarrierID,
                                                     AccountName = carrier.AccountName,
                                                     AccountNumber = carrier.AccountNumber,
                                                     CarrierName = carrier.CarrierName,
                                                     Password = carrier.Password,
                                                     License = carrier.License
                                                 }).SingleOrDefault();

             return serviceCredential;
         }

         public List<ServiceDetails> GetServicesDetails(string originCountry, string destCountry, int customerId)
         {
             string originCountryCode = originCountry, destCountryCode = destCountry;

             if (originCountry != "US" || destCountry != "US")
             {
                 if (originCountry == "US" && destCountry != "CA" && destCountry != "MX" && destCountry != "US")
                 {
                     destCountryCode = "NONUS";
                 }
                 else if (destCountry == "US" && originCountry != "CA" && originCountry != "MX" && originCountry != "US")
                 {
                     originCountryCode = "NONUS";
                 }
                 else if ((originCountry == "CA" && destCountry != "US" && destCountry != "MX" && destCountry != "CA")
                     || (originCountry == "MX" && destCountry != "CA" && destCountry != "US" && destCountry != "MX")
                     || (destCountry == "CA" && originCountry != "MX" && originCountry != "US" && originCountry != "CA")
                     || (destCountry == "MX" && originCountry != "CA" && originCountry != "US" && originCountry != "MX")
                     || (originCountry != "MX" && originCountry != "CA" && originCountry != "US" && destCountry != "MX" && destCountry != "CA" && destCountry != "US"))
                 {
                     originCountryCode = "NONUS";
                     destCountryCode = "NONUS";
                 }
             }

             List<ServiceDetails> serviceCredentials = (from carrier in context.BookingCarriers
                                                 join service in context.BookingServices on carrier.CarrierID equals service.CarrierID
                                                 join condition in context.BookingConditions on service.ConditionID equals condition.BookingConditionID
                                                 where condition.OriginCountry == originCountryCode
                                                 && condition.DestCountry == destCountryCode
                                                 && carrier.BookingCustomerID == customerId
                                                 orderby service.Priority
                                                 select new ServiceDetails()
                                                 {
                                                     ServiceID = service.BookingServiceID,
                                                     ServiceCode = service.ServiceCode,
                                                     ServiceName = service.ServiceName,
                                                     CarrierID = carrier.CarrierID,
                                                     AccountName = carrier.AccountName,
                                                     AccountNumber = carrier.AccountNumber,
                                                     CarrierName = carrier.CarrierName,
                                                     Password = carrier.Password,
                                                     License = carrier.License
                                                 }).ToList();

             return serviceCredentials;
         }
    }
}
