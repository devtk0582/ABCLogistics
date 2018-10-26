using System;
using System.Web.UI;
using DotNetOpenAuth.OpenId.RelyingParty;
using TMSBusinessLogic;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.Extensions.UI;
using ABCLogisticsBusinessLogic;

namespace TMS
{
    public partial class GoogleAuth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckGoogleOpenID();
            }
        }

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
                                lblMsg.Text = CommonMessages.ERROR_INVALID_GOOGLE_IDENTIFIER;
                                return;
                            }
                            var fetch = response.GetExtension<FetchResponse>();
                            if (fetch != null)
                            {
                                LogInUser(fetch.GetAttributeValue(WellKnownAttributes.Contact.Email), id);
                            }
                            //FormsAuthentication.RedirectFromLoginPage(response.ClaimedIdentifier.ToString(), false);
                            break;
                        case AuthenticationStatus.Canceled:
                            lblMsg.Text = CommonMessages.MSG_GOOGLE_AUTHENTICATION_CANCELED;
                            break;
                        case AuthenticationStatus.Failed:
                            lblMsg.Text = CommonMessages.MSG_GOOGLE_AUTHENTICATION_FAILED;
                            break;
                        case AuthenticationStatus.SetupRequired:
                            lblMsg.Text = CommonMessages.MSG_GOOGLE_AUTHENTICATION_SETUP_REQUIRED;
                            break;
                        case AuthenticationStatus.ExtensionsOnly:
                            lblMsg.Text = CommonMessages.MSG_GOOGLE_AUTHENTICATION_EXTENSIONS_ONLY;
                            break;
                    }
                }
                else
                {
                    var request = openid.CreateRequest(System.Configuration.ConfigurationManager.AppSettings["GoogleOpenIDPath"].ToString(), new DotNetOpenAuth.OpenId.Realm(System.Configuration.ConfigurationManager.AppSettings["Domain"].ToString()));
                    var fetch = new FetchRequest();
                    fetch.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
                    var uiMode = new UIRequest();
                    uiMode.Mode = "popup";
                    uiMode.Icon = true;
                    request.AddExtension(uiMode);
                    request.AddExtension(fetch);
                    request.RedirectToProvider();
                }
            }
            catch (Exception ex)
            {
               lblMsg.Text = (new GlobalErrorLogsBLL()).LogGlobalError(ex, "TMS - GoogleAuth - CheckGoogleOpenID");
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

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "AuthReturn", "AuthReturn();", true);
                    }
                    else
                    {
                        lblMsg.Text = CommonMessages.ERROR_GLOBAL_NO_APPLICATION_ACCESS;
                    }
                }
                else
                {
                    lblMsg.Text = CommonMessages.ERROR_GLOBAL_ACCOUNT_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = (new GlobalErrorLogsBLL()).LogGlobalError(ex, "TMS - GoogleAuth - LogInUser");
            }

        }
    }
}