using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using CarrierDevApp.Entity;

namespace CarrierDevApp
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            InitControls();
        }

        #region Init

        private void InitControls()
        {
            InitDHLRateControls();
            InitDHLPickupControls();
            InitDHLCancelPickupControls();
            InitFedExRateControls();
        }

        private void InitDHLRateControls()
        {
            dtpDRShipDate.Value = DateTime.Now;
        }

        private void InitDHLPickupControls()
        {
            dtpDRShipDate.Value = DateTime.Now;
            dtpDPReadyTime.Value = new DateTime(2014, 1, 1, 9, 0, 0);
            dtpDPCloseTime.Value = new DateTime(2014, 1, 1, 18, 0, 0);
        }

        private void InitDHLCancelPickupControls()
        {
            dtpDCPPickupDate.Value = DateTime.Now;
            cbDCPReason.SelectedIndex = 0;
        }

        private void InitFedExRateControls()
        {
            dtpFRShipDate.Value = DateTime.Now;
        }
        #endregion

        #region DHL
        private void DHLRate()
        {
            try
            {
                double weight = 0, height = 0, length = 0, width = 0, insurance = 0, declaredValue = 0;

                if (!double.TryParse(txtDRWeight.Text.Trim(), out weight))
                {
                    MessageBox.Show("Invalid weight");
                    return;
                }

                if (!double.TryParse(txtDRHeight.Text.Trim(), out height))
                {
                    MessageBox.Show("Invalid Height");
                    return;
                }

                if (!double.TryParse(txtDRLength.Text.Trim(), out length))
                {
                    MessageBox.Show("Invalid Length");
                    return;
                }

                if (!double.TryParse(txtDRWidth.Text.Trim(), out width))
                {
                    MessageBox.Show("Invalid Width");
                    return;
                }

                if (!double.TryParse(txtDRInsurance.Text.Trim(), out insurance))
                {
                    MessageBox.Show("Invalid Insurance");
                    return;
                }

                if (!double.TryParse(txtDRDeclaredValue.Text.Trim(), out declaredValue))
                {
                    MessageBox.Show("Invalid Declared Value");
                    return;
                }

                bool isEUtoEU = Util.IsEUCountries(txtDRShipperCountry.Text.Trim()) && Util.IsEUCountries(txtDRReceiverCountry.Text.Trim());
                bool isLBINCountry = Util.IsLBINCountries(txtDRShipperCountry.Text.Trim());

                if ((isLBINCountry && (cbDRDimUnit.Text == "CM" || cbDRWeightUnit.Text == "KG"))
                    || (!isLBINCountry && (cbDRDimUnit.Text == "IN" || cbDRWeightUnit.Text == "LB")))
                    MessageBox.Show("Wrong dim / weight unit type");

                System.Net.HttpWebRequest request;
                Uri uri = new Uri(@"https://xmlpi-ea.dhl.com/XMLShippingServlet");
                request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "text/xml; charset=utf-8";

                StringBuilder soapMsg = new StringBuilder();
                soapMsg.Append(@"<?xml version=""1.0""?>
                <p:DCTRequest xsi:schemaLocation=""http://www.dhl.com DCT-req.xsd"" 
                xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                xmlns:p2=""http://www.dhl.com/DCTRequestdatatypes"" 
                xmlns:p1=""http://www.dhl.com/datatypes/"" 
                xmlns:p=""http://www.dhl.com""> 
                <GetQuote><Request><ServiceHeader><SiteID>");
                soapMsg.Append("xxxx");
                soapMsg.Append("</SiteID><Password>");
                soapMsg.Append("pwd");
                soapMsg.Append("</Password>");
                soapMsg.Append("</ServiceHeader></Request><From><CountryCode>");
                soapMsg.Append(txtDRShipperCountry.Text.Trim());
                soapMsg.Append("</CountryCode>");

                soapMsg.Append("<Postalcode>");
                soapMsg.Append(txtDRShipperPostal.Text.Trim());
                soapMsg.Append("</Postalcode>");

                soapMsg.Append("<City>");
                soapMsg.Append(txtDRShipperCity.Text.Trim());
                soapMsg.Append("</City>");

                soapMsg.Append("</From><BkgDetails><PaymentCountryCode>");
                soapMsg.Append(txtDRPaymentCountry.Text.Trim());
                soapMsg.Append("</PaymentCountryCode><Date>");
                soapMsg.Append(dtpDRShipDate.Value.ToString("yyyy-MM-dd"));
                soapMsg.Append("</Date><ReadyTime>PT0H0M</ReadyTime><DimensionUnit>");
                soapMsg.Append(cbDRDimUnit.Text);
                soapMsg.Append("</DimensionUnit><WeightUnit>");
                soapMsg.Append(cbDRWeightUnit.Text);
                soapMsg.Append("</WeightUnit><Pieces>");

                soapMsg.Append("<Piece><PieceID>");
                soapMsg.Append("1");
                soapMsg.Append("</PieceID><Height>");
                soapMsg.Append(height.ToString("f2"));
                soapMsg.Append("</Height><Depth>");
                soapMsg.Append(length.ToString("f2"));
                soapMsg.Append("</Depth><Width>");
                soapMsg.Append(width.ToString("f2"));
                soapMsg.Append("</Width><Weight>");
                soapMsg.Append(weight.ToString("f2"));
                soapMsg.Append("</Weight></Piece>");


                soapMsg.Append("</Pieces><PaymentAccountNumber>");
                soapMsg.Append(cbDRAccountNo.Text);
                soapMsg.Append("</PaymentAccountNumber><IsDutiable>N</IsDutiable>");
                soapMsg.Append("<QtdShp>");

                soapMsg.Append("<GlobalProductCode>");
                if (txtDRShipperCountry.Text.Trim() == txtDRReceiverCountry.Text.Trim())
                    soapMsg.Append("N");
                else if (isEUtoEU)
                    soapMsg.Append("U");
                else
                    soapMsg.Append("D");
                soapMsg.Append("</GlobalProductCode>");
                soapMsg.Append("</QtdShp>");

                if (insurance > 0)
                {
                    soapMsg.Append("<InsuredValue>");
                    soapMsg.Append(insurance.ToString("f2"));
                    soapMsg.Append("</InsuredValue><InsuredCurrency>USD</InsuredCurrency>");
                }

                soapMsg.Append("</BkgDetails><To><CountryCode>");
                soapMsg.Append(txtDRReceiverCountry.Text.Trim());
                soapMsg.Append("</CountryCode>");

                soapMsg.Append("<Postalcode>");
                soapMsg.Append(txtDRReceiverPostal.Text.Trim());
                soapMsg.Append("</Postalcode>");

                soapMsg.Append("<City>");
                soapMsg.Append(txtDRReceiverCity.Text.Trim());
                soapMsg.Append("</City>");

                soapMsg.Append("</To>");
                if (declaredValue > 0)
                {
                    soapMsg.Append("<Dutiable><DeclaredCurrency>USD</DeclaredCurrency><DeclaredValue>");
                    soapMsg.Append(declaredValue.ToString("f2"));
                    soapMsg.Append("</DeclaredValue></Dutiable>");
                }

                soapMsg.Append("</GetQuote></p:DCTRequest>");

                XmlDocument soapXml = new XmlDocument();
                soapXml.LoadXml(soapMsg.ToString());

                using (Stream stream = request.GetRequestStream())
                {
                    soapXml.Save(stream);
                }

                XDocument xmlDoc;
                System.Net.HttpWebResponse response = null;

                try
                {
                    response = (System.Net.HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        XmlReader responseStream = XmlReader.Create(request.GetResponse().GetResponseStream());
                        xmlDoc = XDocument.Load(responseStream);
                        if (responseStream != null)
                            responseStream.Close();

                        XElement quoteResponse = xmlDoc.Root.Element("GetQuoteResponse");
                        if (quoteResponse != null)
                        {
                            XElement packageDetails = quoteResponse.Element("BkgDetails");
                            if (packageDetails != null)
                            {
                                lblDRFreightCharge.Text = packageDetails.Element("QtdShp").Element("ShippingCharge").Value;
                                lblDRFreightCharge.Text += packageDetails.Element("QtdShp").Element("CurrencyCode") != null ? packageDetails.Element("QtdShp").Element("CurrencyCode").Value : "";

                                XElement estimatedDeliveryDate = packageDetails.Element("QtdShp").Element("DeliveryDate");
                                if (estimatedDeliveryDate != null)
                                    lblDRDeliveryDate.Text = estimatedDeliveryDate.Value;

                                if (packageDetails.Element("QtdShp").Element("DimensionalWeight") != null && packageDetails.Element("QtdShp").Element("WeightUnit") != null)
                                {
                                    lblDRDimWeight.Text = packageDetails.Element("QtdShp").Element("DimensionalWeight").Value;
                                }
                            }
                            else
                            {
                                XElement condition = quoteResponse.Element("Note").Element("Condition");
                                if (condition != null)
                                {
                                    lblDRErrorMsg.Text = condition.Element("ConditionCode").Value + " " + condition.Element("ConditionData").Value;
                                }
                            }
                        }
                        else
                        {
                            XElement errorResponse = xmlDoc.Root.Element("Response");

                            if (errorResponse != null)
                            {
                                lblDRErrorMsg.Text = errorResponse.Element("Status").Element("Condition").Element("ConditionCode").Value + " " + errorResponse.Element("Status").Element("Condition").Element("ConditionData").Value;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (response != null)
                    {
                        response.Close();
                    }
                    response = null;
                    request = null;
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DHLPickup()
        {
            try
            {
                string region = "";
                using (CarrierDatabaseEntities context = new CarrierDatabaseEntities())
                {
                    region = (from o in context.BookingCountries
                              where o.Alpha2Code == txtDPCountry.Text.Trim()
                              select o.DHLPickupRegion).FirstOrDefault();
                }

                if (string.IsNullOrEmpty(region))
                {
                    MessageBox.Show("Invalid Region");
                    return;
                }

                System.Net.HttpWebRequest request;
                Uri uri = new Uri(@"https://xmlpitest-ea.dhl.com/XMLShippingServlet");
                request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "text/xml; charset=utf-8";

                string requestXML = "";
                switch (region)
                {
                    case "AM":
                    default:
                        requestXML = DHLPickupAM();
                        break;
                    case "EA":
                        requestXML = DHLPickupEA();
                        break;
                    case "AP":
                        requestXML = DHLPickupAP();
                        break;
                }

                XmlDocument soapXml = new XmlDocument();
                soapXml.LoadXml(requestXML);

                using (Stream stream = request.GetRequestStream())
                {
                    soapXml.Save(stream);
                }
                XDocument xmlDoc;
                System.Net.HttpWebResponse response = null;
                try
                {
                    response = (System.Net.HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        XmlReader responseStream = XmlReader.Create(request.GetResponse().GetResponseStream());
                        xmlDoc = XDocument.Load(responseStream);
                        if (responseStream != null)
                            responseStream.Close();

                        XElement confirmationNumber = xmlDoc.Root.Element("ConfirmationNumber");
                        if (confirmationNumber != null)
                        {
                            lblDPConfirmation.Text = confirmationNumber.Value; ;
                            XElement originSvcArea = xmlDoc.Root.Element("OriginSvcArea");
                            if (originSvcArea != null)
                                lblDPSvcArea.Text = originSvcArea.Value;
                        }
                        else
                        {
                            XElement condition = xmlDoc.Root.Element("Response").Element("Status").Element("Condition");
                            if (condition != null)
                            {
                                lblDPErrorMsg.Text = condition.Element("ConditionCode").Value + " " + condition.Element("ConditionData") != null ? condition.Element("ConditionData").Value : "";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (response != null)
                    {
                        response.Close();
                    }
                    response = null;
                    request = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string DHLPickupAM()
        {
            double weight = 0, insurance = 0; int pieces = 0;

            if (!double.TryParse(txtDPWeight.Text.Trim(), out weight))
            {
                MessageBox.Show("Invalid weight");
                return "";
            }

            if (!double.TryParse(txtDPInsurance.Text.Trim(), out insurance))
            {
                MessageBox.Show("Invalid insurance");
                return "";
            }

            if (!int.TryParse(txtDPPieces.Text.Trim(), out pieces))
            {
                MessageBox.Show("Invalid pieces");
                return "";
            }

            StringBuilder soapMsg = new StringBuilder();
            soapMsg.Append(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                <req:BookPickupRequest xmlns:req=""http://www.dhl.com"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.dhl.com
                book-pickup-req.xsd""><Request><ServiceHeader><SiteID>");
            soapMsg.Append("xxxx");
            soapMsg.Append("</SiteID><Password>");
            soapMsg.Append("pwd");
            soapMsg.Append("</Password>");
            soapMsg.Append("</ServiceHeader></Request><Requestor><AccountType>D</AccountType>");
            soapMsg.Append("<AccountNumber>");
            soapMsg.Append(cbDPAccountNo.Text);
            soapMsg.Append("</AccountNumber>");
            soapMsg.Append("<RequestorContact><PersonName>");
            soapMsg.Append(txtDPContactName.Text.Trim());
            soapMsg.Append("</PersonName><Phone>");
            soapMsg.Append(txtDPContactPhone.Text.Trim());
            soapMsg.Append("</Phone></RequestorContact></Requestor><Place>");

            string locType = cbDPLocationType.Text.Substring(0, 1);
            switch (locType)
            {
                case "B":
                    soapMsg.Append("<LocationType>B</LocationType><CompanyName>");
                    soapMsg.Append(txtDPCompanyName.Text.Trim());
                    soapMsg.Append("</CompanyName>");
                    break;
                case "R":
                default:
                    soapMsg.Append("<LocationType>R</LocationType>");
                    break;
                case "C":
                    soapMsg.Append("<LocationType>C</LocationType><CompanyName>");
                    soapMsg.Append(txtDPCompanyName.Text.Trim());
                    soapMsg.Append("</CompanyName>");
                    break;
            }

            soapMsg.Append("<Address1>");
            soapMsg.Append(txtDPAddr1.Text.Trim());
            soapMsg.Append("</Address1>");
            
            if(!string.IsNullOrEmpty(txtDPAddr2.Text.Trim()))
            {
                soapMsg.Append("<Address2>");
                soapMsg.Append(txtDPAddr2.Text.Trim());
                soapMsg.Append("</Address2>");
            }
            soapMsg.Append("<PackageLocation>");
            soapMsg.Append(txtDPPackageLoc.Text.Trim() == "" ? "None" : txtDPPackageLoc.Text.Trim());
            soapMsg.Append("</PackageLocation><City>");
            soapMsg.Append(txtDPCity.Text.Trim());
            soapMsg.Append("</City>");

            if (!string.IsNullOrEmpty(txtDPState.Text.Trim()))
            {
                if (txtDPState.Text.Trim().Length == 2)
                {
                    soapMsg.Append("<StateCode>");
                    soapMsg.Append("NY");
                    soapMsg.Append("</StateCode>");
                }
                else
                {
                    soapMsg.Append("<DivisionName>");
                    soapMsg.Append(txtDPState.Text.Trim());
                    soapMsg.Append("</DivisionName>");
                }
            }
            soapMsg.Append("<CountryCode>");
            soapMsg.Append(txtDPCountry.Text.Trim());
            soapMsg.Append("</CountryCode><PostalCode>");
            soapMsg.Append(txtDPPostal.Text.Trim());
            soapMsg.Append("</PostalCode></Place><Pickup><PickupDate>");
            soapMsg.Append(dtpDRShipDate.Value.ToString("yyyy-MM-dd"));
            soapMsg.Append("</PickupDate><ReadyByTime>");
            soapMsg.Append(dtpDPReadyTime.Value.ToString("HH:mm"));
            soapMsg.Append("</ReadyByTime><CloseTime>");
            soapMsg.Append(dtpDPCloseTime.Value.ToString("HH:mm"));
            soapMsg.Append("</CloseTime><Pieces>");
            soapMsg.Append(pieces.ToString());
            soapMsg.Append("</Pieces><weight><Weight>");
            soapMsg.Append(weight.ToString("f1"));
            soapMsg.Append("</Weight>");
            soapMsg.Append("<WeightUnit>");
            soapMsg.Append(cbDPWeightUnit.Text.Substring(0, 1));
            soapMsg.Append("</WeightUnit></weight>");
            soapMsg.Append("</Pickup><PickupContact><PersonName>");
            soapMsg.Append(txtDPContactName.Text.Trim());
            soapMsg.Append("</PersonName><Phone>");
            soapMsg.Append(txtDPContactPhone.Text.Trim());
            soapMsg.Append("</Phone></PickupContact><ShipmentDetails><NumberOfPieces>");
            soapMsg.Append(pieces.ToString());
            soapMsg.Append("</NumberOfPieces><Weight>");
            soapMsg.Append(weight.ToString("f1"));
            soapMsg.Append("</Weight>");
            soapMsg.Append("<WeightUnit>");
            soapMsg.Append(cbDPWeightUnit.Text.Substring(0, 1));
            soapMsg.Append("</WeightUnit>");
            soapMsg.Append("<GlobalProductCode>");
            soapMsg.Append(cbDPProductCode.Text);
            soapMsg.Append("</GlobalProductCode>");
            soapMsg.Append("<DoorTo>DD</DoorTo>");

            if (insurance > 0)
            {
                soapMsg.Append("<InsuredAmount>");
                soapMsg.Append(txtDPInsurance.Text.Trim());
                soapMsg.Append("</InsuredAmount>");
                soapMsg.Append("<InsuredCurrencyCode>USD</InsuredCurrencyCode>");
            }
            soapMsg.Append("</ShipmentDetails>");

            soapMsg.Append("</req:BookPickupRequest>");

            return soapMsg.ToString();
        }

        private string DHLPickupEA()
        {
            double weight = 0, insurance = 0; int pieces = 0;

            if (!double.TryParse(txtDPWeight.Text.Trim(), out weight))
            {
                MessageBox.Show("Invalid weight");
                return "";
            }

            if (!double.TryParse(txtDPInsurance.Text.Trim(), out insurance))
            {
                MessageBox.Show("Invalid insurance");
                return "";
            }

            if (!int.TryParse(txtDPPieces.Text.Trim(), out pieces))
            {
                MessageBox.Show("Invalid pieces");
                return "";
            }

            StringBuilder soapMsg = new StringBuilder();
            soapMsg.Append(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                        <req:BookPickupRequestEA xmlns:req=""http://www.dhl.com"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.dhl.com
                        book-pickup-req_EA.xsd""><Request><ServiceHeader><SiteID>xxxx</SiteID><Password>pwd</Password>
                        </ServiceHeader></Request><Requestor><AccountType>D</AccountType>");
            soapMsg.Append("<AccountNumber>");
            soapMsg.Append(cbDPAccountNo.Text);
            soapMsg.Append("</AccountNumber><RequestorContact><PersonName>");
            soapMsg.Append(txtDPContactName.Text.Trim());
            soapMsg.Append("</PersonName><Phone>");
            soapMsg.Append(txtDPContactPhone.Text.Trim());
            soapMsg.Append("</Phone></RequestorContact><CompanyName>");
            soapMsg.Append(txtDPCompanyName.Text.Trim());
            soapMsg.Append("</CompanyName></Requestor><Place>");

            string locType = cbDPLocationType.Text.Substring(0, 1);
            switch (locType)
            {
                case "B":
                    soapMsg.Append("<LocationType>B</LocationType><CompanyName>");
                    soapMsg.Append(txtDPCompanyName.Text.Trim());
                    soapMsg.Append("</CompanyName>");
                    break;
                case "R":
                default:
                    soapMsg.Append("<LocationType>R</LocationType>");
                    break;
                case "C":
                    soapMsg.Append("<LocationType>C</LocationType><CompanyName>");
                    soapMsg.Append(txtDPCompanyName.Text.Trim());
                    soapMsg.Append("</CompanyName>");
                    break;
            }

            soapMsg.Append("<Address1>");
            soapMsg.Append(txtDPAddr1.Text.Trim());
            soapMsg.Append("</Address1>");

            if (txtDPAddr2.Text.Trim() != "")
            {
                soapMsg.Append("<Address2>");
                soapMsg.Append(txtDPAddr2.Text.Trim());
                soapMsg.Append("</Address2>");
            }
            soapMsg.Append("<PackageLocation>");
            soapMsg.Append(txtDPPackageLoc.Text.Trim() == "" ? "None" : txtDPPackageLoc.Text.Trim());
            soapMsg.Append("</PackageLocation><City>");
            soapMsg.Append(txtDPCity.Text.Trim());
            soapMsg.Append("</City><CountryCode>");
            soapMsg.Append(txtDPCountry.Text.Trim());
            soapMsg.Append("</CountryCode>");
            if (!string.IsNullOrEmpty(txtDPState.Text.Trim()))
            {
                if (txtDPState.Text.Trim().Length == 2)
                {
                    soapMsg.Append("<StateCode>");
                    soapMsg.Append(txtDPState.Text.Trim());
                    soapMsg.Append("</StateCode>");
                }
                else
                {
                    soapMsg.Append("<DivisionName>");
                    soapMsg.Append(txtDPState.Text.Trim());
                    soapMsg.Append("</DivisionName>");
                }
            }
            soapMsg.Append("<PostalCode>");
            soapMsg.Append(txtDPPostal.Text.Trim());
            soapMsg.Append("</PostalCode></Place><Pickup><PickupDate>");
            soapMsg.Append(dtpDPPickupDate.Value.ToString("yyyy-MM-dd"));
            soapMsg.Append("</PickupDate><ReadyByTime>");
            soapMsg.Append(dtpDPReadyTime.Value.ToString("HH:mm"));
            soapMsg.Append("</ReadyByTime><CloseTime>");
            soapMsg.Append(dtpDPCloseTime.Value.ToString("HH:mm"));
            soapMsg.Append("</CloseTime><Pieces>");
            soapMsg.Append(pieces.ToString());
            soapMsg.Append("</Pieces><weight><Weight>");
            soapMsg.Append(weight.ToString("f1"));
            soapMsg.Append("</Weight>");
            soapMsg.Append("<WeightUnit>");
            soapMsg.Append(cbDPWeightUnit.Text.Substring(0, 1));
            soapMsg.Append("</WeightUnit></weight>");
            soapMsg.Append("</Pickup><PickupContact><PersonName>");
            soapMsg.Append(txtDPContactName.Text.Trim());
            soapMsg.Append("</PersonName><Phone>");
            soapMsg.Append(txtDPContactPhone.Text.Trim());
            soapMsg.Append("</Phone></PickupContact>");
            soapMsg.Append("<ShipmentDetails><NumberOfPieces>");
            soapMsg.Append(pieces.ToString());
            soapMsg.Append("</NumberOfPieces><Weight>");
            soapMsg.Append(weight.ToString("f1"));
            soapMsg.Append("</Weight>");
            soapMsg.Append("<WeightUnit>");
            soapMsg.Append(cbDPWeightUnit.Text.Substring(0, 1));
            soapMsg.Append("</WeightUnit>");
            soapMsg.Append("<GlobalProductCode>");
            soapMsg.Append(cbDPProductCode.Text);
            soapMsg.Append("</GlobalProductCode>");

            soapMsg.Append("<DoorTo>DD</DoorTo>");
            soapMsg.Append("<DimensionUnit>");
            soapMsg.Append(cbDPWeightUnit.Text.Substring(0, 1) == "K" ? "C" : "I");
            soapMsg.Append("</DimensionUnit>");
            if (insurance > 0)
            {
                soapMsg.Append("<InsuredAmount>");
                soapMsg.Append(txtDPInsurance.Text.Trim());
                soapMsg.Append("</InsuredAmount>");
                soapMsg.Append("<InsuredCurrencyCode>USD</InsuredCurrencyCode>");
            }

            soapMsg.Append("<Pieces><Weight>");
            soapMsg.Append(txtDPWeight.Text.Trim());
            soapMsg.Append("</Weight><Width>");
            soapMsg.Append(txtDPWidth.Text);
            soapMsg.Append("</Width><Height>");
            soapMsg.Append(txtDPHeight.Text);
            soapMsg.Append("</Height><Depth>");
            soapMsg.Append(txtDPLength.Text);
            soapMsg.Append("</Depth></Pieces>");

            soapMsg.Append("</ShipmentDetails>");

            soapMsg.Append("</req:BookPickupRequestEA>");

            return soapMsg.ToString();
        }

        private string DHLPickupAP()
        {
            double weight = 0, insurance = 0; int pieces = 0;

            if (!double.TryParse(txtDPWeight.Text.Trim(), out weight))
            {
                MessageBox.Show("Invalid weight");
                return "";
            }

            if (!double.TryParse(txtDPInsurance.Text.Trim(), out insurance))
            {
                MessageBox.Show("Invalid insurance");
                return "";
            }

            if (!int.TryParse(txtDPPieces.Text.Trim(), out pieces))
            {
                MessageBox.Show("Invalid pieces");
                return "";
            }

            StringBuilder soapMsg = new StringBuilder();
            soapMsg.Append(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                        <req:BookPickupRequestAP xmlns:req=""http://www.dhl.com"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.dhl.com
                        book-pickup-req_EA.xsd""><Request><ServiceHeader><SiteID>xxxx</SiteID><Password>pwd</Password>
                        </ServiceHeader></Request><Requestor><AccountType>D</AccountType>");
            soapMsg.Append("<AccountNumber>");
            soapMsg.Append(cbDPAccountNo.Text);
            soapMsg.Append("</AccountNumber><RequestorContact><PersonName>");
            soapMsg.Append(txtDPContactName.Text.Trim());
            soapMsg.Append("</PersonName><Phone>");
            soapMsg.Append(txtDPContactPhone.Text.Trim());
            soapMsg.Append("</Phone></RequestorContact><CompanyName>");
            soapMsg.Append(txtDPCompanyName.Text.Trim());
            soapMsg.Append("</CompanyName></Requestor><Place>");

            string locType = cbDPLocationType.Text.Substring(0, 1);
            switch (locType)
            {
                case "B":
                    soapMsg.Append("<LocationType>B</LocationType><CompanyName>");
                    soapMsg.Append(txtDPCompanyName.Text.Trim());
                    soapMsg.Append("</CompanyName>");
                    break;
                case "R":
                default:
                    soapMsg.Append("<LocationType>R</LocationType>");
                    break;
                case "C":
                    soapMsg.Append("<LocationType>C</LocationType><CompanyName>");
                    soapMsg.Append(txtDPCompanyName.Text.Trim());
                    soapMsg.Append("</CompanyName>");
                    break;
            }

            soapMsg.Append("<Address1>");
            soapMsg.Append(txtDPAddr1.Text.Trim());
            soapMsg.Append("</Address1>");

            if (txtDPAddr2.Text.Trim() != "")
            {
                soapMsg.Append("<Address2>");
                soapMsg.Append(txtDPAddr2.Text.Trim());
                soapMsg.Append("</Address2>");
            }
            soapMsg.Append("<PackageLocation>");
            soapMsg.Append(txtDPPackageLoc.Text.Trim() == "" ? "None" : txtDPPackageLoc.Text.Trim());
            soapMsg.Append("</PackageLocation><City>");
            soapMsg.Append(txtDPCity.Text.Trim());
            soapMsg.Append("</City>");
            if (!string.IsNullOrEmpty(txtDPState.Text.Trim()))
            {
                if (txtDPState.Text.Trim().Length == 2)
                {
                    soapMsg.Append("<StateCode>");
                    soapMsg.Append(txtDPState.Text.Trim());
                    soapMsg.Append("</StateCode>");
                }
                else
                {
                    soapMsg.Append("<DivisionName>");
                    soapMsg.Append(txtDPState.Text.Trim());
                    soapMsg.Append("</DivisionName>");
                }
            }
            soapMsg.Append("<CountryCode>");
            soapMsg.Append(txtDPCountry.Text.Trim());
            soapMsg.Append("</CountryCode><PostalCode>");
            soapMsg.Append(txtDPPostal.Text.Trim());
            soapMsg.Append("</PostalCode></Place><Pickup><PickupDate>");
            soapMsg.Append(dtpDRShipDate.Value.ToString("yyyy-MM-dd"));
            soapMsg.Append("</PickupDate><ReadyByTime>");
            soapMsg.Append(dtpDPReadyTime.Value.ToString("HH:mm"));
            soapMsg.Append("</ReadyByTime><CloseTime>");
            soapMsg.Append(dtpDPCloseTime.Value.ToString("HH:mm"));
            soapMsg.Append("</CloseTime><Pieces>");
            soapMsg.Append(pieces.ToString());
            soapMsg.Append("</Pieces><weight><Weight>");
            soapMsg.Append(weight.ToString("f1"));
            soapMsg.Append("</Weight>");
            soapMsg.Append("<WeightUnit>");
            soapMsg.Append(cbDPWeightUnit.Text.Substring(0, 1));
            soapMsg.Append("</WeightUnit></weight>");
            soapMsg.Append("</Pickup><PickupContact><PersonName>");
            soapMsg.Append(txtDPContactName.Text.Trim());
            soapMsg.Append("</PersonName><Phone>");
            soapMsg.Append(txtDPContactPhone.Text.Trim());
            soapMsg.Append("</Phone></PickupContact>");
            soapMsg.Append("<ShipmentDetails><NumberOfPieces>");
            soapMsg.Append(pieces.ToString());
            soapMsg.Append("</NumberOfPieces><Weight>");
            soapMsg.Append(weight.ToString("f1"));
            soapMsg.Append("</Weight>");
            soapMsg.Append("<WeightUnit>");
            soapMsg.Append(cbDPWeightUnit.Text.Substring(0, 1));
            soapMsg.Append("</WeightUnit>");
            soapMsg.Append("<GlobalProductCode>P</GlobalProductCode>");

            soapMsg.Append("<DoorTo>DD</DoorTo>");
            if (insurance > 0)
            {
                soapMsg.Append("<InsuredAmount>");
                soapMsg.Append(txtDPInsurance.Text.Trim());
                soapMsg.Append("</InsuredAmount>");
                soapMsg.Append("<InsuredCurrencyCode>USD</InsuredCurrencyCode>");
            }
            soapMsg.Append("</ShipmentDetails>");

            soapMsg.Append("</req:BookPickupRequestAP>");

            return soapMsg.ToString();
        }

        private void DHLCancelPickup()
        {
            try
            {
                string region = "";
                using (CarrierDatabaseEntities context = new CarrierDatabaseEntities())
                {
                    region = (from o in context.BookingCountries
                              where o.Alpha2Code == txtDCPCountry.Text.Trim()
                              select o.DHLPickupRegion).FirstOrDefault();
                }

                if (string.IsNullOrEmpty(region))
                {
                    MessageBox.Show("Invalid Region");
                    return;
                }
                System.Net.HttpWebRequest request;
                Uri uri = new Uri(@"https://xmlpitest-ea.dhl.com/XMLShippingServlet");
                request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "text/xml; charset=utf-8";

                string requestXML = "";
                switch (region)
                {
                    case "AM":
                    default:
                        requestXML = DHLCancelPickupAM();
                        break;
                    case "EA":
                        requestXML = DHLCancelPickupEA();
                        break;
                    case "AP":
                        requestXML = DHLCancelPickupAP();
                        break;
                }

                XmlDocument soapXml = new XmlDocument();
                soapXml.LoadXml(requestXML);

                using (Stream stream = request.GetRequestStream())
                {
                    soapXml.Save(stream);
                }
                XDocument xmlDoc;
                System.Net.HttpWebResponse response = null;
                try
                {
                    response = (System.Net.HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        XmlReader responseStream = XmlReader.Create(request.GetResponse().GetResponseStream());
                        xmlDoc = XDocument.Load(responseStream);
                        if (responseStream != null)
                            responseStream.Close();
                        XElement eleNote = xmlDoc.Root.Element("Note");
                        if (eleNote != null)
                        {
                            XElement eleActionNote = eleNote.Element("ActionNote");
                            if (eleActionNote != null)
                                lblDCPResult.Text = eleActionNote.Value;
                        }
                        else
                        {
                            XElement condition = xmlDoc.Root.Element("Response").Element("Status").Element("Condition");
                            if (condition != null)
                            {
                                lblDCPErrorMsg.Text = condition.Element("ConditionCode").Value + " " + condition.Element("ConditionData") != null ? condition.Element("ConditionData").Value : "";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (response != null)
                    {
                        response.Close();
                    }
                    response = null;
                    request = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string DHLCancelPickupAM()
        {
            StringBuilder soapMsg = new StringBuilder();
            soapMsg.Append(@"<?xml version=""1.0""?>
                <req:CancelPickupRequest xmlns:req=""http://www.dhl.com"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.dhl.com cancel-pickup-req.xsd""><Request><ServiceHeader><SiteID>");
            soapMsg.Append("xxxx");
            soapMsg.Append("</SiteID><Password>");
            soapMsg.Append("pwd");
            soapMsg.Append("</Password>");
            soapMsg.Append("</ServiceHeader></Request><ConfirmationNumber>");
            soapMsg.Append(txtDCPConfirmation.Text.Trim());
            soapMsg.Append("</ConfirmationNumber><RequestorName>");
            soapMsg.Append(txtDCPRequestor.Text.Trim());
            soapMsg.Append("</RequestorName>");

            soapMsg.Append("<OriginSvcArea>");
            soapMsg.Append(txtDCPOriginSvcArea.Text.Trim());
            soapMsg.Append("</OriginSvcArea>");

            soapMsg.Append("<Reason>");
            soapMsg.Append("00" + cbDCPReason.Text.Substring(0, 1));
            soapMsg.Append("</Reason></req:CancelPickupRequest>");

            return soapMsg.ToString();

        }

        private string DHLCancelPickupEA()
        {
            StringBuilder soapMsg = new StringBuilder();
            soapMsg.Append(@"<?xml version=""1.0""?>
                <req:CancelPickupRequestEA xmlns:req=""http://www.dhl.com"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.dhl.com cancel-pickup-req.xsd""><Request><ServiceHeader><SiteID>");
            soapMsg.Append("xxxx");
            soapMsg.Append("</SiteID><Password>");
            soapMsg.Append("pwd");
            soapMsg.Append("</Password>");
            soapMsg.Append("</ServiceHeader></Request><ConfirmationNumber>");
            soapMsg.Append(txtDCPConfirmation.Text.Trim());
            soapMsg.Append("</ConfirmationNumber><RequestorName>");
            soapMsg.Append(txtDCPRequestor.Text.Trim());
            soapMsg.Append("</RequestorName>");
            soapMsg.Append("<Reason>");
            soapMsg.Append("00" + cbDCPReason.Text.Substring(0, 1));
            soapMsg.Append("</Reason><PickupDate>");
            soapMsg.Append(dtpDCPPickupDate.Value.ToString("yyyy-MM-dd"));
            soapMsg.Append("</PickupDate><CountryCode>");
            soapMsg.Append(txtDCPCountry.Text.Trim());
            soapMsg.Append("</CountryCode><CancelTime>");
            soapMsg.Append(DateTime.Now.ToString("HH:mm"));
            soapMsg.Append("</CancelTime>");

            soapMsg.Append("</req:CancelPickupRequestEA>");

            return soapMsg.ToString();
        }

        private string DHLCancelPickupAP()
        {
            StringBuilder soapMsg = new StringBuilder();
            soapMsg.Append(@"<?xml version=""1.0""?>
                <req:CancelPickupRequestAP xmlns:req=""http://www.dhl.com"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.dhl.com cancel-pickup-req.xsd""><Request><ServiceHeader><SiteID>");
            soapMsg.Append("xxxx");
            soapMsg.Append("</SiteID><Password>");
            soapMsg.Append("pwd");
            soapMsg.Append("</Password>");
            soapMsg.Append("</ServiceHeader></Request><ConfirmationNumber>");
            soapMsg.Append(txtDCPConfirmation.Text.Trim());
            soapMsg.Append("</ConfirmationNumber><RequestorName>");
            soapMsg.Append(txtDCPRequestor.Text.Trim());
            soapMsg.Append("</RequestorName><CountryCode>");
            soapMsg.Append(txtDCPCountry.Text.Trim());
            soapMsg.Append("</CountryCode>");
            soapMsg.Append("<OriginSvcArea>");
            soapMsg.Append(txtDCPOriginSvcArea.Text.Trim());
            soapMsg.Append("</OriginSvcArea>");

            soapMsg.Append("<Reason>");
            soapMsg.Append("00" + cbDCPReason.Text.Substring(0, 1));
            soapMsg.Append("</Reason></req:CancelPickupRequestAP>");

            return soapMsg.ToString();
        }

        private void DHLTASLandedCost()
        {
            DHLTASLandedCostWebReference.LandedCostService service = new DHLTASLandedCostWebReference.LandedCostService();
            service.Credentials = new System.Net.NetworkCredential("xxxx","pwd");
            DHLTASLandedCostWebReference.LandedCostInput input = new DHLTASLandedCostWebReference.LandedCostInput();
            
            input.countryCode = txtDLCProductCountry.Text.Trim();
            input.description = txtDLCProductDesc.Text.Trim();
            input.domain = "IMPORT";
            input.insuranceValue = txtDLCInsurance.Text.Trim();
            input.insuranceCurrency = "USD";

            //Measurement
            input.measurementType = cbDLCMeasureType.Text;
            input.unit = cbDLCMeasureUnit.Text;
            input.value = txtDLCMeasureValue.Text.Trim();

            input.priceValue = txtDLCPrice.Text.Trim();
            input.priceCurrency = "USD";
            input.productCode = txtDLCProductCode.Text.Trim();
            input.type = cbDLCCodeType.Text;
            input.shipperCountry = txtDLCShipperCountry.Text.Trim();
            input.receiverCountry = txtDLCReceiverCountry.Text.Trim();
            input.shipToState = txtDLCReceiverState.Text.Trim();
            input.shipmentCurrency = txtDLCShipmentCurrency.Text.Trim();
            input.totalQuantity = txtDLCTotalQty.Text.Trim();
            input.transportationValue = txtDLCTransportationValue.Text.Trim();
            input.transportationCurrency = "USD";
            
            DHLTASLandedCostWebReference.LandedCostResponseObject response = service.getLandedCostEstimate(input,"");

            if (response != null)
            {
                lblDLCEstimatedFees.Text = response.response.totalEstimatedFees;
                lblDLCLandedCost.Text = response.response.totalLandedCost;
                lblDLCShipmentValue.Text = response.response.shipmentValue;
            }
        }
        #endregion

        #region FedEx
        //private void FedExRate()
        //{
        //    decimal result = 0;
        //    try
        //    {
        //        FedExRateWebReference.RateRequest request = CreateFedExRateRequest();
        //        FedExRateWebReference.RateService service = new FedExRateWebReference.RateService();
        //        FedExRateWebReference.RateReply reply = service.getRates(request);

        //        if (reply.HighestSeverity == FedExRateWebReference.NotificationSeverityType.SUCCESS || reply.HighestSeverity == FedExRateWebReference.NotificationSeverityType.NOTE || reply.HighestSeverity == FedExRateWebReference.NotificationSeverityType.WARNING)
        //        {
        //            FedExRateWebReference.RatedShipmentDetail[] shipmentDetails = reply.RateReplyDetails[0].RatedShipmentDetails;
        //            if (shipmentDetails != null && shipmentDetails.Length > 0)
        //            {
        //                FedExRateWebReference.ShipmentRateDetail rateDetail = shipmentDetails[0].ShipmentRateDetail;
        //                result = rateDetail.TotalNetCharge.Amount;
        //                if (reply.RateReplyDetails[0].DeliveryTimestampSpecified)
        //                    ratingResponse.EstimatedDeliveryDate = reply.RateReplyDetails[0].DeliveryTimestamp.ToString("MM/dd/yyyy");

        //                if (rateDetail.TotalBillingWeight != null)
        //                {
        //                    ratingResponse.DimWeightUnit = rateDetail.TotalBillingWeight.Units.ToString();
        //                    ratingResponse.DimWeight = rateDetail.TotalBillingWeight.Value;
        //                    ratingResponse.ChargeableWeight = rateDetail.TotalBillingWeight.Value.ToString("f2") + " " + rateDetail.TotalBillingWeight.Units.ToString();
        //                }
        //            }
        //            else
        //            {
        //                ratingResponse.ErrorMessage.ErrorCode = "200";
        //                ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_UNKNOWN_ERROR;
        //            }
        //        }
        //        else
        //        {
        //            if (reply.Notifications != null && reply.Notifications.Length > 0)
        //            {
        //                if (string.IsNullOrEmpty(ratingResponse.ErrorMessage.ErrorMessage))
        //                {
        //                    ratingResponse.ErrorMessage.ErrorCode = reply.Notifications[0].Code;
        //                    ratingResponse.ErrorMessage.ErrorMessage = reply.Notifications[0].Message;
        //                }
        //                m_log.Error("Fedex Error: " + ratingResponse.ErrorMessage.ErrorCode + " " + ratingResponse.ErrorMessage.ErrorMessage);
        //            }
        //            else
        //            {
        //                if (string.IsNullOrEmpty(ratingResponse.ErrorMessage.ErrorMessage))
        //                {
        //                    ratingResponse.ErrorMessage.ErrorCode = "019";
        //                    ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_UNKNOWN_ERROR;
        //                }
        //            }
        //        }
        //    }
        //    catch (SoapException ex)
        //    {
        //        ratingResponse.ErrorMessage.ErrorCode = "503";
        //        ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
        //        if (m_log != null) m_log.Error(ex.Detail.InnerText.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        ratingResponse.ErrorMessage.ErrorCode = "504";
        //        ratingResponse.ErrorMessage.ErrorMessage = CommonMessages.ERROR_PROCESSING_REQUEST;
        //        if (m_log != null) m_log.Error(ex.ToString());
        //    }

        //    return result;
        //}



        #endregion


        private void btnRequest_Click(object sender, EventArgs e)
        {
            DHLRate();
        }

        private void btnDPRequest_Click(object sender, EventArgs e)
        {
            DHLPickup();
        }

        private void btnDCPRequest_Click(object sender, EventArgs e)
        {
            DHLCancelPickup();
        }

        private void btnFRRequest_Click(object sender, EventArgs e)
        {

        }

        private void btnDLCRequest_Click(object sender, EventArgs e)
        {
            DHLTASLandedCost();
        }


    }
}
