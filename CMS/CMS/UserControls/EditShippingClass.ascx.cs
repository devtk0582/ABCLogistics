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
    public partial class EditShippingClass : System.Web.UI.UserControl
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";

        public event EventHandler SaveButtonClicked;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void BindData(int shipClassId)
        {            
            try
            {
                if (shipClassId == 0)
                {
                    return;
                }

                DataSet shippingClass = (new SettingsDAL()).GetShippingClassByID(shipClassId);

                if (shippingClass != null && shippingClass.Tables[0].Rows.Count > 0)
                {
                    txtShippingClassName.Text = shippingClass.Tables[0].Rows[0]["ShipClass"].ToString();
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindData", shipClassId.ToString());
                lblErrorMsg.Text = strErrCode;
            }

        }

        public void Popup(string shipClassId)
        {
            try
            {
                lblErrorMsg.Text = string.Empty;
                upEditShippingClass.Visible = true;
                hfShippingClassID.Value = shipClassId;
                BindData(int.Parse(shipClassId));
                mpePopup.Show();
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Popup", shipClassId);
                lblErrorMsg.Text = strErrCode;
            }
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {   
            try
            {
                if (txtShippingClassName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Shipping Class name cannot be empty.";
                    mpePopup.Show();
                    return;
                }

                (new SettingsDAL()).UpdateShippingClass(int.Parse(hfShippingClassID.Value), txtShippingClassName.Text.Trim());
                ClearPanel();
                upEditShippingClass.Visible = false;
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
                upEditShippingClass.Visible = false;
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
            txtShippingClassName.Text = "";
        }
    }
}