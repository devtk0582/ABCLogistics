using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.DAL;

namespace CMS.UserControls
{
    public partial class AddShippingMethod : System.Web.UI.UserControl
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        public event EventHandler SaveButtonClicked;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Popup()
        {
            try
            {
                lblErrorMsg.Text = string.Empty;
                upAddShipMethod.Visible = true;
                mpePopup.Show();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Popup");
                lblErrorMsg.Text = strErrCode;
            }
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {           

            try
            {
                if (txtShippingMethodName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Shipping method name cannot be empty.";
                    mpePopup.Show();
                    return;
                }

                if (txtCost.Text.Trim() == string.Empty)
                {
                    txtCost.Text = "0";
                }

                double cost;
                if (!Double.TryParse(txtCost.Text, out cost))
                {
                    lblErrorMsg.Text = "Please enter a valid Cost.";
                    mpePopup.Show();
                    return;
                }
                
                cost = Convert.ToDouble(txtCost.Text.Trim());
                //if (txtCost.Text.Trim() != "")
                //    cost = float.Parse(txtCost.Text.Trim());

                (new SettingsDAL()).InsertShippingMethod(txtShippingMethodName.Text.Trim(), cost, txtLicense.Text.Trim(), 
                    txtUserName.Text.Trim(), txtPassword.Text.Trim(), txtAccountNumber.Text.Trim()  );
                ClearPanel();
                upAddShipMethod.Visible = false;
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
                upAddShipMethod.Visible = false;
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
            txtShippingMethodName.Text = "";
            txtCost.Text = "";
            txtLicense.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtAccountNumber.Text = "";
        }
    }
}