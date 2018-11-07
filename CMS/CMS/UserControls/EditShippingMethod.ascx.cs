using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.DAL;
using System.Data;

namespace CMS.UserControls
{
    public partial class EditShippingMethod : System.Web.UI.UserControl
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        public event EventHandler SaveButtonClicked;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void BindData(int methodId)
        {   
            try
            {
                if (methodId == 0)
                {
                    return;
                }

                DataSet shippingMethod = (new SettingsDAL()).GetShippingMethodByID(methodId);

                if (shippingMethod != null && shippingMethod.Tables[0].Rows.Count > 0)
                {
                    txtShippingMethodName.Text = shippingMethod.Tables[0].Rows[0]["ShipMethod_Name"].ToString();
                    txtCost.Text = shippingMethod.Tables[0].Rows[0]["ShipMethod_Cost"].ToString();
                    txtLicense.Text = shippingMethod.Tables[0].Rows[0]["License"].ToString();
                    txtUserName.Text = shippingMethod.Tables[0].Rows[0]["UserName"].ToString();
                    txtPassword.Text = shippingMethod.Tables[0].Rows[0]["Password"].ToString();
                    txtAccountNumber.Text = shippingMethod.Tables[0].Rows[0]["AccountNumber"].ToString();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindData", methodId.ToString());
                lblErrorMsg.Text = strErrCode;
            }        
        }

        public void Popup(string methodId)
        {
            try
            {
                lblErrorMsg.Text = string.Empty;
                upEditShipMethod.Visible = true;
                hfMethodID.Value = methodId;
                BindData(int.Parse(methodId));
                mpePopup.Show();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Popup", methodId);
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
                //float cost = 0;
                //if (txtCost.Text.Trim() != "")
                //    cost = float.Parse(txtCost.Text.Trim());


                (new SettingsDAL()).UpdateShippingMethod(int.Parse(hfMethodID.Value), txtShippingMethodName.Text.Trim(), cost, txtLicense.Text.Trim(),
                    txtUserName.Text.Trim(), txtPassword.Text.Trim(), txtAccountNumber.Text.Trim());
                ClearPanel();
                upEditShipMethod.Visible = false;
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
                upEditShipMethod.Visible = false;
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