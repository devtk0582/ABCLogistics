using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.DAL;
using System.Web.UI.HtmlControls;

namespace CMS.Catalog
{
    public partial class Colors : System.Web.UI.Page
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindColors();
                clearInsertNewColorData();          
            }
        }

        private void BindColors()
        {         
            try
            {
                DataSet bindColors = (new CatalogColorsDAL()).GetColors_DAL();
               
                if (bindColors != null && bindColors.Tables[0].Rows.Count > 0)
                {
                    dlColors.DataSource = bindColors.Tables[0];
                    dlColors.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindColors");
                lblErr.Text = strErrCode;  
            }
        }// End of BindColors method...

        protected void AddNewColor_Click(object sender, EventArgs e)
        {
            try
            {
                ModlPopupColor.Show();
                lblErrMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "AddNewColor_Click");
                lblErr.Text = strErrCode;
            }
        }

        /*
         * Insert new Color
         */
        protected void InsertNewColor_Click(object sender, EventArgs e)
        {            
            try
            {
                if (txtBxNewColorName.Text.Trim() == string.Empty)
                {
                    ModlPopupColor.Show();
                    lblErrMessage.Text = "Please enter the color name.";
                    return;
                }
                if (txtBxNewColorCode.Text.Trim() == string.Empty)
                {
                    ModlPopupColor.Show();
                    lblErrMessage.Text = "Please enter the color code.";
                    return;
                }
               
                string colorName = txtBxNewColorName.Text.Trim();
                string colorCode = txtBxNewColorCode.Text.Trim();
                string colorValue = txtBxNewColorvalue.Text.Trim();

                if (txtBxNewColorName.Text != string.Empty && txtBxNewColorCode.Text != string.Empty)
                {
                    int insertNewColor = (new CatalogColorsDAL()).InsertNewColor_DAL(colorName, colorCode, colorValue);
                    if (insertNewColor == 0)
                    {
                        ModlPopupColor.Show();
                        lblErrMessage.Text = "Color Code is already exist.";
                        return;
                    }
                    else
                    {
                        BindColors();
                        clearInsertNewColorData();
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "InsertNewColor_Click");
                lblErr.Text = strErrCode;    
            }
        }//End of InsertNewColor_Click method...


        /*
         * Click on Color ID to popup edit window. 
         */
        protected void DataList_OnItemCommand(object sener, DataListCommandEventArgs e)
        {
            try
            {
                lblErrMessageUpdate.Text = string.Empty;
                if (e.CommandName == "PopupColor")
                {
                    int id = Convert.ToInt32(e.CommandArgument);//"Color_Id" CommandArgument
                    //Get data from database and populate it on the popup window.
                    DataSet editColor = (new CatalogColorsDAL()).GetColorByColorID_DAL(id);

                    hfColor.Value = editColor.Tables[0].Rows[0]["Color_Id"].ToString();
                    txtBxColorName.Text = editColor.Tables[0].Rows[0]["Color_Name"].ToString();
                    txtBxColorCode.Text = editColor.Tables[0].Rows[0]["Color_Code"].ToString();
                    txtBxColorValue.Text = editColor.Tables[0].Rows[0]["Color_Val"].ToString();

                    MdlPopupColorEdit.Show();                
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "DataList_OnItemCommand");
                lblErr.Text = strErrCode;
            }
        }

        protected void CancelInsertNewColor_Click(object sender, EventArgs e)
        {
            try
            {
                clearInsertNewColorData();
                ModlPopupColor.Hide();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "CancelInsertNewColor_Click");
                lblErr.Text = strErrCode;
            }
        }

        public void clearInsertNewColorData()
        {           
            txtBxNewColorName.Text = string.Empty;
            txtBxNewColorCode.Text = string.Empty;
            txtBxNewColorvalue.Text = string.Empty; 
        }

        protected void UpdateColor_Click(object sender, EventArgs e)
        {            
            try
            {
                if (txtBxColorName.Text.Trim() == string.Empty)
                {
                    MdlPopupColorEdit.Show();
                    lblErrMessageUpdate.Text = "Please enter the color name.";
                    return;
                }
                if (txtBxColorCode.Text.Trim() == string.Empty)
                {
                    MdlPopupColorEdit.Show();
                    lblErrMessageUpdate.Text = "Please enter the color code.";
                    return;
                }
               
                int id = Convert.ToInt32(hfColor.Value);
                string colorName = txtBxColorName.Text.Trim();
                string colorCode = txtBxColorCode.Text.Trim();
                string colorValue = txtBxColorValue.Text.Trim();

                if (txtBxColorName.Text != string.Empty && txtBxColorCode.Text != string.Empty)
                {
                    int updateColor = (new CatalogColorsDAL()).UpdateColor_DAL(id, colorName, colorCode, colorValue);
                    BindColors();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateColor_Click");
                lblErr.Text = strErrCode;  
            } 
        }

        /*
         * Delete Color
         */
        protected void DeleteColor_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(hfColor.Value);

                string deleteColor = (new CatalogColorsDAL()).DeleteColor_DAL(id);
                if (deleteColor != string.Empty)
                {
                    lblErrColor.Visible = true;
                    lblErrColor.Text = deleteColor;
                }
                BindColors();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "DeleteColor_Click");
                lblErr.Text = strErrCode; 
            } 
        }

        protected void CancelUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                MdlPopupColorEdit.Hide();
                lblErrMessageUpdate.Text = string.Empty;
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "CancelUpdate_Click");
                lblErr.Text = strErrCode;
            } 
        }

        //Place color to specific column.
        protected void DataList_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                HtmlTable tbl = (HtmlTable)e.Item.FindControl("TableItemTemplate");

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    string color = ((Label)e.Item.FindControl("lblColorValue")).Text;

                    tbl.BgColor = color;
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "DataList_OnItemDataBound");
                lblErr.Text = strErrCode; 
            }
        }

    }
}