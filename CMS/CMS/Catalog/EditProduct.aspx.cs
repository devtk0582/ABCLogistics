using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.DAL;

namespace CMS.Catalog
{
    public partial class EditProdcut : System.Web.UI.Page
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Get id from product and populate the values.
                try
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    if (id > 0)
                    {
                        hfInventoryID.Value = id.ToString();
                        populateEditProduct(id);
                    }
                    else
                    {
                        populateNewProduct();
                    }
                }
                catch (Exception ex)
                {
                    string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Page_Load");
                    lblErr.Text = strErrCode;
                }
            }
        }

        //Populate Category DDL.
        public void populateCateogryDDL()
        {
            try
            {
                DataSet newCategoryNameDLL = (new CatalogCategoriesDAL().GetCategories());
                if (newCategoryNameDLL != null)
                {
                    ddlCategory.DataSource = newCategoryNameDLL.Tables[0];
                    ddlCategory.DataTextField = "Category_Name";
                    ddlCategory.DataValueField = "Category_Id";
                    ddlCategory.DataBind();
                }
                else
                {
                    ddlCategory.DataSource = null;
                    ddlCategory.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "populateCateogryDDL");
                lblErr.Text = strErrCode;
            }
        }

        //Populate Manufacturer DDL
        public void populateManufacturerDDL()
        {
            try
            {
                DataSet newManufacturersDDL = (new CatalogInventoryManagerDAL().GetManufacturer_DAL(1));
                if (newManufacturersDDL != null)
                {
                    ddlMfr.DataSource = newManufacturersDDL.Tables[0];
                    ddlMfr.DataTextField = "Manufactures_Desc";
                    ddlMfr.DataValueField = "Manufactures_Id";
                    ddlMfr.DataBind();
                }
                else
                {
                    ddlMfr.DataSource = null;
                    ddlMfr.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "populateManufacturerDDL");
                lblErr.Text = strErrCode;
            }
        }

        //Populate Subcategory DDL by categoryID
        public void populateSubcategoryDDL(int categoryID)
        {
            try
            {
                DataSet bindSubcategoryDDL = (new CatalogCategoriesDAL().GetSubCategories(categoryID));

                if (bindSubcategoryDDL != null)
                {
                    ddlSubcategory.DataSource = bindSubcategoryDDL.Tables[0];
                    ddlSubcategory.DataTextField = "Subcategory_Name";
                    ddlSubcategory.DataValueField = "Subcategory_ID";
                    ddlSubcategory.DataBind();
                }
                else
                {
                    ddlSubcategory.DataSource = null;
                    ddlSubcategory.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "populateSubcategoryDDL");
                lblErr.Text = strErrCode;
            }
        }

        //Populate Size DDL for inventory
        //public void populateSizeDDL()
        //{
        //    try
        //    {
        //        DataSet newSizeCodeDDL = (new CatalogSizesDAL().GetSizes_DAL(1));
        //        if (newSizeCodeDDL != null)
        //        {
        //            ddlnewInventoriesSize.DataSource = newSizeCodeDDL.Tables[0];
        //            ddlnewInventoriesSize.DataTextField = "Size_Code";
        //            ddlnewInventoriesSize.DataValueField = "Size_Id";
        //            ddlnewInventoriesSize.DataBind();
        //        }
        //        else
        //        {
        //            ddlnewInventoriesSize.DataSource = null;
        //            ddlnewInventoriesSize.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "populateSizeDDL");
        //        lblErr.Text = strErrCode;
        //    }
        //}

        //Populate color DDL for invenotry
        //public void populateColor()
        //{
        //    try
        //    {
        //        DataSet newColorNameDDL = (new CatalogColorsDAL().GetColors_DAL());
        //        if (newColorNameDDL != null)
        //        {
        //            ddlnewInventoriesColor.DataSource = newColorNameDDL.Tables[0];
        //            ddlnewInventoriesColor.DataTextField = "Color_Name";
        //            ddlnewInventoriesColor.DataValueField = "Color_id";
        //            ddlnewInventoriesColor.DataBind();
        //        }
        //        else
        //        {
        //            ddlnewInventoriesColor.DataSource = null;
        //            ddlnewInventoriesColor.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "populateColor");
        //        lblErr.Text = strErrCode;
        //    }
        //}

        //Populate Color for Image DDL
        public void populateImageColorDDL()
        {
            try
            {
                DataSet ddlImageColor = (new CatalogColorsDAL().GetColors_DAL());

                if (ddlImageColor != null && ddlImageColor.Tables[0].Rows.Count > 0)
                {
                    ddlNewColor.DataSource = ddlImageColor;
                    ddlNewColor.DataTextField = "Color_Name";
                    ddlNewColor.DataValueField = "Color_id";
                    ddlNewColor.DataBind();
                }
                else
                {
                    ddlNewColor.DataSource = null;
                    ddlNewColor.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "populateImageColorDDL");
                lblErr.Text = strErrCode;
            }
        }

        //Populate Ship Class DDL
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
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "populateShipClassDDL");
                lblErr.Text = strErrCode;
            }
        }

        //Populate the DDL for new product.
        public void populateNewProduct()
        {
            try
            {
                lblSKU.Text = string.Empty;
                lblErrProduct.Text = string.Empty;
                lnkbtnEdit.Visible = false;
                lnkAddNewProduct.Visible = true;
                //lnkbtnRemove.Visible = false;
                PanelAddNewInventoryButton.Visible = false;
                panelDisplayInventory.Visible = false;
                panelAddNewImage.Visible = false;

                populateCateogryDDL();
                ddlCategory.Items.Insert(0, new ListItem("Select", "0"));

                ddlSubcategory.Items.Insert(0, new ListItem("Select", "0"));

                populateManufacturerDDL();
                ddlMfr.Items.Insert(0, new ListItem("Select", "0"));

                populateShipClassDDL();
                ddlShipClass.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "populateNewProduct");
                lblErr.Text = strErrCode;
            }
        }

        /*
         * Populate Subcategory,shipping class once user select category.
         */
        protected void PopulateSubcat_SelectChange(object sender, EventArgs e)
        {
            try
            {
                hfCategory.Value = ddlCategory.SelectedValue;

                int categoryID = Convert.ToInt32(hfCategory.Value);

                if (categoryID == 0)
                {
                    ddlSubcategory.DataSource = null;
                    ddlSubcategory.DataBind();
                    ddlSubcategory.Items.Clear();
                    ddlSubcategory.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    populateSubcategoryDDL(categoryID);
                    ddlSubcategory.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "PopulateSubcat_SelectChange");
                lblErr.Text = strErrCode;
            }
        }

        //Populate product by ID.
        public void populateEditProduct(int id)
        {
            try
            {
                lnkbtnEdit.Visible = true;
                lnkAddNewProduct.Visible = false;

                DataSet editProduct = (new CatalogInventoryManagerDAL()).GetProductByInventoryID_DAL(id);
                lblSKUData.Text = editProduct.Tables[0].Rows[0]["SKU"].ToString();
                lblCategory.Text = editProduct.Tables[0].Rows[0]["Title"].ToString();
                txtbxPrdTitle.Text = editProduct.Tables[0].Rows[0]["Title"].ToString();
                txtbxSKU.Text = editProduct.Tables[0].Rows[0]["SKU"].ToString();
                txtbxDesc.Text = editProduct.Tables[0].Rows[0]["Description"].ToString();
                txtPrice.Text = editProduct.Tables[0].Rows[0]["Price"].ToString();
                txtListPrice.Text = editProduct.Tables[0].Rows[0]["List Price"].ToString();
                txtWeight.Text = editProduct.Tables[0].Rows[0]["Weight"].ToString();
                string tempchkBxLive = editProduct.Tables[0].Rows[0]["Live"].ToString();

                if (txtWeight.Text.Trim() == "")
                { txtWeight.Text = "1"; }
                

                if (tempchkBxLive.Equals("Y"))
                    chkBxLive.Checked = true;
                else
                    chkBxLive.Checked = false;
                string tempchkBxFeatured = editProduct.Tables[0].Rows[0]["Feature"].ToString();
                if (tempchkBxFeatured.Equals("Y"))
                    chkBxFeatured.Checked = true;
                else
                    chkBxFeatured.Checked = false;

              
                populateCateogryDDL();
                ddlCategory.Items.Insert(0, new ListItem("Select", "0"));

                if ((ddlCategory.Items.FindByValue(editProduct.Tables[0].Rows[0]["CategoryID"].ToString().Trim()) != null))
                {
                    hfCategory.Value = editProduct.Tables[0].Rows[0]["CategoryID"].ToString();
                    ddlCategory.SelectedValue = hfCategory.Value;
                    string tempCatID = editProduct.Tables[0].Rows[0]["CategoryID"].ToString();
                    int tempcategoryID = Convert.ToInt32(tempCatID);
                    populateSubcategoryDDL(tempcategoryID);
                }

                ddlSubcategory.Items.Insert(0, new ListItem("Select", "0"));

                if ((ddlSubcategory.Items.FindByValue(editProduct.Tables[0].Rows[0]["SubcategoryID"].ToString().Trim()) != null))
                {
                    string subcategoryID = editProduct.Tables[0].Rows[0]["SubcategoryID"].ToString();     
                    ddlSubcategory.SelectedValue = subcategoryID;
                }
              
                

               
                populateManufacturerDDL();
                ddlMfr.Items.Insert(0, new ListItem("Select", "0"));
                if ((ddlMfr.Items.FindByValue(editProduct.Tables[0].Rows[0]["mfrID"].ToString().Trim()) != null))
                {
                    string mfrID = editProduct.Tables[0].Rows[0]["mfrID"].ToString();
                    ddlMfr.SelectedValue = mfrID;
                }
                

              
                populateShipClassDDL();
                ddlShipClass.Items.Insert(0, new ListItem("Select", "0"));
                if ((ddlShipClass.Items.FindByValue(editProduct.Tables[0].Rows[0]["ShippingID"].ToString().Trim()) != null))
                {
                    string tempShipClass = editProduct.Tables[0].Rows[0]["ShippingID"].ToString();
                    ddlShipClass.SelectedValue = tempShipClass;
                }
                
               

                BindInventories(id);
                BindInventoriesImage(id);
                panelDisplayInventory.Visible = true;  //panel display update inventory
                panelDisplayImages.Visible = true;     //panel display the images 
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "populateEditProduct", id.ToString());
                lblErr.Text = strErrCode;
            }
        }//End of populateProduct method.

        // Bind the inventory color and size when click on the individual product.
        protected void repeater_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DropDownList ddlSize = (DropDownList)e.Item.FindControl("ddlInventorySize");
                    DropDownList ddlColor = (DropDownList)e.Item.FindControl("ddlInventoryColor");
                    HiddenField hFSize = (HiddenField)e.Item.FindControl("hfInventoriesSize");
                    HiddenField hFColor = (HiddenField)e.Item.FindControl("hfInventoriesColor");

                    DataSet inventoriesSize = (new CatalogSizesDAL()).GetSizes_DAL(1);

                    if (inventoriesSize != null && inventoriesSize.Tables[0].Rows.Count > 0)
                    {
                        ddlSize.DataSource = inventoriesSize.Tables[0];
                        ddlSize.DataTextField = "Size_Code";
                        ddlSize.DataValueField = "Size_ID";
                        ddlSize.DataBind();
                    }
                    else
                    {
                        ddlSize.DataSource = null;
                        ddlSize.DataBind();
                    }
                    ddlSize.SelectedValue = hFSize.Value;

                    DataSet inventoriesColor = (new CatalogColorsDAL()).GetColors_DAL();
                    if (inventoriesColor != null && inventoriesColor.Tables[0].Rows.Count > 0)
                    {
                        ddlColor.DataSource = inventoriesColor.Tables[0];
                        ddlColor.DataTextField = "Color_Name";
                        ddlColor.DataValueField = "Color_Id";
                        ddlColor.DataBind();
                    }
                    else
                    {
                        ddlColor.DataSource = null;
                        ddlColor.DataBind();
                    }
                    ddlColor.SelectedValue = hFColor.Value;
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "repeater_OnItemDataBound");
                lblErr.Text = strErrCode;
            }
        }//End of repeater_OnItemDataBound method.

        //Update the inventory.
        protected void repeaterInventories_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "editInventories")
                {
                    int invtID = Convert.ToInt32(e.CommandArgument); //inv_Template_ext2_ID
                    HiddenField newInvTemplateID = (HiddenField)e.Item.FindControl("hfInventoriesTemplateID");
                    int invTempID = Convert.ToInt32(newInvTemplateID.Value);
                    DropDownList sizeID = (DropDownList)e.Item.FindControl("ddlInventorySize");
                    int tempSizeID = Convert.ToInt32(sizeID.SelectedValue);
                    DropDownList colorID = (DropDownList)e.Item.FindControl("ddlInventoryColor");
                    int tempColorID = Convert.ToInt32(colorID.SelectedValue);
                    Label newSKU = (Label)e.Item.FindControl("lblInvSKU");
                    string tempSKU = newSKU.Text;
                    TextBox newQty = (TextBox)e.Item.FindControl("txtInventoryQuantity");
                    int tempQTY = Convert.ToInt32(newQty.Text);
                    Label updateOK = (Label)e.Item.FindControl("lblMessage");
                    Label updateFailed = (Label)e.Item.FindControl("lblErrorImage");

                    HiddenField dbCurrentQty = (HiddenField)e.Item.FindControl("hfQty");
                    int tempDbCurrentQty = Convert.ToInt32(dbCurrentQty.Value);

                    //updateFailed.Text = string.Empty;
                    lblErrInventory.Text = string.Empty;
                    int updateInventory = 0;
                    DataSet DftwhNum = (new AddCartsDAL()).GetDefaultCMSWarehouses();
                    string strwhse = DftwhNum.Tables[0].Rows[0]["Whse_Id"].ToString();
                    int tempStrwhse = Convert.ToInt32(strwhse);
                    int tempUserID = Convert.ToInt32(Session["UserID"].ToString());

                    if (tempQTY < 0)
                    {
                        lblErrInventory.Text = "Quantity cannot be less than zero.";
                        BindInventories(invTempID);
                        return;
                    }

                    updateOK.Text = string.Empty;
                    updateInventory = (new CatalogInventoryManagerDAL()).UpdateInventories_DAL(invtID, invTempID, tempSizeID, tempColorID, tempSKU, tempQTY, tempStrwhse, tempUserID);

                    lblErrInventory.Text = "Inventory " + tempSKU + " has been updated.";
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "repeaterInventories_OnItemCommand");
                lblErr.Text = strErrCode;
            }

        }//End of repeaterInventories_OnItemCommand...

        //Bind the image when click on the individual product.
        private void BindInventoriesImage(int id)
        {
            try
            {
                repeaterInventoriesImages.DataSource = null;
                repeaterInventoriesImages.DataBind();

                DataSet inventories = (new CatalogInventoryManagerDAL()).GetInventoriesImagesbyInvtID_DAL(id);

                if (inventories != null && inventories.Tables[0].Rows.Count > 0)
                {
                    repeaterInventoriesImages.DataSource = inventories.Tables[0];
                    repeaterInventoriesImages.DataBind();
                }
                else
                {
                    repeaterInventoriesImages.DataSource = null;
                    repeaterInventoriesImages.DataBind();
                    lblErrImage.Text = "No Image Found.";
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindInventoriesImage", id.ToString());
                lblErr.Text = strErrCode;
            }
        }

        //Bind inventories according to product ID.
        private void BindInventories(int id)
        {
            try
            {
                repeaterInventories.DataSource = null;
                repeaterInventories.DataBind();

                DataSet inventories = (new CatalogInventoryManagerDAL()).GetInvenotries_DAL(id);

                if (inventories != null && inventories.Tables[0].Rows.Count > 0)
                {
                    repeaterInventories.DataSource = inventories.Tables[0];
                    repeaterInventories.DataBind();
                }
                else
                {
                    repeaterInventories.DataSource = null;
                    repeaterInventories.DataBind();
                    lblErrInventory.Text = "No Inventory found.";
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindInventories", id.ToString());
                lblErr.Text = strErrCode;
            }
        }

        //Cancel Edit product and redirect back to Inventory Manager page.
        protected void CancelEditProduct_Click(object sender, EventArgs e)
        {
            try
            {
                string from = Request.QueryString["from"];

                if (from == string.Empty)
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    switch (from)
                    {
                        case "IMngr":
                            Response.Redirect("~/Catalog/InventoryManager.aspx");
                            break;
                        case "IReprt":
                            Response.Redirect("~/Reports/InventoryReport.aspx");
                            break;
                        case "IProd":
                            Response.Redirect("~/Catalog/Products.aspx");
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "CancelEditProduct_Click");
                lblErr.Text = strErrCode;
            }
        }

        //Open add inventory panel and populate the size and color DDL.
        //protected void OpenAddInventoryPanel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lblErrProduct.Text = string.Empty;
        //        //txtnewInventoriesSKU.Text = string.Empty;
        //        //txtnewInventoriesQTY.Text = string.Empty;
        //        lblErrInventory.Text = string.Empty;
        //        panelAddnewInventory.Visible = true;
        //        populateSizeDDL();
        //        ddlnewInventoriesSize.Items.Insert(0, new ListItem("Select", "0"));

        //        populateColor();
        //        ddlnewInventoriesColor.Items.Insert(0, new ListItem("Select", "0"));
        //    }
        //    catch (Exception ex)
        //    {
        //        string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "OpenAddInventoryPanel_Click");
        //        lblErr.Text = strErrCode;
        //    }

        //}//End of OpenAddInventoryPanel_Click method.

        //Click to popup window for update image and populate the color DDL.
        protected void OpenAddImagePanel_Click(object sender, EventArgs e)
        {
            try
            {
                lblImage.Visible = true;
                fileUploadImage.Visible = true;
                lblErrInventory.Text = string.Empty;
                lblErrProduct.Text = string.Empty;
                lblErrImage.Text = string.Empty;
                lblNewImage.Text = "Add New Image";
                populateImageColorDDL();
                ddlNewColor.Items.Insert(0, new ListItem("Select", "0"));

                ViewState["imageID"] = "0";
                MdlPopupImage.Show();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "OpenAddImagePanel_Click");
                lblErr.Text = strErrCode;
            }
        }//End of OpenAddImagePanel_Click method.

        /*
         *Edit product.
         */
        protected void EditProduct_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtPrice.Text.Trim() == string.Empty)
                {
                    txtPrice.Text = "0";
                    return;
                }
                if (txtListPrice.Text.Trim() == string.Empty)
                {
                    txtListPrice.Text = "0";
                    return;
                }
                if (txtWeight.Text.Trim() == string.Empty)
                {
                    txtWeight.Text = "0";
                    return;
                }
               

                double price;
                if (!Double.TryParse(txtPrice.Text, out price))
                {
                    lblErrProduct.Text = "Please enter a valid price value.";
                    return;
                }
                double listPrice;
                if (!Double.TryParse(txtListPrice.Text, out listPrice))
                {
                    lblErrProduct.Text = "Please enter a valid List Price Value.";
                    return;
                }

                double weight;
                if (!Double.TryParse(txtWeight.Text, out weight))
                {
                    lblErrProduct.Text = "Please enter a valid weight(LB).";
                    return;
                }

                string addNewProduct = string.Empty;
                int iInvTemp_Id = Convert.ToInt32(hfInventoryID.Value);
                int addNewProductInfo = 0;

                string tempTitle = txtbxPrdTitle.Text.Trim();  //product title
                string tempSKU = txtbxSKU.Text.Trim();         //SKU


                double tempPrice = Convert.ToDouble(txtPrice.Text);
                double tempWeight = Convert.ToDouble(txtWeight.Text);
                double tempListPrice = Convert.ToDouble(txtListPrice.Text);


                if (tempPrice <= 0)
                {
                    lblErrProduct.Text = "Price should be greater than zero.";
                    return;
                }
                if (tempListPrice <= 0)
                {
                    lblErrProduct.Text = "List Price should be greater than zero.";
                    return;
                }
                if (tempWeight < 0)
                {
                    lblErrProduct.Text = "Weight(LB) cannot be less than zero.";
                    return;
                }

                int tempMfrID = Convert.ToInt32(ddlMfr.SelectedValue);
                int tempCategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                int tempSubcatID = Convert.ToInt32(ddlSubcategory.SelectedValue);
                int tempShipClass = Convert.ToInt32(ddlShipClass.SelectedValue);
                bool checkFeature = chkBxFeatured.Checked;
                string tempDesc = txtbxDesc.Text.Trim();

                if (txtbxPrdTitle.Text.Trim() == string.Empty)
                {
                    lblErrProduct.Text = "Please enter the Product Title.";
                    return;
                }
                if (txtbxSKU.Text.Trim() == string.Empty)
                {
                    lblErrProduct.Text = "Please enter the SKU.";
                    return;
                }
                if (tempCategoryID == 0)
                {
                    lblErrProduct.Text = "Please select a product Category.";
                    return;
                }
                if (tempSubcatID == 0)
                {
                    lblErrProduct.Text = "Please select a product Subcategory.";
                    return;
                }

                if (tempMfrID == 0)
                {
                    lblErrProduct.Text = "Please select a Manufacturer.";
                    return;
                }

                if (tempShipClass == 0)
                {
                    lblErrProduct.Text = "Please select a Shipping Class.";
                    return;
                }

                addNewProduct = (new CatalogInventoryManagerDAL().AddNewProductToCustInvTemp_DAL(iInvTemp_Id, tempSKU, tempTitle, tempWeight, tempPrice, 'U'));

                string[] retval;
                retval = addNewProduct.Split(',');
                if (retval.Length > 0)
                {
                    iInvTemp_Id = Convert.ToInt32(retval[1]);
                    ViewState["iInvTemp_Id"] = iInvTemp_Id;
                }

                addNewProductInfo = (new CatalogInventoryManagerDAL().AddNewProduct_DAL(iInvTemp_Id, tempSubcatID, tempCategoryID, tempMfrID, tempListPrice, tempShipClass
                                                                                            , 'U', chkBxFeatured.Checked, chkBxLive.Checked, tempDesc));
                populateEditProduct(iInvTemp_Id);
                lblErrProduct.Text = "Product successfully updated.";

            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "EditProduct_Click");
                lblErr.Text = strErrCode;
            }

        }//End of EditProduct_Click method.


        //Add new product and display the add new inventory and image panel.
        protected void AddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbxPrdTitle.Text.Trim() == string.Empty)
                {
                    lblErrProduct.Text = "Please enter the Product Title.";
                    return;
                }
                if (txtbxSKU.Text.Trim() == string.Empty)
                {
                    lblErrProduct.Text = "Please enter the SKU.";
                    return;
                }
                if (ddlCategory.SelectedIndex == 0)
                {
                    lblErrProduct.Text = "Please select a product Category.";
                    return;
                }
                if (ddlSubcategory.SelectedIndex == 0)
                {
                    lblErrProduct.Text = "Please select a product Subcategory.";
                    return;
                }
                if (ddlMfr.SelectedIndex == 0)
                {
                    lblErrProduct.Text = "Please select a Manufacturer.";
                    return;
                }

                if (txtPrice.Text.Trim() == string.Empty)
                {
                    txtPrice.Text = "0";
                    return;
                }

                double price;
                if (!Double.TryParse(txtPrice.Text, out price))
                {
                    lblErrProduct.Text = "Please enter a valid price value.";
                    return;
                }

                if (txtListPrice.Text.Trim() == string.Empty)
                {
                    txtListPrice.Text = "0";
                }

                double listPrice;
                if (!Double.TryParse(txtListPrice.Text, out listPrice))
                {
                    lblErrProduct.Text = "Please enter a valid List Price Value.";
                    return;
                }

                if (txtWeight.Text.Trim() == string.Empty)
                {
                    txtWeight.Text = "0";
                }

                double weight;
                if (!Double.TryParse(txtWeight.Text, out weight))
                {
                    lblErrProduct.Text = "Please enter a valid weight(LB).";
                    return;
                }
                if (ddlShipClass.SelectedIndex == 0)
                {
                    lblErrProduct.Text = "Please select a Shipping Class.";
                    return;
                }

                string addNewProduct = string.Empty;
                int iInvTemp_Id = 0;
                int addNewProductInfo = 0;

                string tempTitle = txtbxPrdTitle.Text.Trim();  //product title
                string tempSKU = txtbxSKU.Text.Trim();         //SKU
                double tempPrice = Convert.ToDouble(txtPrice.Text);
                double tempWeight = Convert.ToDouble(txtWeight.Text);
                double tempListPrice = Convert.ToDouble(txtListPrice.Text);
                if (tempPrice <= 0)
                {
                    lblErrProduct.Text = "Price should be greater than zero.";
                    return;
                }
                if (tempListPrice <= 0)
                {
                    lblErrProduct.Text = "List Price should be greater than zero.";
                    return;
                }
                if (tempWeight < 0)
                {
                    lblErrProduct.Text = "Weight(LB) cannot be less than zero.";
                    return;
                }


                int tempMfrID = Convert.ToInt32(ddlMfr.SelectedValue);
                int tempShipClass = Convert.ToInt32(ddlShipClass.SelectedValue);
                int tempCategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                int tempSubcatID = Convert.ToInt32(ddlSubcategory.SelectedValue);
                string tempDesc = txtbxDesc.Text.Trim();

                addNewProduct = (new CatalogInventoryManagerDAL().AddNewProductToCustInvTemp_DAL(iInvTemp_Id, tempSKU, tempTitle, tempWeight, tempPrice, 'I'));

                if (addNewProduct.StartsWith("E"))
                {
                    lblErrProduct.Visible = true;
                    lblErrProduct.Text = "The product you entered already exists. Please try again.";
                    txtbxSKU.Text = string.Empty;
                    return;
                }

                string[] retval;
                retval = addNewProduct.Split(',');
                if (retval.Length > 0)
                {
                    iInvTemp_Id = Convert.ToInt32(retval[1]);
                    hfInventoryID.Value = iInvTemp_Id.ToString();
                    ViewState["iInvTemp_Id"] = iInvTemp_Id;
                }

                addNewProductInfo = (new CatalogInventoryManagerDAL().AddNewProduct_DAL(iInvTemp_Id, tempSubcatID, tempCategoryID, tempMfrID, tempListPrice, tempShipClass,
                                                                                         'I', chkBxFeatured.Checked, chkBxLive.Checked, tempDesc));
                lblErrProduct.Text = "Product successfully added.";
                PanelAddNewInventoryButton.Visible = true;
                panelAddNewImage.Visible = true;
                lnkAddNewProduct.Visible = false;
                lnkbtnEdit.Visible = true;
                lnkbtnCancel.Text = "Exit";
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "AddProduct_Click");
                lblErr.Text = strErrCode;
            }
        }//End of AddProduct_Click method.


        //Add new inventory to the new product.
        //protected void AddNewInventory_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlnewInventoriesSize.SelectedIndex == 0)
        //        {
        //            lblErrInventory.Text = "Please select a Size.";
        //            return;
        //        }
        //        if (ddlnewInventoriesColor.SelectedIndex == 0)
        //        {
        //            lblErrInventory.Text = "Please select a Color.";
        //            return;
        //        }
        //        //if (txtnewInventoriesQTY.Text.Trim() == string.Empty)
        //        //{
        //        //    lblErrInventory.Text = "Please enter the Quantity";
        //        //    return;
        //        //}
        //        //int qty;
        //        //if (!int.TryParse(txtnewInventoriesQTY.Text, out qty))
        //        //{
        //        //    lblErrInventory.Text = "Please enter a valid Quantity.";
        //        //    return;
        //        //}
        //        //int tempQty;
        //        //int zeroError = Convert.ToInt32(txtnewInventoriesQTY.Text);
        //        //if (zeroError < 0)
        //        //{
        //        //    lblErrInventory.Text = "Quantity cannot be less than zero.";
        //        //    return;
        //        //}
        //        //else
        //        //{
        //        //    tempQty = Convert.ToInt32(txtnewInventoriesQTY.Text.Trim());
        //        //}
        //        int tempQty = 0;
        //        lblErrInventory.Text = string.Empty;
        //        int tempInvSize = Convert.ToInt32(ddlnewInventoriesSize.SelectedValue);
        //        int tempInvColor = Convert.ToInt32(ddlnewInventoriesColor.SelectedValue);
        //        string tempSKU = txtbxSKU.Text.Trim();

        //        int iInvTemp_Id = 0;

        //        DataSet DftwhNum = (new AddCartsDAL()).GetDefaultCMSWarehouses();
        //        string strwhse = DftwhNum.Tables[0].Rows[0]["Whse_Id"].ToString();
        //        int tempStrwhse = Convert.ToInt32(strwhse);
        //        int tempUserID = Convert.ToInt32(Session["UserID"].ToString());

        //        if (ViewState["iInvTemp_Id"] != null)
        //        {
        //            iInvTemp_Id = Convert.ToInt32(ViewState["iInvTemp_Id"]);
        //            int addNewInventories = (new CatalogInventoryManagerDAL().InsertNewInventory_DAL(iInvTemp_Id, tempInvSize, tempInvColor, tempSKU, tempQty, tempStrwhse, tempUserID));
        //        }
        //        else
        //        {
        //            iInvTemp_Id = Convert.ToInt32(hfInventoryID.Value);
        //            int addNewInventories = (new CatalogInventoryManagerDAL().InsertNewInventory_DAL(iInvTemp_Id, tempInvSize, tempInvColor, tempSKU, tempQty, tempStrwhse, tempUserID));
        //        }

        //        BindInventories(iInvTemp_Id);
        //        panelDisplayInventory.Visible = true;

        //        //Reset add inventory
        //        //txtnewInventoriesQTY.Text = string.Empty;
        //        populateSizeDDL();
        //        ddlnewInventoriesSize.Items.Insert(0, new ListItem("Select", "0"));
        //        populateColor();
        //        ddlnewInventoriesColor.Items.Insert(0, new ListItem("Select", "0"));
        //    }
        //    catch (Exception ex)
        //    {
        //        string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "AddNewInventory_Click");
        //        lblErr.Text = strErrCode;
        //    }
        //}//End of AddNewInventory_Click method.

        protected void SaveImage_Click(object sender, EventArgs e)
        {
            try
            {
                lblErrImage.Text = string.Empty;
                if (ddlNewColor.SelectedIndex == 0)
                {
                    MdlPopupImage.Show();
                    lblErrorImage.Text = "Please select a Color.";
                    return;
                }

                lblErrImage.Text = string.Empty;
                string tempFileName = "";   //image name
                int tempColor = Convert.ToInt32(ddlNewColor.SelectedValue);
                int tempInvTemplateID = Convert.ToInt32(ViewState["iInvTemp_Id"]);
                int imageID = Convert.ToInt32(ViewState["imageID"]);

                bool chckDefaultImage = chkDefaultImage.Checked;
                string fileExt;
                byte[] tempImage;

                if (fileUploadImage.HasFile)
                {
                    fileExt = "";

                    fileExt = System.IO.Path.GetExtension(fileUploadImage.PostedFile.FileName);
                    if (fileExt.ToLower() != ".gif" && fileExt.ToLower() != ".jpg")
                    {
                        MdlPopupImage.Show();
                        lblErrorImage.Text = "Only GIF and JPG image are accepted.";
                        return;
                    }

                    tempImage = fileUploadImage.FileBytes;
                    tempFileName = fileUploadImage.FileName;
                }
                else
                {
                    tempImage = null;
                }

                if (ViewState["iInvTemp_Id"] != null)
                {
                    int uploadImage = (new CatalogInventoryManagerDAL().SaveImage_DAL(tempFileName, tempColor,
                                                                                        tempImage, tempInvTemplateID, imageID, chckDefaultImage));
                }
                else
                {
                    tempInvTemplateID = Convert.ToInt32(hfInventoryID.Value);
                    int uploadImage = (new CatalogInventoryManagerDAL().SaveImage_DAL(tempFileName, tempColor,
                                                                                        tempImage, tempInvTemplateID, imageID, chckDefaultImage));
                }

                //Bind and display the images
                BindInventoriesImage(tempInvTemplateID);
                panelDisplayImages.Visible = true;
                chkDefaultImage.Checked = false;
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "SaveImage_Click");
                lblErr.Text = strErrCode;
            }
        }

        protected void CancelImage_Click(object sender, EventArgs e)
        {
            chkDefaultImage.Checked = false;
            MdlPopupImage.Hide();
        }



        //Click Edit for popup window to edit image.
        protected void repeaterImage_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (e.CommandName == "editImages")
                {
                    lblImage.Visible = false;
                    fileUploadImage.Visible = false;
                    lblNewImage.Text = "Edit Image";

                    ViewState["imageID"] = id;

                    DataSet getImagesbyImageID = (new CatalogInventoryManagerDAL().GetImageByImage_DAL(id));

                    string populateColorID = getImagesbyImageID.Tables[0].Rows[0]["Color_Id"].ToString();
                    string defaultImage = getImagesbyImageID.Tables[0].Rows[0]["defaultImage"].ToString();

                    chkDefaultImage.Checked = Convert.ToBoolean(defaultImage);

                    populateImageColorDDL();
                    ddlNewColor.Items.Insert(0, new ListItem("Select", "0"));
                    ddlNewColor.SelectedValue = populateColorID;

                    MdlPopupImage.Show();
                }
                else if (e.CommandName == "removeImage")
                {
                    int deleteImage = (new CatalogInventoryManagerDAL().DeleteProductImagebyImageID_DAL(id));
                    int iInvTemp_Id = Convert.ToInt32(hfInventoryID.Value);
                    //Bind and display the images
                    BindInventoriesImage(iInvTemp_Id);
                    panelDisplayImages.Visible = true;
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "repeaterImage_OnItemCommand");
                lblErr.Text = strErrCode;
            }
        }

        protected void RemoveProduct_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(hfInventoryID.Value);

                int removeProduct = (new CatalogInventoryManagerDAL().RemoveProduct_DAL(id));
                lblErrProduct.Text = "Product has been removed";
                Response.Redirect("~/Catalog/InventoryManager.aspx?");
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "RemoveProduct_Click");
                lblErr.Text = strErrCode;
            }
        }


    }
}