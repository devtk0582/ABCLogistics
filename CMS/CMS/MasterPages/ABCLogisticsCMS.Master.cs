using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.DAL;
using System.Drawing;

namespace CMS.MasterPages
{
    public partial class ABCLogisticsCMS : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (Session["UserID"] != null)
             {
                 if (!IsPostBack)
                 {
              
                    BindPageUI();
                    if (Session["RoleID"] != null)
                    {
                        if (Session["UserName"] != null)
                        {
                            lblwelcomeUser.Text = Session["UserName"].ToString();  // ***cahnge to user who login later
                            lblcurrentDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");

                            BindMainMenus();
                            LinkButton lb = null;
                            if (Session["MainMenu"] != null && Session["MainMenuID"] != null)
                            {
                                ClearCss(rptMainMenu);
                                foreach (RepeaterItem ri in rptMainMenu.Items)
                                {
                                    lb = (LinkButton)ri.FindControl("lnk");
                                    if (lb.Text == Session["MainMenu"].ToString())
                                    {
                                        lb.Attributes.Add("class", "currentMainMenu");
                                        break;
                                    }
                                }
                                BindSubmenus(int.Parse(Session["MainMenuID"].ToString()));
                                if (Session["SubMenu"] != null)
                                {
                                    ClearCss(rptsubMenu);
                                    foreach (RepeaterItem ri in rptsubMenu.Items)
                                    {
                                        lb = (LinkButton)ri.FindControl("lnk");
                                        if (lb.Text == Session["SubMenu"].ToString())
                                        {
                                            lb.Attributes.Add("class", "currentSubMenu");
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (rptsubMenu.Items.Count > 0)
                                    {
                                        ((LinkButton)rptsubMenu.Items[0].FindControl("lnk")).Attributes.Add("class", "currentSubMenu");
                                    }
                                }
                            }
                            else
                            {
                                if (rptMainMenu.Items.Count > 0)
                                {
                                    Session["MainMenu"] = ((LinkButton)rptMainMenu.Items[0].FindControl("lnk")).Text;
                                    Session["MainMenuID"] = ((LinkButton)rptMainMenu.Items[0].FindControl("lnk")).CommandArgument;
                                    ((LinkButton)rptMainMenu.Items[0].FindControl("lnk")).Attributes.Add("class", "currentMainMenu");
                                    BindSubmenus(int.Parse(((LinkButton)rptMainMenu.Items[0].FindControl("lnk")).CommandArgument));
                                    if (rptsubMenu.Items.Count > 0)
                                        ((LinkButton)rptsubMenu.Items[0].FindControl("lnk")).Attributes.Add("class", "currentSubMenu");
                                }
                            }

                        }
                    }
                }
               
                
           }
             else
             {
                 Response.Redirect("~/Default.aspx");

             }
        }

        private void BindPageUI()
        {
            if (Session["CustomerUI"] != null)
            {
                CustomerUI customerUI = (CustomerUI)Session["CustomerUI"];
                if (customerUI != null)
                {
                    lblHeader.Text = customerUI.HeaderMessage;
                    lblFooter.Text = customerUI.FooterMessage;

                    if (customerUI.HeaderBGColor != string.Empty)
                    {
                        trHeader.BgColor = customerUI.HeaderBGColor;
                    }
                    if (customerUI.HeaderFontName != string.Empty)
                    {
                        lblHeader.Font.Name = customerUI.HeaderFontName;
                        lblwelcomeUser.Font.Name = customerUI.HeaderFontName;
                        lblcurrentDate.Font.Name = customerUI.HeaderFontName;
                        lkbtnSignOut.Font.Name = customerUI.HeaderFontName; 
                    }
                    if (customerUI.HeaderFontSize != string.Empty)
                    {
                        lblHeader.Font.Size = FontUnit.Parse(customerUI.HeaderFontSize);
                        lblwelcomeUser.Font.Size = FontUnit.Parse(customerUI.HeaderFontSize);
                        lblcurrentDate.Font.Size = FontUnit.Parse(customerUI.HeaderFontSize);
                        lkbtnSignOut.Font.Size = FontUnit.Parse(customerUI.HeaderFontSize);
                    }
                    if (customerUI.HeaderForeColor != string.Empty)
                    {
                        lblHeader.ForeColor = Color.FromArgb(int.Parse(customerUI.HeaderForeColor, System.Globalization.NumberStyles.HexNumber));
                        lblwelcomeUser.ForeColor = Color.FromArgb(int.Parse(customerUI.HeaderForeColor, System.Globalization.NumberStyles.HexNumber));
                        lblcurrentDate.ForeColor = Color.FromArgb(int.Parse(customerUI.HeaderForeColor, System.Globalization.NumberStyles.HexNumber));
                        lkbtnSignOut.ForeColor = Color.FromArgb(int.Parse(customerUI.HeaderForeColor, System.Globalization.NumberStyles.HexNumber)); 
                    }
                    if (customerUI.FooterBGColor != string.Empty)
                    {
                        trFooter.BgColor = customerUI.FooterBGColor;
                    }
                    if (customerUI.FooterFontName != string.Empty)
                    {
                        lblFooter.Font.Name = customerUI.FooterFontName;
                    }
                    if (customerUI.FooterFontSize != string.Empty)
                    {
                        lblFooter.Font.Size = FontUnit.Parse(customerUI.FooterFontSize);
                    }
                    if (customerUI.FooterForeColor != string.Empty)
                    {
                        lblFooter.ForeColor = Color.FromArgb(int.Parse(customerUI.FooterForeColor, System.Globalization.NumberStyles.HexNumber));
                    }

                    if (Convert.ToBoolean(customerUI.HasHeaderImage) == true)
                        imgHeader.ImageUrl = "~/ShowImage.ashx?Type=Header";
                    else
                        imgHeader.Visible = false;

                    if (Convert.ToBoolean(customerUI.HasFooterImage) == true)
                        imgFooter.ImageUrl = "~/ShowImage.ashx?Type=Footer";
                    else
                        imgFooter.Visible = false;
                }
            }
            else
            {
                //BindPage();
            }
        }

