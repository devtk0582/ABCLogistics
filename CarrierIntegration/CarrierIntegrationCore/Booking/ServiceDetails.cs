using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarrierIntegrationCore.Booking
{
    public class ServiceDetails
    {

        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceCode { get; set; }
        public int CarrierID { get; set; }
        public string CarrierName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string License { get; set; }

        public ServiceDetails()
        {

        }

        public ServiceDetails(int serviceId, string serviceName, string serviceCode, int carrierId,
            string carrierName, string accountNumber, string accountName, string password, string license)
        {
            ServiceID = serviceId;
            CarrierID = carrierId;
            ServiceName = serviceName;
            ServiceCode = serviceCode;
            CarrierName = carrierName;
            AccountNumber = accountNumber;
            AccountName = accountName;
            Password = password;
            License = license;
        }
    }
}
