using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.DAL;

namespace CMS
{
    public partial class Catalog1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddNewProduct_Click(object sender, EventArgs e)
        {
            userControlAddNewProduct.popupwindowAddNewSize();
        }

        public void getTextBox(string newDesc)
        {

            txtbxDescXXX.Text = newDesc;
        }
    }
}