        private void BindPage()
        {
            UISettingsDAL dalCustomer = new UISettingsDAL();
            DataSet pageMsgs = dalCustomer.GetCustomerMessages();
            if (pageMsgs != null && pageMsgs.Tables[0].Rows.Count > 0)
            {
                lblHeader.Text = pageMsgs.Tables[0].Rows[0]["HeaderMsg"].ToString();
                lblFooter.Text = pageMsgs.Tables[0].Rows[0]["FooterMsg"].ToString();
                //lblSideBar.Text = pageMsgs.Tables[0].Rows[0]["SideBarMsg"].ToString();
                //lblWelcome.Text = pageMsgs.Tables[0].Rows[0]["WelcomeMsg"].ToString().Replace("\n", "<br />");
            }

            DataSet pageUISettings = dalCustomer.GetCustomerUIsettings();
            if (pageUISettings != null && pageUISettings.Tables[0].Rows.Count > 0)
            {
                trHeader.BgColor = pageUISettings.Tables[0].Rows[0]["HeaderBGColor"].ToString();
                lblHeader.Font.Name = pageUISettings.Tables[0].Rows[0]["HeaderFontName"].ToString();
                lblHeader.Font.Size = FontUnit.Parse(pageUISettings.Tables[0].Rows[0]["HeaderFontSize"].ToString());
                lblHeader.ForeColor = Color.FromArgb(int.Parse(pageUISettings.Tables[0].Rows[0]["HeaderForeColor"].ToString(), System.Globalization.NumberStyles.HexNumber));
                trFooter.BgColor = pageUISettings.Tables[0].Rows[0]["FooterBGColor"].ToString();
                lblFooter.Font.Name = pageUISettings.Tables[0].Rows[0]["FooterFontName"].ToString();
                lblFooter.Font.Size = FontUnit.Parse(pageUISettings.Tables[0].Rows[0]["FooterFontSize"].ToString());
                lblFooter.ForeColor = Color.FromArgb(int.Parse(pageUISettings.Tables[0].Rows[0]["FooterForeColor"].ToString(), System.Globalization.NumberStyles.HexNumber));

                //divHeader.Style["background-color"] = "#" + pageUISettings.Tables[0].Rows[0]["HeaderBGColor"].ToString();
                //divHeader.Style["font-family"] = pageUISettings.Tables[0].Rows[0]["HeaderFontName"].ToString();
                //divHeader.Style["font-size"] = pageUISettings.Tables[0].Rows[0]["HeaderFontSize"].ToString();
                //divHeader.Style["color"] = "#" + pageUISettings.Tables[0].Rows[0]["HeaderForeColor"].ToString();
                //divFooter.Style["background-color"] = "#" + pageUISettings.Tables[0].Rows[0]["FooterBGColor"].ToString();
                //divFooter.Style["font-family"] = pageUISettings.Tables[0].Rows[0]["FooterFontName"].ToString();
                //divFooter.Style["font-size"] = pageUISettings.Tables[0].Rows[0]["FooterFontSize"].ToString();
                //divFooter.Style["color"] = "#" + pageUISettings.Tables[0].Rows[0]["FooterForeColor"].ToString();
                //divHeader.Style["background-color"] = "pageUISettings.Tables[0].Rows[0]["SideBarBGColor"].ToString();
                //divHeader.Style["font-family"] = pageUISettings.Tables[0].Rows[0]["SideBarFontName"].ToString();
                //divHeader.Style["font-size"] = pageUISettings.Tables[0].Rows[0]["SideBarFontSize"].ToString();
                //divHeader.Style["color"] = "#" + pageUISettings.Tables[0].Rows[0]["SideBarForeColor"].ToString();

                if (Convert.ToBoolean(pageUISettings.Tables[0].Rows[0]["HasHeaderLogo"]) == true)
                    imgHeader.ImageUrl = "~/ShowImage.ashx?Type=Header";
                else
                    imgHeader.Visible = false;

                if (Convert.ToBoolean(pageUISettings.Tables[0].Rows[0]["HasFooterLogo"]) == true)
                    imgFooter.ImageUrl = "~/ShowImage.ashx?Type=Footer";
                else
                    imgFooter.Visible = false;

                //if (Convert.ToBoolean(pageUISettings.Tables[0].Rows[0]["HasSideBarLogo"]) == true)
                //    imgSideBar.ImageUrl = "~/ShowImage.ashx?Type=SideBar";
                //else
                //    imgSideBar.Visible = false;
            }
        }

