using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.DAL;

namespace CMS.Orders
{
    public partial class Orders : System.Web.UI.Page
    {
        string strWhere = string.Empty;
        string strWhereClouse = string.Empty;
        string strWhereClause = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (Request.QueryString["where"] != null)
                {
                    strWhere = Request.QueryString["where"].ToString();
                    ViewState["strWhere"] = Request.QueryString["where"].ToString();

                    strWhereClouse = Request.QueryString["Type"].ToString();
                    ViewState["Type"] = Request.QueryString["Type"].ToString();
                    trSerchBar.Visible = false;
                }
                else 
                { trSerchBar.Visible = true; }

                ViewState["SortExpression"] = "ordernum";
                ViewState["SortDirection"] = "ASC";
                BindOders();
            }

            lblErr.Visible = false;
            lblErr.Text = string.Empty;
        }

        protected void lbGoSerch_Click(object sender, EventArgs e)
        {
           
            if (IsNumeric(txtOrderID.Text.Trim()))
            {
                    //ViewState["Type"] = null;
                   // ViewState["strWhere"] = null;
                    BindOders();
            }
            else
            {
                lblErr.Visible = true;
                lblErr.Text = "Plese Enter Valid Order ID";
            }

        }

        protected void BindOders()
        {

            //if (ViewState["Type"] != null)
            //{
            //    string strType = ViewState["Type"].ToString();
            //    ddlStatus.SelectedValue = strType;
            //}

            try
            {
                
                    if (ddlStatus.SelectedValue == "New")
                    {
                        if (strWhereClause.Trim() == string.Empty)
                        {
                            strWhereClause = strWhereClause + " Where status = '" + ddlStatus.SelectedValue + "'";

                        }
                        else
                        {
                            strWhereClause = strWhereClause + " and status = '" + ddlStatus.SelectedValue + "'";
                        }
                    }
                    else if (ddlStatus.SelectedValue == "Processing")
                    {
                        if (strWhereClause.Trim() == string.Empty)
                        {
                            strWhereClause = strWhereClause + " Where status = '" + ddlStatus.SelectedValue + "' and Confirmed = 0";
                        }
                        else
                        {
                            strWhereClause = strWhereClause + " and status = '" + ddlStatus.SelectedValue + "' and Confirmed = 0";
                        }
                    }
                    else if (ddlStatus.SelectedValue == "Shipped")
                    {
                        if (strWhereClause.Trim() == string.Empty)
                        {
                            strWhereClause = strWhereClause + " Where status = '" + ddlStatus.SelectedValue + "' and Confirmed = 1";
                        }
                        else
                        {
                            strWhereClause = strWhereClause + " and status = '" + ddlStatus.SelectedValue + "' and Confirmed = 1";
                        }
                    }
                    else if (ddlStatus.SelectedValue == "Delivered")
                    {
                        if (strWhereClause.Trim() == string.Empty)
                        {
                            strWhereClause = strWhereClause + " Where status = '" + ddlStatus.SelectedValue + "'";
                        }
                        else
                        {
                            strWhereClause = strWhereClause + " and status = '" + ddlStatus.SelectedValue + "'";
                        }
                    }
                    else if (ddlStatus.SelectedValue == "Cancelled")
                    {
                        if (strWhereClause.Trim() == string.Empty)
                        {
                            strWhereClause = strWhereClause + " Where status = '" + ddlStatus.SelectedValue + "'";
                        }
                        else
                        {
                            strWhereClause = strWhereClause + " and status = '" + ddlStatus.SelectedValue + "'";
                        }
                    }
                
                //else if (ddlStatus.SelectedValue == "Closed")
                //{
                //    if (strWhereClause.Trim() == string.Empty)
                //    {
                //        strWhereClause = strWhereClause + " Where status = '" + ddlStatus.SelectedValue + "'";
                //    }
                //    else
                //    {
                //        strWhereClause = strWhereClause + " and status = '" + ddlStatus.SelectedValue + "'";
                //    }
                //}


                if (txtOrderID.Text.Trim() != string.Empty)
                {
                    if (strWhereClause.Trim() == string.Empty)
                    {
                        strWhereClause = strWhereClause + " Where ordernum = " + txtOrderID.Text.Trim();
                    }
                    else
                    {
                        strWhereClause = strWhereClause + " and ordernum = " + txtOrderID.Text.Trim();
                    }

                }

                if (ViewState["strWhere"] != null)
                {
                    strWhereClouse = ViewState["Type"].ToString();
                    if (strWhereClause.Trim() == string.Empty)
                    {
                        if (ViewState["strWhere"].ToString() == "c")
                        {
                            strWhereClause = strWhereClause + " Where customer='" + strWhereClouse + "'";
                        }
                        if (ViewState["strWhere"].ToString() == "p")
                        {
                            strWhereClause = strWhereClause + " Where Product='" + strWhereClouse.Replace("'","''") + "'";
                        }
                        if (ViewState["strWhere"].ToString() == "m")
                        {
                            strWhereClause = strWhereClause + " Where CONVERT(CHAR(4),CONVERT(datetime, order_date), 100) + CONVERT(CHAR(4), CONVERT(datetime, order_date), 120)='" + strWhereClouse + "'";
                        }
                        if (ViewState["strWhere"].ToString() == "s")
                        {
                            strWhereClause = strWhereClause + " Where status ='" + strWhereClouse + "'";
                        }
                    }
                    else
                    {

                        if (ViewState["strWhere"].ToString() == "c")
                        {
                            strWhereClause = strWhereClause + " and customer='" + strWhereClouse + "'";
                        }
                        if (ViewState["strWhere"].ToString() == "p")
                        {
                            strWhereClause = strWhereClause + " and Product='" + strWhereClouse + "'";
                        }
                        if (ViewState["strWhere"].ToString() == "m")
                        {
                            strWhereClause = strWhereClause + " and CONVERT(CHAR(4),CONVERT(datetime, order_date), 100) + CONVERT(CHAR(4), CONVERT(datetime, order_date), 120)='" + strWhereClouse + "'";
                        }
                        if (ViewState["strWhere"].ToString() == "s")
                        {
                            strWhereClause = strWhereClause + " and status ='" + strWhereClouse + "'";
                        }
                    }

                }

                string dbName =  System.Configuration.ConfigurationSettings.AppSettings["dbname"].ToString();


                DataSet Orders = (new AddCartsDAL()).GetOrder_DAL(strWhereClause, Convert.ToInt32(Session["UserID"].ToString()), dbName, string.Empty);

                if (Orders != null && Orders.Tables[0].Rows.Count > 0)
                {
                    DataView dvOrders = Orders.Tables[0].DefaultView;
                    if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
                        if (ViewState["SortExpression"].ToString() != string.Empty && ViewState["SortDirection"].ToString() != string.Empty)
                            dvOrders.Sort = ViewState["SortExpression"].ToString() + " " + ViewState["SortDirection"].ToString();
                    gvOrders.DataSource = dvOrders;
                    gvOrders.DataBind();
                }
                else
                {
                    gvOrders.DataSource = null;
                    gvOrders.DataBind();
                    gvOrders.EmptyDataText = "No Record Found.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured while trying to bind the data {0}", ex.Message);
            }


        }

        protected void gvOrders_Sorting(object sender, GridViewSortEventArgs e)
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
                gvOrders.EditIndex = -1;
                BindOders();
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void gvOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvOrders.PageIndex = e.NewPageIndex;
                BindOders();
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        public static bool IsNumeric(string value)
        {
            foreach (char c in value)
                if (!((Int16)c > 47 && (Int16)c < 58)) return false;
            return true;
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)

        {

            if (e.CommandName == "OrderDetail")

            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);


                int iOrderId = Convert.ToInt32(e.CommandArgument);
                Label lblCustomer = (Label)row.FindControl("lblCustomer");
                Label lblStatus = (Label)row.FindControl("lblStatus");

                Response.Redirect("OrderDetail.aspx?OrderId=" + iOrderId + "&DbName=" + lblCustomer.Text + "&Status=" + lblStatus.Text);
            }

        }

        protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
                 if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                    if (lblTotal.Text == "")
                    {
                        lblTotal.Text = "$0";
                    }
                    else
                    {
                        lblTotal.Text = "$" + lblTotal.Text;
                    }
                   

                }
        }

     

    }
}