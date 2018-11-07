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
    public partial class EditBillingCode : System.Web.UI.UserControl
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        public event EventHandler SaveButtonClicked;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void BindData(int billingCodeId)
        {            
            try
            {
                if (billingCodeId == 0)
                {
                    return;
                } 

                DataSet billingCodeMethod = (new SettingsDAL()).GetBillingCodeByID(billingCodeId);

                if (billingCodeMethod != null && billingCodeMethod.Tables[0].Rows.Count > 0)
                {
                    txtBillingCodeName.Text = billingCodeMethod.Tables[0].Rows[0]["BillingCode_Name"].ToString();
                    txtDesc.Text = billingCodeMethod.Tables[0].Rows[0]["BillingCode_Desc"].ToString();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindData",billingCodeId.ToString());
                lblErrorMsg.Text = strErrCode;
            }
        
        }

        public void Popup(string billingCodeId)
        {
            try
            {
                lblErrorMsg.Text = string.Empty;
                upEditBillingCode.Visible = true;
                hfBillingCodeID.Value = billingCodeId;
                BindData(int.Parse(billingCodeId));
                mpePopup.Show();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Popup", billingCodeId);
                lblErrorMsg.Text = strErrCode;
            }
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {      
            try
            {
                if (txtBillingCodeName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Billing Code name cannot be empty.";
                    mpePopup.Show();
                    return;
                }

                (new SettingsDAL()).UpdateBillingCode(int.Parse(hfBillingCodeID.Value), txtBillingCodeName.Text.Trim(), txtDesc.Text.Trim());
                ClearPanel();
                upEditBillingCode.Visible = false;
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
                upEditBillingCode.Visible = false;
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
            txtBillingCodeName.Text = "";
            txtDesc.Text = "";
        }
    }
}