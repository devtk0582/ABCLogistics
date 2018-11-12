using System;
using log4net;
using System.Reflection;

namespace CarrierIntegrationCore.Booking
{
    public class BOBooking
    {
        private ILog m_log;

        public BOBooking()
        {
            m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public BookingResponse BookShipment(CustomerInfo customer, BookingShipper shipper, BookingReceiver receiver, BookingPickUpInfo shipment)
        {
            DateTime requestDateTime = DateTime.Now;
            DateTime responseTime;
            BookingResponse response = new BookingResponse();
            //try
            //{
            //    BOCountries boCountries = new BOCountries();
            //    BookingCountry originCountry = boCountries.GetCountryCode(shipper.Country);
            //    if (originCountry == null)
            //    {
            //        response.ErrorMessage.ErrorCode = "001";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SHIPPER_COUNTRY_INVALID;
            //        return response;
            //    }
            //    shipper.Country = originCountry.Alpha2Code;

            //    BookingCountry destCountry = boCountries.GetCountryCode(shipper.DestinationCountry);
            //    if (destCountry == null)
            //    {
            //        response.ErrorMessage.ErrorCode = "002";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_RECEIVER_COUNTRY_INVALID;
            //        return response;
            //    }
            //    shipper.DestinationCountry = destCountry.Alpha2Code;

            //    if (Validation.ValidateBookRequest(customer, shipper, shipment, response))
            //    {
            //        BookingCustomer customerDetails = (new BOCustomers()).AuthenticationCustomer(customer.CustomerNumber, customer.Password);
            //        if (customerDetails != null && customerDetails.CustomerID > 0)
            //        {
            //            List<ServiceDetails> servicesDetails = (new BOServices()).GetServicesDetails(originCountry.Alpha2Code, destCountry.Alpha2Code, customerDetails.CustomerID);

            //            if (servicesDetails != null && servicesDetails.Count > 0)
            //            {
            //                string carrierName = "";

            //                ConvertDimensionWeight(shipment, shipper.Country);

            //                for (int i = 0; i < servicesDetails.Count; i++)
            //                {
            //                    BookCarrierShipment(servicesDetails[i], shipper, shipment, originCountry.Region, customerDetails.BookUserID, response);
            //                    if (response.ConfirmationNumber != string.Empty)
            //                    {
            //                        response.ErrorMessage.ErrorCode = "";
            //                        response.ErrorMessage.ErrorMessage = "";
            //                        carrierName = servicesDetails[i].CarrierName;
            //                        break;
            //                    }
            //                }

                            
            //                SaveSuccessfulRequest(customer, shipper, shipment, response);
            //                responseTime = DateTime.Now;
            //                if(!string.IsNullOrEmpty(response.ConfirmationNumber) && !string.IsNullOrEmpty(customerDetails.NotificationEmails))
            //                {
            //                    string[] emails = customerDetails.NotificationEmails.Split(';');

            //                    if (emails.Length > 0)
            //                    {
            //                        List<string> trackEmails = new List<string>();
            //                        foreach (string email in emails)
            //                        {
            //                            if (Validation.IsEmail(email.Trim()))
            //                                trackEmails.Add(email.Trim());
            //                        }

            //                        SendEmailNotifications(carrierName, customerDetails.CustomerNumber, shipper, shipment, response, trackEmails, requestDateTime, responseTime);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                response.ErrorMessage.ErrorCode = "017";
            //                response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SERVICE_UNAVAILABLE;
            //            }
            //        }
            //        else
            //        {
            //            response.ErrorMessage.ErrorCode = "018";
            //            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_LOG_IN_FAILED;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    response.ErrorMessage.ErrorCode = "507";
            //    response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
            //    if (m_log != null) m_log.Error(ex.ToString());
            //}
            return response;
        }

        //private void SaveSuccessfulRequest(CustomerInfo customer, BookingPickUpLocation shipper, BookingPickUpInfo shipment, BookingResponse response)
        //{
        //    string mainXmlName = (new XmlGenerator()).CompileBookRequestFile(customer, shipment, shipper, response);

        //    (new BOBookingRequest()).SaveBookRequest(mainXmlName);

        //}

        //private void BookCarrierShipment(ServiceDetails serviceDetails, BookingPickUpLocation shipper, BookingPickUpInfo shipment, string originRegion, int bookUserID, BookingResponse response)
        //{
        //    try
        //    {
        //        switch (serviceDetails.CarrierName)
        //        {
        //            case "FEDEX":
        //                response.ConfirmationNumber = RequestFedexBooking(serviceDetails, shipper, shipment, response);
        //                break;
        //            case "DHL":
        //                response.ConfirmationNumber = RequestDHLBooking(serviceDetails, shipper, shipment, originRegion, response);
        //                break;
        //            case "SBA":
        //                response.ConfirmationNumber = RequestSBABooking(shipper, shipment, bookUserID, response);
        //                break;
        //            case "UPS":
        //                response.ConfirmationNumber = RequestUPSBooking(serviceDetails, shipper, shipment, response);
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ConfirmationNumber = "";
        //        response.ErrorMessage.ErrorCode = "515";
        //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
        //        if (m_log != null) m_log.Error(ex.ToString());
        //    }

        //}

        //private string RequestDHLBooking(ServiceDetails serviceDetails, BookingPickUpLocation shipper, BookingPickUpInfo shipment, string region, BookingResponse bookingResponse)
        //{
        //    string result = "";
        //    try
        //    {
        //        System.Net.HttpWebRequest request;
        //        Uri uri = new Uri(@"https://xmlpitest-ea.dhl.com/XMLShippingServlet");
        //        request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(uri);
        //        request.Method = "POST";
        //        request.ContentType = "text/xml; charset=utf-8";

        //        string requestXML = "";
        //        switch (region)
        //        {
        //            case "AM":
        //            default:
        //                //ConvertDimensionWeight(shipment, shipper.Country);
        //                requestXML = CreateDHLBookingXML_AM(serviceDetails, shipper, shipment);
        //                break;
        //            case "EA":
        //                //ConvertDimensionWeight(shipment, shipper.Country);
        //                requestXML = CreateDHLBookingXML_EA(serviceDetails, shipper, shipment);
        //                break;
        //            case "AP":
        //                //ConvertDimensionWeight(shipment, shipper.Country);
        //                requestXML = CreateDHLBookingXML_AP(serviceDetails, shipper, shipment);
        //                break;
        //        }

        //        XmlDocument soapXml = new XmlDocument();
        //        soapXml.LoadXml(requestXML);

        //        using (Stream stream = request.GetRequestStream())
        //        {
        //            soapXml.Save(stream);
        //        }
        //        XDocument xmlDoc;
        //        System.Net.HttpWebResponse response = null;
        //        try
        //        {
        //            response = (System.Net.HttpWebResponse)request.GetResponse();
        //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                XmlReader responseStream = XmlReader.Create(request.GetResponse().GetResponseStream());
        //                xmlDoc = XDocument.Load(responseStream);
        //                if (responseStream != null)
        //                    responseStream.Close();
        //                XElement confirmationNumber = xmlDoc.Root.Element("ConfirmationNumber");
        //                if (confirmationNumber != null)
        //                {
        //                    result = confirmationNumber.Value;
        //                    XElement readyByTime = xmlDoc.Root.Element("ReadyByTime");
        //                    if (readyByTime != null)
        //                        bookingResponse.ReadyByTime = readyByTime.Value;
        //                }
        //                else
        //                {
        //                    XElement condition = xmlDoc.Root.Element("Response").Element("Status").Element("Condition");
        //                    if (condition != null)
        //                    {
        //                        bookingResponse.ErrorMessage.ErrorCode = condition.Element("ConditionCode").Value;
        //                        bookingResponse.ErrorMessage.ErrorMessage = condition.Element("ConditionData").Value;
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            if (response != null)
        //            {
        //                response.Close();
        //            }
        //            response = null;
        //            request = null;
        //            bookingResponse.ErrorMessage.ErrorCode = "508";
        //            bookingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
        //            if (m_log != null) m_log.Error(ex.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        bookingResponse.ErrorMessage.ErrorCode = "509";
        //        bookingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
        //        if (m_log != null) m_log.Error(ex.ToString());
        //    }
        //    return result;
        //}

        //private string RequestFedexBooking(ServiceDetails serviceDetails, BookingPickUpLocation shipper, BookingPickUpInfo shipment, BookingResponse bookingResponse)
        //{
        //    string result = "";
        //    bool pickupAvailable = false;
        //    try
        //    {
        //        DateTime pickupDate = DateTime.ParseExact(shipment.PickUpDate, "yyyy-MM-dd", null), readyTime = DateTime.ParseExact(shipment.ReadyTime, "HH:mm", null), closeTime = DateTime.ParseExact(shipment.CloseTime, "HH:mm", null);

        //        PickupAvailabilityRequest pickupCheckRequest = CreatePickupAvailabilityRequest(serviceDetails, shipper, shipment);
        //        PickupService service = new PickupService();
        //        PickupAvailabilityReply pickupCheckReply = service.getPickupAvailability(pickupCheckRequest);
        //        if (pickupCheckReply.HighestSeverity == NotificationSeverityType.SUCCESS || pickupCheckReply.HighestSeverity == NotificationSeverityType.NOTE || pickupCheckReply.HighestSeverity == NotificationSeverityType.WARNING)
        //        {
        //            if (pickupCheckReply.Options != null && pickupCheckReply.Options.Length > 0)
        //            {
        //                string pattern = @"^PT(.*)H(.*)M$";
        //                Match accessTime;

        //                foreach (PickupScheduleOption option in pickupCheckReply.Options)
        //                {
        //                    if (option.CutOffTime.TimeOfDay < readyTime.TimeOfDay)
        //                    {
        //                        readyTime = option.CutOffTime;
        //                    }

        //                    accessTime = Regex.Match(option.AccessTime, pattern);
        //                    if (readyTime.AddHours(int.Parse(accessTime.Groups[1].Value)).AddMinutes(int.Parse(accessTime.Groups[2].Value)).TimeOfDay > closeTime.TimeOfDay)
        //                    {
        //                        bookingResponse.ErrorMessage.ErrorCode = "031";
        //                        bookingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_CLOSE_TIME_TOO_EARLY;
        //                        continue;
        //                    }
        //                    else
        //                    {
        //                        bookingResponse.ErrorMessage.ErrorCode = "";
        //                        bookingResponse.ErrorMessage.ErrorMessage = "";
        //                        shipment.ReadyTime = readyTime.ToString("HH:mm");
        //                        shipment.PickUpDate = option.PickupDate.ToString("yyyy-MM-dd");
        //                        bookingResponse.PickupDate = shipment.PickUpDate;
        //                        bookingResponse.ReadyByTime = shipment.ReadyTime;
        //                        pickupAvailable = true;
        //                        break;
        //                    }

        //                }
        //            }
        //            else
        //            {
        //                bookingResponse.ErrorMessage.ErrorCode = "032";
        //                bookingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_NO_PICKUP_AVAILABLE;
        //            }
        //        }
        //        else
        //        {
        //            bookingResponse.ErrorMessage.ErrorCode = pickupCheckReply.Notifications[0].Code;
        //            bookingResponse.ErrorMessage.ErrorMessage = pickupCheckReply.Notifications[0].Message;
        //        }

        //        if (pickupAvailable)
        //        {
        //            CreatePickupRequest request = CreatePickupRequest(serviceDetails, shipper, shipment);

        //            CreatePickupReply reply = service.createPickup(request);
        //            if (reply.HighestSeverity == NotificationSeverityType.SUCCESS || reply.HighestSeverity == NotificationSeverityType.NOTE || reply.HighestSeverity == NotificationSeverityType.WARNING)
        //            {
        //                result = reply.PickupConfirmationNumber;
        //                bookingResponse.ConfirmationNumber = result;
        //            }
        //            else
        //            {
        //                bookingResponse.ReadyByTime = null;
        //                bookingResponse.PickupDate = null;
        //                bookingResponse.ErrorMessage.ErrorMessage = reply.Notifications[0].Message;
        //                bookingResponse.ErrorMessage.ErrorCode = reply.Notifications[0].Code;
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        bookingResponse.ErrorMessage.ErrorCode = "510";
        //        bookingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
        //        if (m_log != null) m_log.Error(ex.ToString());
        //    }
        //    return result;
        //}

        //private string RequestSBABooking(BookingPickUpLocation shipper, BookingPickUpInfo shipment, int bookUserID, BookingResponse bookingResponse)
        //{
        //    string hawb = "";

        //    hawb = (new BOSBABooking()).CreateOrder(shipper, shipment, bookUserID, bookingResponse);

        //    return hawb;
        //}

        //private string RequestUPSBooking(ServiceDetails serviceDetails, BookingPickUpLocation shipper, BookingPickUpInfo shipment, BookingResponse bookingResponse)
        //{
        //    string confirmationNumber = "";
        //    try
        //    {
        //        UPSPickupWebReference.PickupService pickupService = new UPSPickupWebReference.PickupService();
        //        UPSPickupWebReference.UPSSecurity upss = new UPSPickupWebReference.UPSSecurity();
        //        UPSPickupWebReference.UPSSecurityServiceAccessToken upssSvcAccessToken = new UPSPickupWebReference.UPSSecurityServiceAccessToken();
        //        upssSvcAccessToken.AccessLicenseNumber = serviceDetails.License;
        //        upss.ServiceAccessToken = upssSvcAccessToken;
        //        CarrierIntegrationCore.Booking.UPSPickupWebReference.UPSSecurityUsernameToken upssUsrNameToken = new CarrierIntegrationCore.Booking.UPSPickupWebReference.UPSSecurityUsernameToken();
        //        upssUsrNameToken.Username = serviceDetails.AccountName;
        //        upssUsrNameToken.Password = serviceDetails.Password;
        //        upss.UsernameToken = upssUsrNameToken;
        //        pickupService.UPSSecurityValue = upss;

        //        UPSPickupWebReference.PickupCreationRequest pickupCreationRequest = new UPSPickupWebReference.PickupCreationRequest();
        //        UPSPickupWebReference.RequestType request = new UPSPickupWebReference.RequestType();
        //        String[] requestOption = { " " };
        //        request.RequestOption = requestOption;
        //        pickupCreationRequest.Request = request;
        //        pickupCreationRequest.RatePickupIndicator = "N";

        //        UPSPickupWebReference.ShipperType shipperType = new UPSPickupWebReference.ShipperType();
        //        UPSPickupWebReference.AccountType account = new UPSPickupWebReference.AccountType();
        //        account.AccountCountryCode = "US";
        //        account.AccountNumber = serviceDetails.AccountNumber;
        //        shipperType.Account = account;
        //        pickupCreationRequest.Shipper = shipperType;

        //        UPSPickupWebReference.PickupDateInfoType pickupDateInfo = new UPSPickupWebReference.PickupDateInfoType();
        //        pickupDateInfo.CloseTime = shipment.CloseTime.Replace(":", "");
        //        pickupDateInfo.PickupDate = shipment.PickUpDate.Replace("-", "");
        //        pickupDateInfo.ReadyTime = shipment.ReadyTime.Replace(":", "");
        //        pickupCreationRequest.PickupDateInfo = pickupDateInfo;

        //        UPSPickupWebReference.PickupAddressType pickupAddress = new UPSPickupWebReference.PickupAddressType();
        //        pickupAddress.AddressLine = shipper.Address.ToArray();
        //        pickupAddress.City = shipper.City;
        //        pickupAddress.StateProvince = shipper.State;
        //        pickupAddress.CountryCode = shipper.Country;
        //        pickupAddress.PostalCode = shipper.PostalCode;
        //        pickupAddress.CompanyName = shipper.CompanyName;
        //        pickupAddress.ContactName = shipper.ContactName;
        //        pickupAddress.ResidentialIndicator = shipment.LocationType == LocationType.Residence ? "Y" : "N";

        //        UPSPickupWebReference.PhoneType phoneType = new UPSPickupWebReference.PhoneType();
        //        phoneType.Number = shipper.ContactPhone;
        //        if (!string.IsNullOrEmpty(shipper.ContactPhoneExt))
        //            phoneType.Extension = shipper.ContactPhoneExt;
        //        pickupAddress.Phone = phoneType;
        //        if (!string.IsNullOrEmpty(shipment.PackageLocation) && shipment.PackageLocation.Length <= 11)
        //            pickupAddress.PickupPoint = shipment.PackageLocation;

        //        pickupCreationRequest.PickupAddress = pickupAddress;
        //        pickupCreationRequest.AlternateAddressIndicator = "N";

        //        UPSPickupWebReference.PickupPieceType[] pickupPiece = new UPSPickupWebReference.PickupPieceType[1];
        //        UPSPickupWebReference.PickupPieceType pickupType = new UPSPickupWebReference.PickupPieceType();
        //        pickupType.ContainerCode = "01";
        //        pickupType.DestinationCountryCode = shipper.DestinationCountry;
        //        pickupType.Quantity = shipment.PackageCount.ToString();

        //        if (shipper.Country == "US" && shipper.DestinationCountry == "US")
        //            pickupType.ServiceCode = "003";
        //        else if (shipper.Country == "CA" && shipper.DestinationCountry == "CA")
        //            pickupType.ServiceCode = "011";
        //        else
        //            pickupType.ServiceCode = "065";

        //        pickupPiece[0] = pickupType;
        //        pickupCreationRequest.PickupPiece = pickupPiece;

        //        UPSPickupWebReference.WeightType totalWeight = new UPSPickupWebReference.WeightType();
        //        totalWeight.UnitOfMeasurement = shipment.WeightUnit == WeightUnit.LB ? "LBS" : "KGS";
        //        totalWeight.Weight = shipment.TotalWeight.ToString("f1");

        //        pickupCreationRequest.TotalWeight = totalWeight;
        //        pickupCreationRequest.OverweightIndicator = "N";

        //        pickupCreationRequest.PaymentMethod = "01";
        //        pickupCreationRequest.SpecialInstruction = shipment.SpecialInstruction;

        //        System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
        //        UPSPickupWebReference.PickupCreationResponse pickupCreationResponse = pickupService.ProcessPickupCreation(pickupCreationRequest);

        //        if (pickupCreationResponse == null || pickupCreationResponse.Response.ResponseStatus.Code != "1")
        //        {
        //            bookingResponse.ErrorMessage.ErrorCode = "518";
        //            bookingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
        //        }
        //        else
        //        {
        //            confirmationNumber = pickupCreationResponse.PRN;
        //        }
        //    }
        //    catch (System.Web.Services.Protocols.SoapException ex)
        //    {
        //        XDocument xmlDoc = XDocument.Parse(ex.Detail.InnerXml);
        //        XNamespace env = "http://www.ups.com/XMLSchema/XOLTWS/Error/v1.1";
        //        XElement xmlErr = xmlDoc.Root.Descendants(env + "PrimaryErrorCode").First();
        //        if (xmlErr != null)
        //        {
        //            bookingResponse.ErrorMessage.ErrorCode = xmlErr.Descendants(env + "Code").First().Value;
        //            bookingResponse.ErrorMessage.ErrorMessage = xmlErr.Descendants(env + "Description").First().Value;
        //        }
        //        else
        //        {
        //            bookingResponse.ErrorMessage.ErrorCode = "516";
        //            bookingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
        //        }
        //        if (m_log != null) m_log.Error(ex.Detail.InnerText.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        bookingResponse.ErrorMessage.ErrorCode = "517";
        //        bookingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
        //        if (m_log != null) m_log.Error(ex.ToString());

        //    }
        //    return confirmationNumber;
        //}

        //private void SendEmailNotifications(string carrier, string customerNumber, BookingPickUpLocation shipper, BookingPickUpInfo shipment, BookingResponse response, List<string> trackEmails, DateTime requestDateTime, DateTime responseDateTime)
        //{
        //    //try
        //    //{
        //    //    var startText = "Booking Request from Service By Air:";

        //    //    string mailBody = BusinessLogic.Utils.GetTemplateContent(HttpContext.Current.Server.MapPath("~/MailTemplates/BookingRequest.htm"));

        //    //    string dimensionUnit = shipment.DimensionUnit.ToString();
        //    //    string weightUnit = shipment.WeightUnit.ToString();

        //    //    StringBuilder packages = new StringBuilder();
        //    //    if (shipment.Packages != null && shipment.Packages.Count > 0)
        //    //    {
        //    //        packages.Append("Package Information: <br />");
        //    //        for (int i = 0; i < shipment.Packages.Count; i++)
        //    //        {
        //    //            packages.Append("Package ");
        //    //            packages.Append((i + 1).ToString());
        //    //            packages.Append(" : <br />Weight: ");
        //    //            packages.Append(shipment.Packages[i].Weight.ToString());
        //    //            packages.Append(" ");
        //    //            packages.Append(weightUnit);
        //    //            packages.Append("<br />Height: ");
        //    //            packages.Append(shipment.Packages[i].Height.ToString());
        //    //            packages.Append(" ");
        //    //            packages.Append(dimensionUnit);
        //    //            packages.Append("<br />Width: ");
        //    //            packages.Append(shipment.Packages[i].Width.ToString());
        //    //            packages.Append(" ");
        //    //            packages.Append(dimensionUnit);
        //    //            packages.Append(" <br />Length: ");
        //    //            packages.Append(shipment.Packages[i].Length.ToString());
        //    //            packages.Append(" ");
        //    //            packages.Append(dimensionUnit);
        //    //            packages.Append("<br />");
        //    //        }
        //    //    }
        //    //    StringBuilder domesticSettings = new StringBuilder();
        //    //    if (shipment.DomSettings != null)
        //    //    {
        //    //        string receiverAddress = string.Empty;
        //    //        foreach (string addr in shipment.DomSettings.ReceiverAddress)
        //    //            receiverAddress += addr + ", ";

        //    //        packages.Append("Receiver Name: ");
        //    //        packages.Append(shipment.DomSettings.ReceiverName);
        //    //        packages.Append("<br />Receiver Phone: ");
        //    //        packages.Append(shipment.DomSettings.ReceiverPhone);
        //    //        packages.Append("<br />Receiver Company Name:");
        //    //        packages.Append(shipment.DomSettings.ReceiverCompanyName);
        //    //        packages.Append("<br />Receiver's Address: ");
        //    //        packages.Append(receiverAddress);
        //    //        packages.Append(", ");
        //    //        packages.Append(shipment.DomSettings.ReceiverCity);
        //    //        packages.Append(", ");
        //    //        packages.Append(shipment.DomSettings.ReceiverState);
        //    //        packages.Append(" ");
        //    //        packages.Append(shipment.DomSettings.ReceiverZipcode);
        //    //        packages.Append("<br />Delivery Date: ");
        //    //        packages.Append(shipment.DomSettings.DeliveryDate);
        //    //        packages.Append("<br />Service Level: ");
        //    //        packages.Append(shipment.DomSettings.Service);
        //    //        packages.Append("<br />Reference Number: ");
        //    //        packages.Append(shipment.DomSettings.ReferenceNumber ?? "");
        //    //        packages.Append("<br />");
        //    //    }
        //    //    string shipperAddress = string.Empty;
        //    //    foreach (string addr in shipper.Address)
        //    //        shipperAddress += addr + ", ";

        //    //    mailBody = mailBody.Replace("[[ShipperAddress]]", shipperAddress)
        //    //                       .Replace("[[STARTTEXT]]", startText)
        //    //                       .Replace("[[ShipperCity]]", shipper.City)
        //    //                       .Replace("[[ShipperState]]", shipper.State)
        //    //                       .Replace("[[ShipperPostalCode]]", shipper.PostalCode)
        //    //                       .Replace("[[ShipperCountry]]", shipper.Country)
        //    //                       .Replace("[[CompanyName]]", shipper.CompanyName)
        //    //                       .Replace("[[BookerName]]", shipper.BookerName)
        //    //                       .Replace("[[BookerPhone]]", shipper.BookerPhone)
        //    //                       .Replace("[[ContactName]]", shipper.ContactName)
        //    //                       .Replace("[[ContactPhone]]", shipper.ContactPhone)
        //    //                       .Replace("[[PackageCount]]", shipment.PackageCount.ToString())
        //    //                       .Replace("[[TotalWeight]]", shipment.TotalWeight.ToString() + " " + shipment.WeightUnit.ToString())
        //    //                       .Replace("[[Insurance]]", shipment.Insurance.ToString() + " " + shipment.InsuranceCurrency)
        //    //                       .Replace("[[PackageLocation]]", shipment.PackageLocation)
        //    //                       .Replace("[[LocationType]]", shipment.LocationType.ToString())
        //    //                       .Replace("[[PickupDate]]", shipment.PickUpDate)
        //    //                       .Replace("[[ReadyTime]]", shipment.ReadyTime)
        //    //                       .Replace("[[CloseTime]]", shipment.CloseTime)
        //    //                       .Replace("[[SpecialInstruction]]", shipment.SpecialInstruction ?? "")
        //    //                       .Replace("[[DoorTo]]", shipment.DoorTo.ToString())
        //    //                       .Replace("[[SpecialServices]]", "")
        //    //                       .Replace("[[PackageInformation]]", packages.ToString())
        //    //                       .Replace("[[DomesticSettings]]", domesticSettings.ToString())
        //    //                       .Replace("[[ConfirmationNumber]]", response.ConfirmationNumber);

        //    //    if (!string.IsNullOrEmpty(response.PickupDate))
        //    //        mailBody = mailBody.Replace("[[ReturnedPickupDate]]", "Returned Pick up Date: " + response.PickupDate + "<br />");
        //    //    else
        //    //        mailBody = mailBody.Replace("[[ReturnedPickupDate]]", "");
        //    //    if (!string.IsNullOrEmpty(response.ReadyByTime))
        //    //        mailBody = mailBody.Replace("[[ReturnedReadyTime]]", "Returned Package Ready by Time: " + response.ReadyByTime + "<br />");
        //    //    else
        //    //        mailBody = mailBody.Replace("[[ReturnedReadyTime]]", "");

        //    //    if (string.IsNullOrEmpty(response.ConfirmationNumber))
        //    //    {
        //    //        mailBody = mailBody.Replace("[[ErrorMessage]]", "Error Message: " + response.ErrorMessage.ErrorMessage + "<br />");
        //    //        mailBody = mailBody.Replace("[[ErrorCode]]", "Error Code: " + response.ErrorMessage.ErrorCode + "<br />");
        //    //    }
        //    //    else
        //    //    {
        //    //        mailBody = mailBody.Replace("[[ErrorMessage]]", "");
        //    //        mailBody = mailBody.Replace("[[ErrorCode]]", "");
        //    //    }

        //    //    var mailer = new BusinessLogic.Mail.Mailer();
        //    //    string emailFrom = BusinessLogic.Mail.Mailer.CurrentMailSettings.MailFrom;
        //    //    mailer.SendMessage(emailFrom, trackEmails, null, null, "ABC Logistics Booking Request Information ", mailBody, null, null);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    if (m_log != null) m_log.Error(ex.ToString());
        //    //}
        //}


        #region Functions
        #region UPS

        #endregion
        //        #region FedEx
        //        private CreatePickupRequest CreatePickupRequest(ServiceDetails serviceDetails, BookingPickUpLocation shipper, BookingPickUpInfo shipment)
        //        {
        //            CreatePickupRequest request = new CreatePickupRequest();
        //            request.WebAuthenticationDetail = new WebAuthenticationDetail();
        //            request.WebAuthenticationDetail.UserCredential = new WebAuthenticationCredential();
        //            request.WebAuthenticationDetail.UserCredential.Key = serviceDetails.AccountName;
        //            request.WebAuthenticationDetail.UserCredential.Password = serviceDetails.Password;
        //            request.ClientDetail = new ClientDetail();
        //            request.ClientDetail.AccountNumber = serviceDetails.AccountNumber;
        //            request.ClientDetail.MeterNumber = serviceDetails.License;
        //            request.TransactionDetail = new TransactionDetail();
        //            request.TransactionDetail.CustomerTransactionId = "ABC Logistics Shipping Web Service Booking Shipment";
        //            request.Version = new VersionId();
        //            request.OriginDetail = new PickupOriginDetail();
        //            //Optional
        //            request.OriginDetail.PickupLocation = new ContactAndAddress();
        //            request.OriginDetail.PickupLocation.Contact = new Contact();
        //            request.OriginDetail.PickupLocation.Contact.PersonName = shipper.ContactName;
        //            request.OriginDetail.PickupLocation.Contact.CompanyName = shipper.CompanyName;
        //            request.OriginDetail.PickupLocation.Contact.PhoneNumber = shipper.ContactPhone;
        //            if (!string.IsNullOrEmpty(shipper.ContactPhoneExt))
        //                request.OriginDetail.PickupLocation.Contact.PhoneExtension = shipper.ContactPhoneExt;
        //            //Optional
        //            // Pick up location Address
        //            request.OriginDetail.PickupLocation.Address = new Address();
        //            request.OriginDetail.PickupLocation.Address.StreetLines = new string[shipper.Address.Count];
        //            for (int i = 0; i < shipper.Address.Count; i++)
        //            {
        //                request.OriginDetail.PickupLocation.Address.StreetLines[i] = shipper.Address[i];
        //            }

        //            request.OriginDetail.PickupLocation.Address.City = shipper.City;
        //            request.OriginDetail.PickupLocation.Address.StateOrProvinceCode = shipper.State;
        //            request.OriginDetail.PickupLocation.Address.PostalCode = shipper.PostalCode;
        //            request.OriginDetail.PickupLocation.Address.CountryCode = shipper.Country;
        //            request.OriginDetail.PickupLocation.Address.Residential = shipment.LocationType == LocationType.Residence;
        //            request.OriginDetail.PickupLocation.Address.ResidentialSpecified = true;
        //            switch (shipment.PackageLocation)
        //            {
        //                case "NONE":
        //                default:
        //                    request.OriginDetail.PackageLocation = PickupBuildingLocationType.NONE;
        //                    break;
        //                case "FRONT":
        //                    request.OriginDetail.PackageLocation = PickupBuildingLocationType.FRONT;
        //                    break;
        //                case "REAR":
        //                    request.OriginDetail.PackageLocation = PickupBuildingLocationType.REAR;
        //                    break;
        //                case "SIDE":
        //                    request.OriginDetail.PackageLocation = PickupBuildingLocationType.SIDE;
        //                    break;
        //            }

        //            request.OriginDetail.PackageLocationSpecified = true;

        //            request.OriginDetail.ReadyTimestamp = DateTime.ParseExact(shipment.PickUpDate + " " + shipment.ReadyTime, "yyyy-MM-dd HH:mm", null);
        //            request.OriginDetail.ReadyTimestampSpecified = true;
        //            request.OriginDetail.CompanyCloseTime = DateTime.ParseExact(shipment.PickUpDate + " " + shipment.CloseTime, "yyyy-MM-dd HH:mm", null);
        //            request.OriginDetail.CompanyCloseTimeSpecified = true;
        //            if (serviceDetails.ServiceCode == "GROUND")
        //                request.CarrierCode = CarrierCodeType.FDXG;
        //            else
        //                request.CarrierCode = CarrierCodeType.FDXE;
        //            request.CarrierCodeSpecified = true;
        //            request.PackageCount = shipment.PackageCount.ToString();
        //            request.TotalWeight = new Weight();
        //            request.TotalWeight.Value = shipment.TotalWeight;
        //            request.TotalWeight.ValueSpecified = true;

        //            request.TotalWeight.Units = shipment.WeightUnit == WeightUnit.LB ? WeightUnits.LB : WeightUnits.KG;
        //            request.TotalWeight.UnitsSpecified = true;
        //            request.Remarks = "Shipping Web Service Create Pickup";
        //            return request;
        //        }

        //        private PickupAvailabilityRequest CreatePickupAvailabilityRequest(ServiceDetails serviceDetails, BookingPickUpLocation shipper, BookingPickUpInfo shipment)
        //        {
        //            PickupAvailabilityRequest request = new PickupAvailabilityRequest();

        //            request.WebAuthenticationDetail = new WebAuthenticationDetail();
        //            request.WebAuthenticationDetail.UserCredential = new WebAuthenticationCredential();
        //            request.WebAuthenticationDetail.UserCredential.Key = serviceDetails.AccountName;
        //            request.WebAuthenticationDetail.UserCredential.Password = serviceDetails.Password;

        //            request.ClientDetail = new ClientDetail();
        //            request.ClientDetail.AccountNumber = serviceDetails.AccountNumber;
        //            request.ClientDetail.MeterNumber = serviceDetails.License;
        //            request.TransactionDetail = new TransactionDetail();
        //            request.TransactionDetail.CustomerTransactionId = "ABC Logistics Shipping Web Service Pickup Availability";
        //            request.Version = new VersionId();

        //            request.PickupAddress = new Address();
        //            int addressCount = shipper.Address.Count;
        //            request.PickupAddress.StreetLines = new string[addressCount];
        //            for (int i = 0; i < addressCount; i++)
        //            {
        //                request.PickupAddress.StreetLines[i] = shipper.Address[i];
        //            }
        //            request.PickupAddress.City = shipper.City;
        //            request.PickupAddress.StateOrProvinceCode = shipper.State;
        //            request.PickupAddress.PostalCode = shipper.PostalCode;
        //            request.PickupAddress.CountryCode = shipper.Country;

        //            request.PickupRequestType = new PickupRequestType[2];
        //            request.PickupRequestType[0] = PickupRequestType.FUTURE_DAY;
        //            request.PickupRequestType[1] = PickupRequestType.SAME_DAY;

        //            request.DispatchDate = DateTime.ParseExact(shipment.PickUpDate, "yyyy-MM-dd", null);
        //            request.DispatchDateSpecified = true;
        //            request.PackageReadyTime = DateTime.ParseExact(shipment.ReadyTime, "HH:mm", null);
        //            request.PackageReadyTimeSpecified = true;
        //            request.CustomerCloseTime = DateTime.ParseExact(shipment.CloseTime, "HH:mm", null);
        //            request.CustomerCloseTimeSpecified = true;

        //            request.Carriers = new CarrierCodeType[1];
        //            if (serviceDetails.ServiceCode == "GROUND")
        //                request.Carriers[0] = CarrierCodeType.FDXG;
        //            else
        //                request.Carriers[0] = CarrierCodeType.FDXE;

        //            request.ShipmentAttributes = new PickupShipmentAttributes();
        //            switch (serviceDetails.ServiceCode)
        //            {
        //                case "GROUND":
        //                    request.ShipmentAttributes.ServiceType = ServiceType.FEDEX_GROUND;
        //                    break;
        //                case "ECONOMY":
        //                    request.ShipmentAttributes.ServiceType = ServiceType.INTERNATIONAL_ECONOMY;
        //                    break;
        //                case "EXPRESS":
        //                    request.ShipmentAttributes.ServiceType = ServiceType.FEDEX_EXPRESS_SAVER;
        //                    break;
        //            }
        //            request.ShipmentAttributes.ServiceTypeSpecified = true;

        //            request.ShipmentAttributes.Weight = new Weight();
        //            request.ShipmentAttributes.Weight.Value = shipment.TotalWeight;
        //            request.ShipmentAttributes.Weight.ValueSpecified = true;
        //            request.ShipmentAttributes.Weight.Units = shipment.WeightUnit == WeightUnit.LB ? WeightUnits.LB : WeightUnits.KG;
        //            request.ShipmentAttributes.Weight.UnitsSpecified = true;

        //            return request;
        //        }
        //        #endregion
        //        #region DHL
        //        private string CreateDHLBookingXML_AM(ServiceDetails serviceDetails, BookingPickUpLocation shipper, BookingPickUpInfo shipment)
        //        {
        //            StringBuilder soapMsg = new StringBuilder();
        //            soapMsg.Append(@"<?xml version=""1.0"" encoding=""UTF-8""?>
        //                <req:BookPickupRequest xmlns:req=""http://www.dhl.com"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.dhl.com
        //                book-pickup-req.xsd""><Request><ServiceHeader><SiteID>");
        //            soapMsg.Append(serviceDetails.AccountName);
        //            soapMsg.Append("</SiteID><Password>");
        //            soapMsg.Append(serviceDetails.Password);
        //            soapMsg.Append("</Password>");
        //            soapMsg.Append("</ServiceHeader></Request><Requestor><AccountType>D</AccountType>");
        //            soapMsg.Append("<AccountNumber>");
        //            soapMsg.Append(serviceDetails.AccountNumber);
        //            soapMsg.Append("</AccountNumber>");
        //            soapMsg.Append("<RequestorContact><PersonName>");
        //            soapMsg.Append(shipper.BookerName);
        //            soapMsg.Append("</PersonName><Phone>");
        //            soapMsg.Append(shipper.BookerPhone);
        //            soapMsg.Append("</Phone>");
        //            if (!string.IsNullOrEmpty(shipper.BookerPhoneExt))
        //            {
        //                soapMsg.Append("<PhoneExtension>");
        //                soapMsg.Append(shipper.BookerPhoneExt);
        //                soapMsg.Append("</PhoneExtension>");
        //            }
        //            soapMsg.Append("</RequestorContact></Requestor><Place>");

        //            switch (shipment.LocationType)
        //            {
        //                case LocationType.Business:
        //                    soapMsg.Append("<LocationType>B</LocationType><CompanyName>");
        //                    soapMsg.Append(shipper.CompanyName);
        //                    soapMsg.Append("</CompanyName>");
        //                    break;
        //                case LocationType.Residence:
        //                    soapMsg.Append("<LocationType>R</LocationType>");
        //                    break;
        //                case LocationType.Combination:
        //                    soapMsg.Append("<LocationType>C</LocationType><CompanyName>");
        //                    soapMsg.Append(shipper.CompanyName);
        //                    soapMsg.Append("</CompanyName>");
        //                    break;
        //            }
        //            soapMsg.Append("<Address1>");
        //            soapMsg.Append(shipper.Address[0]);
        //            soapMsg.Append("</Address1>");
        //            if (shipper.Address.Count > 1)
        //            {
        //                soapMsg.Append("<Address2>");
        //                soapMsg.Append(shipper.Address[1]);
        //                soapMsg.Append("</Address2>");
        //            }
        //            soapMsg.Append("<PackageLocation>");
        //            soapMsg.Append(string.IsNullOrEmpty(shipment.PackageLocation) ? "n/a" : shipment.PackageLocation);
        //            soapMsg.Append("</PackageLocation><City>");
        //            soapMsg.Append(shipper.City);
        //            soapMsg.Append("</City>");
        //            if (!string.IsNullOrEmpty(shipper.State))
        //            {
        //                soapMsg.Append("<StateCode>");
        //                soapMsg.Append(shipper.State);
        //                soapMsg.Append("</StateCode>");
        //            }
        //            soapMsg.Append("<CountryCode>");
        //            soapMsg.Append(shipper.Country);
        //            soapMsg.Append("</CountryCode><PostalCode>");
        //            soapMsg.Append(shipper.PostalCode);
        //            soapMsg.Append("</PostalCode></Place><Pickup><PickupDate>");
        //            soapMsg.Append(shipment.PickUpDate);
        //            soapMsg.Append("</PickupDate><ReadyByTime>");
        //            soapMsg.Append(shipment.ReadyTime);
        //            soapMsg.Append("</ReadyByTime><CloseTime>");
        //            soapMsg.Append(shipment.CloseTime);
        //            soapMsg.Append("</CloseTime><Pieces>");
        //            soapMsg.Append(shipment.PackageCount.ToString());
        //            soapMsg.Append("</Pieces><weight><Weight>");
        //            soapMsg.Append(shipment.TotalWeight.ToString("f1"));
        //            soapMsg.Append("</Weight>");
        //            if (shipment.WeightUnit == WeightUnit.KG)
        //            {
        //                soapMsg.Append("<WeightUnit>K</WeightUnit></weight>");
        //            }
        //            else
        //            {
        //                soapMsg.Append("<WeightUnit>L</WeightUnit></weight>");
        //            }
        //            if (!string.IsNullOrEmpty(shipment.SpecialInstruction))
        //            {
        //                soapMsg.Append("<SpecialInstructions>");
        //                soapMsg.Append(shipment.SpecialInstruction);
        //                soapMsg.Append("</SpecialInstructions>");
        //            }
        //            soapMsg.Append("</Pickup><PickupContact><PersonName>");
        //            soapMsg.Append(shipper.ContactName);
        //            soapMsg.Append("</PersonName><Phone>");
        //            soapMsg.Append(shipper.ContactPhone);
        //            soapMsg.Append("</Phone>");
        //            if (!string.IsNullOrEmpty(shipper.ContactPhoneExt))
        //            {
        //                soapMsg.Append("<PhoneExtension>");
        //                soapMsg.Append(shipper.ContactPhoneExt);
        //                soapMsg.Append("</PhoneExtension>");
        //            }
        //            soapMsg.Append("</PickupContact><ShipmentDetails><NumberOfPieces>");
        //            soapMsg.Append(shipment.PackageCount.ToString());
        //            soapMsg.Append("</NumberOfPieces><Weight>");
        //            soapMsg.Append(shipment.TotalWeight.ToString("f1"));
        //            soapMsg.Append("</Weight>");
        //            if (shipment.WeightUnit == WeightUnit.KG)
        //            {
        //                soapMsg.Append("<WeightUnit>K</WeightUnit>");
        //            }
        //            else
        //            {
        //                soapMsg.Append("<WeightUnit>L</WeightUnit>");
        //            }
        //            soapMsg.Append("<GlobalProductCode>D</GlobalProductCode>");
        //            soapMsg.Append("<DoorTo>");
        //            soapMsg.Append(shipment.DoorTo.ToString());
        //            soapMsg.Append("</DoorTo>");
        //            if (shipment.DimensionUnit == DimensionUnit.IN)
        //            {
        //                soapMsg.Append("<DimensionUnit>I</DimensionUnit>");
        //            }
        //            else
        //            {
        //                soapMsg.Append("<DimensionUnit>C</DimensionUnit>");
        //            }

        //            if (shipment.Insurance > 0)
        //            {
        //                soapMsg.Append("<InsuredAmount>");
        //                soapMsg.Append(shipment.Insurance.ToString("f2"));
        //                soapMsg.Append("</InsuredAmount><InsuredCurrencyCode>");
        //                soapMsg.Append(string.IsNullOrEmpty(shipment.InsuranceCurrency) ? "USD" : shipment.InsuranceCurrency);
        //                soapMsg.Append("</InsuredCurrencyCode>");
        //            }

        //            if (shipment.Packages != null && shipment.Packages.Count > 0)
        //            {
        //                soapMsg.Append("<Pieces>");
        //                foreach (Package package in shipment.Packages)
        //                {
        //                    soapMsg.Append("<Weight>");
        //                    soapMsg.Append(package.Weight.ToString("f1"));
        //                    soapMsg.Append("</Weight>");
        //                    soapMsg.Append("<Width>");
        //                    soapMsg.Append(Math.Round(package.Width).ToString());
        //                    soapMsg.Append("</Width>");
        //                    soapMsg.Append("<Height>");
        //                    soapMsg.Append(Math.Round(package.Height).ToString());
        //                    soapMsg.Append("</Height>");
        //                    soapMsg.Append("<Depth>");
        //                    soapMsg.Append(Math.Round(package.Length).ToString());
        //                    soapMsg.Append("</Depth>");
        //                }
        //                soapMsg.Append("</Pieces>");
        //            }

        //            if (shipment.SpecialServices.Count > 0)
        //            {
        //                foreach (string specialService in shipment.SpecialServices)
        //                {
        //                    soapMsg.Append("<SpecialService>");
        //                    soapMsg.Append(specialService);
        //                    soapMsg.Append("</SpecialService>");
        //                }
        //            }
        //            soapMsg.Append("</ShipmentDetails></req:BookPickupRequest>");

        //            return soapMsg.ToString();
        //        }

        //        private string CreateDHLBookingXML_EA(ServiceDetails serviceDetails, BookingPickUpLocation shipper, BookingPickUpInfo shipment)
        //        {
        //            StringBuilder soapMsg = new StringBuilder();
        //            soapMsg.Append(@"<?xml version=""1.0"" encoding=""UTF-8""?>
        //                <req:BookPickupRequestEA xmlns:req=""http://www.dhl.com"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.dhl.com
        //                book-pickup-req_EA.xsd""><Request><ServiceHeader><SiteID>");
        //            soapMsg.Append(serviceDetails.AccountName);
        //            soapMsg.Append("</SiteID><Password>");
        //            soapMsg.Append(serviceDetails.Password);
        //            soapMsg.Append("</Password>");
        //            soapMsg.Append("</ServiceHeader></Request><Requestor><AccountType>D</AccountType>");
        //            soapMsg.Append("<AccountNumber>");
        //            soapMsg.Append(serviceDetails.AccountNumber);
        //            soapMsg.Append("</AccountNumber><RequestorContact><PersonName>");
        //            soapMsg.Append(shipper.BookerName);
        //            soapMsg.Append("</PersonName><Phone>");
        //            soapMsg.Append(shipper.BookerPhone);
        //            soapMsg.Append("</Phone>");
        //            if (!string.IsNullOrEmpty(shipper.BookerPhoneExt))
        //            {
        //                soapMsg.Append("<PhoneExtension>");
        //                soapMsg.Append(shipper.BookerPhoneExt);
        //                soapMsg.Append("</PhoneExtension>");
        //            }
        //            soapMsg.Append("</RequestorContact>");
        //            if (!string.IsNullOrEmpty(shipper.CompanyName))
        //            {
        //                soapMsg.Append("<CompanyName>");
        //                soapMsg.Append(shipper.CompanyName);
        //                soapMsg.Append("</CompanyName>");
        //            }
        //            else
        //            {
        //                soapMsg.Append("<CompanyName>");
        //                soapMsg.Append(shipper.BookerName);
        //                soapMsg.Append("</CompanyName>");
        //            }
        //            soapMsg.Append("</Requestor><Place>");

        //            switch (shipment.LocationType)
        //            {
        //                case LocationType.Business:
        //                    soapMsg.Append("<LocationType>B</LocationType><CompanyName>");
        //                    soapMsg.Append(shipper.CompanyName);
        //                    soapMsg.Append("</CompanyName>");
        //                    break;
        //                case LocationType.Residence:
        //                    soapMsg.Append("<LocationType>R</LocationType>");
        //                    break;
        //                case LocationType.Combination:
        //                    soapMsg.Append("<LocationType>C</LocationType><CompanyName>");
        //                    soapMsg.Append(shipper.CompanyName);
        //                    soapMsg.Append("</CompanyName>");
        //                    break;
        //            }
        //            soapMsg.Append("<Address1>");
        //            soapMsg.Append(shipper.Address[0]);
        //            soapMsg.Append("</Address1>");
        //            if (shipper.Address.Count > 1)
        //            {
        //                soapMsg.Append("<Address2>");
        //                soapMsg.Append(shipper.Address[1]);
        //                soapMsg.Append("</Address2>");
        //                if (shipper.Address.Count > 2)
        //                {
        //                    soapMsg.Append("<Address3>");
        //                    soapMsg.Append(shipper.Address[2]);
        //                    soapMsg.Append("</Address3>");
        //                }
        //            }

        //            soapMsg.Append("<PackageLocation>");
        //            soapMsg.Append(string.IsNullOrEmpty(shipment.PackageLocation) ? "n/a" : shipment.PackageLocation);
        //            soapMsg.Append("</PackageLocation><City>");
        //            soapMsg.Append(shipper.City);
        //            soapMsg.Append("</City><CountryCode>");
        //            soapMsg.Append(shipper.Country);
        //            soapMsg.Append("</CountryCode>");
        //            if (!string.IsNullOrEmpty(shipper.State))
        //            {
        //                soapMsg.Append("<StateCode>");
        //                soapMsg.Append(shipper.State);
        //                soapMsg.Append("</StateCode>");
        //            }
        //            soapMsg.Append("<PostalCode>");
        //            soapMsg.Append(shipper.PostalCode);
        //            soapMsg.Append("</PostalCode></Place><Pickup><PickupDate>");
        //            soapMsg.Append(shipment.PickUpDate);
        //            soapMsg.Append("</PickupDate><ReadyByTime>");
        //            soapMsg.Append(shipment.ReadyTime);
        //            soapMsg.Append("</ReadyByTime><CloseTime>");
        //            soapMsg.Append(shipment.CloseTime);
        //            soapMsg.Append("</CloseTime><Pieces>");
        //            soapMsg.Append(shipment.PackageCount.ToString());
        //            soapMsg.Append("</Pieces><weight><Weight>");
        //            soapMsg.Append(shipment.TotalWeight.ToString("f1"));
        //            soapMsg.Append("</Weight>");
        //            if (shipment.WeightUnit == WeightUnit.KG)
        //            {
        //                soapMsg.Append("<WeightUnit>K</WeightUnit></weight>");
        //            }
        //            else
        //            {
        //                soapMsg.Append("<WeightUnit>L</WeightUnit></weight>");
        //            }
        //            if (!string.IsNullOrEmpty(shipment.SpecialInstruction))
        //            {
        //                soapMsg.Append("<SpecialInstructions>");
        //                soapMsg.Append(shipment.SpecialInstruction);
        //                soapMsg.Append("</SpecialInstructions>");
        //            }
        //            soapMsg.Append("</Pickup><PickupContact><PersonName>");
        //            soapMsg.Append(shipper.ContactName);
        //            soapMsg.Append("</PersonName><Phone>");
        //            soapMsg.Append(shipper.ContactPhone);
        //            soapMsg.Append("</Phone>");
        //            if (!string.IsNullOrEmpty(shipper.ContactPhoneExt))
        //            {
        //                soapMsg.Append("<PhoneExtension>");
        //                soapMsg.Append(shipper.ContactPhoneExt);
        //                soapMsg.Append("</PhoneExtension>");
        //            }
        //            soapMsg.Append("</PickupContact><ShipmentDetails><NumberOfPieces>");
        //            soapMsg.Append(shipment.PackageCount.ToString());
        //            soapMsg.Append("</NumberOfPieces><Weight>");
        //            soapMsg.Append(shipment.TotalWeight.ToString("f1"));
        //            soapMsg.Append("</Weight>");
        //            if (shipment.WeightUnit == WeightUnit.KG)
        //            {
        //                soapMsg.Append("<WeightUnit>K</WeightUnit>");
        //            }
        //            else
        //            {
        //                soapMsg.Append("<WeightUnit>L</WeightUnit>");
        //            }
        //            if (BOCountries.IsEUCountries(shipper.Country) && BOCountries.IsEUCountries(shipper.DestinationCountry))
        //            {
        //                soapMsg.Append("<GlobalProductCode>U</GlobalProductCode>");
        //            }
        //            else
        //            {
        //                soapMsg.Append("<GlobalProductCode>D</GlobalProductCode>");
        //            }
        //            soapMsg.Append("<DoorTo>");
        //            soapMsg.Append(shipment.DoorTo.ToString());
        //            soapMsg.Append("</DoorTo>");
        //            soapMsg.Append("<DimensionUnit>C</DimensionUnit>");

        //            if (shipment.Insurance > 0)
        //            {
        //                soapMsg.Append("<InsuredAmount>");
        //                soapMsg.Append(shipment.Insurance.ToString("f2"));
        //                soapMsg.Append("</InsuredAmount><InsuredCurrencyCode>");
        //                soapMsg.Append(string.IsNullOrEmpty(shipment.InsuranceCurrency) ? "USD" : shipment.InsuranceCurrency);
        //                soapMsg.Append("</InsuredCurrencyCode>");
        //            }

        //            if (shipment.Packages != null && shipment.Packages.Count > 0)
        //            {
        //                soapMsg.Append("<Pieces>");
        //                foreach (Package package in shipment.Packages)
        //                {
        //                    soapMsg.Append("<Weight>");
        //                    soapMsg.Append(package.Weight.ToString("f1"));
        //                    soapMsg.Append("</Weight>");
        //                    soapMsg.Append("<Width>");
        //                    soapMsg.Append(Math.Round(package.Width).ToString());
        //                    soapMsg.Append("</Width>");
        //                    soapMsg.Append("<Height>");
        //                    soapMsg.Append(Math.Round(package.Height).ToString());
        //                    soapMsg.Append("</Height>");
        //                    soapMsg.Append("<Depth>");
        //                    soapMsg.Append(Math.Round(package.Length).ToString());
        //                    soapMsg.Append("</Depth>");
        //                }
        //                soapMsg.Append("</Pieces>");
        //            }

        //            if (shipment.SpecialServices.Count > 0)
        //            {
        //                foreach (string specialService in shipment.SpecialServices)
        //                {
        //                    soapMsg.Append("<SpecialService>");
        //                    soapMsg.Append(specialService);
        //                    soapMsg.Append("</SpecialService>");
        //                }
        //            }
        //            soapMsg.Append("</ShipmentDetails></req:BookPickupRequestEA>");

        //            return soapMsg.ToString();
        //        }

        //        private string CreateDHLBookingXML_AP(ServiceDetails serviceDetails, BookingPickUpLocation shipper, BookingPickUpInfo shipment)
        //        {
        //            StringBuilder soapMsg = new StringBuilder();
        //            soapMsg.Append(@"<?xml version=""1.0"" encoding=""UTF-8""?>
        //                <req:BookPickupRequestAP xmlns:req=""http://www.dhl.com"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.dhl.com
        //                book-pickup-req.xsd""><Request><ServiceHeader><SiteID>");
        //            soapMsg.Append(serviceDetails.AccountName);
        //            soapMsg.Append("</SiteID><Password>");
        //            soapMsg.Append(serviceDetails.Password);
        //            soapMsg.Append("</Password>");
        //            soapMsg.Append("</ServiceHeader></Request><Requestor><AccountType>D</AccountType>");
        //            soapMsg.Append("<AccountNumber>");
        //            soapMsg.Append(serviceDetails.AccountNumber);
        //            soapMsg.Append("</AccountNumber>");
        //            soapMsg.Append("<RequestorContact><PersonName>");
        //            soapMsg.Append(shipper.BookerName);
        //            soapMsg.Append("</PersonName><Phone>");
        //            soapMsg.Append(shipper.BookerPhone);
        //            soapMsg.Append("</Phone>");
        //            if (!string.IsNullOrEmpty(shipper.BookerPhoneExt))
        //            {
        //                soapMsg.Append("<PhoneExtension>");
        //                soapMsg.Append(shipper.BookerPhoneExt);
        //                soapMsg.Append("</PhoneExtension>");
        //            }
        //            soapMsg.Append("</RequestorContact></Requestor><Place>");

        //            switch (shipment.LocationType)
        //            {
        //                case LocationType.Business:
        //                    soapMsg.Append("<LocationType>B</LocationType><CompanyName>");
        //                    soapMsg.Append(shipper.CompanyName);
        //                    soapMsg.Append("</CompanyName>");
        //                    break;
        //                case LocationType.Residence:
        //                    soapMsg.Append("<LocationType>R</LocationType>");
        //                    break;
        //                case LocationType.Combination:
        //                    soapMsg.Append("<LocationType>C</LocationType><CompanyName>");
        //                    soapMsg.Append(shipper.CompanyName);
        //                    soapMsg.Append("</CompanyName>");
        //                    break;
        //            }
        //            soapMsg.Append("<Address1>");
        //            soapMsg.Append(shipper.Address[0]);
        //            soapMsg.Append("</Address1>");
        //            if (shipper.Address.Count > 1)
        //            {
        //                soapMsg.Append("<Address2>");
        //                soapMsg.Append(shipper.Address[1]);
        //                soapMsg.Append("</Address2>");
        //                if (shipper.Address.Count > 2)
        //                {
        //                    soapMsg.Append("<Address3>");
        //                    soapMsg.Append(shipper.Address[2]);
        //                    soapMsg.Append("</Address3>");
        //                }
        //            }
        //            soapMsg.Append("<PackageLocation>");
        //            soapMsg.Append(string.IsNullOrEmpty(shipment.PackageLocation) ? "n/a" : shipment.PackageLocation);
        //            soapMsg.Append("</PackageLocation><City>");
        //            soapMsg.Append(shipper.City);
        //            soapMsg.Append("</City>");
        //            if (!string.IsNullOrEmpty(shipper.State))
        //            {
        //                soapMsg.Append("<StateCode>");
        //                soapMsg.Append(shipper.State);
        //                soapMsg.Append("</StateCode>");
        //            }
        //            soapMsg.Append("<CountryCode>");
        //            soapMsg.Append(shipper.Country);
        //            soapMsg.Append("</CountryCode><PostalCode>");
        //            soapMsg.Append(shipper.PostalCode);
        //            soapMsg.Append("</PostalCode></Place><Pickup><PickupDate>");
        //            soapMsg.Append(shipment.PickUpDate);
        //            soapMsg.Append("</PickupDate><ReadyByTime>");
        //            soapMsg.Append(shipment.ReadyTime);
        //            soapMsg.Append("</ReadyByTime><CloseTime>");
        //            soapMsg.Append(shipment.CloseTime);
        //            soapMsg.Append("</CloseTime><Pieces>");
        //            soapMsg.Append(shipment.PackageCount.ToString());
        //            soapMsg.Append("</Pieces><weight><Weight>");
        //            soapMsg.Append(shipment.TotalWeight.ToString("f1"));
        //            soapMsg.Append("</Weight>");
        //            if (shipment.WeightUnit == WeightUnit.KG)
        //            {
        //                soapMsg.Append("<WeightUnit>K</WeightUnit></weight>");
        //            }
        //            else
        //            {
        //                soapMsg.Append("<WeightUnit>L</WeightUnit></weight>");
        //            }
        //            if (!string.IsNullOrEmpty(shipment.SpecialInstruction))
        //            {
        //                soapMsg.Append("<SpecialInstructions>");
        //                soapMsg.Append(shipment.SpecialInstruction);
        //                soapMsg.Append("</SpecialInstructions>");
        //            }
        //            soapMsg.Append("</Pickup><PickupContact><PersonName>");
        //            soapMsg.Append(shipper.ContactName);
        //            soapMsg.Append("</PersonName><Phone>");
        //            soapMsg.Append(shipper.ContactPhone);
        //            soapMsg.Append("</Phone>");
        //            if (!string.IsNullOrEmpty(shipper.ContactPhoneExt))
        //            {
        //                soapMsg.Append("<PhoneExtension>");
        //                soapMsg.Append(shipper.ContactPhoneExt);
        //                soapMsg.Append("</PhoneExtension>");
        //            }
        //            soapMsg.Append("</PickupContact><ShipmentDetails><NumberOfPieces>");
        //            soapMsg.Append(shipment.PackageCount.ToString());
        //            soapMsg.Append("</NumberOfPieces><Weight>");
        //            soapMsg.Append(shipment.TotalWeight.ToString("f1"));
        //            soapMsg.Append("</Weight>");
        //            if (shipment.WeightUnit == WeightUnit.KG)
        //            {
        //                soapMsg.Append("<WeightUnit>K</WeightUnit>");
        //            }
        //            else
        //            {
        //                soapMsg.Append("<WeightUnit>L</WeightUnit>");
        //            }

        //            soapMsg.Append("<GlobalProductCode>D</GlobalProductCode>");
        //            soapMsg.Append("<DoorTo>");
        //            soapMsg.Append(shipment.DoorTo.ToString());
        //            soapMsg.Append("</DoorTo>");
        //            if (shipment.DimensionUnit == DimensionUnit.IN)
        //            {
        //                soapMsg.Append("<DimensionUnit>I</DimensionUnit>");
        //            }
        //            else
        //            {
        //                soapMsg.Append("<DimensionUnit>C</DimensionUnit>");
        //            }

        //            if (shipment.Insurance > 0)
        //            {
        //                soapMsg.Append("<InsuredAmount>");
        //                soapMsg.Append(shipment.Insurance.ToString("f2"));
        //                soapMsg.Append("</InsuredAmount><InsuredCurrencyCode>");
        //                soapMsg.Append(string.IsNullOrEmpty(shipment.InsuranceCurrency) ? "USD" : shipment.InsuranceCurrency);
        //                soapMsg.Append("</InsuredCurrencyCode>");
        //            }

        //            if (shipment.Packages != null && shipment.Packages.Count > 0)
        //            {
        //                soapMsg.Append("<Pieces>");
        //                foreach (Package package in shipment.Packages)
        //                {
        //                    soapMsg.Append("<Weight>");
        //                    soapMsg.Append(package.Weight.ToString("f1"));
        //                    soapMsg.Append("</Weight>");
        //                    soapMsg.Append("<Width>");
        //                    soapMsg.Append(Math.Round(package.Width).ToString());
        //                    soapMsg.Append("</Width>");
        //                    soapMsg.Append("<Height>");
        //                    soapMsg.Append(Math.Round(package.Height).ToString());
        //                    soapMsg.Append("</Height>");
        //                    soapMsg.Append("<Depth>");
        //                    soapMsg.Append(Math.Round(package.Length).ToString());
        //                    soapMsg.Append("</Depth>");
        //                }
        //                soapMsg.Append("</Pieces>");
        //            }

        //            if (shipment.SpecialServices.Count > 0)
        //            {
        //                foreach (string specialService in shipment.SpecialServices)
        //                {
        //                    soapMsg.Append("<SpecialService>");
        //                    soapMsg.Append(specialService);
        //                    soapMsg.Append("</SpecialService>");
        //                }
        //            }
        //            soapMsg.Append("</ShipmentDetails></req:BookPickupRequestAP>");

        //            return soapMsg.ToString();
        //        }

        //        #endregion
        //        #region SBA

        //        #endregion

        //        #region Util
        //        private void ConvertDimensionWeight(BookingPickUpInfo shipment, string countryCode)
        //        {
        //            bool convertWeight = false, convertDim = false, INToCM = false, LBToKG = false;
        //            if (BOCountries.IsLBINCountries(countryCode))
        //            {
        //                if (shipment.WeightUnit == WeightUnit.KG)
        //                {
        //                    convertWeight = true;
        //                    LBToKG = true;
        //                    shipment.WeightUnit = WeightUnit.LB;
        //                }

        //                if (shipment.DimensionUnit == DimensionUnit.CM)
        //                {
        //                    convertDim = true;
        //                    INToCM = true;
        //                    shipment.DimensionUnit = DimensionUnit.IN;
        //                }
        //            }
        //            else
        //            {
        //                if (shipment.WeightUnit == WeightUnit.LB)
        //                {
        //                    convertWeight = true;
        //                    shipment.WeightUnit = WeightUnit.KG;
        //                }
        //                if (shipment.DimensionUnit == DimensionUnit.IN)
        //                {
        //                    convertDim = true;
        //                    shipment.DimensionUnit = DimensionUnit.CM;
        //                }
        //            }

        //            if (convertWeight)
        //                shipment.TotalWeight = LBToKG == true ? 0.453592m : 2.20462m;

        //            if (shipment.Packages != null && shipment.Packages.Count > 0)
        //            {
        //                foreach (Package package in shipment.Packages)
        //                {
        //                    if (convertDim)
        //                    {
        //                        decimal dimParam = INToCM == true ? 2.54m : 0.393701m;
        //                        package.Height *= dimParam;
        //                        package.Width *= dimParam;
        //                        package.Length *= dimParam;
        //                    }

        //                    if (convertWeight)
        //                    {
        //                        decimal weightParam = LBToKG == true ? 0.453592m : 2.20462m;
        //                        package.Weight *= weightParam;
        //                    }
        //                }
        //            }
        //        }
        //        #endregion
        #endregion
    }
}
