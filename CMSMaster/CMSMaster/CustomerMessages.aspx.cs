using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSMaster.DAL;
using System.Data;

namespace CMSMaster
{
    public partial class CustomerMessages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblErr.Text = "";
                if (!IsPostBack)
                {
                    Session["MainMenu"] = "Messages";
                    if (Session["DBName"] != null)
                        BindMessages();
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

        private void BindMessages()
        {
            try
            {
                DataSet messages = (new clsCustomer(Session["DBName"].ToString())).GetCustomerMessages(0);
                if (messages != null && messages.Tables[0].Rows.Count > 0)
                {
                    txtHeaderMsg.Text = messages.Tables[0].Rows[0]["HeaderMsg"].ToString();
                    txtFooterMsg.Text = messages.Tables[0].Rows[0]["FooterMsg"].ToString();
                    txtSideBarMsg.Text = messages.Tables[0].Rows[0]["SideBarMsg"].ToString();
                    txtWelcomeMsg.Text = messages.Tables[0].Rows[0]["WelcomeMsg"].ToString();
                }
                else
                {
                    txtHeaderMsg.Text = "";
                    txtFooterMsg.Text = "";
                    txtSideBarMsg.Text = "";
                    txtWelcomeMsg.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }
                

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveMessages();
        }

        private void SaveMessages()
        {
            try
            {
                clsCustomer dalCustomer = new clsCustomer(Session["DBName"].ToString());
                int custId = int.Parse(Session["CustomerID"].ToString());
                dalCustomer.InsertCustomerMessages(custId, txtHeaderMsg.Text.Trim(), txtFooterMsg.Text.Trim(), txtSideBarMsg.Text.Trim(), txtWelcomeMsg.Text.Trim());
                lblErr.Text = "Messages Are Updated";
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void lbPreview_Click(object sender, EventArgs e)
        {
            SaveMessages();
            Response.Redirect("~/TestPage.aspx?from=message");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                BindMessages();
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }
    }
}