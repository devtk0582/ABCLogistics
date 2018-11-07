using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.DAL;

namespace CMS.Reports
{
    public partial class SalesReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsPostBack ))
            {
                ViewState["SortExpression"] = "Total";
                ViewState["SortDirection"] = "ASC";
                BindGvSalesReport(ddlSalesReport.SelectedValue);
            }
        }

       public void  BindGvSalesReport(string strGroupBy)
        {
            try
            {
                string dbName = System.Configuration.ConfigurationSettings.AppSettings["dbname"].ToString();
                DataSet Orders = (new AddCartsDAL()).GetOrder_DAL(string.Empty, Convert.ToInt32(Session["UserID"].ToString()), dbName, strGroupBy);
                DataView dvOrders = Orders.Tables[0].DefaultView;
                if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
                    if (ViewState["SortExpression"].ToString() != string.Empty && ViewState["SortDirection"].ToString() != string.Empty)
                        dvOrders.Sort = ViewState["SortExpression"].ToString() + " " + ViewState["SortDirection"].ToString();
                

                gvSalesReport.DataSource = null;
                gvSalesReport.DataBind();

                if (Orders.Tables[0].Rows.Count>0)
                {
                    gvSalesReport.DataSource = dvOrders;
                    gvSalesReport.DataBind();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

       protected void ddlSalesReport_SelectedIndexChanged(object sender, EventArgs e)
       {
           try
           {
               ViewState["SortExpression"] = "Total";
               ViewState["SortDirection"] = "ASC";
               if (ddlSalesReport.SelectedItem.Value != "")
               {
                   BindGvSalesReport(Convert.ToString(ddlSalesReport.SelectedItem.Value));
               }
               else
               {
                   gvSalesReport.DataSource = null;
                   gvSalesReport.DataBind();
               }
           }
           catch (Exception)
           {
               
               throw;
           }
           
       }

       protected void gvSalesReport_RowCommand(object sender, GridViewCommandEventArgs e)
       {
           //GridViewRow Row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
           //if (Row.RowType  == DataControlRowType.DataRow)
           //{
           //    if (ddlSalesReport.SelectedValue=="Customer")
           //    {
           //        Row.Attributes["onClick"] = "location.href='../Order/Order.aspx?id=" + DataBinder.Eval(Row.DataItem, "id") + "'";
           //    }
           //    if (ddlSalesReport.SelectedValue == "Month")
           //    {
           //        Row.Attributes["onClick"] = "location.href='../Order/Order.aspx?id=" + DataBinder.Eval(Row.DataItem, "id") + "'";
           //    }
           //    if (ddlSalesReport.SelectedValue == "Status")
           //    {
           //        Row.Attributes["onClick"] = "location.href='../Order/Order.aspx?id=" + DataBinder.Eval(Row.DataItem, "id") + "'";
           //    }
           //    if (ddlSalesReport.SelectedValue == "Products")
           //    {
           //        Row.Attributes["onClick"] = "location.href='../Order/Order.aspx?id=" + DataBinder.Eval(Row.DataItem, "id") + "'";
           //    }
           //}

       }

       protected void gvSalesReport_RowDataBound(object sender, GridViewRowEventArgs e)
       {
           try
           {
               if (e.Row.RowType==DataControlRowType.DataRow)
               {
                                 
               if (ddlSalesReport.SelectedValue=="Status")
               {
                   e.Row.Attributes.Add("onclick", "location.href='../Orders/Orders.aspx?where=s&Type="+ e.Row.Cells[0].Text +"'");
               }
               if (ddlSalesReport.SelectedValue == "Month")
               {
                   e.Row.Attributes.Add("onclick", "location.href='../Orders/Orders.aspx?where=m&Type=" + e.Row.Cells[0].Text + "'");
               }
               if (ddlSalesReport.SelectedValue == "Customer")
               {
                   e.Row.Attributes.Add("onclick", "location.href='../Orders/Orders.aspx?where=c&Type=" + e.Row.Cells[0].Text + "'");
               }
               if (ddlSalesReport.SelectedValue == "Products")
               {
                   e.Row.Attributes.Add("onclick", "location.href='../Orders/Orders.aspx?where=p&Type=" + e.Row.Cells[0].Text + "'");
               }


               e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#ceedfc'");
               e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
               e.Row.Attributes.Add("style", "cursor:pointer;");

               if (ddlSalesReport.SelectedValue == "Customer")
               {
                   if (e.Row.RowType == DataControlRowType.Header)
                   {
                       ((TableCell)e.Row.Cells[1]).Visible = false;
                       
                   }
                   if (e.Row.RowType == DataControlRowType.DataRow)
                   {
                       ((TableCell)e.Row.Cells[1]).Visible = false;
                       if (((TableCell)e.Row.Cells[2]).Text == "&nbsp;")
                       {
                           ((TableCell)e.Row.Cells[2]).Text = "$0";
                       }
                       else
                       {
                           ((TableCell)e.Row.Cells[2]).Text = "$" + ((TableCell)e.Row.Cells[2]).Text;
                       }
                      
                   }
               }
               if (ddlSalesReport.SelectedValue == "Status" || ddlSalesReport.SelectedValue == "Month")
               {
                   
                   if (e.Row.RowType == DataControlRowType.DataRow)
                   {
                       if (((TableCell)e.Row.Cells[1]).Text == "&nbsp;")
                       {
                           ((TableCell)e.Row.Cells[1]).Text = "$0";
                       }
                       else
                       {
                           ((TableCell)e.Row.Cells[1]).Text = "$" + ((TableCell)e.Row.Cells[1]).Text;
                       }
                       
                   }
               }

               if (ddlSalesReport.SelectedValue == "Products")
               {

                   if (e.Row.RowType == DataControlRowType.DataRow)
                   {
                       if (((TableCell)e.Row.Cells[3]).Text == "&nbsp;")
                       {
                           ((TableCell)e.Row.Cells[3]).Text = "$0";
                       }
                       else
                       {
                           ((TableCell)e.Row.Cells[3]).Text = "$" + ((TableCell)e.Row.Cells[3]).Text;
                       }
                       
                   }
               }
              }
               if (ddlSalesReport.SelectedValue == "Customer")
               {
                   if (e.Row.RowType == DataControlRowType.Header)
                   {
                       ((TableCell)e.Row.Cells[1]).Visible = false;

                   }
                  
               }
           }
           catch (Exception)
           {
               
               throw;
           }
          
       }

       protected void gvSalesReport_Sorting(object sender, GridViewSortEventArgs e)
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
               gvSalesReport.EditIndex = -1;
               BindGvSalesReport(ddlSalesReport.SelectedValue );
           }
           catch (Exception ex)
           {
               throw;
           }
       }

       public static bool IsNumeric(string value)
       {
           try
           {
               foreach (char c in value)
                   if (!((Int16)c > 47 && (Int16)c < 58)) return false;
               return true;
           }
           catch (Exception)
           {
               
               throw;
           }
           
       }

       protected void gvSalesReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
       {
           try
           {
               gvSalesReport.PageIndex = e.NewPageIndex;
               BindGvSalesReport(ddlSalesReport.SelectedValue);
           }
           catch (Exception ex)
           {
               throw;
           }
       }
    }
}