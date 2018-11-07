using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.DAL;
using System.Drawing;

namespace CMS.Catalog
{
    public partial class Products : System.Web.UI.Page
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    BindPageUI();
                    BindCategories();
                    BindProducts(0, true, "MAN", string.Empty);
                    ViewState["SearchValue"] = string.Empty;
                    ViewState["Subcategory"] = "0";
                    ViewState["IsSub"] = true;
                    ViewState["OrderBy"] = "MAN";

                    if (Session["RoleName"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["EComAdmin"].ToString())
                    {
                        //lbAddProduct.Visible = true;
                        lbSwitchRole.Visible = true;
                        if (Session["OrderRoleName"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["EComAdmin"].ToString())
                        {
                            lbSwitchRole.Text = "Log In As Sales Representative";
                        }
                        else
                        {
                            lbSwitchRole.Text = "Log In As Administrator";
                        }
                    }
                    else if (Session["RoleName"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["SuperAdmin"].ToString())
                    {
                        lbSwitchRole.Visible = true;
                        if (Session["OrderRoleName"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["SuperAdmin"].ToString())
                        {
                            lbSwitchRole.Text = "Log In As Sales Representative";
                        }
                        else if (Session["OrderRoleName"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["EComAdmin"].ToString())
                        {
                            lbSwitchRole.Text = "Log In As Sales Representative";
                        }
                        else
                        {
                            lbSwitchRole.Text = "Log In As Administrator";
                        }
                    }
                }
                catch (Exception ex)
                {
                    string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Page_Load");
                    lblErr.Text = strErrCode;
                }
            }
        }

        private void BindPageUI()
        {
            try
            {
                if (Session["CustomerUI"] != null)
                {
                    CustomerUI customerUI = (CustomerUI)Session["CustomerUI"];
                    lblSideBar.Text = customerUI.SideBarMessage.Replace("\n", "<br />");

                    if (customerUI.SideBarBGColor != string.Empty)
                    {
                        tblSideBar.Style["background-color"] = "#" + customerUI.SideBarBGColor;
                    }
                    if (customerUI.SideBarFontName != string.Empty)
                    {
                        tblSideBar.Style["font-family"] = customerUI.SideBarFontName;
                        //lblSideBar.Font.Name = customerUI.SideBarFontName;
                    }
                    if (customerUI.SideBarFontSize != string.Empty)
                    {
                        tblSideBar.Style["font-size"] = customerUI.SideBarFontSize;
                        //lblSideBar.Font.Size = FontUnit.Parse(customerUI.SideBarFontSize);
                    }
                    if (customerUI.SideBarForeColor != string.Empty)
                    {
                        tblSideBar.Style["color"] = "#" + customerUI.SideBarForeColor;
                        //lblSideBar.ForeColor = Color.FromArgb(int.Parse(customerUI.SideBarForeColor, System.Globalization.NumberStyles.HexNumber));
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindPageUI");
                lblErr.Text = strErrCode;
            }
        }

        private void BindProducts(int subcategoryId, bool isSub, string orderBy, string searchValue)
        {
            try
            {
                lblProductMsg.Text = "No Products Found";
                DataSet products = null;
                if (searchValue == string.Empty)
                {
                    products = (new ProductsDAL()).GetProducts(subcategoryId, isSub, orderBy);
                }
                else
                {
                    products = (new ProductsDAL()).GetProductSearch(subcategoryId, searchValue, orderBy);
                }

                if (products != null && products.Tables[0].Rows.Count > 0)
                {
                    lblProductMsg.Text = "";
                    ProductsListView.DataSource = products;
                    ProductsListView.DataBind();
                    dpProducts.Visible = true;
                }
                else
                {
                    ProductsListView.DataSource = null;
                    ProductsListView.DataBind();
                    dpProducts.Visible = false;
                }
                lblNavBar.Text = "All Categories";
                if (ViewState["Category"] != null && ViewState["Category"].ToString() != string.Empty)
                {
                    lblNavBar.Text += "&nbsp;&nbsp;&nbsp;>>&nbsp;&nbsp;&nbsp;" + ViewState["Category"].ToString();
                }
                if (ViewState["SubCat"] != null && ViewState["SubCat"].ToString() != string.Empty)
                {
                    lblNavBar.Text += "&nbsp;&nbsp;&nbsp;>>&nbsp;&nbsp;&nbsp;" + ViewState["SubCat"].ToString();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindProducts",subcategoryId.ToString(), 
                                                                    isSub.ToString(), orderBy, searchValue);
                lblErr.Text = strErrCode;
            }
        }

        protected void ProductsListView_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                dpProducts.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                BindProducts(int.Parse(ViewState["Subcategory"].ToString()), (bool)ViewState["IsSub"], ViewState["OrderBy"].ToString(), ViewState["SearchValue"].ToString());
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "ProductsListView_PagePropertiesChanging");
                lblErr.Text = strErrCode;
            }
        }

        private void BindCategories()
        {
            try
            {
                DataSet categories = (new CatalogCategoriesDAL()).GetCategories();
                if (categories != null)
                {
                    rptCategories.DataSource = categories.Tables[0];
                    rptCategories.DataBind();
                }
                else
                {
                    rptCategories.DataSource = null;
                    rptCategories.DataBind();
                }

                DataSet categoriesSearch = (new CatalogCategoriesDAL()).GetCategoriesSearch();
                if (categoriesSearch != null)
                {
                    ddlCategories.DataSource = categoriesSearch.Tables[0];
                    ddlCategories.DataTextField = "Name";
                    ddlCategories.DataValueField = "ID";
                    ddlCategories.DataBind();
                }
                else
                {
                    ddlCategories.DataSource = null;
                    ddlCategories.DataBind();
                }
                ddlCategories.Items.Insert(0, new ListItem("All Categories", "0"));
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindCategories");
                lblErr.Text = strErrCode;
            }
        }

        protected void rptCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    int categoryId = int.Parse(((HiddenField)e.Item.FindControl("hfCategoryID")).Value);
                    DataSet subcategories = (new CatalogCategoriesDAL()).GetSubCategories(categoryId);
                    if (subcategories != null)
                    {
                        Repeater rptSubcategories = (Repeater)e.Item.FindControl("rptSubcategories");
                        rptSubcategories.DataSource = subcategories.Tables[0];
                        rptSubcategories.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "rptCategories_ItemDataBound");
                lblErr.Text = strErrCode;
            }
        }

        protected void rptSubcategories_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Sub")
                {
                    ViewState["Subcategory"] = e.CommandArgument.ToString();
                    ViewState["IsSub"] = true;
                    ViewState["SearchValue"] = string.Empty;
                    ViewState["OrderBy"] = "MAN";
                    ViewState["SubCat"] = ((LinkButton)e.CommandSource).Text;
                    ViewState["Category"] = (new CatalogCategoriesDAL()).GetCategoryNameBySubCategory(int.Parse(e.CommandArgument.ToString()));
                    ResetPager();
                    BindProducts(int.Parse(e.CommandArgument.ToString()), true, ViewState["OrderBy"].ToString(), ViewState["SearchValue"].ToString());
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "rptSubcategories_ItemCommand");
                lblErr.Text = strErrCode;
            }
        }

        protected void rptCategories_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Main")
                {
                    ViewState["IsSub"] = false;
                    ViewState["Subcategory"] = e.CommandArgument.ToString();
                    ViewState["SearchValue"] = string.Empty;
                    ViewState["OrderBy"] = "MAN";
                    ViewState["Category"] = ((LinkButton)e.CommandSource).Text;
                    ViewState["SubCat"] = string.Empty;
                    ResetPager();
                    BindProducts(int.Parse(e.CommandArgument.ToString()), false, ViewState["OrderBy"].ToString(), ViewState["SearchValue"].ToString());
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "rptSubcategories_ItemCommand");
                lblErr.Text = strErrCode;
            }
        }

        protected void ddlSoryBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["OrderBy"] = ddlSoryBy.SelectedValue;
                ResetPager();
                BindProducts(int.Parse(ViewState["Subcategory"].ToString()), (bool)ViewState["IsSub"], ViewState["OrderBy"].ToString(), ViewState["SearchValue"].ToString());
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "ddlSoryBy_SelectedIndexChanged");
                lblErr.Text = strErrCode;
            }
        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int subcategoryId = int.Parse(ddlCategories.SelectedValue);
                ViewState["SearchValue"] = txtSearch.Text.Trim();
                ViewState["OrderBy"] = "MAN";
                ViewState["Category"] = "Search Results";
                ViewState["SubCat"] = string.Empty;
                ResetPager();
                BindProducts(subcategoryId, true, ViewState["OrderBy"].ToString(), txtSearch.Text.Trim());
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lbSearch_Click");
                lblErr.Text = strErrCode;
            }
        }

        private void ResetPager()
        {
            try
            {
                CommandEventArgs commandEventArgs = new CommandEventArgs("0", "");
                NumericPagerField numericPagerField = dpProducts.Fields[0] as NumericPagerField;
                if (numericPagerField != null)
                { numericPagerField.HandleEvent(commandEventArgs); }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "ResetPager");
                lblErr.Text = strErrCode;
            }
        }

        protected void ProductsListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    //Response.Redirect("~/Catalog/EditProduct.aspx?id=" + e.CommandArgument.ToString());
                    if (Session["OrderRoleName"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["EComAdmin"].ToString())
                    {
                        Response.Redirect("~/Catalog/EditProduct.aspx?id=" + e.CommandArgument.ToString() + "&from=IProd");
                    }
                    else if (Session["OrderRoleName"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["SuperAdmin"].ToString())
                    {
                        Response.Redirect("~/Catalog/EditProduct.aspx?id=" + e.CommandArgument.ToString() + "&from=IProd");
                    }
                    else
                    {
                        Response.Redirect("~/Catalog/ProductSalesRep.aspx?id=" + e.CommandArgument.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "ProductsListView_ItemCommand");
                lblErr.Text = strErrCode;
            }
        }

        protected void lbShowAllCategories_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SearchValue"] = string.Empty;
                ViewState["Subcategory"] = "0";
                ViewState["IsSub"] = true;
                ViewState["OrderBy"] = "MAN";
                ViewState["Category"] = string.Empty;
                ViewState["SubCat"] = string.Empty;
                txtSearch.Text = string.Empty;
                BindProducts(0, true, "MAN", string.Empty);
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lbShowAllCategories_Click");
                lblErr.Text = strErrCode;
            }
        }

        protected void lbAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Catalog/EditProduct.aspx?from=IProd");
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lbAddProduct_Click");
                lblErr.Text = strErrCode;
            }
        }

        protected void lbSwitchRole_Click(object sender, EventArgs e)
        {
            try
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
                    lbSwitchRole.Text = "Log In As Sales Representative";
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