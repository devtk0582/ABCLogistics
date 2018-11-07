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
    public partial class InventoryReport : System.Web.UI.Page
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ViewState["SortExpression"] = "ProductName";
                    ViewState["SortDirection"] = "ASC";
                    SalesRepProducts.Visible = true;
                    BindInventories(0);
                    BindDDLProducts();
                }
                catch (Exception ex)
                {
                    string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Page_Load");
                    lblErr.Text = strErrCode;
                }
            }
        }

        /*
         *Get the ACS or DES sorting direction.
         */
        private string GetSortDirection(string sortColumn)
        {
            try
            {
                if (ViewState["SortExpression"] == null)
                {
                    ViewState["SortExpression"] = "Desc";
                }
                else
                {
                    ViewState["SortExpression"] = ViewState["SortExpression"].ToString() == "Desc" ? "Asc" : "Desc";
                }

                return ViewState["SortExpression"].ToString();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetSortDirection", sortColumn);
                lblErr.Text = strErrCode;
            }
            return ViewState["SortExpression"].ToString();
        }//End of GetSortDirection...


        /*
         * Populate DDL when onlick 
         * Add new product button.
         */
        //protected void AddNewProduct_Click(object sender, EventArgs e)
        //{

        //    Response.Redirect("EditProduct.aspx?from=" + "InventoryManager");
        //}




        /*
         * Click on Edit to edit the Product. 
         */
        protected void GridView_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "editProduct")
                {   // Use query string to pass value to Edit page...
                    int id = Convert.ToInt32(e.CommandArgument);

                    Response.Redirect("EditProduct.aspx?id=" + id +
                                        "&from=" + "InventoryManager");
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "GridView_OnRowCommand");
                lblErr.Text = strErrCode;
            }
        }//End of GridView_OnRowCommand... 


        #region Ke Salesman's Inventory Manager

        private void BindInventories(int productId)
        {
            try
            {
                DataSet inventories = (new CatalogInventoryManagerDAL()).GetProductInventory(productId);
                if (inventories != null && inventories.Tables[0].Rows.Count > 0)
                {
                    DataView dvInventories = inventories.Tables[0].DefaultView;
                    if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
                        if (ViewState["SortExpression"].ToString() != string.Empty && ViewState["SortDirection"].ToString() != string.Empty)
                            dvInventories.Sort = ViewState["SortExpression"].ToString() + " " + ViewState["SortDirection"].ToString();
                    gvInventories.DataSource = dvInventories;
                    gvInventories.DataBind();
                }
                else
                {
                    gvInventories.DataSource = null;
                    gvInventories.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindInventories", productId.ToString());
                lblErr.Text = strErrCode;
            }
        }

        protected void gvInventories_Sorting(object sender, GridViewSortEventArgs e)
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
                gvInventories.EditIndex = -1;
                BindInventories(int.Parse(ddlProducts.SelectedValue));
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvInventories_Sorting");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvInventories_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ViewSKU")
                {   // Use query string to pass value to Edit page...
                    int id = Convert.ToInt32(e.CommandArgument);

                    if (Session["OrderRoleName"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["EComAdmin"].ToString())
                    {
                        Response.Redirect("~/Catalog/EditProduct.aspx?id=" + id +
                                            "&from=" + "IReprt");
                    }
                    else if (Session["OrderRoleName"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["SuperAdmin"].ToString())
                    {
                        Response.Redirect("~/Catalog/EditProduct.aspx?id=" + id +
                                           "&from=" + "IReprt");
                    }
                    else
                    {
                        Response.Redirect("~/Catalog/ProductSalesRep.aspx?id=" + e.CommandArgument.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvInventories_RowCommand");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvInventories_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvInventories.PageIndex = e.NewPageIndex;
                BindInventories(int.Parse(ddlProducts.SelectedValue));
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvInventories_PageIndexChanging");
                lblErr.Text = strErrCode;
            }
        }

        protected void gvInventories_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string stockQty = ((TableCell)e.Row.Cells[3]).Text.Trim();
                    if (stockQty == "0")
                    {
                        ((TableCell)e.Row.Cells[4]).Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvInventories_RowDataBound");
                lblErr.Text = strErrCode;
            }
        }

        private void BindDDLProducts()
        {
            try
            {
                DataSet products = (new ProductsDAL()).GetDDLProducts();
                if (products != null && products.Tables[0].Rows.Count > 0)
                {
                    ddlProducts.DataSource = products;
                    ddlProducts.DataTextField = "ProductName";
                    ddlProducts.DataValueField = "ProductID";
                    ddlProducts.DataBind();
                }
                else
                {
                    ddlProducts.DataSource = null;
                    ddlProducts.DataBind();
                }
                ddlProducts.Items.Insert(0, new ListItem("Select Product", "0"));
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindDDLProducts");
                lblErr.Text = strErrCode;
            }
        }



        protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindInventories(int.Parse(ddlProducts.SelectedValue));
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "ddlProducts_SelectedIndexChanged");
                lblErr.Text = strErrCode;
            }
        }
        #endregion
    }
}