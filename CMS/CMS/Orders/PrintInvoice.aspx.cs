using Microsoft.VisualBasic;
using System;
//using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#region "Import Namespaces"

using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using Microsoft.Reporting.WebForms;

#endregion

using CMS.DAL;

namespace CMS.Orders
{
    public partial class PrintInvoice : System.Web.UI.Page
    {
        //protected global::System.Web.UI.HtmlControls.HtmlForm form1;

        protected global::Microsoft.Reporting.WebForms.ReportViewer ReportViewer1;
        Microsoft.Reporting.WebForms.LocalReport rep;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CreatePdf(Convert.ToInt32(Request.QueryString["OrderId"]), Convert.ToInt32(Request.QueryString["UserId"]), Request.QueryString["Dbname"].Trim(), "P");

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        public void CreatePdf(int iOrderId, int iUserID, string strDBName, string strFlag)
        {

            try
            {

                //Generate Report
                rwInvoice.LocalReport.EnableExternalImages = true;
                DataSet dsOrderInfo = null;
                DataSet dsOrderItems = null;

                dsOrderInfo = (new OrderEntryDAL()).GetOrderInfo_DAL(iOrderId, iUserID);
                dsOrderItems = (new OrderEntryDAL()).GetOrderItems_DAL(iOrderId, iUserID);
                ReportDataSource datasource = new ReportDataSource("OrderInfo", dsOrderInfo.Tables[0]);
                ReportDataSource datasource1 = new ReportDataSource("Items", dsOrderItems.Tables[0]);

                rwInvoice.LocalReport.ReportPath = "PrintInvoice.rdlc";
                rwInvoice.ProcessingMode = ProcessingMode.Local;

                ReportParameter ordernum = new ReportParameter("OrderNum", dsOrderInfo.Tables[0].Rows[0]["OrderNum"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { ordernum });

                ReportParameter orderdate = new ReportParameter("OrderDate", dsOrderInfo.Tables[0].Rows[0]["OrderDate"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { orderdate });

                ReportParameter rptprmcustname = new ReportParameter("CustName", dsOrderInfo.Tables[0].Rows[0]["CustName"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmcustname });

                ReportParameter status = new ReportParameter("Status", dsOrderInfo.Tables[0].Rows[0]["Status"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { status });

                //ReportParameter ETA2 = new ReportParameter("ETA", dsOrderInfo.Tables[0].Rows[0]["ETA"].ToString());
                //rwInvoice.LocalReport.SetParameters(new ReportParameter[] { ETA2 });

                ReportParameter ETA = new ReportParameter("ETA", dsOrderInfo.Tables[0].Rows[0]["ETA"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { ETA });




                //ReportParameter ShipFromName = new ReportParameter("ShipFromName", dsOrderInfo.Tables[0].Rows[0]["ShipFromName"].ToString());
                //rwInvoice.LocalReport.SetParameters(new ReportParameter[] { ShipFromName });

                //ReportParameter ShipFromAdd1 = new ReportParameter("ShipFromAdd1", dsOrderInfo.Tables[0].Rows[0]["ShipFromAdd1"].ToString());
                //rwInvoice.LocalReport.SetParameters(new ReportParameter[] { ShipFromAdd1 });


                //ReportParameter ShipFromAdd2 = new ReportParameter("ShipFromAdd2", dsOrderInfo.Tables[0].Rows[0]["ShipFromAdd2"].ToString());
                //rwInvoice.LocalReport.SetParameters(new ReportParameter[] { ShipFromAdd2 });



                //ReportParameter ShipFromCity = new ReportParameter("ShipFromCity", dsOrderInfo.Tables[0].Rows[0]["ShipFromCity"].ToString());
                //rwInvoice.LocalReport.SetParameters(new ReportParameter[] { ShipFromCity });




                //ReportParameter rptprmreference3 = new ReportParameter("ShipFromState", dsOrderInfo.Tables[0].Rows[0]["ShipFromState"].ToString());
                //rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmreference3 });

                //ReportParameter rptprmservicetype = new ReportParameter("ShipFromCountry", dsOrderInfo.Tables[0].Rows[0]["ShipFromCountry"].ToString());
                //rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmservicetype });

                //ReportParameter rptprmplacedby = new ReportParameter("ShipFromZip", dsOrderInfo.Tables[0].Rows[0]["ShipFromZip"].ToString());
                //rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmplacedby });




                ReportParameter BillingToName = new ReportParameter("BillingToName", dsOrderInfo.Tables[0].Rows[0]["BillingToName"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { BillingToName });

                ReportParameter BillingToAdd1 = new ReportParameter("BillingToAdd1", dsOrderInfo.Tables[0].Rows[0]["BillingToAdd1"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { BillingToAdd1 });


                ReportParameter BillingToAdd2 = new ReportParameter("BillingToAdd2", dsOrderInfo.Tables[0].Rows[0]["BillingToAdd2"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { BillingToAdd2 });



                ReportParameter BillingToCity = new ReportParameter("BillingToCity", dsOrderInfo.Tables[0].Rows[0]["BillingToCity"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { BillingToCity });


                ReportParameter BillingToState = new ReportParameter("BillingToState", dsOrderInfo.Tables[0].Rows[0]["BillingToState"].ToString().Trim());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { BillingToState });

                ReportParameter BillingToCountry = new ReportParameter("BillingToCountry", dsOrderInfo.Tables[0].Rows[0]["BillingToCountry"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { BillingToCountry });

                ReportParameter BillingToZip = new ReportParameter("BillingToZip", dsOrderInfo.Tables[0].Rows[0]["BillingToZip"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { BillingToZip });



                ReportParameter rptprmeta = new ReportParameter("ShipToName", dsOrderInfo.Tables[0].Rows[0]["ShipToName"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmeta });

                ReportParameter rptprmcustomerpo = new ReportParameter("ShipToAdd1", dsOrderInfo.Tables[0].Rows[0]["ShipToAdd1"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmcustomerpo });

                ReportParameter rptprmordertype = new ReportParameter("ShipToAdd2", dsOrderInfo.Tables[0].Rows[0]["ShipToAdd2"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmordertype });

                ReportParameter rptprmsubordertype = new ReportParameter("ShipToCity", dsOrderInfo.Tables[0].Rows[0]["ShipToCity"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmsubordertype });

                ReportParameter rptprmorderdate = new ReportParameter("ShipToState", dsOrderInfo.Tables[0].Rows[0]["ShipToState"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmorderdate });

                ReportParameter rptprmconby = new ReportParameter("ShipToCountry", dsOrderInfo.Tables[0].Rows[0]["ShipToCountry"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmconby });

                ReportParameter rptprmcondatetime = new ReportParameter("ShipToZip", dsOrderInfo.Tables[0].Rows[0]["ShipToZip"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmcondatetime });


                ReportParameter rptprmshiptoname = new ReportParameter("CustEmail", dsOrderInfo.Tables[0].Rows[0]["CustEmail"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmshiptoname });

                ReportParameter rptprmshiptoadd = new ReportParameter("NumberOfItems", dsOrderInfo.Tables[0].Rows[0]["NumberOfItems"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmshiptoadd });

                ReportParameter rptprmshiptoadd1 = new ReportParameter("PayMethod", dsOrderInfo.Tables[0].Rows[0]["PayMethod"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmshiptoadd1 });

                ReportParameter rptprmshiptocountry = new ReportParameter("BilledDate", dsOrderInfo.Tables[0].Rows[0]["BilledDate"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmshiptocountry });

                ReportParameter TotalDiscount = new ReportParameter("TotalDiscount", dsOrderInfo.Tables[0].Rows[0]["TotalDiscount"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { TotalDiscount });



                ReportParameter QuoteAmount = new ReportParameter("QuoteAmount", dsOrderInfo.Tables[0].Rows[0]["QuoteAmount"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { QuoteAmount });


                ReportParameter rptprmshipfromname = new ReportParameter("ReqShipMethod", dsOrderInfo.Tables[0].Rows[0]["ReqShipMethod"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmshipfromname });



                ReportParameter rptprmshipfromadd = new ReportParameter("ShipCharge", dsOrderInfo.Tables[0].Rows[0]["ShipCharge"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmshipfromadd });



                ReportParameter rptprmshipfromadd1 = new ReportParameter("SubTotal", dsOrderInfo.Tables[0].Rows[0]["SubTotal"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmshipfromadd1 });


                ReportParameter rptprmshipfromcoun = new ReportParameter("Total", dsOrderInfo.Tables[0].Rows[0]["Total"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmshipfromcoun });

                ReportParameter rptprmordernum = new ReportParameter("BillingCode", dsOrderInfo.Tables[0].Rows[0]["BillingCode"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmordernum });

                ReportParameter rptprmfaxnumber = new ReportParameter("Packaging", dsOrderInfo.Tables[0].Rows[0]["Packaging"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmfaxnumber });

                ReportParameter rptprmcontactnumber = new ReportParameter("TrackingNo", dsOrderInfo.Tables[0].Rows[0]["TrackingNo"].ToString());
                rwInvoice.LocalReport.SetParameters(new ReportParameter[] { rptprmcontactnumber });


                rwInvoice.LocalReport.DataSources.Clear();
                rwInvoice.LocalReport.DataSources.Add(datasource);
                rwInvoice.LocalReport.DataSources.Add(datasource1);
                rwInvoice.LocalReport.Refresh();

                //Convert Report in to Pdf
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                string fileName = "PrintInvoiceReport";

                rwInvoice.ProcessingMode = ProcessingMode.Local;
                rwInvoice.LocalReport.ReportPath = "PrintInvoice.rdlc";


                byte[] bytes = rwInvoice.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
                Response.BinaryWrite(bytes);
                Response.Flush();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }


    }
}