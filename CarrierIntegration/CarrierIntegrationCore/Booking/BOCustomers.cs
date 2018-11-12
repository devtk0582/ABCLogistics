using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarrierIntegrationCore.Booking
{
    public class BOCustomers
    {
        BookingDataClassesDataContext context;

        public BOCustomers()
        {
            context = new BookingDataClassesDataContext();
        }

        public BookingCustomer AuthenticationCustomer(string customerNumber, string password)
        {
            var customer = (from c in context.BookingCustomers
                            where c.CustomerNumber == customerNumber
                            && c.Password == password
                            select c).SingleOrDefault();
            return customer;
        }
    }
}
