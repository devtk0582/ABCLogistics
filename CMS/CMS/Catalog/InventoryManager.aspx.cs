using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using CMS.DAL;

namespace CMS.Catalog
{
    public partial class InventoryManager : System.Web.UI.Page
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    if (Session["RoleName"] != null && Session["RoleName"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["EComAdmin"].ToString())
                    {
                        lbSwitchRole.Visible = true;
                    }
                    else if (Session["RoleName"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["SuperAdmin"].ToString())
                    {
                        lbSwitchRole.Visible = true;
                    }

                    if (Session["OrderRoleName"] != null)
                    {
                        if (Session["OrderRoleName"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["EComAdmin"].ToString())
                        {
                            AdminProducts.Visible = true;
                            SalesRepProducts.Visible = false;
                            populateDDL();
                            BindProducts(0, 0);
                            lbSwitchRole.Text = "Log In As Sales Representative";
                        }
                        else if (Session["OrderRoleName"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["SuperAdmin"].ToString())
                        {
                            AdminProducts.Visible = true;
                            SalesRepProducts.Visible = false;
                            populateDDL();
                            BindProducts(0, 0);
                            lbSwitchRole.Text = "Log In As Sales Representative";
                        }
                        else
                        {
                            ViewState["SortExpression"] = "ProductName";
                            ViewState["SortDirection"] = "ASC";
                            AdminProducts.Visible = false;
                            SalesRepProducts.Visible = true;
                            BindInventories(0);
                            BindDDLProducts();
                            lbSwitchRole.Text = "Log In As Administrator";
                        }
                    }
                }
                catch (Exception ex)
                {
                    string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Page_Load");
                    lblErr.Text = strErrCode;
                }
                lblErr.Visible = false;
                lblErr.Text = string.Empty;
            }
        }

        /*
         * Populate dropdown list for Category and Manufacturer
         */
        private void populateDDL()
        {
            try
            {
                DataTable categoryNameDDL = (new CatalogInventoryManagerDAL().GetCategoryName_DAL()).Tables[0];

                if (categoryNameDDL != null)
                {
                    if (categoryNameDDL.Rows.Count > 0)
                    {
                        ddlCategoryName.DataSource = categoryNameDDL;
                        ddlCategoryName.DataTextField = "Category_Name";
                        ddlCategoryName.DataValueField = "Category_Id";
                        ddlCategoryName.DataBind();
                    }
                }
                ddlCategoryName.Items.Insert(0, new ListItem("Select", "0"));

                DataTable manufacturerDDL = (new CatalogInventoryManagerDAL().GetManufactureDescription_DAL()).Tables[0];

                if (manufacturerDDL != null)
                {
                    if (manufacturerDDL.Rows.Count > 0)
                    {
                        ddlManufacture.DataSource = manufacturerDDL;
                        ddlManufacture.DataTextField = "Manufactures_Desc";
                        ddlManufacture.DataValueField = "Manufactures_Id";
                        ddlManufacture.DataBind();
                    }
                }
                ddlManufacture.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "populateDDL");
                lblErr.Text = strErrCode;
            }
        }//End of populateDDL...

        /*
         * Bind Product.
         */
        private void BindProducts(int categoryID, int mfrID)
        {
            DataSet bindInventory = null;

            try
            {
                gridViewInventory.DataSource = null;
                gridViewInventory.DataBind();

                bindInventory = (new CatalogInventoryManagerDAL().GetInvenotry_DAL(categoryID, mfrID));

                if (bindInventory != null)
                {
                    if (bindInventory.Tables[0].Rows.Count > 0)
                    {
                        DataView dv = bindInventory.Tables[0].DefaultView;
                        if (ViewState["sortBy"] != null)
                        {
                            dv.Sort = ViewState["sortBy"].ToString();
                        }
                        gridViewInventory.DataSource = dv;
                        gridViewInventory.DataBind();
                    }
                    else
                    {
                        gridViewInventory.DataSource = null;
                        gridViewInventory.DataBind();
                    }
                }
                else
                {
                    lblNoProduct.Text = "No Product found.";
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindProducts", categoryID.ToString(), mfrID.ToString());
                lblErr.Text = strErrCode;
            }
        }//End of BindProducts method...


        /*
         * Sort the Sizes by column
         */
        protected void GridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                ViewState["sortBy"] = e.SortExpression + " " + GetSortDirection(e.SortExpression); ;
                int categoryID = Convert.ToInt32(ddlCategoryName.SelectedValue);
                int mfrID = Convert.ToInt32(ddlManufacture.SelectedValue);
                BindProducts(categoryID, mfrID);
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "GridView_Sorting");
                lblErr.Text = strErrCode;
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

        protected void gridViewInventory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gridViewInventory.PageIndex = e.NewPageIndex;
                gridViewInventory.DataBind();

