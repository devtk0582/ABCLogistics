using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CMSMaster
{
    public partial class CMSMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    lblDate.Text = DateTime.Now.ToLongDateString();
                    if (Session["UserID"] != null)
                    {
                        lblWelcomeMessage.Text = "Welcome, " + Session["UserName"].ToString();
                    }

                    if (Session["MainMenu"] != null)
                    {
                        foreach (MenuItem mi in menus.Items)
                        {
                            if (mi.Value == Session["MainMenu"].ToString())
                            {
                                mi.Selected = true;
                            }
                            else
                                mi.Selected = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErr.Visible = true;
                lblErr.Text = ex.Message;
            }
        
        }

        //protected void lnkChangePwd_Click(object sender, EventArgs e)
        //{

        //}

        protected void lnkLogOff_Click(object sender, EventArgs e)
        {
            try
            {
                Session["RoleID"] = null;
                Session["UserName"] = null;
                Session["UserID"] = null;
                Session["MainMenu"] = null;
                Session["CustomerID"] = null;
                Session["CustomerName"] = null;
                Session["DBName"] = null;
                Session.Clear();
                Response.Redirect("~/Default.aspx", false);
            }
            catch (Exception ex)
            {
                lblErr.Visible = true;
                lblErr.Text = ex.Message;
            }
        }
    }
}