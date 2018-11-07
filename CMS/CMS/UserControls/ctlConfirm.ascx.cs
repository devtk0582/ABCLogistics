using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.DAL;

namespace CMS.UserControls
{
    public partial class ctlConfirm : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void lnkBtnok_Click(object sender, EventArgs e)
        {
            try
            {
                int y = (new AddCartsDAL()).DelChkoutParts_DAL(Session.SessionID);
                Session["SubMenu"] = "Products";
                Response.Redirect("../Catalog/Products.aspx");
            }
            catch (Exception)
            {
                throw;
            }
            
        }

       
        public void popup(string iOrderId, string labelNumber)
        {
            lblErrorMagConfirm.Text = "Order Num"+" "+ iOrderId+" "+ "Placed Successfully";
            pnlConfirm.Visible = true;
            iframePreview.Attributes["src"] = "../Labels/" + labelNumber + ".pdf";
            MPEConfirm.Show();
        }
       
    }
}