                int categoryID = Convert.ToInt32(ddlCategoryName.SelectedValue);
                int mfrID = Convert.ToInt32(ddlManufacture.SelectedValue);
                BindProducts(categoryID, mfrID);
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gridViewInventory_PageIndexChanging");
                lblErr.Text = strErrCode;
            }
        }
        /*
         * Populate DDL when onlick 
         * Add new product button.
         */
        protected void AddNewProduct_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("EditProduct.aspx?from=" + "IMngr");
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "AddNewProduct_Click");
                lblErr.Text = strErrCode;
            }
        }

        protected void filterProduct_Click(object sender, EventArgs e)
        {
            try
            {
                int categoryID = Convert.ToInt32(ddlCategoryName.SelectedValue);
                int mfrID = Convert.ToInt32(ddlManufacture.SelectedValue);
                BindProducts(categoryID, mfrID);
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "filterProduct_Click");
                lblErr.Text = strErrCode;
            }
        }


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
                                      "&from=" + "IMngr");
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
                if (e.CommandName == "ViewProduct")
                {
                    Session["SubMenu"] = "Products";
                    //lblErr.Text = e.CommandArgument.ToString() + " is chosen";
                    Response.Redirect("~/Catalog/ProductSalesRep.aspx?id=" + e.CommandArgument.ToString());

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
                    string stockQty = ((TableCell)e.Row.Cells[4]).Text.Trim();
                    if (stockQty == "0")
                    {
                        ((TableCell)e.Row.Cells[5]).Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "gvInventories_RowDataBound");
                lblErr.Text = strErrCode;
            }
            //    LinkButton lbStatus = ((LinkButton)e.Row.FindControl("lnkToggle"));
            //    LinkButton lbName = ((LinkButton)e.Row.FindControl("lnkName"));
            //    if (Convert.ToBoolean(((HiddenField)e.Row.FindControl("hfStatus")).Value) == true)
            //    {
            //        lbStatus.Text = "Inactivate";
            //    }
            //    else
            //    {
            //        lbStatus.Text = "Activate";
            //        e.Row.BackColor = Color.FromArgb(int.Parse("993300", System.Globalization.NumberStyles.HexNumber));
            //        e.Row.ForeColor = Color.White;
            //        lbStatus.BackColor = Color.FromArgb(int.Parse("993300", System.Globalization.NumberStyles.HexNumber));
            //        lbStatus.ForeColor = Color.White;
            //        lbName.BackColor = Color.FromArgb(int.Parse("993300", System.Globalization.NumberStyles.HexNumber));
            //        lbName.ForeColor = Color.White;
            //    }
            //}
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

        protected void lnkbAddtoCart_Click(object sender, EventArgs e)
        {
            bool flag = true;
            try
            {
                foreach (GridViewRow gr in gvInventories.Rows)
                {
                    TextBox txtInerQty = (TextBox)gr.Cells[0].FindControl("txtQty");

                    Label lblQtyStock = (Label)gr.Cells[0].FindControl("lblQty");
                    LinkButton lnkProduct = (LinkButton)gr.Cells[0].FindControl("lnkName");
                    Label lblSizeID = (Label)gr.Cells[0].FindControl("lblSizeID");
                    Label lblColorID = (Label)gr.Cells[0].FindControl("lblColorID");

                    int iStockQty = 0;
                    int iNewQty = 0;

                    iStockQty = Convert.ToInt32(lblQtyStock.Text.Trim());
                    iNewQty = Convert.ToInt32(txtInerQty.Text.Trim());

                    if (IsNumeric(txtInerQty.Text.Trim()))
                    {

                        if (iNewQty > 0)
                        {
                            if (iNewQty <= iStockQty)
                            {
                                string strParName = lnkProduct.Text.Trim();

                                DataSet DftwhNum = (new AddCartsDAL()).GetDefaultCMSWarehouses();
                                string strwhse = DftwhNum.Tables[0].Rows[0]["Whse_Id"].ToString();

                                AddTempParts(strParName, iNewQty, strwhse, Convert.ToInt32(lblColorID.Text.Trim()), Convert.ToInt32(lblSizeID.Text.Trim()));
                            }
                            else
                            {
                                lblErr.Visible = true;
                                lblErr.Text = "Sorry, Enter Quantity is not currently available. Please try with less Quantity.";
                                flag = false;

                            }
                        }
                    }

                }
                if (flag)
                {
                    Response.Redirect("CartAdd.aspx", false);
                    Session["SubMenu"] = "View Cart";
                }
                else
                {
                    lblErr.Visible = true;
                    lblErr.Text = "Sorry, Enter Quantity is not currently available. Please try with less Quantity.";
                    int x = (new AddCartsDAL()).DeleteTempPartsDocuments(Session.SessionID, 0);
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lnkbAddtoCart_Click");
                lblErr.Text = strErrCode;
            }

        }

        public static bool IsNumeric(string value)
        {
            foreach (char c in value)
                if (!((Int16)c > 47 && (Int16)c < 58)) return false;
            return true;
        }

        private void AddTempParts(string PartName, int strAvaQty, string strWhsenum, int ColorID, int SizeID)
        {
            try
            {
                int x = (new AddCartsDAL()).AddTempParts_DAL(Session.SessionID, PartName, strAvaQty, Session["UserID"].ToString(), strWhsenum, ColorID, SizeID);
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "AddTempParts", PartName, strAvaQty.ToString(),
                                                                   strWhsenum, ColorID.ToString(), SizeID.ToString());
                lblErr.Text = strErrCode;
            }
        }
        #endregion

        protected void lbSwitchRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["OrderRoleName"] != null)
                {
                    if (Session["OrderRoleName"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["EComAdmin"].ToString())
                    {
                        Session["OrderRoleName"] = System.Configuration.ConfigurationSettings.AppSettings["EComUser"].ToString();
                        lbSwitchRole.Text = "Log In As Administrator";
                    }
                    else if (Session["OrderRoleName"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["SuperAdmin"].ToString())
                    {
                        Session["OrderRoleName"] = System.Configuration.ConfigurationSettings.AppSettings["EComUser"].ToString();
                        lbSwitchRole.Text = "Log In As Administrator";
                    }
                    else
                    {
                        Session["OrderRoleName"] = System.Configuration.ConfigurationSettings.AppSettings["EComAdmin"].ToString();
                    }
                    Response.Redirect("~/Catalog/InventoryManager.aspx");
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lbSwitchRole_Click");
                lblErr.Text = strErrCode;
            }
        }

    }

}