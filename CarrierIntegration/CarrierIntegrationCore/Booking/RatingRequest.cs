using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CarrierIntegrationCore.Booking
{
    [Serializable]
    [XmlRoot("Customer")]
    public class CustomerInfo
    {
        public string CustomerNumber {get;set;}
        public string Password { get; set; }
    }

    [Serializable]
    [XmlRoot("Shipper")]
    public class Shipper
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        [XmlArrayItem(ElementName = "A")]
        public List<string> Address { get; set; }
    }

    [Serializable]
    [XmlRoot("Receiver")]
    public class Receiver
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        [XmlArrayItem(ElementName="A")]
        public List<string> Address { get; set; }
    }

    [Serializable]
    public class PackageInfo
    {
        public string PackageType { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }

        public string HSCode { get; set; }
        public string ManufacturerCountry { get; set; }
        public int Amount { get; set; }
        public string AmountUnit { get; set; }
        public decimal ProductValue { get; set; }
    }

    [Serializable]
    [XmlRoot("Shipment")]
    public class ShipmentInfo
    {
        public string ServiceType { get; set; }
        [XmlArrayItem(IsNullable=false, ElementName="Package")]
        public List<PackageInfo> Packages { get; set; }
        public DimensionUnit DimensionUnit { get; set; }
        public WeightUnit WeightUnit { get; set; }
        public decimal DeclaredValue { get; set; }
        public string DeclaredValueCurrency { get; set; }
        public decimal Insurance { get; set; }
        public string InsuranceCurrency { get; set; }
        [XmlArrayItem(ElementName = "SpecialService")]
        public List<string> SpecialServices { get; set; }
        [XmlArrayItem(ElementName = "Accessorial")]
        public List<string> Accessorials { get; set; }
    }

    public enum DimensionUnit
    {
        IN, CM
    }

    public enum WeightUnit
    {
        LB, KG
    }

    public enum SpecialService
    {
        ALCOHOL, BROKER_SELECT_OPTION, CALL_BEFORE_DELIVERY, COD, CUSTOM_DELIVERY_WINDOW,
        DANGEROUS_GOODS, DO_NOT_BREAK_DOWN_PALLETS, DO_NOT_STACK_PALLETS, DRY_ICE, EAST_COAST_SPECIAL,
        ELECTRONIC_TRADE_DOCUMENTS, EMAIL_NOTIFICATION, EXHIBITION_DELIVERY, EXHIBITION_PICKUP,
        EXTREME_LENGTH, FLATBED_TRAILER, FOOD, FREIGHT_GUARANTEE, FUTURE_DAY_SHIPMENT, HOLD_AT_LOCATION,
        HOME_DELIVERY_PREMIUM, INSIDE_DELIVERY, INSIDE_PICKUP, LIFTGATE_DELIVERY, LIFTGATE_PICKUP,
        LIMITED_ACCESS_DELIVERY, LIMITED_ACCESS_PICKUP, PENDING_SHIPMENT, POISON, PRE_DELIVERY_NOTIFICATION,
        PROTECTION_FROM_FREEZING, REGIONAL_MALL_DELIVERY, REGIONAL_MALL_PICKUP, RETURN_SHIPMENT,
        SATURDAY_DELIVERY, SATURDAY_PICKUP, TOP_LOAD
    }

    public enum Accessorial
    {
        SpecialDelivery = 1, Appointment = 2, SaturdayPUD = 3, SundayPUD = 4, 
        HolidayPUD = 5, AfterHoursPUD = 6, Liftgate = 7, Inside = 8, PrivateResidence = 9, COD = 10
    }

    public class BORatingRequest
    {
        private BookingDataClassesDataContext context;

        public BORatingRequest()
        {
            context = new BookingDataClassesDataContext();
        }

        public void SaveRateRequest(string requestXML)
        {
            BookingRateRequest newRequest = new BookingRateRequest();
            newRequest.RequestXML = requestXML;
            newRequest.RequestDate = DateTime.Now;
            context.BookingRateRequests.InsertOnSubmit(newRequest);
            context.SubmitChanges();
        }
    }

}
