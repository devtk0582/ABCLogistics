using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.DAL;
using System.Data;


namespace CMS.UserControls
{
    public partial class AddAddresses : System.Web.UI.UserControl
    {
        public event EventHandler SaveButtonClicked;
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMsg.Text = string.Empty;
            if (!IsPostBack)
            {



            }

            txtZip.Attributes.Add("onkeypress", "return onlyNumbers();");
        }

        public void Popup(int iAddressID)
        {
            FillCountry(0);
            upAddUser.Visible = true;
            mpePopup.Show();
            ClearPanel();
            lblAddID.Text = iAddressID.ToString();
            if (iAddressID == 0)
            {
                lblTitle.Text = "Add Address";
                lbSave.Text = "Save";
            }
            else
            {
                lblTitle.Text = "Update Address";
                LoadAddresses(iAddressID);
                lbSave.Text = "Update";
            }

        }

        public void LoadAddresses(int iAddressID)
        {
            try
            {

                DataSet dsAddresses = (new SettingsDAL()).GetAddressBook_DAL(iAddressID, string.Empty, 1, 10000);

                foreach (DataRow dr in dsAddresses.Tables[0].Rows)
                {
                    lblAddID.Text = iAddressID.ToString();

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
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "LoadAddresses", iAddressID.ToString());
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

        protected void ddlCountry_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue != "-1")
            {
                FillState(Convert.ToInt32(ddlCountry.SelectedValue));
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

        protected void lbSave_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("sheet1");
            DataRow dr = null;
            int iCityId = 0;
            int iCountryId = 0;
            int iSateId = 0;
            string strCountry = string.Empty;
            string strState = string.Empty;
            string strCity = string.Empty;
            try
            {
                if (txtAddName.Text.Trim() == string.Empty)
                {
                    lblErrorMsg.Text = "Please enter name";
                    mpePopup.Show();
                    return;

                }

                if (txtAdd1.Text.Trim() == string.Empty)
                {
                    lblErrorMsg.Text = "Please enter addresss1";
                    mpePopup.Show();
                    return;

                }
                if (ddlCountry.SelectedValue == "-1")
                {
                    lblErrorMsg.Text = "Please select country.";
                    mpePopup.Show();
                    return;
                }
                if (ddlState.SelectedValue == "-1")
                {
                    lblErrorMsg.Text = "Please select state.";
                    mpePopup.Show();
                    return;
                }
                if (ddlCity.SelectedValue == "-1")
                {
                    lblErrorMsg.Text = "Please select city.";
                    mpePopup.Show();
                    return;
                }
                if (txtZip.Text.Trim() == string.Empty)
                {
                    lblErrorMsg.Text = "Please enter zip";
                    mpePopup.Show();
                    return;

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
                dr[1] = lblAddID.Text;
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
                if (lblAddID.Text == "0")
                {
                    strFlag = "I";
                }
                else
                {
                    strFlag = "U";
                }
                string dbname = System.Configuration.ConfigurationSettings.AppSettings["dbname"].ToString();
                string strRet = (new SettingsDAL()).UpdateAddressBook_DAL(sw.ToString(), strFlag);

                ClearPanel();
                upAddUser.Visible = false;
                ddlCity.SelectedIndex = 0;
                ddlState.SelectedIndex = 0;
                ddlCountry.SelectedIndex = 0;
                SaveButtonClicked(sender, e);
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lbSave_Click");
                lblErrorMsg.Text = strErrCode;
            }
        }


        private void ClearPanel()
        {
            txtAddName.Text = string.Empty;
            txtAdd1.Text = string.Empty;
            txtAdd2.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtZip.Text = string.Empty;



        }



    }

}