        private void BindSubmenus(int menuId)
        {
            try
            {
                DataSet subMenus = (new UsersDAL()).GetRoleSubMenus(int.Parse(Session["RoleID"].ToString()), menuId);


                if (subMenus != null && subMenus.Tables[0].Rows.Count > 0)
                {
                    rptsubMenu.DataSource = subMenus.Tables[0];
                    rptsubMenu.DataBind();
                }
                else
                {
                    rptsubMenu.DataSource = null;
                    rptsubMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        private void BindMainMenus()
        {
            try
            {
                DataSet mainMenus = (new UsersDAL()).GetRoleMenus(int.Parse(Session["RoleID"].ToString()));
                if (mainMenus != null && mainMenus.Tables[0].Rows.Count > 0)
                {
                    rptMainMenu.DataSource = mainMenus.Tables[0];
                    rptMainMenu.DataBind();
                }
                else
                {
                    rptMainMenu.DataSource = null;
                    rptMainMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        private void ClearCss(Repeater rpt)
        {
            try
            {
                foreach (RepeaterItem ri in rpt.Items)
                {
                    ((LinkButton)ri.FindControl("lnk")).Attributes.Remove("class");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lkbtnSignOut_Click(object sender, EventArgs e)
        {
            int iPOID = 0;
            try
            {
                int x = (new AddCartsDAL()).DeleteTempPartsDocuments(Session.SessionID, iPOID);
                int y = (new AddCartsDAL()).DelChkoutParts_DAL(Session.SessionID);
                Session["UserID"] = null;
                Session["UserName"] = null;
                Session["RoleID"] = null;
                Session["MainMenu"] = null;
                Session["SubMenu"] = null;
                Session["MainMenuID"] = null;
                Session["OrderRoleName"] = null;
                Session.Remove(Session.SessionID);

                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                

                Response.Redirect("~/Default.aspx", true);

            }
            catch (Exception ex)
            {
                throw ex;
            }


            
        }

        protected void rptMainMenu_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "#")
            {
                ClearCss(rptMainMenu);
                Session["MainMenu"] = ((LinkButton)e.Item.FindControl("lnk")).Text;
                Session["MainMenuID"] = e.CommandArgument;
                Session["SubMenu"] = null;
                Response.Redirect(e.CommandName, false);
            }
        }

        protected void rptsubMenu_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            ClearCss(rptsubMenu);
            Session["SubMenu"] = ((LinkButton)e.Item.FindControl("lnk")).Text;
            Response.Redirect(e.CommandName, false);
        }

        protected void rptsubMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lblViewCart = ((LinkButton)e.Item.FindControl("lnk"));
                DataSet ChkExitTemp = (new AddCartsDAL()).ChkMenuPCLSessionExist_DAL(Session.SessionID);
                if (lblViewCart.Text == "View Cart")
                {
                    if (ChkExitTemp.Tables[0].Rows.Count > 0)
                    {lblViewCart.Visible = true;}
                    else
                    { lblViewCart.Visible = false;}
                    
                }
                if (lblViewCart.Text == "Check Out")
                {
                    if (ChkExitTemp.Tables[1].Rows.Count > 0)
                    {lblViewCart.Visible = true;}
                    else
                    {lblViewCart.Visible = false;}
                }
            }

        }

    }
}