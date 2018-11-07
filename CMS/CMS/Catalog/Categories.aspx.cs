using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using CMS.DAL;

namespace CMS.Catalog
{
    public partial class Categories : System.Web.UI.Page
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategories();
                populateShipClassDDL();
                clearAddNewCategory();
                clearAddSubCatTextBox();
                
            }
        }

        //Bind the Categories.
        private void BindCategories()
        {            
            try
            {                
                DataSet bindCategoies = (new CatalogCategoriesDAL()).GetCategories_DAL();

                if (bindCategoies != null && bindCategoies.Tables[0].Rows.Count > 0)
                {
                    DataView dv = bindCategoies.Tables[0].DefaultView;
                    if (ViewState["sortBy"] != null)
                    {
                        dv.Sort = ViewState["sortBy"].ToString();
                    }
                    gridViewCategories.DataSource = dv;
                    gridViewCategories.DataBind();
                }
                else
                {
                    gridViewCategories.DataSource = null;
                    gridViewCategories.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindCategories");
                lblErr.Text = strErrCode;               

            }
        }//End of BindCategories...


        protected void gridViewCategories_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gridViewCategories.PageIndex = e.NewPageIndex;
                BindCategories();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetSortDirection");
                lblErr.Text = strErrCode;
            }
        }


        //Bind the subcategories by category name
        private void BindSubCategories(string name)
        {   
            try
            {
                DataSet bindSubCategories = (new CatalogCategoriesDAL()).GetSubcategoryByCategoryName_DAL(name);
               // hfCatID.Value = bindSubCategories.Tables[0].Rows[0]["Category_ID"].ToString();

                if (bindSubCategories != null && bindSubCategories.Tables[0].Rows.Count > 0
                    && bindSubCategories.Tables.Count >0)
                {
                    DataView dv = bindSubCategories.Tables[0].DefaultView;
                    if (ViewState["sortBy"] != null)
                    {
                        dv.Sort = ViewState["sortBy"].ToString();
                    }
                    gridViewSubCategories.DataSource = dv;
                    gridViewSubCategories.DataBind();
                 }
                else
                {
                    lblnoSubcatFound.Text = "No Subcategory Found.";                   
                    gridViewSubCategories.DataSource = null;
                    gridViewSubCategories.DataBind();
                }                
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindSubCategories", name);
                lblErr.Text = strErrCode;   
            }
        }//End of BindCategories...

        public void populateShipClassDDL()
        {
            try
            {
                DataSet newShippingcategoryDDL = (new CatalogInventoryManagerDAL().GetShippingClass_DAL(1));
                if (newShippingcategoryDDL != null)
                {
                    ddlShipClass.DataSource = newShippingcategoryDDL.Tables[0];
                    ddlShipClass.DataTextField = "ShipClass";
                    ddlShipClass.DataValueField = "ShippingClassID";
                    ddlShipClass.DataBind();
                }
                else
                {
                    ddlShipClass.DataSource = null;
                    ddlShipClass.DataBind();
                }
                ddlShipClass.Items.Insert(0, new ListItem("Select", "-1"));
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "populateShipClassDDL");
                lblErr.Text = strErrCode;
            }
        }

        protected void Category_Click(object sender, EventArgs e)
        {
            try
            {
                string tempCategory = ((LinkButton)sender).Text.Trim();           
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Category_Click");
                lblErr.Text = strErrCode;
            }
        }

        /*
         * Sort the Sizes by column
         */
        protected void GridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                ViewState["sortBy"] = e.SortExpression + " " + GetSortDirection(e.SortExpression); ;
                BindCategories();
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
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetSortDirection",sortColumn);
                lblErr.Text = strErrCode;
            }
            return ViewState["SortExpression"].ToString();
        }

        protected void InsertNewCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbxCatName.Text.Trim() == string.Empty)
                {
                    ModlPopupCategories.Show();
                    lblErrMessage.Text = "Please enter the category name.";
                }
                int insertNewCategory = 0;
                string tempName = txtbxCatName.Text.Trim();

                if (txtbxCatName.Text.Trim() != string.Empty)
                {
                    insertNewCategory = (new CatalogCategoriesDAL()).InsertNewCategory_DAL(tempName);

                    BindCategories();
                    clearAddNewCategory();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "InsertNewCategory_Click");
                lblErr.Text = strErrCode;  
            } 
        }    

        //Clear text box
        public void clearAddNewCategory()
        {
            txtbxCatName.Text = string.Empty;          
        }

        protected void addNewCategory_Click(object sender, EventArgs e)
        {
            try
            {
                ModlPopupCategories.Show();
                lblErrMessage.Text = string.Empty;
                lblErrCategories.Text = string.Empty;
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "addNewCategory_Click");
                lblErr.Text = strErrCode; 
            }
        }

        protected void CancelInsertCat_Click(object sender, EventArgs e)
        {
            try
            {
                clearAddNewCategory();
                ModlPopupCategories.Hide();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "CancelInsertCat_Click");
                lblErr.Text = strErrCode;  
            }
        }

        protected void CancelSubCat_Click(object sender, EventArgs e)
        {
            try
            {
                clearAddSubCatTextBox();
                modPopupSubCat.Hide();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "CancelSubCat_Click");
                lblErr.Text = strErrCode; 
            }
        }
        
        protected void GridView_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try 
            {
                lblErrMessageUpdate.Text = string.Empty;
                if (e.CommandName == "PopupCategory")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    DataSet getCategoryByCatID = (new CatalogCategoriesDAL().GetCategoryByCategory_DAL(id));

                    hfCategoryID.Value = getCategoryByCatID.Tables[0].Rows[0]["Category_ID"].ToString();
                    txtUpdateCat.Text = getCategoryByCatID.Tables[0].Rows[0]["Category_Name"].ToString();

                    ModlPopupUpdatecat.Show(); // pop up Category for update or delete
                }
                else if (e.CommandName == "PopupSubcategories")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    DataSet getCategoryByCatID = (new CatalogCategoriesDAL().GetCategoryByCategory_DAL(id));
                    lblErrCategories.Text = string.Empty;
                    lblErrMessageSubEdit.Text = string.Empty;
                    lblnoSubcatFound.Text = string.Empty;
                    lblCategory.Text = getCategoryByCatID.Tables[0].Rows[0]["Category_Name"].ToString();
                    hfCategoryID.Value = getCategoryByCatID.Tables[0].Rows[0]["Category_Id"].ToString();
                    string tempName = e.CommandArgument.ToString();
                    BindSubCategories(tempName);
                    lnkBtnInsertNewSubCategories.Visible = true;
                    lnkSubCatUpdate.Visible = false;
                    modPopupSubCat.Show();                
                }
                else if (e.CommandName == "ToggleCategory")
                {
                    lblErrCategories.Text = string.Empty;
                    hfCurrentID.Value = e.CommandArgument.ToString();

                    CatalogCategoriesDAL catalogDAL = new CatalogCategoriesDAL();
                    if (((LinkButton)e.CommandSource).Text == "Inactive")
                    {
                        catalogDAL.DeleteCategory_DAL(int.Parse(e.CommandArgument.ToString()), true);
                    }
                    else
                    {
                        catalogDAL.DeleteCategory_DAL(int.Parse(e.CommandArgument.ToString()), false);
                        string deleteCategory = (new CatalogCategoriesDAL()).DeleteCategory_DAL(int.Parse(e.CommandArgument.ToString()), false);
                        if (deleteCategory != string.Empty)
                        {
                            lblErrCategories.Visible = true;
                            lblErrCategories.Text = "Unable to Inactive the category as it is currently in use by the product.";
                            BindCategories();
                        }
                    }
                    BindCategories();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "GridView_OnRowCommand");
                lblErr.Text = strErrCode;
            }                 
            
        }

        protected void GridView_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lbStatus = (LinkButton)e.Row.FindControl("lnkToggle");

                    if (Convert.ToBoolean(((HiddenField)e.Row.FindControl("hfStatus")).Value) == true)
                    {
                        lbStatus.Text = "Active";
                    }
                    else
                    {
                        lbStatus.Text = "Inactive";
                        e.Row.Cells[2].BackColor = Color.FromArgb(int.Parse("993300", System.Globalization.NumberStyles.HexNumber));
                        e.Row.Cells[2].ForeColor = Color.White;
                        lbStatus.BackColor = Color.FromArgb(int.Parse("993300", System.Globalization.NumberStyles.HexNumber));
                        lbStatus.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "GridView_OnRowDataBound");
                lblErr.Text = strErrCode;
            }
        }


        protected void SubcatGridView_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lbStatus = (LinkButton)e.Row.FindControl("lnkToggleSubcat");

                    if (Convert.ToBoolean(((HiddenField)e.Row.FindControl("hfStatusSubcat")).Value) == true)
                    {
                        lbStatus.Text = "Active";
                    }
                    else
                    {
                        lbStatus.Text = "Inactive";
                        e.Row.Cells[4].BackColor = Color.FromArgb(int.Parse("993300", System.Globalization.NumberStyles.HexNumber));
                        e.Row.Cells[4].ForeColor = Color.White;
                        lbStatus.BackColor = Color.FromArgb(int.Parse("993300", System.Globalization.NumberStyles.HexNumber));
                        lbStatus.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "SubcatGridView_OnRowDataBound");
                lblErr.Text = strErrCode;
            }
        }

        /*
         * On row select to popup subcategory window.
         */
        protected void GridView_OnRowCommand_Subcategory(object sender, GridViewCommandEventArgs e)
        {
            try 
            {
                if (e.CommandName == "ToggleSubcat")
                {
                    lblErrMessageSubEdit.Text = string.Empty;
                    hfCurrentSubcatID.Value = e.CommandArgument.ToString();
                    CatalogCategoriesDAL subcatDAL = new CatalogCategoriesDAL();        
        
                    if (((LinkButton)e.CommandSource).Text == "Inactive")
                    {
                        subcatDAL.DeleteSubcategory_DAL(int.Parse(e.CommandArgument.ToString()), true);
                        BindSubCategories(hfCategoryID.Value);
                        modPopupSubCat.Show();
                    }
                    else
                    {
                        subcatDAL.DeleteSubcategory_DAL(int.Parse(e.CommandArgument.ToString()), false);
                        string deleteSubCat = (new CatalogCategoriesDAL()).DeleteSubcategory_DAL(int.Parse(e.CommandArgument.ToString()), false);

                        if (deleteSubCat != string.Empty)
                        {
                            lblErrMessageSubEdit.Visible = true;
                            lblErrMessageSubEdit.Text = "Unable to Inactive as it is currently in use by the product.";

                            BindSubCategories(hfCategoryID.Value);
                            modPopupSubCat.Show();               
                        }
                    }
                    BindSubCategories(hfCategoryID.Value);
                    modPopupSubCat.Show();
                }
                else if (e.CommandName == "PopupSubCatUpdate")
                {
                    lblErrMessageSubEdit.Text = string.Empty;
                    clearAddSubCatTextBox();
                    int id = Convert.ToInt32(e.CommandArgument);
                    DataSet getSubCatBySubCatID = null;
                    lnkBtnInsertNewSubCategories.Visible = false;
                    lnkSubCatUpdate.Visible = true;
                        
                    getSubCatBySubCatID = new DataSet();

                    getSubCatBySubCatID = (new CatalogCategoriesDAL().GetSubcategoryBySubcategoryID_DAL(id));

                    hfSubCatID.Value = getSubCatBySubCatID.Tables[0].Rows[0]["Subcategory_Id"].ToString();
                    txtSubCatName.Text = getSubCatBySubCatID.Tables[0].Rows[0]["Subcategory_Name"].ToString();
                    txtSubDescription.Text = getSubCatBySubCatID.Tables[0].Rows[0]["Subcategory_Desc"].ToString();
                    //txtSubCatShipClass.Text = getSubCatBySubCatID.Tables[0].Rows[0]["Shipping_Class"].ToString();
                    string ddlShipClstext = getSubCatBySubCatID.Tables[0].Rows[0]["Shipping_Class"].ToString();

                    if (ddlShipClstext != string.Empty)
                    {
                        //string ddlvalue = ddlShipClass.Items.FindByText(ddlShipClstext).Value;

                        foreach (ListItem li in ddlShipClass.Items)
                        {
                            if (ddlShipClstext == li.Text)
                            {
                                ddlShipClass.SelectedValue = ddlShipClass.Items.FindByText(ddlShipClstext).Value;
                            }
                        }    
                    }
                    string tempChkTax = getSubCatBySubCatID.Tables[0].Rows[0]["Is_Taxable"].ToString();

                    string ddlSubCartFtext = string.Empty;
                    ddlSubCartFtext = getSubCatBySubCatID.Tables[0].Rows[0]["Feature_Rank"].ToString();
                    if (ddlSubCartFtext != string.Empty)
                    {
                        ddlSubCatFRank.SelectedValue = ddlSubCatFRank.Items.FindByText(ddlSubCartFtext).Value;
                    }
                   // txtSubCatFeatureRank.Text = getSubCatBySubCatID.Tables[0].Rows[0]["Feature_Rank"].ToString();

                    if (tempChkTax.Equals("True"))
                    {
                        chkTax.Checked = true;
                    }
                    else
                    {
                        chkTax.Checked = false;
                    }

                    modPopupSubCat.Show();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "GridView_OnRowCommand_Subcategory");
                lblErr.Text = strErrCode;
            }

        
        }
        
        //Clear text box
        public void clearUpdateCategory()
        {
            txtUpdateCat.Text = string.Empty;
        }

        /*
         * Update Category by Category ID when click on update button.
         */
        protected void UpdateCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUpdateCat.Text.Trim() == string.Empty)
                {
                    ModlPopupUpdatecat.Show();
                    lblErrMessageUpdate.Text = "Please enter the category name.";
                }

                int id = Convert.ToInt32(hfCategoryID.Value);
                int updateCategory = 0;
                string tempName = txtUpdateCat.Text.Trim();

                if (txtUpdateCat.Text.Trim() != string.Empty)
                {
                    updateCategory = (new CatalogCategoriesDAL()).UpdateCategory_DAL(id, tempName);
                    BindCategories();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateCategory_Click");
                lblErr.Text = strErrCode; 
            }  
        }//End of UpdateCategory_Click...

 

        // Get rid of DELETE BUTTON 
        protected void DeleteCategory_Click(object sender, EventArgs e)
        {            
            //try
            //{
            //    int id = Convert.ToInt32(hfCategoryID.Value);
               
            //    int deleteCategory = (new CatalogCategoriesDAL()).DeleteCategory_DAL(id);
            //    BindCategories();
            //}
            //catch (Exception ex)
            //{
            //    string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "DeleteCategory_Click");
            //    lblErrMessageUpdate.Text = strErrCode; 
            //} 
        }

        protected void CancelCategory_Click(object sender, EventArgs e)
        {
            try
            {
                ModlPopupCategories.Hide();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "CancelCategory_Click");
                lblErr.Text = strErrCode;
            }  
        }

        protected void InsertNewSubCategories_Click(object sender, EventArgs e)
        {            
            try
            {
                lblErrMessageSubEdit.Visible = false;
                lblErrMessageSubEdit.Text = string.Empty;
                lblnoSubcatFound.Text = string.Empty;
                lblnoSubcatFound.Visible = false;
                if (txtSubCatName.Text.Trim() == string.Empty)
                {
                    modPopupSubCat.Show();
                    lblErrMessageSubEdit.Visible = true;
                    lblErrMessageSubEdit.Text = "Please enter the Subcatgory Name";
                    return;
                }

                if (ddlSubCatFRank.SelectedItem.Value=="Select")
                {
                    modPopupSubCat.Show();
                    lblErrMessageSubEdit.Visible = true;
                    lblErrMessageSubEdit.Text = "Please Select Feature Rank";
                    return;
                }

                //int featureRank;      
                //if(!int.TryParse(txtSubCatFeatureRank.Text, out featureRank))
                //{
                //    modPopupSubCat.Show();
                //    lblErrMessageSubEdit.Visible = true;
                //    lblErrMessageSubEdit.Text = "Feature rank should be 1 - 12.";
                //    return;
                //}
                //if (int.TryParse(txtSubCatFeatureRank.Text, out featureRank))
                //{
                //    featureRank = Convert.ToInt32(txtSubCatFeatureRank.Text);
                //    if (featureRank > 12 || featureRank < 1)
                //    {
                //        modPopupSubCat.Show();
                //        lblErrMessageSubEdit.Visible = true;
                //        lblErrMessageSubEdit.Text = "Feature rank should be 1 - 12.";
                //        return;
                //    }
                //}
             
                string tempName = txtSubCatName.Text.Trim();
                string tempDesc = txtSubDescription.Text.Trim();
                //string tempShipClass = txtSubCatShipClass.Text.Trim();
                string tempShipClass = string.Empty;
                if (ddlShipClass.SelectedItem.Value != "-1") { tempShipClass = ddlShipClass.SelectedItem.Text.Trim(); }
                else { tempShipClass = ""; }
                 
               // int tempFeatureRank = Convert.ToInt32(txtSubCatFeatureRank.Text.ToString());

                int tempFeatureRank = Convert.ToInt32(ddlSubCatFRank.SelectedItem.Text);

                int tempCatCode = Convert.ToInt32(hfCategoryID.Value); 
            
                int insertNewSubCat = (new CatalogCategoriesDAL()).InsertNewSubcategory_DAL(tempName, tempDesc, tempShipClass, chkTax.Checked,
                                                                            tempFeatureRank,  tempCatCode);
                lblErrMessageSubEdit.Text = tempName + " added.";
                BindSubCategories(hfCategoryID.Value);
                clearAddSubCatTextBox();
                modPopupSubCat.Show();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "InsertNewSubCategories_Click");
                lblErr.Text = strErrCode; 
            }
        }

        public void clearAddSubCatTextBox()
        {        
            txtSubCatName.Text = string.Empty;
            txtSubDescription.Text = string.Empty;
            populateShipClassDDL();
            //txtSubCatShipClass.Text = string.Empty;
            
            ddlSubCatFRank.SelectedValue = ddlSubCatFRank.Items.FindByText("Select").Value;
            chkTax.Checked = false;     
        }

        protected void UpdateSubcategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSubCatName.Text.Trim() == string.Empty)
                {
                    modPopupSubCat.Show();
                    lblErrMessageSubEdit.Text = "Please enter the Subcatgory Name";
                    return;
                }
                if (ddlSubCatFRank.SelectedItem.Value == "Select")
                {
                    modPopupSubCat.Show();
                    lblErrMessageSubEdit.Visible = true;
                    lblErrMessageSubEdit.Text = "Please Select Feature Rank";
                    return;
                }
                //int featureRank;
                //if (txtSubCatFeatureRank.Text.Trim() == string.Empty)
                //{
                //    txtSubCatFeatureRank.Text = "0";
                //}
                //if (!int.TryParse(txtSubCatFeatureRank.Text, out featureRank))
                //{
                //    modPopupSubCat.Show();
                //    lblErrMessageSubEdit.Text = "Feature rank should be 1 - 12.";
                //    return;
                //}
                //if (int.TryParse(txtSubCatFeatureRank.Text, out featureRank))
                //{
                //    featureRank = Convert.ToInt32(txtSubCatFeatureRank.Text);
                //    if (featureRank > 12 || featureRank < 1)
                //    {
                //        modPopupSubCat.Show();
                //        lblErrMessageSubEdit.Text = "Feature rank should be 1 - 12.";
                //        return;
                //    }
                //}
                                
                int id = Convert.ToInt32(hfSubCatID.Value);
                string tempName = txtSubCatName.Text.Trim();
           
                string tempDesc = txtSubDescription.Text.Trim();
                //string tempShipClass = txtSubCatShipClass.Text.Trim();
                 string tempShipClass = string.Empty;
                if (ddlShipClass.SelectedItem.Value != "-1") { tempShipClass = ddlShipClass.SelectedItem.Text.Trim(); }
                else { tempShipClass = ""; }
                //int tempFeatureRank = Convert.ToInt32(txtSubCatFeatureRank.Text.ToString());
                int tempFeatureRank = Convert.ToInt32(ddlSubCatFRank.SelectedItem.Text);
                int tempCatCode = Convert.ToInt32(hfCategoryID.Value);
                          
                int updateSubCat = (new CatalogCategoriesDAL()).UpdateSubcategory_DAL(tempName, tempDesc, tempShipClass, chkTax.Checked,
                                                                            tempFeatureRank, tempCatCode, id);

                lblErrMessageSubEdit.Text = tempName + " updated";
                BindSubCategories(hfCategoryID.Value);
                clearAddSubCatTextBox();
                lnkBtnInsertNewSubCategories.Visible = true;
                lnkSubCatUpdate.Visible = false;
               // lnkSubCatDelete.Visible = false;
                modPopupSubCat.Show();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateSubcategory_Click");
                lblErr.Text = strErrCode; 
            }
        }

        protected void DeleteSubcategory_Click(object sender, EventArgs e)
        { 
            //try
            //{
            //    int id = Convert.ToInt32(hfSubCatID.Value);
            //    int deleteSubCat = (new CatalogCategoriesDAL()).DeleteSubcategory_DAL(id);
            //    BindSubCategories(hfCategoryID.Value);
            //    clearAddSubCatTextBox();
            //    modPopupSubCat.Show();
            //}
            //catch (Exception ex)
            //{
            //    string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "DeleteSubcategory_Click");
            //    lblErrMessageSubEdit.Text = strErrCode; 
            //} 
        }
    }
}