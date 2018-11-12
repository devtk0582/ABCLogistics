using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Web.Services.Protocols;
using CarrierIntegrationCore.Booking.RateServiceWebReference;
using log4net;
using System.Web;
using System.Reflection;
using System.Xml.Serialization;
using CarrierIntegrationCore.RateServiceWebReference;

namespace CarrierIntegrationCore.Booking
{
    public class BORating
    {
        private ILog m_log;

        public BORating()
        {
            m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public RatingResponse GetRates(CustomerInfo customer, ShipmentInfo shipment, Shipper shipper, Receiver receiver)
        {
            DateTime requestDateTime = DateTime.Now;
            DateTime responseTime;
            RatingResponse response = new RatingResponse();
            try
            {
                BOCountries boCountries = new BOCountries();
                BookingCountry originCountry = boCountries.GetCountryCode(shipper.Country);
                if (originCountry == null)
                {
                    response.ErrorMessage.ErrorCode = "001";
                    response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SHIPPER_COUNTRY_INVALID;
                    return response;
                }
                shipper.Country = originCountry.Alpha2Code;

                BookingCountry destCountry = boCountries.GetCountryCode(receiver.Country);
                if (destCountry == null)
                {
                    response.ErrorMessage.ErrorCode = "002";
                    response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_RECEIVER_COUNTRY_INVALID;
                    return response;
                }
                receiver.Country = destCountry.Alpha2Code;

                if (Validation.ValidateRateRequest(customer, shipment, shipper, receiver, response) == true)
                {
                    BookingCustomer customerDetails = (new BOCustomers()).AuthenticationCustomer(customer.CustomerNumber, customer.Password);
                    if (customerDetails != null && customerDetails.CustomerID > 0)
                    {
                        List<ServiceDetails> servicesDetails = (new BOServices()).GetServicesDetails(originCountry.Alpha2Code, destCountry.Alpha2Code, customerDetails.CustomerID);

                        if (servicesDetails != null && servicesDetails.Count > 0)
                        {
                            string carrierName = "";

                            bool convertDimension = false, convertWeight = false;
                            if (BOCountries.IsLBINCountries(shipper.Country))
                            {
                                if (shipment.DimensionUnit == DimensionUnit.CM)
                                {
                                    convertDimension = true;
                                    shipment.DimensionUnit = DimensionUnit.IN;
                                }

                                if (shipment.WeightUnit == WeightUnit.KG)
                                {
                                    convertWeight = true;
                                    shipment.WeightUnit = WeightUnit.LB;
                                }

                                if (convertDimension || convertWeight)
                                    ConvertDimensionWeight(shipment.Packages, convertDimension, convertWeight, false, false);
                            }
                            else
                            {
                                if (shipment.DimensionUnit == DimensionUnit.IN)
                                {
                                    convertDimension = true;
                                    shipment.DimensionUnit = DimensionUnit.CM;
                                }

                                if (shipment.WeightUnit == WeightUnit.LB)
                                {
                                    convertWeight = true;
                                    shipment.WeightUnit = WeightUnit.KG;
                                }

                                if (convertDimension || convertWeight)
                                    ConvertDimensionWeight(shipment.Packages, convertDimension, convertWeight, true, true);
                            }

                            for (int i = 0; i < servicesDetails.Count; i++)
                            {
                                GetCarrierRate(shipment, shipper, receiver, customerDetails, servicesDetails[i], originCountry.Alpha3Code, destCountry.Alpha3Code, response);
                                if (response.TotalCharge > 0)
                                {
                                    response.ErrorMessage.ErrorCode = "";
                                    response.ErrorMessage.ErrorMessage = "";
                                    carrierName = servicesDetails[i].CarrierName;
                                    break;
                                }
                            }

                            SaveSuccessfulRequest(customer, shipment, shipper, receiver, response);
                            responseTime = DateTime.Now;
                            if (response.TotalCharge > 0 && !string.IsNullOrEmpty(customerDetails.NotificationEmails))
                            {
                                string[] emails = customerDetails.NotificationEmails.Split(';');

                                if (emails.Length > 0)
                                {
                                    List<string> trackEmails = new List<string>();
                                    foreach (string email in emails)
                                    {
                                        if (Validation.IsEmail(email.Trim()))
                                            trackEmails.Add(email.Trim());
                                    }

                                    SendEmailNotifications(carrierName, customerDetails.CustomerNumber, shipment, shipper, receiver, response, trackEmails, requestDateTime, responseTime);
                                }
                            }
                            else if (response.TotalCharge == 0 && string.IsNullOrEmpty(response.ErrorMessage.ErrorMessage))
                            {
                                response.ErrorMessage.ErrorCode = "600";
                                response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
                            }
                        }
                        else
                        {
                            response.ErrorMessage.ErrorCode = "017";
                            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SERVICE_UNAVAILABLE;
                        }
                    }
                    else
                    {
                        response.ErrorMessage.ErrorCode = "018";
                        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_LOG_IN_FAILED;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage.ErrorCode = "500";
                response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
                if (m_log != null) m_log.Error(ex.ToString());
            }

            return response;
        }

        private decimal GetCarrierRate(ShipmentInfo shipment, Shipper shipper, Receiver receiver, BookingCustomer customerDetails, ServiceDetails serviceDetails, string origCountryAlpha3Code, string destCountryAlpha3Code, RatingResponse response)
        {
            decimal totalRate = 0;
            try
            {
                switch (serviceDetails.CarrierName)
                {
                    case "FEDEX":
                        if (CheckFedexRules(serviceDetails, shipment))
                        {
                            response.FreightCharge = RequestFedexRate(serviceDetails, shipment, shipper, receiver, response);
                            if (response.FreightCharge != 0)
                            {
                                if (customerDetails.MarkUp.HasValue)
                                    response.FreightCharge *= (customerDetails.MarkUp.Value + 1);
                                response.DutiesAndTaxes = CalculateDutiesAndTaxes(serviceDetails, shipment, shipper, receiver, response.FreightCharge, origCountryAlpha3Code, destCountryAlpha3Code, response);
                                response.TotalCharge = response.FreightCharge + response.DutiesAndTaxes;
                            }
                        }
                        else
                        {
                            response.ErrorMessage.ErrorCode = "016";
                            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SHIPMENT_OVERLIMIT;
                        }
                        break;
                    case "DHL":
                        if (CheckDHLRules(serviceDetails, shipment))
                        {
                            response.FreightCharge = RequestDHLQuote(serviceDetails, shipment, shipper, receiver, response);
                            if (response.FreightCharge != 0)
                            {
                                if (customerDetails.MarkUp.HasValue)
                                    response.FreightCharge *= (customerDetails.MarkUp.Value + 1);
                                response.DutiesAndTaxes = CalculateDutiesAndTaxes(serviceDetails, shipment, shipper, receiver, response.FreightCharge, origCountryAlpha3Code, destCountryAlpha3Code, response);
                                response.TotalCharge = response.FreightCharge + response.DutiesAndTaxes;
                            }
                        }
                        else
                        {
                            response.ErrorMessage.ErrorCode = "016";
                            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SHIPMENT_OVERLIMIT;
                        }
                        break;
                    case "SBA":
                        decimal sbaFreightCharge = RequestSBARate(serviceDetails, shipment, shipper, receiver, response, customerDetails);
                        decimal fedexFreightCharge = RequestFedexRate(serviceDetails, shipment, shipper, receiver, response);
                        if (sbaFreightCharge > 0 && fedexFreightCharge > 0)
                        {
                            if (sbaFreightCharge <= fedexFreightCharge)
                            {
                                response.FreightCharge = sbaFreightCharge;
                                serviceDetails.CarrierName = "SBA";
                            }
                            else
                            {
                                response.FreightCharge = fedexFreightCharge;
                                serviceDetails.CarrierName = "FEDEX";
                            }
                        }
                        else if (sbaFreightCharge == 0 && fedexFreightCharge > 0)
                        {
                            response.FreightCharge = fedexFreightCharge;
                            serviceDetails.CarrierName = "FEDEX";
                        }
                        else
                        {
                            response.FreightCharge = sbaFreightCharge;
                            serviceDetails.CarrierName = "SBA";
                        }

                        response.TotalCharge = response.FreightCharge;
                        break;
                    case "UPS":
                        if (CheckUPSRules(serviceDetails, shipment))
                        {
                            response.FreightCharge = RequestUPSRate(serviceDetails, shipment, shipper, receiver, response);
                            if (response.FreightCharge != 0)
                            {
                                if (customerDetails.MarkUp.HasValue)
                                    response.FreightCharge *= (customerDetails.MarkUp.Value + 1);
                                if (shipper.Country != receiver.Country)
                                    response.DutiesAndTaxes = CalculateDutiesAndTaxes(serviceDetails, shipment, shipper, receiver, response.FreightCharge, origCountryAlpha3Code, destCountryAlpha3Code, response);
                                response.TotalCharge = response.FreightCharge + response.DutiesAndTaxes;
                            }
                        }
                        else
                        {
                            response.ErrorMessage.ErrorCode = "016";
                            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SHIPMENT_OVERLIMIT;
                        }
                        break;
                }
                totalRate = response.TotalCharge;
            }
            catch (Exception ex)
            {
                if (m_log != null) m_log.Error(ex.ToString());
            }
            return totalRate;
        }

        private void SendEmailNotifications(string carrier, string customerNumber, ShipmentInfo shipment, Shipper shipper, Receiver receiver, RatingResponse response, List<string> trackEmails, DateTime requestDateTime, DateTime responseDateTime)
        {
            try
            {
                //string startText = "Rating Request from ABC Logistics:<br />";

                //string mailBody = BusinessLogic.Utils.GetTemplateContent(HttpContext.Current.Server.MapPath("~/MailTemplates/RatingRequest.htm"));

                //string weightUnit = shipment.WeightUnit.ToString();
                //string dimensionUnit = shipment.DimensionUnit.ToString();
                //StringBuilder packages = new StringBuilder();
                //if (shipment.Packages != null)
                //{
                //    packages.Append("Package Information: <br />");
                //    for (int i = 0; i < shipment.Packages.Count; i++)
                //    {
                //        packages.Append("Package ");
                //        packages.Append((i + 1).ToString());
                //        packages.Append(" : <br />");

                //        if (!string.IsNullOrEmpty(shipment.Packages[i].PackageType))
                //        {
                //            packages.Append("Package Type: ");
                //            packages.Append(shipment.Packages[i].PackageType);
                //            packages.Append("<br />");
                //        }
                //        packages.Append("Weight: ");
                //        packages.Append(shipment.Packages[i].Weight.ToString());
                //        packages.Append(" ");
                //        packages.Append(weightUnit);
                //        packages.Append("<br />Height: ");
                //        packages.Append(shipment.Packages[i].Height.ToString());
                //        packages.Append(" ");
                //        packages.Append(dimensionUnit);
                //        packages.Append("<br />Width: ");
                //        packages.Append(shipment.Packages[i].Width.ToString());
                //        packages.Append(" ");
                //        packages.Append(dimensionUnit);
                //        packages.Append("<br />Length: ");
                //        packages.Append(shipment.Packages[i].Length.ToString());
                //        packages.Append(" ");
                //        packages.Append(dimensionUnit);
                //        packages.Append("<br />HS Code: ");
                //        packages.Append(shipment.Packages[i].HSCode);
                //        packages.Append("<br />Manufacturer Country: ");
                //        packages.Append(shipment.Packages[i].ManufacturerCountry);
                //        packages.Append("<br />Amount: ");
                //        packages.Append(shipment.Packages[i].Amount.ToString());
                //        if (!string.IsNullOrEmpty(shipment.Packages[i].AmountUnit))
                //        {
                //            packages.Append("<br />Amount Unit: ");
                //            packages.Append(shipment.Packages[i].AmountUnit);
                //        }
                //        packages.Append("<br />ProductValue: ");
                //        packages.Append(shipment.Packages[i].ProductValue.ToString());
                //        packages.Append("<br />");
                //    }
                //}

                //string shipperAddress = string.Empty;
                //string receiverAddress = string.Empty;
                //string specialServices = string.Empty;
                //string accessorials = string.Empty;
                //if (shipper.Address != null && shipper.Address.Count > 0)
                //{
                //    foreach (string addr in shipper.Address)
                //        shipperAddress += addr + ", ";
                //}
                //if (receiver.Address != null && receiver.Address.Count > 0)
                //{
                //    foreach (string addr in receiver.Address)
                //        receiverAddress += addr + ", ";
                //}
                //if (shipment.SpecialServices != null && shipment.SpecialServices.Count > 0)
                //{
                //    foreach (string specialService in shipment.SpecialServices)
                //        specialServices += specialService + ", ";
                //}
                //if (shipment.Accessorials != null && shipment.Accessorials.Count > 0)
                //{
                //    foreach (string accessorial in shipment.Accessorials)
                //        accessorials += accessorial + ", ";
                //}
                //mailBody = mailBody.Replace("[[CustomerNumber]]", customerNumber)
                //    .Replace("[[Carrier]]", carrier)
                //    .Replace("[[ShipperAddress]]", shipperAddress)
                //    .Replace("[[STARTTEXT]]", startText)
                //    .Replace("[[ShipperCity]]", shipper.City)
                //    .Replace("[[ShipperState]]", shipper.State)
                //    .Replace("[[ShipperPostalCode]]", shipper.PostalCode)
                //    .Replace("[[ShipperCountry]]", shipper.Country)
                //    .Replace("[[ReceiverAddress]]", receiverAddress)
                //    .Replace("[[ReceiverCity]]", receiver.City)
                //    .Replace("[[ReceiverState]]", receiver.State)
                //    .Replace("[[ReceiverPostalCode]]", receiver.PostalCode)
                //    .Replace("[[ReceiverCountry]]", receiver.Country)
                //    .Replace("[[NumberofPackages]]", shipment.Packages.Count.ToString())
                //    .Replace("[[DeclaredValue]]", shipment.DeclaredValue.ToString() + " " + shipment.DeclaredValueCurrency)
                //    .Replace("[[InsurancedValue]]", shipment.Insurance.ToString() + " " + shipment.InsuranceCurrency)
                //    .Replace("[[SpecialServices]]", specialServices ?? "")
                //    .Replace("[[Acccessorials]]", accessorials ?? "")
                //    .Replace("[[PackageInformation]]", packages.ToString())
                //    .Replace("[[FreightCharge]]", response.FreightCharge.ToString())
                //    .Replace("[[DutiesAndTaxes]]", response.DutiesAndTaxes.ToString())
                //    .Replace("[[TotalCharge]]", response.TotalCharge.ToString());

                //if (response.TotalCharge == 0)
                //{
                //    mailBody = mailBody.Replace("[[ErrorCode]]", "Error Message: " + response.ErrorMessage.ErrorCode + "<br />");
                //    mailBody = mailBody.Replace("[[ErrorMessage]]", "Error Message: " + response.ErrorMessage.ErrorMessage + "<br />");
                //}
                //else
                //{
                //    mailBody = mailBody.Replace("[[ErrorCode]]", "");
                //    mailBody = mailBody.Replace("[[ErrorMessage]]", "");
                //}

                //var mailer = new BusinessLogic.Mail.Mailer();
                //string emailFrom = BusinessLogic.Mail.Mailer.CurrentMailSettings.MailFrom;
                //mailer.SendMessage(emailFrom, trackEmails, null, null, "ABC Logistics Rating Request Information ", mailBody, null, null);
            }
            catch (Exception ex)
            {
                if (m_log != null) m_log.Error(ex.ToString());
            }
        }

        private void SaveSuccessfulRequest(CustomerInfo customer, ShipmentInfo shipment, Shipper shipper, Receiver receiver, RatingResponse response)
        {
            string mainXmlName = (new XmlGenerator()).CompileRateRequestFile(customer, shipment, shipper, receiver, response);
            (new BORatingRequest()).SaveRateRequest(mainXmlName);
        }

        private decimal RequestDHLQuote(ServiceDetails serviceDetails, ShipmentInfo shipment, Shipper shipper, Receiver receiver, RatingResponse ratingResponse)
        {
            decimal result = 0;
            try
            {
                bool EUToEU = BOCountries.IsEUCountries(shipper.Country) && BOCountries.IsEUCountries(receiver.Country);

                System.Net.HttpWebRequest request;
                Uri uri = new Uri(@"https://xmlpi-ea.dhl.com/XMLShippingServlet");
                request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "text/xml; charset=utf-8";

                StringBuilder soapMsg = new StringBuilder();
                soapMsg.Append(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                <p:DCTRequest xsi:schemaLocation=""http://www.dhl.com DCT-req.xsd"" 
                xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                xmlns:p2=""http://www.dhl.com/DCTRequestdatatypes"" 
                xmlns:p1=""http://www.dhl.com/datatypes/"" 
                xmlns:p=""http://www.dhl.com""> 
                <GetQuote><Request><ServiceHeader><SiteID>");
                soapMsg.Append(serviceDetails.AccountName);
                soapMsg.Append("</SiteID><Password>");
                soapMsg.Append(serviceDetails.Password);
                soapMsg.Append("</Password>");
                soapMsg.Append("</ServiceHeader></Request><From><CountryCode>");
                soapMsg.Append(shipper.Country);
                soapMsg.Append("</CountryCode>");
                if (!string.IsNullOrEmpty(shipper.PostalCode))
                {
                    soapMsg.Append("<Postalcode>");
                    soapMsg.Append(shipper.PostalCode);
                    soapMsg.Append("</Postalcode>");
                }
                if (!string.IsNullOrEmpty(shipper.City))
                {
                    soapMsg.Append("<City>");
                    soapMsg.Append(shipper.City);
                    soapMsg.Append("</City>");
                }
                soapMsg.Append("</From><BkgDetails><PaymentCountryCode>US</PaymentCountryCode><Date>");
                soapMsg.Append(string.Format("{0:yyyy-MM-dd}", DateTime.Now));
                soapMsg.Append("</Date><ReadyTime>PT0H0M</ReadyTime><DimensionUnit>");
                soapMsg.Append(shipment.DimensionUnit.ToString());
                soapMsg.Append("</DimensionUnit><WeightUnit>");
                soapMsg.Append(shipment.WeightUnit.ToString());
                soapMsg.Append("</WeightUnit><Pieces>");
                List<PackageInfo> packages = shipment.Packages;
                for (int i = 0; i < packages.Count; i++)
                {
                    soapMsg.Append("<Piece><PieceID>");
                    soapMsg.Append((i + 1).ToString());
                    soapMsg.Append("</PieceID><Height>");
                    soapMsg.Append(packages[i].Height.ToString("f2"));
                    soapMsg.Append("</Height><Depth>");
                    soapMsg.Append(packages[i].Length.ToString("f2"));
                    soapMsg.Append("</Depth><Width>");
                    soapMsg.Append(packages[i].Width.ToString("f2"));
                    soapMsg.Append("</Width><Weight>");
                    soapMsg.Append(packages[i].Weight.ToString("f2"));
                    soapMsg.Append("</Weight></Piece>");
                }
                soapMsg.Append("</Pieces><PaymentAccountNumber>");
                soapMsg.Append(serviceDetails.AccountNumber);
                soapMsg.Append("</PaymentAccountNumber><IsDutiable>N</IsDutiable><QtdShp><GlobalProductCode>");
                if (EUToEU)
                {
                    soapMsg.Append("U");
                }
                else
                {
                    soapMsg.Append("D");
                }
                soapMsg.Append("</GlobalProductCode>");

                if (shipment.SpecialServices != null && shipment.SpecialServices.Count > 0)
                {
                    soapMsg.Append("<QtdShpExChrg>");
                    foreach (string specialService in shipment.SpecialServices)
                    {
                        soapMsg.Append("<SpecialServiceType>");
                        soapMsg.Append(specialService);
                        soapMsg.Append("</SpecialServiceType>");
                    }
                    soapMsg.Append("</QtdShpExChrg>");
                }
                soapMsg.Append("</QtdShp>");
                if (shipment.Insurance > 0)
                {
                    soapMsg.Append("<InsuredValue>");
                    soapMsg.Append(shipment.Insurance.ToString("f2"));
                    soapMsg.Append("</InsuredValue>");
                    if (!string.IsNullOrEmpty(shipment.InsuranceCurrency))
                    {
                        soapMsg.Append("<InsuredCurrency>");
                        soapMsg.Append(shipment.InsuranceCurrency);
                        soapMsg.Append("</InsuredCurrency>");
                    }
                    else
                    {
                        soapMsg.Append("<InsuredCurrency>USD</InsuredCurrency>");
                    }
                }
                soapMsg.Append("</BkgDetails><To><CountryCode>");
                soapMsg.Append(receiver.Country);
                soapMsg.Append("</CountryCode>");
                if (!string.IsNullOrEmpty(receiver.PostalCode))
                {
                    soapMsg.Append("<Postalcode>");
                    soapMsg.Append(receiver.PostalCode);
                    soapMsg.Append("</Postalcode>");
                }
                if (!string.IsNullOrEmpty(receiver.City))
                {
                    soapMsg.Append("<City>");
                    soapMsg.Append(receiver.City);
                    soapMsg.Append("</City>");
                }
                soapMsg.Append("</To>");
                if (shipment.DeclaredValue > 0)
                {
                    soapMsg.Append("<Dutiable>");
                    if (!string.IsNullOrEmpty(shipment.DeclaredValueCurrency))
                    {
                        soapMsg.Append("<DeclaredCurrency>");
                        soapMsg.Append(shipment.DeclaredValueCurrency);
                        soapMsg.Append("</DeclaredCurrency>");
                    }
                    else
                    {
                        soapMsg.Append("<DeclaredCurrency>USD</DeclaredCurrency>");
                    }
                    soapMsg.Append("<DeclaredValue>");
                    soapMsg.Append(shipment.DeclaredValue.ToString("f2"));
                    soapMsg.Append("</DeclaredValue></Dutiable>");
                }
                soapMsg.Append("</GetQuote></p:DCTRequest>");

                XmlDocument soapXml = new XmlDocument();
                soapXml.LoadXml(soapMsg.ToString());

                //XDocument xmlDoc;
                //System.Net.HttpWebResponse response = null;

                try
                {
                    using (Stream stream = request.GetRequestStream())
                    {
                        soapXml.Save(stream);
                    }

                    result = GetDHLRequestRate(request, ratingResponse);

                    if (result == 0)
                    {
                        soapXml["p:DCTRequest"]["GetQuote"]["BkgDetails"]["Date"].InnerText = string.Format("{0:yyyy-MM-dd}", AddBusinessDay(DateTime.Now));

                        request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(uri);
                        request.Method = "POST";
                        request.ContentType = "text/xml; charset=utf-8";
                        using (Stream stream = request.GetRequestStream())
                        {
                            soapXml.Save(stream);
                        }

                        result = GetDHLRequestRate(request, ratingResponse);

                        if (result > 0)
                        {
                            ratingResponse.ErrorMessage.ErrorCode = "";
                            ratingResponse.ErrorMessage.ErrorMessage = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    ratingResponse.ErrorMessage.ErrorCode = "501";
                    ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
                    if (m_log != null) m_log.Error(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                ratingResponse.ErrorMessage.ErrorCode = "502";
                ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
                if (m_log != null) m_log.Error(ex.ToString());
            }

            return result;
        }

        private decimal RequestFedexRate(ServiceDetails serviceDetails, ShipmentInfo shipment, Shipper shipper, Receiver receiver, RatingResponse ratingResponse)
        {
            decimal result = 0;
            try
            {
                RateRequest request = CreateRateRequest(serviceDetails, shipment, shipper, receiver);
                RateService service = new RateService();
                RateReply reply = service.getRates(request);

                if (reply.HighestSeverity == NotificationSeverityType.SUCCESS || reply.HighestSeverity == NotificationSeverityType.NOTE || reply.HighestSeverity == NotificationSeverityType.WARNING)
                {
                    RatedShipmentDetail[] shipmentDetails = reply.RateReplyDetails[0].RatedShipmentDetails;
                    if (shipmentDetails != null && shipmentDetails.Length > 0)
                    {
                        ShipmentRateDetail rateDetail = shipmentDetails[0].ShipmentRateDetail;
                        result = rateDetail.TotalNetCharge.Amount;
                    }
                    else
                    {
                        ratingResponse.ErrorMessage.ErrorCode = "200";
                        ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_UNKNOWN_ERROR;
                    }
                }
                else
                {
                    if (reply.Notifications != null && reply.Notifications.Length > 0)
                    {
                        ratingResponse.ErrorMessage.ErrorCode = reply.Notifications[0].Code;
                        ratingResponse.ErrorMessage.ErrorMessage = reply.Notifications[0].Message;
                    }
                    else
                    {
                        ratingResponse.ErrorMessage.ErrorCode = "019";
                        ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_UNKNOWN_ERROR;
                    }
                }
            }
            catch (SoapException ex)
            {
                ratingResponse.ErrorMessage.ErrorCode = "503";
                ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
                if (m_log != null) m_log.Error(ex.Detail.InnerText.ToString());
            }
            catch (Exception ex)
            {
                ratingResponse.ErrorMessage.ErrorCode = "504";
                ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
                if (m_log != null) m_log.Error(ex.ToString());
            }

            return result;
        }

        private decimal RequestUPSRate(ServiceDetails serviceDetails, ShipmentInfo shipment, Shipper shipper, Receiver receiver, RatingResponse ratingResponse)
        {
            decimal result = 0;
            try
            {
                UPSRateWebReference.RateResponse rateResponse = CreateUPSRateRequest(serviceDetails, shipment, shipper, receiver, ratingResponse);
                if (rateResponse == null)
                {
                    ratingResponse.ErrorMessage.ErrorCode = "513";
                    ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
                }
                else
                {
                    decimal factor = 1;
                    if (rateResponse.RatedShipment[0].TotalCharges.CurrencyCode != "USD")
                    {
                        CurrencyConverterWebReference.CurrencyConvertor converter = new CurrencyConverterWebReference.CurrencyConvertor();
                        try
                        {
                            CurrencyConverterWebReference.Currency fromCurrency = (CurrencyConverterWebReference.Currency)Enum.Parse(typeof(CurrencyConverterWebReference.Currency), rateResponse.RatedShipment[0].TotalCharges.CurrencyCode);
                            factor = (decimal)(converter.ConversionRate(fromCurrency, CurrencyConverterWebReference.Currency.USD));
                        }
                        catch (Exception ex)
                        {
                            if (m_log != null) m_log.Error(ex.ToString());
                        }
                    }

                    result = factor * decimal.Parse(rateResponse.RatedShipment[0].TotalCharges.MonetaryValue);
                }
            }
            catch (Exception ex)
            {
                ratingResponse.ErrorMessage.ErrorCode = "514";
                ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
                if (m_log != null) m_log.Error(ex.ToString());
            }

            return result;
        }

        private decimal RequestSBARate(ServiceDetails serviceDetails, ShipmentInfo shipment, Shipper shipper, Receiver receiver, RatingResponse ratingResponse, BookingCustomer customerDetails)
        {
            decimal totalCharge = 0, chargeableWeight = 0;

            try
            {
                chargeableWeight = BOSBARateCalculator.GetChargeableWeight(shipment.Packages, customerDetails.DimFactor.HasValue ? customerDetails.DimFactor.Value : 0);

                BOSBARateCalculator boSBARateCalculator = new BOSBARateCalculator();

                RatesACI rateACIOrigin = boSBARateCalculator.GetRates(shipper.PostalCode);
                if (rateACIOrigin == null)
                {

                    ratingResponse.ErrorMessage.ErrorCode = "300";
                    ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_NO_ORIGIN_ACI;
                    return 0;
                }



                RatesACI rateACIDest = boSBARateCalculator.GetRates(receiver.PostalCode);

                if (rateACIDest == null)
                {

                    ratingResponse.ErrorMessage.ErrorCode = "300";
                    ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_NO_DEST_ACI;
                    return 0;
                }



                string originRegion = boSBARateCalculator.GetRateStateRegion(rateACIOrigin.State, customerDetails.CustomerID);
                string destRegion = boSBARateCalculator.GetRateStateRegion(rateACIDest.State, customerDetails.CustomerID);

                string zone = boSBARateCalculator.GetRateZone(originRegion, destRegion, customerDetails.CustomerID);

                if (zone == "")
                {

                    ratingResponse.ErrorMessage.ErrorCode = "300";
                    ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_NO_ZONE_MAT;
                    return 0;
                }



                totalCharge = boSBARateCalculator.GetRateTariffCharge(shipment.ServiceType, zone, chargeableWeight, customerDetails.CustomerID);

                if (customerDetails.FuelRate.HasValue)
                    totalCharge = totalCharge * (1 + customerDetails.FuelRate.Value);

                BookingRateBeyond rateBeyond = boSBARateCalculator.GetRateBeyond(originRegion, destRegion, customerDetails.CustomerID);

                if (rateBeyond != null)
                {
                    decimal beyondCharge = rateBeyond.MinRate.Value;
                    if (rateBeyond.Rate.HasValue)
                    {



                        beyondCharge = Math.Max(rateBeyond.Rate.Value * chargeableWeight, rateBeyond.MinRate.Value);


                    }
                    totalCharge = totalCharge + beyondCharge;
                }

                if (shipment.Accessorials.Count > 0)
                {
                    decimal accessorialCharge = 0;

                    List<BookingCustomerAccessorial> accessorialRates = boSBARateCalculator.GetAccessorialRates(customerDetails.CustomerID);
                    if (accessorialRates.Count > 0)
                    {
                        foreach (string accessorial in shipment.Accessorials)
                        {


                            accessorialCharge += GetAccessorialItemCharge(originRegion, destRegion, accessorial, accessorialRates, chargeableWeight);

                        }
                    }

                    totalCharge = totalCharge + accessorialCharge;

                }
            }
            catch (Exception ex)
            {
                totalCharge = 0;
                ratingResponse.ErrorMessage.ErrorCode = "505";
                ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
                if (m_log != null) m_log.Error(ex.ToString());
            }

            return totalCharge;
        }


        #region Functions

        #region UPS

        private bool CheckUPSRules(ServiceDetails serviceDetails, ShipmentInfo shipment)
        {
            List<decimal> dims = new List<decimal>();

            foreach (PackageInfo package in shipment.Packages)
            {
                dims.Clear();
                dims.Add(package.Length);
                dims.Add(package.Height);
                dims.Add(package.Width);
                dims.Sort();
                decimal length = dims[2], lengthAndGirth = Math.Round(dims[2] + 2 * dims[0] + 2 * dims[1]);

                if ((shipment.WeightUnit == WeightUnit.KG && package.Weight > 70) ||
                (shipment.WeightUnit == WeightUnit.LB && package.Weight > 150))
                {
                    return false;
                }

                if ((shipment.DimensionUnit == DimensionUnit.CM && (length > 270 || lengthAndGirth > 419))
                    || (shipment.DimensionUnit == DimensionUnit.IN && (length > 108 || lengthAndGirth > 165)))
                {
                    return false;
                }
            }

            return true;
        }

        private UPSRateWebReference.RateResponse CreateUPSRateRequest(ServiceDetails serviceDetails, ShipmentInfo shipment, Shipper shipper, Receiver receiver, RatingResponse ratingResponse)
        {
            UPSRateWebReference.RateResponse rateResponse = null;
            try
            {
                UPSRateWebReference.RateService rate = new UPSRateWebReference.RateService();
                UPSRateWebReference.RateRequest rateRequest = new UPSRateWebReference.RateRequest();
                UPSRateWebReference.UPSSecurity upss = new UPSRateWebReference.UPSSecurity();
                UPSRateWebReference.UPSSecurityServiceAccessToken upssSvcAccessToken = new UPSRateWebReference.UPSSecurityServiceAccessToken();
                upssSvcAccessToken.AccessLicenseNumber = serviceDetails.License;
                upss.ServiceAccessToken = upssSvcAccessToken;
                UPSRateWebReference.UPSSecurityUsernameToken upssUsrNameToken = new UPSRateWebReference.UPSSecurityUsernameToken();
                upssUsrNameToken.Username = serviceDetails.AccountName;
                upssUsrNameToken.Password = serviceDetails.Password;
                upss.UsernameToken = upssUsrNameToken;
                rate.UPSSecurityValue = upss;

                UPSRateWebReference.RequestType request = new UPSRateWebReference.RequestType();
                String[] requestOption = { "Rate" };
                request.RequestOption = requestOption;
                rateRequest.Request = request;
                UPSRateWebReference.ShipmentType shipmentInfo = new UPSRateWebReference.ShipmentType();
                UPSRateWebReference.ShipperType shipperInfo = new UPSRateWebReference.ShipperType();
                shipperInfo.ShipperNumber = serviceDetails.AccountNumber;
                UPSRateWebReference.AddressType shipperAddress = new UPSRateWebReference.AddressType();
                string[] shipperAddressLine = null;
                if (shipper.Address != null)
                    shipperAddressLine = shipper.Address.ToArray();
                shipperAddress.AddressLine = shipperAddressLine;
                shipperAddress.City = shipper.City;
                shipperAddress.PostalCode = shipper.PostalCode;
                shipperAddress.StateProvinceCode = shipper.State;
                shipperAddress.CountryCode = shipper.Country;
                shipperInfo.Address = shipperAddress;
                shipmentInfo.Shipper = shipperInfo;
                UPSRateWebReference.ShipFromType shipFrom = new UPSRateWebReference.ShipFromType();
                UPSRateWebReference.AddressType shipFromAddress = new UPSRateWebReference.AddressType();
                shipFromAddress.AddressLine = shipperAddressLine;
                shipFromAddress.City = shipper.City;
                shipFromAddress.PostalCode = shipper.PostalCode;
                shipFromAddress.StateProvinceCode = shipper.State;
                shipFromAddress.CountryCode = shipper.Country;
                shipFrom.Address = shipFromAddress;
                shipmentInfo.ShipFrom = shipFrom;
                UPSRateWebReference.ShipToType shipTo = new UPSRateWebReference.ShipToType();
                UPSRateWebReference.ShipToAddressType shipToAddress = new UPSRateWebReference.ShipToAddressType();
                string[] receiverAddressLine = null;
                if (receiver.Address != null)
                    receiverAddressLine = receiver.Address.ToArray();
                shipToAddress.AddressLine = receiverAddressLine;
                shipToAddress.City = receiver.City;
                shipToAddress.PostalCode = receiver.PostalCode;
                shipToAddress.StateProvinceCode = receiver.State;
                shipToAddress.CountryCode = receiver.Country;
                shipTo.Address = shipToAddress;
                shipmentInfo.ShipTo = shipTo;
                UPSRateWebReference.CodeDescriptionType service = new UPSRateWebReference.CodeDescriptionType();

                if (shipper.Country == "US" && receiver.Country == "US")
                    service.Code = "03";
                else if (shipper.Country == "CA" && receiver.Country == "CA")
                    service.Code = "11";
                else
                    service.Code = "65";

                shipmentInfo.Service = service;

                List<UPSRateWebReference.PackageType> packages = new List<UPSRateWebReference.PackageType>();
                UPSRateWebReference.PackageServiceOptionsType serviceOptions = new UPSRateWebReference.PackageServiceOptionsType();
                if (shipment.DeclaredValue > 0)
                {
                    serviceOptions.DeclaredValue = new UPSRateWebReference.InsuredValueType();
                    serviceOptions.DeclaredValue.CurrencyCode = string.IsNullOrEmpty(shipment.DeclaredValueCurrency) ? "USD" : shipment.DeclaredValueCurrency;
                    serviceOptions.DeclaredValue.MonetaryValue = (shipment.DeclaredValue / shipment.Packages.Count).ToString("f2");
                }
                if (shipment.Insurance > 0)
                {
                    serviceOptions.Insurance = new UPSRateWebReference.InsuranceType();
                    serviceOptions.Insurance.BasicFlexibleParcelIndicator = new UPSRateWebReference.InsuranceValueType();
                    serviceOptions.Insurance.BasicFlexibleParcelIndicator.CurrencyCode = string.IsNullOrEmpty(shipment.InsuranceCurrency) ? "USD" : shipment.InsuranceCurrency;
                    serviceOptions.Insurance.BasicFlexibleParcelIndicator.MonetaryValue = (shipment.Insurance / shipment.Packages.Count).ToString("f2");
                }

                foreach (PackageInfo p in shipment.Packages)
                {
                    UPSRateWebReference.PackageType package = new UPSRateWebReference.PackageType();
                    UPSRateWebReference.PackageWeightType packageWeight = new UPSRateWebReference.PackageWeightType();
                    packageWeight.Weight = p.Weight.ToString("f2");
                    UPSRateWebReference.CodeDescriptionType uom = new UPSRateWebReference.CodeDescriptionType();
                    uom.Code = shipment.WeightUnit == WeightUnit.KG ? "KGS" : "LBS";
                    packageWeight.UnitOfMeasurement = uom;
                    package.PackageWeight = packageWeight;
                    UPSRateWebReference.CodeDescriptionType packType = new UPSRateWebReference.CodeDescriptionType();
                    packType.Code = "02";
                    package.PackagingType = packType;
                    UPSRateWebReference.DimensionsType dim = new UPSRateWebReference.DimensionsType();
                    dim.Height = p.Height.ToString("f2");
                    dim.Length = p.Length.ToString("f2");
                    dim.Width = p.Width.ToString("f2");
                    dim.UnitOfMeasurement = new UPSRateWebReference.CodeDescriptionType();
                    dim.UnitOfMeasurement.Code = shipment.DimensionUnit == DimensionUnit.CM ? "CM" : "IN";
                    package.Dimensions = dim;
                    package.PackageServiceOptions = serviceOptions;
                    packages.Add(package);
                }

                shipmentInfo.Package = packages.ToArray();
                rateRequest.Shipment = shipmentInfo;
                System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                rateResponse = rate.ProcessRate(rateRequest);
            }
            catch (SoapException ex)
            {
                XDocument xmlDoc = XDocument.Parse(ex.Detail.InnerXml);
                XNamespace env = "http://www.ups.com/XMLSchema/XOLTWS/Error/v1.1";
                XElement xmlErr = xmlDoc.Root.Descendants(env + "PrimaryErrorCode").First();
                if (xmlErr != null)
                {
                    ratingResponse.ErrorMessage.ErrorCode = xmlErr.Descendants(env + "Code").First().Value;
                    ratingResponse.ErrorMessage.ErrorMessage = xmlErr.Descendants(env + "Description").First().Value;
                }
                else
                {
                    ratingResponse.ErrorMessage.ErrorCode = "511";
                    ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
                }
                if (m_log != null) m_log.Error(ex.Detail.InnerText.ToString());
            }
            catch (Exception ex)
            {
                ratingResponse.ErrorMessage.ErrorCode = "512";
                ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
                if (m_log != null) m_log.Error(ex.ToString());
            }
            return rateResponse;
        }
        #endregion

        #region FedEx
        private RateRequest CreateRateRequest(ServiceDetails serviceDetails, ShipmentInfo shipment, Shipper shipper, Receiver receiver)
        {
            RateRequest request = new RateRequest();
            request.WebAuthenticationDetail = new WebAuthenticationDetail();
            request.WebAuthenticationDetail.UserCredential = new WebAuthenticationCredential();
            request.WebAuthenticationDetail.UserCredential.Key = serviceDetails.AccountName;
            request.WebAuthenticationDetail.UserCredential.Password = serviceDetails.Password;
            request.ClientDetail = new ClientDetail();
            request.ClientDetail.AccountNumber = serviceDetails.AccountNumber;
            request.ClientDetail.MeterNumber = serviceDetails.License;
            request.TransactionDetail = new TransactionDetail();
            request.TransactionDetail.CustomerTransactionId = "ABC Logistics Shipping Web Service Rate Request";
            request.Version = new VersionId();
            SetShipmentDetails(request, serviceDetails, shipment, shipper, receiver);
            return request;
        }

        private void SetShipmentDetails(RateRequest request, ServiceDetails serviceDetails, ShipmentInfo shipment, Shipper shipper, Receiver receiver)
        {
            request.RequestedShipment = new RequestedShipment();
            request.RequestedShipment.ShipTimestamp = DateTime.Now;
            request.RequestedShipment.ShipTimestampSpecified = true;

            switch (serviceDetails.ServiceCode)
            {
                case "ECONOMY":
                    request.RequestedShipment.ServiceType = ServiceType.INTERNATIONAL_ECONOMY;
                    break;
                case "GROUND":
                    request.RequestedShipment.ServiceType = ServiceType.FEDEX_GROUND;
                    break;
                case "EXPRESS":
                    request.RequestedShipment.ServiceType = ServiceType.FEDEX_EXPRESS_SAVER;
                    break;
            }


            request.RequestedShipment.ServiceTypeSpecified = true;
            request.RequestedShipment.PackagingType = PackagingType.YOUR_PACKAGING;
            request.RequestedShipment.PackagingTypeSpecified = true;
            SetOrigin(request, shipper);
            SetDestination(request, receiver);
            SetPackageLineItems(request, shipment);
            if (shipment.DeclaredValue > 0)
            {
                request.RequestedShipment.TotalInsuredValue = new Money();
                request.RequestedShipment.TotalInsuredValue.Amount = shipment.DeclaredValue;
                request.RequestedShipment.TotalInsuredValue.Currency = string.IsNullOrEmpty(shipment.DeclaredValueCurrency) ? "USD" : shipment.DeclaredValueCurrency;
            }
            request.RequestedShipment.EdtRequestType = EdtRequestType.NONE;
            request.RequestedShipment.EdtRequestTypeSpecified = true;
            request.RequestedShipment.PackageCount = shipment.Packages.Count.ToString();
            request.RequestedShipment.PreferredCurrency = "USD";
        }

        private void SetOrigin(RateRequest request, Shipper shipper)
        {
            request.RequestedShipment.Shipper = new Party();
            request.RequestedShipment.Shipper.Address = new Address();

            if (shipper.Address != null)
            {
                int addressCount = shipper.Address.Count;
                if (addressCount > 0)
                {
                    request.RequestedShipment.Shipper.Address.StreetLines = shipper.Address.ToArray();
                }
            }
            request.RequestedShipment.Shipper.Address.City = shipper.City;
            request.RequestedShipment.Shipper.Address.StateOrProvinceCode = shipper.State;
            request.RequestedShipment.Shipper.Address.PostalCode = shipper.PostalCode;
            request.RequestedShipment.Shipper.Address.CountryCode = shipper.Country;
        }

        private void SetDestination(RateRequest request, Receiver receiver)
        {
            request.RequestedShipment.Recipient = new Party();
            request.RequestedShipment.Recipient.Address = new Address();

            if (receiver.Address != null)
            {
                int addressCount = receiver.Address.Count;
                if (addressCount > 0)
                {
                    request.RequestedShipment.Recipient.Address.StreetLines = receiver.Address.ToArray();
                }
            }
            request.RequestedShipment.Recipient.Address.City = receiver.City;
            request.RequestedShipment.Recipient.Address.StateOrProvinceCode = receiver.State;
            request.RequestedShipment.Recipient.Address.PostalCode = receiver.PostalCode;
            request.RequestedShipment.Recipient.Address.CountryCode = receiver.Country;
        }

        private void SetPackageLineItems(RateRequest request, ShipmentInfo shipment)
        {
            int packageCount = shipment.Packages.Count;
            if (packageCount > 0)
            {
                request.RequestedShipment.RequestedPackageLineItems = new RequestedPackageLineItem[packageCount];
                WeightUnits weightUnit = shipment.WeightUnit == WeightUnit.LB ? WeightUnits.LB : WeightUnits.KG;
                LinearUnits linearUnit = shipment.DimensionUnit == DimensionUnit.IN ? LinearUnits.IN : LinearUnits.CM;
                for (int i = 0; i < packageCount; i++)
                {
                    request.RequestedShipment.RequestedPackageLineItems[i] = new RequestedPackageLineItem();
                    request.RequestedShipment.RequestedPackageLineItems[i].SequenceNumber = (i + 1).ToString();
                    request.RequestedShipment.RequestedPackageLineItems[i].GroupPackageCount = "1";
                    request.RequestedShipment.RequestedPackageLineItems[i].Weight = new Weight();
                    request.RequestedShipment.RequestedPackageLineItems[i].Weight.Units = weightUnit;
                    request.RequestedShipment.RequestedPackageLineItems[i].Weight.Value = shipment.Packages[i].Weight;
                    request.RequestedShipment.RequestedPackageLineItems[i].Dimensions = new Dimensions();
                    request.RequestedShipment.RequestedPackageLineItems[i].Dimensions.Units = linearUnit;
                    request.RequestedShipment.RequestedPackageLineItems[i].Dimensions.Length = Math.Round(shipment.Packages[i].Length).ToString();
                    request.RequestedShipment.RequestedPackageLineItems[i].Dimensions.Width = Math.Round(shipment.Packages[i].Width).ToString();
                    request.RequestedShipment.RequestedPackageLineItems[i].Dimensions.Height = Math.Round(shipment.Packages[i].Height).ToString();
                }
            }
        }

        private bool CheckFedexRules(ServiceDetails serviceDetails, ShipmentInfo shipment)
        {
            List<decimal> dims = new List<decimal>();
            if (serviceDetails.ServiceCode == "GROUND" || serviceDetails.ServiceCode == "ECONOMY" || serviceDetails.ServiceCode == "EXPRESS")
            {
                foreach (PackageInfo package in shipment.Packages)
                {
                    dims.Clear();
                    dims.Add(package.Length);
                    dims.Add(package.Height);
                    dims.Add(package.Width);
                    dims.Sort();
                    decimal length = dims[2], lengthAndGirth = Math.Round(dims[2] + 2 * dims[0] + 2 * dims[1]);

                    if ((shipment.WeightUnit == WeightUnit.KG && package.Weight > 68) ||
                    (shipment.WeightUnit == WeightUnit.LB && package.Weight > 150))
                    {
                        return false;
                    }

                    if ((shipment.DimensionUnit == DimensionUnit.CM && (length > 274.32m || lengthAndGirth > 419.1m))
                        || (shipment.DimensionUnit == DimensionUnit.IN && (length > 108 || lengthAndGirth > 165)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion

        #region DHL
        private decimal GetDHLRequestRate(System.Net.HttpWebRequest request, RatingResponse ratingResponse)
        {
            decimal result = 0;
            XDocument xmlDoc;
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                XmlReader responseStream = XmlReader.Create(request.GetResponse().GetResponseStream());
                xmlDoc = XDocument.Load(responseStream);
                if (responseStream != null)
                    responseStream.Close();
                XElement packageDetails = xmlDoc.Root.Element("GetQuoteResponse").Element("BkgDetails");
                if (packageDetails != null)
                {
                    XElement currencyCode = packageDetails.Element("QtdShp").Element("CurrencyCode");
                    if (currencyCode != null && currencyCode.Value == "USD")
                        result = decimal.Parse(packageDetails.Element("QtdShp").Element("ShippingCharge").Value);
                    else
                    {
                        var currencies = packageDetails.Element("QtdShp").Elements("QtdSInAdCur");
                        if (currencies != null)
                        {
                            foreach (XElement currency in currencies)
                            {
                                if (currency.Element("CurrencyRoleTypeCode").Value == "BASEC")
                                    result = decimal.Parse(currency.Element("TotalAmount").Value);
                            }
                        }
                    }
                }
                else
                {
                    XElement condition = xmlDoc.Root.Element("GetQuoteResponse").Element("Note").Element("Condition");
                    if (condition != null)
                    {
                        ratingResponse.ErrorMessage.ErrorCode = condition.Element("ConditionCode").Value;
                        ratingResponse.ErrorMessage.ErrorMessage = condition.Element("ConditionData").Value;
                    }
                }
            }

            return result;
        }

        private bool CheckDHLRules(ServiceDetails serviceDetails, ShipmentInfo shipment)
        {
            List<decimal> dims = new List<decimal>();
            foreach (PackageInfo package in shipment.Packages)
            {
                dims.Clear();
                dims.Add(package.Length);
                dims.Add(package.Height);
                dims.Add(package.Width);
                dims.Sort();

                if ((shipment.WeightUnit == WeightUnit.KG && package.Weight > 70) ||
                (shipment.WeightUnit == WeightUnit.LB && package.Weight > 154.324m))
                {
                    return false;
                }

                if ((shipment.DimensionUnit == DimensionUnit.CM && (dims[2] > 120 || dims[0] > 80 || dims[1] > 80))
                    || (shipment.DimensionUnit == DimensionUnit.IN && (dims[2] > 47.2441m || dims[0] > 31.4961m || dims[1] > 31.4961m)))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region DutiesAndTaxes
        private decimal CalculateDutiesAndTaxes(ServiceDetails serviceDetails, ShipmentInfo shipment, Shipper shipper, Receiver receiver, decimal freightCharge, string originCountryCode, string destCountryCode, RatingResponse ratingResponse)
        {
            decimal dutiesAndTaxes = 0;
            decimal totalValues = 0;
            decimal totalShipmentWeight = 0;
            System.Net.HttpWebRequest request = null;
            System.Net.HttpWebResponse response = null;
            try
            {
                StringBuilder productQueryStrings = new StringBuilder();
                productQueryStrings.Append("from=");
                productQueryStrings.Append(originCountryCode);
                productQueryStrings.Append("&to=");
                productQueryStrings.Append(destCountryCode);
                productQueryStrings.Append("&shipping=");
                productQueryStrings.Append(freightCharge);
                productQueryStrings.Append("&insurance=");
                productQueryStrings.Append(shipment.Insurance.ToString());
                if (!string.IsNullOrEmpty(shipment.InsuranceCurrency))
                {
                    productQueryStrings.Append("&currency=");
                    productQueryStrings.Append(shipment.InsuranceCurrency);
                }
                else
                {
                    productQueryStrings.Append("&currency=usd");
                }
                productQueryStrings.Append("&output_currency=usd&detailed_result=0&classify_by=hs");
                //Get information for every package
                PackageInfo packageInfo;
                for (int i = 0; i < shipment.Packages.Count; i++)
                {
                    packageInfo = shipment.Packages[i];
                    if (packageInfo.Amount <= 0)
                        packageInfo.Amount = 1;

                    productQueryStrings.Append("&hs[" + i.ToString() + "]=" + packageInfo.HSCode);
                    productQueryStrings.Append("&value[" + i.ToString() + "]=" + packageInfo.ProductValue);
                    productQueryStrings.Append("&qty[" + i.ToString() + "]=" + packageInfo.Amount);
                    if (!string.IsNullOrEmpty(packageInfo.AmountUnit))
                        productQueryStrings.Append("&amt_unit[" + i.ToString() + "]=" + packageInfo.AmountUnit);
                    if (shipment.WeightUnit == WeightUnit.KG)
                        productQueryStrings.Append("&wt[" + i.ToString() + "]=" + (packageInfo.Weight / packageInfo.Amount).ToString());
                    else
                        productQueryStrings.Append("&wt[" + i.ToString() + "]=" + ConvertLBToKG(packageInfo.Weight / packageInfo.Amount).ToString());
                    if (!string.IsNullOrEmpty(packageInfo.ManufacturerCountry))
                    {
                        productQueryStrings.Append("&origin[" + i.ToString() + "]=" + packageInfo.ManufacturerCountry);
                    }
                    //Get total values & weight for the whole shipment
                    totalValues += packageInfo.ProductValue * packageInfo.Amount;
                    totalShipmentWeight += packageInfo.Weight;
                }
                //If total values of the shipment > 3000 or importing to Brazil
                //we need to consider shipment weight as a parameter
                if (totalValues + freightCharge + shipment.Insurance > 3000 && destCountryCode == "BRA")
                {
                    productQueryStrings.Append("&shipment_wt=" + ConvertLBToKG(totalShipmentWeight).ToString());
                }
                //If importing into Canada, need to specify province code
                if (destCountryCode == "CAN")
                {
                    productQueryStrings.Append("&province=" + receiver.State);
                }
                //If importing into Russia, need to specify whether it's a commercial import or not
                if (destCountryCode == "RUS")
                {
                    productQueryStrings.Append("&commercial_importer=1");
                }


                Uri uri = new Uri(@"http://www.dutycalculator.com/api2.1/key/calculation?" + productQueryStrings);
                request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(uri);
                XDocument xmlDoc;
                response = (System.Net.HttpWebResponse)request.GetResponse();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    XmlReader responseStream = XmlReader.Create(request.GetResponse().GetResponseStream());
                    xmlDoc = XDocument.Load(responseStream);
                    if (responseStream != null)
                        responseStream.Close();
                    XElement totalChargesElement = xmlDoc.Root.Element("total-charges");
                    if (totalChargesElement != null)
                        dutiesAndTaxes = decimal.Parse(totalChargesElement.Element("total").Value);
                    else
                    {
                        ratingResponse.ErrorMessage.ErrorCode = xmlDoc.Root.Element("code").Value;
                        ratingResponse.ErrorMessage.ErrorMessage = xmlDoc.Root.Element("message").Value;
                    }
                }
            }
            catch (Exception ex)
            {
                if (response != null)
                {
                    response.Close();
                }
                response = null;
                request = null;
                dutiesAndTaxes = 0;
                ratingResponse.ErrorMessage.ErrorCode = "506";
                ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
                if (m_log != null) m_log.Error(ex.ToString());
            }

            return dutiesAndTaxes;
        }
        #endregion

        #region Utility Functions
        private decimal ConvertLBToKG(decimal weight)
        {
            return weight * 0.453592m;
        }

        private decimal GetAccessorialItemCharge(string fromRegion, string toRegion, string accessorial, List<BookingCustomerAccessorial> accessorialRates, decimal weight)
        {
            decimal itemCharge = 0;
            int accessrialId = (int)Enum.Parse(typeof(Accessorial), accessorial);
            BookingCustomerAccessorial accessorialFlatRate = (from rate in accessorialRates
                                                              where rate.AccessorialID == accessrialId
                                                              && string.IsNullOrEmpty(rate.FromRegion)
                                                              && string.IsNullOrEmpty(rate.ToRegion)
                                                              select rate).SingleOrDefault();
            if (accessorialFlatRate != null)
            {
                itemCharge = accessorialFlatRate.MinRate.Value;
                if (accessorialFlatRate.Rate.HasValue)
                {
                    itemCharge = Math.Max(accessorialFlatRate.Rate.Value * weight, accessorialFlatRate.MinRate.Value);
                }
            }
            else
            {
                BookingCustomerAccessorial accessorialRegionRate = (from rate in accessorialRates
                                                                    where rate.AccessorialID == accessrialId
                                                                    && rate.FromRegion == fromRegion
                                                                    && rate.ToRegion == toRegion
                                                                    select rate).SingleOrDefault();

                if (accessorialRegionRate != null)
                {
                    itemCharge = accessorialRegionRate.MinRate.Value;
                    if (accessorialRegionRate.Rate.HasValue)
                    {
                        itemCharge = Math.Max(accessorialRegionRate.Rate.Value * weight, accessorialFlatRate.MinRate.Value);
                    }
                }
            }

            return itemCharge;
        }

        private void ConvertDimensionWeight(List<PackageInfo> packages, bool convertDim, bool convertWeight, bool INToCM, bool LBToKG)
        {
            foreach (PackageInfo package in packages)
            {
                if (convertDim)
                {
                    decimal dimParam = INToCM == true ? 2.54m : 0.393701m;
                    package.Height *= dimParam;
                    package.Width *= dimParam;
                    package.Length *= dimParam;
                }

                if (convertWeight)
                {
                    decimal weightParam = LBToKG == true ? 0.453592m : 2.20462m;
                    package.Weight *= weightParam;
                }
            }
        }

        private DateTime AddBusinessDay(DateTime date)
        {
            DateTime newDate;
            switch (date.DayOfWeek)
            {
                default:
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Sunday:
                    newDate = date.AddDays(1);
                    break;
                case DayOfWeek.Friday:
                    newDate = date.AddDays(3);
                    break;
                case DayOfWeek.Saturday:
                    newDate = date.AddDays(2);
                    break;
            }
            return newDate;
        }





















        #endregion


        #endregion
    }
}
