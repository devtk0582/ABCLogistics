﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.DAL;

namespace CMS.UserControls
{
    public partial class AddShippingClass : System.Web.UI.UserControl
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
                upAddShippingClass.Visible = true;
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
                if (txtShippingClassName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Shipping Class name cannot be empty.";
                    mpePopup.Show();
                    return;
                }

                (new SettingsDAL()).InsertShippingClass(txtShippingClassName.Text.Trim());
                ClearPanel();
                upAddShippingClass.Visible = false;
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
                upAddShippingClass.Visible = false;
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