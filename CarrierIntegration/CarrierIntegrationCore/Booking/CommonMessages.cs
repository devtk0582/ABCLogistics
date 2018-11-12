using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarrierIntegrationCore.Booking
{
    public class CommonMessages
    {
        //Error
        public const string ERROR_UNKNOWN_ERROR = "Error processing the request";

        //Login
        public const string ERROR_NO_CUSTOMER = "No customer is specified";
        public const string ERROR_CUSTOMER_NUMBER_EMPTY = "Customer number is required";
        public const string ERROR_PASSWORD_EMPTY = "Password is required";
        public const string ERROR_LOG_IN_FAILED = "Authentication failed. Customer number or password is invalid";

        //Shipper
        public const string ERROR_SHIPPER_COUNTRY_INVALID = "Origin country is invalid";
        public const string ERROR_SHIPPER_COUNTRY_EMPTY = "Origin country is required";
        public const string ERROR_SHIPPER_COUNTRY_CODE = "Origin country code is invalid";
        public const string ERROR_SHIPPER_STATE_EMPTY = "Origin state is required for USA and Canada";
        public const string ERROR_SHIPPER_STATE_CODE = "Origin state code is invalid";
        public const string ERROR_SHIPPER_POSTALCODE_EMPTY = "Postal code is required for selected origin country";
        public const string ERROR_SHIPPER_ADDRESS_EMPTY = "Origin address is required";

        //Receiver
        public const string ERROR_RECEIVER_COUNTRY_INVALID = "Destination country is invalid";
        public const string ERROR_RECEIVER_COUNTRY_EMPTY = "Destination country is required";
        public const string ERROR_RECEIVER_COUNTRY_CODE = "Destination country code is invalid";
        public const string ERROR_RECEIVER_STATE_EMPTY = "Destination state is required for USA and Canada";
        public const string ERROR_RECEIVER_STATE_CODE = "Destination state code is invalid";
        public const string ERROR_RECEIVER_POSTALCODE_EMPTY = "Postal code is required for selected destination country";
        public const string ERROR_RECEIVER_ADDRESS_EMPTY = "Destination address is required";

        //Shipment
        public const string ERROR_SHIPMENT_DIMENSION_UNIT_UNDEFINED = "Package dimension unit is required";
        public const string ERROR_SHIPMENT_WEIGHT_UNIT_UNDEFINED = "Package weight unit is required";
        public const string ERROR_SHIPMENT_PACKAGE_UNDEFINED = "Package details information is required";
        public const string ERROR_SHIPMENT_INVALID_PACKAGE_DIMENSION = "Package weight / height / width / length are required";
        public const string ERROR_SHIPMENT_CURRENCY_CODE = "Currency code is invalid";
        public const string ERROR_SHIPMENT_PRODUCTHSCODE_UNDEFINED = "Product HS Code is required for international shipments";
        public const string ERROR_SHIPMENT_PRODUCTMANUFACTURERCOUNTRY_UNDEFINED = "Product manufacturer country is required";
        public const string ERROR_SHIPMENT_PRODUCT_VALUE_UNDEFINED = "Product value is required for international shipments";
        public const string ERROR_SHIPMENT_PRODUCT_QTYUNIT_UNDEFINED = "Product quantity unit is required";
        public const string ERROR_SHIPMENT_INVALID_SPECIAL_SERVICE = "Invalid special service";
        public const string ERROR_SHIPMENT_INVALID_ACCESSORIAL = "Invalid accessorial";
        public const string ERROR_SHIPMENT_OVERLIMIT = "Weight or dimensions exceed limit";

        //General
        public const string ERROR_SERVICE_UNAVAILABLE = "Service unavailable for the selected origin and destination.";
        public const string ERROR_PROCESSING_REQUEST = "Unable to process the request";
        //SBA Rate Calcualtor
        public const string ERROR_NO_ORIGIN_ACI = "There is no rate for specified origin zipcode in US";
        public const string ERROR_NO_DEST_ACI = "There is no rate for specified destination zipcode in US";
        public const string ERROR_NO_ZONE_MAT = "There is no rate from origin region to destination region for the customer";

        #region Booking Shipment
        public const string ERROR_INVALID_PACKAGE_COUNT = "Package count must be greater than 0";
        public const string ERROR_INVALID_TOTAL_WEIGHT = "Total weight must be greater than 0";
        public const string ERROR_BOOKER_NAME_EMPTY = "Booker name is required";
        public const string ERROR_BOOKER_PHONE_EMPTY = "Booker phone is required";
        public const string ERROR_CONTACT_NAME_EMPTY = "Contact name is required";
        public const string ERROR_CONTACT_PHONE_EMPTY = "Contact phone is required";
        public const string ERROR_PICKUP_DATE_EMPTY = "Pickup date is required";
        public const string ERROR_READY_TIME_EMPTY = "Ready time is required";
        public const string ERROR_CLOSE_TIME_EMPTY = "Close time is required";
        public const string ERROR_PICKUP_DATE_INVALID = "Pickup date should not be earlier than today";
        public const string ERROR_INVALID_READY_TIME = "Invalid ready time format";
        public const string ERROR_INVALID_CLOSE_TIME = "Invalid close time format";
        public const string ERROR_COMPANY_NAME_EMPTY = "Company name is required for business location";
        public const string ERROR_INVALID_ADDRESS = "At least one addres is needed";
        public const string ERROR_PICKUP_CITY_EMPTY = "City is required for pickup location";
        public const string ERROR_NO_PICKUP_AVAILABLE = "No pickup schedule available";
        public const string ERROR_CLOSE_TIME_TOO_EARLY = "Company closing time is too early for the pickup schedule";
        public const string ERROR_DOMESTIC_SETTINGS_EMPTY = "Domestic settings are required for domestic shipments.";
        public const string ERROR_DOMESTIC_RECEIVER_ADDRESS = "Receiver address is required for domestic shipments";
        public const string ERROR_DOMESTIC_RECEIVER_CITY = "Receiver city is required for domestic shipments";
        public const string ERROR_DOMESTIC_RECEIVER_STATE = "Receiver state is required for domestic shipments";
        public const string ERROR_DOMESTIC_RECEIVER_ZIP = "Receiver zip is required for domestic shipments";
        public const string ERROR_DOMESTIC_RECEIVER_NAME = "Receiver name is required for domestic shipments";
        public const string ERROR_DOMESTIC_RECEIVER_COMPANY_NAME = "Receiver's company name is required for domestic shipments";
        public const string ERROR_DOMESTIC_RECEIVER_PHONE = "Receiver phone is required for domestic shipments";
        public const string ERROR_DOMESTIC_DELIVERY_DATE = "Delivery date is required for domestic shipments";
        public const string ERROR_DOMESTIC_SERVICE = "Service level is required for domestic shipments";
        #endregion
        
    }
}
