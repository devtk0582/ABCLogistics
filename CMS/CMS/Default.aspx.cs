using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.DAL;
using System.Drawing;

namespace CMS
{
    public partial class Default : System.Web.UI.Page
    {
        string ERROR_DISPLAY_MESSAGE = "An unknown error occured. Please contact admin with the reference ID #";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindPage();

                    txtUserID.Text = "cmsadmin";
                    txtPWD.Focus();
                }
                catch (Exception ex)
                {
                    string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "Page_Load");
                    lblErrMessage.Text = strErrCode;
                }
                //code che 
            }
        }

        private void BindPage()
        {
            try
            {
                UISettingsDAL dalCustomer = new UISettingsDAL();

                DataSet pageUISettings = dalCustomer.GetCustomerUIsettings();

                if (pageUISettings != null)
                {
                    if (pageUISettings.Tables[0].Rows.Count > 0)
                    {
                        trHeader.BgColor = pageUISettings.Tables[0].Rows[0]["HeaderBGColor"].ToString();
                        lblHeader.Font.Name = pageUISettings.Tables[0].Rows[0]["HeaderFontName"].ToString();
                        lblHeader.Font.Size = FontUnit.Parse(pageUISettings.Tables[0].Rows[0]["HeaderFontSize"].ToString());
                        lblHeader.ForeColor = Color.FromArgb(int.Parse(pageUISettings.Tables[0].Rows[0]["HeaderForeColor"].ToString(), System.Globalization.NumberStyles.HexNumber));
                        trFooter.BgColor = pageUISettings.Tables[0].Rows[0]["FooterBGColor"].ToString();
                        lblFooter.Font.Name = pageUISettings.Tables[0].Rows[0]["FooterFontName"].ToString();
                        lblFooter.Font.Size = FontUnit.Parse(pageUISettings.Tables[0].Rows[0]["FooterFontSize"].ToString());
                        lblFooter.ForeColor = Color.FromArgb(int.Parse(pageUISettings.Tables[0].Rows[0]["FooterForeColor"].ToString(), System.Globalization.NumberStyles.HexNumber));
                        tdSideBarLeft.BgColor = pageUISettings.Tables[0].Rows[0]["SideBarBGColor"].ToString();
                        tdSideBarRight.BgColor = pageUISettings.Tables[0].Rows[0]["SideBarBGColor"].ToString();
                        //lblSideBar.Font.Name = pageUISettings.Tables[0].Rows[0]["SideBarFontName"].ToString();
                        //lblSideBar.Font.Size = FontUnit.Parse(pageUISettings.Tables[0].Rows[0]["SideBarFontSize"].ToString());
                        //lblSideBar.ForeColor = Color.FromArgb(int.Parse(pageUISettings.Tables[0].Rows[0]["SideBarForeColor"].ToString(), System.Globalization.NumberStyles.HexNumber));

                        if (Convert.ToBoolean(pageUISettings.Tables[0].Rows[0]["HasHeaderLogo"]) == true)
                            imgHeader.ImageUrl = "~/ShowImage.ashx?Type=Header";
                        else
                            imgHeader.Visible = false;

                        if (Convert.ToBoolean(pageUISettings.Tables[0].Rows[0]["HasFooterLogo"]) == true)
                            imgFooter.ImageUrl = "~/ShowImage.ashx?Type=Footer";
                        else
                            imgFooter.Visible = false;
                    }

                    if (pageUISettings.Tables[1].Rows.Count > 0)
                    {
                        lblHeader.Text = pageUISettings.Tables[1].Rows[0]["HeaderMsg"].ToString();
                        lblFooter.Text = pageUISettings.Tables[1].Rows[0]["FooterMsg"].ToString();
                        //lblSideBar.Text = pageUISettings.Tables[1].Rows[0]["SideBarMsg"].ToString();
                        lblWelcome.Text = pageUISettings.Tables[1].Rows[0]["WelcomeMsg"].ToString().Replace("\n", "<br />");

                        if (pageUISettings.Tables[1].Rows[0]["HeaderMsg"].ToString() == string.Empty)
                        {
                            Page.Header.Title = pageUISettings.Tables[1].Rows[0]["HeaderMsg"].ToString();
                        }
                    }

                    SaveCustomerUISettings(pageUISettings);
                }
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "BindPage");
                lblErrMessage.Text = strErrCode;
            }
        }

        private void SaveCustomerUISettings(DataSet pageUISettings)
        {
            try
            {
                CustomerUI customerUI = new CustomerUI();
                customerUI.HeaderMessage = pageUISettings.Tables[1].Rows[0]["HeaderMsg"].ToString();
                customerUI.FooterMessage = pageUISettings.Tables[1].Rows[0]["FooterMsg"].ToString();
                customerUI.SideBarMessage = pageUISettings.Tables[1].Rows[0]["SideBarMsg"].ToString();
                customerUI.WelcomeMessage = pageUISettings.Tables[1].Rows[0]["WelcomeMsg"].ToString().Replace("\n", "<br />");

                customerUI.HeaderBGColor = pageUISettings.Tables[0].Rows[0]["HeaderBGColor"].ToString();
                customerUI.HeaderFontName = pageUISettings.Tables[0].Rows[0]["HeaderFontName"].ToString();
                customerUI.HeaderFontSize = pageUISettings.Tables[0].Rows[0]["HeaderFontSize"].ToString();
                customerUI.HeaderForeColor = pageUISettings.Tables[0].Rows[0]["HeaderForeColor"].ToString();
                customerUI.FooterBGColor = pageUISettings.Tables[0].Rows[0]["FooterBGColor"].ToString();
                customerUI.FooterFontName = pageUISettings.Tables[0].Rows[0]["FooterFontName"].ToString();
                customerUI.FooterFontSize = pageUISettings.Tables[0].Rows[0]["FooterFontSize"].ToString();
                customerUI.FooterForeColor = pageUISettings.Tables[0].Rows[0]["FooterForeColor"].ToString();
                customerUI.SideBarBGColor = pageUISettings.Tables[0].Rows[0]["SideBarBGColor"].ToString();
                customerUI.SideBarFontName = pageUISettings.Tables[0].Rows[0]["SideBarFontName"].ToString();
                customerUI.SideBarFontSize = pageUISettings.Tables[0].Rows[0]["SideBarFontSize"].ToString();
                customerUI.SideBarForeColor = pageUISettings.Tables[0].Rows[0]["SideBarForeColor"].ToString();

                customerUI.HasHeaderImage = Convert.ToBoolean(pageUISettings.Tables[0].Rows[0]["HasHeaderLogo"]);
                customerUI.HasFooterImage = Convert.ToBoolean(pageUISettings.Tables[0].Rows[0]["HasFooterLogo"]);
                customerUI.HasSideBarImage = false; // Convert.ToBoolean(pageUISettings.Tables[0].Rows[0]["HasSideBarLogo"]);
                Session["CustomerUI"] = customerUI;
            }
            catch (Exception ex)
            {
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "SaveCustomerUISettings", pageUISettings.ToString());
                lblErrMessage.Text = strErrCode;
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
                UsersDAL objUser = new UsersDAL();
                DataSet user = objUser.AuthenticateUser(txtUserID.Text.Trim(), txtPWD.Text.Trim());
                lblErrMessage.Text = "";
                if (user != null && user.Tables != null && user.Tables[0].Rows.Count > 0)
                {
                    Session["UserID"] = user.Tables[0].Rows[0]["User_Id"];
                    Session["UserName"] = user.Tables[0].Rows[0]["User_First"].ToString() + " " + user.Tables[0].Rows[0]["User_Last"].ToString();
                    Session["RoleID"] = user.Tables[0].Rows[0]["Role_Id"].ToString();
                    Session["RoleName"] = user.Tables[0].Rows[0]["Role_Name"].ToString();
                    Session["OrderRoleName"] = user.Tables[0].Rows[0]["Role_Name"].ToString();
                    //if (cbRememberMe.Checked == true)
                    //{
                    //    HttpCookie LoginCookie = new HttpCookie("LoginCookie");
                    //    LoginCookie.Value = txtUserID.Text;
                    //    LoginCookie.Expires = DateTime.Now.AddMinutes(20);

                    //    Response.Cookies.Add(LoginCookie);
                    //}


                    Response.Redirect("~/Catalog/Products.aspx", false);
                    //lblErrMessage.Text = user.Tables[0].Rows[0]["User_First_Name"].ToString() + " " + user.Tables[0].Rows[0]["User_Last_Name"].ToString();
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
                string strErrCode = ERROR_DISPLAY_MESSAGE + "," + (new Error_Log()).LogErrorIntoDB(ex, "btnLogin_Click");
                lblErrMessage.Text = strErrCode;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtPWD.Text = string.Empty;
            txtUserID.Text = string.Empty;
        }
    }
}