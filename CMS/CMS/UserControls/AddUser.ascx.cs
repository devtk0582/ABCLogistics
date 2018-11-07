

/*
'*   Date Created: 04/24/2012
'*
'*   Created By : Phani Vutla
'*
'*   Purpose: Adding new users and upadting exting users
'*
'*   Notes:
 * 
 * */

#region "Import namespace"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.DAL;
using System.Data;
using System.Text.RegularExpressions;
#endregion

namespace CMS.UserControls
{
    public partial class AddUser : System.Web.UI.UserControl
    {

        #region "Global Declartions "

        public event EventHandler SaveButtonClicked;
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";
        #endregion

        #region "Page Events"

        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMsg.Text = string.Empty;
            if (!IsPostBack)
            {

            }
        }

        #endregion

        #region "Private Members"

        public void Popup(int iUserID)
        {

            FillCountry(0);
            GetAddressBook();
            GetAddressBook1();
            BindSecurityQuestions();
            upAddUser.Visible = true;
            mpePopup.Show();
            ViewState["UserID"] = iUserID;
            ClearPanel();
            if (iUserID == 0)
            {
                lblTitle.Text = "Add User";
                txtPassword.Visible = true;
                lblPasswordRename.Visible = false;
                chkActive.Checked = true;
                lbSave.Text = "Save";
            }
            else
            {
                LoadUsersInfo(iUserID);
                DataSet dsAddinfo = (new SettingsDAL()).GetUserAddress(iUserID);
                if (dsAddinfo != null)
                {
                    if (dsAddinfo.Tables[0].Rows.Count > 0)
                    {
                        if (dsAddinfo.Tables[0].Rows[0]["primaryaddress_id"] != null)
                        {
                            if (dsAddinfo.Tables[0].Rows[0]["primaryaddress_id"].ToString() != string.Empty)
                            {
                                ViewState["iPrimaryaddid"] = dsAddinfo.Tables[0].Rows[0]["primaryaddress_id"];
                            }
                        }
                        if (dsAddinfo.Tables[0].Rows[0]["secondaryaddress_id"] != null)
                        {
                            if (dsAddinfo.Tables[0].Rows[0]["secondaryaddress_id"].ToString() != string.Empty)
                            {
                                ViewState["iSecondaryaddid"] = dsAddinfo.Tables[0].Rows[0]["secondaryaddress_id"];
                            }
                        }
                    }
                }
                LoadAddresses(Convert.ToInt32(ViewState["iPrimaryaddid"]));
                LoadAddresses1(Convert.ToInt32(ViewState["iSecondaryaddid"]));
                lblTitle.Text = "Update User";
                txtPassword.Visible = false;
                lblPasswordRename.Visible = true;
                lbSave.Text = "Update";
            }

        }

        public void GetAddressBook()
        {
            try
            {
                DataSet dsViewCartDetail = (new SettingsDAL()).GetAddressBook_DAL(0, string.Empty, 1, 10000);
                ddlAddress.Items.Clear();
                ddlAddress.DataSource = dsViewCartDetail.Tables[0];
                ddlAddress.DataTextField = "company_name";
                ddlAddress.DataValueField = "address_id";
                ddlAddress.DataBind();
                ddlAddress.Items.Insert(0, new ListItem("Please choose a shipping address", "-1"));
            }
            catch (Exception ex)
            {

                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetAddressBook");
                lblErrorMsg.Text = strErrCode;
            }

        }

        public void GetAddressBook1()
        {
            try
            {
                DataSet dsViewCartDetail = (new SettingsDAL()).GetAddressBook_DAL(0, string.Empty, 1, 10000);
                ddlAddress1.Items.Clear();
                ddlAddress1.DataSource = dsViewCartDetail.Tables[0];
                ddlAddress1.DataTextField = "company_name";
                ddlAddress1.DataValueField = "address_id";
                ddlAddress1.DataBind();
                ddlAddress1.Items.Insert(0, new ListItem("Please choose a shipping address", "-1"));
            }
            catch (Exception ex)
            {

                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetAddressBook");
                lblErrorMsg.Text = strErrCode;
            }

        }

