using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CarrierIntegrationCore.Booking
{
    public class Validation
    {
        public static bool ValidateRateRequest(CustomerInfo customer, ShipmentInfo shipment, Shipper shipper, Receiver receiver, RatingResponse response)
        {
            if (customer == null)
            {
                response.ErrorMessage.ErrorCode = "003";
                response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_NO_CUSTOMER;
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(customer.CustomerNumber))
                {
                    response.ErrorMessage.ErrorCode = "004";
                    response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_CUSTOMER_NUMBER_EMPTY;
                    return false;
                }

                if (string.IsNullOrEmpty(customer.Password))
                {
                    response.ErrorMessage.ErrorCode = "005";
                    response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PASSWORD_EMPTY;
                    return false;
                }

                if (string.IsNullOrEmpty(receiver.Country))
                {
                    response.ErrorMessage.ErrorCode = "006";
                    response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_RECEIVER_COUNTRY_EMPTY;
                    return false;
                }
                else if ((receiver.Country == "US" || receiver.Country == "CA") && string.IsNullOrEmpty(receiver.State))
                {
                    response.ErrorMessage.ErrorCode = "007";
                    response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_RECEIVER_STATE_EMPTY;
                    return false;
                }

                if (string.IsNullOrEmpty(shipper.Country))
                {
                    response.ErrorMessage.ErrorCode = "008";
                    response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SHIPPER_COUNTRY_EMPTY;
                    return false;
                }
                else if ((shipper.Country == "US" || shipper.Country == "CA") && string.IsNullOrEmpty(shipper.State))
                {
                    response.ErrorMessage.ErrorCode = "009";
                    response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SHIPPER_STATE_EMPTY;
                    return false;
                }

                if (shipment.Packages == null || shipment.Packages.Count <= 0)
                {
                    response.ErrorMessage.ErrorCode = "010";
                    response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SHIPMENT_PACKAGE_UNDEFINED;
                    return false;
                }
                else
                {
                    foreach (PackageInfo package in shipment.Packages)
                    {
                        if (package.Weight == 0 || package.Width == 0 || package.Height == 0 || package.Length == 0)
                        {
                            response.ErrorMessage.ErrorCode = "011";
                            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SHIPMENT_INVALID_PACKAGE_DIMENSION;
                            return false;
                        }
                        if (shipper.Country != receiver.Country && string.IsNullOrEmpty(package.HSCode))
                        {
                            response.ErrorMessage.ErrorCode = "012";
                            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SHIPMENT_PRODUCTHSCODE_UNDEFINED;
                            return false;
                        }
                        if (shipper.Country != receiver.Country && package.ProductValue == 0)
                        {
                            response.ErrorMessage.ErrorCode = "013";
                            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SHIPMENT_PRODUCT_VALUE_UNDEFINED;
                            return false;
                        }
                    }
                }


                if (shipment.SpecialServices != null && shipment.SpecialServices.Count > 0)
                {
                    foreach (string specialService in shipment.SpecialServices)
                    {
                        if (!Enum.IsDefined(typeof(SpecialService), specialService))
                        {
                            response.ErrorMessage.ErrorCode = "014";
                            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SHIPMENT_INVALID_SPECIAL_SERVICE;
                            return false;
                        }
                    }
                }

                if (shipment.Accessorials != null && shipment.Accessorials.Count > 0)
                {
                    foreach (string accessorial in shipment.Accessorials)
                    {
                        if (!Enum.IsDefined(typeof(Accessorial), accessorial))
                        {
                            response.ErrorMessage.ErrorCode = "015";
                            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SHIPMENT_INVALID_ACCESSORIAL;
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public static bool ValidateBookRequest(CustomerInfo customer, BookingShipper shipper, BookingReceiver receiver, BookingPickUpInfo shipment, BookingResponse response)
        {
            //if (customer == null)
            //{
            //    response.ErrorMessage.ErrorCode = "003";
            //    response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_NO_CUSTOMER;
            //    return false;
            //}
            //else
            //{
            //    if (string.IsNullOrEmpty(customer.CustomerNumber))
            //    {
            //        response.ErrorMessage.ErrorCode = "004";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_CUSTOMER_NUMBER_EMPTY;
            //        return false;
            //    }

            //    if (string.IsNullOrEmpty(customer.Password))
            //    {
            //        response.ErrorMessage.ErrorCode = "005";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PASSWORD_EMPTY;
            //        return false;
            //    }

            //    if (string.IsNullOrEmpty(shipper.DestinationCountry))
            //    {
            //        response.ErrorMessage.ErrorCode = "006";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_RECEIVER_COUNTRY_EMPTY;
            //        return false;
            //    }

            //    if (string.IsNullOrEmpty(shipper.Country))
            //    {
            //        response.ErrorMessage.ErrorCode = "008";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SHIPPER_COUNTRY_EMPTY;
            //        return false;
            //    }
            //    else if ((shipper.Country.Trim() == "US" || shipper.Country.Trim() == "CA") && string.IsNullOrEmpty(shipper.State.Trim()))
            //    {
            //        response.ErrorMessage.ErrorCode = "009";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SHIPPER_STATE_EMPTY;
            //        return false;
            //    }

            //    if (shipper.Address == null || shipper.Address.Count < 1)
            //    {
            //        response.ErrorMessage.ErrorCode = "025";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_INVALID_ADDRESS;
            //        return false;
            //    }

            //    if (string.IsNullOrEmpty(shipper.City))
            //    {
            //        response.ErrorMessage.ErrorCode = "026";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PICKUP_CITY_EMPTY;
            //        return false;
            //    }

            //    if (string.IsNullOrEmpty(shipper.BookerName))
            //    {
            //        response.ErrorMessage.ErrorCode = "027";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_BOOKER_NAME_EMPTY;
            //        return false;
            //    }

            //    if (string.IsNullOrEmpty(shipper.BookerPhone))
            //    {
            //        response.ErrorMessage.ErrorCode = "028";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_BOOKER_PHONE_EMPTY;
            //        return false;
            //    }

            //    if (string.IsNullOrEmpty(shipper.ContactName))
            //    {
            //        response.ErrorMessage.ErrorCode = "022";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_CONTACT_NAME_EMPTY;
            //        return false;
            //    }

            //    if (string.IsNullOrEmpty(shipper.ContactPhone))
            //    {
            //        response.ErrorMessage.ErrorCode = "023";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_CONTACT_PHONE_EMPTY;
            //        return false;
            //    }

            //    if (shipment.PackageCount <= 0)
            //    {
            //        response.ErrorMessage.ErrorCode = "020";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_INVALID_PACKAGE_COUNT;
            //        return false;
            //    }

            //    if (shipment.TotalWeight <= 0)
            //    {
            //        response.ErrorMessage.ErrorCode = "021";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_INVALID_TOTAL_WEIGHT;
            //        return false;
            //    }

            //    DateTime dt;

            //    if (!DateTime.TryParseExact(shipment.PickUpDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out dt))
            //    {
            //        response.ErrorMessage.ErrorCode = "024";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PICKUP_DATE_INVALID;
            //        return false;
            //    }

            //    if (!DateTime.TryParseExact(shipment.ReadyTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out dt))
            //    {
            //        response.ErrorMessage.ErrorCode = "029";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_INVALID_READY_TIME;
            //        return false;
            //    }

            //    if (!DateTime.TryParseExact(shipment.CloseTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out dt))
            //    {
            //        response.ErrorMessage.ErrorCode = "030";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_INVALID_CLOSE_TIME;
            //        return false;
            //    }

            //    if (shipment.LocationType != LocationType.Residence && string.IsNullOrEmpty(shipper.CompanyName))
            //    {
            //        response.ErrorMessage.ErrorCode = "033";
            //        response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_COMPANY_NAME_EMPTY;
            //        return false;
            //    }

            //    if (shipper.Country == "US" && shipper.DestinationCountry == "US")
            //    {
            //        if (shipment.DomSettings == null)
            //        {
            //            response.ErrorMessage.ErrorCode = "034";
            //            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_DOMESTIC_SETTINGS_EMPTY;
            //            return false;
            //        }

            //        if (shipment.DomSettings.ReceiverAddress == null || shipment.DomSettings.ReceiverAddress.Count < 1)
            //        {
            //            response.ErrorMessage.ErrorCode = "035";
            //            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_DOMESTIC_RECEIVER_ADDRESS;
            //            return false;
            //        }
            //        if (string.IsNullOrEmpty(shipment.DomSettings.ReceiverCity))
            //        {
            //            response.ErrorMessage.ErrorCode = "036";
            //            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_DOMESTIC_RECEIVER_CITY;
            //            return false;
            //        }

            //        if (string.IsNullOrEmpty(shipment.DomSettings.ReceiverState))
            //        {
            //            response.ErrorMessage.ErrorCode = "037";
            //            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_DOMESTIC_RECEIVER_STATE;
            //            return false;
            //        }

            //        if (string.IsNullOrEmpty(shipment.DomSettings.ReceiverZipcode))
            //        {
            //            response.ErrorMessage.ErrorCode = "038";
            //            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_DOMESTIC_RECEIVER_ZIP;
            //            return false;
            //        }

            //        if (string.IsNullOrEmpty(shipment.DomSettings.ReceiverName))
            //        {
            //            response.ErrorMessage.ErrorCode = "039";
            //            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_DOMESTIC_RECEIVER_NAME;
            //            return false;
            //        }
            //        if (string.IsNullOrEmpty(shipment.DomSettings.ReceiverCompanyName))
            //        {
            //            response.ErrorMessage.ErrorCode = "040";
            //            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_DOMESTIC_RECEIVER_COMPANY_NAME;
            //            return false;
            //        }
            //        if (string.IsNullOrEmpty(shipment.DomSettings.ReceiverPhone))
            //        {
            //            response.ErrorMessage.ErrorCode = "041";
            //            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_DOMESTIC_RECEIVER_PHONE;
            //            return false;
            //        }

            //        if (string.IsNullOrEmpty(shipment.DomSettings.DeliveryDate))
            //        {
            //            response.ErrorMessage.ErrorCode = "042";
            //            response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_DOMESTIC_DELIVERY_DATE;
            //            return false;
            //        }

            //        if (shipment.Packages != null && shipment.Packages.Count > 0)
            //        {
            //            foreach (Package package in shipment.Packages)
            //            {
            //                if (package.Weight == 0 || package.Height == 0 || package.Width == 0 || package.Length == 0)
            //                {
            //                    response.ErrorMessage.ErrorCode = "019";
            //                    response.ErrorMessage.ErrorMessage = CommonMessages.ERROR_SHIPMENT_INVALID_PACKAGE_DIMENSION;
            //                    return false;
            //                }
            //            }
            //        }
            //    }
            //}

            return true;
        }

        public static bool IsEmail(string inputEmail)
        {
            string strRedex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRedex);
            if (re.IsMatch(inputEmail))
                return true;
            else
                return false;
        }
    }
}
