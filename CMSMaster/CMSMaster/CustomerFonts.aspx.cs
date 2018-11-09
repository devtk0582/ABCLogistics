using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Text;
using System.Drawing;
using System.Collections;
using CMSMaster.DAL;
using System.Data;
using System.Web.UI.HtmlControls;

namespace CMSMaster
{
    public partial class CustomerFonts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblErr.Text = "";
                if (!IsPostBack)
                {
                    Session["MainMenu"] = "Fonts";
                    if (Session["DBName"] != null)
                    {
                        BindFontNames();
                        BindFontSizes();
                        BindUISettings();
                    }
                    if (Session["CustomerName"] != null)
                    {
                        ((Label)Master.FindControl("CustomerName")).Text = Session["CustomerName"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }

        }

        private void BindFontNames()
        {
            try
            {
                InstalledFontCollection fonts = new InstalledFontCollection();
                foreach (FontFamily font in fonts.Families)
                {
                    ddlHeaderFontNames.Items.Add(font.Name);
                    ddlFooterFontNames.Items.Add(font.Name);
                    ddlSideBarFontNames.Items.Add(font.Name);
                }
                ddlHeaderFontNames.Items.Insert(0, new ListItem("Select Font Name", "-1"));
                ddlFooterFontNames.Items.Insert(0, new ListItem("Select Font Name", "-1"));
                ddlSideBarFontNames.Items.Insert(0, new ListItem("Select Font Name", "-1"));
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        private void BindFontSizes()
        {
            try
            {
                //Hashtable ht = GetEnumForBind(typeof(FontSize));

                Dictionary<string, string> fontSizes = GetFontSizes();

                ddlHeaderFontSizes.DataSource = fontSizes;
                ddlHeaderFontSizes.DataTextField = "Value";
                ddlHeaderFontSizes.DataValueField = "Key";
                ddlHeaderFontSizes.DataBind();

                ddlFooterFontSizes.DataSource = fontSizes;
                ddlFooterFontSizes.DataTextField = "Value";
                ddlFooterFontSizes.DataValueField = "Key";
                ddlFooterFontSizes.DataBind();

                ddlSideBarFontSizes.DataSource = fontSizes;
                ddlSideBarFontSizes.DataTextField = "Value";
                ddlSideBarFontSizes.DataValueField = "Key";
                ddlSideBarFontSizes.DataBind();

                ddlHeaderFontSizes.Items.Insert(0, new ListItem("Select Font Size", "-1"));
                ddlFooterFontSizes.Items.Insert(0, new ListItem("Select Font Size", "-1"));
                ddlSideBarFontSizes.Items.Insert(0, new ListItem("Select Font Size", "-1"));

            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        private void BindUISettings()
        {
            try
            {
                DataSet settings = (new clsCustomer(Session["DBName"].ToString())).GetCustomerUIsettings(0);
                if (settings != null && settings.Tables[0].Rows.Count > 0)
                {
                    if (settings.Tables[0].Rows[0]["HeaderBGColor"].ToString() != "")
                    {
                        txtHeaderBGColor.Text = settings.Tables[0].Rows[0]["HeaderBGColor"].ToString();
                        lblHeaderBG.BackColor = Color.FromArgb(int.Parse(settings.Tables[0].Rows[0]["HeaderBGColor"].ToString(), System.Globalization.NumberStyles.HexNumber));
                        lblHeaderFont.BackColor = Color.FromArgb(int.Parse(settings.Tables[0].Rows[0]["HeaderBGColor"].ToString(), System.Globalization.NumberStyles.HexNumber));
                    }

                    if(settings.Tables[0].Rows[0]["HeaderFontName"].ToString() != "")
                    {
                    ddlHeaderFontNames.Items.FindByText(settings.Tables[0].Rows[0]["HeaderFontName"].ToString()).Selected = true;
                    lblHeaderFont.Font.Name = settings.Tables[0].Rows[0]["HeaderFontName"].ToString();
                    }

                    if (settings.Tables[0].Rows[0]["HeaderFontSize"].ToString() != "")
                    {
                        txtHeaderFontSize.Text = settings.Tables[0].Rows[0]["HeaderFontSize"].ToString();
                        lblHeaderFont.Font.Size = FontUnit.Parse(settings.Tables[0].Rows[0]["HeaderFontSize"].ToString());
                    }

                    if (settings.Tables[0].Rows[0]["HeaderForeColor"].ToString() != "")
                    {
                        txtHeaderFontColor.Text = settings.Tables[0].Rows[0]["HeaderForeColor"].ToString();
                        lblHeaderFontColor.BackColor = Color.FromArgb(int.Parse(settings.Tables[0].Rows[0]["HeaderForeColor"].ToString(), System.Globalization.NumberStyles.HexNumber));
                        lblHeaderFont.ForeColor = Color.FromArgb(int.Parse(settings.Tables[0].Rows[0]["HeaderForeColor"].ToString(), System.Globalization.NumberStyles.HexNumber));
                    }



                    if (settings.Tables[0].Rows[0]["FooterBGColor"].ToString() != "")
                    {
                        txtFooterBGColor.Text = settings.Tables[0].Rows[0]["FooterBGColor"].ToString();
                        lblFooterBG.BackColor = Color.FromArgb(int.Parse(settings.Tables[0].Rows[0]["FooterBGColor"].ToString(), System.Globalization.NumberStyles.HexNumber));
                        lblFooterFont.BackColor = Color.FromArgb(int.Parse(settings.Tables[0].Rows[0]["FooterBGColor"].ToString(), System.Globalization.NumberStyles.HexNumber));
                    }

                    if (settings.Tables[0].Rows[0]["FooterFontName"].ToString() != "")
                    {
                        ddlFooterFontNames.Items.FindByText(settings.Tables[0].Rows[0]["FooterFontName"].ToString()).Selected = true;
                        lblFooterFont.Font.Name = settings.Tables[0].Rows[0]["FooterFontName"].ToString();
                    }

                    if (settings.Tables[0].Rows[0]["FooterFontSize"].ToString() != "")
                    {
                        //ddlFooterFontSizes.SelectedValue = settings.Tables[0].Rows[0]["FooterFontSize"].ToString();
                        txtFooterFontSize.Text = settings.Tables[0].Rows[0]["FooterFontSize"].ToString();
                        lblFooterFont.Font.Size = FontUnit.Parse(settings.Tables[0].Rows[0]["FooterFontSize"].ToString());
                    }

                    if (settings.Tables[0].Rows[0]["FooterForeColor"].ToString() != "")
                    {
                        txtFooterFontColor.Text = settings.Tables[0].Rows[0]["FooterForeColor"].ToString();
                        lblFooterFontColor.BackColor = Color.FromArgb(int.Parse(settings.Tables[0].Rows[0]["FooterForeColor"].ToString(), System.Globalization.NumberStyles.HexNumber));
                        lblFooterFont.ForeColor = Color.FromArgb(int.Parse(settings.Tables[0].Rows[0]["FooterForeColor"].ToString(), System.Globalization.NumberStyles.HexNumber));
                    }

                    if (settings.Tables[0].Rows[0]["SideBarBGColor"].ToString() != "")
                    {
                        txtSideBarBGColor.Text = settings.Tables[0].Rows[0]["SideBarBGColor"].ToString();
                        lblSideBarBG.BackColor = Color.FromArgb(int.Parse(settings.Tables[0].Rows[0]["SideBarBGColor"].ToString(), System.Globalization.NumberStyles.HexNumber));
                        lblSideBarFont.BackColor = Color.FromArgb(int.Parse(settings.Tables[0].Rows[0]["SideBarBGColor"].ToString(), System.Globalization.NumberStyles.HexNumber));
                    }

                    if (settings.Tables[0].Rows[0]["SideBarFontName"].ToString() != "")
                    {
                        ddlSideBarFontNames.Items.FindByText(settings.Tables[0].Rows[0]["SideBarFontName"].ToString()).Selected = true;
                        lblSideBarFont.Font.Name = settings.Tables[0].Rows[0]["SideBarFontName"].ToString();
                    }
                    //ddlSideBarFontSizes.SelectedValue = settings.Tables[0].Rows[0]["SideBarFontSize"].ToString();

                    if (settings.Tables[0].Rows[0]["SideBarFontSize"].ToString() != "")
                    {
                        txtSideBarFontSize.Text = settings.Tables[0].Rows[0]["SideBarFontSize"].ToString();
                        lblSideBarFont.Font.Size = FontUnit.Parse(settings.Tables[0].Rows[0]["SideBarFontSize"].ToString());
                    }

                    if (settings.Tables[0].Rows[0]["SideBarForeColor"].ToString() != "")
                    {
                        txtSideBarFontColor.Text = settings.Tables[0].Rows[0]["SideBarForeColor"].ToString();
                        lblSideBarFontColor.BackColor = Color.FromArgb(int.Parse(settings.Tables[0].Rows[0]["SideBarForeColor"].ToString(), System.Globalization.NumberStyles.HexNumber));
                        lblSideBarFont.ForeColor = Color.FromArgb(int.Parse(settings.Tables[0].Rows[0]["SideBarForeColor"].ToString(), System.Globalization.NumberStyles.HexNumber));
                    }
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        public Hashtable GetEnumForBind(Type enumeration)
        {
            string[] names = Enum.GetNames(enumeration);
            Array values = Enum.GetValues(enumeration);
            Hashtable ht = new Hashtable();
            for (int i = 0; i < names.Length; i++)
            {
                ht.Add(Convert.ToInt32(values.GetValue(i)).ToString(), names[i]);
            }
            return ht;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveFonts();
        }

        private void SaveFonts()
        {
            try
            {
                clsCustomer dalCustomer = new clsCustomer(Session["DBName"].ToString());
                int custId = int.Parse(Session["CustomerID"].ToString());
                string headerFontName = "", headerFontSize = "", footerFontName = "", footerFontSize = "", sidebarFontName = "", sidebarFontSize = "";
                if (ddlHeaderFontNames.SelectedIndex > 0)
                {
                    headerFontName = ddlHeaderFontNames.SelectedItem.Text;
                }
                if (ddlFooterFontNames.SelectedIndex > 0)
                {
                    footerFontName = ddlFooterFontNames.SelectedItem.Text;
                }
                if (ddlSideBarFontNames.SelectedIndex > 0)
                {
                    sidebarFontName = ddlSideBarFontNames.SelectedItem.Text;
                }

                if (rblHeaderFontSizeByName.Checked == true)
                {
                    if (ddlHeaderFontSizes.SelectedIndex > 0)
                    {
                        headerFontSize = ddlHeaderFontSizes.SelectedValue;
                    }
                }
                else
                {
                    if (txtHeaderFontSize.Text.Trim() != "")
                    {
                        headerFontSize = txtHeaderFontSize.Text;
                    }
                }

                if (rblFooterFontSizeByName.Checked == true)
                {
                    if (ddlFooterFontSizes.SelectedIndex > 0)
                    {
                        footerFontSize = ddlFooterFontSizes.SelectedValue;
                    }
                }
                else
                {
                    if (txtFooterFontSize.Text.Trim() != "")
                    {
                        footerFontSize = txtFooterFontSize.Text;
                    }
                }

                if (rblSideBarFontSizeByName.Checked == true)
                {
                    if (ddlSideBarFontSizes.SelectedIndex > 0)
                    {
                        sidebarFontSize = ddlSideBarFontSizes.SelectedValue;
                    }
                }
                else
                {
                    if (txtSideBarFontSize.Text.Trim() != "")
                    {
                        sidebarFontSize = txtSideBarFontSize.Text;
                    }
                }


                dalCustomer.InsertCustomerUISettings(custId, txtHeaderBGColor.Text.Trim(), headerFontName, headerFontSize, txtHeaderFontColor.Text.Trim(),
                    txtFooterBGColor.Text.Trim(), footerFontName, footerFontSize, txtFooterFontColor.Text.Trim(),
                    txtSideBarBGColor.Text.Trim(), sidebarFontName, sidebarFontSize, txtSideBarFontColor.Text.Trim());
                lblErr.Text = "UI Settings Are Updated";
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }
        protected void ddlHeaderFontNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlHeaderFontNames.SelectedIndex > 0)
                {
                    lblHeaderFont.Font.Name = ddlHeaderFontNames.Text;
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void ddlHeaderFontSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblHeaderFontSizeByName.Checked == true && ddlHeaderFontSizes.SelectedIndex > 0)
                {
                    //lblHeaderFont.Font.Size = new FontUnit((FontSize)Enum.Parse(typeof(FontSize), ddlHeaderFontSizes.Text));
                    lblHeaderFont.Font.Size = FontUnit.Parse(ddlHeaderFontSizes.SelectedItem.Text);
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void txtHeaderFontColor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtHeaderFontColor.Text.Trim() != "")
                {
                    lblHeaderFont.ForeColor = Color.FromArgb(int.Parse(txtHeaderFontColor.Text.Trim(), System.Globalization.NumberStyles.HexNumber));
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void ddlFooterFontNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlFooterFontNames.SelectedIndex > 0)
                {
                    lblFooterFont.Font.Name = ddlFooterFontNames.Text;
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void ddlFooterFontSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblFooterFontSizeByName.Checked == true && ddlFooterFontSizes.SelectedIndex > 0)
                {
                    //lblFooterFont.Font.Size = new FontUnit((FontSize)Enum.Parse(typeof(FontSize), ddlFooterFontSizes.Text));
                    lblFooterFont.Font.Size = FontUnit.Parse(ddlFooterFontSizes.SelectedItem.Text);
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void txtFooterFontColor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFooterFontColor.Text.Trim() != "")
                {
                    lblFooterFont.ForeColor = Color.FromArgb(int.Parse(txtFooterFontColor.Text.Trim(), System.Globalization.NumberStyles.HexNumber));
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void ddlSideBarFontNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSideBarFontNames.SelectedIndex > 0)
                {
                    lblSideBarFont.Font.Name = ddlSideBarFontNames.Text;
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void ddlSideBarFontSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblSideBarFontSizeByName.Checked == true && ddlSideBarFontSizes.SelectedIndex > 0)
                {
                    //lblSideBarFont.Font.Size = new FontUnit((FontSize)Enum.Parse(typeof(FontSize), ddlSideBarFontSizes.Text));
                    lblSideBarFont.Font.Size = FontUnit.Parse(ddlSideBarFontSizes.SelectedItem.Text);
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }

        }

        protected void txtSideBarFontColor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSideBarFontColor.Text.Trim() != "")
                {
                    lblSideBarFont.ForeColor = Color.FromArgb(int.Parse(txtSideBarFontColor.Text.Trim(), System.Globalization.NumberStyles.HexNumber));
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void txtHeaderBGColor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtHeaderBGColor.Text.Trim() != "")
                {
                    lblHeaderFont.BackColor = Color.FromArgb(int.Parse(txtHeaderBGColor.Text.Trim(), System.Globalization.NumberStyles.HexNumber));
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void txtFooterBGColor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFooterBGColor.Text.Trim() != "")
                {
                    lblFooterFont.BackColor = Color.FromArgb(int.Parse(txtFooterBGColor.Text.Trim(), System.Globalization.NumberStyles.HexNumber));
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void txtSideBarBGColor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSideBarBGColor.Text.Trim() != "")
                {
                    lblSideBarFont.BackColor = Color.FromArgb(int.Parse(txtSideBarBGColor.Text.Trim(), System.Globalization.NumberStyles.HexNumber));
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        private Dictionary<string, string> GetFontSizes()
        {
            try
            {
                Dictionary<string, string> fontSizes = new Dictionary<string, string>()
            
            { 
                {"4","XXSmall"},
                {"5", "XSmall"},
                {"6", "Small"},
                {"7", "Medium"},
                {"8", "Large"},
                {"9", "XLarge"},
                {"10", "XXLarge"}
            };

                return fontSizes;
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
                return null;
            }
        }

        protected void lbPreview_Click(object sender, EventArgs e)
        {
            SaveFonts();
            Response.Redirect("TestPage.aspx?from=font");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            BindUISettings();
            rblHeaderFontSizeByUnits.Checked = true;
            rblFooterFontSizeByUnits.Checked = true;
            rblSideBarFontSizeByUnits.Checked = true;
        }

    }
}