        public void FillCountry(int iCountryId)
        {
            DataSet dsCountry = null;
            try
            {
                dsCountry = (new SettingsDAL()).GetCountry_DAL(iCountryId);
                ddlCountry.Items.Clear();
                ddlCountry1.Items.Clear();
                if (dsCountry != null)
                {
                    if (dsCountry.Tables[0].Rows.Count > 0)
                    {
                        ddlCountry.DataSource = dsCountry.Tables[0];
                        ddlCountry.DataValueField = "CountryId";
                        ddlCountry.DataTextField = "CountryName";
                        ddlCountry.DataBind();

                        ddlCountry1.DataSource = dsCountry.Tables[0];
                        ddlCountry1.DataValueField = "CountryId";
                        ddlCountry1.DataTextField = "CountryName";
                        ddlCountry1.DataBind();
                    }
                    ddlCountry.Items.Insert(0, new ListItem("Select", "-1"));
                    ddlCountry1.Items.Insert(0, new ListItem("Select", "-1"));
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "FillCountry", iCountryId.ToString());
                lblErrorMsg.Text = strErrCode;
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
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "FillState", iContryid.ToString());
                lblErrorMsg.Text = strErrCode;
            }
        }

        protected void FillState1(int iContryid)
        {
            DataSet dsState = null;
            try
            {
                dsState = (new SettingsDAL()).GetState_DAL(iContryid);

                ddlState1.Items.Clear();
                if (dsState != null)
                {
                    if (dsState.Tables[0].Rows.Count > 0)
                    {


                        ddlState1.DataSource = dsState.Tables[0];
                        ddlState1.DataValueField = "StateId";
                        ddlState1.DataTextField = "State";
                        ddlState1.DataBind();
                    }

                    ddlState1.Items.Insert(0, new ListItem("Select", "-1"));
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "FillState1", iContryid.ToString());
                lblErrorMsg.Text = strErrCode;
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
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "FillCity", iStateId.ToString());
                lblErrorMsg.Text = strErrCode;
            }
        }

        protected void FillCity1(int iStateId)
        {
            DataSet dsCity = null;
            try
            {
                dsCity = (new SettingsDAL()).GetCity_DAL(iStateId);

                ddlCity1.Items.Clear();
                if (dsCity != null)
                {
                    if (dsCity.Tables[0].Rows.Count > 0)
                    {


                        ddlCity1.DataSource = dsCity.Tables[0];
                        ddlCity1.DataValueField = "CityId";
                        ddlCity1.DataTextField = "City";
                        ddlCity1.DataBind();
                    }

                    ddlCity1.Items.Insert(0, new ListItem("Select", "-1"));
                }

            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "FillCity1", iStateId.ToString());
                lblErrorMsg.Text = strErrCode;
            }
        }

        protected void ddlCountry_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue != "-1")
            {
                FillState(Convert.ToInt32(ddlCountry.SelectedValue));
            }
            mpePopup.Show();
        }

        protected void ddlCountry1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlCountry1.SelectedValue != "-1")
            {
                FillState1(Convert.ToInt32(ddlCountry1.SelectedValue));
            }
            mpePopup.Show();
        }

        protected void ddlState_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue != "-1")
            {
                FillCity(Convert.ToInt16(ddlState.SelectedValue));
            }
            mpePopup.Show();
        }

        protected void ddlState1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlState1.SelectedValue != "-1")
            {
                FillCity1(Convert.ToInt16(ddlState1.SelectedValue));
            }
            mpePopup.Show();
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
                            txtFirstName.Text = dsusers.Tables[0].Rows[0]["user_first"].ToString();
                        }
                        if (dsusers.Tables[0].Rows[0]["user_last"].ToString() != string.Empty)
                        {
                            txtLastName.Text = dsusers.Tables[0].Rows[0]["user_last"].ToString();
                        }
                        if (dsusers.Tables[0].Rows[0]["user_email"].ToString() != string.Empty)
                        {
                            txtEmail.Text = dsusers.Tables[0].Rows[0]["user_email"].ToString();
                        }
                        if (dsusers.Tables[0].Rows[0]["user_pwd"].ToString() != string.Empty)
                        {
                            txtPassword.Text = dsusers.Tables[0].Rows[0]["user_pwd"].ToString();
                            lblPassword.Text = dsusers.Tables[0].Rows[0]["user_pwd"].ToString();
                            string sResultPwd = string.Empty;

                            for (int iCount = 0; iCount < lblPassword.Text.Length; iCount++)
                            {
                                sResultPwd += "X";
                            }

                            lblPasswordRename.Text = sResultPwd;

                        }
                        if (dsusers.Tables[0].Rows[0]["phone1"].ToString() != string.Empty)
                        {
                            txtPhone1.Text = dsusers.Tables[0].Rows[0]["phone1"].ToString();
                        }
                        if (dsusers.Tables[0].Rows[0]["phone2"].ToString() != string.Empty)
                        {
                            txtPhone2.Text = dsusers.Tables[0].Rows[0]["phone2"].ToString();
                        }
                        if (dsusers.Tables[0].Rows[0]["Role_Id"].ToString() != string.Empty)
                        {
                            ddlRoles.SelectedValue = dsusers.Tables[0].Rows[0]["Role_Id"].ToString();
                        }
                        chkActive.Checked = false;
                        if (dsusers.Tables[0].Rows[0]["Active"].ToString() != string.Empty)
                        {
                            if (Convert.ToBoolean(dsusers.Tables[0].Rows[0]["Active"]) == true)
                            {
                                chkActive.Checked = true;
                            }
                        }
                        BindSecurityQuestions();
                        if (dsusers.Tables[0].Rows[0]["SecurityQuestion1"].ToString() != string.Empty)
                        {
                            ddlSecQuestion1.SelectedValue = dsusers.Tables[0].Rows[0]["SecurityQuestion1"].ToString();
                        }
                        if (dsusers.Tables[0].Rows[0]["SecurityQuestion2"].ToString() != string.Empty)
                        {
                            ddlSecQuestion2.SelectedValue = dsusers.Tables[0].Rows[0]["SecurityQuestion2"].ToString();
                        }
                        if (dsusers.Tables[0].Rows[0]["SecurityAnswer1"].ToString() != string.Empty)
                        {
                            txtSeQuAns1.Text = dsusers.Tables[0].Rows[0]["SecurityAnswer1"].ToString();
                        }
                        if (dsusers.Tables[0].Rows[0]["SecurityAnswer2"].ToString() != string.Empty)
                        {
                            txtSeQuAns2.Text = dsusers.Tables[0].Rows[0]["SecurityAnswer2"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "LoadUsersInfo", iUserID.ToString());
                lblErrorMsg.Text = strErrCode;
            }
        }

        private void BindSecurityQuestions()
        {
            DataSet dsQuestions = null;

            try
            {


                dsQuestions = (new UsersDAL()).GetSecurityQuestions_DAL(0);
                ddlSecQuestion1.DataValueField = "Question_id";
                ddlSecQuestion1.DataTextField = "Question_description";
                ddlSecQuestion1.DataSource = dsQuestions.Tables[0];
                ddlSecQuestion1.DataBind();
                ddlSecQuestion2.DataValueField = "Question_id";
                ddlSecQuestion2.DataTextField = "Question_description";
                ddlSecQuestion2.DataSource = dsQuestions.Tables[0];
                ddlSecQuestion2.DataBind();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindSecurityQuestions");
                lblErrorMsg.Text = strErrCode;
            }
            finally
            {

            }
        }

        private void ClearPanel()
        {
            ViewState["iPrimaryaddid"] = "0";
            ViewState["iSecondaryaddid"] = "0";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtPhone1.Text = "";
            txtPhone2.Text = "";
            ddlSecQuestion1.SelectedIndex = -1;
            ddlSecQuestion2.SelectedIndex = -1;
            txtSeQuAns1.Text = "";
            txtSeQuAns2.Text = "";
            chkActive.Checked = false;
            ddlRoles.SelectedIndex = -1;
            ddlAddress.SelectedIndex = -1;
            txtAddName.Text = "";
            txtAdd1.Text = "";
            txtAdd2.Text = "";
            ddlCountry.SelectedIndex = -1;
            //  ddlState.SelectedValue = "-1";
            // ddlCity.SelectedValue = "-1";
            txtZip.Text = "";
            txtPhone.Text = "";
            ddlAddress1.SelectedIndex = -1;
            txtAddNamesec.Text = "";
            txtAddSecon.Text = "";
            txtAdd2Sec.Text = "";
            ddlCountry1.SelectedIndex = -1;
            // ddlState1.SelectedValue = "-1";
            //  ddlCity1.SelectedValue = "-1";
            txtZipSec.Text = "";
            txtPhoneSec.Text = "";
        }

        public string UpdateExistingPrimaryAddress(int iAddressid)
        {
            DataTable dt = new DataTable("sheet1");
            DataRow dr = null;
            int iCityId = 0;
            int iCountryId = 0;
            int iSateId = 0;
            string strCountry = string.Empty;
            string strState = string.Empty;
            string strCity = string.Empty;
            string strAddressid = "0";
            try
            {
                if (txtAddName.Text.Trim() == string.Empty)
                {
                    lblErrorMsg.Text = "Please enter name";
                    mpePopup.Show();
                    return strAddressid;

                }

                if (txtAdd1.Text.Trim() == string.Empty)
                {
                    lblErrorMsg.Text = "Please enter addresss1";
                    mpePopup.Show();
                    return strAddressid;

                }
                if (txtZip.Text.Trim() == string.Empty)
                {
                    lblErrorMsg.Text = "Please enter zip";
                    mpePopup.Show();
                    return strAddressid;

                }

                if (ddlCity.SelectedValue != "-1")
                {
                    iCityId = Convert.ToInt32(ddlCity.SelectedValue);
                    strCity = ddlCity.SelectedItem.Text;
                }
                if (ddlState.SelectedValue != "-1")
                {
                    iSateId = Convert.ToInt32(ddlState.SelectedValue);
                    strState = ddlState.SelectedItem.Text;
                }
                if (ddlCountry.SelectedValue != "-1")
                {
                    iCountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                    strCountry = ddlCountry.SelectedItem.Text;
                }



                dt.Columns.Add("custnum");
                dt.Columns.Add("address_id");
                dt.Columns.Add("company_name");
                dt.Columns.Add("rma_ship_name");
                dt.Columns.Add("addr");
                dt.Columns.Add("city");
                dt.Columns.Add("state");
                dt.Columns.Add("country");
                dt.Columns.Add("postal_code");
                dt.Columns.Add("phone");
                dt.Columns.Add("ext");
                dt.Columns.Add("room");
                dt.Columns.Add("addr2");
                dt.Columns.Add("locale");
                dt.Columns.Add("postal_code2");
                dt.Columns.Add("addr3");
                dt.Columns.Add("cust_address_id");
                dt.Columns.Add("attn");
                dt.Columns.Add("Region");
                dt.Columns.Add("customer_email");
                dt.Columns.Add("vatnumber");
                dt.Columns.Add("GMT");
                dr = dt.NewRow();
                dr[0] = 0;
                dr[1] = iAddressid;
                dr[2] = txtAddName.Text.Trim();
                dr[3] = txtAddName.Text.Trim();
                dr[4] = txtAdd1.Text.Trim();
                dr[5] = strCity;
                dr[6] = strState;
                dr[7] = strCountry;
                dr[8] = txtZip.Text.Trim();
                dr[9] = txtPhone.Text.Trim();
                dr[10] = string.Empty;
                dr[11] = string.Empty;
                dr[12] = txtAdd2.Text.Trim();
                dr[13] = string.Empty;
                dr[14] = string.Empty;
                dr[15] = string.Empty;
                dr[16] = 0;
                dr[17] = string.Empty;
                dr[18] = string.Empty;
                dr[19] = string.Empty;
                dr[20] = string.Empty;
                dr[21] = 0;
                dt.Rows.Add(dr);
                System.Text.StringBuilder sb = new System.Text.StringBuilder(2000);
                System.IO.StringWriter sw = new System.IO.StringWriter(sb);
                foreach (DataColumn col in dt.Columns)
                {
                    col.ColumnMapping = MappingType.Attribute;
                }
                dt.WriteXml(sw, System.Data.XmlWriteMode.WriteSchema);

                string strFlag = "I";
                if (iAddressid == 0)
                {
                    strFlag = "I";
                }
                else
                {
                    strFlag = "U";
                }
                string dbname = System.Configuration.ConfigurationSettings.AppSettings["dbname"].ToString();
                string strRetAddid = (new SettingsDAL()).UpdateCMSAddressBook_DAL(sw.ToString(), strFlag);

                if (strFlag == "I")
                    strAddressid = strRetAddid;
                else
                    strAddressid = iAddressid.ToString();



            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateExistingAddress");
                lblErrorMsg.Text = strErrCode;
            }
            return strAddressid;
        }

        public string UpdateExistingSecondaryAddress(int iAddressid)
        {
            DataTable dt = new DataTable("sheet1");
            DataRow dr = null;
            int iCityId = 0;
            int iCountryId = 0;
            int iSateId = 0;
            string strCountry = string.Empty;
            string strState = string.Empty;
            string strCity = string.Empty;
            string strAddressid = "0";
            try
            {
                if (txtAddNamesec.Text.Trim() == string.Empty)
                {
                    lblErrorMsg.Text = "Please enter name";
                    mpePopup.Show();
                    return strAddressid;

                }

                if (txtAddSecon.Text.Trim() == string.Empty)
                {
                    lblErrorMsg.Text = "Please enter addresss1";
                    mpePopup.Show();
                    return strAddressid;

                }
                if (txtZipSec.Text.Trim() == string.Empty)
                {
                    lblErrorMsg.Text = "Please enter zip";
                    mpePopup.Show();
                    return strAddressid;

                }

                if (ddlCity1.SelectedValue != "-1")
                {
                    iCityId = Convert.ToInt32(ddlCity1.SelectedValue);
                    strCity = ddlCity1.SelectedItem.Text;
                }
                if (ddlState1.SelectedValue != "-1")
                {
                    iSateId = Convert.ToInt32(ddlState1.SelectedValue);
                    strState = ddlState1.SelectedItem.Text;
                }
                if (ddlCountry1.SelectedValue != "-1")
                {
                    iCountryId = Convert.ToInt32(ddlCountry1.SelectedValue);
                    strCountry = ddlCountry1.SelectedItem.Text;
                }



                dt.Columns.Add("custnum");
                dt.Columns.Add("address_id");
                dt.Columns.Add("company_name");
                dt.Columns.Add("rma_ship_name");
                dt.Columns.Add("addr");
                dt.Columns.Add("city");
                dt.Columns.Add("state");
                dt.Columns.Add("country");
                dt.Columns.Add("postal_code");
                dt.Columns.Add("phone");
                dt.Columns.Add("ext");
                dt.Columns.Add("room");
                dt.Columns.Add("addr2");
                dt.Columns.Add("locale");
                dt.Columns.Add("postal_code2");
                dt.Columns.Add("addr3");
                dt.Columns.Add("cust_address_id");
                dt.Columns.Add("attn");
                dt.Columns.Add("Region");
                dt.Columns.Add("customer_email");
                dt.Columns.Add("vatnumber");
                dt.Columns.Add("GMT");
                dr = dt.NewRow();
                dr[0] = 0;
                dr[1] = iAddressid;
                dr[2] = txtAddNamesec.Text.Trim();
                dr[3] = txtAddNamesec.Text.Trim();
                dr[4] = txtAddSecon.Text.Trim();
                dr[5] = strCity;
                dr[6] = strState;
                dr[7] = strCountry;
                dr[8] = txtZipSec.Text.Trim();
                dr[9] = txtPhoneSec.Text.Trim();
                dr[10] = string.Empty;
                dr[11] = string.Empty;
                dr[12] = txtAdd2.Text.Trim();
                dr[13] = string.Empty;
                dr[14] = string.Empty;
                dr[15] = string.Empty;
                dr[16] = 0;
                dr[17] = string.Empty;
                dr[18] = string.Empty;
                dr[19] = string.Empty;
                dr[20] = string.Empty;
                dr[21] = 0;
                dt.Rows.Add(dr);
                System.Text.StringBuilder sb = new System.Text.StringBuilder(2000);
                System.IO.StringWriter sw = new System.IO.StringWriter(sb);
                foreach (DataColumn col in dt.Columns)
                {
                    col.ColumnMapping = MappingType.Attribute;
                }
                dt.WriteXml(sw, System.Data.XmlWriteMode.WriteSchema);

                string strFlag = "I";
                if (iAddressid == 0)
                {
                    strFlag = "I";
                }
                else
                {
                    strFlag = "U";
                }
                string dbname = System.Configuration.ConfigurationSettings.AppSettings["dbname"].ToString();
                string strRetAddid = (new SettingsDAL()).UpdateCMSAddressBook_DAL(sw.ToString(), strFlag);


                strAddressid = strRetAddid;

                if (strFlag == "I")
                    strAddressid = strRetAddid;
                else
                    strAddressid = iAddressid.ToString();

            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateExistingAddress");
                lblErrorMsg.Text = strErrCode;
            }
            return strAddressid;
        }

        public void LoadAddresses(int iAddressID)
        {
            try
            {
                if (iAddressID != 0)
                {
                    ddlAddress.SelectedValue = iAddressID.ToString();


                    DataSet dsAddresses = (new SettingsDAL()).GetAddressBook_DAL(iAddressID, string.Empty, 1, 10000);

                    foreach (DataRow dr in dsAddresses.Tables[0].Rows)
                    {
                        // lblAddID.Text = iAddressID.ToString();

                        if (!DBNull.Value.Equals(dr["company_name"]) & (dr["company_name"] != null))
                        {
                            txtAddName.Text = Convert.ToString(dr["company_name"]);
                        }

                        if (!DBNull.Value.Equals(dr["addr"]) & (dr["addr"] != null))
                        {
                            txtAdd1.Text = Convert.ToString(dr["addr"].ToString());
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

                        if (!DBNull.Value.Equals(dr["addr2"]) & (dr["addr2"] != null))
                        {
                            txtAdd2.Text = Convert.ToString(dr["addr2"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "LoadAddresses", iAddressID.ToString());
                lblErrorMsg.Text = strErrCode;
            }
        }

        public void LoadAddresses1(int iAddressID)
        {
            try
            {
                if (iAddressID != 0)
                {
                    ddlAddress1.SelectedValue = iAddressID.ToString();

                    DataSet dsAddresses = (new SettingsDAL()).GetAddressBook_DAL(iAddressID, string.Empty, 1, 10000);

                    foreach (DataRow dr in dsAddresses.Tables[0].Rows)
                    {
                        // lblAddID.Text = iAddressID.ToString();

                        if (!DBNull.Value.Equals(dr["company_name"]) & (dr["company_name"] != null))
                        {
                            txtAddNamesec.Text = Convert.ToString(dr["company_name"]);
                        }

                        if (!DBNull.Value.Equals(dr["addr"]) & (dr["addr"] != null))
                        {
                            txtAddSecon.Text = Convert.ToString(dr["addr"].ToString());
                        }
                        if (!DBNull.Value.Equals(dr["Country_id"]))
                        {
                            if ((ddlCountry1.Items.FindByValue(dr["Country_id"].ToString()) != null))
                            {
                                ddlState1.Items.Clear();
                                ddlCountry1.SelectedValue = dr["Country_id"].ToString();
                                FillState1(Convert.ToInt32(ddlCountry1.SelectedValue));
                            }
                            else
                            {
                                ddlCountry1.SelectedValue = "-1";
                            }
                        }
                        else
                        {
                            ddlCountry1.SelectedValue = "-1";
                        }
                        if (!DBNull.Value.Equals(dr["State_Id"]))
                        {
                            if ((ddlState1.Items.FindByValue(dr["State_Id"].ToString()) != null))
                            {
                                ddlState1.SelectedValue = dr["State_Id"].ToString();
                                FillCity1(Convert.ToInt32(ddlState1.SelectedValue));
                            }
                            else
                            {
                                ddlState1.SelectedValue = "-1";
                            }
                        }
                        else
                        {
                            ddlState1.SelectedValue = "-1";
                        }
                        if (!DBNull.Value.Equals(dr["City_Id"]))
                        {
                            if ((ddlCity1.Items.FindByValue(dr["City_Id"].ToString()) != null))
                            {
                                ddlCity1.SelectedValue = dr["City_Id"].ToString();
                            }
                            else
                            {
                                ddlCity1.SelectedValue = "-1";
                            }
                        }
                        else
                        {
                            ddlCity1.SelectedValue = "-1";
                        }
                        if (!DBNull.Value.Equals(dr["postal_code"]) & (dr["postal_code"] != null))
                        {
                            txtZipSec.Text = Convert.ToString(dr["postal_code"].ToString());
                        }
                        if (!DBNull.Value.Equals(dr["phone"]) & (dr["phone"] != null))
                        {
                            txtPhoneSec.Text = Convert.ToString(dr["phone"].ToString());
                        }

                        if (!DBNull.Value.Equals(dr["addr2"]) & (dr["addr2"] != null))
                        {
                            txtAdd2Sec.Text = Convert.ToString(dr["addr2"].ToString());
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "LoadAddresses1", iAddressID.ToString());
                lblErrorMsg.Text = strErrCode;
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
        #endregion

        #region "Controls Events"

        protected void lbSave_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text.Trim() == "")
            {
                lblErrorMsg.Text = "First name cannot be empty.";
                mpePopup.Show();
                return;
            }
            if (txtLastName.Text.Trim() == "")
            {
                lblErrorMsg.Text = "Last name cannot be empty.";
                mpePopup.Show();
                return;
            }
            if (ddlRoles.SelectedValue == "-1")
            {
                lblErrorMsg.Text = "Please select role.";
                mpePopup.Show();
                return;
            }

            if (txtEmail.Text.Trim() == "")
            {
                lblErrorMsg.Text = "Email cannot be empty.";
                mpePopup.Show();
                return;
            }
            if (txtEmail.Text.Trim() != "")
            {
                if (isEmail(txtEmail.Text.Trim()) == false)
                {
                    lblErrorMsg.Text = "Invalid Email.";
                    mpePopup.Show();
                    return;
                }
            }


            if (lbSave.Text == "Save")
            {
                if (txtPassword.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Password cannot be empty.";
                    mpePopup.Show();
                    return;
                }
            }
            if (txtAddName.Text.Trim() == "")
            {
                lblErrorMsg.Text = "Primary Address Name cannot be empty.";
                mpePopup.Show();
                return;
            }

            if (txtAdd1.Text.Trim() == "")
            {
                lblErrorMsg.Text = "Primary Address cannot be empty.";
                mpePopup.Show();
                return;
            }
            if (txtZip.Text.Trim() == "")
            {
                lblErrorMsg.Text = "Primary zip cannot be empty.";
                mpePopup.Show();
                return;
            }

            if (txtAddNamesec.Text.Trim() == "")
            {
                lblErrorMsg.Text = "Secondary Address Name cannot be empty.";
                mpePopup.Show();
                return;
            }

            if (txtAddSecon.Text.Trim() == "")
            {
                lblErrorMsg.Text = "Secondary Address cannot be empty.";
                mpePopup.Show();
                return;
            }
            if (txtZipSec.Text.Trim() == "")
            {
                lblErrorMsg.Text = "Secondary zip cannot be empty.";
                mpePopup.Show();
                return;
            }
            try
            {
                DataSet dbDbIds=new DataSet();
                DataTable dt = new DataTable("dtWhse");
                DataRow dr = null;
                  string dbName = System.Configuration.ConfigurationSettings.AppSettings["dbname"].ToString();
                  string strDbId="";
                  dbDbIds = (new UsersDAL()).GetCMSCusDatabaseId_DAL(dbName);
                  if (dbDbIds.Tables[0].Rows.Count > 0)
                  {
                      strDbId = dbDbIds.Tables[0].Rows[0]["GteDatabases_Id"].ToString().Trim();
                  }
                  else
                  {
                      lblErrorMsg.Text = "Database is not Exist.";
                      mpePopup.Show();
                      return;
                  }

                dt.Columns.Add("Gtedatabases_Id");
                dt.Columns.Add("Whse_Id");
                dt.Columns.Add("Terminal_Id");
                dr = dt.NewRow();
                dr[0] = strDbId; 
                dr[1] = "-1";
                dr[2] = "-1";
                dt.Rows.Add(dr);

                System.Text.StringBuilder sb = new System.Text.StringBuilder(2000);
                System.IO.StringWriter sw = new System.IO.StringWriter(sb);
                foreach (DataColumn col in dt.Columns)
                {
                    col.ColumnMapping = MappingType.Attribute;
                }
                dt.WriteXml(sw, System.Data.XmlWriteMode.WriteSchema);

                int iUserID = 0;
                string strSattaus = "I";
                Boolean blnActive = false;


                if (ViewState["UserID"] != null && ViewState["UserID"].ToString() != "0")
                {
                    if (ViewState["UserID"].ToString() != string.Empty)
                    {
                        iUserID = Convert.ToInt32(ViewState["UserID"]);
                        strSattaus = "U";
                    }
                }


                if (lblPassword.Text == string.Empty)
                {
                    lblPassword.Text = txtPassword.Text.Trim();
                }

                if (chkActive.Checked == true)
                {
                    blnActive = true;
                }

                string strUserid = (new UsersDAL()).UserUpdation_DAL(iUserID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmail.Text.Trim(), lblPassword.Text, txtPhone1.Text.Trim(), txtPhone2.Text.Trim(), blnActive, ddlSecQuestion1.SelectedValue, txtSeQuAns1.Text.Trim(),
                            ddlSecQuestion2.SelectedValue, txtSeQuAns2.Text.Trim(), strSattaus, Convert.ToInt32(ddlRoles.SelectedValue), sw.ToString());

                if (strUserid == "User's Email already exists in the database")
                {
                    lblErrorMsg.Text = "Email ID is already exists please try with another Email ID.";
                    mpePopup.Show();
                    return;
                }
                else
                {

                    string strprimaryaddress_id = "0";
                    string strsecondaryaddress_id = "0";
                    if (ViewState["iPrimaryaddid"] != "0" && ViewState["iPrimaryaddid"] != string.Empty)
                    {
                        // Update existing Addressinformation
                        UpdateExistingPrimaryAddress(Convert.ToInt32(ViewState["iPrimaryaddid"]));
                        strprimaryaddress_id = ViewState["iPrimaryaddid"].ToString();
                    }
                    else
                    {
                        strprimaryaddress_id = UpdateExistingPrimaryAddress(Convert.ToInt32(0));
                    }

                    if (ViewState["iSecondaryaddid"] != "0" && ViewState["iSecondaryaddid"] != string.Empty)
                    {
                        UpdateExistingSecondaryAddress(Convert.ToInt32(ViewState["iSecondaryaddid"]));
                        strsecondaryaddress_id = ViewState["iSecondaryaddid"].ToString();
                    }
                    else
                    {
                        strsecondaryaddress_id = UpdateExistingSecondaryAddress(Convert.ToInt32(0));
                    }

                    string strRetAddid = (new SettingsDAL()).UpdateUserAddressBook_DAL(Convert.ToInt32(strprimaryaddress_id), Convert.ToInt32(strsecondaryaddress_id), Convert.ToInt32(strUserid), strSattaus, "P");
                    
                    ClearPanel();
                    upAddUser.Visible = false;

                    SaveButtonClicked(sender, e);
                }
               

            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lbSave_Click");
                lblErrorMsg.Text = strErrCode;
            }
        }




        protected void lbCancel_Click(object sender, EventArgs e)
        {

        }


        protected void ddlAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAddress.SelectedIndex > 0)
                {
                    ViewState["iPrimaryaddid"] = ddlAddress.SelectedValue;
                    LoadAddresses(Convert.ToInt32(ddlAddress.SelectedValue));

                }
                mpePopup.Show();
            }
            catch (Exception ex)
            {

                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "ddlAddress_SelectedIndexChanged");
                lblErrorMsg.Text = strErrCode;
            }

        }

        protected void ddlAddress1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAddress1.SelectedIndex > 0)
                {
                    ViewState["iSecondaryaddid"] = ddlAddress1.SelectedValue;
                    LoadAddresses1(Convert.ToInt32(ddlAddress1.SelectedValue));
                }
                mpePopup.Show();
            }
            catch (Exception ex)
            {

                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "ddlAddress1_SelectedIndexChanged");
                lblErrorMsg.Text = strErrCode;
            }

        }



        #endregion

    }
}