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
    public partial class Sizes : System.Web.UI.Page
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    hfCurrentSizeID.Value = "0";
                    FillGridView();
                    clearAddNewSize();
                }
                catch (Exception ex)
                {
                    string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Page_Load");
                    lblErr.Text = strErrCode;
                }
            }
        }

        /*
         * Insert a new Color. 
         */
        protected void InsertNewSize_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbxSizeCode.Text.Trim() == string.Empty)
                {
                    ModlPopup2.Show();
                    lblErrMessage.Text = "Please enter the size code.";
                    return;
                }
                if (txtbxSizeName.Text.Trim() == string.Empty)
                {
                    ModlPopup2.Show();
                    lblErrMessage.Text = "Please enter the size name.";
                    return;
                }

                if (txtbxSizeCode.Text != string.Empty && txtbxSizeName.Text != string.Empty)
                {
                    int insertNewSize = (new CatalogSizesDAL()).InsertNewSize_DAL(txtbxSizeCode.Text.Trim(),
                                                 txtbxSizeName.Text.Trim());

                    if (insertNewSize == 0)
                    {
                        ModlPopup2.Show();
                        lblErrMessage.Text = "Size Code is already exist.";
                        return;
                    }
                    else
                    {
                        FillGridView();
                        clearAddNewSize();
                    }
                }
                else
                {
                    lblErrProduct.Text = "No Size found!";
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "InsertNewSize_Click");
                lblErr.Text = strErrCode;
            }
        }


        /*
         * Update the size.
         */
        protected void UpdateSize_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSizeCode.Text.Trim() == string.Empty)
                {
                    ModlPopup.Show();
                    lblErrMessageUpdate.Text = "Please enter the size code.";
                    return;
                }
                if (txtSizeName.Text.Trim() == string.Empty)
                {
                    ModlPopup.Show();
                    lblErrMessageUpdate.Text = "Please enter the size name.";
                    return;
                }

                int id = Convert.ToInt32(HiddenField.Value);
                string sizeCode = txtSizeCode.Text.Trim();
                string lblSizeName = txtSizeName.Text.Trim();
                if (txtSizeCode.Text != string.Empty && txtSizeName.Text != string.Empty)
                {
                    int updateSize = (new CatalogSizesDAL()).UpdateSize_DAL(id, sizeCode, lblSizeName);
                    FillGridView();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateSize_Click");
                lblErr.Text = strErrCode;
            }
        }

        //Clear text box
        public void clearAddNewSize()
        {
            txtbxSizeCode.Text = string.Empty;
            txtbxSizeName.Text = string.Empty;
            lblErrMessageUpdate.Text = string.Empty;
        }

        /*
         * Fill the sizes to gridView 
         */
        private void FillGridView()
        {
            try
            {
                DataSet fillgridViewSize = (new CatalogSizesDAL().GetSizes_DAL(0));

                if (fillgridViewSize != null && fillgridViewSize.Tables[0].Rows.Count > 0)
                {
                    DataView dv = fillgridViewSize.Tables[0].DefaultView;
                    if (ViewState["sortBy"] != null)
                    {
                        dv.Sort = ViewState["sortBy"].ToString();
                    }
                    gridViewSizes.DataSource = dv;
                    gridViewSizes.DataBind();
                }
                else
                {
                    gridViewSizes.DataSource = null;
                    gridViewSizes.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "FillGridView");
                lblErr.Text = strErrCode;
            }
        }//End of FillGridView method...

        /*
         * Sort the Sizes by column
         */
        protected void GridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                ViewState["sortBy"] = e.SortExpression + " " + GetSortDirection(e.SortExpression);

                FillGridView();
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
        }


        protected void gridViewSizes_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gridViewSizes.PageIndex = e.NewPageIndex;
                FillGridView();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetSortDirection");
                lblErr.Text = strErrCode;
            }
        }



        /*
         * Change the color of the row when mouse over.
         */
        //protected void GridView_OnRowCreated(object sender, GridViewRowEventArgs e)
        //{            
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
        //        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
        //        //e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex.ToString()));
        //    }
        //}

        /*
         * Click on Size ID to popup edit window. 
         */
        protected void GridView_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                lblErrProduct.Text = string.Empty;
                lblErrMessageUpdate.Text = string.Empty;
                if (e.CommandName == "Popup")
                {
                    int id = Convert.ToInt32(e.CommandArgument);  //Size_ID

                    try
                    {
                        //Get data from database and populate it on the popup window.  
                        DataSet editSize = (new CatalogSizesDAL()).GetSizeBySizeID_DAL(id);
                        HiddenField.Value = editSize.Tables[0].Rows[0]["Size_ID"].ToString();
                        txtSizeCode.Text = editSize.Tables[0].Rows[0]["Size_Code"].ToString();
                        txtSizeName.Text = editSize.Tables[0].Rows[0]["Size_Name"].ToString();

                        ModlPopup.Show();
                    }
                    catch (Exception ex)
                    {
                        lblErrProduct.Text = ex.Message;
                    }
                }
                else if (e.CommandName == "ToggleSize")
                {
                    lblErrProduct.Text = string.Empty;
                    hfCurrentSizeID.Value = e.CommandArgument.ToString();

                    CatalogSizesDAL sizeDAL = new CatalogSizesDAL();
                    if (((LinkButton)e.CommandSource).Text == "Inactive")
                        sizeDAL.DeleteSize_DAL(int.Parse(e.CommandArgument.ToString()), true);
                    else
                    {
                        sizeDAL.DeleteSize_DAL(int.Parse(e.CommandArgument.ToString()), false);
                        string deleteSize = (new CatalogSizesDAL()).DeleteSize_DAL(int.Parse(e.CommandArgument.ToString()), false);

                        if (deleteSize != string.Empty)
                        {
                            lblErrProduct.Visible = true;
                            lblErrProduct.Text = "Unable to Inactive the size as it is currently in use by the product.";
                            FillGridView();
                        }
                    }
                    FillGridView();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "GridView_OnRowCommand");
                lblErr.Text = strErrCode;
            }

        }//End of GridView_OnRowCommand...  

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

        protected void Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                ModlPopup.Hide();
                lblErrMessageUpdate.Text = string.Empty;
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Cancel_Click");
                lblErr.Text = strErrCode;
            }
        }

        protected void addNewSize_Click(object sender, EventArgs e)
        {
            try
            {
                ModlPopup2.Show();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "addNewSize_Click");
                lblErr.Text = strErrCode;
            }
        }

        protected void CancelInsertNewColor_Click(object sender, EventArgs e)
        {
            try
            {
                clearAddNewSize();
                ModlPopup2.Hide();
                lblErrMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "CancelInsertNewColor_Click");
                lblErr.Text = strErrCode;
            }
        }

        protected void DeleteSize_Click(object sender, EventArgs e)
        {
            //int id = Convert.ToInt32(HiddenField.Value);

            //try
            //{
            //    string deleteSize = (new CatalogSizesDAL()).DeleteSize_DAL(id,);
            //    if (deleteSize != string.Empty)
            //    {
            //        lblErrMessageUpdate.Visible = true;
            //        lblErrMessageUpdate.Text = deleteSize;
            //        ModlPopup.Show();
            //    }
            //    FillGridView();
            //}
            //catch (Exception ex)
            //{
            //    lblErrProduct.Text = ex.Message;
            //} 
        }
    }
}