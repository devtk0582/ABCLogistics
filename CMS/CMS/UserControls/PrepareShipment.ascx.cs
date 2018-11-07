using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.DAL;
using System.IO;
using iTextSharp.text.pdf;
using CMS.UPSShipWebReference;

namespace CMS.UserControls
{
    public partial class PrepareShipment : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void popup(string iOrderId)
        {
            pnlPrepare.Visible = true;
            hfOrderId.Value = iOrderId;
            BindShipment(iOrderId);
            MPEPrepare.Show();
        }

        private void BindShipment(string orderId)
        {
            try
            {
                DataSet orderShipment = (new OrderEntryDAL()).GetOrderShipmentInformation(int.Parse(orderId));
                if (orderShipment != null && orderShipment.Tables[0].Rows.Count > 0)
                {
                    lblFromName.Text = orderShipment.Tables[0].Rows[0]["FromName"].ToString();
                    lblFromStreet.Text = orderShipment.Tables[0].Rows[0]["FromStreet"].ToString();
                    lblFromCity.Text = orderShipment.Tables[0].Rows[0]["FromCity"].ToString();
                    lblFromState.Text = orderShipment.Tables[0].Rows[0]["FromState"].ToString();
                    hfFromStateUPSCode.Value = orderShipment.Tables[0].Rows[0]["FromUPSStateCode"].ToString();
                    hfFromCountryUPSCode.Value = orderShipment.Tables[0].Rows[0]["FromUPSCountryCode"].ToString();
                    lblFromCountry.Text = orderShipment.Tables[0].Rows[0]["FromCountry"].ToString();
                    lblFromZip.Text = orderShipment.Tables[0].Rows[0]["FromZip"].ToString();
                    lblFromPhone.Text = orderShipment.Tables[0].Rows[0]["FromPhone"].ToString();
                    lblToName.Text = orderShipment.Tables[0].Rows[0]["ToName"].ToString();
                    lblToStreet.Text = orderShipment.Tables[0].Rows[0]["ToStreet"].ToString();
                    lblToCity.Text = orderShipment.Tables[0].Rows[0]["ToCity"].ToString();
                    lblToState.Text = orderShipment.Tables[0].Rows[0]["ToState"].ToString();
                    lblToCountry.Text = orderShipment.Tables[0].Rows[0]["ToCountry"].ToString();
                    hfToStateUPSCode.Value = orderShipment.Tables[0].Rows[0]["ToUPSStateCode"].ToString();
                    hfToCountryUPSCode.Value = orderShipment.Tables[0].Rows[0]["ToUPSCountryCode"].ToString();
                    lblToZip.Text = orderShipment.Tables[0].Rows[0]["ToZip"].ToString();
                    lblToPhone.Text = orderShipment.Tables[0].Rows[0]["ToPhone"].ToString();
                    lblShipService.Text = orderShipment.Tables[0].Rows[0]["ServiceCode"].ToString();
                    hfLicense.Value = orderShipment.Tables[0].Rows[0]["License"].ToString();
                    hfUserName.Value = orderShipment.Tables[0].Rows[0]["UserName"].ToString();
                    hfPassword.Value = orderShipment.Tables[0].Rows[0]["Password"].ToString();
                    hfAccountNumber.Value = orderShipment.Tables[0].Rows[0]["AccountNumber"].ToString();

                    lblAddressValidation.Text = orderShipment.Tables[0].Rows[0]["AddressValidation"].ToString();
                    lblChargeType.Text = orderShipment.Tables[0].Rows[0]["ChargeType"].ToString();
                    lblMeasurementType.Text = orderShipment.Tables[0].Rows[0]["UnitType"].ToString();
                    lblPackagingType.Text = orderShipment.Tables[0].Rows[0]["PackagingType"].ToString();

                    lblWeight.Text = orderShipment.Tables[0].Rows[0]["Weight"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void lbPrintLabel_Click(object sender, EventArgs e)
        {

            try
            {
                string labelNumber = PrintLabel();
                
                ClearPanel();
                ctlEmptyCart.popup(hfOrderId.Value, labelNumber);
                MPEPrepare.Hide();
                
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        private string PrintLabel()
        {
            try
            {
                ShipService shpSvc = new ShipService();
                ShipmentRequest shipmentRequest = new ShipmentRequest();
                UPSSecurity upss = new UPSSecurity();
                UPSSecurityServiceAccessToken upssSvcAccessToken = new UPSSecurityServiceAccessToken();
                upssSvcAccessToken.AccessLicenseNumber = hfLicense.Value; 
                upss.ServiceAccessToken = upssSvcAccessToken;
                UPSSecurityUsernameToken upssUsrNameToken = new UPSSecurityUsernameToken();
                upssUsrNameToken.Username = hfUserName.Value; 
                upssUsrNameToken.Password = hfPassword.Value; 
                upss.UsernameToken = upssUsrNameToken;
                shpSvc.UPSSecurityValue = upss;
                RequestType request = new RequestType();
                String[] requestOption = { lblAddressValidation.Text.Trim() };
                request.RequestOption = requestOption;
                shipmentRequest.Request = request;
                ShipmentType shipment = new ShipmentType();
                shipment.Description = "CMS Label Printing";
                ShipperType shipper = new ShipperType();
                shipper.ShipperNumber = hfAccountNumber.Value;
                PaymentInfoType paymentInfo = new PaymentInfoType();
                ShipmentChargeType shpmentCharge = new ShipmentChargeType();
                BillShipperType billShipper = new BillShipperType();
                billShipper.AccountNumber = hfAccountNumber.Value;
                shpmentCharge.BillShipper = billShipper;
                shpmentCharge.Type = lblChargeType.Text.Trim(); 
                ShipmentChargeType[] shpmentChargeArray = { shpmentCharge };
                paymentInfo.ShipmentCharge = shpmentChargeArray;
                shipment.PaymentInformation = paymentInfo;
                UPSShipWebReference.ShipAddressType shipperAddress = new UPSShipWebReference.ShipAddressType();

                String[] addressLine = { lblFromStreet.Text.Trim() };
                shipperAddress.AddressLine = addressLine;
                shipperAddress.City = lblFromCity.Text.Trim();
                shipperAddress.PostalCode = lblFromZip.Text.Trim();
                shipperAddress.StateProvinceCode = hfFromStateUPSCode.Value;
                shipperAddress.CountryCode = hfFromCountryUPSCode.Value;
                shipperAddress.AddressLine = addressLine;
                shipper.Address = shipperAddress;
                shipper.Name = lblFromName.Text.Trim();
                shipper.AttentionName = lblFromName.Text.Trim();
                ShipPhoneType shipperPhone = new ShipPhoneType();
                shipperPhone.Number = lblFromPhone.Text.Trim();
                shipper.Phone = shipperPhone;
                shipment.Shipper = shipper;

                ShipFromType shipFrom = new ShipFromType();
                UPSShipWebReference.ShipAddressType shipFromAddress = new UPSShipWebReference.ShipAddressType();
                String[] shipFromAddressLine = { lblFromStreet.Text.Trim() };
                shipFromAddress.AddressLine = addressLine;
                shipFromAddress.City = lblFromCity.Text.Trim();
                shipFromAddress.PostalCode = lblFromZip.Text.Trim();
                shipFromAddress.StateProvinceCode = hfFromStateUPSCode.Value;
                shipFromAddress.CountryCode = hfFromCountryUPSCode.Value;
                shipFrom.Address = shipFromAddress;
                shipFrom.AttentionName = lblFromName.Text.Trim();
                shipFrom.Name = lblFromName.Text.Trim();
                shipment.ShipFrom = shipFrom;

                ShipToType shipTo = new ShipToType();
                ShipToAddressType shipToAddress = new ShipToAddressType();
                String[] addressLine1 = { lblToStreet.Text.Trim() };
                shipToAddress.AddressLine = addressLine1;
                shipToAddress.City = lblToCity.Text.Trim();
                shipToAddress.PostalCode = lblToZip.Text.Trim();
                shipToAddress.StateProvinceCode = hfToStateUPSCode.Value;
                shipToAddress.CountryCode = hfToCountryUPSCode.Value;
                shipTo.Address = shipToAddress;
                shipTo.AttentionName = lblToName.Text.Trim();
                shipTo.Name = lblToName.Text.Trim();
                ShipPhoneType shipToPhone = new ShipPhoneType();
                shipToPhone.Number = lblToPhone.Text.Trim();
                shipTo.Phone = shipToPhone;
                shipment.ShipTo = shipTo;

                ServiceType service = new ServiceType();
                service.Code = lblShipService.Text.Trim();
                shipment.Service = service;
                PackageType package = new PackageType();
                PackageWeightType packageWeight = new PackageWeightType();
                packageWeight.Weight = lblWeight.Text.Trim();
                ShipUnitOfMeasurementType uom = new ShipUnitOfMeasurementType();
                uom.Code = lblMeasurementType.Text.Trim();
                packageWeight.UnitOfMeasurement = uom;
                package.PackageWeight = packageWeight;
                PackagingType packType = new PackagingType();
                packType.Code = lblPackagingType.Text.Trim();
                package.Packaging = packType;
                PackageType[] pkgArray = { package };
                shipment.Package = pkgArray;
                LabelSpecificationType labelSpec = new LabelSpecificationType();
                LabelStockSizeType labelStockSize = new LabelStockSizeType();
                labelStockSize.Height = "6";
                labelStockSize.Width = "4";
                labelSpec.LabelStockSize = labelStockSize;
                LabelImageFormatType labelImageFormat = new LabelImageFormatType();
                labelImageFormat.Code = "GIF";
                labelSpec.LabelImageFormat = labelImageFormat;
                shipmentRequest.LabelSpecification = labelSpec;
                shipmentRequest.Shipment = shipment;
                System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

                ShipmentResponse shipmentResponse = shpSvc.ProcessShipment(shipmentRequest);

                iTextSharp.text.Document doc = new iTextSharp.text.Document();
                //Output to File
                string localPath = Server.MapPath("../Labels") + "\\" + shipmentResponse.ShipmentResults.ShipmentIdentificationNumber + ".pdf";
                PdfWriter.GetInstance(doc, new FileStream(localPath, FileMode.Create));
                doc.Open();
                Byte[] labelBuffer = System.Convert.FromBase64String(shipmentResponse.ShipmentResults.PackageResults[0].ShippingLabel.GraphicImage);
                MemoryStream stream = new MemoryStream(labelBuffer);

                iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(stream);
                gif.RotationDegrees = -90f;
                gif.ScalePercent(50f);
                stream.Close();
                doc.NewPage();
                doc.Add(gif);
                doc.Close();

                MemoryStream output = new MemoryStream();
                doc = new iTextSharp.text.Document();
                PdfWriter.GetInstance(doc, output);
                doc.Open();
                doc.NewPage();
                doc.Add(gif);
                doc.Close();


                (new OrderEntryDAL()).UploadShippingLabel_DAL("Shipping Label", output.ToArray(), "application/pdf", shipmentResponse.ShipmentResults.ShipmentIdentificationNumber + ".pdf", "", labelBuffer.Length, int.Parse(hfOrderId.Value), int.Parse(Session["UserID"].ToString()));

                output.Close();
                return shipmentResponse.ShipmentResults.ShipmentIdentificationNumber;
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                lblErr.Text = ex.Message;
                MPEPrepare.Show();
                return "";
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
                MPEPrepare.Show();
                return "";
            }
        }

        protected void lbOK_Click(object sender, EventArgs e)
        {
            try
            {
                //ClearPanel();
                //pnlPrepare.Visible = false;
                //MPEPrepare.Hide();
                    int y = (new AddCartsDAL()).DelChkoutParts_DAL(Session.SessionID);
                    Session["SubMenu"] = "Products";
                    Response.Redirect("../Catalog/Products.aspx");
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        private void ClearPanel()
        {
            lblFromName.Text = "";
            lblFromStreet.Text = "";
            lblFromCity.Text = "";
            lblFromState.Text = "";
            hfFromStateUPSCode.Value = "";
            hfFromCountryUPSCode.Value = "";
            lblFromCountry.Text = "";
            lblFromZip.Text = "";
            lblFromPhone.Text = "";
            lblToName.Text = "";
            lblToStreet.Text = "";
            lblToCity.Text = "";
            lblToState.Text = "";
            lblToCountry.Text = "";
            hfToStateUPSCode.Value = "";
            hfToCountryUPSCode.Value = "";
            lblToZip.Text = "";
            lblToPhone.Text = "";
            lblShipService.Text = "";
            lblAddressValidation.Text = "";
            lblChargeType.Text = "";
            lblMeasurementType.Text = "";
            lblPackagingType.Text = "";
            lblWeight.Text = "";

            lblErr.Text = "";
        }


    }
}