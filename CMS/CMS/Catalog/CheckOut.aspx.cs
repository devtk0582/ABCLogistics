using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using CMS.DAL;
using CMS.UPSShipWebReference;
using System.IO;
using iTextSharp.text.pdf;

namespace CMS.Catalog
{
    public partial class CheckOut : System.Web.UI.Page
    {
        decimal TPrice = 0;
        decimal TPrice1 = 0;
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        #region "Form event"
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblvaliderror.Visible = false;

                if (!(IsPostBack))
                {
                    tr_CheckOut.Visible = true;
                    BindBillingCodes();
                    BindPackagings();
                    BindShippingMethods();
                    ddlBillingCode.SelectedIndex = 1;
                    ddlpackaging.SelectedIndex = 1;
                    ddlShippingMethod.SelectedIndex = 1;
                    GetViewCartDetail();
                    GetAddressBook();
                    FillCountry(0);
                    LoadUsersInfo(Convert.ToInt32(Session["UserID"]));
                    Session["Cart"] = "CheckOut";
                    lnkNext.Text = "Continue";
                    lnkBack.Visible = false;
                    // rblEnclose.SelectedValue = "S";
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Page_Load");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }
            // ACECompany.ContextKey = "Costco";
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

        }

        protected void gv_ViewCart_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label Price1 = (Label)e.Row.FindControl("lblEach1");
                    Label lblqty1 = (Label)e.Row.FindControl("lblqty1");
                    Label lblList_price1 = (Label)e.Row.FindControl("lblList_price1");
                    lblList_price1.Text = Convert.ToString(Convert.ToDecimal(Price1.Text) * Convert.ToDecimal(lblqty1.Text));
                    TPrice = TPrice + Convert.ToDecimal(lblList_price1.Text);
                    lblList_price1.Text = "$" + lblList_price1.Text;
                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label TotalPrice1 = (Label)e.Row.FindControl("lblTotalAmount1");
                    TotalPrice1.Text = "$" + TPrice;
                }
                Session["Total"] = TPrice;
            }
            catch (Exception ex)
            {

                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gv_ViewCart_RowDataBound");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }

        }

        protected void ddlAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAddresses(Convert.ToInt32(ddlAddress.SelectedValue));

            }
            catch (Exception ex)
            {

                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "ddlAddress_SelectedIndexChanged");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }

        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCountry.SelectedValue != "-1")
                {
                    FillState(Convert.ToInt32(ddlCountry.SelectedValue));
                }
            }
            catch (Exception ex)
            {

                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "ddlCountry_SelectedIndexChanged");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }


        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlState.SelectedValue != "-1")
                {
                    FillCity(Convert.ToInt16(ddlState.SelectedValue));
                }
            }
            catch (Exception ex)
            {

                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "ddlState_SelectedIndexChanged");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }

        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (lnkNext.Text == "Confirm Order")
                {
                    string x = (new OrderEntryDAL()).PlaceOrder_DAL(Session.SessionID, Convert.ToInt32(Session["UserID"].ToString()), txtInHandsBy.Text, txtNotes.Text, Session["UserName"].ToString(), txtCompany.Text, txtStreetAdd1.Text, ddlCity.SelectedItem.Text, ddlState.SelectedItem.Text, ddlCountry.SelectedItem.Text, txtAdd2.Text, txtZip.Text, txtPhone.Text.Trim(), string.Empty, string.Empty, txtEmail.Text.Trim(), string.Empty);
                    //string x = "108";
                    if (x != "")
                    {
                        AddGiftCartDetail(x);
                        if (InsertOrderShipment(x) == true)
                        {
                            ctlPrepareShipment.popup(x);
                            //labelNumber = PrintLabel(x);
                        }

                        //if (labelNumber == "")
                        //    return;
                        
                        //ctlEmptyCart.popup(x, labelNumber);
                    }

                }
                if (Session["Cart"].ToString() == "PickShip")
                {
                    DateTime dt = new DateTime();
                    dt = System.DateTime.Today;

                    if (Convert.ToDateTime(txtInHandsBy.Text.Trim()) < dt)
                    {
                        lblErrorHandby.Visible = true;
                        lblErrorHandby.Text = "In hand by date not less then today date";

                    }
                    else
                    {
                        lblErrorHandby.Visible = false;
                        tr_CheckOut.Visible = false;
                        tr_PickShip.Visible = false;
                        tr_Confirm.Visible = true;
                        lnkNext.Text = "Confirm Order";
                        lnkBack.Visible = true;
                        Session["Cart"] = "Confirm";

                        lblInHandBy1.Text = "In hand by:" + txtInHandsBy.Text.Trim();
                        lblpackaging1.Text = "Packaging:" + ddlpackaging.SelectedItem.ToString();
                        if (rblEnclose.SelectedIndex != -1)
                        {
                            lblEnclosure1.Text = "Enclosure:" + rblEnclose.SelectedItem.ToString();
                        }

                        lblamount1.Text = "The total amount to be billed is $" + Session["Total"].ToString();
                        string dbName = System.Configuration.ConfigurationSettings.AppSettings["dbname"].ToString();
                        GetDatabases(0, dbName);

                        lblShippingAddress.Text = txtCompany.Text + "<br>" + txtStreetAdd1.Text + "<br>" + txtAdd2.Text + "<br>" + ddlCity.SelectedItem.Text + " " + ddlState.SelectedItem.Text + " " + ddlCountry.SelectedItem.Text + "<br>" + txtZip.Text;
                    }

                }
                if (Session["Cart"].ToString() == "CheckOut")
                {
                    lblErrorHandby.Visible = false;
                    if (validationCheckOut())
                    {
                        tr_CheckOut.Visible = false;
                        tr_PickShip.Visible = true;
                        tr_Confirm.Visible = false;
                        lnkNext.Text = "Continue";
                        lnkBack.Visible = true;
                        txtInHandsBy.Text = Convert.ToString(String.Format("{0:M/d/yyyy}", System.DateTime.Now));

                        //---- code for address insert

                        string addID = (new AddCartsDAL()).InsertAddress_DAL(txtCompany.Text.Trim(), txtStreetAdd1.Text.Trim(), txtAdd2.Text.Trim(), Convert.ToInt32(ddlCountry.SelectedItem.Value), Convert.ToInt32(ddlState.SelectedItem.Value), Convert.ToInt32(ddlCity.SelectedItem.Value), txtZip.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim());
                        Session["Cart"] = "PickShip";
                        if (addID != "")
                        {
                            GetAddressBook();
                            ddlAddress.SelectedValue = addID;
                        }

                    }

                }
            }
            catch (Exception ex)
            {

                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lnkNext_Click");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }


        }

        protected void cbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblEnclose.SelectedValue == "C")
                {
                    trGift.Visible = true;
                    txtDear.Text = string.Empty;
                    txtGiftcard.Text = string.Empty;
                    txtSincerely.Text = string.Empty;
                    txtTitle.Text = string.Empty;
                    txtYourName.Text = string.Empty;

                }
                else
                {
                    trGift.Visible = false;
                }
            }
            catch (Exception ex)
            {

                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "cbl_SelectedIndexChanged");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }

        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["Cart"].ToString() == "PickShip")
                {
                    tr_CheckOut.Visible = true;
                    tr_PickShip.Visible = false;
                    tr_Confirm.Visible = false;
                    lnkNext.Text = "Continue";
                    lnkBack.Visible = false;
                    Session["Cart"] = "CheckOut";


                }
                if (Session["Cart"].ToString() == "Confirm")
                {
                    tr_CheckOut.Visible = false;
                    tr_PickShip.Visible = true;
                    tr_Confirm.Visible = false;
                    lnkNext.Text = "Continue";
                    lnkBack.Visible = true;
                    Session["Cart"] = "PickShip";

                }
            }
            catch (Exception ex)
            {

                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lnkBack_Click");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }

        }

        protected void gv_ViewCartCheckout_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label Price2 = (Label)e.Row.FindControl("lblEach2");
                    Label lblqty2 = (Label)e.Row.FindControl("lblqty2");
                    Label lblList_price2 = (Label)e.Row.FindControl("lblList_price2");
                    lblList_price2.Text = Convert.ToString(Convert.ToDecimal(Price2.Text) * Convert.ToDecimal(lblqty2.Text));
                    TPrice1 = TPrice1 + Convert.ToDecimal(lblList_price2.Text);

                    lblList_price2.Text = "$" + lblList_price2.Text;

                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label TotalPrice2 = (Label)e.Row.FindControl("lblTotalAmount2");
                    TotalPrice2.Text = "$" + TPrice1;
                }
            }
            catch (Exception ex)
            {

                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gv_ViewCartCheckout_RowDataBound");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }

        }

        #endregion



        #region "Function"

        public void GetViewCartDetail()
        {
            try
            {
                DataSet dsViewCartDetail = (new AddCartsDAL()).GetViewCartDetail_DAL(Session.SessionID, Convert.ToInt32(Session["UserID"]));
                if (dsViewCartDetail.Tables[0].Rows.Count > 0)
                {
                    tr_CheckOut.Visible = true;
                    tblEmptyCart.Visible = false;
                    lnkNext.Visible = true;
                    gv_ViewCart.DataSource = dsViewCartDetail.Tables[0];
                    gv_ViewCart.DataBind();
                    gv_ViewCartCheckout.DataSource = dsViewCartDetail.Tables[0];
                    gv_ViewCartCheckout.DataBind();
                }
                else
                {
                    tr_CheckOut.Visible = false;
                    tblEmptyCart.Visible = true;
                    lnkNext.Visible = false;
                    gv_ViewCart.DataSource = null;
                    gv_ViewCart.DataBind();
                    gv_ViewCartCheckout.DataSource = null;
                    gv_ViewCartCheckout.DataBind();
                }
            }
            catch (Exception ex)
            {

                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetViewCartDetail");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }

        }

        public void GetAddressBook()
        {
            try
            {
                DataSet dsViewCartDetail = (new SettingsDAL()).GetAddressBook_DAL(0, string.Empty, 1, 10000);

                ddlAddress.DataSource = dsViewCartDetail.Tables[0];
                ddlAddress.DataTextField = "company_name";
                ddlAddress.DataValueField = "address_id";
                ddlAddress.DataBind();
                ddlAddress.Items.Insert(0, new ListItem("Please choose a shipping address", "-1"));
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetAddressBook");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }

        }

        public void LoadAddresses(int iAddressID)
        {
            try
            {

                DataSet dsAddresses = (new SettingsDAL()).GetAddressBook_DAL(iAddressID, string.Empty, 1, 10000);

                foreach (DataRow dr in dsAddresses.Tables[0].Rows)
                {
                    // lblAddID.Text = iAddressID.ToString();

                    if (!DBNull.Value.Equals(dr["company_name"]) & (dr["company_name"] != null))
                    {
                        txtCompany.Text = Convert.ToString(dr["company_name"]);
                    }

                    if (!DBNull.Value.Equals(dr["addr"]) & (dr["addr"] != null))
                    {
                        txtStreetAdd1.Text = Convert.ToString(dr["addr"].ToString());
                    }
                    if (!DBNull.Value.Equals(dr["Country_id"]))
                    {
                        if ((ddlCountry.Items.FindByValue(dr["Country_id"].ToString()) != null))
                        {
                            ddlState.Items.Clear();
                            ddlCountry.SelectedValue = dr["Country_id"].ToString();
                            FillState(Convert.ToInt32(ddlCountry.SelectedValue));
                        }
                        else
                        {
                            ddlCountry.SelectedValue = "-1";
                        }
                    }
                    else
                    {
                        ddlCountry.SelectedValue = "-1";
                    }
                    if (!DBNull.Value.Equals(dr["State_Id"]))
                    {
                        if ((ddlState.Items.FindByValue(dr["State_Id"].ToString()) != null))
                        {
                            ddlState.SelectedValue = dr["State_Id"].ToString();
                            FillCity(Convert.ToInt32(ddlState.SelectedValue));
                        }
                        else
                        {
                            ddlState.SelectedValue = "-1";
                        }
                    }
                    else
                    {
                        ddlState.SelectedValue = "-1";
                    }
                    if (!DBNull.Value.Equals(dr["City_Id"]))
                    {
                        if ((ddlCity.Items.FindByValue(dr["City_Id"].ToString()) != null))
                        {
                            ddlCity.SelectedValue = dr["City_Id"].ToString();
                        }
                        else
                        {
                            ddlCity.SelectedValue = "-1";
                        }
                    }
                    else
                    {
                        ddlCity.SelectedValue = "-1";
                    }
                    if (!DBNull.Value.Equals(dr["postal_code"]) & (dr["postal_code"] != null))
                    {
                        txtZip.Text = Convert.ToString(dr["postal_code"].ToString());
                    }
                    if (!DBNull.Value.Equals(dr["phone"]) & (dr["phone"] != null))
                    {
                        txtPhone.Text = Convert.ToString(dr["phone"].ToString());
                    }
                    if (!DBNull.Value.Equals(dr["customer_email"]) & (dr["customer_email"] != null))
                    {
                        txtEmail.Text = Convert.ToString(dr["customer_email"].ToString());
                    }
                    if (!DBNull.Value.Equals(dr["addr2"]) & (dr["addr2"] != null))
                    {
                        txtAdd2.Text = Convert.ToString(dr["addr2"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "LoadAddresses");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }
        }

        public void FillCountry(int iCountryId)
        {
            DataSet dsCountry = null;
            try
            {
                dsCountry = (new SettingsDAL()).GetCountry_DAL(iCountryId);
                ddlCountry.Items.Clear();
                if (dsCountry != null)
                {
                    if (dsCountry.Tables[0].Rows.Count > 0)
                    {
                        ddlCountry.DataSource = dsCountry.Tables[0];
                        ddlCountry.DataValueField = "CountryId";
                        ddlCountry.DataTextField = "CountryName";
                        ddlCountry.DataBind();
                    }
                    ddlCountry.Items.Insert(0, new ListItem("Select", "-1"));
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "FillCountry");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }
        }

        protected void FillState(int iContryid)
        {
            DataSet dsState = null;
            try
            {
                dsState = (new SettingsDAL()).GetState_DAL(iContryid);
                ddlState.Items.Clear();
                if (dsState != null)
                {
                    if (dsState.Tables[0].Rows.Count > 0)
                    {
                        ddlState.DataSource = dsState.Tables[0];
                        ddlState.DataValueField = "StateId";
                        ddlState.DataTextField = "State";
                        ddlState.DataBind();
                    }
                    ddlState.Items.Insert(0, new ListItem("Select", "-1"));
                }
            }

            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "FillState");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }

        }

        protected void FillCity(int iStateId)
        {
            DataSet dsCity = null;
            try
            {
                dsCity = (new SettingsDAL()).GetCity_DAL(iStateId);
                ddlCity.Items.Clear();
                if (dsCity != null)
                {
                    if (dsCity.Tables[0].Rows.Count > 0)
                    {
                        ddlCity.DataSource = dsCity.Tables[0];
                        ddlCity.DataValueField = "CityId";
                        ddlCity.DataTextField = "City";
                        ddlCity.DataBind();
                    }
                    ddlCity.Items.Insert(0, new ListItem("Select", "-1"));
                }

            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "FillCity");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }
        }

        public void LoadUsersInfo(int iUserID)
        {
            DataTable dtRoles;
            DataRow drRoles;
            try
            {
                dtRoles = new DataTable("Roles");
                dtRoles.Columns.Add("RolesName");
                drRoles = dtRoles.NewRow();
                drRoles[0] = System.Configuration.ConfigurationSettings.AppSettings["EComAdmin"].ToString();
                dtRoles.Rows.Add(drRoles);
                drRoles = dtRoles.NewRow();
                drRoles[0] = System.Configuration.ConfigurationSettings.AppSettings["EComUser"].ToString();
                dtRoles.Rows.Add(drRoles);
                System.Text.StringBuilder roles = new System.Text.StringBuilder(2000);
                System.IO.StringWriter sroles = new System.IO.StringWriter(roles);
                foreach (DataColumn col in dtRoles.Columns)
                {
                    col.ColumnMapping = MappingType.Attribute;
                }
                dtRoles.WriteXml(sroles, System.Data.XmlWriteMode.WriteSchema);
                DataSet dsusers = (new UsersDAL()).GetUsersList(iUserID, roles.ToString());

                if (dsusers != null)
                {
                    if (dsusers.Tables[0].Rows.Count > 0)
                    {
                        if (dsusers.Tables[0].Rows[0]["user_first"].ToString() != string.Empty)
                        {
                            lblName.Text = dsusers.Tables[0].Rows[0]["user_first"].ToString() + " " + dsusers.Tables[0].Rows[0]["user_last"].ToString();
                        }

                        if (dsusers.Tables[0].Rows[0]["user_email"].ToString() != string.Empty)
                        {
                            lblEmail.Text = dsusers.Tables[0].Rows[0]["user_email"].ToString();
                        }

                        if (dsusers.Tables[0].Rows[0]["phone1"].ToString() != string.Empty)
                        {
                            lblPhone.Text = dsusers.Tables[0].Rows[0]["phone1"].ToString();
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "LoadUsersInfo");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }
        }

        private void BindPackagings()
        {
            try
            {
                DataSet packagingList = (new SettingsDAL()).GetPackagings(1);
                if (packagingList != null && packagingList.Tables[0].Rows.Count > 0)
                {
                    ddlpackaging.DataSource = packagingList;
                    ddlpackaging.DataTextField = "Package_Name";
                    ddlpackaging.DataValueField = "Packaging_ID";
                    ddlpackaging.DataBind();
                    ddlpackaging.Items.Insert(0, new ListItem("Please choose packaging for this order", "-1"));
                }
                else
                {
                    ddlpackaging.DataSource = null;
                    ddlpackaging.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindPackagings");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }
        }

        private void BindShippingMethods()
        {
            try
            {
                DataSet shippingMethodList = (new SettingsDAL()).GetShippingServices();
                if (shippingMethodList != null && shippingMethodList.Tables[0].Rows.Count > 0)
                {
                    ddlShippingMethod.DataSource = shippingMethodList;
                    ddlShippingMethod.DataTextField = "ServiceName";
                    ddlShippingMethod.DataValueField = "ServiceID";
                    ddlShippingMethod.DataBind();
                    ddlShippingMethod.Items.Insert(0, new ListItem("Please choose shipping method", "-1"));
                }
                else
                {
                    ddlShippingMethod.DataSource = null;
                    ddlShippingMethod.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindShippingMethods");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }
        }

        private void BindBillingCodes()
        {
            try
            {
                DataSet billingCodeList = (new SettingsDAL()).GetBillingCodes(1);
                if (billingCodeList != null && billingCodeList.Tables[0].Rows.Count > 0)
                {
                    ddlBillingCode.DataSource = billingCodeList;
                    ddlBillingCode.DataTextField = "BillingCode_Desc";
                    ddlBillingCode.DataValueField = "BillingCode_ID";
                    ddlBillingCode.DataBind();
                    ddlBillingCode.Items.Insert(0, new ListItem("Please choose billing codes", "-1"));
                }
                else
                {
                    ddlBillingCode.DataSource = null;

                    ddlBillingCode.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindBillingCodes");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }
        }

        public void AddGiftCartDetail(string strOrderId)
        {
            try
            {
                string strAddress = string.Empty;
                string strEnclose = string.Empty;
                if (rblEnclose.SelectedIndex != -1)
                {
                    strEnclose = Convert.ToString(rblEnclose.SelectedValue);
                }

                else
                {
                    strEnclose = string.Empty;
                }
                strAddress = txtCompany.Text.Trim() + "," + txtStreetAdd1.Text.Trim() + "," + txtAdd2.Text.Trim() + "," + ddlCity.SelectedItem.Text + "," + ddlState.SelectedItem.Text + "," + ddlCountry.SelectedItem.Text + "," + txtZip.Text.Trim() + "," + txtEmail.Text.Trim() + "," + txtPhone.Text.Trim();
                string y = (new OrderEntryDAL()).AddGiftCartDetail_DAL(Session.SessionID, strAddress, txtNotes.Text.Trim(), Convert.ToInt32(ddlBillingCode.SelectedValue), Convert.ToInt32(ddlShippingMethod.SelectedValue), txtInHandsBy.Text.Trim(), txtDear.Text.Trim(), txtGiftcard.Text.Trim(), txtSincerely.Text.Trim(), txtYourName.Text.Trim(), txtTitle.Text.Trim(), Convert.ToInt32(ddlpackaging.SelectedValue), Convert.ToInt32(strOrderId), strEnclose);
                if (y != "1")
                {
                    //
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "AddGiftCartDetail");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }
        }

        public Boolean validationCheckOut()
        {
            lblvaliderror.Visible = false;
            try
            {
                Boolean result = true;
                if (ddlAddress.SelectedValue == "-1")
                {
                    lblvaliderror.Visible = true;
                    lblvaliderror.Text = "Please select a shipping address ";
                    result = false;
                }
                if (txtEmail.Text.Trim() != "")
                {
                    if (isEmail(txtEmail.Text.Trim()) == false)
                    {
                        lblvaliderror.Visible = true;
                        lblvaliderror.Text = "Invalid Email.";
                        result = false;
                    }
                }

                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static bool isEmail(string inputEmail)
        {
            //inputEmail = NulltoString(inputEmail);
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }



        public Boolean validationPickShip()
        {
            try
            {
                Boolean result = false;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Boolean validationConfirm()
        {
            try
            {
                Boolean result = false;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GetDatabases(int iDatabaseId, string strDatabaseName)
        {
            DataSet dsLogin = new DataSet();
            string strAdd = string.Empty;
            try
            {

                dsLogin = (new OrderEntryDAL()).GetGTEDatabasesSearch_DAL(0, strDatabaseName, Convert.ToString(Session["UserID"]), 1);

                if ((dsLogin.Tables[0].Rows.Count > 0))
                {

                    if ((!object.ReferenceEquals(dsLogin.Tables[0].Rows[0]["Address1"], DBNull.Value)) && (!object.ReferenceEquals(dsLogin.Tables[0].Rows[0]["Address1"], string.Empty)))
                    {
                        strAdd = strAdd + dsLogin.Tables[0].Rows[0]["Address1"].ToString().Trim() + ",</BR>";
                    }
                    if ((!object.ReferenceEquals(dsLogin.Tables[0].Rows[0]["Address2"], DBNull.Value)) && (!object.ReferenceEquals(dsLogin.Tables[0].Rows[0]["Address2"], string.Empty)))
                    {
                        strAdd = strAdd + dsLogin.Tables[0].Rows[0]["Address2"].ToString().Trim() + ",</BR>";
                    }
                    if ((!object.ReferenceEquals(dsLogin.Tables[0].Rows[0]["City"], DBNull.Value)) && (!object.ReferenceEquals(dsLogin.Tables[0].Rows[0]["City"], string.Empty)))
                    {
                        strAdd = strAdd + dsLogin.Tables[0].Rows[0]["City"].ToString().Trim() + ", ";
                    }
                    if ((!object.ReferenceEquals(dsLogin.Tables[0].Rows[0]["State"], DBNull.Value)) && (!object.ReferenceEquals(dsLogin.Tables[0].Rows[0]["State"], string.Empty)))
                    {
                        strAdd = strAdd + dsLogin.Tables[0].Rows[0]["State"].ToString().Trim() + ",</BR> ";
                    }
                    if ((!object.ReferenceEquals(dsLogin.Tables[0].Rows[0]["CountryName"], DBNull.Value)) && (!object.ReferenceEquals(dsLogin.Tables[0].Rows[0]["CountryName"], string.Empty)))
                    {
                        strAdd = strAdd + dsLogin.Tables[0].Rows[0]["CountryName"].ToString().Trim() + ",</BR> ";
                    }
                    if ((!object.ReferenceEquals(dsLogin.Tables[0].Rows[0]["Zip"], DBNull.Value)) && (!object.ReferenceEquals(dsLogin.Tables[0].Rows[0]["Zip"], string.Empty)))
                    {
                        strAdd = strAdd + dsLogin.Tables[0].Rows[0]["Zip"].ToString().Trim();
                    }

                    lblBillingAddress.Text = strAdd;
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetDatabases");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
            }


        }

        #endregion

        protected void lnkEmptCarbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Products.aspx", false);
            Session["SubMenu"] = "Products";
        }


        private bool InsertOrderShipment(string orderId)
        {
            try
            {
                OrderEntryDAL orderEntryDAL = new OrderEntryDAL();
                DataSet warehouse = orderEntryDAL.GetGlobalWarehouseLabelAddress_DAL(int.Parse(orderId));
                if (warehouse != null && warehouse.Tables[0].Rows.Count > 0)
                {
                    orderEntryDAL.InsertOrderShipment_DAL(int.Parse(orderId), warehouse.Tables[0].Rows[0]["Name"].ToString(), warehouse.Tables[0].Rows[0]["Name"].ToString(), warehouse.Tables[0].Rows[0]["Address1"].ToString(),
                        warehouse.Tables[0].Rows[0]["Address2"].ToString(), int.Parse(warehouse.Tables[0].Rows[0]["City_Id"].ToString()), int.Parse(warehouse.Tables[0].Rows[0]["State_id"].ToString()), int.Parse(warehouse.Tables[0].Rows[0]["Country_id"].ToString()),
                        warehouse.Tables[0].Rows[0]["Zip"].ToString(), warehouse.Tables[0].Rows[0]["Phone1"].ToString(), txtCompany.Text.Trim(), txtCompany.Text.Trim(), txtStreetAdd1.Text.Trim(), txtAdd2.Text.Trim(), int.Parse(ddlCity.SelectedValue),
                        int.Parse(ddlState.SelectedValue), int.Parse(ddlCountry.SelectedValue), txtZip.Text.Trim(), txtPhone.Text.Trim(), int.Parse(ddlShippingMethod.SelectedValue), "nonvalidate", "01", "LBS", "02");
                    return true;
                }
                else
                {
                    return false;
                }
               
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "InsertOrderShipment");
                lblvaliderror.Text = strErrCode;
                lblvaliderror.Visible = true;
                return false;
            }
        }
    }
}