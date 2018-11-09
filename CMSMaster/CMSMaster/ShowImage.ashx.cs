using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ABCLogistics.DataAccess;
using System.Web.SessionState;
using CMSMaster.DAL;

namespace CMSMaster
{
    /// <summary>
    /// Summary description for ShowImage
    /// </summary>
    public class ShowImage : IHttpHandler, IReadOnlySessionState
    {
        //string strConn = System.Configuration.ConfigurationSettings.AppSettings["CMSCustomerConnectionString"].ToString();
        public void ProcessRequest(HttpContext context)
        {
            try
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
                    byte[] logo = (new clsCustomer(context.Session["DBName"].ToString())).GetCustomerLogo(logoId);
                    if (logo != null)
                        context.Response.BinaryWrite(logo);
                    else
                        context.Response.Write("");
                    context.Response.End();
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("");
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