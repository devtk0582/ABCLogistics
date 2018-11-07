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
    public partial class Packagings : System.Web.UI.Page
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ViewState["SortExpression"] = "Package_Name";
                    ViewState["SortDirection"] = "ASC";
                    hfCurrentID.Value = "0";
                    BindPackagings();
                }
                catch (Exception ex)
                {
                    string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Page_Load");
                    lblErr.Text = strErrCode;
                }
            }
        }

        private void BindPackagings()
        {
            try
            {
                DataSet packagingList = (new SettingsDAL()).GetPackagings(0);
                if (packagingList != null && packagingList.Tables[0].Rows.Count > 0)
                {
                    DataView dvPackagings = packagingList.Tables[0].DefaultView;
                    if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
                        if (ViewState["SortExpression"].ToString() != string.Empty && ViewState["SortDirection"].ToString() != string.Empty)
                            dvPackagings.Sort = ViewState["SortExpression"].ToString() + " " + ViewState["SortDirection"].ToString();
                    gvPackagings.DataSource = dvPackagings;
                    gvPackagings.DataBind();
                }
                else
                {
                    gvPackagings.DataSource = null;
                    gvPackagings.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindPackagings");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvPackagings_Sorting(object sender, GridViewSortEventArgs e)
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
                gvPackagings.EditIndex = -1;
                BindPackagings();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvPackagings_Sorting");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvPackagings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditPackaging")
                {
                    hfCurrentID.Value = e.CommandArgument.ToString();
                    ucEditPackaging.Popup(e.CommandArgument.ToString());
                }
                else if (e.CommandName == "TogglePackaging")
                {
                    hfCurrentID.Value = e.CommandArgument.ToString();
                    SettingsDAL settingsDAL = new SettingsDAL();
                    if (((LinkButton)e.CommandSource).Text == "Inactive")
                        settingsDAL.TogglePackaging(int.Parse(e.CommandArgument.ToString()), true);
                    else
                        settingsDAL.TogglePackaging(int.Parse(e.CommandArgument.ToString()), false);
                    BindPackagings();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvPackagings_RowCommand");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvPackagings_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvPackagings.PageIndex = e.NewPageIndex;
                BindPackagings();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvPackagings_PageIndexChanging");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvPackagings_RowDataBound(object sender, GridViewRowEventArgs e)
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
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvPackagings_RowDataBound");
                lblErr.Text = strErrCode;
            }
        }

        protected void lbAddPackaging_Click(object sender, EventArgs e)
        {
            try
            {
                ucAddPackaging.Popup();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lbAddPackaging_Click");
                lblErr.Text = strErrCode;
            }
            //lblErr.Text = "add a shipping method";
        }

        protected void ucAddPackaging_SaveButtonClicked(object sender, EventArgs e)
        {
            BindPackagings();
        }

        protected void ucEditPackaging_SaveButtonClicked(object sender, EventArgs e)
        {
            BindPackagings();
        }
    }
}