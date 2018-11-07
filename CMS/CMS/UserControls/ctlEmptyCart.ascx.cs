using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.DAL;

namespace CMS.UserControls
{
    public partial class ctlEmptyCart : System.Web.UI.UserControl
    {
        //public event EventHandler EmptyCartBG;
 
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkBtnok_Click(object sender, EventArgs e)
        {
            int iPOID = 0;
            try
            {
                int x = (new AddCartsDAL()).DeleteTempPartsDocuments(Session.SessionID, iPOID);
                int y = (new AddCartsDAL()).DelChkoutParts_DAL(Session.SessionID);

                Session["SubMenu"] = "Products";
                Response.Redirect("Products.aspx");
                
               // EmptyCartBG(sender, e);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void popup()
        {
            panelEC.Visible = true;
            ModlPopupEC.Show(); 
        }
       
    }
}