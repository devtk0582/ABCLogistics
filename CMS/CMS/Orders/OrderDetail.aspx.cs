using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.DAL;


namespace CMS.Orders
{
    public partial class OrderDetail : System.Web.UI.Page
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        protected void Page_Load(object sender, EventArgs e)
        {
            int iOrderId;
            String strDbName;
            String strOrderStatus;
            lblErrorMsg.Visible = false;
            try
            {

                iOrderId = Convert.ToInt32(Request.QueryString["OrderId"]);
                ViewState["OrderId"] = iOrderId;
                strDbName = Request.QueryString["DbName"];
                ViewState["DbName"] = strDbName;
                strOrderStatus = Request.QueryString["Status"];
                ViewState["OrderStatus"] = strOrderStatus;
                //BindShippingMethods();
                if (!(IsPostBack))
                {
                    Bind(iOrderId);
                    BindShippingMethods();
                }


            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Page_Load");
                lblErrorMsg.Text = strErrCode;
                lblErrorMsg.Visible = true;
            }

        }

        public void Bind(int iOrderId)
        {
            DataSet dsOrderInfo = null;
            DataSet dsOrderItems = null;
            DataSet dsNotesDetail = null;
            dsOrderInfo = (new OrderEntryDAL().GetOrderInfo_DAL(iOrderId, (int)Session["UserID"]));
            FillOrderInfo(dsOrderInfo);
            dsOrderItems = (new OrderEntryDAL().GetOrderItems_DAL(iOrderId, (int)Session["UserID"]));
            FillOrderItems(dsOrderItems);
            if (lblOrderNum.Text.Trim() != string.Empty)
            {
                dsNotesDetail = (new OrderEntryDAL().GetOrderNotes_DAL(Convert.ToInt32(lblOrderNum.Text.Trim()), 0));
                FillNotesGrid(dsNotesDetail);
            }
        }
        private void BindShippingMethods()
        {
            try
            {
                DataSet shippingMethodList = (new SettingsDAL()).GetShippingMethods(1);
                if (shippingMethodList != null && shippingMethodList.Tables[0].Rows.Count > 0)
                {
                    ddlShippMethod.DataSource = shippingMethodList;
                    ddlShippMethod.DataTextField = "ShipMethod_Name";
                    ddlShippMethod.DataValueField = "ShipMethod_ID";
                    ddlShippMethod.DataBind();

                }
                else
                {
                    ddlShippMethod.DataSource = null;
                    ddlShippMethod.DataBind();
                }
                ddlShippMethod.Items.Insert(0, new ListItem("Select", "-1"));
            }
            catch (Exception ex)
            {

                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindShippingMethods");
                lblErrorMsg.Text = strErrCode;
                lblErrorMsg.Visible = true;
            }
        }
        public void FillOrderItems(DataSet dsOrderItems)
        {

            try
            {
                if ((dsOrderItems.Tables[0].Rows.Count > 0))
                {

                    gvItems.DataSource = dsOrderItems.Tables[0];
                    gvItems.DataBind();
                }

            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "FillOrderItems");
                lblErrorMsg.Text = strErrCode;
                lblErrorMsg.Visible = true;
            }
        }
        public void FillNotesGrid(DataSet dsNotesDetail)
        {
            gvNotes.DataSource = null;
            gvNotes.DataBind();
            gvNotes.DataSource = dsNotesDetail;
            gvNotes.DataBind();


        }
        public void FillOrderInfo(DataSet dsOrderInfo)
        {

            try
            {
                if ((dsOrderInfo.Tables[0].Rows.Count > 0))
                {
                    if (dsOrderInfo.Tables[0].Rows[0]["Ordernum"].ToString() != "")
                    {
                        lblOrderNum.Text = dsOrderInfo.Tables[0].Rows[0]["Ordernum"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["OrderDate"].ToString() != "")
                    {
                        lblSubmittedOn.Text = dsOrderInfo.Tables[0].Rows[0]["OrderDate"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["CustName"].ToString() != "")
                    {
                        lblCustomer.Text = dsOrderInfo.Tables[0].Rows[0]["CustName"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["CustEmail"].ToString() != "")
                    {
                        lblCustMail.Text = dsOrderInfo.Tables[0].Rows[0]["CustEmail"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["status"].ToString() != "")
                    {
                        lblOrderStatus.Text = dsOrderInfo.Tables[0].Rows[0]["status"].ToString();
                        if (lblOrderStatus.Text.Trim() == "CANCELLED")
                        {
                            lbtnPrintInovoice.Visible = false;
                        }
                        else
                        {
                            lbtnPrintInovoice.Visible = true;
                        }
                        String Status = dsOrderInfo.Tables[0].Rows[0]["status"].ToString().Trim().Substring(0, 1);
                        if (Status == "N")
                        {
                            Status = "Processing";
                            tblOrderupdate.Visible = true;
                            trTracking.Visible = false;
                            trShipDate.Visible = false;
                            trShipMethod.Visible = false;
                            txtComment.Text = string.Empty;
                            ddlStatus.Items.Clear();
                            ddlStatus.Items.Insert(0, new ListItem("Select", "-1"));
                            ddlStatus.Items.Insert(1, new ListItem("Processing", "Processing"));
                            ddlStatus.Items.Insert(2, new ListItem("Cancelled", "Cancelled"));
                        }
                        else if (Status == "P")
                        {
                            Status = "Shipped";
                            tblOrderupdate.Visible = true;
                            trTracking.Visible = true;
                            trShipDate.Visible = false;
                            trShipMethod.Visible = true;
                            txtComment.Text = string.Empty;
                            lblShipDate.Text = "Ship Date:";
                            ddlStatus.Items.Clear();
                            ddlStatus.Items.Insert(0, new ListItem("Select", "-1"));
                            ddlStatus.Items.Insert(1, new ListItem("Shipped", "Shipped"));
                            ddlStatus.Items.Insert(2, new ListItem("Cancelled", "Cancelled"));
                        }
                        else if (Status == "S")
                        {
                            Status = "Delivered";
                            tblOrderupdate.Visible = true;
                            trTracking.Visible = false;
                            trShipDate.Visible = true;
                            trShipMethod.Visible = false;
                            txtComment.Text = string.Empty;
                            ddlStatus.Items.Clear();
                            lblShipDate.Text = "Delivery Date:";
                            ddlStatus.Items.Insert(0, new ListItem("Select", "-1"));
                            ddlStatus.Items.Insert(1, new ListItem("Delivered", "Delivered"));
                            ddlStatus.Items.Insert(2, new ListItem("Cancelled", "Cancelled"));

                        }

                        else if (Status == "D")
                        {
                            Status = "Delivered";
                            tblOrderupdate.Visible = false;
                        }
                        else if (Status == "C")
                        {
                            Status = "Cancelled";
                            tblOrderupdate.Visible = false;
                        }
                        ddlStatus.SelectedValue = Status;
                        //lblStatus.Text = ddlStatus.SelectedItem.Text.Trim(); 
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["ETA"].ToString() != "")
                    {
                        lblInHandBy.Text = dsOrderInfo.Tables[0].Rows[0]["ETA"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["ETA"].ToString() != "")
                    {
                        lblInHandBy.Text = dsOrderInfo.Tables[0].Rows[0]["ETA"].ToString();
                    }


                    //if (dsOrderInfo.Tables[0].Rows[0]["ShipFromName"].ToString()!="")
                    //{
                    //    lblBillingName.Text=dsOrderInfo.Tables[0].Rows[0]["ShipFromName"].ToString();
                    //}
                    //if (dsOrderInfo.Tables[0].Rows[0]["ShipFromAdd1"].ToString()!="")
                    //{
                    //   lblBillAdd1.Text=dsOrderInfo.Tables[0].Rows[0]["ShipFromAdd1"].ToString();
                    //}
                    // if (dsOrderInfo.Tables[0].Rows[0]["ShipFromAdd2"].ToString()!="")
                    //{
                    //    lblBillAdd2.Text=dsOrderInfo.Tables[0].Rows[0]["ShipFromAdd2"].ToString();
                    //}
                    //if (dsOrderInfo.Tables[0].Rows[0]["ShipFromAdd2"].ToString()!="")
                    //{
                    //     lblBillAdd2.Text=dsOrderInfo.Tables[0].Rows[0]["ShipFromAdd2"].ToString();
                    //}
                    //if (dsOrderInfo.Tables[0].Rows[0]["ShipFromCity"].ToString()!="")
                    //{
                    //     lblBillCity.Text=dsOrderInfo.Tables[0].Rows[0]["ShipFromCity"].ToString();
                    //}
                    //if (dsOrderInfo.Tables[0].Rows[0]["ShipFromCountry"].ToString() != "")
                    //{
                    //    lblCountry.Text = dsOrderInfo.Tables[0].Rows[0]["ShipFromCountry"].ToString();
                    //}
                    //if (dsOrderInfo.Tables[0].Rows[0]["ShipFromState"].ToString() != "")
                    //{
                    //    lblBillStateZip.Text = dsOrderInfo.Tables[0].Rows[0]["ShipFromState"].ToString();
                    //}
                    //if (dsOrderInfo.Tables[0].Rows[0]["ShipFromZip"].ToString() != "")
                    //{
                    //    lblBillStateZip.Text = lblBillStateZip.Text + " " + dsOrderInfo.Tables[0].Rows[0]["ShipFromZip"].ToString();
                    //}


                    if (dsOrderInfo.Tables[0].Rows[0]["BillingToName"].ToString() != "")
                    {
                        lblBillingName.Text = dsOrderInfo.Tables[0].Rows[0]["BillingToName"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["BillingToAdd1"].ToString() != "")
                    {
                        lblBillAdd1.Text = dsOrderInfo.Tables[0].Rows[0]["BillingToAdd1"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["BillingToAdd2"].ToString() != "")
                    {
                        lblBillAdd2.Text = dsOrderInfo.Tables[0].Rows[0]["BillingToAdd2"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["BillingToAdd2"].ToString() != "")
                    {
                        lblBillAdd2.Text = dsOrderInfo.Tables[0].Rows[0]["BillingToAdd2"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["BillingToCity"].ToString() != "")
                    {
                        lblBillCity.Text = dsOrderInfo.Tables[0].Rows[0]["BillingToCity"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["BillingToCountry"].ToString() != "")
                    {
                        lblCountry.Text = dsOrderInfo.Tables[0].Rows[0]["BillingToCountry"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["BillingToState"].ToString() != "")
                    {
                        lblBillStateZip.Text = dsOrderInfo.Tables[0].Rows[0]["BillingToState"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["BillingToZip"].ToString() != "")
                    {
                        lblBillStateZip.Text = lblBillStateZip.Text + " " + dsOrderInfo.Tables[0].Rows[0]["BillingToZip"].ToString();
                    }

                    if (dsOrderInfo.Tables[0].Rows[0]["ShipMethod_ID"].ToString() != "0")
                    {
                        ddlShippMethod.SelectedValue = dsOrderInfo.Tables[0].Rows[0]["ShipMethod_ID"].ToString();
                    }



                    if (dsOrderInfo.Tables[0].Rows[0]["ShipToName"].ToString() != "")
                    {
                        lblShipName.Text = dsOrderInfo.Tables[0].Rows[0]["ShipToName"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["ShipToAdd1"].ToString() != "")
                    {
                        lblShipAdd1.Text = dsOrderInfo.Tables[0].Rows[0]["ShipToAdd1"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["ShipToAdd2"].ToString() != "")
                    {
                        lblShipAdd2.Text = dsOrderInfo.Tables[0].Rows[0]["ShipToAdd2"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["ShipToCity"].ToString() != "")
                    {
                        lblShippCity.Text = dsOrderInfo.Tables[0].Rows[0]["ShipToCity"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["ShipToState"].ToString() != "")
                    {
                        lblShipStateZip.Text = dsOrderInfo.Tables[0].Rows[0]["ShipToState"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["ShipToCountry"].ToString() != "")
                    {
                        lblShipCountry.Text = dsOrderInfo.Tables[0].Rows[0]["ShipToCountry"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["ShipToZip"].ToString() != "")
                    {
                        lblShipStateZip.Text = lblShipStateZip.Text + " " + dsOrderInfo.Tables[0].Rows[0]["ShipToZip"].ToString();
                    }

                    if (dsOrderInfo.Tables[0].Rows[0]["NumberOfItems"].ToString() != "")
                    {
                        lblNoOfItems.Text = dsOrderInfo.Tables[0].Rows[0]["NumberOfItems"].ToString();
                    }

                    if (dsOrderInfo.Tables[0].Rows[0]["PayMethod"].ToString() != "")
                    {
                        lblPaymentMethod.Text = dsOrderInfo.Tables[0].Rows[0]["PayMethod"].ToString();
                    }

                    if (dsOrderInfo.Tables[0].Rows[0]["BilledDate"].ToString() != "")
                    {
                        lblBillingDate.Text = dsOrderInfo.Tables[0].Rows[0]["BilledDate"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["TotalDiscount"].ToString() != "")
                    {
                        lblTtlDisc.Text = dsOrderInfo.Tables[0].Rows[0]["TotalDiscount"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["QuoteAmount"].ToString() != "")
                    {
                        lblQuoteAmt.Text = "$" + dsOrderInfo.Tables[0].Rows[0]["QuoteAmount"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["ReqShipMethod"].ToString() != "")
                    {
                        lblReqShipMethod.Text = dsOrderInfo.Tables[0].Rows[0]["ReqShipMethod"].ToString();
                        lblShipping.Text = dsOrderInfo.Tables[0].Rows[0]["ReqShipMethod"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["ShipCharge"].ToString() != "")
                    {
                        lblShippingCost.Text = "$" + dsOrderInfo.Tables[0].Rows[0]["ShipCharge"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["SubTotal"].ToString() != "")
                    {
                        lblSubTotal.Text = "$" + dsOrderInfo.Tables[0].Rows[0]["SubTotal"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["Total"].ToString() != "")
                    {
                        lblTotalCost.Text = "$" + dsOrderInfo.Tables[0].Rows[0]["Total"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["BillingCode"].ToString() != "")
                    {
                        lblBillingCode.Text = dsOrderInfo.Tables[0].Rows[0]["BillingCode"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["Packaging"].ToString() != "")
                    {
                        lblPackaging.Text = dsOrderInfo.Tables[0].Rows[0]["Packaging"].ToString();
                    }
                    if (dsOrderInfo.Tables[0].Rows[0]["TrackingNo"].ToString() != "")
                    {
                        lblTrackingNo.Text = dsOrderInfo.Tables[0].Rows[0]["TrackingNo"].ToString();
                    }

                }
                else
                {
                    lblErrorMsg.Text = "No Record Found";
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "FillOrderInfo");
                lblErrorMsg.Text = strErrCode;
                lblErrorMsg.Visible = true;
            }
        }
        protected void lbtnPrintInovoice_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrintInvoice.aspx?OrderId=" + ViewState["OrderId"] + "&DbName=" + ViewState["DbName"] + "&UserId=" + Session["UserID"]);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script language='javascript'>");
            sb.Append("javascript:OpenChildWindow('PrintInvoice.aspx?OrderId=" + ViewState["OrderId"] + "&DbName=" + ViewState["DbName"] + "&UserId=" + Session["UserID"] + "','width=550,height=350,resizable=yes,scrollbars=yes,top=20,left=0','Name');");

            sb.Append("</script>");

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "JavaScriptBlock", sb.ToString(), false);
        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label Price1 = (Label)e.Row.FindControl("lblEach");
                    Label lblqty1 = (Label)e.Row.FindControl("lblTotal");
                    lblqty1.Text = "$" + lblqty1.Text;
                    Price1.Text = "$" + Price1.Text;
                }
            }
            catch (Exception ex)
            {

                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvItems_RowDataBound");
                lblErrorMsg.Text = strErrCode;
                lblErrorMsg.Visible = true;
            }
        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            int addID = 0;
            try
            {
                if (ddlStatus.SelectedValue == "Processing")
                {
                    addID = (new OrderEntryDAL().InsertNotes_DAL((int)Session["UserID"], Convert.ToInt32(lblOrderNum.Text.Trim()), 2, txtComment.Text.Trim(), 1));
                    if (addID > 0)
                    {
                        addID = (new OrderEntryDAL().InsertNotes_DAL((int)Session["UserID"], Convert.ToInt32(lblOrderNum.Text.Trim()), 3, txtComment.Text.Trim(), 1));
                        if (addID > 0)
                        {
                            ctlErrormsg.heading = "Success!";
                            ctlErrormsg.msg = "Order is processing";
                            ctlErrormsg.popup();
                        }
                        else
                        {
                            ctlErrormsg.heading = "Error!";
                            ctlErrormsg.msg = "Order is not processing";
                            ctlErrormsg.popup();
                        }
                    }
                    else
                    {
                        ctlErrormsg.heading = "Error!";
                        ctlErrormsg.msg = "Order is not processing";
                        ctlErrormsg.popup();
                    }

                }
                if (ddlStatus.SelectedValue == "Shipped")
                {
                    addID = (new OrderEntryDAL().InsertShippNotes_DAL((int)Session["UserID"], Convert.ToInt32(lblOrderNum.Text.Trim()), 4, txtComment.Text.Trim(), Convert.ToInt32(ddlShippMethod.SelectedValue), txtTrackingNo.Text.Trim()));
                    if (addID > 0)
                    {
                        ctlErrormsg.heading = "Success!";
                        ctlErrormsg.msg = "Order is shipped";
                        ctlErrormsg.popup();
                    }
                    else
                    {
                        ctlErrormsg.heading = "Error!";
                        ctlErrormsg.msg = "Order is not shipped";
                        ctlErrormsg.popup();
                    }
                }

                if (ddlStatus.SelectedValue == "Cancelled")
                {
                    addID = (new OrderEntryDAL().InsertNotes_DAL((int)Session["UserID"], Convert.ToInt32(lblOrderNum.Text.Trim()), 7, txtComment.Text.Trim(), 1));
                    if (addID > 0)
                    {
                        ctlErrormsg.heading = "Success!";
                        ctlErrormsg.msg = "Order is cancelled";
                        ctlErrormsg.popup();
                    }
                    else
                    {
                        ctlErrormsg.heading = "Error!";
                        ctlErrormsg.msg = "Order is not cancelled";
                        ctlErrormsg.popup();
                    }
                }

                if (ddlStatus.SelectedValue == "Delivered")
                {
                    string PODID = (new OrderEntryDAL().InsertNotesPOD_DAL((int)Session["UserID"], 1, Convert.ToInt32(lblOrderNum.Text.Trim()), 5, txtComment.Text.Trim(), txtInHandsBy.Text.Trim(), string.Empty, txtInHandsBy.Text.Trim(), string.Empty, string.Empty, Convert.ToString(Session["UserName"]), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty));
                    if (PODID == "Order Poded Successfuly.")
                    {
                        ctlErrormsg.heading = "Success!";
                        ctlErrormsg.msg = "Order is pod";
                        ctlErrormsg.popup();
                    }
                    else
                    {
                        ctlErrormsg.heading = "Error!";
                        ctlErrormsg.msg = "Order is not pod";
                        ctlErrormsg.popup();
                    }
                }
                Bind((int)ViewState["OrderId"]);
            }
            catch (Exception ex)
            {

                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lbtnUpdate_Click");
                lblErrorMsg.Text = strErrCode;
                lblErrorMsg.Visible = true;
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
