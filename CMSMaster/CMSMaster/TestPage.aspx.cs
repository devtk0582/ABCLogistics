using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMSMaster.DAL;
using System.Drawing;
using System.IO;
using System.Web.UI.HtmlControls;

namespace CMSMaster
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPage();
            }
        }

        private void BindPage()
        {
            try
            {
                DataSet settings = (new clsCustomer(Session["DBName"].ToString())).GetCustomerUIsettings(0);

                if (settings != null && settings.Tables.Count > 1)
                {
                    DataTable pageMsgs = settings.Tables[1];
                    if (pageMsgs != null && pageMsgs.Rows.Count > 0)
                    {
                        lblHeader.Text = pageMsgs.Rows[0]["HeaderMsg"].ToString().Replace("\n", "<br/>");
                        lblFooter.Text = pageMsgs.Rows[0]["FooterMsg"].ToString().Replace("\n", "<br/>");
                        lblSideBar.Text = pageMsgs.Rows[0]["SideBarMsg"].ToString().Replace("\n", "<br/>");
                        lblWelcome.Text = pageMsgs.Rows[0]["WelcomeMsg"].ToString().Replace("\n", "<br/>");
                    }

                    DataTable pageUISettings = settings.Tables[0];
                    if (pageUISettings != null && pageUISettings.Rows.Count > 0)
                    {
                        if (pageUISettings.Rows[0]["HeaderBGColor"].ToString() != "")
                            trHeader.BgColor = pageUISettings.Rows[0]["HeaderBGColor"].ToString();

                        if (pageUISettings.Rows[0]["HeaderFontName"].ToString() != "")
                            lblHeader.Font.Name = pageUISettings.Rows[0]["HeaderFontName"].ToString();

                        if (pageUISettings.Rows[0]["HeaderFontSize"].ToString() != "")
                            lblHeader.Font.Size = FontUnit.Parse(pageUISettings.Rows[0]["HeaderFontSize"].ToString());

                        if (pageUISettings.Rows[0]["HeaderForeColor"].ToString() != "")
                            lblHeader.ForeColor = Color.FromArgb(int.Parse(pageUISettings.Rows[0]["HeaderForeColor"].ToString(), System.Globalization.NumberStyles.HexNumber));

                        if (pageUISettings.Rows[0]["FooterBGColor"].ToString() != "")
                            trFooter.BgColor = pageUISettings.Rows[0]["FooterBGColor"].ToString();

                        if (pageUISettings.Rows[0]["FooterFontName"].ToString() != "")
                            lblFooter.Font.Name = pageUISettings.Rows[0]["FooterFontName"].ToString();

                        if (pageUISettings.Rows[0]["FooterFontSize"].ToString() != "")
                            lblFooter.Font.Size = FontUnit.Parse(pageUISettings.Rows[0]["FooterFontSize"].ToString());

                        if (pageUISettings.Rows[0]["FooterForeColor"].ToString() != "")
                            lblFooter.ForeColor = Color.FromArgb(int.Parse(pageUISettings.Rows[0]["FooterForeColor"].ToString(), System.Globalization.NumberStyles.HexNumber));

                        if (pageUISettings.Rows[0]["SideBarBGColor"].ToString() != "")
                            tdSideBar.BgColor = pageUISettings.Rows[0]["SideBarBGColor"].ToString();

                        if (pageUISettings.Rows[0]["SideBarFontName"].ToString() != "")
                            lblSideBar.Font.Name = pageUISettings.Rows[0]["SideBarFontName"].ToString();

                        if (pageUISettings.Rows[0]["SideBarFontSize"].ToString() != "")
                            lblSideBar.Font.Size = FontUnit.Parse(pageUISettings.Rows[0]["SideBarFontSize"].ToString());

                        if (pageUISettings.Rows[0]["SideBarForeColor"].ToString() != "")
                            lblSideBar.ForeColor = Color.FromArgb(int.Parse(pageUISettings.Rows[0]["SideBarForeColor"].ToString(), System.Globalization.NumberStyles.HexNumber));

                        if (pageUISettings.Rows[0]["HasHeaderLogo"] != null && Convert.ToBoolean(pageUISettings.Rows[0]["HasHeaderLogo"]) == true)
                            imgHeader.ImageUrl = "~/ShowImage.ashx?Type=Header";
                        else
                            imgHeader.Visible = false;

                        if (pageUISettings.Rows[0]["HasFooterLogo"] != null && Convert.ToBoolean(pageUISettings.Rows[0]["HasFooterLogo"]) == true)
                            imgFooter.ImageUrl = "~/ShowImage.ashx?Type=Footer";
                        else
                            imgFooter.Visible = false;

                        if (pageUISettings.Rows[0]["HasSideBarLogo"] != null && Convert.ToBoolean(pageUISettings.Rows[0]["HasSideBarLogo"]) == true)
                            imgSideBar.ImageUrl = "~/ShowImage.ashx?Type=SideBar";
                        else
                            imgSideBar.Visible = false;
                    }
                    else
                    {
                        imgHeader.Visible = false;
                        imgFooter.Visible = false;
                        imgSideBar.Visible = false;
                    }
                }
                else
                {
                    imgHeader.Visible = false;
                    imgFooter.Visible = false;
                    imgSideBar.Visible = false;
                }
                
            }
            catch (Exception ex)
            {
                return;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["from"] == null)
            {
                Response.Redirect("~/CustomerDetails.aspx");
            }
            else
            {
                string from = Request.QueryString["from"].ToString();
                switch (from)
                {
                    case "message":
                        Response.Redirect("~/CustomerMessages.aspx");
                        break;
                    case "font":
                        Response.Redirect("~/CustomerFonts.aspx");
                        break;
                    case "logo":
                        Response.Redirect("~/CustomerLogos.aspx");
                        break;
                }
            }
        }
    }

}