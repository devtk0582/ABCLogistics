using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using CMS.DAL;
using System.Data;

namespace CMS.Catalog
{
    public partial class Manufacturers : System.Web.UI.Page
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ViewState["SortExpression"] = "Manufactures_Desc";
                    ViewState["SortDirection"] = "ASC";
                    hfCurrentID.Value = "0";
                    BindManufacturers();
                }
                catch (Exception ex)
                {
                    string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Page_Load");
                    lblErr.Text = strErrCode;
                }
            }
        }

        private void BindManufacturers()
        {
            try
            {
                DataSet ManufacturerList = (new SettingsDAL()).GetManufacturers(0);
                if (ManufacturerList != null && ManufacturerList.Tables[0].Rows.Count > 0)
                {
                    DataView dvManufacturers = ManufacturerList.Tables[0].DefaultView;
                    if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
                        if (ViewState["SortExpression"].ToString() != string.Empty && ViewState["SortDirection"].ToString() != string.Empty)
                            dvManufacturers.Sort = ViewState["SortExpression"].ToString() + " " + ViewState["SortDirection"].ToString();
                    gvManufacturers.DataSource = dvManufacturers;
                    gvManufacturers.DataBind();
                }
                else
                {
                    gvManufacturers.DataSource = null;
                    gvManufacturers.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindManufacturers");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvManufacturers_Sorting(object sender, GridViewSortEventArgs e)
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
                gvManufacturers.EditIndex = -1;
                BindManufacturers();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvManufacturers_Sorting");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvManufacturers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditManufacturer")
                {
                    hfCurrentID.Value = e.CommandArgument.ToString();
                    ucEditManufacturer.Popup(e.CommandArgument.ToString());
                }
                else if (e.CommandName == "ToggleManufacturer")
                {
                    hfCurrentID.Value = e.CommandArgument.ToString();
                    SettingsDAL settingsDAL = new SettingsDAL();
                    if (((LinkButton)e.CommandSource).Text == "Inactive")
                        settingsDAL.ToggleManufacturer(int.Parse(e.CommandArgument.ToString()), true);
                    else
                        settingsDAL.ToggleManufacturer(int.Parse(e.CommandArgument.ToString()), false);
                    BindManufacturers();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvManufacturers_RowCommand");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvManufacturers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvManufacturers.PageIndex = e.NewPageIndex;
                BindManufacturers();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvManufacturers_PageIndexChanging");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvManufacturers_RowDataBound(object sender, GridViewRowEventArgs e)
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
                        e.Row.Cells[1].BackColor = Color.FromArgb(int.Parse("993300", System.Globalization.NumberStyles.HexNumber));
                        e.Row.Cells[1].ForeColor = Color.White;
                        lbStatus.BackColor = Color.FromArgb(int.Parse("993300", System.Globalization.NumberStyles.HexNumber));
                        lbStatus.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvManufacturers_RowDataBound");
                lblErr.Text = strErrCode;
            }
        }

        protected void lbAddManufacturer_Click(object sender, EventArgs e)
        {
            try
            {
                ucAddManufacturer.Popup();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lbAddManufacturer_Click");
                lblErr.Text = strErrCode;
            }
            //lblErr.Text = "add a shipping method";
        }

        protected void ucAddManufacturer_SaveButtonClicked(object sender, EventArgs e)
        {
            BindManufacturers();
        }

        protected void ucEditManufacturer_SaveButtonClicked(object sender, EventArgs e)
        {
            BindManufacturers();
        }
    }
}