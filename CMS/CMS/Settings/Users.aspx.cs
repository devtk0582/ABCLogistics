


/*
'*   Date Created: 04/24/2012
'*
'*   Created By : Phani Vutla
'*
'*   Purpose: To Assign  Users  and Create new users based on customer
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

#endregion

namespace CMS.Settings
{
    public partial class Users : System.Web.UI.Page
    {

        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        #region "Page Events"

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "user_email";
                ViewState["SortDirection"] = "ASC";
                BindGrid();

            }
        }

        #endregion

        #region "Private Members"

        void BindGrid()
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
                DataSet dsusers = (new UsersDAL()).GetUsersList(0, roles.ToString());
                if (dsusers != null && dsusers.Tables[0].Rows.Count > 0)
                {
                    DataView dvusers = dsusers.Tables[0].DefaultView;
                    if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
                        if (ViewState["SortExpression"].ToString() != string.Empty && ViewState["SortDirection"].ToString() != string.Empty)
                            dvusers.Sort = ViewState["SortExpression"].ToString() + " " + ViewState["SortDirection"].ToString();
                    gvUsers.DataSource = dvusers;
                    gvUsers.DataBind();
                }
                else
                {
                    gvUsers.DataSource = null;
                    gvUsers.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindGrid");
                lblErr.Text = strErrCode;   
            }
        }

        #endregion

        #region "Controls Events"

        protected void lbAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                AddUser1.Popup(0);
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lbAddUser_Click");
                lblErr.Text = strErrCode;   
            }

        }

        protected void ucAddUser_SaveButtonClicked(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditMethod")
            {
                AddUser1.Popup(Convert.ToInt32(e.CommandArgument.ToString()));
            }
        }

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            BindGrid();
        }


        protected void gvUsers_Sorting(object sender, GridViewSortEventArgs e)
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
                gvUsers.EditIndex = -1;
                BindGrid();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvUsers_Sorting");
                lblErr.Text = strErrCode;   
            }
        }

        #endregion


    }
}