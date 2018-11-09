using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSMaster.DAL;
using System.Data;

namespace CMSMaster
{
    public partial class CustomerDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session["MainMenu"] = "Details";
                    if (Session["DBName"] != null)
                    {
                        BindCountries(0);
                        ddlState.Items.Insert(0, new ListItem("Select", "-1"));
                        ddlCity.Items.Insert(0, new ListItem("Select", "-1"));
                        if (Session["CustomerID"] != null && Session["UserID"] != null)
                        {
                            BindCustomerInformation(Session["CustomerID"].ToString(), Session["UserID"].ToString());

                        }
                        if (Session["CustomerName"] != null)
                        {
                            ((Label)Master.FindControl("CustomerName")).Text = Session["CustomerName"].ToString();
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Home.aspx", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrMessage.Visible = true;
               lblErrMessage.Text = ex.Message;
            }
        }

        private void BindCustomerInformation(string customerId, string userId)
        {
            try
            {
                DataSet customerInfo = new clsCustomer(Session["DBName"].ToString()).GetCustomers(int.Parse(customerId), string.Empty, userId);
                if (customerInfo != null && customerInfo.Tables[0].Rows.Count > 0)
                {
                    txtCustomerNo.Text = customerInfo.Tables[0].Rows[0]["CustNumber"].ToString();
                    txtCustomerName.Text = customerInfo.Tables[0].Rows[0]["CustName"].ToString();
                    txtDBName.Text = customerInfo.Tables[0].Rows[0]["Databasename"].ToString();
                    txtDBName.Enabled = false;
                    txtAddress1.Text = customerInfo.Tables[0].Rows[0]["Address1"].ToString();
                    txtAddress2.Text = customerInfo.Tables[0].Rows[0]["Address2"].ToString();
                    txtZip.Text = customerInfo.Tables[0].Rows[0]["Zip"].ToString();
                    txtMainContact.Text = customerInfo.Tables[0].Rows[0]["Primary_ContactName"].ToString();
                    txtPrimaryEmail.Text = customerInfo.Tables[0].Rows[0]["Primary_Email"].ToString();
                    txtPhone1.Text = customerInfo.Tables[0].Rows[0]["Primary_Phone"].ToString();
                    txtSecondaryContact.Text = customerInfo.Tables[0].Rows[0]["Secondary_ContactName"].ToString();
                    txtSecondaryemail.Text = customerInfo.Tables[0].Rows[0]["Secondary_Email"].ToString();
                    txtPhone2.Text = customerInfo.Tables[0].Rows[0]["Secondary_Phone"].ToString();
                    txtCustomerSince.Text = customerInfo.Tables[0].Rows[0]["Cust_Since"].ToString();
                    txtNotes.Text = customerInfo.Tables[0].Rows[0]["Notes"].ToString();
                    txtDateEntered.Text = ((DateTime)customerInfo.Tables[0].Rows[0]["DateEntered"]).ToShortDateString();
                    txtABCLogisticsSalRepEmail.Text = customerInfo.Tables[0].Rows[0]["ABCLogisticsSalRep_Email"].ToString();
                    txtCSRConEmail.Text = customerInfo.Tables[0].Rows[0]["CSRCon_Email"].ToString();
                    chkActive.Checked = Boolean.Parse(customerInfo.Tables[0].Rows[0]["Active"].ToString());
                    if (ddlCountry.Items.FindByValue(customerInfo.Tables[0].Rows[0]["Country_Id"].ToString()) != null)
                    {
                        ddlCountry.SelectedValue = customerInfo.Tables[0].Rows[0]["Country_Id"].ToString();
                        BindStates(int.Parse(ddlCountry.SelectedValue));
                    }
                    else
                    {
                        ddlCountry.SelectedValue = "-1";
                    }

                    if (ddlState.Items.FindByValue(customerInfo.Tables[0].Rows[0]["State_Id"].ToString()) != null)
                    {
                        ddlState.SelectedValue = customerInfo.Tables[0].Rows[0]["State_Id"].ToString();
                        BindCities(int.Parse(ddlState.SelectedValue));
                    }
                    else
                    {
                        ddlState.SelectedValue = "-1";
                    }

                    if (ddlCity.Items.FindByValue(customerInfo.Tables[0].Rows[0]["City_Id"].ToString()) != null)
                    {
                        ddlCity.SelectedValue = customerInfo.Tables[0].Rows[0]["City_Id"].ToString();
                    }
                    else
                    {
                        ddlCity.SelectedValue = "-1";
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrMessage.Visible = true;
                lblErrMessage.Text = ex.Message;
            }
        }

        private void BindCountries(int countryId)
        {
            try
            {
                DataSet countryList = new clsCustomer(Session["DBName"].ToString()).GetCountries(0);
                ddlCountry.Items.Clear();
                if (countryList != null && countryList.Tables[0].Rows.Count > 0)
                {
                    ddlCountry.DataSource = countryList.Tables[0];
                    ddlCountry.DataValueField = "CountryId";
                    ddlCountry.DataTextField = "CountryName";
                    ddlCountry.DataBind();
                }
                ddlCountry.Items.Insert(0, new ListItem("Select", "-1"));
            }
            catch (Exception ex)
            {
                lblErrMessage.Visible = true;
                lblErrMessage.Text = ex.Message;
            }
        }

        private void BindStates(int countryId)
        {
            try
            {
                DataSet stateList = new clsCustomer(Session["DBName"].ToString()).GetStates(countryId);
                ddlState.Items.Clear();
                if (stateList != null && stateList.Tables[0].Rows.Count > 0)
                {
                    ddlState.DataSource = stateList.Tables[0];
                    ddlState.DataValueField = "StateId";
                    ddlState.DataTextField = "State";
                    ddlState.DataBind();
                }
                ddlState.Items.Insert(0, new ListItem("Select", "-1"));
            }
            catch (Exception ex)
            {
                lblErrMessage.Visible = true;
                lblErrMessage.Text = ex.Message;
            }
        }

        private void BindCities(int stateId)
        {
            try
            {
                DataSet cityList = new clsCustomer(Session["DBName"].ToString()).GetCities(stateId);
                ddlCity.Items.Clear();
                if (cityList != null && cityList.Tables[0].Rows.Count > 0)
                {
                    ddlCity.DataSource = cityList.Tables[0];
                    ddlCity.DataValueField = "CityId";
                    ddlCity.DataTextField = "City";
                    ddlCity.DataBind();
                }
                ddlCity.Items.Insert(0, new ListItem("Select", "-1"));
            }
            catch (Exception ex)
            {
                lblErrMessage.Visible = true;
                lblErrMessage.Text = ex.Message;
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCountry.SelectedIndex > 0)
                    BindStates(int.Parse(ddlCountry.SelectedValue));
            }
            catch (Exception ex)
            {
                lblErrMessage.Visible = true;
                lblErrMessage.Text = ex.Message;
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlState.SelectedIndex > 0)
                    BindCities(int.Parse(ddlState.SelectedValue));
            }
            catch (Exception ex)
            {
                lblErrMessage.Visible = true;
                lblErrMessage.Text = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["CustomerID"] != null && Session["UserID"] != null)
                {
                    BindCustomerInformation(Session["CustomerID"].ToString(), Session["UserID"].ToString());

                }
            }
            catch (Exception ex)
            {
                lblErrMessage.Visible = true;
                lblErrMessage.Text = ex.Message;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                (new clsCustomer(Session["DBName"].ToString())).UpdateCustomerInfo(int.Parse(Session["CustomerID"].ToString()),
                    txtCustomerName.Text.Trim(),
                    txtDBName.Text.Trim(),
                    chkActive.Checked,
                    txtDateEntered.Text.Trim(),
                    txtCustomerNo.Text.Trim(),
                    txtAddress1.Text.Trim(), 
                    txtAddress2.Text.Trim(),
                    txtZip.Text.Trim(),
                    int.Parse(ddlCity.SelectedIndex == 0 ? "0" : ddlCity.SelectedValue),
                    int.Parse(ddlState.SelectedIndex == 0 ? "0" : ddlState.SelectedValue),
                    int.Parse(ddlCountry.SelectedIndex == 0 ? "0" : ddlCountry.SelectedValue),
                    int.Parse(txtCustomerSince.Text.Trim()),
                    txtMainContact.Text.Trim(),
                    txtPrimaryEmail.Text.Trim(),
                    txtPhone1.Text.Trim(),
                    txtSecondaryContact.Text.Trim(),
                    txtSecondaryemail.Text.Trim(),
                    txtPhone2.Text.Trim(),
                    txtNotes.Text.Trim(),
                    txtABCLogisticsSalRepEmail.Text.Trim(),
                    txtCSRConEmail.Text.Trim());         
            }
            catch (Exception ex)
            {
                lblErrMessage.Text = ex.Message;
            }
        }
    }
}