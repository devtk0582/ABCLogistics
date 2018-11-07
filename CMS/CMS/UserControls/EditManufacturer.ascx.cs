using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.DAL;

namespace CMS.UserControls
{
    public partial class EditManufacturer : System.Web.UI.UserControl
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        public event EventHandler SaveButtonClicked;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void BindData(int manuId)
        {            
            try
            {
                if (manuId == 0)
                {
                    return;
                }

                DataSet manufacturer = (new SettingsDAL()).GetManufacturerByID(manuId);

                if (manufacturer != null && manufacturer.Tables[0].Rows.Count > 0)
                {
                    txtManufacturerName.Text = manufacturer.Tables[0].Rows[0]["Manufactures_Desc"].ToString();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindData", manuId.ToString());
                lblErrorMsg.Text = strErrCode;
            }

        }

        public void Popup(string manuId)
        {
            try
            {
                lblErrorMsg.Text = string.Empty;
                upEditManufacturer.Visible = true;
                hfManufacturerID.Value = manuId;
                BindData(int.Parse(manuId));
                mpePopup.Show();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Popup", manuId);
                lblErrorMsg.Text = strErrCode;
            }
        }

        protected void lbSave_Click(object sender, EventArgs e)
        { 
            try
            {
                if (txtManufacturerName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Manufacturer name cannot be empty.";
                    mpePopup.Show();
                    return;
                }

                (new SettingsDAL()).UpdateManufacturer(int.Parse(hfManufacturerID.Value), txtManufacturerName.Text.Trim());
                ClearPanel();
                upEditManufacturer.Visible = false;
                SaveButtonClicked(sender, e);
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lbSave_Click");
                lblErrorMsg.Text = strErrCode;
            }
        }

        protected void lbCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPanel();
                upEditManufacturer.Visible = false;
                mpePopup.Hide();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "lbCancel_Click");
                lblErrorMsg.Text = strErrCode;
            }
        }

        private void ClearPanel()
        {
            txtManufacturerName.Text = "";
        }
    }
}