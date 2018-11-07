using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.UserControls
{
    public partial class ctlErrormsg : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lnkBtnok.Attributes.Add("onclick", " HidePopup('" + MPEConfirm.ClientID + "');");
        }

       

        public void popup()
        {
            
            pnlConfirm.Visible = true;
            MPEConfirm.Show();
        }

        public string heading
        {
            get
            {
                if ((ViewState["_heading"] != null))
                {
                    return Convert.ToString(ViewState["_heading"]);
                }
                else
                {
                    return string.Empty;
                }

            }
            set { ViewState["_heading"] = value; lblHeader.Text = value; }
        }
        public string msg
        {
            get
            {
                if ((ViewState["_msg"] != null))
                {
                    return Convert.ToString(ViewState["_msg"]);
                }
                else
                {
                    return string.Empty;
                }

            }
            set { ViewState["_msg"] = value; lblErrorMagConfirm.Text = value; }
        }
    }
}