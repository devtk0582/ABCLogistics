using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMSMaster.DAL;
using System.Web.UI.HtmlControls;

namespace CMSMaster
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ((HtmlTableCell)Master.FindControl("tdMenus")).Visible = false;
                    ((Label)Master.FindControl("CustomerName")).Text = "Customers";
                    ViewState["SortExpression"] = "CustName";
                    ViewState["SortDirection"] = "ASC";
                    txtSearchCustomer_AutoCompleteExtender.ContextKey = Session["UserID"].ToString();
                    BindCustomers(string.Empty);
                }
            }
            catch (Exception ex)
            {
                lblErrUser.Text = ex.Message;
            }
        }

        private void BindCustomers(string searchValue)
        {
            lblGVMsg.Text = "No Customer Found";
            try
            {
                DataSet customerList = new clsCustomer("").GetCustomers(0, searchValue, Session["UserID"].ToString());
                gvCustomers.DataSource = null;
                gvCustomers.DataBind();
                if (customerList == null)
                {
                    return;
                }
                lblGVMsg.Text = "";
                DataView dvCustomers = customerList.Tables[0].DefaultView;
                dvCustomers.RowFilter = "CMSCustomer=True";
                if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
                    if (ViewState["SortExpression"].ToString() != string.Empty && ViewState["SortDirection"].ToString() != string.Empty)
                        dvCustomers.Sort = ViewState["SortExpression"].ToString() + " " + ViewState["SortDirection"].ToString();
                gvCustomers.DataSource = dvCustomers;
                gvCustomers.DataBind();
            }
            catch (Exception ex)
            {
                lblErrUser.Text = ex.Message;
            }
        }

        protected void gvCustomers_Sorting(object sender, GridViewSortEventArgs e)
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
                gvCustomers.EditIndex = -1;
                BindCustomers(txtSearchCustomer.Text.Trim());
            }
            catch (Exception ex)
            {
                lblErrUser.Text = ex.Message;
            }
        }

        protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ShowDetails")
                {
                    LinkButton lbName = (LinkButton)e.CommandSource;
                    Session["CustomerID"] = e.CommandArgument.ToString();
                    Session["CustomerName"] = lbName.Text;
                    Session["DBName"] = ((HiddenField)((GridViewRow)lbName.NamingContainer).FindControl("hfDBName")).Value;
                    Response.Redirect("~/CustomerDetails.aspx");
                }
            }
            catch (Exception ex)
            {
                lblErrUser.Text = ex.Message;
            }
        }

        protected void gvCustomers_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.Header)
                    foreach (TableCell tc in e.Row.Cells)
                    {
                        if (tc.HasControls())
                        {
                            LinkButton lnk = (LinkButton)tc.Controls[0];
                            if (ViewState["SortExpression"].ToString() == lnk.CommandArgument)
                            {
                                Image img = new Image();
                                img.Width = 10;
                                img.Height = 10;
                                img.ImageUrl = "~/images/arrow_" + (ViewState["SortDirection"].ToString() == "ASC" ? "up" : "down") + ".gif";
                                tc.Controls.Add(new LiteralControl(" "));
                                tc.Controls.Add(img);
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                lblErrUser.Text = ex.Message;
            }
        }

        protected void gvCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCustomers.PageIndex = e.NewPageIndex;
                BindCustomers(txtSearchCustomer.Text.Trim());
            }
            catch (Exception ex)
            {
                lblErrUser.Text = ex.Message;
            }
        }

        protected void lbShowAll_Click(object sender, EventArgs e)
        {
            BindCustomers(string.Empty);
        }

        //protected void lbAddCustomer_Click(object sender, EventArgs e)
        //{
        //    lblGVMsg.Text = "Add Customer";
        //}

        protected void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchCustomer.Text.Trim() != "")
                {
                    BindCustomers(txtSearchCustomer.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                lblErrUser.Text = ex.Message;
            }
        }   
    }
}