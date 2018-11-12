using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarrierIntegrationCore.Booking
{
    public class ErrorDetails
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public ErrorDetails()
        {

        }

        public ErrorDetails(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
