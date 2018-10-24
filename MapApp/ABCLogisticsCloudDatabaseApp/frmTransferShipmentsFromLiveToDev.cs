using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ABCLogisticsCloudDatabaseApp
{
    public partial class frmTransferShipmentsFromLiveToDev : Form
    {
        public frmTransferShipmentsFromLiveToDev()
        {
            InitializeComponent();
        }

        private void frmTransferShipmentsFromLiveToDev_Load(object sender, EventArgs e)
        {
            ABCLogisticsDevFFMShipmentWebReference.ABCLogistics_GlobalShipment service = new ABCLogisticsDevFFMShipmentWebReference.ABCLogistics_GlobalShipment();
            
            using (ABCLogisticsGlobalDataClassesDataContext context = new ABCLogisticsGlobalDataClassesDataContext())
            {
                List<ABCLogisticsDevFFMShipmentWebReference.Item> items = new List<ABCLogisticsDevFFMShipmentWebReference.Item>();
                var shipments = (from o in context.Shipments
                                 where o.ShipperID == null
                                 select o).ToList();

                foreach (Shipment shipment in shipments)
                {
                    try
                    {
                        ABCLogisticsDevFFMShipmentWebReference.ShipmentRequest request = new ABCLogisticsDevFFMShipmentWebReference.ShipmentRequest();
                        request.MAWBInfo = new ABCLogisticsDevFFMShipmentWebReference.MAWBInfo();
                        request.HAWBInfo = new ABCLogisticsDevFFMShipmentWebReference.HAWBInfo();
                        request.Shipper = new ABCLogisticsDevFFMShipmentWebReference.Shipper();
                        request.Receiver = new ABCLogisticsDevFFMShipmentWebReference.Receiver();
                        request.PickupAddress = new ABCLogisticsDevFFMShipmentWebReference.PickupAddress();
                        request.DeliveryAddress = new ABCLogisticsDevFFMShipmentWebReference.DeliveryAddress();
                        request.Shipment = new ABCLogisticsDevFFMShipmentWebReference.Shipment();
                        request.SpecialService = new ABCLogisticsDevFFMShipmentWebReference.SpecialService();
                        request.AirGnd = new ABCLogisticsDevFFMShipmentWebReference.AirGnd();
                        request.Transportation = new ABCLogisticsDevFFMShipmentWebReference.Transportation();

                        var maseterPro = shipment.ShipmentMasterPros.FirstOrDefault();

                        if (maseterPro != null)
                        {
                            var mPro = context.MasterPros.Where(o => o.MAWB == maseterPro.MAWB).FirstOrDefault();
                            request.MAWBInfo.MAWB = mPro.MAWB;
                            request.MAWBInfo.Carrier = mPro.Carrier;
                            request.MAWBInfo.FLTDate = mPro.FLTDate.Value.ToString("MM/dd/yyyy");
                            request.MAWBInfo.ETA = mPro.ETA.Value.ToString("MM/dd/yyyy");
                        }

                        request.HAWBInfo.HAWB = shipment.ABCLogisticsTracking;
                        request.HAWBInfo.HAWBID = shipment.HAWB;

                        var shipmentAddresses = context.ShipmentAddresses.Where(o => o.ShipmentID == shipment.ShipmentID).FirstOrDefault();

                        var shipperAddress = context.Addresses.Where(o => o.AddressID == shipmentAddresses.ShipperAddress.Value).FirstOrDefault();
                        request.Shipper.Address1 = shipperAddress.Street;
                        request.Shipper.Address2 = shipperAddress.Street2;
                        request.Shipper.Address3 = shipperAddress.Street3;
                        request.Shipper.Num = shipperAddress.AccountNumber;
                        request.Shipper.Name = shipperAddress.Name;
                        request.Shipper.City = shipperAddress.City;
                        request.Shipper.State = shipperAddress.State;
                        request.Shipper.Zip = shipperAddress.Zip;
                        request.Shipper.Country = shipperAddress.Country;
                        request.Shipper.Phone = shipperAddress.Phone;

                        var receiverAddress = context.Addresses.Where(o => o.AddressID == shipmentAddresses.ConsigneeAddress.Value).FirstOrDefault();
                        request.Receiver.Address1 = receiverAddress.Street;
                        request.Receiver.Address2 = receiverAddress.Street2;
                        request.Receiver.Address3 = receiverAddress.Street3;
                        request.Receiver.Num = receiverAddress.AccountNumber;
                        request.Receiver.Name = receiverAddress.Name;
                        request.Receiver.City = receiverAddress.City;
                        request.Receiver.State = receiverAddress.State;
                        request.Receiver.Zip = receiverAddress.Zip;
                        request.Receiver.Country = receiverAddress.Country;
                        request.Receiver.Phone = receiverAddress.Phone;

                        if (shipmentAddresses.ThirdPartyAddress.HasValue)
                        {
                            request.ThirdParty = new ABCLogisticsDevFFMShipmentWebReference.ThirdParty();
                            var thirdPartyAddress = context.Addresses.Where(o => o.AddressID == shipmentAddresses.ThirdPartyAddress.Value).FirstOrDefault();
                            request.ThirdParty.Address1 = thirdPartyAddress.Street;
                            request.ThirdParty.Address2 = thirdPartyAddress.Street2;
                            request.ThirdParty.Address3 = thirdPartyAddress.Street3;
                            request.ThirdParty.Num = thirdPartyAddress.AccountNumber;
                            request.ThirdParty.Name = thirdPartyAddress.Name;
                            request.ThirdParty.City = thirdPartyAddress.City;
                            request.ThirdParty.State = thirdPartyAddress.State;
                            request.ThirdParty.Zip = thirdPartyAddress.Zip;
                            request.ThirdParty.Country = thirdPartyAddress.Country;
                            request.ThirdParty.Phone = thirdPartyAddress.Phone;
                        }

                        var pualert = context.PUAlerts.Where(o => o.ShipmentID == shipment.ShipmentID).FirstOrDefault();

                        var pickupAddress = context.Addresses.Where(o => o.AddressID == pualert.PickupFrom.Value).FirstOrDefault();
                        request.PickupAddress.Address1 = pickupAddress.Street;
                        request.PickupAddress.Address2 = pickupAddress.Street2;
                        request.PickupAddress.Address3 = pickupAddress.Street3;
                        request.PickupAddress.Name = pickupAddress.Name;
                        request.PickupAddress.City = pickupAddress.City;
                        request.PickupAddress.State = pickupAddress.State;
                        request.PickupAddress.Zip = pickupAddress.Zip;
                        request.PickupAddress.Country = pickupAddress.Country;
                        request.PickupAddress.PrimaryContactName = pickupAddress.PrimaryContactName;
                        request.PickupAddress.PrimaryContactPhone = pickupAddress.PrimaryContactPhone;

                        var deliveryAddress = context.Addresses.Where(o => o.AddressID == pualert.DeliveryTo.Value).FirstOrDefault();
                        request.DeliveryAddress.Address1 = deliveryAddress.Street;
                        request.DeliveryAddress.Address2 = deliveryAddress.Street2;
                        request.DeliveryAddress.Address3 = deliveryAddress.Street3;
                        request.DeliveryAddress.Name = deliveryAddress.Name;
                        request.DeliveryAddress.City = deliveryAddress.City;
                        request.DeliveryAddress.State = deliveryAddress.State;
                        request.DeliveryAddress.Zip = deliveryAddress.Zip;
                        request.DeliveryAddress.Country = deliveryAddress.Country;
                        request.DeliveryAddress.PrimaryContactName = deliveryAddress.PrimaryContactName;
                        request.DeliveryAddress.PrimaryContactPhone = deliveryAddress.PrimaryContactPhone;

                        request.Transportation.PickupDate = pualert.ScheduleDate.HasValue ? pualert.ScheduleDate.Value.ToString("MM/dd/yyyy") : null;
                        request.Transportation.ReadyTime = pualert.FreightReady;
                        request.Transportation.MBDDate = pualert.MBDDate.HasValue ? pualert.MBDDate.Value.ToString("MM/dd/yyyy") : null;
                        request.Transportation.CloseTime = pualert.CloseTime;

                        if (shipment.BillTo == "S")
                            request.Shipment.BillTo = ABCLogisticsDevFFMShipmentWebReference.BillTo.S;
                        else if (shipment.BillTo == "R")
                            request.Shipment.BillTo = ABCLogisticsDevFFMShipmentWebReference.BillTo.R;
                        else if (shipment.BillTo == "T")
                            request.Shipment.BillTo = ABCLogisticsDevFFMShipmentWebReference.BillTo.T;

                        request.Shipment.SplitCode = shipment.SplitCode;
                        request.Shipment.SplitLocation = shipment.SplitLocation;
                        request.Shipment.DeclaredValue = Convert.ToDecimal(shipment.DeclaredValue);
                        request.Shipment.Insurance = Convert.ToDecimal(shipment.Insurance);
                        request.Shipment.CustomsValue = Convert.ToDecimal(shipment.CustomsValue);

                        switch (shipment.ServiceLevel.Value)
                        {
                            case 3:
                                request.Shipment.ServiceLevel = ABCLogisticsDevFFMShipmentWebReference.ServiceLevel.N;
                                break;
                            case 4:
                                request.Shipment.ServiceLevel = ABCLogisticsDevFFMShipmentWebReference.ServiceLevel.E;
                                break;
                            case 6:
                                request.Shipment.ServiceLevel = ABCLogisticsDevFFMShipmentWebReference.ServiceLevel.V;
                                break;
                            case 12:
                                request.Shipment.ServiceLevel = ABCLogisticsDevFFMShipmentWebReference.ServiceLevel.P;
                                break;
                            case 13:
                                request.Shipment.ServiceLevel = ABCLogisticsDevFFMShipmentWebReference.ServiceLevel.S;
                                break;
                            case 15:
                                request.Shipment.ServiceLevel = ABCLogisticsDevFFMShipmentWebReference.ServiceLevel.T;
                                break;
                            case 18:
                                request.Shipment.ServiceLevel = ABCLogisticsDevFFMShipmentWebReference.ServiceLevel.X;
                                break;
                            case 19:
                                request.Shipment.ServiceLevel = ABCLogisticsDevFFMShipmentWebReference.ServiceLevel.H;
                                break;
                        }

                        request.Shipment.ChargeWeight = Convert.ToInt32(shipment.ChargeWeight);
                        request.Shipment.DimFactor = Convert.ToInt32(shipment.DimFactor);
                        request.Shipment.IsGovt = shipment.IsGovt.HasValue && shipment.IsGovt.Value ? true : false;
                        request.Shipment.IsQuote = shipment.IsQuote.HasValue && shipment.IsQuote.Value ? true : false;
                        request.Shipment.SpecialInstructions = shipment.SpecialInstructions;
                        request.Shipment.PUArea = shipment.PUArea;
                        request.Shipment.DELArea = shipment.DELArea;
                        request.Shipment.AES = shipment.AES;
                        request.Shipment.FTRExemptions = shipment.FTRExemptions;
                        request.Shipment.NextStatusDate = shipment.NextStatusDate.HasValue ? shipment.NextStatusDate.Value.ToString("MM/dd/yyyy") : null;
                        request.Shipment.NoFuelSurcharge = shipment.NoFuelSurcharge.HasValue && shipment.NoFuelSurcharge.Value ? true : false;
                        request.Shipment.Terminal = shipment.Terminal;

                        var specialService = context.ShipmentSpecialServices.Where(o => o.ShipmentID == shipment.ShipmentID).FirstOrDefault();

                        request.SpecialService.SpecialServiceID = specialService.SpecialServiceID.Value == 1 ? ABCLogisticsDevFFMShipmentWebReference.SpecialServiceID.FirstMO : ABCLogisticsDevFFMShipmentWebReference.SpecialServiceID.FinalMO;

                        if (specialService.ServiceCode == "S")
                            request.SpecialService.SpecialServiceCode = ABCLogisticsDevFFMShipmentWebReference.SpecialServiceCode.S;
                        else if (specialService.ServiceCode == "SP")
                            request.SpecialService.SpecialServiceCode = ABCLogisticsDevFFMShipmentWebReference.SpecialServiceCode.SP;
                        else
                            request.SpecialService.SpecialServiceCode = null;

                        var airGnd = context.AirGNDs.Where(o => o.ShipmentID == shipment.ShipmentID).FirstOrDefault();

                        request.AirGnd.AirOrGnd = airGnd.AirOrGnd.HasValue && airGnd.AirOrGnd.Value ? ABCLogisticsDevFFMShipmentWebReference.AirORGnd.AIR : ABCLogisticsDevFFMShipmentWebReference.AirORGnd.GND;
                        request.AirGnd.Origin = airGnd.Origin;
                        request.AirGnd.Destination = airGnd.Destination;

                        var dims = context.Dims.Where(o => o.ShipmentID == shipment.ShipmentID).ToList();

                        foreach (Dim dim in dims)
                        {
                            ABCLogisticsDevFFMShipmentWebReference.Item item = new ABCLogisticsDevFFMShipmentWebReference.Item()
                            {
                                Pieces = Convert.ToInt32(dim.Pieces),
                                Height = dim.Height,
                                Length = dim.Length,
                                Width = dim.Width,
                                Description = dim.Description
                            };

                            if (dim.UOW == "LB")
                            {
                                item.UOW = ABCLogisticsDevFFMShipmentWebReference.UOW.LB;
                                item.Weight = dim.Pounds;
                            }
                            else
                            {
                                item.UOW = ABCLogisticsDevFFMShipmentWebReference.UOW.KG;
                                item.Weight = dim.Kilos;
                            }

                            item.UOM = dim.UOW == "IN" ? ABCLogisticsDevFFMShipmentWebReference.UOM.IN : ABCLogisticsDevFFMShipmentWebReference.UOM.FT;
                            items.Add(item);
                        }

                        request.Items = items.ToArray();
                        service.InsertUpdateShipmentData(request);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        
    }
}
