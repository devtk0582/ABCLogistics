using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.DAL;
using System.Drawing;

namespace CMS.Settings
{
    public partial class BillingCodes : System.Web.UI.Page
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ViewState["SortExpression"] = "BillingCode_Name";
                    ViewState["SortDirection"] = "ASC";
                    hfCurrentID.Value = "0";
                    BindBillingCodes();
                }
                catch (Exception ex)
                {
                    string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Page_Load");
                    lblErr.Text = strErrCode;
                }
            }
        }

        private void BindBillingCodes()
        {
            try
            {
                DataSet billingCodeList = (new SettingsDAL()).GetBillingCodes(0);
                if (billingCodeList != null && billingCodeList.Tables[0].Rows.Count > 0)
                {
                    DataView dvBillingCodes = billingCodeList.Tables[0].DefaultView;
                    if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
                        if (ViewState["SortExpression"].ToString() != string.Empty && ViewState["SortDirection"].ToString() != string.Empty)
                            dvBillingCodes.Sort = ViewState["SortExpression"].ToString() + " " + ViewState["SortDirection"].ToString();
                    gvBillingCodes.DataSource = dvBillingCodes;
                    gvBillingCodes.DataBind();
                }
                else
                {
                    gvBillingCodes.DataSource = null;
                    gvBillingCodes.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindBillingCodes");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvBillingCodes_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                if (ViewState["SortDirection"] != null)
                    if (ViewState["SortDirection"].ToString() == "DESC")
                    {
                        e.SortDirection = SortDirection.Ascending;
                        ViewState["SortDirection"] = "ASC";
                    }
                    else
                    {
                        e.SortDirection = SortDirection.Descending;
                        ViewState["SortDirection"] = "DESC";
                    }
                else
                {
                    if (e.SortDirection == SortDirection.Ascending)
                    {
                        e.SortDirection = SortDirection.Descending;
                        ViewState["SortDirection"] = "DESC";
                    }
                    else
                    {
                        e.SortDirection = SortDirection.Ascending;
                        ViewState["SortDirection"] = "ASC";
                    }
                }

                ViewState["SortExpression"] = e.SortExpression.ToString();
                gvBillingCodes.EditIndex = -1;
                BindBillingCodes();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvBillingCodes_Sorting");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvBillingCodes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditBillingCode")
                {
                    hfCurrentID.Value = e.CommandArgument.ToString();
                    ucEditBillingCode.Popup(e.CommandArgument.ToString());
                }
                else if (e.CommandName == "ToggleBillingCode")
                {
                    hfCurrentID.Value = e.CommandArgument.ToString();
                    SettingsDAL settingsDAL = new SettingsDAL();
                    if (((LinkButton)e.CommandSource).Text == "Inactive")
                        settingsDAL.ToggleBillingCode(int.Parse(e.CommandArgument.ToString()), true);
                    else
                        settingsDAL.ToggleBillingCode(int.Parse(e.CommandArgument.ToString()), false);
                    BindBillingCodes();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvBillingCodes_RowCommand");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvBillingCodes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvBillingCodes.PageIndex = e.NewPageIndex;
                BindBillingCodes();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvBillingCodes_PageIndexChanging");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvBillingCodes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lbStatus = ((LinkButton)e.Row.FindControl("lnkToggle"));
                    //LinkButton lbName = ((LinkButton)e.Row.FindControl("lnkName"));
                    if (Convert.ToBoolean(((HiddenField)e.Row.FindControl("hfStatus")).Value) == true)
                    {
                        lbStatus.Text = "Active";
                    }
                    else
                    {
                        lbStatus.Text = "Inactive";
                        e.Row.Cells[2].BackColor = Color.FromArgb(int.Parse("993300", System.Globalization.NumberStyles.HexNumber));
                        e.Row.Cells[2].ForeColor = Color.White;
                        lbStatus.BackColor = Color.FromArgb(int.Parse("993300", System.Globalization.NumberStyles.HexNumber));
                        lbStatus.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvBillingCodes_RowDataBound");
                lblErr.Text = strErrCode;
            }
        }

        protected void lbAddBillingCode_Click(object sender, EventArgs e)
        {
            try
            {
                ucAddBillingCode.Popup();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lbAddBillingCode_Click");
                lblErr.Text = strErrCode;
            }
            //lblErr.Text = "add a shipping method";
        }

        protected void ucAddBillingCode_SaveButtonClicked(object sender, EventArgs e)
        {
            BindBillingCodes();
        }

        protected void ucEditBillingCode_SaveButtonClicked(object sender, EventArgs e)
        {
            BindBillingCodes();
        }
    }
}