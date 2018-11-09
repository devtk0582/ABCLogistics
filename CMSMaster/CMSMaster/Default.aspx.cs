using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMSMaster.DAL;

namespace CMSMaster
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblErrMessage.Text = string.Empty;
                if (!IsPostBack)
                {
                    txtUserID.Text = "admin@abclogistics.com";
                    tdMassage.InnerHtml = "Welcome to ABCLogistics Content Management System<br />" + DateTime.Now.ToLongDateString() + "<br />";
                    //BindSecurityQuestions();
                }
            }
            catch (Exception ex)
            {
                lblErrMessage.Text = ex.Message;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserID.Text == string.Empty)
                {
                    lblErrMessage.Text = "Please enter user name.";
                    return;
                }
                if (txtPWD.Text == string.Empty)
                {
                    lblErrMessage.Text = "Please enter password.";
                    return;
                }
                clsUser objUser = new clsUser();
                DataSet user = objUser.AuthenticateUser(txtUserID.Text.Trim(), txtPWD.Text.Trim());
                lblErrMessage.Text = "";
                if (user != null && user.Tables != null && user.Tables[0].Rows.Count > 0)
                {
                    Session["UserID"] = user.Tables[0].Rows[0]["User_Id"];
                    Session["UserName"] = user.Tables[0].Rows[0]["User_First"].ToString() + " " + user.Tables[0].Rows[0]["User_Last"].ToString();
                    Session["RoleID"] = user.Tables[0].Rows[0]["Role_Id"].ToString();
                  

                    if (cbRememberMe.Checked == true)
                    {
                        HttpCookie LoginCookie = new HttpCookie("LoginCookie");
                        LoginCookie.Value = txtUserID.Text;
                        LoginCookie.Expires = DateTime.Now.AddMinutes(20);

                        Response.Cookies.Add(LoginCookie);
                    }
                    //Context.Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUserID.Text.Trim(), true));
                    //FormsAuthentication.RedirectFromLoginPage(txtUserID.Text.Trim(), false);

                    Response.Redirect("~/Home.aspx", false);
                    
                }
                else
                {
                    lblErrMessage.Text = "Username or password is invalid.";
                    txtPWD.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                lblErrMessage.Text = ex.Message;
            }
        }

        protected void lnkForgotPasword_Click(object sender, EventArgs e)
        {

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtPWD.Text = string.Empty;
            txtUserID.Text = string.Empty;
        }
    }
}