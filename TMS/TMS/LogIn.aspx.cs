using System;
using System.Web;
using TMSBusinessLogic;
using System.Security.Cryptography;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using System.Net.Mail;
using ABCLogisticsBusinessLogic;

namespace TMS
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrMessage.Text = string.Empty;

            if (!IsPostBack)
            {
                CheckGoogleOpenID();
                if (CheckCookie())
                {
                    string username = GetCookieUserName();
                    txtUserID.Text = username;
                }
                else
                {
                    txtUserID.Focus();
                }

            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text.Trim() == string.Empty)
            {
                lblErrMessage.Text = CommonMessages.MSG_LOG_IN_EMPTY_EMAIL;
                return;
            }

            if (txtPWD.Text.Trim() == string.Empty)
            {
                lblErrMessage.Text = CommonMessages.MSG_LOG_IN_EMPTY_PASSWORD;
                return;
            }


            try
            {
                GlobalUsersBLL globalUsersBLL = new GlobalUsersBLL();
                var globalUserDetails = globalUsersBLL.AuthenticateUser(txtUserID.Text.Trim(), EncryptString(txtPWD.Text.Trim()));

                if (globalUserDetails != null)
                {
                    int appUserID = globalUsersBLL.GetApplicationUserByUser(globalUserDetails.UserID, ABCLogisticsApp.TMS);

                    if (appUserID > 0)
                    {
                        Session["GlobalUserID"] = globalUserDetails.UserID.ToString();
                        Session["UserName"] = globalUserDetails.FirstName + " " + globalUserDetails.LastName;
                        Session["FirstName"] = globalUserDetails.FirstName;
                        Session["LastName"] = globalUserDetails.LastName;
                        Session["RoleID"] = globalUserDetails.RoleID.ToString();
                        Session["UserEmail"] = globalUserDetails.Email;
                        Session["Password"] = globalUserDetails.Password;
                        Session["AppUserID"] = appUserID.ToString();

                        if (cbRememberMe.Checked == true)
                        {
                            RememberUser(txtUserID.Text.Trim());
                        }
                        Response.Redirect("~/home", false);
                    }
                    else
                    {
                        lblErrMessage.Text = CommonMessages.ERROR_GLOBAL_NO_APPLICATION_ACCESS;
                    }
                }
                else
                {
                    lblErrMessage.Text = CommonMessages.ERROR_GLOBAL_ACCOUNT_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                lblErrMessage.Text = (new GlobalErrorLogsBLL()).LogGlobalError(ex, "TMS - LogIn - btnLogin_Click");
                lblErrMessage.Visible = true;
            }
        }

        protected void lbtnForgotPwd_Click(object sender, EventArgs e)
        {
            try
            {
                lblValidate.Text = string.Empty;
                txtEmail.Text = string.Empty;
                mpeForgotPW.Show();
            }
            catch (Exception ex)
            {
                lblErrMessage.Text = (new GlobalErrorLogsBLL()).LogGlobalError(ex, "TMS - LogIn - lbtnForgotPwd_Click");
            }
        }

        protected void lbSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmail.Text.Trim() == "")
                {
                    lblValidate.Text = CommonMessages.ERROR_FORGOT_PW_EMPTY_EMAIL;
                    mpeForgotPW.Show();
                    return;
                }

                if (!GlobalUtil.IsEmail(txtEmail.Text.Trim()))
                {
                    lblValidate.Text = CommonMessages.ERROR_FORGOT_PW_INVALID_EMAIL;
                    mpeForgotPW.Show();
                    return;
                }

                ResetPassword();
            }
            catch (Exception ex)
            {
                lblErrMessage.Text = (new GlobalErrorLogsBLL()).LogGlobalError(ex, "TMS - LogIn - lbSubmit_Click");
            }
        }

        private void ResetPassword()
        {
            try
            {
                string randomPassword = GlobalUtil.GeneratePassword();

                string name = (new GlobalUsersBLL()).GetUserNameByEmail(txtEmail.Text.Trim());
                if (name != string.Empty)
                {
                    SendEmail(txtEmail.Text.Trim(), name, randomPassword);
                    lbSubmit.Visible = false;
                    lbCancel.Text = "Exit";
                    lblValidate.Text = CommonMessages.MSG_FORGOT_PW_EMAIL_SENT;
                }
                else
                {
                    lblValidate.Text = CommonMessages.ERROR_FORGOT_PW_EMAIL_NOT_FOUND;
                }
                mpeForgotPW.Show();
            }
            catch (Exception ex)
            {
                lblErrMessage.Text = (new GlobalErrorLogsBLL()).LogGlobalError(ex, "TMS - LogIn - ResetPassword");
            }
        }

        private void SendEmail(string email, string name, string password)
        {
            try
            {
                string subject = CommonMessages.FORGOT_PW_EMAIL_SUBJECT;
                MailMessage message;

                string mailBody = GlobalUtil.GetTemplateContent(Server.MapPath("~/MailTemplates/ForgotPW.htm"));

                if (mailBody != string.Empty)
                {
                    mailBody = mailBody.Replace("[[txtName]]", name);
                    mailBody = mailBody.Replace("[[txtLogin]]", email);
                    mailBody = mailBody.Replace("[[txtPassword]]", password);
                    mailBody = mailBody.Replace("[[txtResetLink]]", System.Configuration.ConfigurationManager.AppSettings["PasswordResetURL"].ToString() + "e=" + email + "&p=" + (new AESCryto()).EncryptToString(password));

                    message = new MailMessage();
                    message.To.Add(email);
                    message.Subject = subject;
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailBody.Replace("[[txtLogo]]", @"<img src=""cid:Logo"" />"), null, "text/html");
                    LinkedResource imageResource = new LinkedResource(Server.MapPath("~/images/forgot-password-logo.gif"), "image/gif");
                    imageResource.ContentId = "Logo";
                    htmlView.LinkedResources.Add(imageResource);
                    //message.Body = body.Replace("[[txtLogo]]", "");
                    message.IsBodyHtml = true;
                    message.AlternateViews.Add(htmlView);
                    message.Priority = MailPriority.Normal;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private bool CheckCookie()
        {

            return Request.Cookies["TMSUserName"] != null && Request.Cookies["TMSUserName"].Name != "" &&
                GetCookieUserName() != "" ? true : false;
        }

        private string GetCookieUserName()
        {
            string username = "";
            if (Request.Cookies["TMSUserName"] != null)
            {
                HttpCookie cookie = Request.Cookies["TMSUserName"];
                username = Request.Cookies["TMSUserName"].Value;
            }

            return username;
        }

        private void RememberUser(string userName)
        {
            try
            {
                HttpCookie LoginCookie;

                if (Request.Cookies["TMSUserName"] == null)
                {
                    LoginCookie = new HttpCookie("TMSUserName", userName);
                    LoginCookie.Expires = DateTime.Now.AddYears(10);

                    Response.Cookies.Add(LoginCookie);
                }
                else
                {
                    LoginCookie = Request.Cookies["TMSUserName"];
                    LoginCookie.Value = userName;
                    LoginCookie.Expires = DateTime.Now.AddYears(10);
                    Response.Cookies.Add(LoginCookie);
                }
                Response.Write(Request.Cookies["TMSUserName"].Value);
            }

            catch (Exception ex)
            {
                lblErrMessage.Text = (new GlobalErrorLogsBLL()).LogGlobalError(ex, "TMS - LogIn - RememberUser");
            }
        }

        public static string EncryptString(string strInput)
        {
            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
            byte[] bytesValue = System.Text.Encoding.UTF8.GetBytes(strInput);
            byte[] bytesHash = hashAlg.ComputeHash(bytesValue);
            return Convert.ToBase64String(bytesHash);
        }

        #region Google Open ID
        private void CheckGoogleOpenID()
        {
            try
            {
                var openid = new OpenIdRelyingParty();
                var response = openid.GetResponse();

                if (response != null)
                {
                    switch (response.Status)
                    {
                        case AuthenticationStatus.Authenticated:

                            var id = GlobalUtil.StringToGUID(response.ClaimedIdentifier.ToString());
                            if (id == Guid.Empty)
                            {
                                lblErrMessage.Text = CommonMessages.ERROR_INVALID_GOOGLE_IDENTIFIER;
                                return;
                            }
                            var fetch = response.GetExtension<FetchResponse>();
                            if (fetch != null)
                            {
                                LogInUser(fetch.GetAttributeValue(WellKnownAttributes.Contact.Email), id);
                            }
                            else
                            {
                                lblErrMessage.Text = CommonMessages.ERROR_GOOGLE_ACCOUNT_NOT_FOUND;
                            }
                            break;
                        case AuthenticationStatus.Canceled:
                            lblErrMessage.Text = CommonMessages.MSG_GOOGLE_AUTHENTICATION_CANCELED;
                            break;
                        case AuthenticationStatus.Failed:
                            lblErrMessage.Text = CommonMessages.MSG_GOOGLE_AUTHENTICATION_FAILED;
                            break;
                    }
                }
                else
                {
                    var request = openid.CreateRequest(System.Configuration.ConfigurationManager.AppSettings["GoogleOpenIDPath"].ToString(), new DotNetOpenAuth.OpenId.Realm(System.Configuration.ConfigurationManager.AppSettings["Domain"].ToString()));
                    var fetch = new FetchRequest();
                    fetch.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
                    request.Mode = DotNetOpenAuth.OpenId.AuthenticationRequestMode.Immediate;
                    request.AddExtension(fetch);
                    request.RedirectToProvider();
                }
            }
            catch (Exception ex)
            {
                lblErrMessage.Text = (new GlobalErrorLogsBLL()).LogGlobalError(ex, "TMS - LogIn - CheckGoogleOpenID");
            }
        }

        private void LogInUser(string email, Guid identifier)
        {
            try
            {
                GlobalUsersBLL globalUsersBLL = new GlobalUsersBLL();
                var globalUserDetails = globalUsersBLL.AuthenticateUserByOpenID(email, identifier);
                if (globalUserDetails != null)
                {
                    int appUserID = globalUsersBLL.GetApplicationUserByUser(globalUserDetails.UserID, ABCLogisticsApp.TMS);

                    if (appUserID > 0)
                    {
                        Session["GlobalUserID"] = globalUserDetails.UserID.ToString();
                        Session["UserName"] = globalUserDetails.FirstName + " " + globalUserDetails.LastName;
                        Session["FirstName"] = globalUserDetails.FirstName;
                        Session["LastName"] = globalUserDetails.LastName;
                        Session["RoleID"] = globalUserDetails.RoleID.ToString();
                        Session["UserEmail"] = globalUserDetails.Email;
                        Session["Password"] = globalUserDetails.Password;
                        Session["AppUserID"] = appUserID.ToString();
                        Session["GoogleID"] = identifier;

                        if (cbRememberMe.Checked == true)
                        {
                            RememberUser(txtUserID.Text.Trim());
                        }
                        Response.Redirect("~/home", true);
                    }
                    else
                    {
                        lblErrMessage.Text = CommonMessages.ERROR_GLOBAL_NO_APPLICATION_ACCESS;
                    }
                }
                else
                {
                    lblErrMessage.Text = CommonMessages.ERROR_GLOBAL_ACCOUNT_NOT_FOUND;
                    txtUserID.Focus();
                }
            }
            catch (Exception ex)
            {
                lblErrMessage.Text = (new GlobalErrorLogsBLL()).LogGlobalError(ex, "TMS - LogIn - LogInUser");
            }
        }

        #endregion

        protected void lnkForgotPasword_Click(object sender, EventArgs e)
        {
            lblValidate.Text = "";
            txtEmail.Text = "";
            lbSubmit.Visible = true;
            lbCancel.Text = "Cancel";
            mpeForgotPW.Show();
        }
    }
}
