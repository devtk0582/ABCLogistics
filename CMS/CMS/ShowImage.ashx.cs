using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ABCLogistics.DataAccess;
using System.Data;
using CMS.DAL;

namespace CMS
{
    /// <summary>
    /// Summary description for ShowImage1
    /// </summary>
    public class ShowImage1 : IHttpHandler
    {

        //string strConn = ConfigurationManager.AppSettings["CMSConnectionString"].ToString();
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["Type"] != null)
            {
                string type = context.Request.QueryString["Type"].ToString();
                
                int logoId = 0;
                    switch (type)
                    {
                        case "Header":
                            logoId = 1;
                            break;
                        case "Footer":
                            logoId = 2;
                            break;
                        case "SideBar":
                            logoId = 3;
                            break;
                    }
                byte[] logo = (new UISettingsDAL()).GetCustomerLogo(logoId);
                if(logo != null)
                    context.Response.BinaryWrite(logo);
                else 
                    context.Response.Write("");
                context.Response.End();
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}