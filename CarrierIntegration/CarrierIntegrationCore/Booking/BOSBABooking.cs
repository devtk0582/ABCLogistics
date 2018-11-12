using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using log4net;
using System.Reflection;

namespace CarrierIntegrationCore.Booking
{
    public class BOSBABooking
    {
        private BookingDataClassesDataContext context;

        public BOSBABooking()
        {
            context = new BookingDataClassesDataContext();
        }

        public string CreateOrder(BookingShipper shipper, BookingReceiver receiver, BookingPickUpInfo shipment, int bookUserId, BookingResponse bookingResponse)
        {
            string hawb = "";
            //try
            //{
            //    User bookUser = BOUser.GetUserById(bookUserId);
            //    Order order = new Order();
            //    order.DomOrInt = true;
            //    order.UserId = bookUser;
            //    //Shipper
            //    order.Shipper = new BusinessLogic.Shipper();
            //    order.Shipper.User = bookUser;
            //    order.Shipper.Address = new Address();
            //    order.Shipper.Address.City = shipper.City;
            //    order.Shipper.Address.States = BOStates.GetStateByShortName(shipper.State);
            //    order.Shipper.Address.CountryName = "USA";
            //    order.Shipper.Address.Street = shipper.Address[0];
            //    if (shipper.Address.Count > 1)
            //        order.Shipper.Address.Street2 = shipper.Address[1];
            //    order.Shipper.Address.Zip = shipper.PostalCode;
            //    order.Shipper.Address.Phone = shipper.BookerPhone;
            //    order.Shipper.Name = shipper.BookerName;
            //    order.Shipper.Company = shipper.CompanyName;

            //    //Consignee
            //    order.Consignee = new BusinessLogic.Consignee();
            //    order.Consignee.User = bookUser;
            //    order.Consignee.Address = new Address();
            //    order.Consignee.Address.City = shipment.DomSettings.ReceiverCity;
            //    order.Consignee.Address.States = BOStates.GetStateByShortName(shipment.DomSettings.ReceiverState);
            //    order.Consignee.Address.CountryName = "USA";
            //    order.Consignee.Address.Street = shipment.DomSettings.ReceiverAddress[0];
            //    if (shipper.Address.Count > 1)
            //        order.Consignee.Address.Street2 = shipment.DomSettings.ReceiverAddress[1];
            //    order.Consignee.Address.Zip = shipment.DomSettings.ReceiverZipcode;
            //    order.Consignee.Address.Phone = shipment.DomSettings.ReceiverPhone;
            //    order.Consignee.Name = shipment.DomSettings.ReceiverName;
            //    order.Consignee.Company = shipment.DomSettings.ReceiverCompanyName;

            //    //Bill To
            //    order.ThirdParty = new ThirdParty();
            //    order.ThirdParty.User = bookUser;
            //    order.ThirdParty.CustomerNo = bookUser.BillToNumber;
            //    order.ThirdParty.RefNo = bookUser.BillToNumber;
            //    order.ThirdParty.Company = bookUser.BillToName;
            //    order.ThirdParty.Contact = bookUser.BillToContact;
            //    if (bookUser.BillToAddress != null)
            //    {
            //        order.ThirdParty.Address = bookUser.BillToAddress;
            //    }

            //    // Fill Order Info:
            //    order.ShipDate = DateTime.ParseExact(shipment.PickUpDate, "yyyy-MM-dd", null);
            //    order.ReqDelDate = DateTime.ParseExact(shipment.DomSettings.DeliveryDate, "yyyy-MM-dd", null);
            //    order.Service = BOService.GetServiceByID((int)shipment.DomSettings.Service);
            //    order.ReadyTime = shipment.ReadyTime;
            //    order.CloseTime = shipment.CloseTime;
            //    order.InternalReferenceNumber = shipment.DomSettings.ReferenceNumber;
            //    if (shipment.Insurance > 0)
            //    {
            //        order.Insurance = shipment.Insurance;
            //    }

            //    order.BillTo = BOBillTo.GetBillTo("3rd Party");
            //    order.SpecialInstructions = shipment.SpecialInstruction ?? "";
            //    order.IsLTLShipment = shipment.DomSettings.Service == ServiceLevel.LTL;
            //    order.ShipperCodType = BOShipper.LoadShipperCOD(1);
            //    //order.EmailStatusNotification = ShipmentEmailStatusNotification.Text.Trim();
            //    List<Freight> dimensions = new List<Freight>();
            //    Freight freight = new Freight();
            //    freight.Qty = shipment.PackageCount;
            //    freight.Weight = shipment.TotalWeight;
            //    freight.Description = "";
            //    dimensions.Add(freight);

            //    order.Freight = dimensions;

            //    order.SpecialServices = new SpecialServices();
            //    order.SpecialServices.AfterHourspud = false;
            //    order.SpecialServices.Amdelivery = false;
            //    order.SpecialServices.Hazardous = false;
            //    order.SpecialServices.Holidaypud = false;
            //    order.SpecialServices.InsideDelivery = false;
            //    order.SpecialServices.Liftgatepud = false;
            //    order.SpecialServices.MallConventionpud = false;
            //    order.SpecialServices.Residentialpud = false;
            //    order.SpecialServices.Saturdaypud = false;
            //    order.SpecialServices.Sundaypud = false;
            //    order.SpecialServices.WhiteGloveDelivery = false;

            //    var oDao = new OrderDao();
            //    order = oDao.CreateOrder(order);
            //    Utils.PutOrderInQueue(order, 1);
            //    hawb = order.HblNumber;
            //    BOOrder.SendOrderCreationNotifications(order.OrderId, new List<string>());
            //}
            //catch (Exception ex)
            //{
            //    bookingResponse.ErrorMessage.ErrorCode = "511";
            //    bookingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
            //    ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            //    m_log.Error(ex.ToString());
            //}
            return hawb;
        }
    }
}
