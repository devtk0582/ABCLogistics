using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace CarrierIntegrationCore.Booking
{
    public class XmlGenerator
    {
        private MemoryStream stream = null;
        private XmlTextWriter xmlWriter = null;

        public string CompileRateRequestFile(CustomerInfo customer, ShipmentInfo shipment, Shipper shipper, Receiver receiver, RatingResponse response)
        {
            stream = new MemoryStream();
            xmlWriter = new XmlTextWriter(stream, Encoding.UTF8);

            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("RateRequest");

            ProcessRateRequest(customer, shipment, shipper, receiver, response);

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();

            byte[] buffer = stream.ToArray();

            return Encoding.UTF8.GetString(buffer);
        }

        private void AddElement(string strName, string strContent)
        {
            xmlWriter.WriteStartElement(strName, "");
            xmlWriter.WriteString(strContent);
            xmlWriter.WriteEndElement();
        }

        private void ProcessRateRequest(CustomerInfo customer, ShipmentInfo shipment, Shipper shipper, Receiver receiver, RatingResponse response)
        {
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
            if (shipment == null)
            {
                throw new Exception("Shipment not found");
            }
            if (shipper == null)
            {
                throw new Exception("Shipper not found");
            }
            if (receiver == null)
            {
                throw new Exception("Receiver not found");
            }
            if (response == null)
            {
                throw new Exception("Response not found");
            }
            xmlWriter.WriteStartElement("Customer", "");
            AddElement("CustomerNumber", customer.CustomerNumber);
            AddElement("Password", customer.Password);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("Shipment", "");
            xmlWriter.WriteStartElement("Packages", "");
            foreach (PackageInfo packageInfo in shipment.Packages)
            {
                xmlWriter.WriteStartElement("Package", "");
                AddElement("Weight", packageInfo.Weight.ToString());
                AddElement("Height", packageInfo.Height.ToString());
                AddElement("Length", packageInfo.Length.ToString());
                AddElement("Width", packageInfo.Width.ToString());
                AddElement("HSCode", packageInfo.HSCode);
                AddElement("Amount", packageInfo.Amount.ToString());
                AddElement("ProductValue", packageInfo.ProductValue.ToString());
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            AddElement("DimensionUnit", string.IsNullOrEmpty(shipment.DimensionUnit.ToString()) ? "IN" : shipment.DimensionUnit.ToString());
            AddElement("WeightUnit", string.IsNullOrEmpty(shipment.WeightUnit.ToString()) ? "LB" : shipment.WeightUnit.ToString());
            if (shipment.DeclaredValue > 0)
                AddElement("DeclaredValue", shipment.DeclaredValue.ToString());
            if (!string.IsNullOrEmpty(shipment.DeclaredValueCurrency))
                AddElement("DeclaredValueCurrency", shipment.DeclaredValueCurrency);
            if (shipment.Insurance > 0)
                AddElement("Insurance", shipment.Insurance.ToString());
            if (!string.IsNullOrEmpty(shipment.InsuranceCurrency))
                AddElement("InsuranceCurrency", shipment.InsuranceCurrency);
            if (shipment.SpecialServices != null && shipment.SpecialServices.Count > 0)
            {
                foreach (string specialService in shipment.SpecialServices)
                {
                    AddElement("SpecialServiceTypes", specialService);
                }
            }
            if (shipment.Accessorials != null && shipment.Accessorials.Count > 0)
            {
                foreach (string accessorials in shipment.Accessorials)
                {
                    AddElement("Accessorials", accessorials);
                }
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("Shipper", "");
            AddElement("Country", shipper.Country);
            AddElement("State", shipper.State);
            AddElement("City", shipper.City);
            AddElement("PostalCode", shipper.PostalCode);
            if (shipper.Address != null)
            {
                xmlWriter.WriteStartElement("Address", "");
                foreach (string address in shipper.Address)
                {
                    AddElement("A", address);
                }
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("Receiver", "");
            AddElement("Country", receiver.Country);
            AddElement("State", receiver.State);
            AddElement("City", receiver.City);
            AddElement("PostalCode", receiver.PostalCode);
            if (receiver.Address != null)
            {
                xmlWriter.WriteStartElement("Address", "");
                foreach (string address in receiver.Address)
                {
                    AddElement("A", address);
                }
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("Response", "");
            xmlWriter.WriteStartElement("ErrorMessage", "");
            AddElement("ErrorCode", response.ErrorMessage.ErrorCode);
            AddElement("ErrorMessage", response.ErrorMessage.ErrorMessage);
            xmlWriter.WriteEndElement();
            AddElement("FreightCharge", response.FreightCharge.ToString());
            AddElement("DutiesAndTaxes", response.DutiesAndTaxes.ToString());
            AddElement("TotalCharges", response.TotalCharge.ToString());
            xmlWriter.WriteEndElement();
        }

        //private void ProcessBookRequest(CustomerInfo customer, BookingPickUpInfo shipment, BookingPickUpLocation shipper, BookingResponse response)
        //{
        //    if (customer == null)
        //    {
        //        throw new Exception("Customer not found");
        //    }
        //    if (shipment == null)
        //    {
        //        throw new Exception("Shipment not found");
        //    }
        //    if (shipper == null)
        //    {
        //        throw new Exception("Shipper not found");
        //    }
        //    if (response == null)
        //    {
        //        throw new Exception("Response not found");
        //    }
        //    xmlWriter.WriteStartElement("Customer", "");
        //    AddElement("CustomerNumber", customer.CustomerNumber);
        //    AddElement("Password", customer.Password);
        //    xmlWriter.WriteEndElement();
        //    xmlWriter.WriteStartElement("Shipment", "");
        //    AddElement("TotalWeight", shipment.TotalWeight.ToString());
        //    AddElement("PackageCount", shipment.PackageCount.ToString());
        //    if (shipment.Packages != null && shipment.Packages.Count > 0)
        //    {
        //        xmlWriter.WriteStartElement("Packages", "");
        //        foreach (Package package in shipment.Packages)
        //        {
        //            xmlWriter.WriteStartElement("Package", "");
        //            AddElement("Weight", package.Weight.ToString());
        //            AddElement("Height", package.Height.ToString());
        //            AddElement("Length", package.Length.ToString());
        //            AddElement("Width", package.Width.ToString());
        //            xmlWriter.WriteEndElement();
        //        }
        //        xmlWriter.WriteEndElement();
        //    }
        //    AddElement("LocationType", shipment.LocationType.ToString());
        //    AddElement("WeightUnit", string.IsNullOrEmpty(shipment.WeightUnit.ToString()) ? "LB" : shipment.WeightUnit.ToString());
        //    AddElement("DimensionUnit", string.IsNullOrEmpty(shipment.DimensionUnit.ToString()) ? "IN" : shipment.DimensionUnit.ToString());
        //    AddElement("PickUpDate", shipment.PickUpDate);
        //    AddElement("ReadyTime", shipment.ReadyTime);
        //    AddElement("CloseTime", shipment.CloseTime);
        //    AddElement("DoorTo", shipment.DoorTo.ToString());
        //    if (shipment.Insurance > 0)
        //        AddElement("Insurance", shipment.Insurance.ToString());
        //    if (!string.IsNullOrEmpty(shipment.InsuranceCurrency))
        //        AddElement("InsuranceCurrency", shipment.InsuranceCurrency);
        //    if (shipment.SpecialServices != null && shipment.SpecialServices.Count > 0)
        //    {
        //        foreach (string specialService in shipment.SpecialServices)
        //        {
        //            AddElement("SpecialServiceTypes", specialService);
        //        }
        //    }

        //    if (shipment.DomSettings != null)
        //    {
        //        xmlWriter.WriteStartElement("DomSettings", "");
        //        AddElement("ReceiverName", shipment.DomSettings.ReceiverName);
        //        AddElement("ReceiverCompany", shipment.DomSettings.ReceiverCompanyName);
        //        if (shipment.DomSettings.ReceiverAddress != null)
        //        {
        //            xmlWriter.WriteStartElement("ReceiverAddress", "");
        //            foreach (string address in shipment.DomSettings.ReceiverAddress)
        //            {
        //                AddElement("A", address);
        //            }
        //            xmlWriter.WriteEndElement();
        //        }
        //        AddElement("ReceiverCity", shipment.DomSettings.ReceiverCity);
        //        AddElement("ReceiverState", shipment.DomSettings.ReceiverState);
        //        AddElement("ReceiverZipCode", shipment.DomSettings.ReceiverZipcode);
        //        AddElement("ReceiverPhone", shipment.DomSettings.ReceiverPhone);
        //        AddElement("DeliveryDate", shipment.DomSettings.DeliveryDate);
        //        AddElement("Service", shipment.DomSettings.Service.ToString());
        //        AddElement("ReferenceNumber", shipment.DomSettings.ReferenceNumber);
        //        xmlWriter.WriteEndElement();
        //    }
        //    xmlWriter.WriteEndElement();
        //    //Shipment ends here.
        //    xmlWriter.WriteStartElement("Shipper", "");

        //    xmlWriter.WriteStartElement("Shipper", "");
        //    AddElement("Country", shipper.Country);
        //    AddElement("State", shipper.State);
        //    AddElement("City", shipper.City);
        //    AddElement("PostalCode", shipper.PostalCode);
        //    if (shipper.Address != null)
        //    {
        //        xmlWriter.WriteStartElement("Address", "");
        //        foreach (string address in shipper.Address)
        //        {
        //            AddElement("A", address);
        //        }
        //        xmlWriter.WriteEndElement();
        //    }
        //    AddElement("DestinationCountry", shipper.DestinationCountry);
        //    AddElement("CompanyName", shipper.CompanyName);
        //    AddElement("BookerName", shipper.BookerName);
        //    AddElement("BookerPhone", shipper.BookerPhone);
        //    if (!string.IsNullOrEmpty(shipper.BookerPhoneExt))
        //        AddElement("BookerPhoneExt", shipper.BookerPhoneExt);
        //    AddElement("ContactName", shipper.ContactName);
        //    AddElement("ContactPhone", shipper.ContactPhone);
        //    if (!string.IsNullOrEmpty(shipper.ContactPhoneExt))
        //        AddElement("ContactPhoneExt", shipper.ContactPhoneExt);
        //    xmlWriter.WriteEndElement();
        //    //Shipper Ends Here.
        //    xmlWriter.WriteStartElement("Response", "");
        //    xmlWriter.WriteStartElement("ErrorMessage", "");
        //    AddElement("ErrorCode", response.ErrorMessage.ErrorCode);
        //    AddElement("ErrorMessage", response.ErrorMessage.ErrorMessage);
        //    xmlWriter.WriteEndElement();
        //    AddElement("ConfirmationNumber", response.ConfirmationNumber);
        //    if (!string.IsNullOrEmpty(response.PickupDate))
        //        AddElement("PickupDate", response.PickupDate);
        //    if (!string.IsNullOrEmpty(response.ReadyByTime))
        //        AddElement("ReadyByTime", response.ReadyByTime.ToString());
        //    xmlWriter.WriteEndElement();
        //}

        
    }
}
