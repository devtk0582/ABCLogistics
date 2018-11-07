using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.DAL;

namespace CMS.Catalog
{
    public partial class CartAdd : System.Web.UI.Page
    {
        decimal SubTotal = 0;
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindTempPartGrid();
                }
                ErrorMsg.Visible = false;
                ErrorMsg.Text = string.Empty;
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Page_Load");
                ErrorMsg.Text = strErrCode;
                ErrorMsg.Visible = true;
            }
            
        }

       

        public void BindTempPartGrid()
        {
            try
            {
                DataSet TempData = (new AddCartsDAL()).GetTempParts(Session.SessionID, "O");

                if (TempData.Tables[0].Rows.Count > 0)
                {
                    gvCart.DataSource = TempData.Tables[0];
                    gvCart.DataBind();

                    tblEmptyCart.Visible = false;
                    tblAddCart.Visible = true;
                }
                else
                {
                    tblEmptyCart.Visible = true;
                    tblAddCart.Visible = false;
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindTempPartGrid");
                ErrorMsg.Text = strErrCode;
                ErrorMsg.Visible = true;
            }

        }

        protected void gvCart_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCart.PageIndex = e.NewPageIndex;
                BindTempPartGrid();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindTempPartGrid");
                ErrorMsg.Text = strErrCode;
                ErrorMsg.Visible = true;
            }
        }


        protected void gvCart_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Label CurTotal = ((Label)(e.Row.FindControl("lblPrice2")));
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtQty = ((TextBox)(e.Row.FindControl("txtQty")));
                    ViewState["strQty"] = txtQty.Text.Trim();

                    txtQty.Attributes.Add("onchange", "javascript:VisibleBtn('" + lnkQtyUpdate.ClientID + "');");

                    Label lblPrice = ((Label)(e.Row.FindControl("lblPrice")));
                    Label CurTotal = ((Label)(e.Row.FindControl("lblPrice2")));

                    decimal Total;
                    if (ViewState["strInterQty"] == null)
                    {
                        Total = (Convert.ToInt32(txtQty.Text.Trim())) * (Convert.ToDecimal(lblPrice.Text.Trim()));
                    }
                    else
                    {
                        Total = (Convert.ToInt32(ViewState["strInterQty"].ToString())) * (Convert.ToDecimal(lblPrice.Text.Trim()));
                        txtQty.Text = ViewState["strInterQty"].ToString();
                    }
                    CurTotal.Text = Convert.ToString(Total);

                    SubTotal = SubTotal + Total;

                    //Label lblPartNum = ((Label)(e.Row.FindControl("lblManufacturer")));
                    //Label lblTotalQty = ((Label)(e.Row.FindControl("lblTQty")));
                    //ViewState["strpart_num"] = lblPartNum.Text.Trim();
                    //ViewState["strTotalQty"] = lblTotalQty.Text.Trim();



                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblSubTotal = ((Label)(e.Row.FindControl("lblFoterAmt")));

                    lblSubTotal.Text = Convert.ToString(SubTotal);
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvGrid_RowDataBound");
                ErrorMsg.Text = strErrCode;
                ErrorMsg.Visible = true;
            }
 
        }

        protected void lnlchkOutNow_Click(object sender, EventArgs e)
        {
            try
            {
                int x = (new AddCartsDAL()).InsertChkoutParts(Session.SessionID, Convert.ToInt32(Session["UserID"].ToString()));

                Response.Redirect("CheckOut.aspx", false);
                Session["SubMenu"] = "Check Out";
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lnlchkOutNow_Click");
                ErrorMsg.Text = strErrCode;
                ErrorMsg.Visible = true;
            }
        }

        protected void lnkEmptyCart_Click(object sender, EventArgs e)
        {
            ctlEmptyCart.popup();
           // RemoveTempParts();
        }

        //protected void ctlEmptyCart_OnEmptyCartBG(object sender, EventArgs e)
        //{
        //    BindTempPartGrid();

        //    tblEmptyCart.Visible = true;
        //    tblAddCart.Visible = false;
        //    Response.Redirect("CartAdd.aspx", false);
        //}
 


        //private void RemoveTempParts()
        //{
        //    int iPOID=0;
        //    try
        //    {
        //        int x = (new AddCartsDAL()).DeleteTempPartsDocuments(Session.SessionID, iPOID);
        //        BindTempPartGrid();
               
        //    }
        //    catch (Exception ex)
        //    {
        //         throw ex;
        //    }
            
        //}

        protected void lnkQtyUpdate_Click(object sender, EventArgs e)
        { 
            try
            {
                foreach (GridViewRow gr in gvCart.Rows)
                {
                    TextBox txtInerQty = (TextBox)gr.Cells[0].FindControl("txtQty");
                    //ViewState["strInterQty"] = txtInerQty.Text.Trim();
                    if (txtInerQty.Text.Trim() != string.Empty)
                    {
                        if (IsNumeric(txtInerQty.Text.Trim()))
                        {
                            Label lblOldQty = (Label)gr.Cells[0].FindControl("lblTQty");

                            Label lblPartNum = (Label)gr.Cells[0].FindControl("lblManufacturer");
                            Label lblProdID = (Label)gr.Cells[0].FindControl("lblProdID");
                            Label lblColorID = (Label)gr.Cells[0].FindControl("lblColorID");
                            Label lblSizeD = (Label)gr.Cells[0].FindControl("lblSizeID");



                            int iNewQty = 0;
                            int iOldQty = 0;
                            iNewQty = Convert.ToInt32(txtInerQty.Text.Trim());
                            iOldQty = Convert.ToInt32(lblOldQty.Text.Trim());

                            if (iOldQty == iNewQty)
                            {
                            }
                            else
                            {
                                if (iOldQty > iNewQty)
                                {
                                    int Qty = iOldQty - iNewQty;

                                    int x = (new AddCartsDAL()).RemoveTempParts(Session.SessionID, lblPartNum.Text.Trim(), Qty, Session["UserID"].ToString(), Convert.ToInt32(lblColorID.Text.Trim()), Convert.ToInt32(lblSizeD.Text.Trim()));
                                }
                                else
                                {
                                    if (iOldQty < iNewQty)
                                    {
                                        int Qty = iNewQty - iOldQty;
                                        DataSet chkQty = (new AddCartsDAL()).ChkAvaQty(Convert.ToInt32(lblProdID.Text.Trim()), Convert.ToInt32(lblColorID.Text.Trim()), Convert.ToInt32(lblSizeD.Text.Trim()));

                                        if (chkQty.Tables[0].Rows.Count > 0)
                                        {

                                            string strAvaQty = chkQty.Tables[0].Rows[0]["AvlQty"].ToString();
                                            DataSet DftwhNum = (new AddCartsDAL()).GetDefaultCMSWarehouses();
                                            string strwhse = DftwhNum.Tables[0].Rows[0]["Whse_Id"].ToString();
                                            if (Qty <= Convert.ToInt32(strAvaQty))
                                            {
                                                AddTempParts(lblPartNum.Text.Trim(), Qty, strwhse, Convert.ToInt32(lblColorID.Text.Trim()), Convert.ToInt32(lblSizeD.Text.Trim()));
                                            }
                                            else
                                            {
                                                ErrorMsg.Visible = true;
                                                ErrorMsg.Text = "Sorry, Enter Quantity is not currently available. Please try with less Quantity.";
                                            }
                                        }
                                        else
                                        {
                                            ErrorMsg.Visible = true;
                                            ErrorMsg.Text = "Sorry, Enter Quantity is not currently available. Please try with less Quantity.";
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            ErrorMsg.Visible = true;
                            ErrorMsg.Text = "Please Enter Valid Quantity.";

                        }
                    }
                    else
                    {
                        ErrorMsg.Visible = true;
                        ErrorMsg.Text = "Please Enter Valid Quantity.";
                    }
                }

                BindTempPartGrid();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lnlchkOutNow_Click");
                ErrorMsg.Text = strErrCode;
                ErrorMsg.Visible = true;
            }
            
        }

        private void AddTempParts(string PartName, int strAvaQty, string strWhsenum,int ColorID, int SizeID)
        {
            try
            {

                int x = (new AddCartsDAL()).AddTempParts_DAL(Session.SessionID, PartName, strAvaQty, Session["UserID"].ToString(), strWhsenum, ColorID, SizeID);
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "AddTempParts");
                ErrorMsg.Text = strErrCode;
                ErrorMsg.Visible = true;
            }
            
        }

        protected void lnkEmptCarbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Products.aspx", false);
            Session["SubMenu"] = "Products";
        }
        public static bool IsNumeric(string value)
        {
            foreach (char c in value)
                if (!((Int16)c > 47 && (Int16)c < 58)) return false;
            return true;
        }

 
    }
}