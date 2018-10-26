using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMSBusinessLogic;

namespace TMS
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblDate.Text = DateTime.Now.ToLongDateString();
                if (Session["AppUserID"] != null && Session["UserName"] != null)
                {
                    lblWelcomeMsg.Text = CommonMessages.MSG_WELCOME + Session["UserName"].ToString();
                    lnkLogOff.Visible = true;
                }
                else
                {
                    Response.Redirect("~/log-in", true);
                    return;
                }

                if (Session["GoogleID"] != null && Session["UserEmail"] != null)
                {
                    normalUserPanel.Visible = false;
                    abclogisticsEmployeePanel.Visible = true;
                    spanUserEmail.InnerText = Session["UserEmail"].ToString();
                }
            }
        }

        protected void lnkLogOff_Click(object sender, EventArgs e)
        {
            Session["GlobalUserID"] = null;
            Session["UserName"] = null;
            Session["FirstName"] = null;
            Session["LastName"] = null;
            Session["RoleID"] = null;
            Session["UserEmail"] = null;
            Session["Password"] = null;
            Session["AppUserID"] = null;
            Session["GoogleID"] = null;
            Session.Clear();
            Response.Redirect("~/log-in");
        }
    }
}
