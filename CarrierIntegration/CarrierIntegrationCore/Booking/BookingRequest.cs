using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CarrierIntegrationCore.Booking
{
    [Serializable]
    [XmlRoot("Shipper")]
    public class BookingShipper
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        [XmlArrayItem(ElementName = "A")]
        public List<string> Address { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    [Serializable]
    [XmlRoot("Receiver")]
    public class BookingReceiver
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        [XmlArrayItem(ElementName = "A")]
        public List<string> Address { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    [Serializable]
    [XmlRoot("Shipment")]
    public class BookingPickUpInfo
    {
        public List<Package> Packages { get; set; }
        public WeightUnit WeightUnit { get; set; }
        public DimensionUnit DimensionUnit { get;set;}
        public string ShipDate { get; set; }
        public string ReadyTime { get; set; }
        public string CloseTime { get; set; }
        public string DeliveryDate { get; set; }
        public ServiceLevel Service { get; set; }
        public string SpecialInstruction { get; set; }
        public decimal DeclaredValue { get; set; }
        public string DeclaredValueCurrency { get; set; }
        public decimal Insurance { get; set; }
        public string InsuranceCurrency { get; set; }
        [XmlArrayItem(ElementName = "SpecialService")]
        public List<string> SpecialServices { get; set; }
        [XmlArrayItem(ElementName = "Accessorial")]
        public List<string> Accessorials { get; set; }
        public string ReferenceNumber { get; set; }
    }

    public enum ServiceLevel
    {
        TBD = 1, NBDP = 2, NBDA = 3, THBD = 4,
        ECO = 5, VIP = 6, GE = 10, LTL = 11
    }
     
    [Serializable]
    public class Package
    {
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public string Description { get; set; }
        public string HSCode { get; set; }
        public string ManufacturerCountry { get; set; }
        public int Amount { get; set; }
        public string AmountUnit { get; set; }
        public decimal ProductValue { get; set; }
    }

    public class BOBookingRequest
    {
        private BookingDataClassesDataContext context;

        public BOBookingRequest()
        {
            context = new BookingDataClassesDataContext();
        }

        public void SaveBookRequest(string requestXML)
        {
            BookingBookRequest newRequest = new BookingBookRequest();
            newRequest.BookXML = requestXML;
            newRequest.BookDate = DateTime.Now;
            context.BookingBookRequests.InsertOnSubmit(newRequest);
            context.SubmitChanges();
        }
    }
}
