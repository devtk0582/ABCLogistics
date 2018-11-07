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
    public partial class EditPackaging : System.Web.UI.UserControl
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        public event EventHandler SaveButtonClicked;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void BindData(int packagingId)
        {   
            try
            {
                if (packagingId == 0)
                {
                    return;
                }

                DataSet packaging = (new SettingsDAL()).GetPackagingByID(packagingId);

                if (packaging != null && packaging.Tables[0].Rows.Count > 0)
                {
                    txtPackagingName.Text = packaging.Tables[0].Rows[0]["Package_Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindData", packagingId.ToString());
                lblErrorMsg.Text = strErrCode;
            }
        
        }

        public void Popup(string packagingId)
        {
            try
            {
                lblErrorMsg.Text = string.Empty;
                upEditPackaging.Visible = true;
                hfPackagingID.Value = packagingId;
                BindData(int.Parse(packagingId));
                mpePopup.Show();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Popup", packagingId);
                lblErrorMsg.Text = strErrCode;
            }
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPackagingName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Packaging name cannot be empty.";
                    mpePopup.Show();
                    return;
                }

                (new SettingsDAL()).UpdatePackaging(int.Parse(hfPackagingID.Value), txtPackagingName.Text.Trim());
                ClearPanel();
                upEditPackaging.Visible = false;
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
                upEditPackaging.Visible = false;
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
            txtPackagingName.Text = "";
        }
    }
}