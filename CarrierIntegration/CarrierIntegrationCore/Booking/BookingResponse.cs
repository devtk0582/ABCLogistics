using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CarrierIntegrationCore.Booking
{
    [Serializable]
    [XmlRoot("Response")]
    public class BookingResponse
    {
        public ErrorDetails ErrorMessage { get; set; }
        public string ConfirmationNumber { get; set; }

        public BookingResponse()
        {
            ErrorMessage = new ErrorDetails("", "");
        }
    }
}
