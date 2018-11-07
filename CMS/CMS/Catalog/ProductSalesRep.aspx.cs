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
    public partial class ProductSalesRep : System.Web.UI.Page
    {
        string strProdID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                strProdID = Request.QueryString["id"].ToString();
                ErrorMsg.Visible = false;
                ErrorMsg.Text = string.Empty;
                if (!IsPostBack)
                {
                    if (strProdID != string.Empty)
                    {
                        //BindPageUI();
                        BindImage(strProdID, "0", "0");
                        BindProductDetails(strProdID);
                        BindSize(strProdID);
                        BindColor(strProdID, "0");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        private void BindSize(string ProductID)
        {
            try
            {
                DataSet getProductSize = (new AddCartsDAL()).GetProductSize(Convert.ToInt32(ProductID));
                if (getProductSize != null)
                {
                    ddlSize.DataSource = getProductSize.Tables[0];
                    ddlSize.DataTextField = "Size_Name";
                    ddlSize.DataValueField = "Size_ID";
                    ddlSize.DataBind();
                }
                else
                {
                    ddlSize.DataSource = null;
                    ddlSize.DataBind();
                }
                ddlSize.Items.Insert(0, new ListItem("Select Size", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void BindColor(string ProductID, string SizeID)
        {
            try
            {
                DataSet getProductColor = (new AddCartsDAL()).GetProductColor(Convert.ToInt32(ProductID), Convert.ToInt32(SizeID));
                if (getProductColor != null)
                {
                    ddlColor.DataSource = getProductColor.Tables[0];
                    ddlColor.DataTextField = "Color_Name";
                    ddlColor.DataValueField = "Color_Id";
                    ddlColor.DataBind();

                }
                else
                {
                    ddlColor.DataSource = null;
                    ddlColor.DataBind();
                }
                ddlColor.Items.Insert(0, new ListItem("Select Color", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void BindImage(string ProductID, string ColorID, string SizeID)
        {
            try
            {
                DataSet getProductImg = (new AddCartsDAL()).GetProductImage(Convert.ToInt32(ProductID), Convert.ToInt32(ColorID), Convert.ToInt32(SizeID), 0);
                if (getProductImg.Tables[0].Rows.Count > 0)
                {
                    ProductsDetails.DataSource = getProductImg.Tables[0];
                    ProductsDetails.DataBind();
                }
                else
                {
                    ProductsDetails.DataSource = null;
                    ProductsDetails.DataBind();
                }

                rptProductImg.DataSource = getProductImg.Tables[1];
                rptProductImg.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindProductDetails(string ProductID)
        {
            try
            {


                DataSet getProductDetails = (new AddCartsDAL()).GetProductDetails(Convert.ToInt32(ProductID));
                if (getProductDetails.Tables[0].Rows.Count > 0)
                {
                    lblProducerName.Text = getProductDetails.Tables[0].Rows[0]["ProducerName"].ToString();
                    lblProductDesc.Text = getProductDetails.Tables[0].Rows[0]["ProductName"].ToString();
                    lblProductName.Text = getProductDetails.Tables[0].Rows[0]["part_num"].ToString();
                    lblPrice.Text = getProductDetails.Tables[0].Rows[0]["Price"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindImage(strProdID, ddlColor.SelectedItem.Value,ddlSize.SelectedItem.Value);
        }

        protected void ddlSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindColor(strProdID, ddlSize.SelectedItem.Value);
        }

        protected void rptProductImg_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                string strImgId = e.CommandArgument.ToString();

                DataSet getProductImg = (new AddCartsDAL()).GetProductImage(Convert.ToInt32(strProdID), 0, 0, Convert.ToInt32(strImgId));

                if (getProductImg.Tables[2].Rows.Count > 0)
                {
                    ProductsDetails.DataSource = getProductImg.Tables[2];
                    ProductsDetails.DataBind();
                }
                else
                {
                    ProductsDetails.DataSource = null;
                    ProductsDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void lnkbAddtoCart_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDftQty.Text.Trim() != string.Empty)
                {

                    if (IsNumeric(txtDftQty.Text.Trim()))
                    {
                        DataSet chkQty = (new AddCartsDAL()).ChkAvaQty(Convert.ToInt32(strProdID), Convert.ToInt32(ddlColor.SelectedItem.Value), Convert.ToInt32(ddlSize.SelectedItem.Value));

                        if (chkQty.Tables[0].Rows.Count > 0)
                        {
                            string strAvaQty = chkQty.Tables[0].Rows[0]["AvlQty"].ToString();

                            DataSet DftwhNum = (new AddCartsDAL()).GetDefaultCMSWarehouses();
                            string strwhse = DftwhNum.Tables[0].Rows[0]["Whse_Id"].ToString();

                            if (Convert.ToInt32(strAvaQty) > 0)
                            {
                                int iInterQty = Convert.ToInt16(txtDftQty.Text.Trim());

                                if (iInterQty <= Convert.ToInt32(strAvaQty))
                                {

                                    string strParName = lblProductName.Text;

                                    AddTempParts(strParName, iInterQty, strwhse, Convert.ToInt32(ddlColor.SelectedItem.Value), Convert.ToInt32(ddlSize.SelectedItem.Value));

                                    Response.Redirect("CartAdd.aspx", false);
                                    Session["SubMenu"] = "View Cart";
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
                                ErrorMsg.Text = "Sorry, the style you selected is not currently available. Please try another combination.";
                            }
                        }
                        else
                        {
                            ErrorMsg.Visible = true;
                            ErrorMsg.Text = "Sorry, the style you selected is not currently available. Please try another combination.";
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
            catch (Exception ex)
            {
                ErrorMsg.Visible = true;
                ErrorMsg.Text = ex.ToString();
            }
        }

   private void AddTempParts(string PartName, int strAvaQty, string strWhsenum,int ColorID,int SizeID)
   {
       try
       {

           int x = (new AddCartsDAL()).AddTempParts_DAL(Session.SessionID, PartName, strAvaQty, Session["UserID"].ToString(), strWhsenum,ColorID,SizeID);
       }
       catch (Exception ex)
       {
           throw ex;
       }
   }

   protected void lnkbViewCart_Click(object sender, EventArgs e)
   {
       Response.Redirect("CartAdd.aspx", false);
       Session["SubMenu"] = "View Cart";
   }

   public static bool IsNumeric(string value)
   {
       foreach (char c in value)
           if (!((Int16)c > 47 && (Int16)c < 58)) return false;
       return true;
   }

      

       
    }
}