using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSMaster.DAL;
using System.Data;
using System.IO;

namespace CMSMaster
{
    public partial class CustomerLogos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblErr.Text = "";
                if (!IsPostBack)
                {
                    Session["MainMenu"] = "Logos";
                    if (Session["DBName"] != null)
                        BindLogos();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveImages();
        }

        private void SaveImages()
        {
            string fileExt;
            byte[] headerImg, footerImg, sidebarImg;
            try
            {
                if (fuHeader.HasFile)
                {
                    fileExt = "";

                    fileExt = System.IO.Path.GetExtension(fuHeader.FileName);
                    if (fileExt.ToLower() != ".gif" && fileExt.ToLower() != ".jpg" && fileExt.ToLower() != ".jpeg")
                    {
                        lblErr.Text = "Only GIF and JPG image are accepted.";
                        return;
                    }
                    headerImg = fuHeader.FileBytes;
                    imgHeader.Visible = true;
                    ibtnDelHeaderLogo.Visible = true;
                }
                else
                {
                    headerImg = null;
                }

                if (fuFooter.HasFile)
                {
                    fileExt = "";

                    fileExt = System.IO.Path.GetExtension(fuFooter.FileName);
                    if (fileExt.ToLower() != ".gif" && fileExt.ToLower() != ".jpg" && fileExt.ToLower() != ".jpeg")
                    {
                        lblErr.Text = "Only GIF and JPG image are accepted.";
                        return;
                    }

                    footerImg = fuFooter.FileBytes;
                    imgFooter.Visible = true;
                    ibtnDelFooterLogo.Visible = true;
                }
                else
                {
                    footerImg = null;
                }

                if (fuSideBar.HasFile)
                {
                    fileExt = "";

                    fileExt = System.IO.Path.GetExtension(fuSideBar.FileName);
                    if (fileExt.ToLower() != ".gif" && fileExt.ToLower() != ".jpg" && fileExt.ToLower() != ".jpeg")
                    {
                        lblErr.Text = "Only GIF and JPG image are accepted.";
                        return;
                    }
                    sidebarImg = fuSideBar.FileBytes;
                    imgSideBar.Visible = true;
                    ibtnDelSideBarLogo.Visible = true;
                }
                else
                {
                    sidebarImg = null;
                }

                clsCustomer dalCustomer = new clsCustomer(Session["DBName"].ToString());
                dalCustomer.InsertCustomerLogos(int.Parse(Session["CustomerID"].ToString()), Session["CustomerName"].ToString(), headerImg, footerImg, sidebarImg);
                lblErr.Text = "Logos Are Updated.";
                BindLogos();

            }

            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        private void BindLogos()
        {
            try
            {
                DataSet logos = (new clsCustomer(Session["DBName"].ToString())).GetCustomerLogos();
                if (logos != null && logos.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToBoolean(logos.Tables[0].Rows[0]["HasHeaderLogo"]) == false)
                    {
                        imgHeader.Visible = false;
                        ibtnDelHeaderLogo.Visible = false;
                    }

                    if (Convert.ToBoolean(logos.Tables[0].Rows[0]["HasFooterLogo"]) == false)
                    {
                        imgFooter.Visible = false;
                        ibtnDelFooterLogo.Visible = false;
                    }

                    if (Convert.ToBoolean(logos.Tables[0].Rows[0]["HasSideBarLogo"]) == false)
                    {
                        imgSideBar.Visible = false;
                        ibtnDelSideBarLogo.Visible = false;
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
                lblErr.Text = ex.Message;
            }
        }

        protected void ibtnDelHeaderLogo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                (new clsCustomer(Session["DBName"].ToString())).DeleteCustomerLogo(1);
                imgHeader.Visible = false;
                ibtnDelHeaderLogo.Visible = false;
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void ibtnDelFooterLogo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                (new clsCustomer(Session["DBName"].ToString())).DeleteCustomerLogo(2);
                imgFooter.Visible = false;
                ibtnDelFooterLogo.Visible = false;
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void ibtnDelSideBarLogo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                (new clsCustomer(Session["DBName"].ToString())).DeleteCustomerLogo(3);
                imgSideBar.Visible = false;
                ibtnDelSideBarLogo.Visible = false;
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        protected void lbPreview_Click(object sender, EventArgs e)
        {
            SaveImages();
            Response.Redirect("~/TestPage.aspx?from=logo");
        }

    }
}