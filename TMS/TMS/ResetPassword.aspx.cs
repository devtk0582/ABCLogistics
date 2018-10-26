using ABCLogisticsBusinessLogic;
using System;
using TMSBusinessLogic;

namespace TMS
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["e"] != null && Request.QueryString["p"] != null)
                    {
                        string password = (new AESCryto()).DecryptString(Request.QueryString["p"].ToString());

                        if (ValidatePassword(password))
                        {
                            if ((new GlobalUsersBLL()).UpdatePassword(Request.QueryString["e"].ToString(), password))
                            {
                                lblMsg.Text = CommonMessages.MSG_PW_RESET_SUCCESS;
                            }
                            else
                            {
                                lblMsg.Text = CommonMessages.ERROR_PW_RESET_FAIL;
                            }
                        }
                        else
                        {
                            lblMsg.Text = CommonMessages.ERROR_PW_RESET_INVALID_PW;
                        }
                    }
                    else
                    {
                        lblMsg.Text = CommonMessages.ERROR_PW_RESET_INVALID_REQUEST;
                    }
                }
                catch (Exception ex)
                {
                    (new GlobalErrorLogsBLL()).LogGlobalError(ex, "TMS - ResetPassword - Page_Load");
                    lblMsg.Text = CommonMessages.ERROR_PW_RESET_UNKNOWN_ERROR;
                }
            }
        }

        private bool ValidatePassword(string password)
        {
            try
            {
                if (password.Length == 6 && System.Text.RegularExpressions.Regex.IsMatch(password, "^[a-zA-Z0-9]*$"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}