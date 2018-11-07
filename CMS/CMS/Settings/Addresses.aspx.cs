

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.DAL;
using System.Data;

namespace CMS.Settings
{
    public partial class Addresses : System.Web.UI.Page
    {

        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "company_name";
                ViewState["SortDirection"] = "ASC";
                BindGrid();

            }
        }



        #region "Private Members"

        void BindGrid()
        {

            try
            {

                DataSet dsAddresses = (new SettingsDAL()).GetAddressBook_DAL(0, string.Empty, 1, 10000);
                if (dsAddresses != null && dsAddresses.Tables[0].Rows.Count > 0)
                {
                    DataView dvAddresses = dsAddresses.Tables[0].DefaultView;
                    if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
                        if (ViewState["SortExpression"].ToString() != string.Empty && ViewState["SortDirection"].ToString() != string.Empty)
                            dvAddresses.Sort = ViewState["SortExpression"].ToString() + " " + ViewState["SortDirection"].ToString();
                    gvAddresses.DataSource = dvAddresses;
                    gvAddresses.DataBind();
                }
                else
                {
                    gvAddresses.DataSource = null;
                    gvAddresses.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindGrid");
                lblErr.Text = strErrCode; 
            }
        }

        #endregion



        protected void lbAddAddresses_Click(object sender, EventArgs e)
        {
            lblErr.Text = "";
            AddAddresses1.Popup(0);
            
        }
        protected void ucAddaddresses_SaveButtonClicked(object sender, EventArgs e)
        {
            BindGrid();
        }


        protected void gvAddresses_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
                if (e.CommandName == "EditMethod")
                {
                    AddAddresses1.Popup(Convert.ToInt32(e.CommandArgument.ToString()));
                }
                if (e.CommandName == "Delete")
                {
                    String strRetValue = (new SettingsDAL()).DeleteAddressBook_DAL( Convert.ToInt32(e.CommandArgument));
                    if (strRetValue != "Deleted")
                    {
                        lblErr.Text = strRetValue;
                    }
                    else
                    {
                        lblErr.Text = "Record deleted successfully ";
                        BindGrid();
                    }               
                }
           
        }

        protected void gvAddresses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAddresses.PageIndex = e.NewPageIndex;
            BindGrid();
        }


        protected void gvAddresses_Sorting(object sender, GridViewSortEventArgs e)
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
                gvAddresses.EditIndex = -1;
                BindGrid();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvAddresses_Sorting");
                lblErr.Text = strErrCode; 
            }
        }

        protected void gvAddresses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvAddresses_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {            
                LinkButton lnkdelete = ((LinkButton)e.Row.FindControl("lnkdelete"));
                lnkdelete.Attributes.Add("onclick", "javascript:if(ValidDelete()==false){return false;}");
            }
        }



    }
}