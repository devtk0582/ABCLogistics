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
    public partial class ShippingMethods : System.Web.UI.Page
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ViewState["SortExpression"] = "ShipMethod_Name";
                    ViewState["SortDirection"] = "ASC";
                    hfCurrentID.Value = "0";
                    BindShippingMethods();
                }
                catch (Exception ex)
                {
                    string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Page_Load");
                    lblErr.Text = strErrCode;
                }
            }
        }

        private void BindShippingMethods()
        {
            try
            {
                DataSet shippingMethodList = (new SettingsDAL()).GetShippingMethods(0);
                if (shippingMethodList != null && shippingMethodList.Tables[0].Rows.Count > 0)
                {
                    DataView dvShippingMethods = shippingMethodList.Tables[0].DefaultView;
                    if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
                        if (ViewState["SortExpression"].ToString() != string.Empty && ViewState["SortDirection"].ToString() != string.Empty)
                            dvShippingMethods.Sort = ViewState["SortExpression"].ToString() + " " + ViewState["SortDirection"].ToString();
                    gvShippingMethods.DataSource = dvShippingMethods;
                    gvShippingMethods.DataBind();
                }
                else
                {
                    gvShippingMethods.DataSource = null;
                    gvShippingMethods.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindShippingMethods");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvShippingMethods_Sorting(object sender, GridViewSortEventArgs e)
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
                gvShippingMethods.EditIndex = -1;
                BindShippingMethods();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvShippingMethods_Sorting");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvShippingMethods_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditMethod")
                {
                    hfCurrentID.Value = e.CommandArgument.ToString();
                    ucEditShippingMethod.Popup(e.CommandArgument.ToString());
                }
                else if (e.CommandName == "ToggleMethod")
                {
                    hfCurrentID.Value = e.CommandArgument.ToString();
                    SettingsDAL settingsDAL = new SettingsDAL();
                    if (((LinkButton)e.CommandSource).Text == "Inactive")
                        settingsDAL.ToggleShippingMethod(int.Parse(e.CommandArgument.ToString()), true);
                    else
                        settingsDAL.ToggleShippingMethod(int.Parse(e.CommandArgument.ToString()), false);
                    BindShippingMethods();

                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvShippingMethods_RowCommand");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvShippingMethods_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvShippingMethods.PageIndex = e.NewPageIndex;
                BindShippingMethods();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvShippingMethods_PageIndexChanging");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvShippingMethods_RowDataBound(object sender, GridViewRowEventArgs e)
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
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvShippingMethods_RowDataBound");
                lblErr.Text = strErrCode;
            }
        }

        protected void lbAddShippingMethod_Click(object sender, EventArgs e)
        {
            try
            {
                ucAddShippingMethod.Popup();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lbAddShippingMethod_Click");
                lblErr.Text = strErrCode;
            }
            
            //lblErr.Text = "add a shipping method";
        }

        protected void ucAddShippingMethod_SaveButtonClicked(object sender, EventArgs e)
        {
            BindShippingMethods();
        }

        protected void ucEditShippingMethod_SaveButtonClicked(object sender, EventArgs e)
        {
            BindShippingMethods();
        }
    }
}