using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS.UserControls
{
    public partial class ContentManager : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BindContent(string header, string content)
        {
            lblHeader.Text = header;
            ltlContent.Text = content;
        }
    }
}