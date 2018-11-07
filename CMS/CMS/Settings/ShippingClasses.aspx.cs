using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using CMS.DAL;

namespace CMS.Settings
{
    public partial class ShippingClasses : System.Web.UI.Page
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ViewState["SortExpression"] = "ShipClass";
                    ViewState["SortDirection"] = "ASC";
                    hfCurrentID.Value = "0";
                    BindShippingClasses();
                }
                catch (Exception ex)
                {
                    string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Page_Load");
                    lblErr.Text = strErrCode;
                }
            }
        }

        private void BindShippingClasses()
        {
            try
            {
                DataSet ShippingClassList = (new SettingsDAL()).GetShippingClasses(0);
                if (ShippingClassList != null && ShippingClassList.Tables[0].Rows.Count > 0)
                {
                    DataView dvShippingClasss = ShippingClassList.Tables[0].DefaultView;
                    if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
                        if (ViewState["SortExpression"].ToString() != string.Empty && ViewState["SortDirection"].ToString() != string.Empty)
                            dvShippingClasss.Sort = ViewState["SortExpression"].ToString() + " " + ViewState["SortDirection"].ToString();
                    gvShippingClasss.DataSource = dvShippingClasss;
                    gvShippingClasss.DataBind();
                }
                else
                {
                    gvShippingClasss.DataSource = null;
                    gvShippingClasss.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindShippingClasses");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvShippingClasss_Sorting(object sender, GridViewSortEventArgs e)
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
                gvShippingClasss.EditIndex = -1;
                BindShippingClasses();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvShippingClasss_Sorting");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvShippingClasss_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditShippingClass")
                {
                    hfCurrentID.Value = e.CommandArgument.ToString();
                    ucEditShippingClass.Popup(e.CommandArgument.ToString());
                }
                else if (e.CommandName == "ToggleShippingClass")
                {
                    hfCurrentID.Value = e.CommandArgument.ToString();
                    SettingsDAL settingsDAL = new SettingsDAL();
                    if (((LinkButton)e.CommandSource).Text == "Inactive")
                        settingsDAL.ToggleShippingClass(int.Parse(e.CommandArgument.ToString()), true);
                    else
                        settingsDAL.ToggleShippingClass(int.Parse(e.CommandArgument.ToString()), false);
                    BindShippingClasses();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvShippingClasss_RowCommand");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvShippingClasss_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvShippingClasss.PageIndex = e.NewPageIndex;
                BindShippingClasses();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvShippingClasss_PageIndexChanging");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvShippingClasss_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lbStatus = ((LinkButton)e.Row.FindControl("lnkToggle"));

                    if (Convert.ToBoolean(((HiddenField)e.Row.FindControl("hfStatus")).Value) == true)
                    {
                        lbStatus.Text = "Active";
                    }
                    else
                    {
                        lbStatus.Text = "Inactive";
                        e.Row.Cells[1].BackColor = Color.FromArgb(int.Parse("993300", System.Globalization.NumberStyles.HexNumber));
                        e.Row.Cells[1].ForeColor = Color.White;
                        lbStatus.BackColor = Color.FromArgb(int.Parse("993300", System.Globalization.NumberStyles.HexNumber));
                        lbStatus.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvShippingClasss_RowDataBound");
                lblErr.Text = strErrCode;
            }

        }

        protected void lbAddShippingClass_Click(object sender, EventArgs e)
        {
            try
            {
                ucAddShippingClass.Popup();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lbAddShippingClass_Click");
                lblErr.Text = strErrCode;
            }
            //lblErr.Text = "add a shipping method";
        }

        protected void ucAddShippingClass_SaveButtonClicked(object sender, EventArgs e)
        {
            BindShippingClasses();
        }

        protected void ucEditShippingClass_SaveButtonClicked(object sender, EventArgs e)
        {
            BindShippingClasses();
        }
    }
}