using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CarrierIntegrationCore.Booking
{
    [Serializable]
    [XmlRoot("Response")]
    public class RatingResponse
    {
        public ErrorDetails ErrorMessage { get; set; }
        public decimal FreightCharge { get; set; }
        public decimal DutiesAndTaxes { get; set; }
        public decimal TotalCharge { get; set; }

        public RatingResponse()
        {
            FreightCharge = 0;
            DutiesAndTaxes = 0;
            TotalCharge = 0;
            ErrorMessage = new ErrorDetails("0", "");
        }
    }
}
