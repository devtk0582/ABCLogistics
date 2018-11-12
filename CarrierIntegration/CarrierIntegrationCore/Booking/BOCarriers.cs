using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarrierIntegrationCore.Booking
{
    public enum CarrierType
    {
        Fedex, DHL, SBA
    }

    public class BOCarriers
    {
         BookingDataClassesDataContext context;

         public BOCarriers()
        {
            context = new BookingDataClassesDataContext();
        }

         
    }
}
